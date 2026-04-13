using System.ComponentModel.DataAnnotations;

namespace VehicleVault.Models
{
    public class Vehicle
    {
        public int VehicleID { get; set; }

        public string VehicleName { get; set; }

        public int Price { get; set; }

        public int Mileage { get; set; }

        public string Engine { get; set; }

        public string FuelType { get; set; }

        public string SafetyFeatures { get; set; }

        public string ImagePath { get; set; } // 👈 NEW
    }
}