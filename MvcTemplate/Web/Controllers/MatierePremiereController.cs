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
    public class MatierePremiereController : Controller
    {
        private readonly IMatierePremiereService matierePremiereService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthentificationRepository authentificationRepository;
        private readonly IZoneStockageService zoneStockageService;
        private readonly IGestionMouvementService gestionMouvementService;
        private readonly IProduitVendableService produitVendableService;
        private readonly IPointVenteService pointVenteService;

        public MatierePremiereController(IMatierePremiereService matierePremiereService, UserManager<ApplicationUser> userManager, IAuthentificationRepository authentificationRepository,
            IZoneStockageService zoneStockageService, IGestionMouvementService gestionMouvementService, IProduitVendableService produitVendableService, IPointVenteService pointVenteService)
        {
            this.authentificationRepository = authentificationRepository;
            this.matierePremiereService = matierePremiereService;
            this.zoneStockageService = zoneStockageService;
            this.gestionMouvementService = gestionMouvementService;
            this.produitVendableService = produitVendableService;
            this.pointVenteService = pointVenteService;
            _userManager = userManager;
        }
        [Authorize(Roles = "Client")]

        public IActionResult AjouterMatierePremiere()
        {
            var abo = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["MatierePremiere_UniteMesureId_FT"] = new SelectList(zoneStockageService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
            ViewData["MatierePremiere_FormeStockageId"] = new SelectList(matierePremiereService.getListFormeStockage(abo), "FormStockage_Id", "FormStockage_Libelle");
            ViewData["MatierePremiere_AllergeneId"] = new SelectList(matierePremiereService.getListAllergene(abo), "Allergene_Id", "Allergene_Libelle");
            //ViewData["Fournisseur"] = new SelectList(matierePremiereService.getListFournisseur(abo), "Founisseur_Id", "Founisseur_RaisonSocial");
            ViewData["FamilleParent"] = new SelectList(matierePremiereService.getListMatiereFamilleParent(abo), "MatiereFamilleParent_ID", "MatiereFamilleParent_Libelle");


            return View();
        }
        [HttpPost]

        public async Task<bool> AjouterMatierePremiere(MatierePremiereModel matiereModel, List<int> ListeUnite)
        {
            // GET CURRENT USER_ID and query it's abo_ID 
            var abo = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            matiereModel.MatierePremiere_AbonnementId = abo;
            var redirect = await matierePremiereService.CreateMatierePremiere(matiereModel,ListeUnite);
            return redirect;
            

        }
        [HttpPost]
        public SelectList familleList(int familleParent)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            SelectList sousfamilleList = new SelectList(matierePremiereService.getListMatiereFamille(Id).Where(s => s.MatiereFamille_ParentID == familleParent), "MatiereFamille_ID", "MatiereFamille_Libelle");
            return sousfamilleList;
        }

        [Authorize(Roles = "Client")]
        public IActionResult AjouterAllergene()
        {
            return View();
        }
        [HttpPost]

        public async Task<bool> AjouterAllergene(AllergeneModel allergeneModel)
        {
            // GET CURRENT USER_ID and query it's abo_ID 
            var abo = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            allergeneModel.Allergene_AbonnementId = abo;
            var redirect = await matierePremiereService.CreateAllergene(allergeneModel);
            return redirect;
        }

        [Authorize(Roles = "Client,Gerant_de_stock")]
        public IActionResult ListeMatierePremiere(int? allergene,int? forme, string name, int pg=1)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["MatierePremiere_FormeStockageId"] = new SelectList(matierePremiereService.getListFormeStockage(Id), "FormStockage_Id", "FormStockage_Libelle");
            ViewData["MatierePremiere_AllergeneId"] = new SelectList(matierePremiereService.getListAllergene(Id), "Allergene_Id", "Allergene_Libelle");
            var query = matierePremiereService.getListMatierePremiere(Id, allergene, forme);
            if (!String.IsNullOrEmpty(name))
                query = query.Where(p => p.MatierePremiere_Libelle.Contains(name, StringComparison.OrdinalIgnoreCase)); 
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;
            int recsCount = query.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var model = query.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            if (User.IsInRole("Gerant_de_stock"))
            {
                return View("~/Views/MatierePremiere/ListeMatierePremiereGerant.cshtml", model);
            }
            if (User.IsInRole("Client"))
            {
                ViewData["Unite"] = new SelectList(zoneStockageService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
                return View(model);
            }
            else
            {
                return NotFound();
            }


        }
        [Authorize(Roles = "Client")]
        public IActionResult ListeAllergie()
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            return View(matierePremiereService.getListAllergene(Id));
        }

        [Authorize(Roles = "Gerant_de_stock")]
        public IActionResult MatiereStock(int? zone, int? section, int pg = 1)
        {
            var lieuId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["ZoneStockage_LieuStockageId"] = new SelectList(zoneStockageService.getListZones(lieuId), "ZoneStockage_Id", "ZoneStockage_Designation");
            var query = matierePremiereService.getListMatiereStockerAll(Id, (int)lieuId, zone, section);
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
        public IActionResult ConsulterUnite(int Id)
        {
            ViewData["Unite"] = new SelectList(zoneStockageService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
            return View(matierePremiereService.getListUniteUtilisation(Id));
        }
        public IActionResult ConsulterHistoriqueAchat(int Id)
        {
            var aboID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            return View(matierePremiereService.getListHistorique(aboID, Id));
        }


        [Authorize(Roles = "Client")]
        public IActionResult ModificationMatierePremiere(int? id)
        {
            var abo = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["MatierePremiere_UniteMesureId_FT"] = new SelectList(zoneStockageService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
            ViewData["MatierePremiere_FormeStockageId"] = new SelectList(matierePremiereService.getListFormeStockage(abo), "FormStockage_Id", "FormStockage_Libelle");
            ViewData["MatierePremiere_AllergeneId"] = new SelectList(matierePremiereService.getListAllergene(abo), "Allergene_Id", "Allergene_Libelle");
            ViewData["FamilleParent"] = new SelectList(matierePremiereService.getListMatiereFamilleParent(abo), "MatiereFamilleParent_ID", "MatiereFamilleParent_Libelle");
            if (id == null)
            {
                return NotFound();
            }
            var donnee = matierePremiereService.findFormulaireMatiereP((int)id);
            if (donnee == null)
            {
                return NotFound();
            }

            return View(donnee);
        }
        [HttpPost]
        public async Task<IActionResult> ModificationMatierePremiere(int id, MatierePremiereModel matiereModel)
        {
            var redirect = await matierePremiereService.updateFormulaireMatiere(id, matiereModel);
            if (redirect)
            {
                TempData["Modification"] = "OK";
                return Redirect("/MatierePremiere/ListeMatierePremiere");
            }
            else
            {
                return View();
            }
        }


        [Authorize(Roles = "Client")]
        public IActionResult ModificationAllergie(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var donnee = matierePremiereService.findFormulaireAllergie((int)id);
            if (donnee == null)
            {
                return NotFound();
            }

            return View(donnee);
        }
        [HttpPost]
        public string getMatiere(int id)
        {
            var matiere = this.matierePremiereService.findFormulaireMatiereP(id);
            return matiere.MatierePremiere_Libelle;
        }
        [HttpPost]

        public async Task<IActionResult> ModificationAllergie(int id, AllergeneModel allergieModel)
        {
            var redirect = await matierePremiereService.updateFormulaireAllergie(id, allergieModel);
            if (redirect)
            {
                TempData["Modification"] = "OK";
                return Redirect("/MatierePremiere/ListeAllergie");
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        public async Task<bool> DeleteMatierePremiere(int id)
        {
            var result = await matierePremiereService.deleteMatiere(id);
            return result;
        }
        [HttpPost]
        public async Task<bool> DeleteUniteLink(int id,int code)
        {
            var result = await matierePremiereService.deleteUniteLink(id,code);
            return result;
        }

        [HttpPost]
        public async Task<bool> DeleteAllergie(int id)
        {
            var result = await matierePremiereService.deleteAllergie(id);
            return result;
        }
        [Authorize(Roles = "Gerant_de_stock")]
        public IActionResult StockerMatiere(int? id)
        {

            var userId = HttpContext.User.FindFirst("userId").Value;
            ViewData["ZoneStockage_LieuStockageId"] = new SelectList(zoneStockageService.getListStockUser(userId), "Lieu_Stockage.LieuStockag_Id", "Lieu_Stockage.LieuStockag_Nom");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> StockerMatiere(int id, [FromForm] MatierePremiereStockageModel matiereModel)
        {
            // GET CURRENT USER_ID and query it's abo_ID 

            matiereModel.MatierePremiereStokage_AbonnementId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

            var redirect = await matierePremiereService.StockerMatiere(id, matiereModel);
            if (redirect)
            {
                TempData["Creation"] = "OK";
                return Redirect("/MatierePremiere/ListeMatierePremiere");
            }
            else
            {
                var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                ViewData["ZoneStockage_LieuStockageId"] = new SelectList(zoneStockageService.getListLieuStockage(aboId,1), "LieuStockag_Id", "LieuStockag_Nom");

                return View();
            }
        }

        [HttpPost]
        public bool EstEnStock(int id)
        {
            var result = matierePremiereService.MatiereStocker(id);
            return result;
        }

        [HttpPost]
        public SelectList sectionList(int zoneId)
        {
            SelectList secteurSelect = new SelectList(zoneStockageService.getListSection(zoneId), "Section_Id", "Section_Designation");
            return secteurSelect;
        }
        [HttpPost]
        public SelectList zoneList(int lieuId)
        {
            SelectList secteurSelect = new SelectList(zoneStockageService.getListZones(lieuId), "ZoneStockage_Id", "ZoneStockage_Designation");
            return secteurSelect;
        }

        [HttpPost]
        public async Task<bool> DeleteproduitEnStock(int id)
        {
            var result = await matierePremiereService.deleteMatierePremiereStockes(id);
            return result;
        }
        [HttpPost]
        public decimal CalculerPrix(int matiere, int unite, decimal quantite)
        {
            var prix = this.matierePremiereService.CalculerPrix(matiere, unite, quantite);
            return prix;
        }
        [HttpPost]
        public string getUniteById(int uniteId)
        {
            return this.matierePremiereService.getUniteById(uniteId);
        }
        [HttpPost]

        public async Task<bool> AjouterFamilleParent(MatiereFamille_ParentModel matiereFamille_ParentModel)
        {
            // GET CURRENT USER_ID and query it's abo_ID 
            matiereFamille_ParentModel.MatiereFamilleParent_AbonnementID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

            var redirect = await matierePremiereService.CreateMatiereFamilleParent(matiereFamille_ParentModel);
            return redirect;
        }
        [HttpPost]

        public async Task<bool> AjouterFamille(MatiereFamilleModel matiereFamilleModel)
        {
            // GET CURRENT USER_ID and query it's abo_ID 
            matiereFamilleModel.MatiereFamille_AbonnementID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

            var redirect = await matierePremiereService.CreateMatiereFamille(matiereFamilleModel);
            return redirect;
        } 
        [HttpPost]
        public async Task<bool> AjouterUniteUtilisation(int idMatiere, List<int> listUnite)
        {
            var redirect = await matierePremiereService.AjouterUnites(idMatiere,listUnite);
            return redirect;
        } 
        [HttpPost]
        public SelectList getListUniteMesure(int Id)
        {
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            SelectList secteurSelect = new SelectList(matierePremiereService.getListUniteMesure(Id, aboId), "UniteMesure_Id", "UniteMesure_Libelle");
            return secteurSelect;
        }
        public IActionResult DeclarationPerte()
        {
            var abo = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var userLieu = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            ViewData["Unite"] = new SelectList(zoneStockageService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
            ViewData["MatiereStockage"] = new SelectList(produitVendableService.getListMatiereStock(userLieu, abo), "MatiereStockAtelier_ID", "Matiere_Premiere.MatierePremiere_Libelle");
            return View();
        }
        [HttpPost]
        public async Task<bool> DeclarationPerte([FromForm] Perte_MatiereModel perte_MatiereModel)
        {
            var abo = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var userLieu = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            perte_MatiereModel.PerteMatiere_AbonnementID = abo;
            perte_MatiereModel.PerteMatiere_AtelierID = userLieu;
            perte_MatiereModel.PerteMatiere_DateCreation = DateTime.Now;
            var redirect = await matierePremiereService.DeclarationPerte(perte_MatiereModel);
            /*if (redirect)
            {
                TempData["Creation"] = "OK";
                return Redirect("/MatierePremiere/ListeDeclarations");
            }
            else
            {
                var aboid = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                ViewData["Unite"] = new SelectList(zoneStockageService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
                ViewData["MatiereStockage"] = new SelectList(produitVendableService.getListMatiereStock(userLieu, abo), "MatiereStockAtelier_ID", "Matiere_Premiere.MatierePremiere_Libelle");
                return View();
            }*/
            return redirect;
        }
        public IActionResult ListeDeclarations(int? atelierID, string date, int? matiereID)
        {
            var abo = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var userLieu = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            if (date == null || date == "null")
                date = "";
            ViewData["MatiereStockage"] = new SelectList(produitVendableService.getListMatiereStock(userLieu, abo), "MatiereStockAtelier_ID", "Matiere_Premiere.MatierePremiere_Libelle");
            ViewData["production"] = new SelectList(produitVendableService.getListAteliers(abo), "Atelier_ID", "Atelier_Nom");
            IEnumerable<Perte_MatiereModel> model;
            if (User.IsInRole("Client"))
            {
                model = matierePremiereService.getListPertes(abo, atelierID, date, matiereID);
            }
            else
            {
                model = matierePremiereService.getListPertes(abo, userLieu, date, matiereID);

            }
            return View(model);
        } 
        public IActionResult DeclarationPerteStock()
        {
            var abo = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var userLieu = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            ViewData["Unite"] = new SelectList(zoneStockageService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
            ViewData["MatiereStockage"] = new SelectList(matierePremiereService.getListMatiereStockerAll(abo, userLieu, null, null), "MatierePremiereStokage_Id", "Matiere_Premiere.MatierePremiere_Libelle");
            return View();
        }
        [HttpPost]
        public async Task<bool> DeclarationPerteStock([FromForm] Perte_MatiereStockModel perte_MatiereStockModel)
        {
            var abo = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var userId = HttpContext.User.FindFirst("userId").Value;
            var userLieu = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            perte_MatiereStockModel.PerteMatiere_AbonnementID = abo;
            perte_MatiereStockModel.PerteMatiere_StockID = userLieu;
            perte_MatiereStockModel.PerteMatiere_Utilisateur = userId;
            var redirect = await matierePremiereService.DeclarationPerteStock(perte_MatiereStockModel);
            return redirect;
        }
        public IActionResult ListeDeclarationsStock(int? stockID, string date, int? matiereID)
        {
            var abo = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var userId = HttpContext.User.FindFirst("userId").Value;
            var userLieu = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            if (date == null || date == "null")
                date = "";
            ViewData["MatiereStockage"] = new SelectList(matierePremiereService.getListMatiereStockerAll(abo, userLieu, null, null), "MatierePremiereStokage_Id", "Matiere_Premiere.MatierePremiere_Libelle");
            //ViewData["production"] = new SelectList(zoneStockageService.getListLieuStockage(abo,1), "LieuStockag_Id", "LieuStockag_Nom");
            IEnumerable<Perte_MatiereStockModel> model;
            if (User.IsInRole("Client"))
            {
                ViewData["production"] = new SelectList(zoneStockageService.getListLieuStockage(abo, 1), "LieuStockag_Id", "LieuStockag_Nom");
                model = matierePremiereService.getListPertesStock(abo, stockID, date, matiereID);
            }
            else
            {
                ViewData["production"] = new SelectList(zoneStockageService.getListStockUser(userId), "Lieu_Stockage.LieuStockag_Id", "Lieu_Stockage.LieuStockag_Nom");
                model = matierePremiereService.getListPertesStock(abo, stockID, date, matiereID);

            }
            return View(model);
        }
        public IActionResult Approvisionnement()
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var point = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            ViewData["production"] = new SelectList(produitVendableService.getListAteliers(Id), "Atelier_ID", "Atelier_Nom");
            ViewData["MatiereStockage"] = new SelectList(matierePremiereService.getListMatiereStockerAll(Id, point, null, null), "MatierePremiereStokage_Id", "Matiere_Premiere.MatierePremiere_Libelle");
            return View("~/Views/MatierePremiere/Approvisionnement.cshtml");
        }
        [HttpPost]
        public string getQteStock(int id)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var matiereStockage = matierePremiereService.getListMatiereStocker(Id, id).FirstOrDefault();
            return matiereStockage.MatierePremiereStokage_QuantiteActuelle + " " + matiereStockage.Matiere_Premiere.Unite_Mesure.UniteMesure_Libelle;
        }
        [HttpPost]
        public async Task<IActionResult> ApprovisionnementMatiere(Approvisionnement_MatiereModel approModel)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            approModel.ApprovisionnementMatiere_Utilisateur = _userManager.GetUserAsync(HttpContext.User).Result.Nom_Complet;
            approModel.ApprovisionnementMatiere_AbonnementID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var userStock = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            approModel.ApprovisionnementMatiere_StockID = userStock;
           // approModel.Approvisionnement_UtilisateurId = _userManager.GetUserId(HttpContext.User);
            var redirect = await matierePremiereService.CreateApprov(approModel);
            if (redirect)
            {
                TempData["Creation"] = "OK";
                ViewData["production"] = new SelectList(produitVendableService.getListAteliers(Id), "Atelier_ID", "Atelier_Nom");
                ViewData["MatiereStockage"] = new SelectList(matierePremiereService.getListMatiereStockerAll(Id, userStock, null, null), "MatierePremiereStokage_Id", "Matiere_Premiere.MatierePremiere_Libelle");
                return View("~/Views/MatierePremiere/Approvisionnement.cshtml");
            }
            else
            { 
                TempData["Creation"] = "OK";
                ViewData["production"] = new SelectList(produitVendableService.getListAteliers(Id), "Atelier_ID", "Atelier_Nom");
                ViewData["MatiereStockage"] = new SelectList(matierePremiereService.getListMatiereStockerAll(Id, userStock, null, null), "MatierePremiereStokage_Id", "Matiere_Premiere.MatierePremiere_Libelle");
                return View("~/Views/MatierePremiere/Approvisionnement.cshtml");

            }
        }

        public IActionResult ListeApprovisionnement(int? stockID, int? prodID, string etat, string date)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            if (date == "" || date == null || date == "null")
                date = ""; 
            if (etat == "" || etat == null || etat == "null")
                etat = "";
            ViewData["production"] = new SelectList(produitVendableService.getListAteliers(Id), "Atelier_ID", "Atelier_Nom");
            ViewData["Lieu"] = new SelectList(zoneStockageService.getListLieuStockage(Id, 1), "LieuStockag_Id", "LieuStockag_Nom");
            if (User.IsInRole("Client"))
            {
                var approv = matierePremiereService.getListApprov(Id, stockID, date, etat, prodID);
                return View("~/Views/MatierePremiere/ListeApprovisionnement.cshtml",approv);
            }
            else if (User.IsInRole("Gerant_de_stock"))
            {
                var point = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                var approv = matierePremiereService.getListApprov(Id, point, date, etat, prodID);
                return View("~/Views/MatierePremiere/ListeApprovisionnement.cshtml", approv);

            }
            else
            {
                var point = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                var approv = matierePremiereService.getListApprov(Id, stockID, date, etat, point);
                return View("~/Views/MatierePremiere/ListeApprovisionnement.cshtml", approv);
            }
        }
        public Task<bool> ValiderApprovisionnement(int Approvisionnement_Id)
        {
            int pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var valideId = _userManager.GetUserAsync(HttpContext.User).Result.Nom_Complet;
            return matierePremiereService.ValiderApprovisionnement(Approvisionnement_Id, valideId, pdvId);

        }
    }
}
