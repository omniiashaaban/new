using System.ComponentModel.DataAnnotations;

namespace Chemistry_laboratory_management.Dtos
{
    public class EquipmentDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Status { get; set; } // متاحة، تحتاج إلى صيانة  >> Available || need a maintenance

        public DateTime LastMaintenanceDate { get; set; }

        public DateTime NextMaintenanceDate { get; set; }
    }
}
