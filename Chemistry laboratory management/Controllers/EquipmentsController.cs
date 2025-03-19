using Chemistry_laboratory_management.Dtos;
using laboratory.DAL.Models;
using laboratory.DAL.Repository;
using LinkDev.Facial_Recognition.BLL.Helper.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chemistry_laboratory_management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly GenericRepository<Equipment> _equipmentRepository;

        public EquipmentController(GenericRepository<Equipment> equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentDTO>>> GetEquipments()
        {
            var equipments = await _equipmentRepository.GetAllAsync();
            var equipmentDTOs = equipments.Select(e => new EquipmentDTO
            {
                Name = e.Name,
                Status = e.Status,
                LastMaintenanceDate = e.LastMaintenanceDate,
                NextMaintenanceDate = e.NextMaintenanceDate
            }).ToList();

            return Ok(equipmentDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentDTO>> GetEquipment(int id)
        {
            var equipment = await _equipmentRepository.GetByIdAsync(id);
            if (equipment == null)
                return NotFound(new ApiResponse(404, "Equipment not found."));

            var equipmentDTO = new EquipmentDTO
            {
                Name = equipment.Name,
                Status = equipment.Status,
                LastMaintenanceDate = equipment.LastMaintenanceDate,
                NextMaintenanceDate = equipment.NextMaintenanceDate
            };

            return Ok(equipmentDTO);
        }

        [HttpPost]
        public async Task<ActionResult> AddEquipment([FromBody] EquipmentDTO equipmentDTO)
        {
            if (string.IsNullOrWhiteSpace(equipmentDTO.Name))
                return BadRequest(new ApiResponse(400, "Equipment Name is required."));

            var equipment = new Equipment
            {
                Name = equipmentDTO.Name,
                Status = equipmentDTO.Status,
                LastMaintenanceDate = equipmentDTO.LastMaintenanceDate,
                NextMaintenanceDate = equipmentDTO.NextMaintenanceDate
            };

            await _equipmentRepository.AddAsync(equipment);
            return CreatedAtAction(nameof(GetEquipment), new { id = equipment.Id }, equipmentDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEquipment(int id, [FromBody] EquipmentDTO equipmentDTO)
        {
            var existingEquipment = await _equipmentRepository.GetByIdAsync(id);
            if (existingEquipment == null)
                return NotFound(new ApiResponse(404, "Equipment not found."));

            existingEquipment.Name = equipmentDTO.Name;
            existingEquipment.Status = equipmentDTO.Status;
            existingEquipment.LastMaintenanceDate = equipmentDTO.LastMaintenanceDate;
            existingEquipment.NextMaintenanceDate = equipmentDTO.NextMaintenanceDate;

            await _equipmentRepository.UpdateAsync(existingEquipment);
            return Ok(new ApiResponse(200, "Equipment updated successfully."));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEquipment(int id)
        {
            var existingEquipment = await _equipmentRepository.GetByIdAsync(id);
            if (existingEquipment == null)
                return NotFound(new ApiResponse(404, "Equipment not found."));

            await _equipmentRepository.DeleteAsync(id);
            return Ok(new ApiResponse(200, "Equipment deleted successfully."));
        }
        [HttpGet("equipment/maintenance")]
        public async Task<ActionResult<IEnumerable<EquipmentDTO>>> GetEquipmentNeedingMaintenance()
        {
            var maintenanceRequiredEquipment = await _equipmentRepository.GetAllAsync();

            var MRE = maintenanceRequiredEquipment.Where(e => e.Status == "Need a maintenance");

            var equipmentDTOs = maintenanceRequiredEquipment.Select(e => new EquipmentDTO
            {
                Id = e.Id,
                Name = e.Name,
                Status = e.Status,
                LastMaintenanceDate = e.LastMaintenanceDate,
                NextMaintenanceDate = e.NextMaintenanceDate
            }).ToList();

            return Ok(equipmentDTOs);
        }
    }
}
