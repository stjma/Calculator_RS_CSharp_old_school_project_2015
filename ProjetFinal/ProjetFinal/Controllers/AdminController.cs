using ProjetFinal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetFinal.Controllers
{
    //Selement les Admins peuveut utilisé cette page
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private projetFinalEntities1 projetFinalEntities1 = new projetFinalEntities1();

        /// <summary>
        /// GET: AllUser
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AllUser()
        {
            //Afficher une liste de tous les users
            List<AspNetUser> AspNetUser = projetFinalEntities1.AspNetUsers.ToList();
            return PartialView(AspNetUser);
        }

        /// <summary>
        /// GET: AllXpTable
        /// </summary>
        /// <returns></returns>
        public ActionResult AllXpTable()
        {
            //Affiche une liste de tous les tables d'expérience
            List<xpTable> xpTable = projetFinalEntities1.xpTables.ToList();
            return View(xpTable);
        }

        /// <summary>
        /// POST: EditXpTable
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditXpTable(FormCollection formCollection)
        {
            //Modifie la table d'expérience sélectionné
            projetFinalEntities1.Entry(new xpTable()
            {
                id = Int32.Parse(formCollection["id"]),
                name = formCollection["item.name"]
            }
            ).State = System.Data.Entity.EntityState.Modified;
            projetFinalEntities1.SaveChanges();
            return RedirectToAction("AllXpTable");
        }

        /// <summary>
        /// GET: AddXpTable
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AddXpTable()
        {
            return PartialView();
        }

        /// <summary>
        /// POST: AddXpTable
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddXpTable(FormCollection formCollection)
        {
            //Ajouter des données dans la table xpTable
            projetFinalEntities1.xpTables.Add(new xpTable()
            {
                name = formCollection["name"]
            });
            projetFinalEntities1.SaveChanges();

            //Redirection vers tous les xpTables
            return RedirectToAction("AllXpTable");
        }

        /// <summary>
        /// GET: AllXp/?5
        /// </summary>
        /// <param name="idXpTable"></param>
        /// <returns></returns>
        public PartialViewResult AllXp(int? idXpTable)
        {
            //Validation pour savoir si l'utilisateur a sélectioner une table d'expérience ou il veut tous les voir afficher
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

        /// <summary>
        /// POST: EditXp
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns>view AllXp</returns>
        [HttpPost]
        public ActionResult EditXp(FormCollection formCollection)
        {
            //Modification de la table xp et redirection vers  tous les xps
            //formCollection["table"] devient null quand lutilisateur n'a pas fait de modification sur le dropdownlist
            xp xp;
            if (!formCollection["table"].Equals(""))
            {
                xp = new xp()
                {
                    id = Int32.Parse(formCollection["id"]),
                    id_xpTable = projetFinalEntities1.xpTables.ToList().Find(x => x.name == formCollection["table"]).id,
                    lvl = Int32.Parse(formCollection["item.lvl"]),
                    xps = Int32.Parse(formCollection["item.xps"]),
                    dif = Int32.Parse(formCollection["item.dif"])
                };
            }
            else
            {
                xp = new xp()
                {
                    id = Int32.Parse(formCollection["id"]),
                    id_xpTable = projetFinalEntities1.xpTables.ToList().Find(x => x.name == formCollection["else"]).id,
                    lvl = Int32.Parse(formCollection["item.lvl"]),
                    xps = Int32.Parse(formCollection["item.xps"]),
                    dif = Int32.Parse(formCollection["item.dif"])
                };
            }
            //Sauvegarde des modification apporter a la table
            projetFinalEntities1.Entry(xp).State = System.Data.Entity.EntityState.Modified; projetFinalEntities1.SaveChanges();
            return RedirectToAction("AllXp");
        }

        /// <summary>
        /// GET: EditXpTable 
        /// </summary>
        /// <param name="idXp"></param>
        /// <returns></returns>
        public ActionResult DeleteXp(int idXp)
        {
            //Supprimer une donnée dans la table xp
            xp xp = projetFinalEntities1.xps.ToList().Find(x => x.id == idXp);
            projetFinalEntities1.xps.Remove(xp);
            projetFinalEntities1.SaveChanges();

            //Redirection vers tous les xps
            return RedirectToAction("Allxp");
        }

        /// <summary>
        /// GET: EditXpTable 
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AddXp()
        {
            return PartialView();
        }

        /// <summary>
        /// POST: EditXpTable 
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddXp(FormCollection formCollection)
        {
            //Ajout de donnée dans la table xp
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

        /// <summary>
        /// GET: EditXpTable 
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AllSkill()
        {
            //Afficher tous les données de la table skill
            List<skill> skill = projetFinalEntities1.skills.ToList();
            return PartialView(skill);
        }

        /// <summary>
        /// GET: EditXpTable 
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        public ActionResult EditSkill(FormCollection formCollection)
        {
            skill skill;
            //Modification de la table skill et redirection vers  tous les skill
            //formCollection["name"] devient null quand lutilisateur n'a pas fait de modification sur le dropdownlist
            if (!formCollection["name"].Equals(""))
            {
                skill = new skill()
                {
                    id = Int32.Parse(formCollection["id"]),
                    id_xptable = projetFinalEntities1.xpTables.ToList().Find(x => x.name == formCollection["name"]).id,
                    nameSkill = formCollection["item.nameSkill"]
                };
            }
            else
            {
                skill = new skill()
                {
                    id = Int32.Parse(formCollection["id"]),
                    id_xptable = projetFinalEntities1.xpTables.ToList().Find(x => x.name == formCollection["else"]).id,
                    nameSkill = formCollection["item.nameSkill"]
                };
            }
            projetFinalEntities1.Entry(skill).State = System.Data.Entity.EntityState.Modified;
            projetFinalEntities1.SaveChanges();
            return RedirectToAction("AllSkill");
        }

        /// <summary>
        /// GET: EditXpTable 
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AddSkill()
        {

            return PartialView();
        }

        /// <summary>
        /// POST: EditXpTable 
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddSkill(FormCollection formCollection)
        {
            //Ajouter des données dans la table skill et redirection verts tous les skills
            projetFinalEntities1.skills.Add(
                new skill()
                {
                    id_xptable = projetFinalEntities1.xpTables.ToList().Find(x => x.name == formCollection["name"]).id,
                    nameSkill = formCollection["nameSkill"]
                });
            projetFinalEntities1.SaveChanges();

            return RedirectToAction("AllSkill");
        }

        /// <summary>
        /// GET: AllCompetence 
        /// </summary>
        /// <param name="idSkill"></param>
        /// <returns></returns>
        public PartialViewResult AllCompetence(int? idSkill)
        {
            //Validation pour savoir si l'utilisateur veut tous afficher les skills ou veut seulement en afficher certains
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

        /// <summary>
        /// GET: EditCompentence 
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        public ActionResult EditCompentence(FormCollection formCollection)
        {
            //Modification de la table Competence et redirection vers  tous les Competence
            //formCollection["nameSkill"] devient null quand lutilisateur n'a pas fait de modification sur le dropdownlist
            competence competence;
            if (!formCollection["nameSkill"].Equals(""))
            {
                competence = new competence()
                {
                    id = Int32.Parse(formCollection["id"]),
                    id_skillName = projetFinalEntities1.skills.ToList().Find(x => x.nameSkill == formCollection["nameSkill"]).id,
                    name = formCollection["item.name"],
                    xp = Int32.Parse(formCollection["item.xp"])
                };
            }
            else
            {
                competence = new competence()
                {
                    id = Int32.Parse(formCollection["id"]),
                    id_skillName = projetFinalEntities1.skills.ToList().Find(x => x.nameSkill == formCollection["else"]).id,
                    name = formCollection["item.name"],
                    xp = Int32.Parse(formCollection["item.xp"])
                };
            }
            projetFinalEntities1.Entry(competence).State = System.Data.Entity.EntityState.Modified;
            projetFinalEntities1.SaveChanges();
            return RedirectToAction("AllCompetence");
        }

        /// <summary>
        /// GET: EditXpTable 
        /// </summary>
        /// <returns></returns>
        public PartialViewResult AddCompetence()
        {
            return PartialView();
        }

        /// <summary>
        /// POST: AddCompetence 
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddCompetence(FormCollection formCollection)
        {
            //Ajouter une competence dans la table competence et redirection vers competence
            projetFinalEntities1.competences.Add(new competence()
            {
                id_skillName = projetFinalEntities1.skills.ToList().Find(x => x.nameSkill == formCollection["nameSkill"]).id,
                name = formCollection["name"],
                xp = Int32.Parse(formCollection["xp"])
            });
            projetFinalEntities1.SaveChanges();
            return RedirectToAction("AllCompetence");
        }
    }
}