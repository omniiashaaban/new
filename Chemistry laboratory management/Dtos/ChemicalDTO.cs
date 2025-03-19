using System;
using System.ComponentModel.DataAnnotations;

namespace laboratory.DAL.DTOs
{
    public class ChemicalDTO
    {
        public int Id { get; internal set; }
        public string Name { get; set; }
            public double Quantity { get; set; }
            public DateTime ExpiryDate { get; set; }
            public string HazardInformation { get; set; }
            public string StorageLocation { get; set; }
    
    }
    }
