using System.ComponentModel.DataAnnotations;

namespace Chemistry_laboratory_management.Dtos
{
    public class EquipmentDTO
    {
        public int Id { get; internal set; }
        [Required]
        public string Name { get; set; }

        public int Status { get; set; } //متاحة، تحتاج إلى صيانة  >> Available =1 || need a maintenance=0
        public DateTime LastMaintenanceDate { get; set; }

        public DateTime NextMaintenanceDate { get; set; }
    }
}
