using Microsoft.AspNetCore.Mvc;
using SPG_Fachtheorie.Aufgabe2.Dto;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;

namespace SPG_Fachtheorie.Aufgabe3.Mvc.Controllers {
    public class CustomersController : Controller {
        private readonly StickerContext _db;
        public CustomersController(StickerContext db) {
            _db = db;
        }
        public IActionResult Index() {
            var Customers = _db.Customers.Select(s => new CustomerDto {
                Guid = s.Guid,
                Firstname = s.Firstname,
                Lastname = s.Lastname,
                SumStickers = s.Stickers.Count(),
                SumVehicles = s.Vehicles.Count()
            });
            return View(Customers);
        }
    }
}
