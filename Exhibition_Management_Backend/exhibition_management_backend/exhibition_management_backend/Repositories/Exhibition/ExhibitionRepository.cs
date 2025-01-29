using exhibition_management_backend.DTO;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using exhibition_management_backend.Helpers;
using System.Data;

namespace exhibition_management_backend.Repositories.Exhibition
{
    public class ExhibitionRepository : IExhibitionRepository
    {
        private readonly string _connectionString;
        private readonly NpgsqlConnection connection;

        public ExhibitionRepository(IConfiguration configuration)
        {
            
            _connectionString = configuration.GetConnectionString("WebApiDatabase");

            connection = new NpgsqlConnection(_connectionString);
            connection.Open();
        }

        public async Task<IEnumerable<ExhibitionDTO>> GetAllExhibitions()
        {
            string commandText = @"
            SELECT 
                id, 
                venuename, 
                TO_CHAR(startdate, 'YYYY-MM-DD') AS startdate, 
                TO_CHAR(enddate, 'YYYY-MM-DD') AS enddate, 
                starttime, 
                endtime, 
                bannerimage 
            FROM exhibition";

            var exhibitions = await connection.QueryAsync<ExhibitionDTO>(commandText);
            return exhibitions.ToList();
        }

        public async Task<IEnumerable<ExhibitionAddressDTO>> GetExhibitionById(int id)
        {
            string commandText = @"
            SELECT 
                e.id, 
                e.venuename,
                a.addressline1, 
                a.addressline2, 
                a.addressline3, 
                a.googlemapslink, 
                TO_CHAR(e.startdate, 'YYYY-MM-DD') AS startdate, 
                TO_CHAR(e.enddate, 'YYYY-MM-DD') AS enddate, 
                e.starttime, 
                e.endtime, 
                e.nooftables,
                e.description,
                e.venueimages,
                e.bannerimage,
                e.layoutimage
            FROM 
                exhibition e 
            INNER JOIN 
                address a 
            ON 
                e.addressid = a.id
            where e.id = @Id";

            var exhibition = await connection.QueryAsync<ExhibitionAddressDTO>(commandText, new { Id = id });
            return exhibition;
        }

        public async Task<int> CreateExhibitionAsync(ExhibitionAddressDTO exhibitionAddressDTO)
        {
            const string commandText = "CALL sp_insertexhibition(@Venuename, @AddressLine1, @AddressLine2, @AddressLine3, @GoogleMapsLink, " +
                                       "@StartDate, @EndDate, @StartTime, @EndTime, @NoOfTables, @Description, @VenueImages, @BannerImage, @LayoutImage);";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("Venuename", exhibitionAddressDTO.Venuename, DbType.String);
                parameters.Add("AddressLine1", exhibitionAddressDTO.AddressLine1, DbType.String);
                parameters.Add("AddressLine2", exhibitionAddressDTO.AddressLine2, DbType.String);
                parameters.Add("AddressLine3", exhibitionAddressDTO.AddressLine3, DbType.String);
                parameters.Add("GoogleMapsLink", exhibitionAddressDTO.GoogleMapsLink, DbType.String);
                parameters.Add("StartDate", Converter.ConvertStringToDateOnlyLegacy(exhibitionAddressDTO.Startdate), DbType.Date);
                parameters.Add("EndDate", Converter.ConvertStringToDateOnlyLegacy(exhibitionAddressDTO.Enddate), DbType.Date);
                parameters.Add("StartTime", exhibitionAddressDTO.Starttime, DbType.Time);
                parameters.Add("EndTime", exhibitionAddressDTO.Endtime, DbType.Time);
                parameters.Add("NoOfTables", exhibitionAddressDTO.Nooftables ?? 0, DbType.Int32);
                parameters.Add("Description", exhibitionAddressDTO.Description ?? string.Empty, DbType.String);
                parameters.Add("VenueImages", exhibitionAddressDTO.Venueimages ?? Array.Empty<string>(), DbType.Object);
                parameters.Add("BannerImage", exhibitionAddressDTO.Bannerimage ?? string.Empty, DbType.String);
                parameters.Add("LayoutImage", exhibitionAddressDTO.Layoutimage ?? string.Empty, DbType.String);

                try
                {
                    return await connection.ExecuteAsync(commandText, parameters);
                }
                catch (PostgresException pgEx)
                {
                    Console.WriteLine($"Postgres Error: {pgEx.Message}\nDetail: {pgEx.Detail}");
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                    throw;
                }
            }
        }


