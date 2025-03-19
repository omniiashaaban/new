using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace laboratory.DAL.Models
{
    public class Experiment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime ExperimentDate { get; set; }     

        // المشرف على التجربة
        public int SupervisorID { get; set; }
        [ForeignKey("SupervisorID")]
        public User Supervisor { get; set; }

        // العلاقة: تجربة قد تستخدم عدة مواد كيميائية
        public ICollection<Chemical> Chemicals { get; set; }

        // العلاقة: تجربة قد تستخدم عدة معدات
        public ICollection<Equipment> Equipments { get; set; }
    }
}
