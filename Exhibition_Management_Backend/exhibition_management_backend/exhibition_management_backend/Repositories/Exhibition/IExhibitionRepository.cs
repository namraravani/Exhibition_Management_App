using exhibition_management_backend.DTO;
using exhibition_management_backend.Models;

namespace exhibition_management_backend.Repositories.Exhibition
{
    public interface IExhibitionRepository
    {
        Task<IEnumerable<ExhibitionDTO>> GetAllExhibitions();

        Task<IEnumerable<ExhibitionAddressDTO>> GetExhibitionById(int id);
    }
}
