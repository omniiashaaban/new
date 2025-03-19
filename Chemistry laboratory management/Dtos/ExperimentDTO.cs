namespace Chemistry_laboratory_management.Dtos
{
    public class ExperimentDTO
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int SupervisorId { get; set; } // المشرف (UserId)

        public List<int> ChemicalsUsedIds { get; set; } = new List<int>();
        public List<int> EquipmentUsedIds { get; set; } = new List<int>();
    }
}
