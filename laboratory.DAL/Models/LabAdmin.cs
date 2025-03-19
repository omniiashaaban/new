using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratory.DAL.Models
{
   public class LabAdmin 
    {
        public int Id { get; set; }
        [Required]
        public string AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser User { get; set; }

        [Required]
        public string Specialty { get; set; }

        public string Email { get; set; }
        public ICollection<Chemical> Materials { get; set; } = new List<Chemical>();
    }
}
