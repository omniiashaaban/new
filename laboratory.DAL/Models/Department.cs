using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace laboratory.DAL.Models
{
    public class Department
    { 
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

       
        public ICollection<Experiment> Experiments { get; set; } = new HashSet<Experiment>();

        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>(); 
        public ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}
