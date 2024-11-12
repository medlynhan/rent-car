using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyekRentCar.Models;

namespace ProyekRentCar.Controllers
{
    public class HistoryController : Controller
    {
        private readonly RentCarsContext _context;

        public HistoryController(RentCarsContext context)
        {
            _context = context;
        }

        public IActionResult History()
        {
            // CustomerId dari session
            var customerId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("Login", "Account");
            }

            // Ambil data rental berdasarkan CustomerId
            var rentalHistory = _context.TrRentals
                .Include(r => r.Car)
                .Where(r => r.CustomerId == customerId)
                .Select(r => new
                {
                    RentalDate = r.RentalDate,
                    ReturnDate = r.ReturnDate,
                    CarName = r.Car.Name,
                    PricePerDay = r.Car.PricePerDay,
                    TotalDays = r.ReturnDate.HasValue
                        ? (r.ReturnDate.Value - r.RentalDate).Days + 1
                        : 1,
                    TotalPrice = r.TotalPrice,
                    PaymentStatus = r.PaymentStatus.HasValue && r.PaymentStatus.Value 
                        ? "Sudah Dibayar" 
                        : "Belum Dibayar"
                })
                .ToList();

            return View(rentalHistory);
        }
    }
}
