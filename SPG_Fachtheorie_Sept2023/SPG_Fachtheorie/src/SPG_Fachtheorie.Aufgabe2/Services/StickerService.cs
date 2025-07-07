using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe2.Services
{
    public record SaleStatistics(string StickerTypeName, decimal TotalRevenue);

    public class StickerService
    {
        private readonly StickerContext _db;

        public StickerService(StickerContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Checks the permission for a scanned numberplate.
        /// </summary>
        public bool HasPermission(string numberplate, DateTime dateTime, VehicleType carType)
        {
            //1. Überprüfen, ob das gegebene Kennzeichen dem gespeichertem Kennzeichen entspricht.
            var sticker_numberplate = _db.Stickers.Where(s => s.Numberplate == numberplate);
            if(sticker_numberplate is null) {
                return false;
            }       


            //2. Überprüfen, ob das eingegebene Fahrzeugtype dem gespeichertem Fahrzeugtype enspricht.
            //var sticker_vehicletype = _db.Stickers.SingleOrDefault(s => s.Numberplate == numberplate && s.StickerType.VehicleType == carType);
            var sticker_vehicletype = sticker_numberplate.Where(s => s.StickerType.VehicleType == carType);
            if (sticker_vehicletype is null) {
                return false;
            }

            //3. Überprüfen, ob das eingegebene Datum dem gespeichertem Datum entspricht.
            var dt = sticker_vehicletype.SingleOrDefault(s => s.ValidFrom < dateTime && s.ValidFrom.AddDays(s.StickerType.DaysValid) > dateTime);
            if(dt is null) {
                return false;
            }

            return true;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the total revenue for every sticker type sold in a specific year.
        /// Use PurchaseDate.Year to get the year of purchase.
        /// Hint: To use Sum() you have to cast Price to double.
        ///       After that you have to cast the sum back to decimal.
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<SaleStatistics> CalcSaleStatistics(int year)
        {
            var sum = _db.Stickers
                .Where(s => s.PurchaseDate.Year == year)
                .GroupBy(s => s.StickerType.Name)
                .Select(g => new SaleStatistics(
                    g.Key,
                    (decimal) g.Sum(s => (double)s.Price)
                    )).ToList();
            return sum;
            //throw new NotImplementedException();
        }
    }
}