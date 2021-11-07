using Evidance_Works.Models;
using Evidance_Works.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Evidance_Works.Controllers
{
    public class SolutionsController : Controller
    {
        readonly SolutionDbContext db = null;
        private readonly IWebHostEnvironment env;
        public SolutionsController(SolutionDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public IActionResult Index()
        {
            return View(db.Solutions.Include(x => x.Customer).ToList());
        }
        public IActionResult Create()
        {
            ViewBag.Customers = db.Customers.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(SolutionViewVM p)
        {
            if (ModelState.IsValid)
            {
                var ProNew = new Solution
                {
                    Picture = "on2.jpg",
                    servicePointName = p.servicePointName,
                    SolutionDate = p.SolutionDate,
                    serviceCategory = p.serviceCategory,
                    CustomerId = p.CustomerId

                };
                if (p.Picture != null && p.Picture.Length > 0)
                {
                    string dir = Path.Combine(env.WebRootPath, "Uploads");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    string fileName = Guid.NewGuid() + Path.GetExtension(p.Picture.FileName);
                    string fullPath = Path.Combine(dir, fileName);
                    FileStream fs = new FileStream(fullPath, FileMode.Create);
                    p.Picture.CopyTo(fs);
                    fs.Flush();
                    fs.Close();
                    ProNew.Picture = fileName;
                }
                db.Solutions.Add(ProNew);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customers = db.Customers.ToList();
            return View(p);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Customers = db.Customers.ToList();
            var pro = db.Solutions.First(p => p.SolutionId == id);
            ViewBag.CurrentPic = pro.Picture;
            return View(new SolutionViewVM
            {

                SolutionId = pro.SolutionId,
                servicePointName = pro.servicePointName,
                SolutionDate = pro.SolutionDate,
                serviceCategory = pro.serviceCategory,
                CustomerId = pro.CustomerId
            });
        }
        [HttpPost]
        public IActionResult Edit(SolutionViewVM p)
        {
            var Pro = db.Solutions.First(p => p.SolutionId == p.SolutionId);
            if (ModelState.IsValid)
            {
                
                Pro.servicePointName = p.servicePointName;
                Pro.serviceCategory = p.serviceCategory;
                Pro.SolutionDate = p.SolutionDate;
                Pro.CustomerId = p.CustomerId;
                if (p.Picture != null && Pro.Picture.Length > 0)
                {
                    string dir = Path.Combine(env.WebRootPath, "Uploads");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    string fileName = Guid.NewGuid() + Path.GetExtension(p.Picture.FileName);
                    string fullPath = Path.Combine(dir, fileName);
                    FileStream fs = new FileStream(fullPath, FileMode.Create);
                    p.Picture.CopyTo(fs);
                    fs.Flush();
                    fs.Close();
                    Pro.Picture = fileName;
                }
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customers = db.Customers.ToList();
            ViewBag.CurrentPic = Pro.Picture;
            return View(p);
        }
        public IActionResult Delete(int id)
        {
            return View(db.Solutions.Include(c => c.Customer).First(p => p.SolutionId == id));
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DoDelete(int id)
        {
            var Product = new Solution { SolutionId = id };
            db.Entry(Product).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
