using Microsoft.AspNetCore.Mvc;
using SPG_Fachtheorie.Aufgabe2.Domain;
using SPG_Fachtheorie.Aufgabe2.Dto;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe3.Controllers {
    public class PlaylistController : Controller {
        private readonly PodcastContext _db;
        public PlaylistController(PodcastContext db) {
            _db = db;
        }
        public IActionResult Index() {
            var playlist = _db.Playlists
                .Select(p => new PlaylistDto {
                    Name = p.Name,
                    UserName = p.UserName,
                    Anzahl = p.ListenedItems.Count(l => l.Item is Podcast)
                }).OrderBy(p => p.Name);
                return View(playlist);
        }
    }
}
