using Domain.Authentication;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.IRepositories;
using Service.IServices;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Helpers;

namespace Web.Controllers
{
    [Authorize(Roles = "Client, Responsable_de_production")]

    public class FicheTechniqueController : Controller
    {
        private readonly IProduitFicheTechniqueService produitFicheTechniqueService;
        private readonly IProduitVendableService produitVendableService;
        public FicheTechniqueController(IProduitFicheTechniqueService produitFicheTechniqueService,IProduitVendableService produitVendableService)
        {
            this.produitVendableService = produitVendableService;
            this.produitFicheTechniqueService = produitFicheTechniqueService;
        }

        [HttpPost]
        public SelectList formeProduit(int Id)
        {
            //var d = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            SelectList formeProduit = new SelectList(produitFicheTechniqueService.getListFormes(Id), "FormeProduit_ID", "FormeProduit_Libelle");
            return formeProduit;
        }

        public IActionResult Ajouter()
        {
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["produitBase"] = new SelectList(produitVendableService.getListProduitBase(aboId, null), "ProduitBase_ID", "ProduitBase_Designation");
            ViewData["FicheTechnique_ProduitVendableId"] = new SelectList(produitFicheTechniqueService.getListProduitVendable(aboId), "ProduitVendable_Id", "ProduitVendable_Designation");
            ViewData["FicheTechnique_MatierePremiereId"] = new SelectList(produitFicheTechniqueService.getListMatierePremiere(aboId), "MatierePremiere_Id", "MatierePremiere_Libelle");
            ViewData["FicheTechnique_UniteMesureId"] = new SelectList(produitFicheTechniqueService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
            return View();
        }
        [HttpPost]
        public async Task<bool> AjouterFiche(FicheTechniqueBridgeModel fiche)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            fiche.FicheTechniqueBridge_AbonnementID = Id;
            var result = await produitFicheTechniqueService.CreateFiche(fiche);
            return result;
        }
        public IActionResult ListeFichesTechniques(int? categ, int? SousCateg, string name, int pg = 1)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["ProduitVendable_FamilleProduitId"] = new SelectList(produitVendableService.getListFamilleProduit(Id), "FamilleProduit_Id", "FamilleProduit_Libelle");
            var query = produitFicheTechniqueService.getListFicheTechniqueAll(Id, categ, SousCateg);
            if (!String.IsNullOrEmpty(name))
                query = query.Where(p => p.FicheTechniqueBridge_Libelle.Contains(name, StringComparison.OrdinalIgnoreCase));
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;
            int recsCount = query.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var model = query.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(model);
        }


        public IActionResult Modification(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var donnee = produitFicheTechniqueService.findFormulaireFiche((int)id);
            if (donnee == null)
            {
                return NotFound();
            }
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["FicheTechnique_ProduitVendableId"] = new SelectList(produitFicheTechniqueService.getListProduitVendable(aboId), "ProduitVendable_Id", "ProduitVendable_Designation");
            ViewData["FicheTechnique_MatierePremiereId"] = new SelectList(produitFicheTechniqueService.getListMatierePremiere(aboId), "MatierePremiere_Id", "MatierePremiere_Libelle");
            ViewData["FicheTechnique_UniteMesureId"] = new SelectList(produitFicheTechniqueService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
            return View(donnee);
        }
        [HttpPost]

        public async Task<IActionResult> Modification(int id, ProduitFicheTechniqueModel ficheModel)
        {
            var redirect = await produitFicheTechniqueService.updateFormulaireFicheTechnique(id, ficheModel);
            if (redirect)
            {
                TempData["Modification"] = "OK";
                return Redirect("/FicheTechnique/ListeFichesTechniques");
            }
            else
            {
                var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                ViewData["FicheTechnique_ProduitVendableId"] = new SelectList(produitFicheTechniqueService.getListProduitVendable(aboId), "ProduitVendable_Id", "ProduitVendable_Designation");
                ViewData["FicheTechnique_MatierePremiereId"] = new SelectList(produitFicheTechniqueService.getListMatierePremiere(aboId), "MatierePremiere_Id", "MatierePremiere_Libelle");
                ViewData["FicheTechnique_UniteMesureId"] = new SelectList(produitFicheTechniqueService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
                return View();
            }
        }


        [HttpPost]
        public async Task<bool> DeleteFicheTechnique(int id)
        {
            var result = await produitFicheTechniqueService.deleteFicheTechnique(id);
            return result;
        }

        public IActionResult ListeUniteMesure()
        {
            return View(produitFicheTechniqueService.getListUniteMesure());
        }
        [HttpPost]

        public IActionResult Detailss(int Id)
        {
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            return View(produitFicheTechniqueService.getListFicheTechnique(Id, aboId));

        }
        public IActionResult Details(int Id)
        {
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            return View(produitFicheTechniqueService.getListFicheTechnique(Id, aboId));
        }
        [HttpPost]
        public async Task<bool> SetInUse(int id)
        {
            var result = await produitFicheTechniqueService.SetInUse(id);
            return result;
        }
        public IActionResult Formes(int Id)
        {
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            return View(produitFicheTechniqueService.GetFicheFormes(Id));
        }
        public IActionResult AjouterFicheBase()
        {
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["produitBase"] = new SelectList(produitVendableService.getListProduitBase(aboId,null), "ProduitBase_ID", "ProduitBase_Designation");
            ViewData["FicheTechnique_MatierePremiereId"] = new SelectList(produitFicheTechniqueService.getListMatierePremiere(aboId), "MatierePremiere_Id", "MatierePremiere_Libelle");
            ViewData["FicheTechnique_UniteMesureId"] = new SelectList(produitFicheTechniqueService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
            return View();
        }
        [HttpPost]
        public async Task<bool> AjouterFicheBase(FicheTehcniqueProduitBaseModel fiche)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            fiche.FicheTechniqueProduitBase_AbonnementID = Id;
            var result = await produitFicheTechniqueService.CreateFicheBase(fiche);
            return result;
        }
        public IActionResult ListeFichesTechniquesBase(int? categ, int? SousCateg)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["ProduitVendable_FamilleProduitId"] = new SelectList(produitVendableService.getListFamilleProduit(Id), "FamilleProduit_Id", "FamilleProduit_Libelle");
            return View(produitFicheTechniqueService.getListFicheTechniqueBaseAll(Id, categ, SousCateg));
        }
        public IActionResult DetailsBase(int Id)
        {
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            return View(produitFicheTechniqueService.getListFicheTechniqueBase(Id, aboId));
        }
        [HttpPost]
        public async Task<bool> SetInUseBase(int id)
        {
            var result = await produitFicheTechniqueService.SetInUseBase(id);
            return result;
        }
        [HttpPost]
        public decimal CalculerPrix(int matiere, int unite, decimal quantite)
        {
            var prix = produitFicheTechniqueService.CalculerPrix(matiere, unite, quantite);
            return prix;
        }
    }
}
