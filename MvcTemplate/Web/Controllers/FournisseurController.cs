using Domain.Authentication;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using QRCoder;
using Repository.IRepositories;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Helpers;
using Web.Tools;
using Humanizer;
using Domain.Entities;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
//using NHibernate.Cache;

namespace Web.Controllers
{
    [Authorize(Roles = "Client, Gerant_des_achats, Gerant_de_stock")]

    public class FournisseurController : Controller
    {
        private readonly IFournisseurService fournisseurService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGestionMouvementService gestionMouvementService;
        private readonly IZoneStockageService zoneStockageService;
        //private ICacheProvider _cacheProvider;

        public FournisseurController(IFournisseurService fournisseurService, UserManager<ApplicationUser> userManager, IGestionMouvementService gestionMouvementService, IZoneStockageService zoneStockageService)
        {
            this.fournisseurService = fournisseurService;
            _userManager = userManager;
            this.gestionMouvementService = gestionMouvementService;
            this.zoneStockageService = zoneStockageService;
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
        public IActionResult ListeBonDeCommande(int? fournisseurID, int? date, string name, int pg = 1)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["fournisseur"] = new SelectList(gestionMouvementService.getListFournisseur(Id), "Founisseur_Id", "Founisseur_RaisonSocial");
            var point = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var query = fournisseurService.GetBonDeCommandes(Id, point, fournisseurID, name, date, null);
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;
            int recsCount = query.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var model = query.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View("~/Views/Fournisseur/BonDeCommandes/ListeBonDeCommande.cshtml", model);
        }
        public IActionResult ListeBonDeLivraison(int? fournisseurID, int? bonCommandeID, string date, int? etat, int pg = 1)
        {
            if (date == null || date == "null" || date == "")
                date = "";
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["fournisseur"] = new SelectList(gestionMouvementService.getListFournisseur(Id), "Founisseur_Id", "Founisseur_RaisonSocial");
            var query = fournisseurService.GetBonDeLivraisons(fournisseurID, bonCommandeID, Id, date, etat);
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;
            int recsCount = query.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var model = query.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View("~/Views/Fournisseur/BonDeLivraison/ListeBonDeLivraison.cshtml", model);
        }   
        public IActionResult ListeFactures(string nomFac, int? bcID, int? date, int pg = 1)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var query = fournisseurService.GetFactures(Id, bcID, nomFac, date);
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;
            int recsCount = query.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var model = query.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            ViewData["fournisseur"] = new SelectList(gestionMouvementService.getListFournisseur(Id), "Founisseur_Id", "Founisseur_RaisonSocial");
            ViewData["zone"] = new SelectList(zoneStockageService.getListZoneStockage(Id), "ZoneStockage_Id", "ZoneStockage_Designation");

