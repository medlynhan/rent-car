using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyekRentCar.Models
{
    public partial class MsCustomer
    {
        public MsCustomer()
        {
            TrRentals = new HashSet<TrRental>();
        }

        public string CustomerId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;

        [Column("phone_number")]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        [Column("driver_license_number")]
        public string? DriverLicenseNumber { get; set; }

        public virtual ICollection<TrRental> TrRentals { get; set; }
    }
}
