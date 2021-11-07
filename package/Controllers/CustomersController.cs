using Evidance_Works.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evidance_Works.Controllers
{
    public class CustomersController : Controller
    {
        private readonly SolutionDbContext db = null;
        public CustomersController(SolutionDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View(db.Customers.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer cus)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(cus);
                db.SaveChanges();
                return PartialView("_ArifPartial", true);
            }
            return PartialView("_ArifPartial", false);
        }
        public IActionResult Edit(int id)
        {
            return View(db.Customers.First(x => x.CustomerId == id));
        }
        [HttpPost]
        public IActionResult Edit(Customer cus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cus).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return PartialView("_ArifPartial", true);
            }
            return PartialView("_ArifPartial", false);
        }
        public IActionResult Delete(int id)
        {
            return View(db.Customers.First(x => x.CustomerId == id));
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            Customer c = new Customer { CustomerId = id };
            db.Entry(c).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
