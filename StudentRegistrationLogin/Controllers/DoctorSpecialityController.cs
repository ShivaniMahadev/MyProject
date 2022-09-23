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
using Microsoft.AspNet.Identity.Owin;

namespace StudentRegistrationLogin.Controllers
{
    public class DoctorSpecialityController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        public DoctorSpecialityController()
        {
        }

        public DoctorSpecialityController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
            
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: DoctorSpeciality
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            if (Session["LoginUser"].ToString() == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View(db.DoctorSpeciality.ToList().Where(x => x.IsActive == "1"));
            }
            
        }

        // GET: DoctorSpeciality/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorSpeciality doctorSpeciality = await db.DoctorSpeciality.FindAsync(id);
            if (doctorSpeciality == null)
            {
                return HttpNotFound();
            }
            return View(doctorSpeciality);
        }

        // GET: DoctorSpeciality/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            DoctorSpeciality ds = new DoctorSpeciality();
            ds.SpecialityCollection = db.Specialty.ToList();
            ViewBag.DS = ds.SpecialityCollection;

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var specialty in db.Specialty.ToList())
                if(specialty.IsActive == "1")
                {
                    list.Add(new SelectListItem() { Value = specialty.SpecialtyName, Text = specialty.SpecialtyName });
                }                
            ViewBag.DoctorSpecialty = list;           

            List<SelectListItem> doctorlist = new List<SelectListItem>();            
            foreach (var doctor in db.Users.ToList())
                if(doctor.RoleName == "Doctor" && doctor.IsActive == "1")
                {
                    doctorlist.Add(new SelectListItem() { Value = doctor.FullName, Text = doctor.FullName });
                }                
            ViewBag.Doctors = doctorlist;
            return View(ds);
        }

        // POST: DoctorSpeciality/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DoctorName,SpecialityName")] DoctorSpeciality doctorSpeciality)
        {
            DoctorSpeciality ds = new DoctorSpeciality();
            ds.SpecialityCollection = db.Specialty.ToList();
            ViewBag.DS = ds.SpecialityCollection;
            List<SelectListItem> doctorlist = new List<SelectListItem>();
            foreach (var doctor in db.Users.ToList())
                if (doctor.RoleName == "Doctor" && doctor.IsActive == "1")
                {
                    doctorlist.Add(new SelectListItem() { Value = doctor.FullName, Text = doctor.FullName });
                }
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var specialty in db.Specialty.ToList())
                if (specialty.IsActive == "1")
                {
                    list.Add(new SelectListItem() { Value = specialty.SpecialtyName, Text = specialty.SpecialtyName });
                }
            ViewBag.DoctorSpecialty = list;
            ViewBag.Doctors = doctorlist;
            if (ModelState.IsValid)
            {
                
                //doctorSpeciality.Id = Guid.NewGuid().ToString();
                var doc = db.Users.Where(x => x.FullName == doctorSpeciality.DoctorName).ToList();
                foreach(var a in doc)
                {
                    ViewBag.Userid = a.Id;
                }
                var special = db.Specialty.Where(x => x.SpecialtyName == doctorSpeciality.SpecialityName).ToList();
                foreach(var b in special)
                {
                    ViewBag.Special = b.SpecialtyID;
                }
                
                var users = db.DoctorSpeciality.Where(x => x.DoctorName == doctorSpeciality.DoctorName && x.IsActive =="1").FirstOrDefault();                               
                if(users != null)
                {                   
                    
                    ViewBag.Message = "Exist";                    
                    return View(doctorSpeciality);
                }
                else
                {
                    doctorSpeciality.UpdatedBy = Session["LoginUser"].ToString();
                    doctorSpeciality.CreatedDateUtc = DateTime.UtcNow;
                    doctorSpeciality.IsActive = "1";
                    doctorSpeciality.DoctorId = ViewBag.Userid;
                    doctorSpeciality.SpecialityId = ViewBag.Special;
                    db.DoctorSpeciality.Add(doctorSpeciality);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");                    
                }                
            }            
            return View(doctorSpeciality);
        }

        // GET: DoctorSpeciality/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var specialty in db.Specialty.ToList())
                if (specialty.IsActive == "1")
                {
                    list.Add(new SelectListItem() { Value = specialty.SpecialtyName, Text = specialty.SpecialtyName });
                }
            ViewBag.DoctorSpecialty = list;
            //var abc = db.Users.ToList();

            List<SelectListItem> doctorlist = new List<SelectListItem>();
            foreach (var doctor in db.Users.ToList())
                if (doctor.RoleName == "Doctor" && doctor.IsActive == "1")
                {
                    doctorlist.Add(new SelectListItem() { Value = doctor.FullName, Text = doctor.FullName });
                }
            ViewBag.Doctors = doctorlist;
            DoctorSpeciality doctorSpeciality = await db.DoctorSpeciality.FindAsync(id);
            if (doctorSpeciality == null)
            {
                return HttpNotFound();
            }
            return View(doctorSpeciality);
        }

        // POST: DoctorSpeciality/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DoctorName,SpecialityName,CreatedDateUtc")] DoctorSpeciality doctorSpeciality)
        {
            if (ModelState.IsValid)
            {
                List<SelectListItem> doctorlist = new List<SelectListItem>();
                foreach (var doctor in db.Users.ToList())
                    if (doctor.RoleName == "Doctor" && doctor.IsActive == "1")
                    {
                        doctorlist.Add(new SelectListItem() { Value = doctor.FullName, Text = doctor.FullName });
                    }
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (var specialty in db.Specialty.ToList())
                    if (specialty.IsActive == "1")
                    {
                        list.Add(new SelectListItem() { Value = specialty.SpecialtyName, Text = specialty.SpecialtyName });
                    }
                ViewBag.DoctorSpecialty = list;
                ViewBag.Doctors = doctorlist;
                var doc = db.Users.Where(x => x.FullName == doctorSpeciality.DoctorName).ToList();
                foreach (var a in doc)
                {
                    ViewBag.Userid = a.Id;
                }
                var special = db.Specialty.Where(x => x.SpecialtyName == doctorSpeciality.SpecialityName).ToList();
                foreach (var b in special)
                {
                    ViewBag.Special = b.SpecialtyID;
                }
                var users = db.DoctorSpeciality.Where(x => x.DoctorName == doctorSpeciality.DoctorName && x.SpecialityName == doctorSpeciality.SpecialityName).FirstOrDefault();
                if (users != null)
                {

                    
                    ViewBag.Message = "Exist";
                    return View(doctorSpeciality);
                }
                else
                {
                    doctorSpeciality.UpdatedBy = Session["LoginUser"].ToString();
                    doctorSpeciality.UpdatedDateUtc = DateTime.UtcNow;
                    doctorSpeciality.IsActive = "1";
                    doctorSpeciality.DoctorId = ViewBag.Userid;
                    doctorSpeciality.SpecialityId = ViewBag.Special;
                    db.Entry(doctorSpeciality).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                
            }
            return View(doctorSpeciality);
        }

        // GET: DoctorSpeciality/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorSpeciality doctorSpeciality = await db.DoctorSpeciality.FindAsync(id);
            if (doctorSpeciality == null)
            {
                return HttpNotFound();
            }
            return View(doctorSpeciality);
        }

        // POST: DoctorSpeciality/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DoctorSpeciality doctorSpeciality = await db.DoctorSpeciality.FindAsync(id);
            db.DoctorSpeciality.Remove(doctorSpeciality);
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
