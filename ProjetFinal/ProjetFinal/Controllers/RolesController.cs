using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjetFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace ProjetFinal.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        /// <summary>
        /// GET: Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // Remplir les dropDownlists
            var context = new ApplicationDbContext();

            var rolelist = context.Roles.OrderBy(r => r.Name).ToList().Select(rr =>
            new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = rolelist;

            var userlist = context.Users.OrderBy(u => u.UserName).ToList().Select(uu =>
            new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Users = userlist;

            ViewBag.Message = "";

            return View();
        }


        /// <summary>
        /// GET: /Roles/Create 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }


        /// <summary>
        /// POST: /Roles/Create 
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            //Créer un rôle
            try
            {
                var context = new ApplicationDbContext();
                context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                context.SaveChanges();
                ViewBag.Message = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        /// <summary>
        /// GET : Delete 
        /// </summary>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        public ActionResult Delete(string RoleName)
        {
            //Supprimer un Rôle
            var context = new Models.ApplicationDbContext();
            var thisRole = context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            context.Roles.Remove(thisRole);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        /// <summary>
        /// GET: /Roles/Edit/5 
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public ActionResult Edit(string roleName)
        {
            //Modifier un Rôle
            var context = new Models.ApplicationDbContext();
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(thisRole);
        }


        /// <summary>
        /// POST: /Roles/Edit/5 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            //Modifier le rôle d'un utilisateur
            try
            {
                var context = new Models.ApplicationDbContext();
                context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        /// <summary>
        /// POST : Adding Roles to a user 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            //Ajouter un rôle a un utilisateur
            var context = new Models.ApplicationDbContext();

            if (context == null)
            {
                throw new ArgumentNullException("context", "Context must not be null.");
            }

            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.AddToRole(user.Id, RoleName);


            ViewBag.Message = "Role created successfully !";

            // Remplir les dropDownlists
            var rolelist = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = rolelist;
            var userlist = context.Users.OrderBy(u => u.UserName).ToList().Select(uu =>
            new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Users = userlist;

            return View("Index");
        }


        /// <summary>
        /// POST : Getting a List of Roles for a User
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            //Rechercher tous les rôle
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                var context = new Models.ApplicationDbContext();
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                ViewBag.RolesForThisUser = userManager.GetRoles(user.Id);


                // Remplir les dropDownlists
                var rolelist = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = rolelist;
                var userlist = context.Users.OrderBy(u => u.UserName).ToList().Select(uu =>
                new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
                ViewBag.Users = userlist;
                ViewBag.Message = "Roles retrieved successfully !";
            }

            return View("Index");
        }

        /// <summary>
        /// POST : Deleting a User from A Role 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            //Supprimer un Rôle pour un utilisateur
            var account = new AccountController();
            var context = new Models.ApplicationDbContext();
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);


            if (userManager.IsInRole(user.Id, RoleName))
            {
                userManager.RemoveFromRole(user.Id, RoleName);
                ViewBag.Message = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.Message = "This user doesn't belong to selected role.";
            }

            // Remplir les dropDownlists
            var rolelist = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = rolelist;
            var userlist = context.Users.OrderBy(u => u.UserName).ToList().Select(uu =>
            new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Users = userlist;

            return View("Index");
        }
    }
}
