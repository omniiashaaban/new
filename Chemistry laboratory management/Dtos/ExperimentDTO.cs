﻿using System.ComponentModel.DataAnnotations;

namespace Chemistry_laboratory_management.Dtos
{
    public class ExperimentDTO
    {
        public int Id { get;  set; }
        [Required]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int SupervisorId { get; set; } // المشرف (UserId)

        public List<int> ChemicalsUsedIds { get; set; } = new List<int>();
        public List<int> EquipmentUsedIds { get; set; } = new List<int>();
    }
    public class ExperimentDTOPost
    {
        [Required]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int SupervisorId { get; set; } // المشرف (UserId)

        public List<int> ChemicalsUsedIds { get; set; } = new List<int>();
        public List<int> EquipmentUsedIds { get; set; } = new List<int>();
    }
    public class ExperimentDTOResponse
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int SupervisorId { get; set; } // المشرف (UserId)

        public List<string> ChemicalsUsedNames { get; set; } = new List<string>();
        public List<string> EquipmentUsedNames { get; set; } = new List<string>();
    }
    public class ExperimentDTOSimpleResponse
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }

    }
