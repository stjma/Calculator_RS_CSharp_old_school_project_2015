using ProjetFinal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetFinal.Controllers
{
    [Authorize(Roles = "Admin")]
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
            projetFinalEntities1.Entry(new xpTable()
            {
                id = Int32.Parse(formCollection["id"]),
                name = formCollection["name"]
            }
            ).State = System.Data.Entity.EntityState.Modified;
            projetFinalEntities1.SaveChanges();
            return RedirectToAction("AllXpTable");
        }       

        public PartialViewResult AddXpTable()
        {
            return PartialView();
        }
      
        [HttpPost]
        public ActionResult AddXpTable(FormCollection formCollection)
        {          
            projetFinalEntities1.xpTables.Add(new xpTable()
            {
                name = formCollection["name"]
            });
            projetFinalEntities1.SaveChanges();
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
            xp xp;
            if (!formCollection["table"].Equals(""))
            {
                xp = new xp()
                {
                    id = Int32.Parse(formCollection["id"]),
                    id_xpTable = projetFinalEntities1.xpTables.ToList().Find(x => x.name == formCollection["table"]).id,
                    lvl = Int32.Parse(formCollection["lvl"]),
                    xps = Int32.Parse(formCollection["xps"]),
                    dif = Int32.Parse(formCollection["dif"])
                };
            }
            else
            {
                xp = new xp()
                {
                    id = Int32.Parse(formCollection["id"]),
                    id_xpTable = projetFinalEntities1.xpTables.ToList().Find(x => x.name == formCollection["else"]).id,
                    lvl = Int32.Parse(formCollection["lvl"]),
                    xps = Int32.Parse(formCollection["xps"]),
                    dif = Int32.Parse(formCollection["dif"])
                };
            }
            projetFinalEntities1.Entry(xp).State = System.Data.Entity.EntityState.Modified; projetFinalEntities1.SaveChanges();
            return RedirectToAction("AllXp");  
        }

        public ActionResult DeleteXp(int idXp)
        {
            xp xp = projetFinalEntities1.xps.ToList().Find(x => x.id == idXp);
            projetFinalEntities1.xps.Remove(xp);
            projetFinalEntities1.SaveChanges();
            return RedirectToAction("Allxp");
        }

        public PartialViewResult AddXp()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddXp(FormCollection formCollection)
        {
            projetFinalEntities1.xps.Add(new xp()
            {
                id_xpTable = projetFinalEntities1.xpTables.ToList().Find(x => x.name == formCollection["table"]).id,
                lvl = Int32.Parse(formCollection["lvl"]),
                xps = Int32.Parse(formCollection["xps"]),
                dif = Int32.Parse(formCollection["dif"])
            });
            projetFinalEntities1.SaveChanges();
            return RedirectToAction("AllXp");
        }

        public PartialViewResult AllSkill()
        {
            List<skill> skill = projetFinalEntities1.skills.ToList();
            return PartialView(skill);
        }

        public ActionResult EditSkill(FormCollection formCollection)
        {
            skill skill;
            if(!formCollection["name"].Equals(""))
            {
                skill = new skill()
                {
                    id = Int32.Parse(formCollection["id"]),
                    id_xptable = projetFinalEntities1.xpTables.ToList().Find(x => x.name == formCollection["name"]).id,
                    nameSkill = formCollection["nameSkill"]
                };
            }
            else
            {
                skill = new skill()
                {
                    id = Int32.Parse(formCollection["id"]),
                    id_xptable = projetFinalEntities1.xpTables.ToList().Find(x => x.name == formCollection["else"]).id,
                    nameSkill = formCollection["nameSkill"]
                };
            }
            projetFinalEntities1.Entry(skill).State = System.Data.Entity.EntityState.Modified;
            projetFinalEntities1.SaveChanges();
            return RedirectToAction("AllSkill");
        }

        public PartialViewResult AddSkill()
        {
           
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddSkill(FormCollection formCollection)
        {
            projetFinalEntities1.skills.Add(
                new skill()
                {
                    id_xptable = projetFinalEntities1.xpTables.ToList().Find(x=>x.name == formCollection["name"]).id,
                    nameSkill = formCollection["nameSkill"]
                });
            projetFinalEntities1.SaveChanges();
                      
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

        public ActionResult EditCompentence(FormCollection formCollection)
        {
            competence competence;
            if (!formCollection["nameSkill"].Equals(""))
            {
                competence = new competence()
                {
                    id = Int32.Parse(formCollection["id"]),
                    id_skillName = projetFinalEntities1.skills.ToList().Find(x => x.nameSkill == formCollection["nameSkill"]).id,
                    name = formCollection["name"],
                    xp = Int32.Parse(formCollection["xp"])
                };
            }
            else
            {
                competence = new competence()
                {
                    id = Int32.Parse(formCollection["id"]),
                    id_skillName = projetFinalEntities1.skills.ToList().Find(x => x.nameSkill == formCollection["else"]).id,
                    name = formCollection["name"],
                    xp = Int32.Parse(formCollection["xp"])
                };
            }
            projetFinalEntities1.Entry(competence).State = System.Data.Entity.EntityState.Modified;
            projetFinalEntities1.SaveChanges();
            return RedirectToAction("AllCompetence");
        } 

        public PartialViewResult AddCompetence()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddCompetence(FormCollection formCollection)
        {
            projetFinalEntities1.competences.Add(new competence()
            {
                id_skillName = projetFinalEntities1.skills.ToList().Find(x=>x.nameSkill == formCollection["nameSkill"]).id,
                name = formCollection["name"],
                xp =Int32.Parse(formCollection["xp"])
            });
            projetFinalEntities1.SaveChanges();
            return RedirectToAction("AllCompetence");
        }
    }
}