using exhibition_management_backend.DTO;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


    }
}
