using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2;
using SPG_Fachtheorie.Aufgabe2.Dto;
using SPG_Fachtheorie.Aufgabe2.Model;
using SPG_Fachtheorie.Aufgabe3Mvc.Services;
using System;
using System.IO;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe3Mvc.Controllers {
    public class OfferController : Controller {
        private readonly AppointmentContext _db;
        private readonly AuthService _auth;
        public OfferController(AppointmentContext db, AuthService auth) {
            _db = db;
            _auth = auth;
        }
        public IActionResult Index() {
            if(_auth.Username == null) {
                return RedirectToAction("Index", "Home", null);
            }
            var offers = _db.Offers
                .Where(o => o.Teacher.Username == _auth.Username)
                .Select(o => new OfferDto {
                    Id = o.Id,
                    Term = o.Subject.Term,
                    Name = o.Subject.Name,
                    EducationType= o.Subject.EducationType,
                    From = o.From,
                    To = o.To,
                    Anzahl = o.Appointments.Count()
                });
            return View(offers);
        }

        public IActionResult Detail(Guid id) {
            if (_auth.Username == null) {
                return RedirectToAction("Index", "Home", null);
            }

            //var appointments = _db.Offers
            //    .Where(o => o.Id == id)
            //    .SelectMany(o => o.Appointments);
            var appointments = _db.Offers
                .Where(o => o.Id == id)
                .SelectMany(o => o.Appointments)
                .Select(a => new AppointmentDto{
                    FirstName = a.Student.Firstname,
                    LastName = a.Student.Lastname,
                    Date = a.Date,
                    Location = a.Location,
                    State = a.State,
                });
            return View(appointments);
        }

        public IActionResult Add() {
            if (_auth.Username == null) {
                return RedirectToAction("Index", "Home", null);
            }
            SubjectDropDownList();
            return View();
        }
        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        public IActionResult Add(OfferRequest offerRequest) {
            if (_auth.Username == null) {
                return RedirectToAction("Index", "Home", null);
            }

            var teacher = _db.Students.SingleOrDefault(s => s.Username == _auth.Username);
            if (teacher == null) {
                throw new InvalidDataException("Teacher ist nicht identisch!");
            }

            var offer = new Offer() {
                Id = new Guid(),
                TeacherId = teacher.Id,
                SubjectId = offerRequest.SubjectId,
                From = offerRequest.From,
                To = offerRequest.To
            };

            _db.Offers.Add(offer);
            try {
                _db.SaveChanges();
            } catch {
                throw new DbUpdateException("Hinzufügen ist nicht möglich!");
            }            
            return RedirectToAction("Index", "Offer", null);
        }

        public void SubjectDropDownList(object? selectedSubject = null) {
            var subjectQuery = _db.Subjects
                .Select(s => new {
                    Id = s.Id,
                    Name = s.Term + "-" + s.Name + "-" + s.EducationType,
                }).ToList();

            ViewBag.SubjectId = new SelectList(subjectQuery, "Id", "Name"
            , selectedSubject);
        }
        
        public IActionResult Edit(Guid id) {
            if (_auth.Username == null) {
                return RedirectToAction("Index", "Home", null);
            }
            var offer = _db.Offers
                .Where(o => o.Teacher.Username == _auth.Username && o.Id == id)
                .Select(o => new OfferDto {
                    Id = o.Id,
                    Term = o.Subject.Term,
                    Name = o.Subject.Name,
                    EducationType = o.Subject.EducationType,
                    From = o.From,
                    To = o.To,
                    Anzahl = o.Appointments.Count()
                }).FirstOrDefault();
            return View(offer);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, OfferDto offerDto) {
            if (_auth.Username == null) {
                return RedirectToAction("Index", "Home", null);
            }
            var offer = _db.Offers
                .Where(o => o.Teacher.Username == _auth.Username && o.Id == id)
                .FirstOrDefault();            

            if(offer == null) {
                throw new InvalidDataException("Angebot nicht gefunden!");
            }

            offer.To = offerDto.To;
            
            _db.Update(offer);
            _db.SaveChanges();
            return RedirectToAction("Index");            

        }

        public ActionResult Delete(Guid id) {

            var offer = _db.Offers
                .Where(o => o.Id == id && o.Appointments.Count() == 0).FirstOrDefault();
            if (offer == null) {
                throw new ArgumentException("Löschen ist nicht möglich, denn gibt es eingetragene Termine.");
            }

            _db.Offers.Remove(offer);
            try {
                _db.SaveChanges();
            } catch {
                throw new DbUpdateException("Löschen ist nicht möglich!");
            }            
            return RedirectToAction("Index");
        }
    }
}
