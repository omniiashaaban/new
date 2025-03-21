using Chemistry_laboratory_management.Dtos;
using iText.Commons.Actions.Contexts;
using laboratory.DAL.DTOs;
using laboratory.DAL.Models;
using laboratory.DAL.Repository;
using LinkDev.Facial_Recognition.BLL.Helper.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Chemistry_laboratory_management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperimentController : ControllerBase
    {
        private readonly GenericRepository<Experiment> _experimentRepository;
        private readonly GenericRepository<Chemical> _chemicalRepository;
        private readonly GenericRepository<Equipment> _equipmentRepository;
        private readonly GenericRepository<User> _userRepository;
       


        public ExperimentController(GenericRepository<Experiment> experimentRepository, GenericRepository<Chemical> chemicalRepository, GenericRepository<Equipment> equipmentRepository, GenericRepository<User> userRepository)
        {
            _experimentRepository = experimentRepository;
            _chemicalRepository = chemicalRepository;
            _equipmentRepository = equipmentRepository;
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAllExperiments()
        {
            var experiments = await _experimentRepository.GetAllWithIncludesAsync(
                e => e.Equipments,
                e => e.Chemicals
            );

            if (experiments == null || !experiments.Any())
                return NotFound(new ApiResponse(404, "No experiments found."));

            var experimentsDTO = experiments.Select(experiment => new ExperimentDTOResponse
            {
                Name = experiment.Name,
                Date = experiment.ExperimentDate,
                SupervisorId = experiment.SupervisorID,
                ChemicalsUsedNames = experiment.Chemicals.Select(c => c.Name).ToList(),
                EquipmentUsedNames = experiment.Equipments.Select(eq => eq.Name).ToList()
            }).ToList();

            return Ok(experimentsDTO);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetExperiment(int id)
        {
            var experiment = await _experimentRepository.GetByIdWithIncludesAsync(
                e => e.Id == id,
                e => e.Equipments,
                e => e.Chemicals
            );

            if (experiment == null)
                return NotFound(new ApiResponse(404, "Experiment not found."));

            var experimentDTO = new ExperimentDTOResponse
            {
                Id = id,
                Name = experiment.Name,
                Date = experiment.ExperimentDate,
                SupervisorId = experiment.SupervisorID,
                ChemicalsUsedNames = experiment.Chemicals.Select(c => c.Name).ToList(),
                EquipmentUsedNames = experiment.Equipments.Select(eq => eq.Name).ToList()
            };

            return Ok(experimentDTO);
        }

        [HttpPost]
        public async Task<ActionResult> AddExperiment([FromBody] ExperimentDTOPost experimentDTO)
        {
            if (string.IsNullOrWhiteSpace(experimentDTO.Name))
                return BadRequest(new ApiResponse(400, "Experiment Name is required."));

            // التحقق من وجود المشرف في قاعدة البيانات
            var supervisor = await _userRepository.GetByIdAsync(experimentDTO.SupervisorId);
            if (supervisor == null)
                return NotFound(new ApiResponse(404, "Supervisor not found."));

            // جلب المواد من قاعدة البيانات باستخدام GetByIdAsync لكل ID
            var chemicals = new List<Chemical>();
            foreach (var chemicalId in experimentDTO.ChemicalsUsedIds)
            {
                var chemical = await _chemicalRepository.GetByIdAsync(chemicalId);
                if (chemical == null)
                    return NotFound(new ApiResponse(404, $"Chemical with ID {chemicalId} not found."));
                chemicals.Add(chemical);
            }

            // جلب الأجهزة من قاعدة البيانات باستخدام GetByIdAsync لكل ID
            var equipments = new List<Equipment>();
            foreach (var equipmentId in experimentDTO.EquipmentUsedIds)
            {
                var equipment = await _equipmentRepository.GetByIdAsync(equipmentId);
                if (equipment == null)
                    return NotFound(new ApiResponse(404, $"Equipment with ID {equipmentId} not found."));
                equipments.Add(equipment);
            }

            // إنشاء كيان التجربة
            var experiment = new Experiment
            {
                Name = experimentDTO.Name,
                ExperimentDate = experimentDTO.Date,
                SupervisorID = experimentDTO.SupervisorId,
                Chemicals = chemicals,
                Equipments = equipments
            };

            await _experimentRepository.AddAsync(experiment);
            return CreatedAtAction(nameof(GetExperiment), new { id =experiment.Id }, experimentDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWxperiment(int id)
        {
            var existingExperiment = await _experimentRepository.GetByIdAsync(id);
            if (existingExperiment == null)
                return NotFound(new ApiResponse(404, "Experiment not found."));

            await _experimentRepository.DeleteAsync(id);

            return Ok(new ApiResponse(200, "Experiment deleted successfully."));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExperiment(int id, ExperimentDTOPost experimentDTO)
        {
            var experiment = await _experimentRepository.GetByIdWithIncludesAsync(
                e => e.Id == id,
                e => e.Equipments,
                e => e.Chemicals
            );

            if (experiment == null)
                return NotFound(new ApiResponse(404, "Experiment not found."));

            // التحقق من الـ Equipments
            if (experimentDTO.EquipmentUsedIds != null && experimentDTO.EquipmentUsedIds.Any())
            {
                var allEquipments = await _equipmentRepository.GetAllWithIncludesAsync();
                var existingEquipments = allEquipments.Where(eq => experimentDTO.EquipmentUsedIds.Contains(eq.Id)).ToList();

                var missingEquipments = experimentDTO.EquipmentUsedIds.Except(existingEquipments.Select(eq => eq.Id)).ToList();
                if (missingEquipments.Any())
                {
                    return BadRequest(new ApiResponse(400, $"Some Equipments not found: {string.Join(", ", missingEquipments)}"));
                }

                experiment.Equipments = existingEquipments;
            }

            // التحقق من الـ Chemicals
            if (experimentDTO.ChemicalsUsedIds != null && experimentDTO.ChemicalsUsedIds.Any())
            {
                var allChemicals = await _chemicalRepository.GetAllWithIncludesAsync();
                var existingChemicals = allChemicals.Where(c => experimentDTO.ChemicalsUsedIds.Contains(c.Id)).ToList();

                var missingChemicals = experimentDTO.ChemicalsUsedIds.Except(existingChemicals.Select(c => c.Id)).ToList();
                if (missingChemicals.Any())
                {
                    return BadRequest(new ApiResponse(400, $"Some Chemicals not found: {string.Join(", ", missingChemicals)}"));
                }

                experiment.Chemicals = existingChemicals;
            }

            // تحديث البيانات
            experiment.Name = experimentDTO.Name;
            experiment.ExperimentDate = experimentDTO.Date;
            experiment.SupervisorID = experimentDTO.SupervisorId;

            await _experimentRepository.UpdateAsync(experiment);

            return Ok(new ApiResponse(200, "Experiment updated successfully."));
        }


        [HttpGet("Supervisor/{supervisorId}")]
        public async Task<ActionResult<IEnumerable<string>>> GetExperimentNamesBySupervisor(int supervisorId)
        {
            var experiments = await _experimentRepository.GetByConditionAsync(e => e.SupervisorID == supervisorId);

            var experimentDTOs = experiments.Select(e => new ExperimentDTOSimpleResponse
            {
                Id = e.Id,
                Name = e.Name
            }).ToList();

            return Ok(experimentDTOs);
        }


     
    }
}