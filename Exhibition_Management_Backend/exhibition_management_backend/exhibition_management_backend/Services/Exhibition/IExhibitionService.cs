using exhibition_management_backend.DTO;

namespace exhibition_management_backend.Services.Exhibition
{
    public interface IExhibitionService
    {
        Task<IEnumerable<ExhibitionDTO>> GetAllExhibitions();
        Task<IEnumerable<ExhibitionAddressDTO>> GetExhibitionById(int id);
    }
}
