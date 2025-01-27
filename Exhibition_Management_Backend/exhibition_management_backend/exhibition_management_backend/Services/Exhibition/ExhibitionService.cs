using exhibition_management_backend.DTO;
using exhibition_management_backend.Helpers;
using exhibition_management_backend.Models;
using exhibition_management_backend.Repositories.Exhibition;
using Microsoft.AspNetCore.Mvc;

namespace exhibition_management_backend.Services.Exhibition
{
    public class ExhibitionService : IExhibitionService
    {
        private readonly IExhibitionRepository _repository;

        public ExhibitionService(IExhibitionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ExhibitionAddressDTO>> GetExhibitionById(int id)
        {
            var exhibition = await _repository.GetExhibitionById(id);
            return exhibition;
        }

        public async Task<IEnumerable<ExhibitionDTO>> GetAllExhibitions()
        {
           
            var exhibitions = await _repository.GetAllExhibitions();
            return exhibitions.ToList();
        }

        public async Task<object> CreateExhibitionAsync(ExhibitionAddressDTO exhibitionAddressDTO)
        {
            if (string.IsNullOrWhiteSpace(exhibitionAddressDTO.Venuename) ||
                string.IsNullOrWhiteSpace(exhibitionAddressDTO.AddressLine1))
            {
                throw new ArgumentException("Venue name and Address Line 1 are required.");
            }

            var result = await _repository.CreateExhibitionAsync(exhibitionAddressDTO);
            return new { Success = true, Message = "Exhibition created successfully.", Data = result };
        }

    }
}
