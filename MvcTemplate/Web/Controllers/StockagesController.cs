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
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class StockagesController : Controller
    {
        private readonly IZoneStockageService zoneStockageService;
        private readonly IGestionMouvementService gestionMouvementService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthentificationRepository authentificationRepository;

        public StockagesController(IZoneStockageService zoneStockageService, UserManager<ApplicationUser> userManager, IAuthentificationRepository authentificationRepository, IGestionMouvementService gestionMouvementService)
        {
            this.zoneStockageService = zoneStockageService;
            this.authentificationRepository = authentificationRepository;
            this.gestionMouvementService = gestionMouvementService;
            _userManager = userManager;
        }
        [Authorize(Roles = "Client,Gerant_de_stock")]
        public IActionResult AjouterZone()
        {
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["ZoneStockage_LieuStockageId"] = new SelectList(zoneStockageService.getListLieuStockage(aboId,1), "LieuStockag_Id", "LieuStockag_Nom");
            ViewData["ZoneStockage_FormeStockageId"] = new SelectList(zoneStockageService.getListFormeSotckage(aboId), "FormStockage_Id", "FormStockage_Libelle");
            ViewData["ZoneStockage_UniteMesureId"] = new SelectList(zoneStockageService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
            ViewData["ZoneStockage_TypeContenuId"] = new SelectList(zoneStockageService.getListTypeContenu(), "TypeContenu_Id", "TypeContenu_Libelle");

            return View();
        }
        [HttpPost]

        public async Task<IActionResult> AjouterZone([FromForm] Zone_StockageModel zoneModel)
        {
            // GET CURRENT USER_ID and query it's abo_ID 
            zoneModel.ZoneStockage_AbonnementId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var redirect = await zoneStockageService.CreatZoneStockage(zoneModel);
            if (redirect)
            {
                TempData["Creation"] = "OK";
                return Redirect("/Stockages/ListeZoneStockage");
            }
            else
            {
                var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                ViewData["ZoneStockage_LieuStockageId"] = new SelectList(zoneStockageService.getListLieuStockage(aboId,1), "LieuStockag_Id", "LieuStockag_Nom");
                ViewData["ZoneStockage_FormeStockageId"] = new SelectList(zoneStockageService.getListFormeSotckage(aboId), "FormStockage_Id", "FormStockage_Libelle");
                ViewData["ZoneStockage_UniteMesureId"] = new SelectList(zoneStockageService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
                ViewData["ZoneStockage_TypeContenuId"] = new SelectList(zoneStockageService.getListTypeContenu(), "TypeContenu_Id", "TypeContenu_Libelle");

                return View();
            }
        }

        [Authorize(Roles = "Client")]

        [HttpPost]

        public async Task<bool> AjouterLieu(Lieu_StockageModel lieu_StockageModel)
        {
            // GET CURRENT USER_ID and query it's abo_ID 
            lieu_StockageModel.LieuStockag_AbonnementId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

            var redirect = await zoneStockageService.CreatLieuStockage(lieu_StockageModel);
            return redirect;
        }


        [Authorize(Roles = "Gerant_de_stock")]
        public IActionResult AjouterSection()
        {
            var userStock =Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["Section_ZoneStockageId"] = new SelectList(zoneStockageService.getListZoneStockage(aboId).Where(z => z.ZoneStockage_IsActive == 1), "ZoneStockage_Id", "ZoneStockage_Designation");
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> AjouterSection([FromForm] Section_StockageModel section_StockageModel)
        {
            // GET CURRENT USER_ID and query it's abo_ID 
            var redirect = await zoneStockageService.CreatSectionStockage(section_StockageModel);
            if (redirect)
            {
                TempData["Creation"] = "OK";
                return Redirect("/Stockages/ListeSectionStockage");
            }
            else
            {
                var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                ViewData["Section_ZoneStockageId"] = new SelectList(zoneStockageService.getListZoneStockage(aboId).Where(z => z.Lieu_Stockage.LieuStockag_UTILISATEUR == HttpContext.User.Identity.Name.ToString()), "ZoneStockage_Id", "ZoneStockage_Designation");


                return View();
            }
        }

        [Authorize(Roles = "Client,Gerant_de_stock")]
        public IActionResult AjouterForme()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> AjouterForme([FromForm] Forme_StockageModel forme_StockageModel)
        {
            // GET CURRENT USER_ID and query it's abo_ID 
            forme_StockageModel.FormStockage_AbonnementId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var redirect = await zoneStockageService.CreatFormeStockage(forme_StockageModel);
            if (redirect)
            {
                TempData["Creation"] = "OK";
                return Redirect("/Stockages/ListeFormeStockage");
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Client,Gerant_de_stock")]

        public IActionResult ListeZoneStockage()
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

            if (User.IsInRole("Gerant_de_stock"))
            {
                return View(zoneStockageService.getListZoneStockage(Id).Where(z => z.ZoneStockage_LieuStockageId ==Convert.ToInt32(HttpContext.Session.GetString("mysession"))));
            }
            return View(zoneStockageService.getListZoneStockage(Id));

        }
        [Authorize(Roles = "Client,Gerant_de_stock")]

        public IActionResult ListeSectionStockage(int? zoneID)
        {
            var current = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            ViewData["zone"] = new SelectList(zoneStockageService.getListZones(current), "ZoneStockage_Id", "ZoneStockage_Designation");
            return View(zoneStockageService.getListSectionSotckage(current,zoneID));
        }
        [Authorize(Roles = "Client")]

        public IActionResult ListeLieuStockage(int? statut)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["User"] = new SelectList(_userManager.GetUsersInRoleAsync("Gerant_de_stock").Result.Where(u => u.Abonnement_ISACTIVE == 1 & u.Abonnement_ID == Id).AsEnumerable(), "Id", "Nom");
            ViewData["Ville"] = new SelectList(zoneStockageService.getlistVille(), "Ville_ID", "Ville_Libelle");
            return View(zoneStockageService.getListLieuStockage(Id,statut));
        }
        [Authorize(Roles = "Client,Gerant_de_stock")]

        public IActionResult ListeFormeStockage()
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            return View(zoneStockageService.getListFormeSotckage(Id));
        }

        [HttpPost]
        public async Task<bool> DeleteZoneStockage(int id, int code)
        {
            var result = await zoneStockageService.deleteZoneStockage(id, code);
            return result;
        }

        [HttpPost]
        public async Task<bool> DeleteFormeStockage(int id)
        {
            var result = await zoneStockageService.deleteFormeStockage(id);
            return result;
        }

        [HttpPost]
        public async Task<bool> DeleteLieuStockage(int id, int code)
        {
            var result = await zoneStockageService.deleteLieuStockage(id, code);
            return result;
        }

        [HttpPost]
        public async Task<bool> DeleteSectionStockage(int id, int code)
        {
            var result = await zoneStockageService.deleteSectionStockage(id, code);
            return result;
        }
        [Authorize(Roles = "Client")]

        public IActionResult AjouterType()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> AjouterType([FromForm] MouvementTypeModel mouvementTypeModel)
        {
            // GET CURRENT USER_ID and query it's abo_ID 
            mouvementTypeModel.MouvementType_AbonnementId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var redirect = await this.gestionMouvementService.CreateTypeMouvement(mouvementTypeModel);
            if (redirect)
            {
                TempData["Creation"] = "OK";
                return Redirect("/Stockages/ListeLieuStockage");
            }
            else
            {
                return View();
            }
        }

        public IActionResult ModificationPointStock(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["LieuStockag_NomResponsable"] = new SelectList(_userManager.GetUsersInRoleAsync("Gerant_de_stock").Result.Where(u => u.LieuStockage_ID == null).AsEnumerable(), "Nom", "Nom");
            ViewData["LieuStockag_VilleID"] = new SelectList(zoneStockageService.getlistVille(), "Ville_ID", "Ville_Libelle");
            var donnee = zoneStockageService.findFormulaireLieuStockage((int)id);
            if (donnee == null)
            {
                return NotFound();
            }
            return View(donnee);
        }

        [HttpPost]
        public async Task<IActionResult> ModificationPointStock(int id, Lieu_StockageModel lieuModel)
        {
            var redirect = await zoneStockageService.updateFormulaireLieuStockage(id, lieuModel);

            if (redirect)
            {
                TempData["Modification"] = "OK";
                return Redirect("/Stockages/ListeLieuStockage");
            }
            else
            {
                ViewData["LieuStockag_NomResponsable"] = new SelectList(_userManager.GetUsersInRoleAsync("Gerant_de_stock").Result.Where(u => u.LieuStockage_ID == null).AsEnumerable(), "Nom", "Nom");
                ViewData["LieuStockag_VilleID"] = new SelectList(zoneStockageService.getlistVille(), "Ville_ID", "Ville_Libelle");
                return View();
            }
        }     
        public IActionResult ModificationZone(int? id)
        {
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["ZoneStockage_LieuStockageId"] = new SelectList(zoneStockageService.getListLieuStockage(aboId,1), "LieuStockag_Id", "LieuStockag_Nom");
            ViewData["ZoneStockage_FormeStockageId"] = new SelectList(zoneStockageService.getListFormeSotckage(aboId), "FormStockage_Id", "FormStockage_Libelle");
            ViewData["ZoneStockage_UniteMesureId"] = new SelectList(zoneStockageService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
            ViewData["ZoneStockage_TypeContenuId"] = new SelectList(zoneStockageService.getListTypeContenu(), "TypeContenu_Id", "TypeContenu_Libelle");
            if (id == null)
            {
                return NotFound();
            }
            var donnee = zoneStockageService.findFormulaireZoneStockage((int)id);
            if (donnee == null)
            {
                return NotFound();
            }
            return View(donnee);
        }
        [HttpPost]

        public async Task<IActionResult> ModificationZone(int id, Zone_StockageModel zoneModel)
        {
            var redirect = await zoneStockageService.updateFormulaireZoneStockage(id, zoneModel);

            if (redirect)
            {
                TempData["Modification"] = "OK";
                return Redirect("/Stockages/ListeZoneStockage");
            }
            else
            {
                return View();
            }
        } 
        public IActionResult ModificationSection(int? id)
        {
            var userStock =Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["Section_ZoneStockageId"] = new SelectList(zoneStockageService.getListZoneStockage(aboId).Where(z => z.Lieu_Stockage.LieuStockag_Id == (int)userStock && z.ZoneStockage_IsActive==1), "ZoneStockage_Id", "ZoneStockage_Designation");
            if (id == null)
            {
                return NotFound();
            }
            var donnee = zoneStockageService.findFormulaireSectionStockage((int)id);
            if (donnee == null)
            {
                return NotFound();
            }
            return View(donnee);
        }
        [HttpPost]

        public async Task<IActionResult> ModificationSection(int id, Section_StockageModel sectionModel)
        {
            var redirect = await zoneStockageService.updateFormulaireSectionStockage(id, sectionModel);

            if (redirect)
            {
                TempData["Modification"] = "OK";
                return Redirect("/Stockages/ListeSectionStockage");
            }
            else
            {
                var userStock =Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                ViewData["Section_ZoneStockageId"] = new SelectList(zoneStockageService.getListZoneStockage(aboId).Where(z => z.Lieu_Stockage.LieuStockag_Id == (int)userStock && z.ZoneStockage_IsActive == 1), "ZoneStockage_Id", "ZoneStockage_Designation");
                return View();
            }
        }
        public IActionResult ConsulterZone(int Id)
        {
            return View("~/Views/Stockages/ListeZoneStockage.cshtml", zoneStockageService.getListZones(Id));

        }

    }

}
