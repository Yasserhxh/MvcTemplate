using Domain.Authentication;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.IRepositories;
using Service.IServices;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Authorize(Roles = "Client")]

    public class FamilleProduitsController : Controller
    {

        private readonly IFamilleProduitService familleProduitService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthentificationRepository authentificationRepository;

        public FamilleProduitsController(IFamilleProduitService familleProduitService, UserManager<ApplicationUser> userManager, IAuthentificationRepository authentificationRepository)
        {
            this.familleProduitService = familleProduitService;
            _userManager = userManager;
            this.authentificationRepository = authentificationRepository;
        }

        /*		public IActionResult Ajouter()
                {
                    var aboId = thisConvert.ToInt32(HttpContext.User.FindFirst("AboId").Value);;

                    ViewData["FamilleProduit_ParentId"] = new SelectList(familleProduitService.getListFamilles(aboId), "FamilleProduit_ParentId", "FamilleProduit_Libelle");

                    return View();
                }*/
        [HttpPost]

        public async Task<bool> Ajouter(FamilleProduitModel familleModel)
        {
            // GET CURRENT USER_ID and query it's abo_ID 
            familleModel.FamilleProduit_AbonnemnetId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var redirect = await familleProduitService.CreateFamille(familleModel);
            return redirect;
        }
        [HttpPost]

        public async Task<bool> AjouterSousFamille(SousFamilleModel sousFamilleModel)
        {
            // GET CURRENT USER_ID and query it's abo_ID 
            sousFamilleModel.SousFamille_AbonnementID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var redirect = await familleProduitService.CreatSousFamille(sousFamilleModel);
            return redirect;
        }

        public IActionResult ListeProduits()
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

            return View(familleProduitService.getListFamilles(Id));
        }
        public IActionResult ListeSousFamilles()
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

            return View(familleProduitService.getListSousFamilles(Id));
        }

        public IActionResult Modification(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var donnee = familleProduitService.findFormulaireFamille((int)id);
            if (donnee == null)
            {
                return NotFound();
            }
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);;
            ViewData["FamilleProduit_ParentId"] = new SelectList(familleProduitService.getListFamilles(aboId), "FamilleProduit_ParentId", "FamilleProduit_Libelle");
            return View(donnee);
        }
        [HttpPost]

        public async Task<IActionResult> Modification(int id, FamilleProduitModel familleModel)
        {
            var redirect = await familleProduitService.updateFormulaireFamille(id, familleModel);
            if (redirect)
            {
                TempData["Modification"] = "OK";
                return Redirect("/FamilleProduits/ListeProduits");
            }
            else
            {
                var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);;
                ViewData["FamilleProduit_ParentId"] = new SelectList(familleProduitService.getListFamilles(aboId), "FamilleProduit_ParentId", "FamilleProduit_Libelle");
                return View();
            }
        }


        [HttpPost]
        public async Task<bool> DeleteFamille(int id)
        {
            var result = await familleProduitService.deleteFormulaireFamille(id);
            return result;
        }

    }

}
