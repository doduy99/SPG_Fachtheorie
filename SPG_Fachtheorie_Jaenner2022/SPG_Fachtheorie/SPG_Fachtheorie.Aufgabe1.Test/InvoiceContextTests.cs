using Xunit;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Model;
using System.Linq;
using Bogus.DataSets;
using System;

namespace SPG_Fachtheorie.Aufgabe1.Test;

/// <summary>
/// Unittests für den DBContext.
/// Die Datenbank wird im Ordner SPG_Fachtheorie\SPG_Fachtheorie.Aufgabe1.Test\bin\Debug\net6.0\Invoice.db
/// erzeugt und kann mit SQLite Management Studio oder DBeaver betrachtet werden
/// </summary>
[Collection("Sequential")]
public class InvoiceContextTests {
    public InvoiceContext CreateDb() {
        var options = new DbContextOptionsBuilder()
            .UseSqlite("Data Source=Invoice.db")
            .Options;

        var db = new InvoiceContext(options);
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
        return db;
    }
    /// <summary>
    /// Prüft, ob die Datenbank mit dem Model im InvoiceContext angelegt werden kann.
    /// </summary>
    [Fact]
    public void CreateDatabaseTest()
    {
        var options = new DbContextOptionsBuilder()
            .UseSqlite("Data Source=Invoice.db")
            .Options;

        var db = new InvoiceContext(options);
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }

    [Fact]
    public void AddCustomerSuccessTest() {
        using(var db = CreateDb()) {
            //var customer = new Customer() { Anrede = Anrede.HERR, Name = "Ngoc Duy DO", Anschrift = "Spengergasse 20 1050 Wien" };
            var customer = new Customer(kundennummer: 10000, anrede: Anrede.HERR, name: "Ngoc Duy DO", anschrift: "Spengergasse 20 1050 Wien");
            
            db.Customers.Add(customer);
            db.SaveChanges();

            //Assert.True(db.Customers.Where(c => c.Kundennummer == 1).First().Name == "Ngoc Duy DO");
            Assert.True(db.Customers.Count() == 1);
        }
    }
}