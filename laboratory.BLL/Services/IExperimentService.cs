using laboratory.DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratory.BLL.Services
{
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    namespace laboratory.BLL.Services
    {
        public interface IExperimentService
        {
            // Get an experiment by its ID
            Task<Experiment> GetExperimentByIdAsync(int id);

            // Get all experiments
            Task<IEnumerable<Experiment>> GetAllExperimentsAsync();

            // Add a new experiment
            Task AddExperimentAsync(Experiment experiment);

            // Update an existing experiment
            Task UpdateExperimentAsync(Experiment experiment);

            // Delete an experiment by ID
            Task DeleteExperimentAsync(int id);

            // Upload a PDF file for an experiment
            Task<bool> UploadPdfAsync(int experimentId, IFormFile file);
        }
    }



}
