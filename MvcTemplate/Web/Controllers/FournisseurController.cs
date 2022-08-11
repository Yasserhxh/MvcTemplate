using Domain.Authentication;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.IRepositories;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Helpers;

namespace Web.Controllers
{
    [Authorize(Roles = "Client")]

    public class FournisseurController : Controller
    {
        private readonly IFournisseurService fournisseurService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGestionMouvementService gestionMouvementService;

        public FournisseurController(IFournisseurService fournisseurService, UserManager<ApplicationUser> userManager, IGestionMouvementService gestionMouvementService)
        {
            this.fournisseurService = fournisseurService;
            _userManager = userManager;
            this.gestionMouvementService = gestionMouvementService;
        }

        public IActionResult Ajouter()
        {
            ViewData["Fonction"] = new SelectList(fournisseurService.getListFonction(), "Fonction_ID", "Fonction_Libelle");
            ViewData["Ville"] = new SelectList(fournisseurService.getListeVille(), "Ville_ID", "Ville_Libelle");
            return View();
        }
        [HttpPost]
        public async Task<bool> AjouterFournisseur(FournisseurModel fournisseurModel)
        {
            fournisseurModel.Founisseur_AbonnementId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            fournisseurModel.Founisseur_UtilisateurId = _userManager.GetUserId(HttpContext.User);
            var redirect = await fournisseurService.CreateFournisseur(fournisseurModel);
            return redirect;
        }
        public IActionResult ListeFournisseur(int? Ville, int? statut, int pg = 1)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["Ville"] = new SelectList(fournisseurService.getListeVille(), "Ville_ID", "Ville_Libelle");
            ViewData["matieres"] = new SelectList(fournisseurService.getListMatiere(Id), "MatierePremiere_Id", "MatierePremiere_Libelle");
            var query = fournisseurService.getListFournisseur(Id, Ville, statut);
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
        public IActionResult ConsulterMatieres(int Id)
        {
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["matieres"] = new SelectList(fournisseurService.getListMatiere(aboId), "MatierePremiere_Id", "MatierePremiere_Libelle");
            return View(fournisseurService.getListMatiereLink(Id));
        }
        /*public IActionResult ListeAllFournisseur()
		{
;
			return View(fournisseurService.getLisAlltFournisseur());
		}*/

        public IActionResult Modification(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var donnee = fournisseurService.findFormulaireFournisseur((int)id);
            if (donnee == null)
            {
                return NotFound();
            }
            ViewData["Ville"] = new SelectList(fournisseurService.getListeVille(), "Ville_ID", "Ville_Libelle");

            return View(donnee);
        } 
        
        [HttpPost]

        public async Task<bool> Modification(int id, FournisseurModel fournisseurModel)
        {
            var redirect = await fournisseurService.updateFormulaireFournisseur(id, fournisseurModel);
            return redirect;
        }


        [HttpPost]
        public async Task<bool> DeleteFournisseur(int id, int code)
        {
            var result = await fournisseurService.deleteFournisseur(id, code);
            return result;
        } 
        [HttpPost]
        public async Task<bool> DeleteMatieresLink(int id, int code)
        {
            var result = await fournisseurService.deleteMatieresLink(id, code);
            return result;
        }
        [HttpPost]
        public string getFournisseurNom(int Id )
        {
            return fournisseurService.findFormulaireFournisseur(Id).Founisseur_RaisonSocial;
        }
        [HttpPost]
        public async Task<bool> AjouterMatiere(int idFournisseur, List<int> listMatiere)
        {
            try
            {
                
                await fournisseurService.AjouterMatiere(idFournisseur, listMatiere);
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IActionResult ListeBonDeCommande(int? fournisseurID, string date, int pg = 1)
        {
            if (date == null || date == "null" || date == "")
                date = "";
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["fournisseur"] = new SelectList(gestionMouvementService.getListFournisseur(Id), "Founisseur_Id", "Founisseur_RaisonSocial");
            var point = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var query = fournisseurService.GetBonDeCommandes(Id, point, fournisseurID, date);
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
        public IActionResult ListeBonDeLivraison(int bonCommandeID, string date, int pg = 1)
        {
            if (date == null || date == "null" || date == "")
                date = "";
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var query = fournisseurService.GetBonDeLivraisons(bonCommandeID, Id, date);
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
        public IActionResult AjouterBC()
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["fournisseur"] = new SelectList(gestionMouvementService.getListFournisseur(Id), "Founisseur_Id", "Founisseur_RaisonSocial");
            return View();
        }
        [HttpPost]
        public async Task<bool> AjouterBC(BonDeCommande_Model bonDeCommande_Model)
        {
            bonDeCommande_Model.BonDeCommande_AbonnementID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            bonDeCommande_Model.BonDeCommande_CreePar = _userManager.GetUserId(HttpContext.User);
            bonDeCommande_Model.BonDeCommande_PointStockID = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var redirect = await fournisseurService.CreateBonDeCommande(bonDeCommande_Model);
            return redirect;
        }
    }
}
