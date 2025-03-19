using laboratory.DAL.DTOs;
using laboratory.DAL.Models;
using laboratory.DAL.Repository;
using LinkDev.Facial_Recognition.BLL.Helper.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Chemistry_laboratory_management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChemicalsController : ControllerBase
    {
        private readonly GenericRepository<Chemical> _chemicalRepository;

        public ChemicalsController(GenericRepository<Chemical> chemicalRepository)
        {
            _chemicalRepository = chemicalRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChemicalDTO>>> GetChemicals()
        {
            var chemicals = await _chemicalRepository.GetAllAsync();
            var chemicalDTOs = chemicals.Select(c => new ChemicalDTO
            {
                Id = c.Id,
                Name = c.Name,
                Quantity = c.Quantity,
                ExpiryDate = c.ExpiryDate,
                HazardInformation = c.HazardInformation,
                StorageLocation = c.StorageLocation,
            }).ToList();

            return Ok(chemicalDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChemicalDTO>> GetChemical(int id)
        {
            var chemical = await _chemicalRepository.GetByIdAsync(id);
            if (chemical == null)
                return NotFound(new ApiResponse(404, "Chemical not found."));

            var chemicalDTO = new ChemicalDTO
            {
              
                Name = chemical.Name,
                Quantity = chemical.Quantity,
                ExpiryDate = chemical.ExpiryDate,
                HazardInformation = chemical.HazardInformation,
                StorageLocation = chemical.StorageLocation,
            };

            return Ok(chemicalDTO);
        }

        [HttpPost]
        public async Task<ActionResult> AddChemical([FromBody] ChemicalDTO chemicalDTO)
        {
            if (string.IsNullOrWhiteSpace(chemicalDTO.Name) || chemicalDTO.Quantity < 0)
            {
                return BadRequest(new ApiResponse(400, "Name and valid Quantity are required."));
            }

            var chemical = new Chemical
            {
                Name = chemicalDTO.Name,
                Quantity = chemicalDTO.Quantity,
                ExpiryDate = chemicalDTO.ExpiryDate,
                HazardInformation = chemicalDTO.HazardInformation,
                StorageLocation = chemicalDTO.StorageLocation
            };

            await _chemicalRepository.AddAsync(chemical);
            return CreatedAtAction(nameof(GetChemical), new { id = chemical.Id }, chemicalDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateChemical(int id, [FromBody] ChemicalDTO chemicalDTO)
        {
            if (id != chemicalDTO.Id)
                return BadRequest(new ApiResponse(400, "ID mismatch."));

            if (string.IsNullOrWhiteSpace(chemicalDTO.Name) || chemicalDTO.Quantity < 0)
            {
                return BadRequest(new ApiResponse(400, "Name and valid Quantity are required."));
            }

            var existingChemical = await _chemicalRepository.GetByIdAsync(id);
            if (existingChemical == null)
                return NotFound(new ApiResponse(404, "Chemical not found."));

            existingChemical.Name = chemicalDTO.Name;
            existingChemical.Quantity = chemicalDTO.Quantity;
            existingChemical.ExpiryDate = chemicalDTO.ExpiryDate;
            existingChemical.HazardInformation = chemicalDTO.HazardInformation;
            existingChemical.StorageLocation = chemicalDTO.StorageLocation;

            await _chemicalRepository.UpdateAsync(existingChemical);

            return Ok(new ApiResponse(200, "Chemical updated successfully."));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChemical(int id)
        {
            var existingChemical = await _chemicalRepository.GetByIdAsync(id);
            if (existingChemical == null)
                return NotFound(new ApiResponse(404, "Chemical not found."));

            await _chemicalRepository.DeleteAsync(id);

            return Ok(new ApiResponse(200, "Chemical deleted successfully."));
        }
        [HttpGet("chemicals/expiring")]
        public async Task<ActionResult<IEnumerable<ChemicalDTO>>> GetExpiringChemicals()
        {
            var today = DateTime.UtcNow;
            var thresholdDate = today.AddMonths(1); // اعتبري المواد التي ستنتهي خلال شهر

            var expiringChemicals = await _chemicalRepository.GetAllAsync();

            var expiring = expiringChemicals.Where(c => c.ExpiryDate <= thresholdDate);

            var chemicalDTOs = expiringChemicals.Select(c => new ChemicalDTO
            {
                Id = c.Id,
                Name = c.Name,
                ExpiryDate = c.ExpiryDate
            }).ToList();

            return Ok(chemicalDTOs);
        }
    }
}