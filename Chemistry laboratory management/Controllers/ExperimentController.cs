using Chemistry_laboratory_management.Dtos;
using laboratory.DAL.DTOs;
using laboratory.DAL.Models;
using laboratory.DAL.Repository;
using LinkDev.Facial_Recognition.BLL.Helper.Errors;
using Microsoft.AspNetCore.Mvc;
namespace Chemistry_laboratory_management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperimentController : ControllerBase
    {
        private readonly GenericRepository<Experiment> _experimentRepository;

        public ExperimentController(GenericRepository<Experiment> experimentRepository)
        {
            _experimentRepository = experimentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExperimentDTO>>> GetExperiments()
        {
            var experiments = await _experimentRepository.GetAllAsync();
            var experimentDTOs = experiments.Select(e => new ExperimentDTO
            {
                Name = e.Name,
                Date = e.ExperimentDate,
                SupervisorId = e.SupervisorID,
                ChemicalsUsedIds = e.Chemicals.Select(c => c.Id).ToList(),
                EquipmentUsedIds = e.Equipments.Select(eq => eq.Id).ToList()
            }).ToList();

            return Ok(experimentDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExperimentDTO>> GetExperiment(int id)
        {
            var experiment = await _experimentRepository.GetByIdAsync(id);
            if (experiment == null)
                return NotFound(new ApiResponse(404, "Experiment not found."));

            var experimentDTO = new ExperimentDTO
            {
                Name = experiment.Name,
                Date = experiment.ExperimentDate,
                SupervisorId = experiment.SupervisorID,
                ChemicalsUsedIds = experiment.Chemicals.Select(c => c.Id).ToList(),
                EquipmentUsedIds = experiment.Equipments.Select(eq => eq.Id).ToList()
            };

            return Ok(experimentDTO);
        }

        [HttpPost]
        public async Task<ActionResult> AddExperiment([FromBody] ExperimentDTO experimentDTO)
        {
            if (string.IsNullOrWhiteSpace(experimentDTO.Name))
                return BadRequest(new ApiResponse(400, "Experiment Name is required."));

            var experiment = new Experiment
            {
                Name = experimentDTO.Name,
                ExperimentDate = experimentDTO.Date,
                SupervisorID = experimentDTO.SupervisorId,
                Chemicals = experimentDTO.ChemicalsUsedIds.Select(id => new Chemical { Id = id }).ToList(),
                Equipments = experimentDTO.EquipmentUsedIds.Select(id => new Equipment { Id = id }).ToList()
            };

            await _experimentRepository.AddAsync(experiment);
            return CreatedAtAction(nameof(GetExperiment), new { id = experiment.Id }, experimentDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateExperiment(int id, [FromBody] ExperimentDTO experimentDTO)
        {
            var existingExperiment = await _experimentRepository.GetByIdAsync(id);
            if (existingExperiment == null)
                return NotFound(new ApiResponse(404, "Experiment not found."));

            existingExperiment.Name = experimentDTO.Name;
            existingExperiment.ExperimentDate = experimentDTO.Date;
            existingExperiment.SupervisorID = experimentDTO.SupervisorId;
            existingExperiment.Chemicals = experimentDTO.ChemicalsUsedIds.Select(cid => new Chemical { Id = cid }).ToList();
            existingExperiment.Equipments = experimentDTO.EquipmentUsedIds.Select(eqid => new Equipment { Id = eqid }).ToList();

            await _experimentRepository.UpdateAsync(existingExperiment);
            return Ok(new ApiResponse(200, "Experiment updated successfully."));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExperiment(int id)
        {
            var existingExperiment = await _experimentRepository.GetByIdAsync(id);
            if (existingExperiment == null)
                return NotFound(new ApiResponse(404, "Experiment not found."));

            await _experimentRepository.DeleteAsync(id);
            return Ok(new ApiResponse(200, "Experiment deleted successfully."));
        }

        [HttpGet("Supervisor/{supervisorId}")]
        public async Task<ActionResult<IEnumerable<ExperimentDTO>>> GetExperimentsBySupervisor(int supervisorId)
        {
            var experiments = await _experimentRepository.GetAllAsync();
            var exbysupervisor = experiments.Where(e => e.SupervisorID == supervisorId);
            var experimentDTOs = exbysupervisor.Select(e => new ExperimentDTO
            {
                Name = e.Name,
                Date = e.ExperimentDate,
                SupervisorId = e.SupervisorID,
                ChemicalsUsedIds = e.Chemicals.Select(c => c.Id).ToList(),
                EquipmentUsedIds = e.Equipments.Select(eq => eq.Id).ToList()
            }).ToList();

            return Ok(experimentDTOs);
        }
        [HttpGet("Equipment/{equipmentId}")]
        public async Task<ActionResult<IEnumerable<ExperimentDTO>>> GetExperimentsByEquipment(int equipmentId)
        {
            var experiments = await _experimentRepository.GetAllAsync();
            var experimentsbyequi = experiments.Where(e => e.Equipments.Any(eq => eq.Id == equipmentId));
            var experimentDTOs = experimentsbyequi.Select(e => new ExperimentDTO
            {
                Name = e.Name,
                Date = e.ExperimentDate,
                SupervisorId = e.SupervisorID,
                ChemicalsUsedIds = e.Chemicals.Select(c => c.Id).ToList(),
                EquipmentUsedIds = e.Equipments.Select(eq => eq.Id).ToList()
            }).ToList();

            return Ok(experimentDTOs);
        }
        [HttpGet("{experimentId}/chemicals")]
        public async Task<ActionResult<IEnumerable<ChemicalDTO>>> GetChemicalsByExperiment(int experimentId)
        {
            var experiment = await _experimentRepository.GetAllAsync();

            var exp=experiment.FirstOrDefault(e => e.Id == experimentId);

            if (exp == null)
                return NotFound("Experiment not found");

            var chemicalDTOs = exp.Chemicals.Select(c => new ChemicalDTO
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return Ok(chemicalDTOs);
        }

    }
}