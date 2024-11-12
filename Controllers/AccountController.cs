using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyekRentCar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http; 

namespace ProyekRentCar.Controllers
{
    public class AccountController : Controller
    {
        private readonly RentCarsContext _context;

        public AccountController(RentCarsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(MsCustomer model)
        {
            // Cek apakah email sudah ada di database
            var existingCustomer = _context.MsCustomers.FirstOrDefault(c => c.Email == model.Email);
            if (existingCustomer != null)
            {
                Console.WriteLine("EMAIL UDA ADA");
                ViewBag.ErrorRegisterMessage = "Email sudah terdaftar. Gunakan email lain.";
                return View(model);
            }

            // Generate CustomerId sebelum periksa ModelState
            var maxCustomerId = _context.MsCustomers
                .OrderByDescending(c => c.CustomerId)
                .Select(c => c.CustomerId)
                .FirstOrDefault();

            int nextIdNumber = maxCustomerId != null ? int.Parse(maxCustomerId.Replace("CUS", "")) + 1 : 1;
            model.CustomerId = "CUS" + nextIdNumber;

            // Hapus error pada CustomerId di ModelState karena sudah diisi
            ModelState.Clear();
            TryValidateModel(model);

            // Jika ModelState tidak valid
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
                ViewBag.ErrorRegisterMessage = "Registrasi gagal. Silakan periksa input Anda.";
                return View(model);
            }

            // Cek PhoneNumber dan DriverLicenseNumber
            Console.WriteLine($"PhoneNumber: {model.PhoneNumber}");
            Console.WriteLine($"DriverLicenseNumber: {model.DriverLicenseNumber}");

            // Simpan data pelanggan baru
            _context.MsCustomers.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Validasi input email dan password
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorLoginMessage = "Email dan password harus diisi.";
                return View();
            }

            var customer = _context.MsCustomers.FirstOrDefault(c => c.Email == email && c.Password == password);

            // Jika customer tidak ditemukan
            if (customer == null)
            {
                ViewBag.ErrorLoginMessage = "Data tidak ditemukan. Coba lagi!";
                Console.WriteLine("GA ADA DATANYA");
                return View();
            }

            // Simpan UserId dan Username di session
            HttpContext.Session.SetString("UserId", customer.CustomerId);
            HttpContext.Session.SetString("Username", customer.Name);

            Console.WriteLine("SUKSES ADA DATANYA");
            return RedirectToAction("HomeIn", "HomeIn", new { username = customer.Name });
        }
    }
}
