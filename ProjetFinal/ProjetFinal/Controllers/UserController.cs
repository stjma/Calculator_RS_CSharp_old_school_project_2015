using ProjetFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetFinal.Controllers
{
    public class UserController : Controller
    {
        private projetFinalEntities1 projetFinalEntities1 = new projetFinalEntities1();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllSkill()
        {
            List<skill> listSkill = projetFinalEntities1.skills.ToList();
            return View(listSkill);
        }

        public ActionResult Recherche()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Recherche(FormCollection formCollection)
        {
            TempData.Remove("errorRechercheUser");
            TempData.Remove("DifXp");
            try
            {
            int id = -1;

            if(formCollection["idRecherche"].Equals(""))
            {
                TempData["errorRechercheUser"] = "id vide";
                return RedirectToAction("Erreur");
            }
            else
            {
                id = Int32.Parse(formCollection["idRecherche"]);
            }         

            int findXpTable = (int)projetFinalEntities1.skills.ToList().Find(x => x.id == id).id_xptable;

            int lvlActuel = -1;
            int xpVoulu = -1;
            if (formCollection["lvlActuel"].Equals(""))
            {
                TempData["errorRechercheUser"] = "lvl Vide";
                return RedirectToAction("Erreur");
            }
            else
            {
                lvlActuel = Int32.Parse(formCollection["lvlActuel"]);
            }
           
            if(formCollection["xpVoulu"].Equals(""))
            {
                TempData["errorRechercheUser"] = "xp Vide";
                return RedirectToAction("Erreur");
            }
            else
            {
                xpVoulu = Int32.Parse(formCollection["xpVoulu"]);
            }
          

            int xpMax = (int)projetFinalEntities1.xps.ToList().FindAll(x => x.id_xpTable == findXpTable).Max(x => x.xps);
            int lvlMax = projetFinalEntities1.xps.ToList().FindAll(x=>x.id_xpTable == findXpTable).Count();

            if(xpVoulu <= 0 && lvlActuel <= 0)
            {
                TempData["errorRechercheUser"] = "xp actuel et lvl voulu sont vide";
                return RedirectToAction("Erreur");
            }
            else if(xpVoulu <= 0)
            {
                TempData["errorRechercheUser"] = "xp actuel est vide";
                return RedirectToAction("Erreur");
            }
            else if(lvlActuel <= 0)
            {
                TempData["errorRechercheUser"] = "lvl actuel est vide";
                return RedirectToAction("Erreur");
            }

           
            if (xpVoulu <= xpMax && lvlActuel <= lvlMax)
            {
                List<xp> liste = projetFinalEntities1.xps.ToList().FindAll(x => x.lvl >= lvlActuel);
                int xpLvl = (int)liste.Find(x => x.id_xpTable == findXpTable).xps;
                int DifXp = xpVoulu - xpLvl;
                if (DifXp >= 1)
                {
                    //https://stackoverflow.com/questions/26474579/viewbag-property-not-displaying-in-view
                    TempData["DifXp"] = DifXp;
                    //ViewBag.Ids = id;
                }
                else
                {
                    TempData["errorRechercheUser"] = "La valeur de xp actuel est plus grand que le lvl voulu" + xpMax + " " + xpVoulu;
                    return RedirectToAction("Erreur");
                }
            }
            else
            {
                TempData["errorRechercheUser"] = "Les chiffres entres sont trop grand pour les niveaus ou experiences";
                return RedirectToAction("Erreur");
            }

            return RedirectToAction("Calcul", new {id = id});
            }
            catch(Exception e)
            {
                TempData["errorRechercheUser"] = "Désoler un erreur ces produit";
                return RedirectToAction("Erreur");
            }
        }

        public PartialViewResult Calcul(int id)
        {          
            List<competence> listComp = projetFinalEntities1.competences.ToList().FindAll(x => x.id_skillName == id);
            return PartialView(listComp);
        }

        public PartialViewResult Erreur()
        {
            return PartialView();
        }
    }
}