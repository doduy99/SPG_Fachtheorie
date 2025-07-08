using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Domain;
using SPG_Fachtheorie.Aufgabe2.Dto;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe3.Classes;
using System.IO;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe3.Controllers {
    public class AdvertisementController : Controller {
        private readonly PodcastContext _db;
        private readonly AuthService _auth;
        public AdvertisementController(PodcastContext db, AuthService auth) {
            _db = db;
            _auth = auth;
        }

        public IActionResult Index() {
            return View();
        }

        [HttpGet()]
        public IActionResult Details(int Id) {
            var advertisement = _db.Advertisements
                .Where(a => a.Id == Id)
                .Select(a => new AdvertisementDto {
                    Id = a.Id,
                    ProductName = a.ProductName,
                    Customer = a.Customer.LastName + " " + a.Customer.FirstName,
                    Production = a.Production,
                    Length = a.Length,
                    MinPlayTime = a.MinPlayTime,
                    CostsPerPlay = a.CostsPerPlay,
                    NumberOfListenedItems = a.ListenedItems.Count()
                }).FirstOrDefault();
            if (advertisement == null) {
                return NotFound();
            }

            var model = _db.Advertisements
                .SingleOrDefault(a => a.Id == advertisement.Id && a.Customer.ResponsibleAdminId == _auth.AdminId);
            if(model == null) {
                return NotFound();
            }

            return View(advertisement);
        }

        public IActionResult Create() {
            //ViewData["customers"] = new SelectList(_db.Customers
            //    .Where(c => c.ResponsibleAdminId == _auth.AdminId), "Id", "LastName");
            CustomersDropDownList();
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Advertisement advertisement) {
            var customer = _db.Customers.Where(c => c.Id == advertisement.CustomerId).FirstOrDefault();
            if (customer == null) {
                return NotFound();
            }

            if(advertisement.Production < customer.RegistrationDate) {
                throw new InvalidDataException("Production muss nach RegistrationDate von Customer liegen!");
            }
            _db.Advertisements.Add(advertisement);
            try {
                _db.SaveChanges();
            } catch {
                throw new DbUpdateException("Speichern ist nicht möglicht!");
            }
            return RedirectToAction("Index", "Customer", null);
        }

        private void CustomersDropDownList(object? selectedCustomer = null) {
            var customersQuery = _db.Customers
                .Where(c => c.ResponsibleAdminId == _auth.AdminId)
                .ToList();
            ViewBag.CustomerId = new SelectList(customersQuery, "Id", "LastName", selectedCustomer);
        }
    }
}