            return View("~/Views/Fournisseur/Factures/ListeDesFactures.cshtml", model);
        }
        public IActionResult AjouterBC()
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["fournisseur"] = new SelectList(gestionMouvementService.getListFournisseur(Id), "Founisseur_Id", "Founisseur_RaisonSocial");
            return View("~/Views/Fournisseur/BonDeCommandes/Ajouter.cshtml");
        }
        [HttpPost]
        public async Task<bool> AjouterBC(BonDeCommande_Model bonDeCommande_Model)
        {
            bonDeCommande_Model.BonDeCommande_AbonnementID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            bonDeCommande_Model.BonDeCommande_CreePar = _userManager.GetUserId(HttpContext.User);
            //bonDeCommande_Model.BonDeCommande_PointStockID = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            bonDeCommande_Model.BonDeCommande_Statut = "Non réceptionné";
            var redirect = await fournisseurService.CreateBonDeCommande(bonDeCommande_Model);
            return redirect;
        }  
        [HttpPost]
        public IEnumerable<ArticleBC_Model> GetArticlesBCforBL(int Id)
        {
            var model = fournisseurService.GetArticlesBCForBL(Id);
            return model;
        }
        [HttpPost]
        public IEnumerable<BonDeLivraison_Model> GetBLForBC(int boncommande)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var model = fournisseurService.GetBonDeLivraisons(null,boncommande, Id, "",1);
            return model;
        }
        public IActionResult GetArticlesBC(int Id)
        {
            var model = fournisseurService.GetArticlesBC(Id);
            return View("~/Views/Fournisseur/BonDeCommandes/ListeArticles.cshtml",model);
        }
        public IActionResult GetArticlesBL(int Id)
        {
            var model = fournisseurService.GetArticlesBL(Id);
            return View("~/Views/Fournisseur/BonDeLivraison/ListeArticles.cshtml", model);
        }
        public IActionResult AjouterBL()
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["fournisseur"] = new SelectList(gestionMouvementService.getListFournisseur(Id), "Founisseur_Id", "Founisseur_RaisonSocial");
            return View("~/Views/Fournisseur/BonDeLivraison/Ajouter.cshtml");
        }
        [HttpPost]
        public SelectList getBcFournisseur(int fournisseurID, int? date, string etat)
        {
            if (string.IsNullOrEmpty(etat))
                etat = "Non réceptionné";
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var point = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var query = fournisseurService.GetBonDeCommandes(Id, point, fournisseurID, null, date, etat);
            return new SelectList(query, "BonDeCommande_ID", "BonDeCommande_Numero");
        }
        [HttpPost]
        public SelectList getBcFournisseurForArticle(int fournisseurID)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var point = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var query = fournisseurService.GetBonDeCommandes(Id, point, fournisseurID, null, null, "");
            return new SelectList(query, "BonDeCommande_ID", "BonDeCommande_Numero");
        }
        [HttpPost]
        public async Task<List<QrClassM>> AjouterBL(BonDeLivraison_Model bonDeLivraison_Model)
        {
            bonDeLivraison_Model.BonDeLivraison_AbonnementID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            bonDeLivraison_Model.BonDeLivraison_TotalHT = bonDeLivraison_Model.listeArticles.Sum(p => p.ArticleBL_PrixTotal);
            bonDeLivraison_Model.BonDeLivraison_TotalTTC = bonDeLivraison_Model.BonDeLivraison_TotalHT * (decimal)1.2;
            bonDeLivraison_Model.BonDeLivraison_TotalTVA = bonDeLivraison_Model.BonDeLivraison_TotalTTC - bonDeLivraison_Model.BonDeLivraison_TotalHT;
            
            var redirect = await fournisseurService.CreateBonDeLivraison(bonDeLivraison_Model);
            var listQr = new List<QrClassM>();
            foreach (var item in redirect)
            { 
                var qRCode = new QRCodeModel()
                {
                    // QRCodeText = redirect.ToString(),
                    DESIGNATION = item.ArticleBL_Designation.ToUpper(),
                    LOT_INTERN = item.ArticleBL_LotTemp,
                    LOT_FOURNISSEUR = item.ArticleBL_LotFournisseur.ToUpper(),
                    DATE_RECEP = item.bonDeLivraison.BonDeLivraison_DateLivraison.ToString(),
                    //DATE_P = item.ArticleBL_DateProduction.Value.ToShortDateString(),
                    DLC = item.ArticleBL_DateLimiteConso.Value.ToShortDateString(),
                    TEMPERATURE = item.ArticleBL_Teemperature +" "+ "°C"
                };
               

                var serializer = new JsonSerializer();
                var stringWriter = new StringWriter();
                using (var writer = new JsonTextWriter(stringWriter))
                {
                    writer.QuoteName = false;
                    writer.Indentation = 6;
                    writer.Formatting = Formatting.Indented;
                    serializer.Serialize(writer, qRCode);
                }
                var json = stringWriter.ToString();
                //string output = JsonConvert.SerializeObject(qRCode, Formatting.Indented);
                QRCodeGenerator QrGenerator = new QRCodeGenerator();
                //json = json.Replace("/{|}/g", " ");
                QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(json, QRCodeGenerator.ECCLevel.Q);
                QRCode QrCode = new QRCode(QrCodeInfo);
                Bitmap QrBitmap = QrCode.GetGraphic(60);
                byte[] BitmapArray = QrBitmap.BitmapToByteArray();
                string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
                var qRCodeM = new QrClassM()
                {
                    qRCodeIMG = QrUri,
                    qRCodeTITLE = item.ArticleBL_Designation
                };
                listQr.Add(qRCodeM);
            }

            return listQr;
        }
        public IActionResult AjouterFA()
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["fournisseur"] = new SelectList(gestionMouvementService.getListFournisseur(Id), "Founisseur_Id", "Founisseur_RaisonSocial");
            return View("~/Views/Fournisseur/Factures/Ajouter.cshtml");
        }
        [HttpPost]
        public async Task<bool> AjouterFA(FactureModel factureModel, List<BonDeLivraison_Model> listeBL)
        {
            factureModel.Facture_AbonnementID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

            factureModel.Facture_MontantTVA = listeBL.Sum(p=>p.BonDeLivraison_TotalTVA);
            factureModel.Facture_TotalTTC = listeBL.Sum(p=>p.BonDeLivraison_TotalTTC);
            factureModel.Facture_TotalHT = listeBL.Sum(p=>p.BonDeLivraison_TotalHT);
            //factureModel.Facture_PointStockID = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var redirect = await fournisseurService.CreateFacture(factureModel, listeBL);
            return redirect;
        }
        public Task<ActionResult> GeneratePDfBC(int? id)
        {
            try
            {
                var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

                var bc = fournisseurService.FindFormulaireBonDeCommande(Id, (int)id);
                //var tableau = this.declarationService.GetDeclaration((int)id);
                /*var model = new ViewModelValidation
                {
                    cartographie = carto,
                    tableauDeclaration = tableau
                };*/
                var check = bc.BonDeCommande_TotalTTC.ToString("G29").Split(",");
                if(check.Count()>1)
                {
                    var dh = check[0];
                    var cent = check[1];
                    bc.BonDeCommande_TTCWords = NumberToWordsExtension.ToWords(Convert.ToInt32(dh), new CultureInfo("fr")).Titleize() + " " + "VIRGULE" + " " + NumberToWordsExtension.ToWords(Convert.ToInt32(cent), new CultureInfo("fr")).Titleize() + " " + "Dirhams";
                }
                else
                {
                    var dh = check[0];
                    bc.BonDeCommande_TTCWords = NumberToWordsExtension.ToWords(Convert.ToInt32(dh), new CultureInfo("fr")).Titleize() + " " + "Dirhams";
                }

                Controller controller = this;


                Task<ActionResult> lFileResult = ConvertHTmlToPdf.ConvertCurrentPageToPdf(controller, bc, "Pdf","Bon_de_Commande_"+bc.BonDeCommande_Numero);
                return lFileResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Task<ActionResult> GeneratePDfBL(int? id)
        {
            try
            {
                var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

                var bc = fournisseurService.FindFormulaireBonDeLivraison(Id, (int)id);
                //var tableau = this.declarationService.GetDeclaration((int)id);
                /*var model = new ViewModelValidation
                {
                    cartographie = carto,
                    tableauDeclaration = tableau
                };*/
                var check = bc.BonDeLivraison_TotalTTC.ToString("G29").Split(",");
                if (check.Count() > 1)
                {
                    var dh = check[0];
                    var cent = check[1];
                    bc.BonDeLivraison_TTCWords = NumberToWordsExtension.ToWords(Convert.ToInt32(dh), new CultureInfo("fr")).Titleize() + " " + "VIRGULE" + " " + NumberToWordsExtension.ToWords(Convert.ToInt32(cent), new CultureInfo("fr")).Titleize() + " " + "Dirhams";
                }
                else
                {
                    var dh = check[0];
                    bc.BonDeLivraison_TTCWords = NumberToWordsExtension.ToWords(Convert.ToInt32(dh), new CultureInfo("fr")).Titleize() + " " + "Dirhams";
                }

                Controller controller = this;


                Task<ActionResult> lFileResult = ConvertHTmlToPdf.ConvertCurrentPageToPdf(controller, bc, "Pdf_Livraison", "Bon_de_Livraison_" + bc.BonDeLivraison_Designation);
                return lFileResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IActionResult getAllProds(int pg = 2)
        {
            var query = fournisseurService.getAllProds(pg);
            var pager = new Pager(query.count, pg, 10);
            ViewBag.Pager = pager;
            return View(query.objList);
        }
        public IActionResult MatieresEnStock(int? matireID, string lotIntern, int pg = 1)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["lieu"] = new SelectList(zoneStockageService.getListLieuStockage(Id, 1), "LieuStockag_Id", "LieuStockag_Nom");
            var query = fournisseurService.GetMatireStockAchat(Id, matireID, lotIntern);
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;
            int recsCount = query.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var model = query.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            int i = 0;
            foreach(var item in model)
            {
                ViewData["unite"+ i] = new SelectList(item.unite_Utilisation, "UniteMesure_Id", "UniteMesure_Libelle");
                i++;
            }
            return View("~/Views/Fournisseur/AlimentationStock/MatieresEnStock.cshtml", model);
        }
        [HttpPost]
        public async Task<bool> AjouterOrdreT(TransfertMatiere_Model TransfertMat_Model)
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            TransfertMat_Model.TransfertMat_Numero = DateTime.UtcNow.Year.ToString() + "/" + DateTime.UtcNow.Month.ToString() + DateTime.UtcNow.Day.ToString() + "ORDRE" + _rdm.Next(_min, _max);
            TransfertMat_Model.TransfertMat_AbonnementID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            TransfertMat_Model.TransfertMat_CreePar = HttpContext.User.Identity.Name;
            //bonDeCommande_Model.BonDeCommande_PointStockID = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var redirect = await fournisseurService.CreateOrdreTransfer(TransfertMat_Model);
            return redirect;
        }
        public IActionResult ListeDesTransfer(string statut, string date, int? point, int pg = 1)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["lieu"] = new SelectList(zoneStockageService.getListLieuStockage(Id, 1), "LieuStockag_Id", "LieuStockag_Nom");
            if (User.IsInRole("Gerant_de_stock"))
                point = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var query = fournisseurService.GetListeOrdreTransfert(Id, statut, point, date);
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;
            int recsCount = query.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var model = query.Skip(recSkip).Take(pager.PageSize).ToList();
            ViewBag.Pager = pager;
          
            return View("~/Views/Fournisseur/AlimentationStock/ListeDesTransfer.cshtml", model);
        }
        public IActionResult ListeDesMatieres(int? id, string matiereID, string lot, string date, string statut, int pg = 1)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            int? pd = null;
            if(User.IsInRole("Gerant_de_stock"))
                pd = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var query = fournisseurService.GetListeMatiereParOrdre(id, pd, matiereID, lot, statut, date);
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;
            int recsCount = query.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var model = query.Skip(recSkip).Take(pager.PageSize).ToList();
            ViewBag.Pager = pager;
            ViewData["zone"] = new SelectList(zoneStockageService.getListZoneStockage(Id), "ZoneStockage_Id", "ZoneStockage_Designation");

            return View("~/Views/Fournisseur/AlimentationStock/ListeDesMatieres.cshtml", model);
        }

        [HttpPost]
        public async Task<bool?> ReceptionMatiereAchats(ReceptionAchatModel receptionAchatModel)
        {
            receptionAchatModel.userID = _userManager.GetUserName(HttpContext.User);
            receptionAchatModel.StockID = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var redirect = await fournisseurService.ReceptionMatiereAchats(receptionAchatModel);
            return redirect;
        }
        
        [HttpPost]
        public async Task<bool?> PaiementFacture(PayementFacture_Model payementFacture_Model)
        {
            payementFacture_Model.PayementFacture_CreePar = _userManager.GetUserName(HttpContext.User);
            payementFacture_Model.PayementFacture_AbonnementID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var redirect = await fournisseurService.CreatePayementFacture(payementFacture_Model);
            return redirect;
        }
        [HttpPost]
        public SelectList getListSections(int matiereID)
        {
            var res = fournisseurService.getListSections(matiereID);
            foreach(var item in res)
            {
                item.Section_Designation = item.Zone_Stockage.ZoneStockage_Designation + " / " + item.Section_Designation; 
            }
            return new SelectList(res, "Section_Id", "Section_Designation");
        }
        public IActionResult getlistFactureDetails(int id)
        {         
            var aboID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var model = fournisseurService.getlistFactureDetails(id, aboID);
            return View("~/Views/Fournisseur/Factures/DetailsFacture.cshtml", model);
        }

    }

    public static class BitmapExtension
    {
        public static byte[] BitmapToByteArray(this Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
  
}
