using Microsoft.AspNetCore.Mvc;
using SPG_Fachtheorie.Aufgabe2.Dto;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe3.Classes;
using System.Collections.Generic;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe3.Controllers {
    public class CustomerController : Controller {
        private readonly PodcastContext _db;
        private readonly AuthService _auth;
        public CustomerController(PodcastContext db, AuthService auth) {
            _db = db;
            _auth = auth;
        }
        public IActionResult Index() {
            var customers = _db.Customers
                .Where(c => c.ResponsibleAdminId == _auth.AdminId)
                .Select(c => new CustomerDto {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    CompanyName = c.CompanyName,
                    Links = (List<LinkDto>) c.Advertisements
                    .Where(a => a.Length > 5000)
                    .Select(a => new LinkDto {
                        Id = a.Id,
                        Link = a.Id + " " + a.ProductName
                    })
                });
            return View(customers);
        }
    }
}
