using ProjetFinal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetFinal.Controllers
{
    public class AdminController : Controller
    {
        private projetFinalEntities1 projetFinalEntities1 = new projetFinalEntities1();

        // GET: Admin
        //public PartialViewResult Index()
        //{
        //    return PartialView("Index");
        //}

        public PartialViewResult AllUser()
        {
            List<AspNetUser> AspNetUser = projetFinalEntities1.AspNetUsers.ToList();
            return PartialView(AspNetUser);
        }

        public ActionResult AllXpTable()
        {
            List<xpTable> xpTable = projetFinalEntities1.xpTables.ToList();
            return View(xpTable);
        }

        [HttpPost]
        public ActionResult EditXpTable(FormCollection formCollection)
        {
            return RedirectToAction("Index");
        }

        public ActionResult DeleteXpTable(int idXpTable)
        {
            xpTable xpTable = projetFinalEntities1.xpTables.ToList().Find(x => x.id == idXpTable);
            projetFinalEntities1.xpTables.Remove(xpTable);
            return RedirectToAction("index");
        }

        public PartialViewResult AddXpTable()
        {
            return PartialView();
        }
      
        [HttpPost]
        public ActionResult AddXpTable(FormCollection formCollection)
        {
            return RedirectToAction("AllXpTable");
        }

        public PartialViewResult AllXp(int? idXpTable)
        {
            List<xp> xp;
            if (idXpTable != null)
            {
                xp = projetFinalEntities1.xps.ToList().FindAll(x => x.id_xpTable == idXpTable);
            }
            else
            {
                xp = projetFinalEntities1.xps.ToList();
            }
            
            return PartialView(xp);
        }

        [HttpPost]
        public ActionResult EditXp(FormCollection formCollection)
        {
            //projetFinalEntities1.xps.AddOrUpdate(new xpTable() { id = Int32.Parse(formCollection["id"]), name = formCollection["name"] });

            return RedirectToAction("index","Roles");
            //return View(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult DeleteXp(int idXp)
        {
            xp xp = projetFinalEntities1.xps.ToList().Find(x => x.id == idXp);
            projetFinalEntities1.xps.Remove(xp);
            return RedirectToAction("index");
        }

        public PartialViewResult AddXp()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddXp(FormCollection formCollection)
        {
            return RedirectToAction("AllXp");
        }

        public PartialViewResult AllSkill()
        {
            List<skill> skill = projetFinalEntities1.skills.ToList();
            return PartialView(skill);
        }

        public ActionResult EditSkill()
        {
            return View();
        }

        public ActionResult DeleteSkill(int idSkill)
        {
            skill skill = projetFinalEntities1.skills.ToList().Find(x => x.id == idSkill);
            projetFinalEntities1.skills.Remove(skill);
            return RedirectToAction("index");
        }

        public PartialViewResult AddSkill()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddSkill(FormCollection formCollection)
        {
            return RedirectToAction("AllSkill");
        }
       
        public PartialViewResult AllCompetence(int? idSkill)
        {
            List<competence> competence;
            if (idSkill != null)
            {
                competence = projetFinalEntities1.competences.ToList().FindAll(x => x.id_skillName == idSkill);
            }
            else
            {
                competence = projetFinalEntities1.competences.ToList();
            }
            
            return PartialView(competence);
        }

        public ActionResult EditCompentence()
        {
            return View();
        } 

        public ActionResult DeleteCompetence(int idCompentence)
        {
            competence competence = projetFinalEntities1.competences.ToList().Find(x => x.id == idCompentence);
            projetFinalEntities1.competences.Remove(competence);
            return RedirectToAction("index");
        }

        public PartialViewResult AddCompetence()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddCompetence(FormCollection formCollection)
        {
            return RedirectToAction("AllCompetence");
        }
    }
}