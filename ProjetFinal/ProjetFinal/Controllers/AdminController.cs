using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetFinal.Controllers
{
    public class AdminController : Controller
    {


        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult AllUser()
        {
            return PartialView();
        }

        public PartialViewResult DeleteUser()
        {
            return PartialView();
        }

        public PartialViewResult AllRole()
        {
            return PartialView();
        }

        public PartialViewResult DeleteRole()
        {
            return PartialView();
        }

        public PartialViewResult AllXpTable()
        {
            return PartialView();
        }

        public PartialViewResult AllXp()
        {
            return PartialView();
        }

        public PartialViewResult DeleteXpTable()
        {
            return PartialView();
        }

        public PartialViewResult DeleteXp()
        {
            return PartialView();
        }

        public PartialViewResult AllSkill()
        {
            return PartialView();
        }

        public PartialViewResult AllCompetence()
        {
            return PartialView();
        }
    }
}