        public async Task<int> UpdateExhibition(int id, ExhibitionAddressDTO exhibitionAddressDTO)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = await connection.BeginTransactionAsync())
                {
                    try
                    {
                        // Check if the exhibition exists
                        const string checkExhibitionQuery = "SELECT addressid FROM exhibition WHERE id = @id";
                        int? existingAddressId = await connection.ExecuteScalarAsync<int?>(checkExhibitionQuery, new { id });

                        if (existingAddressId == null)
                        {
                            return 0; // Exhibition not found
                        }

                        // Check if the address exists
                        const string checkAddressQuery = "SELECT id FROM address WHERE id = @id";
                        int? addressId = await connection.ExecuteScalarAsync<int?>(checkAddressQuery, new { id = existingAddressId });

                        if (addressId.HasValue)
                        {
                            // Update existing address
                            const string updateAddressQuery = @"
                        UPDATE address 
                        SET addressline1 = @AddressLine1, 
                            addressline2 = @AddressLine2, 
                            addressline3 = @AddressLine3, 
                            googlemapslink = @GoogleMapsLink 
                        WHERE id = @Id";

                            await connection.ExecuteAsync(updateAddressQuery, new
                            {
                                Id = addressId.Value,
                                exhibitionAddressDTO.AddressLine1,
                                exhibitionAddressDTO.AddressLine2,
                                exhibitionAddressDTO.AddressLine3,
                                exhibitionAddressDTO.GoogleMapsLink
                            });
                        }
                        else
                        {
                            // Insert new address if it doesn't exist
                            const string insertAddressQuery = @"
                        INSERT INTO address (addressline1, addressline2, addressline3, googlemapslink) 
                        VALUES (@AddressLine1, @AddressLine2, @AddressLine3, @GoogleMapsLink) 
                        RETURNING id";

                            addressId = await connection.ExecuteScalarAsync<int>(insertAddressQuery, new
                            {
                                exhibitionAddressDTO.AddressLine1,
                                exhibitionAddressDTO.AddressLine2,
                                exhibitionAddressDTO.AddressLine3,
                                exhibitionAddressDTO.GoogleMapsLink
                            });

                            // Update exhibition with the new address ID
                            const string updateExhibitionAddressQuery = "UPDATE exhibition SET addressid = @AddressId WHERE id = @Id";
                            await connection.ExecuteAsync(updateExhibitionAddressQuery, new { Id = id, AddressId = addressId });
                        }

                        // Update exhibition details
                        const string updateExhibitionQuery = @"
                        UPDATE exhibition 
                        SET venuename = @Venuename, 
                            startdate = @Startdate, 
                            enddate = @Enddate, 
                            starttime = @Starttime, 
                            endtime = @Endtime, 
                            nooftables = @Nooftables, 
                            description = @Description, 
                            venueimages = @Venueimages, 
                            bannerimage = @Bannerimage, 
                            layoutimage = @Layoutimage 
                        WHERE id = @Id";

                        await connection.ExecuteAsync(updateExhibitionQuery, new
                        {
                            Id = id,
                            exhibitionAddressDTO.Venuename,
                            Startdate = Converter.ConvertStringToDateOnlyLegacy(exhibitionAddressDTO.Startdate),
                            Enddate = Converter.ConvertStringToDateOnlyLegacy(exhibitionAddressDTO.Enddate),
                            exhibitionAddressDTO.Starttime,
                            exhibitionAddressDTO.Endtime,
                            exhibitionAddressDTO.Nooftables,
                            exhibitionAddressDTO.Description,
                            Venueimages = exhibitionAddressDTO.Venueimages ?? new string[] { },  // Pass as an array
                            exhibitionAddressDTO.Bannerimage,
                            exhibitionAddressDTO.Layoutimage
                        });


                        await transaction.CommitAsync();
                        return 1; // Success
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        Console.WriteLine($"Repository Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                        throw;
                    }
                }
            }
        }





        public async Task<int> DeleteExhibition(int id)
        {
            const string commandText = "DELETE FROM Exhibition WHERE Id = @Id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Int32);

                try
                {
                    return await connection.ExecuteAsync(commandText, parameters);
                }
                catch (PostgresException pgEx)
                {
                    Console.WriteLine($"Postgres Error: {pgEx.Message}\nDetail: {pgEx.Detail}");
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Repository Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                    throw;
                }
            }
        }




    }
}
