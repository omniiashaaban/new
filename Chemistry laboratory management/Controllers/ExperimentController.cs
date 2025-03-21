using Chemistry_laboratory_management.Dtos;
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
       
        public async Task<IActionResult> GetAllExperiments()
        {
            var result = await _experimentRepository.GetAllAsync();
            var query = result.AsQueryable()
                              .Include(e => e.Equipments)
                              .Include(e => e.Chemicals);

            var experimentsList = await query.ToListAsync();

            return Ok(experimentsList);
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

           var savedexperiment = await _experimentRepository.GetByIdAsync(experiment.Id); 

            return CreatedAtAction(nameof(GetExperiment), new { id = savedexperiment.Id }, savedexperiment);
        }

       

       

    }
}