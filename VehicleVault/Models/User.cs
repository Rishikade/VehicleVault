using System.ComponentModel.DataAnnotations;

namespace VehicleVault.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}