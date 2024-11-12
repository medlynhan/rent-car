using System;
using System.Collections.Generic;

namespace ProyekRentCar.Models
{
    public partial class MsCar
    {
        public MsCar()
        {
            MsCarImages = new HashSet<MsCarImage>();
            TrMaintenances = new HashSet<TrMaintenance>();
            TrRentals = new HashSet<TrRental>();
        }

        public string CarId { get; set; } = null!;
        public string? Name { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }
        public string? LicensePlate { get; set; }
        public int? NumberOfCarSeats { get; set; }
        public string? Transmission { get; set; }
        public decimal? PricePerDay { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<MsCarImage> MsCarImages { get; set; }
        public virtual ICollection<TrMaintenance> TrMaintenances { get; set; }
        public virtual ICollection<TrRental> TrRentals { get; set; }
    }
}
