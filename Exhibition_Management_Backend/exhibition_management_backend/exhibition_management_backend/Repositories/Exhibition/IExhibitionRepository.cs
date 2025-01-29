using exhibition_management_backend.DTO;
using exhibition_management_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace exhibition_management_backend.Repositories.Exhibition
{
    public interface IExhibitionRepository
    {
        Task<IEnumerable<ExhibitionDTO>> GetAllExhibitions();

        Task<IEnumerable<ExhibitionAddressDTO>> GetExhibitionById(int id);
        Task<int> CreateExhibitionAsync(ExhibitionAddressDTO exhibitionAddressDTO);

        Task<int> DeleteExhibition(int id);

        Task<int> UpdateExhibition(int id, ExhibitionAddressDTO exhibitionAddressDTO);
    }

}
