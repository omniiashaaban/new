using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratory.DAL.Models
{
    public class Chemical
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double Quantity { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string HazardInformation { get; set; }

        public string StorageLocation { get; set; }

        // العلاقة: مادة كيميائية يمكن استخدامها في عدة تجارب
        public ICollection<Experiment> Experiments { get; set; }
    }
}
