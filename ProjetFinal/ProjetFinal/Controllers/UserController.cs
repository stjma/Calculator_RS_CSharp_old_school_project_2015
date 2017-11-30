using ProjetFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetFinal.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        private projetFinalEntities1 projetFinalEntities1 = new projetFinalEntities1();

        /// <summary>
        /// GET: User
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET: AllSkill 
        /// </summary>
        /// <returns></returns>
        public ActionResult AllSkill()
        {
            List<skill> listSkill = projetFinalEntities1.skills.ToList();
            return View(listSkill);
        }

        /// <summary>
        /// GET: AllSkill 
        /// </summary>
        /// <returns></returns>
        public ActionResult Recherche()
        {
            return View();
        }

        /// <summary>
        /// GET: AllSkill  
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Recherche(FormCollection formCollection)
        {
            //La recherche de la différence d'expérience

            //Vider les tempsData juste au cas ou il reste des informations.
            TempData.Remove("errorRechercheUser");
            TempData.Remove("DifXp");

            try
            {
                int id = -1;

                //Vérifier si le textBox idRecherche est null
                if (formCollection["idRecherche"].Equals(""))
                {
                    //si null il y a une erreur afficher sur la vue
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

                //Vérifier si le textBox lvlActuel est null
                if (formCollection["lvlActuel"].Equals(""))
                {
                    //si null il y a une erreur afficher sur la vue
                    TempData["errorRechercheUser"] = "lvl Vide";
                    return RedirectToAction("Erreur");
                }
                else
                {
                    lvlActuel = Int32.Parse(formCollection["lvlActuel"]);
                }

                //Vérifier si le textBox xpVoulu est null
                if (formCollection["xpVoulu"].Equals(""))
                {
                    //si null il y a une erreur afficher sur la vue
                    TempData["errorRechercheUser"] = "xp Vide";
                    return RedirectToAction("Erreur");
                }
                else
                {
                    xpVoulu = Int32.Parse(formCollection["xpVoulu"]);
                }

                //Recherche dans les tables les valeurs maximum pour xp et lvl
                int xpMax = (int)projetFinalEntities1.xps.ToList().FindAll(x => x.id_xpTable == findXpTable).Max(x => x.xps);
                int lvlMax = projetFinalEntities1.xps.ToList().FindAll(x => x.id_xpTable == findXpTable).Count();

                //l'expérience voulu doit être plus petit que le niveau actuel
                if (xpVoulu <= 0 && lvlActuel <= 0)
                {
                    TempData["errorRechercheUser"] = "xp actuel et lvl voulu sont vide";
                    return RedirectToAction("Erreur");
                }

                //l'expérience voulu doit être plus grand que 0
                else if (xpVoulu < 0)
                {
                    //Redirection vers une page d'erreur 
                    TempData["errorRechercheUser"] = "xp actuel est vide";
                    return RedirectToAction("Erreur");
                }

                //Le niveau actuel doit être plus grand que 0
                else if (lvlActuel < 0)
                {
                    //Redirection vers une page d'erreur
                    TempData["errorRechercheUser"] = "lvl actuel est vide";
                    return RedirectToAction("Erreur");
                }

                //Vérification que les niveau actuel et expérience voulu ne sont plus grand que ceux dans la bade de donnée
                if (xpVoulu <= xpMax && lvlActuel <= lvlMax)
                {
                    //Trandformer le niveau en expérience
                    List<xp> liste = projetFinalEntities1.xps.ToList().FindAll(x => x.lvl >= lvlActuel);
                    int xpLvl = (int)liste.Find(x => x.id_xpTable == findXpTable).xps;

                    //Le calcul de la différence d'expérience
                    int DifXp = xpVoulu - xpLvl;
                    if (DifXp >= 1)
                    {
                        //Lors d'une redirection le viewbag devien null
                        //https://stackoverflow.com/questions/26474579/viewbag-property-not-displaying-in-view
                        TempData["DifXp"] = DifXp;
                    }
                    else
                    {
                        //Redirection vers une page d'erreur
                        TempData["errorRechercheUser"] = "La valeur de xp voulu est plus grand que le lvl de départ";
                        return RedirectToAction("Erreur");
                    }
                }
                else
                {
                    //Redirection vers une page d'erreur
                    TempData["errorRechercheUser"] = "Les chiffres entres sont trop grand pour les niveaus ou experiences";
                    return RedirectToAction("Erreur");
                }

                return RedirectToAction("Calcul", new { id = id });
            }
            catch (Exception e)
            {
                //Redirection vers une page d'erreur
                TempData["errorRechercheUser"] = "Désoler un erreur ces produit";
                return RedirectToAction("Erreur");
            }
        }

        /// <summary>
        /// GET: Calcul  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult Calcul(int id)
        {
            //la table utilisé pour afficher les résultats du calculateur
            List<competence> listComp = projetFinalEntities1.competences.ToList().FindAll(x => x.id_skillName == id);
            return PartialView(listComp);
        }

        /// <summary>
        /// GET: Erreur  
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Erreur()
        {
            //Vue partiel pour les erreurs de calcul
            return PartialView();
        }
    }
}