using Microsoft.AspNet.Identity.Owin;
using StudentRegistrationLogin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistrationLogin.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationRoleManager _roleManager;

        public RoleController()
        {
        }

        public RoleController(ApplicationRoleManager roleManager)
        {
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
        // GET: Role
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            if (Session["LoginUser"].ToString() == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                List<RoleViewModel> rolelist = new List<RoleViewModel>();
                foreach (var role in RoleManager.Roles)
                    if (role.IsActive == "1")
                    {
                        rolelist.Add(new RoleViewModel(role));
                    }

                return View(rolelist);
            }
            
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    UpdatedBy = Session["LoginUser"].ToString(),
                    CreatedDateUtc = DateTime.UtcNow,
                    Name = model.Name,
                    IsActive = "1"
                };
                await RoleManager.CreateAsync(role);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }
        [HttpPost]
        public ActionResult Edit(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var role = new ApplicationRole
                    {
                        UpdatedBy = Session["LoginUser"].ToString(),
                        UpdatedDateUtc = DateTime.UtcNow,
                        CreatedDateUtc = model.CreatedDateUtc,
                        IsActive = "1",
                        Id = model.Id,
                        Name = model.Name
                    };
                    db.Entry(role).State = EntityState.Modified;
                    db.SaveChanges();
                }

                //await RoleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id, string name)
        {
            var role = await RoleManager.FindByIdAsync(id);
            await RoleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
    }
}