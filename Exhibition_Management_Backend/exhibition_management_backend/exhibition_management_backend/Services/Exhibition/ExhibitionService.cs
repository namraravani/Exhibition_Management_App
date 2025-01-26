using exhibition_management_backend.DTO;
using exhibition_management_backend.Repositories.Exhibition;

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
            // Convert to a list to ensure proper serialization
            var exhibitions = await _repository.GetAllExhibitions();
            return exhibitions.ToList();
        }

        
    }
}
