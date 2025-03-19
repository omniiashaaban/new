//using laboratory.DAL.Models;
//using laboratory.DAL.Repository;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace laboratory.BLL.Services
//{
//    using Microsoft.AspNetCore.Hosting;
//    using Microsoft.AspNetCore.Http;
//    using System;
//    using System.IO;
//    using System.Threading.Tasks;

//    namespace laboratory.BLL.Services
//    {
//        public class ExperimentService : IExperimentService
//        {
//            private readonly GenericRepository<Experiment> _experimentRepository;
//            private readonly GenericRepository<Chemical> _materialRepository; // Assuming you have a material repository
//            private readonly IWebHostEnvironment _env;

//            public ExperimentService(
//                GenericRepository<Experiment> experimentRepository,
//                GenericRepository<Chemical> materialRepository,
//                IWebHostEnvironment env)
//            {
//                _experimentRepository = experimentRepository;
//                _materialRepository = materialRepository;
//                _env = env;
//            }

//            public async Task<Experiment> GetExperimentByIdAsync(int id)
//            {
//                return await _experimentRepository.GetByIdAsync(id);
//            }

//            public async Task<IEnumerable<Experiment>> GetAllExperimentsAsync()
//            {
//                return await _experimentRepository.GetAllAsync();
//            }

//            public async Task AddExperimentAsync(Experiment experiment)
//            {
//                await _experimentRepository.AddAsync(experiment);
//            }

//            public async Task UpdateExperimentAsync(Experiment experiment)
//            {
//                await _experimentRepository.UpdateAsync(experiment);
//            }

//            public async Task DeleteExperimentAsync(int id)
//            {
//                await _experimentRepository.DeleteAsync(id);
//            }

//         //   public async Task<bool> UploadPdfAsync(int experimentId, IFormFile file)
//         //   {
//         //       var experiment = await _experimentRepository.GetByIdAsync(experimentId);
//         //       if (experiment == null) return false;

//         //       // Validate that the file is a PDF and not empty
//         //       if (file == null || file.Length == 0 || Path.GetExtension(file.FileName).ToLower() != ".pdf")
//         //           return false;

//         //       // Generate a unique file name
//         //       var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
//         //       var folderPath = Path.Combine(_env.WebRootPath, "Experiments");

//         //       // Create the directory if it doesn't exist
//         //       if (!Directory.Exists(folderPath))
//         //           Directory.CreateDirectory(folderPath);

//         //       // Define the file path
//         //       var filePath = Path.Combine(folderPath, fileName);

//         //       // Save the file to the disk
//         //       using (var stream = new FileStream(filePath, FileMode.Create))
//         //       {
//         //           await file.CopyToAsync(stream);
//         //       }

//         //       // Update the experiment with the file path
//         ////       experiment.PdfFilePath = $"/Experiments/{fileName}";
//         //       await _experimentRepository.UpdateAsync(experiment);

//         //       return true;
//         //   }

//            // Additional logic to handle material validation and stock updating
//            public async Task<bool> CheckMaterialAvailability(int materialId, double quantityRequired)
//            {
//                var material = await _materialRepository.GetByIdAsync(materialId);
//                if (material == null || material.Quantity < quantityRequired)
//                {
//                    return false; // Chemical is not available or doesn't have enough stock
//                }
//                return true;
//            }

//            public async Task DecreaseMaterialStock(int materialId, int quantityRequired)
//            {
//                var material = await _materialRepository.GetByIdAsync(materialId);
//                if (material != null && material.Quantity >= quantityRequired)
//                {
//                    material.Quantity -= quantityRequired; // Decrease stock
//                    await _materialRepository.UpdateAsync(material); // Save the changes
//                }
//            }
//        }
//    }
//}
