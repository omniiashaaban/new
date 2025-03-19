using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratory.DAL.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Role { get; set; } // طالب، أستاذ، فني

        public int AccessLevel { get; set; } // مستوى الوصول إلى المعمل

        // العلاقة: يمكن للمستخدم الإشراف على عدة تجارب
        public ICollection<Experiment> SupervisedExperiments { get; set; }
    }
}
