using exhibition_management_backend.DTO;
using exhibition_management_backend.Models;
using exhibition_management_backend.Services.Exhibition;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace exhibition_management_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExhibitionController : ControllerBase
    {
        private readonly IExhibitionService _service;

        public ExhibitionController(IExhibitionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExhibitions()
        {
            var exhibitions = await _service.GetAllExhibitions();
            return Ok(exhibitions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExhibitionById(int id)
        {
            var exhibition = await _service.GetExhibitionById(id);
            return Ok(exhibition);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExhibition([FromBody] ExhibitionAddressDTO exhibitionAddressDTO)
        {
            if (exhibitionAddressDTO == null)
            {
                return BadRequest("Invalid input.");
            }

            try
            {
                var result = await _service.CreateExhibitionAsync(exhibitionAddressDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExhibition(int id)
        {
            try
            {
                var result = await _service.DeleteExhibition(id);

                if (result)
                    return NoContent();

                return NotFound($"Exhibition with Id {id} not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while deleting the exhibition.");
            }
        }


    }
}
