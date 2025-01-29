using exhibition_management_backend.DTO;
using Microsoft.AspNetCore.Mvc;

namespace exhibition_management_backend.Services.Exhibition
{
    public interface IExhibitionService
    {
        Task<IEnumerable<ExhibitionDTO>> GetAllExhibitions();
        Task<IEnumerable<ExhibitionAddressDTO>> GetExhibitionById(int id);
        Task<object> CreateExhibitionAsync(ExhibitionAddressDTO exhibitionAddressDTO);

        Task<bool> DeleteExhibition(int id);

        Task<int> UpdateExhibitionAsync(int id, ExhibitionAddressDTO exhibitionAddressDTO);
    }
}
