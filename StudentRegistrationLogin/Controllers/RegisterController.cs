using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using StudentRegistrationLogin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistrationLogin.Controllers
{
    public class RegisterController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private ApplicationDbContext db = new ApplicationDbContext();
        public RegisterController()
        {
        }

        public RegisterController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
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
        //[Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            
            if (User.Identity.GetUserName() == "")
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var admin = db.Users.ToList().Where(x => x.Email == User.Identity.GetUserName()).FirstOrDefault();
                if(admin.RoleName == "Admin")
                {
                    return View(db.Users.ToList().Where(x => x.IsActive == "1"));
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
                
            }
            
        }
        //[Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var role in RoleManager.Roles)
                if (role.IsActive == "1")
                {
                    list.Add(new SelectListItem() { Value = role.Name, Text = role.Name });
                }
            ViewBag.Roles = list;
            List<SelectListItem> Prefixlist = new List<SelectListItem>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                foreach (var pre in db.PrefixMaster.ToList())
                    Prefixlist.Add(new SelectListItem() { Value = pre.PrefixName, Text = pre.PrefixName });
                ViewBag.Prefix = Prefixlist;
            }
            ApplicationUser userManage = db.Users.Find(id);
            //userManage.ConfirmPassword = userManage.Password;
            if (userManage == null)
            {
                return HttpNotFound();
            }
            //ViewBag.RoleID = new SelectList(db.RoleManages, "RoleID", "RoleName", userManage.RoleID);
            return View(userManage);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(ApplicationUser model)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var role in RoleManager.Roles)
                if (role.IsActive == "1")
                {
                    list.Add(new SelectListItem() { Value = role.Name, Text = role.Name });
                }
            ViewBag.Roles = list;
            List<SelectListItem> Prefixlist = new List<SelectListItem>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                foreach (var pre in db.PrefixMaster.ToList())
                    Prefixlist.Add(new SelectListItem() { Value = pre.PrefixName, Text = pre.PrefixName });
                ViewBag.Prefix = Prefixlist;
            }
            if (ModelState.IsValid)
            {
                var role = new ApplicationUser();
                role.Id = model.Id;
                role.FirstName = model.FirstName;
                role.LastName = model.LastName;
                role.UserName = model.Email;
                role.Email = model.Email;
                role.PasswordHash = model.PasswordHash;
                role.DOB = model.DOB;
                role.RoleName = model.RoleName;
                role.ImagePath = model.ImagePath;
                role.EmailConfirmed = model.EmailConfirmed;
                role.SecurityStamp = model.SecurityStamp;
                role.LockoutEnabled = model.LockoutEnabled;
                role.FullName = model.FullName;
                role.PrefixName = model.PrefixName;
                role.CreatedDateUtc = model.CreatedDateUtc;
                role.UpdatedDateUtc = DateTime.UtcNow;
                role.IsActive = model.IsActive;
                //var result = await UserManager.UpdateAsync(role);
                role.UpdatedBy = Session["LoginUser"].ToString();
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                var result = await UserManager.AddToRoleAsync(model.Id, model.RoleName);
                return RedirectToAction("Index");
            }
            return View(model);  
        }
        //[Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser userManage = db.Users.Find(id);
            if (userManage == null)
            {
                return HttpNotFound();
            }
            return View(userManage);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser userManage = db.Users.Find(id);
            db.Users.Remove(userManage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult ImageUpload(RegisterModel model, ApplicationUser m)
        {

            var file = model.ImageFile;

            var pathname ="";
            if (file != null)
            {

                var fileName = Path.GetFileName(file.FileName);
                var extention = Path.GetExtension(file.FileName);
                var filenamewithoutextension = Path.GetFileNameWithoutExtension(file.FileName);
                pathname = "/UploadedImage/" + file.FileName;
                //ViewBag.PathData = pathname;
                //var temp = model.ImagePath;
                file.SaveAs(Server.MapPath("/UploadedImage/" + file.FileName));
                

            }
            //var role = new ApplicationUser();
            //role.ImagePath = pathname;
            TempData["datas"] = pathname;
            return Json(file.FileName, JsonRequestBehavior.AllowGet);

        }
    }
}