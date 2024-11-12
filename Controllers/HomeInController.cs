using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyekRentCar.Models;
using Microsoft.EntityFrameworkCore;

namespace ProyekRentCar.Controllers
{
    public class HomeInController : Controller
    {
        private readonly ILogger<HomeInController> _logger;
        private readonly RentCarsContext _context;

        public HomeInController(ILogger<HomeInController> logger, RentCarsContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult HomeIn(DateTime? pickupDate, DateTime? returnDate, int? year, string sortOrder)
        { 
            // Ambil semua mobil
            var cars = _context.MsCars.AsQueryable();
            bool isSearchPerformed = false;
            if (pickupDate.HasValue || returnDate.HasValue || year.HasValue || !string.IsNullOrEmpty(sortOrder))
            {
                isSearchPerformed = true;
            }

            // Filter berdasarkan pickup dan return date jika tersedia
            if (pickupDate.HasValue && returnDate.HasValue)
            {
                cars = cars.Where(c => !_context.TrRentals
                    .Any(r => r.CarId == c.CarId && 
                              ((pickupDate >= r.RentalDate && pickupDate <= r.ReturnDate) || 
                               (returnDate >= r.RentalDate && returnDate <= r.ReturnDate))));
            }

            // Filter berdasarkan tahun pembuatan jika disediakan
            if (year.HasValue)
            {
                cars = cars.Where(c => c.Year == year.Value);
            }

            // Sorting berdasarkan harga jika diatur
            switch (sortOrder)
            {
                case "Harga Tertinggi - Terendah":
                    cars = cars.OrderByDescending(c => c.PricePerDay);
                    break;
                case "Harga Terendah - Tertinggi":
                    cars = cars.OrderBy(c => c.PricePerDay);
                    break;
                default:
                    // Jika tidak ada opsi sorting, urutkan berdasarkan nama mobil
                    cars = cars.OrderBy(c => c.Name);
                    break;
            }

            ViewBag.IsSearchPerformed = isSearchPerformed;
            var carList = cars.ToList();
            return View(carList);
        }

        public IActionResult CarView(string carId)
        {
            var car = _context.MsCars
                .Include(c => c.MsCarImages)
                .FirstOrDefault(c => c.CarId == carId);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
