using Domain.Authentication;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NHibernate.Cfg;
using Repository.IRepositories;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Files = System.IO.File;

namespace Web.Controllers
{
    public class PointVenteController : Controller
    {
        private readonly IPointVenteService pointVenteService;
        private readonly IProduitVendableService produitVendableService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthentificationRepository authentificationRepository;
        private readonly IHostingEnvironment _environment;

        public PointVenteController(IPointVenteService pointVenteService, IProduitVendableService produitVendableService, UserManager<ApplicationUser> userManager, IAuthentificationRepository authentificationRepository, IHostingEnvironment environment)
        {
            this.pointVenteService = pointVenteService;
            this.authentificationRepository = authentificationRepository;
            this.produitVendableService = produitVendableService;
            _userManager = userManager;
            _environment = environment;
        }

        [Authorize(Roles = "Client")]

        [HttpPost]
        public async Task<bool> Ajouter(Point_VenteModel pdv)
        {
            // GET CURRENT USER_ID and query it's abo_ID 
            var newFileName = string.Empty;
            if (HttpContext.Request.Form.Files != null)
            {
                var fileName = string.Empty;
                string PathDB = string.Empty;

                var files = HttpContext.Request.Form.Files;

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        //Getting FileName
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var FileExtension = Path.GetExtension(fileName);

                        // concating  FileName + FileExtension
                        newFileName = myUniqueFileName + FileExtension;

                        // Combines two strings into a path.
                        fileName = Path.Combine(_environment.WebRootPath, "images") + $@"\{newFileName}";

                        // if you want to store path of folder in database
                        PathDB = "images/" + newFileName;

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                        pdv.PointVente_Logo = PathDB;

                    }
                }


            }
            // GET CURRENT USER_ID and query it's abo_ID 
            pdv.PointVente_AbonnementId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var redirect = await pointVenteService.CreatePointVente(pdv);


