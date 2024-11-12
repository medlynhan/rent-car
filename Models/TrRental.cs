using System;
using System.Collections.Generic;

namespace ProyekRentCar.Models
{
    public partial class TrRental
    {
        public TrRental()
        {
            TrPayments = new HashSet<TrPayment>();
        }

        public string RentalId { get; set; } = null!;
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public bool? PaymentStatus { get; set; }
        public string? CustomerId { get; set; }
        public string? CarId { get; set; }

        public virtual MsCar? Car { get; set; }
        public virtual MsCustomer? Customer { get; set; }
        public virtual ICollection<TrPayment> TrPayments { get; set; }
    }
}
