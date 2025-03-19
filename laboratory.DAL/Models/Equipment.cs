using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratory.DAL.Models
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Status { get; set; } //متاحة، تحتاج إلى صيانة  >> Available || need a maintenance

        public DateTime LastMaintenanceDate { get; set; }

        public DateTime NextMaintenanceDate { get; set; }

        // العلاقة: معدات يمكن استخدامها في عدة تجارب
        public ICollection<Experiment> Experiments { get; set; }
    }
}
