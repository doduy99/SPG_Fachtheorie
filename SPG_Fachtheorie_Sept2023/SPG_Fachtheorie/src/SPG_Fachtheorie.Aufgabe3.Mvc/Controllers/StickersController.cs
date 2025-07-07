using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Dto;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Model;

namespace SPG_Fachtheorie.Aufgabe3.Mvc.Controllers {
    public class StickersController : Controller {
        private readonly StickerContext _db;
        public StickersController(StickerContext db) {
            _db = db;
        }
        public IActionResult Index(Guid Id) {
            var CustomerStickers = _db.Customers
                .Where(c => c.Guid == Id)
                .Select(c => new CustomerStickerDto {
                    Name = c.Firstname + " " + c.Lastname,
                    Email = c.Email,
                    StickerList = (List<StickerDto>)c.Stickers.Select(s => new StickerDto {
                        Numberplate = s.Numberplate,
                        StickerType = s.StickerType.Name,
                        PurchaseDate = s.PurchaseDate,
                        ValidFrom = s.ValidFrom,
                        ValidUntil = s.ValidFrom.AddDays(s.StickerType.DaysValid),
                        Price = s.Price
                    })
                }).First();
            return View(CustomerStickers);
        }

        public IActionResult Add(Guid Id) {
            VehicleDropDownList(Id);
            StickerDropDownList();            
            return View();
        }

        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Guid Id, VehicleStickerDto vehicleStickerDto) {
            var vehicle = _db.Vehicles.
                SingleOrDefault(v => v.Guid == vehicleStickerDto.VehicleGuid);
            if (vehicle == null) {
                return NotFound();
            }

            var stickerType = _db.StickerTypes.
                SingleOrDefault(s => s.Guid == vehicleStickerDto.StickerTypeGuid);
            if(stickerType == null) {
                return NotFound();
            }

            var customer = _db.Customers.
                SingleOrDefault(c => c.Guid == Id);
            if(customer == null) {
                return NotFound();
            }

            Sticker sticker = new Sticker(vehicle.Numberplate, customer, stickerType, DateTime.Now, vehicleStickerDto.ValidFrom, stickerType.Price);

            try {
                _db.Stickers.Add(sticker);
                _db.SaveChanges();
            }
            catch {
                throw new DbUpdateException("Speichern ist nicht möglicht!");
            }
            return RedirectToAction("Index", new {Guid = Id });
            //return View();
        }

        public void VehicleDropDownList(Guid Id, object? selectedVehicle = null) {
            var vehicleQuery = _db.Vehicles
                .Where(v => v.Customer.Guid == Id)
                .ToList();

            ViewBag.VehicleGuid = new SelectList(vehicleQuery, "Guid", "VehicleInfo", selectedVehicle);
        }

        public void StickerDropDownList(object? selectedSticker = null) {
            var stickerTypeQuery = _db.StickerTypes.ToList();
            ViewBag.StickerTypeGuid = new SelectList(stickerTypeQuery, "Guid", "Name", selectedSticker);
        }
    }
}
