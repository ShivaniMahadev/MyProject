using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentRegistrationLogin.Models;
using System.Data.Entity.Infrastructure;

namespace StudentRegistrationLogin.Controllers
{
    public class SpecialtyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Specialty
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            if(Session["LoginUser"].ToString() == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View(db.Specialty.ToList().Where(x => x.IsActive == "1"));
            }
            
        }

        // GET: Specialty/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = await db.Specialty.FindAsync(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
        }

        // GET: Specialty/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Specialty/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SpecialtyName")] Specialty specialty)
        {
            if (ModelState.IsValid)
            {

                //specialty.SpecialtyID = Guid.NewGuid().ToString();
                specialty.UpdatedBy = Session["LoginUser"].ToString();
                specialty.CreatedDateUtc = DateTime.UtcNow;
                specialty.IsActive = "1";
                db.Specialty.Add(specialty);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(specialty);
        }

        // GET: Specialty/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = await db.Specialty.FindAsync(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
        }

        // POST: Specialty/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SpecialtyID,SpecialtyName,IsActive,CreatedDateUtc")] Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                specialty.UpdatedBy = Session["LoginUser"].ToString();
                specialty.UpdatedDateUtc = DateTime.UtcNow;
                specialty.IsActive = "1";
                db.Entry(specialty).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(specialty);
        }

        // GET: Specialty/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = await db.Specialty.FindAsync(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
        }

        // POST: Specialty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Specialty specialty = await db.Specialty.FindAsync(id);            
            db.Specialty.Remove(specialty);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
