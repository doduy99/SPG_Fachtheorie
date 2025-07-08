using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Domain;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;

namespace SPG_Fachtheorie.Aufgabe2.Services
{
    public class PodcastService
    {
        private readonly PodcastContext _db;
        public PodcastService(PodcastContext db) {
            _db = db;
        }
        public bool CalcTotalCosts(int customerId, DateTime begin, DateTime end)
        {
            var customer = _db.Customers.SingleOrDefault(c => c.Id == customerId);

            //1. Bedingung
            if(customer is null) {
                return false;
            }
            //2. Bedingung
            //if(customer.TotalCosts is not null) {
            //    return false;
            //}

            // 3. Bedingung
            if(end < begin) {
                return false;
            }

            //decimal sum = 0;

            //1. Version
            //foreach (Advertisement adv in customer.Advertisements)
            //    foreach (ListenedItem lt in _db.ListenedItems.Where(l => l.Item is Advertisement
            //             && l.Timestamp > begin && l.Timestamp < end)) {
            //        if (adv.Id == lt.ItemId) {
            //            sum += adv.CostsPerPlay;
            //        }
            //    }

            //var tmp = customer.Advertisements
            //    .SelectMany(a => a.ListenedItems)
            //    .Where(l => l.Timestamp > begin && l.Timestamp < end && l.Item is Advertisement)
            //    .Select(l => l.Item);

            //2. Version
            //foreach (Advertisement item in customer.Advertisements
            //    .SelectMany(a => a.ListenedItems)
            //    .Where(l => l.Timestamp > begin && l.Timestamp < end && l.Item is Advertisement)
            //    .Select(l => l.Item)) {
            //    sum += item.CostsPerPlay;
            //}

            //3. Version
            var sum = customer.Advertisements
                .SelectMany(a => a.ListenedItems)
                .Where(l => l.Timestamp > begin && l.Timestamp < end && l.Item is Advertisement)
                .Select(l => l.Item)
                .Sum(a => (a as Advertisement ?? new Advertisement()).CostsPerPlay);
            //4.Bedingung
            //if (sum == 0) {
            //    return false;
            //}

            customer.TotalCosts = sum;
            _db.Update(customer);
            //_db.SaveChanges();
            try {
                _db.SaveChanges();
            }
            catch (DbUpdateException) {
                return false;
            }

            //5. Bedingung
            return true;

            //throw new NotImplementedException("Noch keine Implementierung vorhanden");
        }

        public int CalcQuantityAdditionalAds(int playlistId)
        {
            var playlist = _db.Playlists.SingleAsync(p => p.Id == playlistId);
            //1. Bedingung
            if(playlist is null) {
                return -3;
            }

            //2. Bedingung
            if(_db.ListenedItems.Where(l => l.PlaylistId == playlistId && l.Item is Podcast).Count() == 0) {
                return -2;
            }

            int max = 0;
            foreach(Podcast item in _db.ListenedItems
                .Where(l => l.PlaylistId == playlistId && l.Item is Podcast)
                .Select(l => l.Item)) {
                max += item.MaxQuantityAds;
            }

            int anzahl = _db.ListenedItems.Where(l => l.PlaylistId == playlistId && l.Item is Advertisement).Count();

            //3. Bedingung
            if(max < anzahl) {
                return -1;
            }

            return max - anzahl;

            //throw new NotImplementedException("Noch keine Implementierung vorhanden");
        }

        public bool AddPostionForAd(int itemId, int position)
        {
            var item = _db.Podcasts.SingleOrDefault(i => i.Id == itemId);            

            //1. Bedingung
            if(item is null) {
                return false;
            }

            //2. Bedingung
            if(position > item.Length) {
                return false;
            }

            //3. Bedingung
            if(item.PositionForAd.Count() == item.MaxQuantityAds) {
                return false;
            }                

            item.PositionForAd.Add(position);

            return true;
            //throw new NotImplementedException("Noch keine Implementierung vorhanden");
        }
    }
}