            return redirect;
        }
        [Authorize(Roles = "Client")]

        public IActionResult ListePointVente(int? type, int? statut)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["Ateliers"] = new SelectList(pointVenteService.getListAtelier(Id), "Atelier_ID", "Atelier_Nom");
            ViewData["Stocks"] = new SelectList(pointVenteService.getListPointStock(Id), "LieuStockag_Id", "LieuStockag_Nom");
            ViewData["Familles"] = new SelectList(pointVenteService.getListFamilles(Id), "FamilleProduit_Id", "FamilleProduit_Libelle");
            ViewData["User"] = new SelectList(_userManager.GetUsersInRoleAsync("Responsable_Vente").Result.Where(u => u.Abonnement_ISACTIVE == 1 & u.Abonnement_ID == Id).AsEnumerable(), "Id", "Nom_Complet");
            ViewData["Ville"] = new SelectList(pointVenteService.getListVilles(), "Ville_ID", "Ville_Libelle");

            if (User.IsInRole("Administrateur") == true)
                return RedirectToAction("Register", "Authentification");
            return View(pointVenteService.getListPointVente(Id, type, statut));
        }
        [Authorize(Roles = "Client")]

        public IActionResult Modification(int? id)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

            if (id == null)
            {
                return NotFound();
            }
            ViewData["PointVente_VilleID"] = new SelectList(pointVenteService.getListVilles(), "Ville_ID", "Ville_Libelle");
            ViewData["PointVente_NomResponsable"] = new SelectList(_userManager.GetUsersInRoleAsync("Responsable_Vente").Result.Where(u => u.Abonnement_ISACTIVE == 1 & u.Abonnement_ID == Id).AsEnumerable(), "Nom_Complet", "Nom_Complet");
            var donnee = pointVenteService.findFormulairePointVente((int)id);
            if (donnee == null)
            {
                return NotFound();
            }

            return View(donnee);
        }
        [Authorize(Roles = "Client")]

        [HttpPost]

        public async Task<IActionResult> Modification(int id, Point_VenteModel pontventeModel)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

            var redirect = await pointVenteService.updateFormulairePointVente(id, pontventeModel);
            if (redirect)
            {
                TempData["Modification"] = "OK";
                return Redirect("/PointVente/ListePointVente");
            }
            else
            {
                ViewData["PointVente_VilleID"] = new SelectList(pointVenteService.getListVilles(), "Ville_ID", "Ville_Libelle");
                ViewData["PointVente_NomResponsable"] = new SelectList(_userManager.GetUsersInRoleAsync("Responsable_Vente").Result.Where(u => u.Abonnement_ISACTIVE == 1 & u.Abonnement_ID == Id).AsEnumerable(), "Nom_Complet", "Nom_Complet");
                return View();
            }
        }
        [Authorize(Roles = "Client")]


        [HttpPost]
        public async Task<bool> DeletePointVente(int id, int code)
        {
            var result = await pointVenteService.deletePointVente(id, code);
            return result;
        }
        [HttpPost]
        public async Task<bool> DeleteFamillePdv(int id, int code)
        {
            var result = await pointVenteService.deleteFamillePdv(id, code);
            return result;
        }
        [HttpPost]
        public string getPvNom(int Id)
        {
            return pointVenteService.findFormulairePointVente(Id).PointVente_Nom;
        }
        [HttpPost]
        public string getPositionDesi(int Id)
        {
            return pointVenteService.findFormulairePositionVente(Id).PositionVente_Libelle;
        }
        [HttpPost]
        public string getPositionDesignation(int Id)
        {
            return pointVenteService.findFormulaireSalle(Id).Salle_Libelle;
        }
        [HttpPost]
        public string getSalleDesi(int Id, int salleid)
        {

            var salle = pointVenteService.findFormulairePositionVente(Id).Salles;
            var sallename = salle.Where(p => p.Salle_Id == salleid).FirstOrDefault();
            return sallename.Salle_Libelle;
        }
        [Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<bool> AjouterFamille(int idPointVente, List<int> listFamille)
        {
            try
            {

                var res = await pointVenteService.AjouterFamille(idPointVente, listFamille);
                if (res == null)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [Authorize(Roles = "Responsable_Vente")]

        [HttpPost]
        public async Task<bool> AjouterPosition(PositionVenteModel positionVenteModel)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            positionVenteModel.PositionVente_AbonnementId = Id;
            var pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            positionVenteModel.PositionVente_PointVenteId = pdvId;
            var redirect = await pointVenteService.CreatePositionVente(positionVenteModel);
            return redirect;
        }
        [Authorize(Roles = "Responsable_Vente")]

        public IActionResult ListePositionsVente()
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

            ViewData["mode"] = new SelectList(pointVenteService.getListModePaiement(), "ModePaiement_Id", "ModePaiement_Libelle");
            ViewData["User"] = new SelectList(_userManager.GetUsersInRoleAsync("Position_Vente").Result.Where(u => u.PositionVente_ID == null & u.Abonnement_ID == Id & u.Abonnement_ISACTIVE == 1).AsEnumerable(), "Id", "Nom");
            return View("~/Views/PointVente/GestionSalles/ListePositionsVente.cshtml", pointVenteService.getListPositionVente(Convert.ToInt32(HttpContext.Session.GetString("mysession"))));
        }
        public IActionResult ConsulterSalle(int Id)
        {
            return View("~/Views/PointVente/GestionSalles/ListeSalles.cshtml", pointVenteService.getListSalles(Id));
        }
        [HttpPost]
        public async Task<bool> AjouterSalle(int id, List<SalleModel> salle)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            try
            {
                foreach (var f in salle)
                {
                    f.Salle_AbonnementId = AboId;
                    f.Salle_UtilisateurId = _userManager.GetUserId(HttpContext.User);
                    await pointVenteService.AjouterSalle(id, f);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost]
        public async Task<bool> AjouterTable(int id, List<TableModel> table)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            try
            {
                foreach (var f in table)
                {
                    f.Table_AbonnementId = AboId;
                    f.Table_UtilisateurId = _userManager.GetUserId(HttpContext.User);
                    f.Table_DateCreation = DateTime.Now;

                }
                await pointVenteService.AjouterTable(id, table);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost]
        public string getRespoName(string userId)
        {
            var qte = authentificationRepository.GetUserById(userId).Nom_Complet;
            return qte;
        }
        [HttpPost]
        public string getRespoPhoneNumber(string userId)
        {
            var qte = authentificationRepository.GetUserById(userId).PhoneNumber;
            return qte;
        }
        public IActionResult ConsulterFamilles(int Id)
        {
            ViewData["Familles"] = new SelectList(pointVenteService.getListFamilles(Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value)), "FamilleProduit_Id", "FamilleProduit_Libelle");
            return View("~/Views/PointVente/ListeDesFamilles.cshtml", pointVenteService.getListFammilesPointVente(Id));
        }
        public IActionResult ConsulterTables(int Id)
        {
            return View("~/Views/PointVente/GestionSalles/ListeTables.cshtml", pointVenteService.getListTables(Id));
        }
        [HttpPost]
        public SelectList getListAllProduitStock(int pointventeID)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var stocks = pointVenteService.getStockPointVente(AboId, pointventeID);
            var model = new List<VenteApiModel>();
            if (stocks.Count() > 1)
            {
                var stock = stocks.GroupBy(p => new { key = p.PointVenteStock_ProduitID, item = p.Produit_Vendable.ProduitVendable_Designation,sousCateg = p.Produit_Vendable.ProduitVendable_FamilleProduitId });
                foreach (var s in stock)
                {
                    var v1 = new VenteApiModel
                    {
                        ID = s.Key.key,
                        Designation = s.Key.item,
                        Type = "vendable",
                        concat = s.Key.key + "," + "vendable" + "," + s.Key.sousCateg,
                    };
                    model.Add(v1);
                }
            }
            else
            {
                foreach (var s in stocks)
                {
                    var v1 = new VenteApiModel
                    {
                        ID = s.PointVenteStock_ProduitID,//s.Key.key,
                        Designation = s.Produit_Vendable.ProduitVendable_Designation, //s.Key.item,
                        Type = "vendable",
                        concat = s.PointVenteStock_ProduitID + "," + "vendable" + "," + s.Produit_Vendable.ProduitVendable_FamilleProduitId,

                    };
                    model.Add(v1);
                }
            }
            var pp = pointVenteService.getStockProduitPackage(AboId, pointventeID).Where(p => p.formes.Count() > 0);
            var package = pp.Where(p => p.formes.Where(f => f.FormeProduit_PrixVente > 0).Count() > 0);
            var prt = produitVendableService.getListProduitConsomableEnMagasin(AboId, pointventeID, null);
            var pret = prt;
            foreach (var pack in package)
            {
                var v1 = new VenteApiModel
                {
                    ID = pack.ProduitPackage_ID,
                    Designation = pack.ProduitPackage_Designation,
                    Type = "package",
                    concat = pack.ProduitPackage_ID + "," + "package" + "," + pack.ProduitPackage_FamilleID,

                };
                model.Add(v1);
            }
            foreach (var p in pret)
            {
                var v1 = new VenteApiModel
                {
                    ID = p.ProduitPretConsomer_ID,
                    Designation = p.ProduitPretConsomer_Designation,
                    Type = "consomable",
                    concat = p.ProduitPretConsomer_ID + "," + "consomable" +","+ p.ProduitPretConsomer_FamilleProduit,
                };
                model.Add(v1);
            }
            return new SelectList(model, "concat", "Designation");
        }
        [HttpPost]
        public SelectList getListPointVenteByCateg(string concat, int pdv)
        {
            var res = pointVenteService.getListePdvByCateg(Convert.ToInt32(concat), pdv);
            var select = new SelectList(res, "PointVente_Id", "PointVente_Nom");
            return select;
        }
        [HttpPost]
        public SelectList getListFormesByProduit(string concat)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var split = concat.Split(",");
            var produitID = Convert.ToInt32(split.First());
            var categ = split[1];
            var pdv = Convert.ToInt32(split.Last());
            if (categ == "vendable")
            {
                var formesAll = pointVenteService.getStockPointVente(AboId, pdv).Where(p => p.PointVenteStock_ProduitID == produitID);
                var formes = formesAll.Select(p => p.Forme_Produit);
                var model = new List<FormeApiResultModel>();
                var select = new SelectList(formes, "FormeProduit_ID", "FormeProduit_Libelle");
                return select;
            }
            else if (categ == "package")
            {
                var formes = produitVendableService.getListFormeProduitsPackage(AboId, produitID);
                return new SelectList(formes, "FormeProduit_ID", "FormeProduit_Libelle");
            }
            else
            {
                var formes = produitVendableService.getListFormeProduitsPret(AboId, produitID);
                return new SelectList(formes, "FormeProduit_ID", "FormeProduit_Libelle");
            }
        }
        [HttpPost]
        public decimal getQteFome(string categ, int forme, int pdv)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            if (categ == "vendable")
                return pointVenteService.findformulaireStockByFormeID(AboId, pdv, forme);

            else if (categ == "package")
                return produitVendableService.getFormeDetails(forme, pdv).FormeDetails_Quantite;

            else
                return produitVendableService.getListProduitsStockesPdv(AboId, forme).FirstOrDefault().PdV_ProduitsPret_Quantite;
        }
        [HttpPost]
        public SelectList getUniteVente(string concat)
        {
            var split = concat.Split(",");
            var produitID = Convert.ToInt32(split.First());
            var categ = split[1];
            if (categ == "vendable")
            {
                var produit = produitVendableService.findFormulaireProduitVendable(produitID);
                return new SelectList(produitVendableService.findFormulaireUniteMesure((int)produit.ProduitVendable_UniteMesureId), "UniteMesure_Id", "UniteMesure_Libelle");
            }
            else if (categ == "package")
            {
                var package = produitVendableService.findFormulaireProduitPackage(produitID);
                return new SelectList(produitVendableService.findFormulaireUniteMesure((int)package.ProduitPackage_UniteVente), "UniteMesure_Id", "UniteMesure_Libelle");
            }
            else
            {
                var pret = produitVendableService.findFormulaireProduitPret(produitID);
                return new SelectList(produitVendableService.findFormulaireUniteMesure((int)pret.ProduitPretConsomer_UniteMesureAchatId), "UniteMesure_Id", "UniteMesure_Libelle");
            }

        }

        [Authorize(Roles = "Client")]
        public IActionResult EchangeProduits()
        {
            ViewData["pdv"] = new SelectList(pointVenteService.getListPointVenteActive(Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value)), "PointVente_Id", "PointVente_Nom");
            return View("~/Views/PointVente/EchangeProduits.cshtml");
        }
        [HttpPost]
        public async Task<bool> EchangeProduits(Echange_ProduitsModel echange)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            try
            {
                echange.EchangeProduits_AdminID = _userManager.GetUserAsync(HttpContext.User).Result.Id;
                echange.EchangeProduits_Statut = "En ordre de transfert";
                echange.EchangeProduits_AbonnementID = AboId;
                await pointVenteService.CreateEchange(echange);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        [Authorize(Roles = "Client")]
        public IActionResult ListeEchanges(string statut, string date, int? pdvF, int? pdvRec)
        {
            ViewData["pdv"] = new SelectList(pointVenteService.getListPointVenteActive(Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value)), "PointVente_Id", "PointVente_Nom");
            var aboid = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            if (date == "" || date == null || date == "null")
                date = "";
            if (statut == "" || statut == null || statut == "null")
                statut = "";
            return View("~/Views/PointVente/ListeEchangeProduits.cshtml",pointVenteService.getListEchanges(aboid,statut,date,pdvF,pdvRec));
        }
        public IActionResult ListeEchangesSortant(string statut, string date, int? pdvRec)
        {
            ViewData["pdv"] = new SelectList(pointVenteService.getListPointVenteActive(Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value)), "PointVente_Id", "PointVente_Nom");
            var aboid = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var pdv = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            if (date == "" || date == null || date == "null") 
                date = "";
            if (statut == "" || statut == null || statut == "null") 
                statut = "";
            return View("~/Views/PointVente/ListEchangeSortant.cshtml",pointVenteService.getListEchangesSortant(aboid,statut,date,pdv,pdvRec));
        }
        public IActionResult ListeEchangesEntrant(string statut, string date, int? pdvF)
        {
            ViewData["pdv"] = new SelectList(pointVenteService.getListPointVenteActive(Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value)), "PointVente_Id", "PointVente_Nom");
            var aboid = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var pdv = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            if (date == "" || date == null || date == "null")
                date = "";
            if (statut == "" || statut == null || statut == "null")
                statut = "";
            return View("~/Views/PointVente/ListeEchangeEntrant.cshtml", pointVenteService.getListEchangesEntrant(aboid,statut,date, pdvF, pdv));
        }
        public IActionResult ListeDetailsEchanges(int? Id)
        { 
            return View("~/Views/PointVente/ListeDetailsEchanges.cshtml", pointVenteService.getListEchangesDetails((int)Id));
        }
        [HttpPost]
        public async Task<bool?> AccepterOrdreEchange(int echangeId)
        {
            var pdv = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var validePar = _userManager.GetUserAsync(HttpContext.User).Result.Nom_Complet;
            try
            {
                return await pointVenteService.AccepterOrdreEchange(echangeId, validePar, pdv);
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost]
        public async Task<bool?> AccepterLivraisonEchange(int echangeId)
        {
            var pdv = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var validePar = _userManager.GetUserAsync(HttpContext.User).Result.Nom_Complet;
            try
            {
                return await pointVenteService.AccepterLivraisonEchange(echangeId, validePar, pdv);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
