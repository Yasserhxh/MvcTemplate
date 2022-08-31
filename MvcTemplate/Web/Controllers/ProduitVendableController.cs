using Domain.Authentication;
using Domain.Models;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Rewrite.Internal;
using Newtonsoft.Json;
using Repository.IRepositories;
using Repository.UnitOfWork;
using Service.IServices;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Web.Helpers;
using Web.Tools;

namespace Web.Controllers
{

    public class ProduitVendableController : Controller
    {
        private readonly IProduitVendableService produitVendableService;
        private readonly IPointVenteService pointVenteService;
        private readonly IZoneStockageService zoneStockageService;
        private readonly IGestionMouvementService gestionMouvementService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthentificationRepository authentificationRepository;
        private readonly IProduitFicheTechniqueService produitFicheTechniqueService;
        private readonly IMatierePremiereService matierePremiereService;
        private readonly IHostingEnvironment _environment;
        private readonly IFamilleProduitService familleProduitService;
        private readonly IVenteProduitsService venteProduitsService;
        private readonly IUnitOfWork _unitOfWork;
        private static readonly string key = "b1529618774e40c09cf10149dcaca84c";
        private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com";
        private static readonly string location = "westeurope";

        public ProduitVendableController(IProduitVendableService produitVendableService, IAuthentificationRepository authentificationRepository, UserManager<ApplicationUser> userManager,
            IZoneStockageService zoneStockageService, IProduitFicheTechniqueService produitFicheTechniqueService, IMatierePremiereService matierePremiereService,
            IGestionMouvementService gestionMouvementService, IHostingEnvironment IHostingEnvironment, IPointVenteService pointVenteService, IFamilleProduitService familleProduitService,
            IVenteProduitsService venteProduitsService, IUnitOfWork unitOfWork = null)
        {
            this.gestionMouvementService = gestionMouvementService;
            this.authentificationRepository = authentificationRepository;
            this.produitVendableService = produitVendableService;
            this.zoneStockageService = zoneStockageService;
            this.produitFicheTechniqueService = produitFicheTechniqueService;
            this.matierePremiereService = matierePremiereService;
            this.pointVenteService = pointVenteService;
            _userManager = userManager;
            _environment = IHostingEnvironment;
            this.familleProduitService = familleProduitService;
            this.venteProduitsService = venteProduitsService;
            _unitOfWork = unitOfWork;
        }


        [Authorize(Roles = "Client")]
        public IActionResult Ajouter()
        {
            var aboid = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["ProduitVendable_FamilleProduitId"] = new SelectList(produitVendableService.getListFamilleProduit(aboid), "FamilleProduit_Id", "FamilleProduit_Libelle");
            ViewData["ProduitVendable_FormStockageId"] = new SelectList(produitVendableService.getListFormeSotckage(aboid), "FormStockage_Id", "FormStockage_Libelle");
            ViewData["ProduitVendable_UniteMesureId"] = new SelectList(produitVendableService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");

            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Ajouter([FromForm] ProduitVendableModel produitModel)
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
                        produitModel.ProduitVendable_GrandePhoto = PathDB;

                    }
                }


            }
            produitModel.ProduitVendable_AbonnementId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
           /* string route = "/translate?api-version=3.0&from=fr&to=en&to=ar";
            string textToTranslate = produitModel.Composants.ElementAt(0).ProduitComposant_ComposantName;
            object[] body = new object[] { new { Text = textToTranslate } };
            var requestBody = JsonConvert.SerializeObject(body);
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);
                request.Headers.Add("Ocp-Apim-Subscription-Region", location);
                string pattern = "text: (.*), to: en }";
                string pattern2 = ",{\"text\": (.*),\"to\":\"ar\"} ";
                //string pattern3 = "[{\"translations\":[{\"text\":\"Energy\",\"to\":\"en\"},{\"text\":\"طاقة\",\"to\":\"ar\"}]}]";

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                // Read response as a string.
                string result =  response.Content.ReadAsStringAsync().Result;
                List<Object> businessunits = JsonConvert.DeserializeObject<List<Object>>(result);
                Match match = Regex.Match(businessunits.FirstOrDefault().ToString(), pattern);
                Console.WriteLine(result);
            }*/
            foreach (var item in produitModel.Composants)
            {
                item.ProduitComposant_AbonnementID = (int)produitModel.ProduitVendable_AbonnementId;
               // item.ProduitComposant_ComposantNameAR = ;
                item.ProduitComposant_isActive = 1;
            }
            var redirect = await produitVendableService.CreateProduitVendable(produitModel);
            if (redirect)
            {
                TempData["Creation"] = "OK";
                return Redirect("/ProduitVendable/ListeProduitVendable");
            }
            else
            {
                var aboid = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                ViewData["ProduitVendable_FamilleProduitId"] = new SelectList(produitVendableService.getListFamilleProduit(aboid), "FamilleProduit_Id", "FamilleProduit_Libelle");
                ViewData["ProduitVendable_FormStockageId"] = new SelectList(produitVendableService.getListFormeSotckage(aboid), "FormeStockage_Id", "FormeStockage_Libelle");
                ViewData["ProduitVendable_UniteMesureId"] = new SelectList(produitVendableService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");

                return View();
            }
        }
        [HttpPost]
        public SelectList familleList(int familleParent)
        {
            var aboid = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

            SelectList sousfamilleList = new SelectList(familleProduitService.getListSousFamilles(familleParent, aboid), "SousFamille_ID", "SousFamille_Libelle");
            return sousfamilleList;
        }
        [Authorize(Roles = "Client")]

        public IActionResult ListeProduitVendable(int? categ, int? sousCateg,string name, int pg=1)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["ProduitVendable_FamilleProduitId"] = new SelectList(produitVendableService.getListFamilleProduit(Id), "FamilleProduit_Id", "FamilleProduit_Libelle");
            var query = produitVendableService.getListProduitVendable(Id, categ, sousCateg, name);
            if (!String.IsNullOrEmpty(name))
                query = query.Where(p => p.ProduitVendable_Designation.Contains(name, StringComparison.OrdinalIgnoreCase));
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

        [Authorize(Roles = "Client")]

        public IActionResult Modification(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var donnee = produitVendableService.findFormulaireProduitVendable((int)id);
            if (donnee == null)
            {
                return NotFound();
            }
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var formes = produitVendableService.getListFormeSotckage(Id);
            ViewData["Form"] = new SelectList(formes, "FormeStockage_Id", "FormeStockage_Libelle");
            ViewData["ProduitVendable_FamilleProduitId"] = new SelectList(produitVendableService.getListFamilleProduit(Id), "FamilleProduit_Id", "FamilleProduit_Libelle");
            //ViewData["sous"] = new SelectList(familleProduitService.getListSousFamilles(null,Id), "SousFamille_ID", "SousFamille_Libelle");
            ViewData["ProduitVendable_FormStockageId"] = new SelectList(produitVendableService.getListFormeSotckage(Id), "FormStockage_Id", "FormStockage_Libelle");
            ViewData["ProduitVendable_UniteMesureId"] = new SelectList(produitVendableService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
            return View(donnee);
        }
        [HttpPost]
        public async Task<IActionResult> Modification(int id, ProduitVendableModel produitVendableModel)
        {
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
                        produitVendableModel.ProduitVendable_GrandePhoto = PathDB;

                    }
                }

            }
            var redirect = await produitVendableService.updateFormulaireProduitVendable(id, produitVendableModel);
            if (redirect)
            {
                TempData["Modification"] = "OK";
                return Redirect("/ProduitVendable/ListeProduitVendable");
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        public async Task<bool> DeleteproduitVendable(int id)
        {
            var result = await produitVendableService.deleteProduitVendable(id);
            return result;
        }
        [HttpPost]
        public async Task<bool> DeleteproduitConso(int id)
        {
            var result = await produitVendableService.deleteProduitConso(id);
            return result;
        }
        [HttpPost]
        public async Task<bool> DeleteProduitsLink(int id, int code)
        {
            var result = await produitVendableService.deleteProduitsLink(id, code);
            return result;
        }
        [Authorize(Roles = "Client")]

        public IActionResult ListeUniteMesure()
        {
            //var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            return View(produitVendableService.getListUniteMesure());
        }
        [Authorize(Roles = "Chef_Patissier, Gerant_de_stock")]

        public IActionResult ProduitEnStock()
        {
            var atelierId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            return View("~/Views/ProduitVendable/Stockage/ProduitEnStock.cshtml", produitVendableService.getListProduitVendableStockerByAtelier(atelierId));
        }
        [HttpPost]
        public PdV_ProduitsPretModel getListFormePretEnStock(int id)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            return produitVendableService.getListProduitsStockesPdv(Id, id).FirstOrDefault();
        }


        [Authorize(Roles = "Chef_Patissier, Gerant_de_stock")]

        public IActionResult StockerProduit(int? id)
        {

            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["ZoneStockage_LieuStockageId"] = new SelectList(zoneStockageService.getListLieuStockage(aboId, 1), "LieuStockag_Id", "LieuStockag_Nom");

            return View("~/Views/ProduitVendable/Stockage/StockerProduit.cshtml");


        }
        [HttpPost]
        public async Task<IActionResult> StockerProduit(int id, [FromForm] ProduitConsomableStokageModel produitModel)
        {
            // GET CURRENT USER_ID and query it's abo_ID 
            produitModel.ProduitConsomableStokage_AbonnementId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            produitModel.ProduitConsomableStokage_LieuStockID = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var redirect = await produitVendableService.StockerProduitVendable(id, produitModel);
            if (redirect)
            {
                TempData["Creation"] = "OK";
                return Redirect("/ProduitVendable/StockerProduit");
            }
            else
            {
                var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                ViewData["ZoneStockage_LieuStockageId"] = new SelectList(zoneStockageService.getListLieuStockage(aboId, 1), "LieuStockag_Id", "LieuStockag_Nom");

                return Redirect("/ProduitVendable/StockerProduit");
            }
        }


        [HttpPost]
        public bool EstEnStock(int id)
        {
            var result = produitVendableService.ProduitStocker(id);
            return result;
        }
        [HttpPost]
        public string GetUnitebyFrome(int Id)
        {
            var result = produitVendableService.GetUnitebyFrome(Id);
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
            var result = await produitVendableService.deleteProduitVendableStockes(id);
            return result;
        }
        [Authorize(Roles = "Client")]

        public IActionResult Consulter(int Id)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            return View(this.produitFicheTechniqueService.findFormulaireFiche(Id, AboId).OrderByDescending(p => p.FicheTechniqueBridge_InUse));
        }

        public IActionResult PrixForme(int Id)
        {
            return View(produitVendableService.getListPrixFormes(Id));
        }
        [Authorize(Roles = "Chef_Patissier,Client")]

        public IActionResult Planification()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            ViewData["PlanificationProduction_ProduitVendableId"] = new SelectList(produitVendableService.getListProduitVendableAvecPlanif(adresse,id), "ProduitVendable_Id", "ProduitVendable_Designation");
            ViewData["ZoneStockage_LieuStockageId"] = new SelectList(zoneStockageService.getListLieuStockage(id, 1), "LieuStockag_Id", "LieuStockag_Nom");
            return View("~/Views/ProduitVendable/PlanificationProduction/Planification.cshtml");
        }



        [HttpPost]
        public async Task<int?> Planifier(PlanificationJourneeModel plan)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            plan.PlanificationJournee_AbonnementID = Id;
            plan.PlanificationJournee_AtelierID = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            plan.PlanificationJournee_UtilisateurId = _userManager.GetUserId(HttpContext.User);
            var result = await produitVendableService.Planifier(plan);
            return result;
        } 
        [HttpPost]
        public async Task<List<matStockViewModel>> getProdsBasePlans(int planID)
        {
            var result = await   produitVendableService.SetProdsDeBase(planID);
            return result;
        }

        public async Task<ActionResult> ListeDesPlans(string etat, int? point,int? point2, string date, int pg=1)
        {
            if (date == null || date == "null")
                date = "";
            if (etat == null || etat == "null")
                etat = "";
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;
            
            if (User.IsInRole("Chef_Patissier"))
            {
                //var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                var plans = produitVendableService.getListPansAtelier(Id, adresse, etat, point, date);
                var demandes = produitVendableService.GetDemandes(Id, date, point, adresse);
                int recsCount = plans.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var model = plans.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                var tuple = new Tuple<IEnumerable<PlanificationJourneeModel>, IEnumerable<DemandeModel>>(model, demandes);
                ViewData["ZoneStockage_LieuStockageId"] = new SelectList(zoneStockageService.getListLieuStockage(Id, null), "LieuStockag_Id", "LieuStockag_Nom");
                return View("~/Views/ProduitVendable/PlanificationProduction/ListeDesPlans.cshtml", tuple);
            }
            else if (User.IsInRole("Gerant_de_stock"))
            {
                //var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                ViewData["production"] = new SelectList(produitVendableService.getListAteliers(Id), "Atelier_ID", "Atelier_Nom");
                
                var query = await produitVendableService.getListPansStock(Id, adresse, etat, point, date);
                int recsCount = query.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var model = query.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                return View("~/Views/ProduitVendable/PlanificationProduction/ListeDesPlansGerant.cshtml", model);
            }
            else if(User.IsInRole("Client"))
            {
                var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                ViewData["production"] = new SelectList(produitVendableService.getListAteliers(Id), "Atelier_ID", "Atelier_Nom");
                ViewData["ZoneStockage_LieuStockageId"] = new SelectList(zoneStockageService.getListLieuStockage(Id, null), "LieuStockag_Id", "LieuStockag_Nom");
               
                var query = produitVendableService.getListPansAtelier(Id, point, etat, point2, date);
                int recsCount = query.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var model = query.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                return View("~/Views/ProduitVendable/PlanificationProduction/ListeDesPlansAdmin.cshtml", model);

            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult DetailsPlan(int Id)
        {
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            return View("~/Views/ProduitVendable/PlanificationProduction/DetailsPlan.cshtml", produitVendableService.getDetailPlan(aboId, Id));
        }



        [Authorize(Roles = "Chef_Patissier")]

        public IActionResult ModificationPlan(int? id)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            ViewData["PlanificationProduction_ProduitVendableId"] = new SelectList(produitVendableService.getListProduitVendableAvecPlanif(adresse, AboId), "ProduitVendable_Id", "ProduitVendable_Designation");
            if (id == null)
            {
                return NotFound();
            }
            var donnee = produitVendableService.findFormulairePlans((int)id);
            if (donnee == null)
            {
                return NotFound();
            }

            return View("~/Views/ProduitVendable/PlanificationProduction/ModificationPlan.cshtml", donnee);
        }
        [Authorize(Roles = "Chef_Patissier")]
        /*[HttpPost]
		public async Task<IActionResult> ModificationPlan(int id, PlanificationdeProductionModel produitVendableModel)
		{
			var redirect = await produitVendableService.updateFormulairePans(id, produitVendableModel);
			if (redirect)
			{
				TempData["Modification"] = "OK";
				return Redirect("/ProduitVendable/ListeDesPlans");
			}
			else
			{
				var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
				ViewData["PlanificationProduction_ProduitVendableId"] = new SelectList(produitVendableService.getListProduitVendable(AboId), "ProduitVendable_Id", "ProduitVendable_Designation");
				return View("~/Views/ProduitVendable/PlanificationProduction/ModificationPlan.cshtml");
			}
		}*/
        [HttpPost]
        public async Task<bool> DeletePlanification(int id)
        {
            var result = await produitVendableService.deletePlan(id);
            return result;
        }

        [HttpPost]
        public FicheTechniqueBridgeModel GetFicheTech(int Id)
        {
            return produitVendableService.GetFicheTech(Id);
        }
        [HttpPost]
        public IEnumerable<BonDetailsModel> getBonDeSortie(int Id)
        {
            return produitVendableService.getBonDetails(Id);
        }

        public IActionResult BonDeSortie(int Id, int? plan, int? state)
        {
           
            var res = produitVendableService.getBonDetails(Id);
           // var test =
            double test = 0.0001;
            return View("~/Views/ProduitVendable/PlanificationProduction/BonDeSortie.cshtml", res .Where(p =>Convert.ToDouble( p.BonDeSortie_QuantiteDemandee.ToString("0.0000")) > test));
        }
        [Authorize(Roles = "Chef_Patissier,Client")]

        public IActionResult Demande()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            ViewData["lieuID"] = new SelectList(zoneStockageService.getListLieuStockage(id, 1), "LieuStockag_Id", "LieuStockag_Nom");
            ViewData["Demande_PlanificationID"] = new SelectList(produitVendableService.getListPansDemandes(id, adresse), "PlanificationJournee_ID", "PlanificationJournee_Date");
            ViewData["MatierePremiere"] = new SelectList(matierePremiereService.getListMatierePremiere(id, null, null), "MatierePremiere_Id", "MatierePremiere_Libelle");
            ViewData["ProduitVendable_UniteMesureId"] = new SelectList(produitVendableService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");

            return View("~/Views/ProduitVendable/Demandes/Demande.cshtml");
        }
        [HttpPost]
        public SelectList fillMatiereDemande(int planJourneeID)
        {
            var matieres = produitVendableService.fillMatiereDemande(planJourneeID);
            return new SelectList(matieres, "MatierePremiere_Id", "MatierePremiere_Libelle"); 
        }
            
        [HttpPost]
        public async Task<bool> Demander(DemandeModel demandeModel)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            demandeModel.Demande_AbonnementID = Id;
            demandeModel.Demande_UtilisateurID = _userManager.GetUserId(HttpContext.User);
            demandeModel.Demande_AtelierID = adresse;
            var result = await produitVendableService.CreerDemande(demandeModel);
            return result;
        }
        [HttpPost]
        public async Task<MatStockFlagModel> CloturerDemande(int demandeId)
        {
            var result = await produitVendableService.CloturerDemande(demandeId);
            return result;
        }
        public IActionResult ListeDeDemandes(int? point,string date)
        {
            if (date == null || date == "null")
                date = "";
            
            if (User.IsInRole("Chef_Patissier"))
            {
                var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                ViewData["ZoneStockage_LieuStockageId"] = new SelectList(zoneStockageService.getListLieuStockage(Id, null), "LieuStockag_Id", "LieuStockag_Nom");
                return View("~/Views/ProduitVendable/Demandes/ListeDeDemandes.cshtml", produitVendableService.GetDemandes(Id, date, point, adresse));
            }
            if (User.IsInRole("Gerant_de_stock"))
            {
                var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                ViewData["production"] = new SelectList(produitVendableService.getListAteliers(Id), "Atelier_ID", "Atelier_Nom");
                return View("~/Views/ProduitVendable/Demandes/ListeDeDemandesGerantStock.cshtml", produitVendableService.GetDemandes(Id, date, adresse, point));
            }
            return NotFound();

        }
        public Task<bool> AccepterDemande(int demandeId)
        {
            if (User.IsInRole("Chef_Patissier"))
                return produitVendableService.ValiderLivraison(demandeId);
            var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var valideId = _userManager.GetUserAsync(HttpContext.User).Result.Nom_Complet;
            return produitVendableService.AccepterDemande(demandeId, adresse, valideId);

        }
        public IActionResult Mouvements(int? matiere, string date, int pg=1)
        {
            if (date == null || date == "null")
                date = "";
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            if (User.IsInRole("Client"))
            {
                ViewData["MatierePremiere"] = new SelectList(matierePremiereService.getListMatierePremiere(Id, null, null), "MatierePremiere_Id", "MatierePremiere_Libelle");
                var query = produitVendableService.GetMouvementStocksActiveOnly(Id, matiere, date);
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
            if (User.IsInRole("Gerant_de_stock"))
            {
                var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                ViewData["MouvementStock_MatierePremiereStokageId"] = new SelectList(gestionMouvementService.getListMatiereStockageAll(Id, (int)adresse), "MatierePremiereStokage_Id", "Matiere_Premiere.MatierePremiere_Libelle");
                var query = produitVendableService.GetMouvementStocksBystockActiveOnly(Id, adresse, matiere, date);
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
            return NotFound();
        }
        public Task<MatStockFlagModel> AccepterPlanification(int planificationId, List<BonViewModel> ListBons)
        {
            if (User.IsInRole("Chef_Patissier"))
            {
                return produitVendableService.ValiderPlanification(planificationId);
            }
            var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var validePar = _userManager.GetUserAsync(HttpContext.User).Result.Nom_Complet;
            return produitVendableService.AccepterPlanification(planificationId,adresse, ListBons,validePar);

        }
        [Authorize(Roles = "Gerant_de_stock")]

        public IActionResult Receptionfournisseur()
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["MouvementStock_FournisseurId"] = new SelectList(gestionMouvementService.getListFournisseur(AboId), "Founisseur_Id", "Founisseur_RaisonSocial");
            return View("~/Views/ProduitVendable/Alimentation/Receptionfournisseur.cshtml");
        }
        [Authorize(Roles = "Chef_Patissier")]

        public IActionResult ReceptionMatiereAtelier()
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["lieuID"] = new SelectList(zoneStockageService.getListLieuStockage(AboId, 1), "LieuStockag_Id", "LieuStockag_Nom");
            return View("~/Views/ProduitVendable/Alimentation/ReceptionAtelier.cshtml");
        }
        [HttpPost]

        public async Task<bool> Receptionfournisseur(List<MouvementStockModel> mouvementStockModel)
        {
            var redirect = false;
            foreach (var m in mouvementStockModel)
            {
                // GET CURRENT USER_ID and query it's abo_ID 
                m.MouvementStock_AbonnementId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                redirect = await gestionMouvementService.AlimentationStock(m);
            }
            return redirect;
        }
        [HttpPost]

        public async Task<bool> ReceptionMatiere(List<Reception_StockModel> reception_StockModels)
        {
            var redirect = false;
            foreach (var m in reception_StockModels)
            {
                // GET CURRENT USER_ID and query it's abo_ID 
                m.ReceptionStock_AbonnementID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                m.ReceptionStock_AtelierID = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                redirect = await gestionMouvementService.ReceptionStock(m);
            }
            return redirect;
        }
        [HttpPost]
        public SelectList matiereListe(int fournisseurId)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            SelectList secteurSelect = new SelectList(gestionMouvementService.getListMatiereStockage(fournisseurId, AboId, adresse), "MatierePremiereStokage_Id", "Matiere_Premiere.MatierePremiere_Libelle");
            return secteurSelect;
        }   
        [HttpPost]
        public SelectList matiereListeBC(int fournisseurId)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            SelectList secteurSelect = new SelectList(gestionMouvementService.getListMatierePremieresBC(fournisseurId, AboId), "MatierePremiere_Id", "MatierePremiere_Libelle");
            return secteurSelect;
        } 
        [HttpPost]
        public SelectList matiereListePointStock(int point)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
           // var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            SelectList secteurSelect = new SelectList(matierePremiereService.getListMatiereStockerAll(AboId, point, null,null), "MatierePremiereStokage_Id", "Matiere_Premiere.MatierePremiere_Libelle");
            return secteurSelect;
        }
        [HttpPost]
        public SelectList FormeListe(int produitId)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            SelectList secteurSelect = new SelectList(produitVendableService.getListFormeProduits(AboId, produitId), "FormeProduit_ID", "FormeProduit_Libelle");
            return secteurSelect;
        }
        [HttpPost]
        public SelectList produitListe(int Id)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var list = produitVendableService.getDetailPlanProduitList(AboId, Id).GroupBy(p => new { key = p.PlanificationProduction_ProduitVendableId, item = p.Produit_Vendable.ProduitVendable_Designation });
            SelectList secteurSelect = new SelectList(list, "Key.key", "Key.item");
            return secteurSelect;
        }
        [HttpPost]
        public SelectList FormePlanifListe(int Id, int produitId)
        {
            var list = produitVendableService.getDetailPlanFormeList(Id, produitId);
            SelectList secteurSelect = new SelectList(list, "PlanificationProduction_Id", "Forme_Produit.FormeProduit_Libelle");
            return secteurSelect;
        }
        [HttpPost]
        public decimal QtePrevue(int Id, int produitId)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var qte = produitVendableService.getDetailPlanByPlan(AboId, Id, produitId).Select(p => p.PlanificationProduction_QuantitePrevue).FirstOrDefault();
            return qte;
        }
        [HttpPost]
        public SelectList getUnite(int MatierePremiereStokage_Id)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var unite = matierePremiereService.getListMatiereStocker(Id, MatierePremiereStokage_Id).Select(m => m.Matiere_Premiere.MatierePremiere_AchatUniteMesureId).FirstOrDefault();
            SelectList uniteselect = new SelectList(produitVendableService.findFormulaireUniteMesure(unite), "UniteMesure_Id", "UniteMesure_Libelle");
            return uniteselect;
        }
        [HttpPost]
        public SelectList getUniteMatPrem(int matiere)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var unite = matierePremiereService.findFormulaireMatiereP(Id, matiere).MatierePremiere_AchatUniteMesureId;
            SelectList uniteselect = new SelectList(produitVendableService.findFormulaireUniteMesure(unite), "UniteMesure_Id", "UniteMesure_Libelle");
            return uniteselect;
        }
        [HttpPost]
        public async Task<MatStockFlagModel> ModifierQteProduite(int id, List<PlanificationdeProductionModel> plans)
        {
            var res = await produitVendableService.updateFormulaireQteProduite(id, plans);
          //  var group = res.Matieres.GroupBy(p => new { key = p.matID, lib = p.matiereLibelle, qteS = res.Matieres.Sum(x => x.qteEnStock), uniteS = p.uniteStock, qteLiv = res.Matieres.Sum(x => x.qteLivrer), uniteLiv = p.uniteLivrer });
          //  var mats = new List<matStockViewModel>();
           
            return res;

        }

        [Authorize(Roles = "Gerant_de_stock")]

        public IActionResult AlimentationStock()
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var userLieu = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            ViewData["MouvementStock_MatierePremiereStokageId"] = new SelectList(gestionMouvementService.getListMatiereStockageAll(AboId, (int)userLieu), "MatierePremiereStokage_Id", "Matiere_Premiere.MatierePremiere_Libelle");
            ViewData["Lieu"] = new SelectList(zoneStockageService.getListLieuStockageActive(AboId, (int)userLieu), "LieuStockag_Id", "LieuStockag_Nom");
            ViewData["MouvementStock_UniteMesureId"] = new SelectList(produitVendableService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
            return View("~/Views/ProduitVendable/Alimentation/AlimentationStock.cshtml");
        }
        [HttpPost]

        public async Task<IActionResult> AlimentationStock([FromForm] MouvementStockModel mouvementStockModel)
        {
            // GET CURRENT USER_ID and query it's abo_ID 
            var userLieu = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            mouvementStockModel.MouvementStock_AbonnementId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            mouvementStockModel.MouvementStock_DateCreation = DateTime.Now;
            mouvementStockModel.MouvementStock_DateSaisie = DateTime.Now;
            mouvementStockModel.MouvementStock_DateReception = DateTime.Now;
            var redirect = await gestionMouvementService.SendStock(mouvementStockModel, (int)userLieu);
            if (redirect)
            {
                TempData["Creation"] = "OK";
                return Redirect("/ProduitVendable/AlimentationStock");
            }
            else
            {
                var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

                //  ViewData["MouvementStock_FournisseurId"] = new SelectList(gestionMouvementService.getListFournisseur(AboId), "Founisseur_Id", "Founisseur_RaisonSocial");
                //	ViewData["MouvementStock_MouvementTypeId"] = new SelectList(gestionMouvementService.getListMouvement(AboId), "MouvementType_Id", "MouvementType_Libelle");
                ViewData["MouvementStock_MatierePremiereStokageId"] = new SelectList(gestionMouvementService.getListMatiereStockageAll(AboId, (int)userLieu), "MatierePremiereStokage_Id", "Matiere_Premiere.MatierePremiere_Libelle");
                ViewData["Lieu"] = new SelectList(zoneStockageService.getListLieuStockageActive(AboId, (int)userLieu), "LieuStockag_Id", "LieuStockag_Nom");
                ViewData["MouvementStock_UniteMesureId"] = new SelectList(produitVendableService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
                return View("~/Views/ProduitVendable/Alimentation/AlimentationStock.cshtml");
            }
        }
        [Authorize(Roles = "Gerant_de_stock")]
        public IActionResult ListAlimentationIntraStock(int? point, int? matiere, string date, int pg = 1)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var userLieu = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            if (date == null || date == "null" || date == "")
                date = "";
            ViewData["MouvementStock_MatierePremiereStokageId"] = new SelectList(gestionMouvementService.getListMatiereStockageAll(AboId, (int)userLieu), "MatierePremiereStokage_Id", "Matiere_Premiere.MatierePremiere_Libelle");
            ViewData["Lieu"] = new SelectList(zoneStockageService.getListLieuStockageActive(AboId, (int)userLieu), "LieuStockag_Id", "LieuStockag_Nom");
            var res = gestionMouvementService.getlistMouvements(AboId, userLieu, point, matiere, date);
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;
            int recsCount = res.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var model = res.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View("~/Views/ProduitVendable/Alimentation/ListeAlimentationIntra.cshtml", model);
        }
        public IActionResult MouvementsAll()
        {
            if (User.IsInRole("Client"))
            {
                var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                return View(produitVendableService.GetMouvementStocks(Id));
            }
            if (User.IsInRole("Gerant_de_stock"))
            {
                var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                return View(produitVendableService.GetMouvementStocksBystock(Id, (int)adresse));
            }
            return NotFound();
        }




        public IActionResult Approvisionnement()
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["PdV"] = new SelectList(pointVenteService.getListPointVenteActive(Id), "PointVente_Id", "PointVente_Nom");
            ViewData["production"] = new SelectList(produitVendableService.getListAteliers(Id), "Atelier_ID", "Atelier_Nom");
            return View("~/Views/ProduitVendable/Approvisionnement/Ajouter.cshtml");
        }
        [HttpPost]
        public SelectList produitList(int PointVenteId)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var point = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var prodPatissier = produitVendableService.getListProduitVendableAvecPlanif(point, AboId);
            var prodPdv = pointVenteService.getListProduitVendable(PointVenteId, AboId).Where(s => s.ProduitAppro_QuantiteProduite > 0).Select(p => p.Produit_Vendable);
            var listProds = new List<ProduitVendableModel>();
            foreach (var prod in prodPatissier)
            {
                bool alreadyExists = prodPdv.Any(x => x.ProduitVendable_Id == prod.ProduitVendable_Id);
                if (alreadyExists)
                    listProds.Add(prod);
                
            }
            var list = listProds;
            SelectList produitSelect = new SelectList(list, "ProduitVendable_Id", "ProduitVendable_Designation");
            return produitSelect;
        } 
        [HttpPost]
        public SelectList produitListPlanif(int PointProd)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var list = pointVenteService.getListProduitVendable(PointProd, AboId)
                .Where(s => s.ProduitAppro_QuantiteProduite > 0)
                .GroupBy(p => new { key = p.ProduitAppro_ProduitID, item = p.Produit_Vendable.ProduitVendable_Designation });
            SelectList produitSelect = new SelectList(list, "Key.key", "Key.item");
            return produitSelect;
        }
        [HttpPost]
        public decimal getQteProduite(int produitApproId)
        {
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var current = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var qte = produitVendableService.FindFormulaireProduitAppro(aboId, current, produitApproId);
            return qte;
        }
        [HttpPost]
        public decimal getQte(int forme)
        {
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var qte = gestionMouvementService.findformulaireProduitStock(aboId, forme).ProduitConsomableStokage_QuantiteActuelle;
            return qte;
        }

        [HttpPost]
        public async Task<bool> Approvisionnement( ApprovisionnementModel approModel)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            // GET CURRENT USER_ID and query it's abo_ID 
            approModel.Approvisionnement_AbonnementId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            if (User.IsInRole("Chef_Patissier"))
            {
                var userAtelier = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                approModel.Approvisionnement_AtelierID = userAtelier;
            }
            approModel.Approvisionnement_ProduitApproID = produitVendableService.GetProduitAppros(Id, approModel.Approvisionnement_AtelierID, approModel.Approvisionnement_ProduitId)
                .Select(p => p.ProduitAppro_Id)
                .FirstOrDefault();
            approModel.Approvisionnement_UtilisateurId = _userManager.GetUserId(HttpContext.User);
            var redirect = await produitVendableService.Approvisionnement(approModel);
            /*  if (redirect)
              {
                  TempData["Creation"] = "OK";
                  ViewData["PdV"] = new SelectList(pointVenteService.getListPointVenteActive(Id), "PointVente_Id", "PointVente_Nom");
                  return View("~/Views/ProduitVendable/Approvisionnement/Ajouter.cshtml");
              }
              else
              {
                  //ViewData["produit"] = new SelectList(produitVendableService.GetProduitAppros(Id), "Produit_Vendable.ProduitVendable_Id", "Produit_Vendable.ProduitVendable_Designation");
                  ViewData["PdV"] = new SelectList(pointVenteService.getListPointVenteActive(Id), "PointVente_Id", "PointVente_Nom");
                  return View("~/Views/ProduitVendable/Approvisionnement/Ajouter.cshtml");
              }*/
            return redirect;
        }
        public IActionResult ListeApprovisionnement(int? point, string date, int? etat,int? atelierID)
        {
            if (date == null || date == "null")
                date = "";
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["PdV"] = new SelectList(pointVenteService.getListPointVente(Id, null, null), "PointVente_Id", "PointVente_Nom");
            ViewData["atelier"] = new SelectList(produitVendableService.getListAteliers(Id), "Atelier_ID", "Atelier_Nom");
            if (User.IsInRole("Client"))
            {
                return View("~/Views/ProduitVendable/Approvisionnement/listeApprovAdmin.cshtml", produitVendableService.ListeApprovisionnement(Id, atelierID, point, date, etat));
            }
            if (User.IsInRole("Chef_Patissier"))
            {
                int ateId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                return View("~/Views/ProduitVendable/Approvisionnement/listeApprovisionnement.cshtml", produitVendableService.ListeApprovisionnementAtelier(Id, ateId, point, date, etat));
            }
            if (User.IsInRole("Responsable_Vente"))
            {
                int pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                return View("~/Views/ProduitVendable/Approvisionnement/ListeApproPointVente.cshtml", produitVendableService.ListeApprovisionnementPointVente(Id, pdvId, point, date));
            }
            return NotFound();

        }

        public Task<bool> ValiderApprovisionnement(int Approvisionnement_Id)
        {
            int pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var valideId = _userManager.GetUserId(HttpContext.User);
            return produitVendableService.ValiderApprovisionnement(Approvisionnement_Id, valideId, pdvId);

        }
        public IActionResult Packager()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            IEnumerable<ProduitVendableModel> stocks = null;
            if (User.IsInRole("Client"))
            {
                stocks = produitVendableService.getListProduitVendable(id, null, null, null);
            }
            else
            {
                var pdv = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                stocks = produitVendableService.getListProduitVendable(id, null, null, null);
            }
            var stock = stocks.GroupBy(p => new { key = p.ProduitVendable_Id, item = p.ProduitVendable_Designation, photo = p.ProduitVendable_GrandePhoto });
            //var package = pointVenteService.getStockProduitPackage(id, pdv);
            var pret = produitVendableService.getListProduitConsomable(id, null, null).Where(p => p.formes.Count() > 0);
            var model = new List<VenteApiModel>();
            foreach (var s in stock)
            {
                var v1 = new VenteApiModel
                {
                    ID = s.Key.key,
                    Designation = s.Key.item,
                    Image = s.Key.photo,
                    Type = "vendable",
                    filter = s.Key.key + "," + "vendable",
                };
                model.Add(v1);
            }
            foreach (var p in pret)
            {
                var v1 = new VenteApiModel
                {
                    ID = p.ProduitPretConsomer_ID,
                    Designation = p.ProduitPretConsomer_Designation,
                    Image = p.ProduitPretConsomer_Photo,
                    Type = "consommable",
                    filter = p.ProduitPretConsomer_ID + "," + "consommable",

                };
                model.Add(v1);
            }
            ViewData["ProduitVendable_FamilleProduitId"] = new SelectList(produitVendableService.getListFamilleProduit(id), "FamilleProduit_Id", "FamilleProduit_Libelle");
            ViewData["Produits"] = new SelectList(model, "filter", "Designation");
            ViewData["MatierePremiere"] = new SelectList(matierePremiereService.getListMatierePremiere(id, null, null), "MatierePremiere_Id", "MatierePremiere_Libelle");
            var x = new SelectList(produitVendableService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
            ViewData["Unite"] = x;
            return View("~/Views/ProduitVendable/ProduitPackage/AjouterProduit.cshtml");
        }

        [Authorize(Roles = "Client,Chef_Patissier")]
        public IActionResult ListeProduitsPackage(int pg=1)
        {
            var pdv = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var query = produitVendableService.getListProduitpackage(Id);
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;
            int recsCount = query.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var model = query.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager; 
            if (User.IsInRole("Chef_Patissier"))
            {
                return View("~/Views/ProduitVendable/ProduitPackage/ListeProduitsClient.cshtml", model);
            }
            /*if (User.IsInRole("Responsable_Vente"))
            {
                return View("~/Views/ProduitVendable/ProduitPackage/ListeProduitsPointVente.cshtml", produitVendableService.getListProduitpackagePointVente(Id, pdv));
            }*/
            if (User.IsInRole("Client"))
                return View("~/Views/ProduitVendable/ProduitPackage/ListeProduitsClient.cshtml", model);
            return NotFound();
        }
        [HttpPost]
        public ProduitPackageModel getProduitpackage(int Id)
        {
            var qte = produitVendableService.findFormulaireProduitPackage(Id);
            return qte;
        }
        [HttpPost]
        public ProduitPretConsomerModel getProduitpret(int Id)
        {
            var qte = produitVendableService.findFormulaireProduitPret(Id);
            return qte;
        }
        public IActionResult SousPrdouitsPackage(int Id)
        {
            var produitPackage = produitVendableService.findFormulaireProduitPackage(Id);
            var sousProduits = produitPackage.Sous_ProduitPackage;
            var sousMatieres = produitPackage.SousMatierePackages;
            var Composants = new Tuple<List<Sous_ProduitPackageModel>, List<SousMatierePackageModel>>(sousProduits, sousMatieres);
            return View("~/Views/ProduitVendable/ProduitPackage/SousProduits.cshtml", Composants);
        }
        public IActionResult ProduitPackageMatiere(int Id)
        {
            return View("~/Views/ProduitVendable/ProduitPackage/MatieresPremieres.cshtml", produitVendableService
                .findFormulaireProduitPackage(Id).Sous_ProduitPackage);
        }
        [HttpPost]
        public async Task<bool> PackagerProduit([FromForm] ProduitPackageModel produitModel)
        {
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
                        fileName = Path.Combine(_environment.WebRootPath, "images") + $@"\PorduitsPackage\{newFileName}";

                        // if you want to store path of folder in database
                        PathDB = "images/PorduitsPackage/" + newFileName;

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                        produitModel.ProduitPackage_Photo = PathDB;

                    }
                }


            }
            produitModel.ProduitPackage_CoutdeRevient = produitModel.Sous_ProduitPackage.Sum(p => p.SousProduitPackage_CoutDeRevient);
            produitModel.ProduitPackage_DateCreation = DateTime.Now;
            produitModel.ProduitPackage_AbonnementID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            //produitModel.ProduitPackage_PointVenteID = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var redirect = await produitVendableService.PackageProduit(produitModel);
            return redirect;
        }

        [HttpPost]
        public ProduitVendableModel GetProduit(int id)
        {
            return produitVendableService.findFormulaireProduitVendable(id);
        }
        [HttpPost]
        public Forme_ProduitModel GetFormeProduit(int id)
        {
            return produitVendableService.findFormulaireFormeProduit(id);
        }
        [HttpPost]
        public decimal GetPrix(int id, decimal qte)
        {
            return produitVendableService.GetPrix(id, qte);
        }
        [Authorize(Roles = "Client")]

        public IActionResult Formes(int Id)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var f = produitVendableService.getListFormeProduits(AboId, Id);
            return View("~/Views/ProduitVendable/FormesDeProduit.cshtml", f);
        }
        public IActionResult FormesPackage(int Id)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var f = produitVendableService.getListFormeProduitsPackage(AboId, Id);
            return View("~/Views/ProduitVendable/ProduitPackage/FormesProduit.cshtml", f);
        }
        public IActionResult FormesPret(int Id)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var f = produitVendableService.getListFormeProduitsPret(AboId, Id);
            return View("~/Views/ProduitVendable/ProduitConsomable/FormesProduitPret.cshtml", f);
        }
        [HttpPost]
        public SelectList FormesPretList(int id)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            SelectList desi = new SelectList(produitVendableService.getListFormeProduitsPret(AboId, id), "FormeProduit_ID", "FormeProduit_Libelle");
            return desi;
        }
        [HttpPost]
        public string getProduitDesi(int id)
        {
            var desi = produitVendableService.findFormulaireProduitVendable(id).ProduitVendable_Designation;
            return desi;
        }
        [HttpPost]
        public string getProduitDesiPackage(int id)
        {
            var desi = produitVendableService.findFormulaireProduitPackage(id).ProduitPackage_Designation;
            return desi;
        }
        [HttpPost]
        public string getProduitDesiPret(int id)
        {
            var desi = produitVendableService.findFormulaireProduitPret(id).ProduitPretConsomer_Designation;
            return desi;
        }
        [HttpPost]
        public IEnumerable<Forme_ProduitModel> getFormes(int Id)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            int pdv = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var f = pointVenteService.getStockPointVenteByProduit(AboId, pdv, Id).Select(p => p.Forme_Produit);
            return f.Where(p => p.FormeProduit_PrixVente > 0);
        }
        [HttpPost]
        public IEnumerable<Forme_ProduitModel> getFormesPackage(int Id)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            int pdv = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var f = produitVendableService.getListFormeProduitsPackage(AboId, Id);
            return f.Where(p => p.FormeProduit_PrixVente > 0);
        }
        [HttpPost]
        public IEnumerable<Forme_ProduitModel> getFormesPret(int Id)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            int pdv = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var f = produitVendableService.getListFormeProduitsPret(AboId, Id);
            return f.Where(p => p.FormeProduit_PrixVente > 0);
        }
        [HttpPost]
        public async Task<bool> AjouterFormeProduit(int id, List<Forme_ProduitModel> forme)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            try
            {
                foreach (var f in forme)
                {
                    f.FormeProduit_AbonnementID = AboId;
                    await produitVendableService.AjouterFormeProduitAsync(id, f);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost]
        public async Task<bool> AjouterFormeProduitPackage(int id, List<Forme_ProduitModel> forme)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            try
            {
                foreach (var f in forme)
                {
                    f.FormeProduit_AbonnementID = AboId;
                    await produitVendableService.AjouterFormeProduitPackageAsync(id, f);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost]
        public async Task<bool> AjouterFormeProduitPret(int id, List<Forme_ProduitModel> forme)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            try
            {
                foreach (var f in forme)
                {
                    f.FormeProduit_AbonnementID = AboId;
                    await produitVendableService.AjouterFormeProduitPretAsync(id, f);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost]
        public SelectList formeProduit(int Id)
        {
            SelectList formeProduit = new SelectList(produitFicheTechniqueService.getListFormes(Id), "FormeProduit_ID", "FormeProduit_Libelle");
            return formeProduit;
        }

        [HttpPost]
        public SelectList formeStock(int id)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            int pdv = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            SelectList formeProduit = new SelectList(produitVendableService.GetProduitAppros(Id, pdv, id), "Forme_Produit.FormeProduit_ID", "Forme_Produit.FormeProduit_Libelle");
            return formeProduit;
        }
        [HttpPost]
        public SelectList formeStockPV(string filter)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            // int pdv = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var words = filter.Split(',');
            var Id = Convert.ToInt32(words.ElementAt(0));
            var type = words.ElementAt(1);
            if (type == "vendable")
            {
                SelectList formeProduit = new SelectList(produitVendableService.getListFormeProduits(AboId, Id), "FormeProduit_ID", "FormeProduit_Libelle");
                return formeProduit;
            }
            else
            {
                SelectList formeConso = new SelectList(produitVendableService.getListFormeProduitsPret(AboId, Id), "FormeProduit_ID", "FormeProduit_Libelle");
                return formeConso;
            }
        }
        public IActionResult RetourStock()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["lieuID"] = new SelectList(zoneStockageService.getListLieuStockage(id, 1), "LieuStockag_Id", "LieuStockag_Nom");
            ViewData["MatierePremiere"] = new SelectList(matierePremiereService.getListMatierePremiere(id, null, null), "MatierePremiere_Id", "MatierePremiere_Libelle");
            ViewData["ProduitVendable_UniteMesureId"] = new SelectList(produitVendableService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
            return View("~/Views/ProduitVendable/Demandes/RetourStock.cshtml");
        }

        [HttpPost]
        public async Task<bool> Retour(RetourStockModel demandeModel)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            demandeModel.RetourStok_AbonnementID = Id;
            demandeModel.RetourStok_UtilisateurID = _userManager.GetUserId(HttpContext.User);
            var result = await produitVendableService.RetourStock(demandeModel);
            return result;
        }
        public IActionResult ListeDeRetour()
        {
            if (User.IsInRole("Chef_Patissier"))
            {
                var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                return View("~/Views/ProduitVendable/Demandes/ListRetours.cshtml", produitVendableService.GetRetourStocksByAtelier(Id, (int)adresse));
            }
            if (User.IsInRole("Gerant_de_stock"))
            {
                var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                return View("~/Views/ProduitVendable/Demandes/ListRetours.cshtml", produitVendableService.GetRetourStocksByStock(Id, (int)adresse));
            }
            return NotFound();

        }
        public Task<bool> AccepterRetour(int demandeId)
        {
            if (User.IsInRole("Chef_Patissier"))
                return produitVendableService.ValiderLivraison(demandeId);
            var adresse = authentificationRepository.Geturser(_userManager.GetUserId(HttpContext.User)).Lieu_Stockage.LieuStockag_Nom;
            var valideId = _userManager.GetUserId(HttpContext.User);
            return produitVendableService.AccepterRetour(demandeId, adresse, valideId);

        }
        public IActionResult AjouterProduitConsomable()
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

            ViewData["ProduitVendable_FamilleProduitId"] = new SelectList(produitVendableService.getListFamilleProduit(Id), "FamilleProduit_Id", "FamilleProduit_Libelle");
            ViewData["ProduitVendable_FormStockageId"] = new SelectList(produitVendableService.getListFormeSotckage(Id), "FormStockage_Id", "FormStockage_Libelle");
            ViewData["ProduitVendable_UniteMesureId"] = new SelectList(produitVendableService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");

            return View("~/Views/ProduitVendable/ProduitConsomable/Ajouter.cshtml");
        }
        [HttpPost]

        public async Task<IActionResult> AjouterProduitConsomable([FromForm] ProduitPretConsomerModel produitPret)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

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
                        fileName = Path.Combine(_environment.WebRootPath, "images") + $@"\produitsConso\{newFileName}";

                        // if you want to store path of folder in database
                        PathDB = "images/produitsConso/" + newFileName;

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                        produitPret.ProduitPretConsomer_Photo = PathDB;

                    }
                }


            }
            produitPret.ProduitPretConsomer_AbonnementID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

            var redirect = await produitVendableService.ProduitConsomable(produitPret);
            if (redirect)
            {
                TempData["Creation"] = "OK";
                return Redirect("/ProduitVendable/ListeProduitConsomable");
            }
            else
            {
                ViewData["ProduitVendable_FamilleProduitId"] = new SelectList(produitVendableService.getListFamilleProduit(Id), "FamilleProduit_Id", "FamilleProduit_Libelle");
                ViewData["ProduitVendable_FormStockageId"] = new SelectList(produitVendableService.getListFormeSotckage(Id), "FormeStockage_Id", "FormeStockage_Libelle");
                ViewData["ProduitVendable_UniteMesureId"] = new SelectList(produitVendableService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");

                return View("~/Views/ProduitVendable/ProduitConsomable/Ajouter.cshtml");
            }
        }

        public IActionResult ListeProduitConsomable(int? categ, int? SousCateg, int pg=1)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["ProduitVendable_FamilleProduitId"] = new SelectList(produitVendableService.getListFamilleProduit(Id), "FamilleProduit_Id", "FamilleProduit_Libelle");
            var query = produitVendableService.getListProduitConsomable(Id, categ, SousCateg);
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;
            int recsCount = query.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var model = query.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager; 
            return View("~/Views/ProduitVendable/ProduitConsomable/ListProduitsConso.cshtml",model);
        }
        [Authorize(Roles = "Chef_Patissier, Gerant_de_stock")]

        public IActionResult ReceptionProduitsConsomable()
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var f = gestionMouvementService.getListFournisseurProduits(AboId);
            ViewData["MouvementStock_FournisseurId"] = new SelectList(f, "FournisseurProduitConso_Id", "FournisseurProduitConso_RaisonSocial");
            return View("~/Views/ProduitVendable/ProduitConsomable/Stockage/AlimentationStockProduits.cshtml");
        }
        [HttpPost]
        public async Task<bool> ReceptionProduitsConsomable(List<MouvementProduitsConsoModel> mouvementStockModel)
        {
            var redirect = false;
            foreach (var m in mouvementStockModel)
            {
                // GET CURRENT USER_ID and query it's abo_ID 
                m.MouvementProduitsConso_AbonnementID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                redirect = await gestionMouvementService.ReceptionProduitsPretConso(m);
            }
            return redirect;
        }

        public IActionResult ListMouvementsProduits(int? produit, string date)
        {
            if (date == null || date == "null")
                date = "";
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var atelierId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var list = produitVendableService.getListProduitVendableStockerByAtelier(atelierId).GroupBy(p => new { key = p.ProduitConsomableStokage_ProduitVendableId, item = p.Produit_PretConsomer.ProduitPretConsomer_Designation });
            ViewData["produits"] = new SelectList(list, "Key.key", "Key.item");
            var mouvements = gestionMouvementService.getListMouvementsProduits(Id, atelierId, produit, date);
            return View("~/Views/ProduitVendable/ProduitConsomable/Stockage/MouvementConsomable.cshtml", mouvements);
        }


        [HttpPost]
        public SelectList ProsuitConsoListe(int fournisseurId)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var userId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var lst = gestionMouvementService.getProduitPretConsomers(fournisseurId, AboId, userId);
            var list = lst.GroupBy(p => new { key = p.ProduitConsomableStokage_ProduitVendableId, item = p.Forme_Produit.Produit_PretConsomer.ProduitPretConsomer_Designation });
            SelectList secteurSelect = new SelectList(list
                , "Key.key", "Key.item");
            return secteurSelect;
        }
        [HttpPost]
        public SelectList FormeConsoListe(int fournisseur, int produitID)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var list = gestionMouvementService.getProduitPretConsomersByPorduit(fournisseur, AboId, userId, produitID);
            // var list = lst.Where(p => p.ProduitConsomableStokage_ProduitVendableId == produitID);
            SelectList secteurSelect = new SelectList(list, "ProduitConsomableStokage_Id", "Forme_Produit.FormeProduit_Libelle");
            return secteurSelect;
        }
        [HttpPost]
        public SelectList FormeConsoEnStockListe(int produitId)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var list = gestionMouvementService.getProduitEnStockByProduit(produitId, userId);
            SelectList secteurSelect = new SelectList(list, "ProduitConsomableStokage_Id", "Forme_Produit.FormeProduit_Libelle");
            return secteurSelect;
        }

        [HttpPost]
        public SelectList getUniteAchatproduit(int produitStockageId)
        {
            var unite = produitVendableService.finformulaireProduitConsomableStockage(produitStockageId).Produit_PretConsomer.ProduitPretConsomer_UniteMesureAchatId;
            SelectList uniteselect = new SelectList(produitVendableService.findFormulaireUniteMesure((int)unite), "UniteMesure_Id", "UniteMesure_Libelle");
            return uniteselect;
        }
        public IActionResult FournisseurProduits()
        {
            ViewData["Fonction"] = new SelectList(produitVendableService.getListFonction(), "Fonction_ID", "Fonction_Libelle");
            ViewData["Ville"] = new SelectList(produitVendableService.getListeVille(), "Ville_ID", "Ville_Libelle");
            return View("~/Views/ProduitVendable/ProduitConsomable/Fournisseur/Ajouter.cshtml");
        }
        public IActionResult ConsulterProduitsConso(int Id)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["produitconso"] = new SelectList(produitVendableService.getListProduitConsomable(AboId, null, null), "ProduitPretConsomer_ID", "ProduitPretConsomer_Designation");
            return View("~/Views/ProduitVendable/ProduitConsomable/Fournisseur/ConsulterProduits.cshtml", produitVendableService.ListeProduitFournisseur(Id));
        }
        public IActionResult ModificationFournisseurProduit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var donnee = produitVendableService.findFormulaireFournisseur((int)Id);
            if (donnee == null)
            {
                return NotFound();
            }
            ViewData["Ville"] = new SelectList(produitVendableService.getListeVille(), "Ville_ID", "Ville_Libelle");

            return View("~/Views/ProduitVendable/ProduitConsomable/Fournisseur/Modification.cshtml", donnee);
        }
        [HttpPost]
        public async Task<bool> ModificationFournisseur(int id, FournisseurProduitsModel fournisseurModel)
        {
            var redirect = await produitVendableService.updateFormulaireFournisseur(id, fournisseurModel);
            return redirect;
        }
        public IActionResult ListeFournisseurProduits(int? ville, int? statut)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["produitconso"] = new SelectList(produitVendableService.getListProduitConsomable(AboId, null, null), "ProduitPretConsomer_ID", "ProduitPretConsomer_Designation");
            ViewData["Ville"] = new SelectList(produitVendableService.getListeVille(), "Ville_ID", "Ville_Libelle");
            return View("~/Views/ProduitVendable/ProduitConsomable/Fournisseur/ListeFournisseurs.cshtml", produitVendableService.ListeFournisseurProduits(AboId, ville, statut));
        }
        [HttpPost]
        public async Task<bool> FournisseurProduits(FournisseurProduitsModel fournisseurModel)
        {
            fournisseurModel.FournisseurProduitConso_AbonnementID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            fournisseurModel.FournisseurProduitConso_UtilisateurId = _userManager.GetUserId(HttpContext.User);
            var redirect = await produitVendableService.FournisseurProduit(fournisseurModel);
            return redirect;
        }
        [HttpPost]
        public string getFournisseurNom(int Id)
        {
            return produitVendableService.findFormulaireFournisseur(Id).FournisseurProduitConso_RaisonSocial;
        }
        [HttpPost]
        public async Task<bool> AjouterMatiere(int idFournisseur, List<int> listMatiere)
        {
            try
            {
                var res = await produitVendableService.AjouterMatiere(idFournisseur, listMatiere);
                if (res == null)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IActionResult ApprovisionnementProduits()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var atelierId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var list = produitVendableService.getListProduitVendableStockerByAtelier(atelierId);
            var o = new SelectList(list.GroupBy(p => new { key = p.ProduitConsomableStokage_ProduitVendableId, item = p.Produit_PretConsomer.ProduitPretConsomer_Designation })
            , "Key.key", "Key.item");
            ViewData["produit"] = o;
            ViewData["PdV"] = new SelectList(pointVenteService.getListPointVenteActive(Id), "PointVente_Id", "PointVente_Nom");
            ViewData["unite"] = new SelectList(produitVendableService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
            return View("~/Views/ProduitVendable/ProduitConsomable/Approvisionnement/Ajouter.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> ApprovisionnementProduits([FromForm] Approvisionnement_ProduitConsoModel approModel)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var userLieu = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            // GET CURRENT USER_ID and query it's abo_ID 
            approModel.ApprovisionnementProduit_AbonnementId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            approModel.ApprovisionnementProduit_StockID = userLieu;
            approModel.ApprovisionnementProduit_LieuUserId = _userManager.GetUserId(HttpContext.User);
            var redirect = await produitVendableService.ApprovisionnementProduit(approModel);
            if (redirect)
            {
                TempData["Creation"] = "OK";
                var list = produitVendableService.getListProduitConsomable(Id, null, null);
                ViewData["produit"] = new SelectList(list, "ProduitPretConsomer_ID", "ProduitPretConsomer_Designation");
                ViewData["PdV"] = new SelectList(pointVenteService.getListPointVenteActive(Id), "PointVente_Id", "PointVente_Nom");
                return View("~/Views/ProduitVendable/ProduitConsomable/Approvisionnement/Ajouter.cshtml");
            }
            else
            {
                var list = produitVendableService.getListProduitConsomable(Id, null, null);
                ViewData["produit"] = new SelectList(list, "ProduitPretConsomer_ID", "ProduitPretConsomer_Designation");
                ViewData["PdV"] = new SelectList(pointVenteService.getListPointVenteActive(Id), "PointVente_Id", "PointVente_Nom");
                return View("~/Views/ProduitVendable/ProduitConsomable/Approvisionnement/Ajouter.cshtml");
            }
        }
        public IActionResult ListeApprovisionnementProduits(int? point, string date)
        {
            if (date == null || date == "null")
                date = "";
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["PdV"] = new SelectList(pointVenteService.getListPointVente(Id, null, null), "PointVente_Id", "PointVente_Nom");
            ViewData["atelier"] = new SelectList(zoneStockageService.getListLieuStockage(Id, 1), "LieuStockag_Id", "LieuStockag_Nom");
            if (User.IsInRole("Client"))
            {
                return View("~/Views/ProduitVendable/ProduitConsomable/Approvisionnement/ApprovisionnementProduit.cshtml", produitVendableService.ListeApprovisionnementProduit(Id));
            }
            if (User.IsInRole("Gerant_de_stock"))
            {
                int ateId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                return View("~/Views/ProduitVendable/ProduitConsomable/Approvisionnement/ApprovisionnementProduit.cshtml", produitVendableService.ListeApprovisionnementProduitAtelier(Id, ateId, point, date));
            }
            if (User.IsInRole("Responsable_Vente"))
            {
                int pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                return View("~/Views/ProduitVendable/ProduitConsomable/Approvisionnement/ApproProduit_Pdv.cshtml", produitVendableService.ListeApprovisionnementProduitPdv(Id, pdvId, point, date));
            }
            return NotFound();

        }

        public Task<bool> ValiderApprovisionnementProduit(int Approvisionnement_Id)
        {
            int pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var userid = _userManager.GetUserId(HttpContext.User);
            return produitVendableService.ValiderApprovisionnementproduit(Approvisionnement_Id, userid, pdvId);

        }
        [HttpPost]

        public decimal getStockPointVente(int formeId)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var pdv = authentificationRepository.Geturser(_userManager.GetUserId(HttpContext.User)).Position_Vente.Point_Vente.PointVente_Id;
            var o = pointVenteService.getStockPointVente(Id, pdv)
                .Where(p => p.PointVenteStock_FormeProduitID == formeId)
                .FirstOrDefault()
                .PointVenteStock_QuantiteProduit;
            return o;
        }
        [HttpPost]

        public async Task<bool> Vente(VenteModel venteModel)
        {
            decimal totalemarge = 0;
            decimal totaleprix = 0;
            // GET CURRENT USER_ID and query it's abo_ID 
            venteModel.Vente_AbonnementId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            venteModel.Vente_PointVenteId = authentificationRepository.Geturser(_userManager.GetUserId(HttpContext.User)).Position_Vente.Point_Vente.PointVente_Id;
            venteModel.Vente_PositionVenteId = Convert.ToInt32(HttpContext.User.FindFirst("posId").Value);
            venteModel.Vente_UtilisateurId = _userManager.GetUserId(HttpContext.User);
            venteModel.Vente_Date = DateTime.Now;
            foreach (var v in venteModel.Details)
            {
                totalemarge += v.VenteDetails_Marge;
                totaleprix += v.VenteDetails_Prix;
            }
            venteModel.Vente_Prix = totaleprix;
            venteModel.Vente_Marge = totalemarge;
            var redirect = await venteProduitsService.CreateVente(venteModel);
            if (redirect)
            {
                TempData["Creation"] = "OK";
                return redirect;
            }
            else
            {
                return false;
            }
        }
        public IActionResult ListeVentes(string date, int? point)
        {
            if (date == null || date == "null")
                date = "";
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var ventes = new List<VenteModel>();
            ViewData["ProduitVendable_FamilleProduitId"] = new SelectList(produitVendableService.getListFamilleProduit(Id), "FamilleProduit_Id", "FamilleProduit_Libelle");
            if (User.IsInRole("Client"))
            { 
                if(point!=null)
                    ventes = (List<VenteModel>)venteProduitsService.getListVenteByPdv(Id, (int)point, date);
                ViewData["pdv"] = new SelectList(pointVenteService.getListPointVenteActive(Id), "PointVente_Id", "PointVente_Nom");
                return View("~/Views/ProduitVendable/Ventes/ListVentes.cshtml",ventes);
            }
            if (User.IsInRole("Responsable_Vente"))
            {
                int pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                return View("~/Views/ProduitVendable/Ventes/ListVentes.cshtml",
                    venteProduitsService.getListVenteByPdv(Id, pdvId, date));
            }
            return NotFound();
        }
        public IActionResult ProductionPackage(int? Id)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["lieuStock"] = new SelectList(zoneStockageService.getListLieuStockage(id, 1), "LieuStockag_Id", "LieuStockag_Nom");
            if (Id != null)
            {
                ViewData["prduits"] = new SelectList(produitVendableService.getProduitpackagePointVente((int)Id), "ProduitPackage_ID", "ProduitPackage_Designation");
                return View("~/Views/ProduitVendable/ProduitPackage/ProdPackage.cshtml");
            }
            else
            {
                int pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                ViewData["prduits"] = new SelectList(produitVendableService.getListProduitpackagePointVente(id, pdvId), "ProduitPackage_ID", "ProduitPackage_Designation");
                if (User.IsInRole("Client"))
                    return View("~/Views/ProduitVendable/ProduitPackage/ProdPackage.cshtml");
                else
                    return View("~/Views/ProduitVendable/ProduitPackage/ProductionProduit.cshtml");
            }
        }

        [HttpPost]
        public async Task<bool> ProduirePackage(PackageProductionModel productionModel)
        {
            int pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            productionModel.PackageProduction_AbonnementID = Id;
            productionModel.PackageProduction_UtilisateurID = _userManager.GetUserId(HttpContext.User);
            var result = await produitVendableService.ProductionPackage(productionModel, pdvId);
            return result;

        }
        [HttpPost]
        public async Task<bool> PackageProd(Package_FormeModel packFormeModel)
        {
            //int pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            packFormeModel.PackageForme_AbonnementID = Id;
            var result = await produitVendableService.PackageForme(packFormeModel);
            return result;

        }
        [HttpPost]
        public SelectList sousProduitPackage(int produitId, string Id)
        {
            var res = Id.Split(",");
            SelectList sousProduit = new SelectList(produitVendableService.getListSousProduitpackage(produitId).Where(p => p.SousProduitPackage_FormeProduittID == Convert.ToInt32(res.ElementAt(0))), "SousProduitPackage_FormeProduittID", "Forme_Produit.FormeProduit_Libelle");
            return sousProduit;
        }
        [HttpPost]
        public SelectList getUniteDeMesure(int produitId, int SousMatiereId)
        {
            var produitPackage = produitVendableService.findFormulaireProduitPackage(produitId);
            var sousMatiere = produitPackage.SousMatierePackages.Where(p => p.SousMatierePackage_ID == SousMatiereId).FirstOrDefault();
            if (sousMatiere != null)
            {
                var Unites = matierePremiereService.getListUniteUtilisation(sousMatiere.SousMatierePackage_MatiereID);
                SelectList unitesUtils = new SelectList(Unites, "Unite_Mesure.UniteMesure_Id", "Unite_Mesure.UniteMesure_Libelle");
                return unitesUtils;
            }
            else
                return null;
            
        }
        [HttpPost]
        public string getCoutDeRevient(int produitId, int SousMatiereId)
        {
            var produitPackage = produitVendableService.findFormulaireProduitPackage(produitId);
            var sousMatiere = produitPackage.SousMatierePackages.Where(p => p.SousMatierePackage_ID == SousMatiereId).FirstOrDefault();
            if (sousMatiere != null)
            {
                var cdr = sousMatiere.Matiere_Premiere.MatierePremiere_PrixMoyenAchat;
                var uniteAchat = sousMatiere.Matiere_Premiere.MatierePremiere_AchatUniteMesureId;
                return cdr + "_" + uniteAchat;
            }
            else
             return null;
        }
        [HttpPost]
        public SelectList sousProduitPackageDesi(int produitId)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            //var SProduit = produitVendableService.getListSousProduitpackage(produitId);
            var produit = produitVendableService.getListSousProduitpackage(produitId);
            var produitsPret = new List<ProduitPretConsomerModel>();
            var produitsVendable = new List<ProduitVendableModel>();
            var model = new List<VenteApiModel>();
            foreach (var i in produit)
            {
                var produitvendable = produitVendableService.findFormulaireFormeProduit(i.SousProduitPackage_FormeProduittID).Produit_Vendable;
                if (produitvendable == null)
                {
                    var produitPret = produitVendableService.findFormulaireFormeProduit(i.SousProduitPackage_FormeProduittID).Produit_PretConsomer;
                    var v1 = new VenteApiModel
                    {
                        ID = produitPret.ProduitPretConsomer_ID,
                        Designation = produitPret.ProduitPretConsomer_Designation,
                        //Type = "consomable",
                        filter = i.SousProduitPackage_FormeProduittID.ToString() +","+ i.SousProduitPackage_ID.ToString() +","+ produitPret.ProduitPretConsomer_ID,
                    };
                    model.Add(v1); 
                    //produitsPret.Add(produitPret);
                }
                else
                {
                    var v1 = new VenteApiModel
                    {
                        ID = produitvendable.ProduitVendable_Id,
                        Designation = produitvendable.ProduitVendable_Designation,
                        //Type = "vendable",
                        filter = i.SousProduitPackage_FormeProduittID.ToString() + "," + i.SousProduitPackage_ID.ToString() + "," + produitvendable.ProduitVendable_Id,
                    };
                    model.Add(v1);
                    //produitsVendable.Add(produitvendable);
                }
            }
/*            foreach (var pv in produitsVendable)
            {
                
            }
            foreach (var pp in produitsPret)
            {
                
            }*/
            var m = model.GroupBy(p => new { key = p.ID, item = p.Designation, filter = p.filter });
            return new SelectList(m, "Key.filter", "Key.item");
        } [HttpPost]
        public SelectList sousMatieresPackageDesi(int produitId)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            //var SProduit = produitVendableService.getListSousProduitpackage(produitId);
            var produit = produitVendableService.findFormulaireProduitPackage(produitId);
            var SousMatieres = produit.SousMatierePackages;
            return new SelectList(SousMatieres, "SousMatierePackage_ID", "Matiere_Premiere.MatierePremiere_Libelle");
        }
        [HttpPost]
        public SelectList formeProduitPackage(int Id)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            SelectList formeProduit = new SelectList(produitVendableService.getListFormeProduitsPackage(AboId, Id), "FormeProduit_ID", "FormeProduit_Libelle");
            return formeProduit;
        }
        [HttpPost]
        public SelectList FormePretstockage(int Id)
        {
            var list = produitVendableService.getListProduitVendableStockerByProduit(Convert.ToInt32(HttpContext.Session.GetString("mysession")), Id);
            SelectList formeProduit = new SelectList(list, "ProduitConsomableStokage_Id", "Forme_Produit.FormeProduit_Libelle");
            return formeProduit;
        }
        [HttpPost]
        public FormeDetailsModel getFormeDetails(int Id)
        {
            var formeDetails = produitVendableService.getFormeDetails(Id,null);
            return formeDetails;
        }
        [HttpPost]
        public decimal getFormeCoutdeRevient(int Id)
        {
            var CdR = produitVendableService.findFormulaireFormeProduit(Id).FormeProduit_CoutDeRevient;
            return CdR;
        }
        public IActionResult RetourDesProduits()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var pdv = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var stocks = pointVenteService.getStockPointVenteAvecPlan(id, pdv);
            var stock = stocks.GroupBy(p => new { key = p.PointVenteStock_ProduitID, item = p.Produit_Vendable.ProduitVendable_Designation, photo = p.Produit_Vendable.ProduitVendable_GrandePhoto });
            var package = pointVenteService.getStockProduitPackage(id, pdv);
            var pret = produitVendableService.getListProduitConsomable(id, null, null);
            var model = new List<VenteApiModel>();
            foreach (var s in stock)
            {
                var v1 = new VenteApiModel
                {
                    ID = s.Key.key,
                    Designation = s.Key.item,
                    Image = s.Key.photo,
                    Type = "vendable"
                };
                model.Add(v1);
            }
            foreach (var pack in package)
            {
                var v1 = new VenteApiModel
                {
                    ID = pack.ProduitPackage_ID,
                    Designation = pack.ProduitPackage_Designation,
                    Image = pack.ProduitPackage_Photo,
                    Type = "package"

                };
                model.Add(v1);
            }
            foreach (var p in pret)
            {
                var v1 = new VenteApiModel
                {
                    ID = p.ProduitPretConsomer_ID,
                    Designation = p.ProduitPretConsomer_Designation,
                    Image = p.ProduitPretConsomer_Photo,
                    Type = "consomable"

                };
                model.Add(v1);
            }
            ViewData["produits"] = new SelectList(model, "ID", "Designation");
            return View("~/Views/ProduitVendable/ventes/RetourDesProduits.cshtml");
        }
        [HttpPost]
        public SelectList getUniteVente(int produitId)
        {
            var produit = produitVendableService.findFormulaireProduitVendable(produitId);
            if (produit == null)
            {
                var pret = produitVendableService.findFormulaireProduitPret(produitId);
                if (pret == null)
                {
                    var package = produitVendableService.findFormulaireProduitPackage(produitId);
                    if (package != null)
                    {
                        SelectList unitepackage = new SelectList(produitVendableService.findFormulaireUniteMesure(package.ProduitPackage_UniteVente), "UniteMesure_Id", "UniteMesure_Libelle");
                        return unitepackage;
                    }
                }
                else
                {
                    SelectList unitepret = new SelectList(produitVendableService.findFormulaireUniteMesure((int)pret.ProduitPretConsomer_UniteMesureAchatId), "UniteMesure_Id", "UniteMesure_Libelle");
                    return unitepret; 
                }
            }
            else 
            {
                SelectList unitevendable = new SelectList(produitVendableService.findFormulaireUniteMesure((int)produit.ProduitVendable_UniteMesureId), "UniteMesure_Id", "UniteMesure_Libelle");
                return unitevendable;
            }
            return null;
        }
        public SelectList getUniteVentePret(int produitId)
        {
            var pret = produitVendableService.findFormulaireProduitPret(produitId);
            SelectList unitepret = new SelectList(produitVendableService.findFormulaireUniteMesure((int)pret.ProduitPretConsomer_UniteMesureAchatId), "UniteMesure_Id", "UniteMesure_Libelle");
            return unitepret;
        }

        [HttpPost]
        public async Task<bool> RetourDesProduits(RetourProduitsModel retourModel)
        {
            decimal sum = 0;
            var userId = _userManager.GetUserId(HttpContext.User);
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            retourModel.Retour_AbonnementID = Id;
            retourModel.Retour_UtilisateurID = userId;
            retourModel.Retour_PositionVenteID = Convert.ToInt32(HttpContext.User.FindFirst("posId").Value);
            foreach (var p in retourModel.retourDetails)
            {
                sum += p.RetourDetails_PrixTotal;
            }
            retourModel.Retour_PrixTotal = sum;
            var result = await venteProduitsService.RetourProduit(retourModel);
            return result;

        }
        [Authorize(Roles = "Responsable_Vente, Client")]
        public IActionResult ListeRetours(string date, int? point)
        {
            if (date == null || date == "null")
                date = "";
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var retours = new List<RetourProduitsModel>();
            if (User.IsInRole("Client"))
            {
                ViewData["pdv"] = new SelectList(pointVenteService.getListPointVenteActive(Id), "PointVente_Id", "PointVente_Nom");
                if (point != null)
                {
                    retours = (List<RetourProduitsModel>)venteProduitsService.getListRetours(Id, (int)point, date);
                }
                return View("~/Views/ProduitVendable/Ventes/ListeRetours.cshtml",retours);

            }
            else
            {
                int pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                return View("~/Views/ProduitVendable/Ventes/ListeRetours.cshtml", venteProduitsService.getListRetours(Id, pdvId, date));
            }
            
        }
        public IActionResult ConsulterRetour(int Id)
        {
            return View("~/Views/ProduitVendable/Ventes/Consulter.cshtml", venteProduitsService.getListDetails(Id));
        }
        public IActionResult Consultervente(int Id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            int? pv = null;
            string date = "";
            return View("~/Views/ProduitVendable/Ventes/DetailsVente.cshtml", venteProduitsService.getListDetailsVentes(pv, date, aboId, Id));
        }
        public IActionResult ConsulterMouvement(int Id)
        {

            return View("~/Views/ProduitVendable/Caisse/MouvementsCaisse.cshtml",
                venteProduitsService.getListMouvementsCaisseParCaisse(Id));
        }
        [Authorize(Roles = "Responsable_Vente, Client")]
        public IActionResult AlimentationCaisse()
        {
            int pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["pdv"] = new SelectList(pointVenteService.getListPointVenteActive(aboId), "PointVente_Id", "PointVente_Nom");
            var data = new SelectList(pointVenteService.getListPositionVente(pdvId), "PositionVente_Id", "PositionVente_Libelle");
            ViewData["Positions"] = data;
            return View("~/Views/ProduitVendable/Caisse/AlimentationCaisse.cshtml");
        }
        [HttpPost]
        public async Task<bool> AlimentationCaisse(List<AllimentationCaisseModel> alimentationModel)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            try
            {
                foreach (var alim in alimentationModel)
                {
                    alim.AllimentationCaisse_AbonnementID = Id;
                    alim.AllimentationCaisse_UtilsateurID = userId;
                    if (User.IsInRole("Responsable_Vente"))
                    {
                        alim.AllimentationCaisse_PointVenteID = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                    }
                    await venteProduitsService.AlimentationCaisse(alim);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public IActionResult ListeAlimentations(int? pdv, string date)
        {
            if (date == null || date == "null")
                date = "";
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var alimentation = new List<AllimentationCaisseModel>();
            if (User.IsInRole("Client"))
            {
                ViewData["pdv"] = new SelectList(pointVenteService.getListPointVenteActive(Id), "PointVente_Id", "PointVente_Nom");
                if (pdv != null)
                {
                    alimentation = (List<AllimentationCaisseModel>)venteProduitsService.getListAlimentations(Id, pdv, date);
                }
                return View("~/Views/ProduitVendable/Caisse/ListeAlimentations.cshtml", alimentation);
            }
            else
            {
                int pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                var alim = venteProduitsService.getListAlimentations(Id, pdvId, date);
                return View("~/Views/ProduitVendable/Caisse/ListeAlimentations.cshtml", alim);
            }

        }
        [Authorize(Roles = "Responsable_Vente , Client")]
        public IActionResult RetraitCaisse()
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            int pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            ViewData["Positions"] = new SelectList(pointVenteService.getListPositionVente(pdvId), "PositionVente_Id", "PositionVente_Libelle");
            ViewData["pdv"] = new SelectList(pointVenteService.getListPointVenteActive(Id), "PointVente_Id", "PointVente_Nom");
            var type = venteProduitsService.getListRetraitType();
            ViewData["Type"] = new SelectList(type, "RetraitType_ID", "RetraitType_Libelle");
            return View("~/Views/ProduitVendable/Caisse/RetraitCaisse.cshtml");
        }
        [HttpPost]
        public async Task<bool> RetraitCaisse(List<RetraitCaisseModel> retraitModel)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            try
            {
                foreach (var r in retraitModel)
                {
                    r.RetraitCaisse_AbonnementID = Id;
                    r.RetraitCaisse_UtilisateurID = userId;
                    //r.RetraitCaisse_PositionVenteID = authentificationRepository.Geturser(userId).Point_Vente.PointVente_Id;
                    await venteProduitsService.RetraitCaisse(r);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        [Authorize(Roles = "Responsable_Vente, Client")]
        public IActionResult ListeRetraits(int? pdv, string date)
        {
            if (date == null || date == "null")
                date = "";
            var retraits = new List<RetraitCaisseModel>();
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["pdv"] = new SelectList(pointVenteService.getListPointVenteActive(Id), "PointVente_Id", "PointVente_Nom");
            int pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            if (User.IsInRole("Responsable_Vente"))
            { retraits = (List<RetraitCaisseModel>)venteProduitsService.getListRetrait(Id, pdvId, date); }
            else
            { 
                if(pdv!=null)
                    retraits = (List<RetraitCaisseModel>)venteProduitsService.getListRetrait(Id, pdv, date); }

            return View("~/Views/ProduitVendable/Caisse/ListeRetraits.cshtml", retraits);
        }
        [Authorize(Roles = "Responsable_Vente, Client")]
        public IActionResult ClotureJournee()
        {
            int pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            //ViewData["Id"] = pdvId;
            ViewData["Positions"] = new SelectList(pointVenteService.getListPositionVente(pdvId), "PositionVente_Id", "PositionVente_Libelle");
            ViewData["pdv"] = new SelectList(pointVenteService.getListPointVenteActive(Id), "PointVente_Id", "PointVente_Nom");
            return View("~/Views/ProduitVendable/Caisse/ClotureJournee.cshtml");
        }
        [HttpPost]
        public decimal getSoldeDebitCaisse(int caisseId, string date)
        {
            if (date == null)
                date = "";
            var aboID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var soldeD = venteProduitsService.GetSoleDebit(caisseId, date, aboID);
            return soldeD;
        }
        [HttpPost]
        public decimal getSoldeCreditCaisse(int caisseId, string date)
        {
            if (date == null)
                date = "";
            var aboID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var soldec = venteProduitsService.GetSoleCredit(caisseId, date, aboID);
            return soldec;
        }
        [HttpPost]
        public decimal getAlimentationCaisse(int caisseId, string date)
        {
            if (date == null)
                date = "";
            var aboID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var alim = venteProduitsService.GetAlimentation(caisseId, date, aboID);
            return alim;
        } 
        [HttpPost]
        public decimal getSoldeTotal(int caisseId)
        {
            
            var aboID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var soldeD = venteProduitsService.GetSoleDebit(caisseId, "", aboID);
            var soldec = venteProduitsService.GetSoleCredit(caisseId, "", aboID);
            var alim = venteProduitsService.GetAlimentation(caisseId, "", aboID);
            var solde = soldeD + alim - soldec;
            return solde;
        }
        [HttpPost]
        public async Task<bool> Cloturer(ClotureJourneeModel clotureModel)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            clotureModel.ClotueJournee_UtilisateurID = userId;
            clotureModel.ClotueJournee_AbonnementID = Id;
            clotureModel.ClotueJournee_Valide = 0;
            await venteProduitsService.UpdateVente(clotureModel.ClotueJournee_PositionVenteID, "");
            var redirect = await venteProduitsService.ClotureJournee(clotureModel);
            return redirect;
        }
        public IActionResult ListeDesClotures(int? point, int? caisseId, string date)
        {
            if (date == null || date == "null")
                date = "";
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var pdv = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var clotures = new List<ClotureJourneeModel>();
            ViewData["Pdv"] = new SelectList(pointVenteService.getListPointVenteActive(Id), "PointVente_Id", "PointVente_Nom");
            var reste = false;
            if (User.IsInRole("Client"))
            {
                clotures = (List<ClotureJourneeModel>)venteProduitsService.getListCloture(Id, point, date);
            }
            if (User.IsInRole("Responsable_Vente"))
            {
                ViewData["Positions"] = new SelectList(pointVenteService.getListPositionVente(pdv), "PositionVente_Id", "PositionVente_Libelle");
                var r = venteProduitsService.CalculeClotures(pdv);
                reste = r;
                clotures = (List<ClotureJourneeModel>)venteProduitsService.getListCloturePerPdv(Id, pdv, caisseId, date);
            }
            var tuple = new Tuple<List<ClotureJourneeModel>, bool>(clotures, reste);
            return View("~/Views/ProduitVendable/Caisse/ListeDesClotures.cshtml", tuple);
        }

        [HttpPost]
        public async Task<bool> DeleteFournisseurPrdConso(int id, int code)
        {
            var result = await produitVendableService.deleteFournisseurPrdConso(id, code);
            return result;
        }

        [HttpPost]

        public async Task<bool> Commader(CommandeModel commandeModel)
        {

            commandeModel.Commande_AbonnementId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            commandeModel.Commande_CaisseCommandeId = Convert.ToInt32(HttpContext.User.FindFirst("posId").Value);
            commandeModel.Commande_DateCreation = DateTime.Now;
            commandeModel.Commande_MontantTotal = commandeModel.details.Sum(p => p.CommandeDetail_Prix);
            commandeModel.Commande_UtilisateurCommandeId = _userManager.GetUserId(HttpContext.User);

            bool redirect = await venteProduitsService.CreateCommade(commandeModel);
            if (redirect)
            {
                TempData["Creation"] = "OK";
                return redirect;
            }
            else
            {
                return false;
            }
        }
        public IActionResult ListeCommandes(int? statutLiv, int? statutPaiement, string date, int? point)
        {
            if (date == null || date == "null")
                date = "";
            var commandes = new List<CommandeModel>();
            ViewData["livraison"] = new SelectList(venteProduitsService.getListStatutLivraison(), "StatutCommande_Id", "StatutCommande_Libelle");
            ViewData["paiement"] = new SelectList(venteProduitsService.getListStatutPaiement(), "StatutPaiementCommande_ID", "StatutPaiementCommande_Libelle"); 
            if (User.IsInRole("Client"))
            {
                var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                ViewData["pdv"] = new SelectList(pointVenteService.getListPointVenteActive(Id), "PointVente_Id", "PointVente_Nom");
                if (point != null)
                {
                    commandes = (List<CommandeModel>)venteProduitsService.getListCommandes((int)point, statutLiv, statutPaiement, date);
                }
                return View("~/Views/ProduitVendable/Ventes/ListeCommandes.cshtml", commandes);
            }
            else
            {
                var pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                
                return View("~/Views/ProduitVendable/Ventes/ListeCommandes.cshtml",
                    venteProduitsService.getListCommandes(pdvId, statutLiv, statutPaiement, date));
            }

        }
        [HttpPost]
        public async Task<bool> PlanificationAuto()
        {
            var pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var result = await venteProduitsService.PlanificationDeCloture(Id, (int)pdvId);
            return result;
        }
        public IActionResult ConsulterCommande(int Id)
        {
            var details = venteProduitsService.getListDetailsCommande(Id);
            return View("~/Views/ProduitVendable/Ventes/DetailsCommande.cshtml", details);
        }
        public IActionResult ListePertes(int? point, string date)
        {
            if (date == null || date == "null")
                date = "";
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var pertes = new List<PerteModel>();
            if (User.IsInRole("Client"))
            {
                ViewData["pdv"] = new SelectList(pointVenteService.getListPointVenteActive(Id), "PointVente_Id", "PointVente_Nom");
                if (point != null)
                {
                    pertes = (List<PerteModel>)venteProduitsService.getListPertes((int)point, date);
                }
                return View("~/Views/ProduitVendable/Ventes/ListePertes.cshtml", pertes);

            }
            else
            {
                var pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                return View("~/Views/ProduitVendable/Ventes/ListePertes.cshtml",
                    venteProduitsService.getListPertes(pdvId, date));
            }
            
        }
        public IActionResult ListeGratuites(int? point, string date)
        {
            if (date == null || date == "null")
                date = "";
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var gratuite = new List<GratuiteModel>();
            if (User.IsInRole("Client"))
            {
                ViewData["pdv"] = new SelectList(pointVenteService.getListPointVenteActive(Id), "PointVente_Id", "PointVente_Nom");
                if (point != null)
                {
                    gratuite = (List<GratuiteModel>)venteProduitsService.getListGratuite((int)point, date);
                }
                return View("~/Views/ProduitVendable/Ventes/ListeGratuites.cshtml", gratuite);

            }
            else
            {
                var pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                return View("~/Views/ProduitVendable/Ventes/ListeGratuites.cshtml",
                    venteProduitsService.getListGratuite(pdvId, date));
            }

        }
        public IActionResult ConsulterPerte(int Id)
        {
            var details = venteProduitsService.getListDetailsPertes(Id);
            return View("~/Views/ProduitVendable/Ventes/DetailsPerte.cshtml", details);
        }
        public IActionResult ConsulterGratuite(int Id)
        {
            var details = venteProduitsService.getListDetailsGratuite(Id);
            return View("~/Views/ProduitVendable/Ventes/DetailsGratuite.cshtml", details);
        }
        public IActionResult ConsulterInventaire(int? pdvId, int pg = 1)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var Inventaire = new List<InventaireModel>();
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;
            ViewData["Pdv"] = new SelectList(pointVenteService.getListPointVenteActive(id), "PointVente_Id", "PointVente_Nom");
            if (User.IsInRole("Client"))
            {
                if (pdvId == null)
                { }
                else
                {
                    var stocks = pointVenteService.getStockPointVente(id, (int)pdvId);
                    var stock = stocks.GroupBy(p => new { key = p.PointVenteStock_ProduitID, item = p.Produit_Vendable.ProduitVendable_Designation, photo = p.Produit_Vendable.ProduitVendable_GrandePhoto, unite = p.Produit_Vendable.Unite_Mesure });
                    var pp = pointVenteService.getStockProduitPackage(id, (int)pdvId);
                    var package = pp.Where(p => p.formes.Where(f => f.FormeProduit_PrixVente > 0).Count() > 0);
                    var pret = produitVendableService.getListProduitConsomableEnMagasin(id, (int)pdvId, null);
                    foreach (var s in stock)
                    {
                        var formesAll = pointVenteService.getStockPointVenteByProduit(id, (int)pdvId, s.Key.key);
                        var formes = formesAll.Select(p => p.Forme_Produit);
                        var i = 0;
                        var formeModel = new List<FormeApiResultModel>();
                        foreach (var f in formes)
                        {
                            var m1 = new FormeApiResultModel
                            {
                                Forme_Libelle = f.FormeProduit_Libelle,
                                Forme_QteStock = (formesAll.Count() > 0 ? formesAll.ElementAt(i).PointVenteStock_QuantiteProduit : 0),
                                maison = f.Produit_Vendable,
                            };
                            formeModel.Add(m1);
                            i++;
                        }
                        var inv = new InventaireModel()
                        {
                            formes = formeModel,
                            pdv = pointVenteService.findFormulairePointVente((int)pdvId).PointVente_Nom
                        };
                        Inventaire.Add(inv);
                    }
                    foreach (var pack in package)
                    {

                        var formes = produitVendableService.getListFormeProduitsPackage(id, pack.ProduitPackage_ID);
                        var formeModel = new List<FormeApiResultModel>();
                        foreach (var f in formes)
                        {
                            var QteForme = produitVendableService.getFormeDetails(f.FormeProduit_ID, pdvId);
                            var m1 = new FormeApiResultModel
                            {
                                Forme_ID = f.FormeProduit_ID,
                                Forme_ProduitPackageId = f.FormeProduit_ProduitPackageId,
                                Forme_CoutDeRevient = f.FormeProduit_CoutDeRevient,
                                Forme_Libelle = f.FormeProduit_Libelle,
                                Forme_QteStock = (QteForme != null ? QteForme.FormeDetails_Quantite : 0),
                                package = f.ProduitPackage

                            };
                            formeModel.Add(m1);
                        }
                        var inv = new InventaireModel()
                        {
                            formes = formeModel
                        }; Inventaire.Add(inv);

                    }
                    foreach (var p in pret)
                    {
                        var formes = produitVendableService.getListFormeProduitsPret(id, p.ProduitPretConsomer_ID);
                        var formeModel = new List<FormeApiResultModel>();
                        foreach (var f in formes)
                        {
                            var QteForme = produitVendableService.getListProduitsStockesPdv(id, f.FormeProduit_ID).FirstOrDefault();
                            var m1 = new FormeApiResultModel
                            {
                                Forme_ID = f.FormeProduit_ID,
                                Forme_ProduitPretConsomerId = f.FormeProduit_ProduitPretId,
                                Forme_CoutDeRevient = f.FormeProduit_CoutDeRevient,
                                Forme_Libelle = f.FormeProduit_Libelle,
                                Forme_QteStock = (QteForme != null ? QteForme.PdV_ProduitsPret_Quantite : 0),
                                prets = f.Produit_PretConsomer

                            };
                            formeModel.Add(m1);
                        }
                        var inv = new InventaireModel()
                        {
                            formes = formeModel
                        }; Inventaire.Add(inv);

                    }
                }

            }
            else
            {
                var pdv = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                var stocks = pointVenteService.getStockPointVente(id, (int)pdv);
                var stock = stocks.GroupBy(p => new { key = p.PointVenteStock_ProduitID, item = p.Produit_Vendable.ProduitVendable_Designation, photo = p.Produit_Vendable.ProduitVendable_GrandePhoto, unite = p.Produit_Vendable.Unite_Mesure });
                var pp = pointVenteService.getStockProduitPackage(id, (int)pdv);
                var package = pp.Where(p => p.formes.Where(f => f.FormeProduit_PrixVente > 0).Count() > 0);
                var pret = produitVendableService.getListProduitConsomable(id, null, null).Where(p => p.formes.Count() > 0);
                foreach (var s in stock)
                {
                    var formesAll = pointVenteService.getStockPointVenteByProduit(id, (int)pdv, s.Key.key);
                    var formes = formesAll.Select(p => p.Forme_Produit);
                    var i = 0;
                    var formeModel = new List<FormeApiResultModel>();
                    foreach (var f in formes)
                    {
                        var m1 = new FormeApiResultModel
                        {
                            Forme_Libelle = f.FormeProduit_Libelle,
                            Forme_QteStock = (formesAll.Count() > 0 ? formesAll.ElementAt(i).PointVenteStock_QuantiteProduit : 0),
                            maison = f.Produit_Vendable,
                        };
                        formeModel.Add(m1);
                        i++;
                    }
                    var inv = new InventaireModel()
                    {
                        formes = formeModel
                    };
                    Inventaire.Add(inv);
                }
                foreach (var pack in package)
                {

                    var formes = produitVendableService.getListFormeProduitsPackage(id, pack.ProduitPackage_ID);
                    var formeModel = new List<FormeApiResultModel>();
                    foreach (var f in formes)
                    {
                        var QteForme = produitVendableService.getFormeDetails(f.FormeProduit_ID, pdvId);
                        var m1 = new FormeApiResultModel
                        {
                            Forme_ID = f.FormeProduit_ID,
                            Forme_ProduitPackageId = f.FormeProduit_ProduitPackageId,
                            Forme_CoutDeRevient = f.FormeProduit_CoutDeRevient,
                            Forme_Libelle = f.FormeProduit_Libelle,
                            Forme_QteStock = (QteForme != null ? QteForme.FormeDetails_Quantite : 0),
                            package = f.ProduitPackage

                        };
                        formeModel.Add(m1);
                    }
                    var inv = new InventaireModel()
                    {
                        formes = formeModel
                    }; Inventaire.Add(inv);

                }
                foreach (var p in pret)
                {
                    var formes = produitVendableService.getListFormeProduitsPret(id, p.ProduitPretConsomer_ID);
                    var formeModel = new List<FormeApiResultModel>();
                    foreach (var f in formes)
                    {
                        var QteForme = produitVendableService.getListProduitsStockesPdv(id, f.FormeProduit_ID).FirstOrDefault();
                        var m1 = new FormeApiResultModel
                        {
                            Forme_ID = f.FormeProduit_ID,
                            Forme_ProduitPretConsomerId = f.FormeProduit_ProduitPretId,
                            Forme_CoutDeRevient = f.FormeProduit_CoutDeRevient,
                            Forme_Libelle = f.FormeProduit_Libelle,
                            Forme_QteStock = (QteForme != null ? QteForme.PdV_ProduitsPret_Quantite : 0),
                            prets = f.Produit_PretConsomer

                        };
                        formeModel.Add(m1);
                    }
                    var inv = new InventaireModel()
                    {
                        formes = formeModel
                    }; Inventaire.Add(inv);

                }
            }
            int recsCount = Inventaire.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var model = Inventaire.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View("~/Views/ProduitVendable/Ventes/Inventaire.cshtml", model);
        }
        /*[HttpPost]
        public List<FormeApiResultModel> getFormesIventaire(FormApiBody formApiBody)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

            if (formApiBody.Type == "Maison")
            {
                var pdv = authentificationRepository.Geturser(_userManager.GetUserId(HttpContext.User)).PointVente_ID;
                var formesAll = pointVenteService.getStockPointVenteByProduit(AboId, (int)pdv, formApiBody.Id);
                var formes = formesAll.Select(p => p.Forme_Produit);
                var i = 0;
                var model = new List<FormeApiResultModel>();
                foreach (var f in formes)
                {
                    var m1 = new FormeApiResultModel
                    {
                        Forme_ID = f.FormeProduit_ID,
                        Forme_ProduitVendableId = f.FormeProduit_ProduitID,
                        Forme_CoutDeRevient = f.FormeProduit_CoutDeRevient,
                        Forme_Libelle = f.FormeProduit_Libelle,
                        Forme_QteStock = (formesAll.Count() > 0 ? formesAll.ElementAt(i).PointVenteStock_QuantiteProduit : 0),
                    };
                    model.Add(m1);
                    i++;
                }
                return model;
            }
            if (formApiBody.Type == "Package")
            {
                var formes = produitVendableService.getListFormeProduitsPackage(AboId, formApiBody.Id);
                var model = new List<FormeApiResultModel>();
                foreach (var f in formes)
                {
                    var QteForme = produitVendableService.getFormeDetails(f.FormeProduit_ID);
                    var m1 = new FormeApiResultModel
                    {
                        Forme_ID = f.FormeProduit_ID,
                        Forme_ProduitPackageId = f.FormeProduit_ProduitPackageId,
                        Forme_CoutDeRevient = f.FormeProduit_CoutDeRevient,
                        Forme_Libelle = f.FormeProduit_Libelle,
                        Forme_QteStock = (QteForme != null ? QteForme.FormeDetails_Quantite : 0)

                    };
                    model.Add(m1);
                }
                return model;
            }
            else
            {
                var formes = produitVendableService.getListFormeProduitsPret(AboId, formApiBody.Id);
                var model = new List<FormeApiResultModel>();
                foreach (var f in formes)
                {
                    var QteForme = produitVendableService.getListProduitsStockesPdv(AboId, f.FormeProduit_ID).FirstOrDefault();
                    var m1 = new FormeApiResultModel
                    {
                        Forme_ID = f.FormeProduit_ID,
                        Forme_ProduitPretConsomerId = f.FormeProduit_ProduitPretId,
                        Forme_CoutDeRevient = f.FormeProduit_CoutDeRevient,
                        Forme_Libelle = f.FormeProduit_Libelle,
                        Forme_QteStock = (QteForme != null ? QteForme.PdV_ProduitsPret_Quantite : 0)
                    };
                    model.Add(m1);
                }
                return model;
            }

        }*/
        [HttpPost]
        public async Task<bool> DeletePositionVente(int id, int code)
        {
            var result = await produitVendableService.deletePositionVente(id, code);
            return result;
        }
        [HttpPost]
        public async Task<bool> DeleteSalle(int id, int code)
        {
            var result = await produitVendableService.deleteSalle(id, code);
            return result;
        }
        public List<ClotureJourneeModel> getClotureFilter(int? pdv, string date)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            if (date == null)
                date = "";
            var clotures = new List<ClotureJourneeModel>();
            if (User.IsInRole("Client"))
            {
                ViewData["Pdv"] = new SelectList(pointVenteService.getListPointVenteActive(id), "PointVente_Id", "PointVente_Nom");
                clotures = (List<ClotureJourneeModel>)venteProduitsService.getListClotureFiltered(id, date, pdv);
            }
            if (User.IsInRole("Responsable_Vente"))
            {
            }
            var tuple = clotures;
            return tuple;
        }
        /*  public IActionResult filter(Tuple<List<ClotureJourneeModel>, bool>)
          {
              var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
              if (date == null)
                  date = "";
              var clotures = new List<ClotureJourneeModel>();
              var reste = false;
              if (User.IsInRole("Client"))
              {
                  ViewData["Pdv"] = new SelectList(pointVenteService.getListPointVenteActive(id), "PointVente_Id", "PointVente_Nom");
                  clotures = (List<ClotureJourneeModel>)venteProduitsService.getListClotureFiltered(id, date, pdv);
              }
              if (User.IsInRole("Responsable_Vente"))
              {
              }
              var tuple = new Tuple<List<ClotureJourneeModel>, bool>(clotures, reste);
              return tuple;
          }*/
        [HttpPost]
        public async Task<bool> UpdatePrixForme(int formeID, decimal prix)
        {
            var redirect = await produitVendableService.updateFormePrix(formeID, prix);
            if (redirect)
                return true;
            else
                return false;
        }
        [HttpPost]
        public async Task<bool> updateFicheForme(int formeID, int produitID, decimal qteForme)
        {
            var redirect = await produitVendableService.updateFicheForme(formeID, produitID, qteForme);
            if (redirect)
                return true;
            else
                return false;
        }
        public async Task<int> GetPlanifications(int Id)
        {
            var qte = await produitVendableService.GetPlanificationsForStock(Id);
            return qte;
        }
        public async Task<int> GetPlanificationsAtelier(int Id)
        {
            var qte = await produitVendableService.GetPlanificationsForAtelier(Id);
            return qte;
        }
        [HttpPost]
        public SelectList GetPositions(int Id)
        {
            var list = pointVenteService.getListPositionVente(Id);
            SelectList pos = new SelectList(list, "PositionVente_Id", "PositionVente_Libelle");
            return pos;
        }
        public IActionResult getDemandesPrets(string date, int? point, string etat)
        {
            var current = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            if (date == null || date == "null")
                date = "";
            if (etat == null || etat == "null")
                etat = "";
            if (User.IsInRole("Chef_Patissier"))
            {
                var demandes = produitVendableService.getListDemandesPretAtelier(current, id, date, point, etat);
                ViewData["ZoneStockage_LieuStockageId"] = new SelectList(zoneStockageService.getListLieuStockage(id, 1), "LieuStockag_Id", "LieuStockag_Nom");
                return View("~/Views/ProduitVendable/ProduitConsomable/DemandesProduits/Demandes.cshtml", demandes);
            }
            if (User.IsInRole("Gerant_de_stock"))
            {
                var demandes = produitVendableService.getListDemandesPretStock(current, id, date, point, etat);
                ViewData["production"] = new SelectList(produitVendableService.getListAteliers(id), "Atelier_ID", "Atelier_Nom");
                return View("~/Views/ProduitVendable/ProduitConsomable/DemandesProduits/Demandes.cshtml", demandes);
            }
            return NotFound();
        }
        public IActionResult getDetailsDemandes(int Id)
        {
            var current = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            if (User.IsInRole("Chef_Patissier"))
            {
                var details = produitVendableService.getListDetailsDemandesPretAtelier(Id, id, current);
                return View("~/Views/ProduitVendable/ProduitConsomable/DemandesProduits/DetailsDemande.cshtml", details);
            }
            if (User.IsInRole("Gerant_de_stock"))
            {
                var details = produitVendableService.getListDetailsDemandesPretStock(Id, id, current);
                return View("~/Views/ProduitVendable/ProduitConsomable/DemandesProduits/DetailsDemande.cshtml", details);
            }
            return NotFound();
        }
        [HttpPost]
        public List<DemandePret_DetailsModel> getDetailsPret(int Id)
        {
            var current = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var details = produitVendableService.getListDetailsDemandesPretStock(Id, id, current);
            return details.ToList();
        }
        public Task<bool> AccepterDemandePrets(int demandeID, List<BonViewModel> listBon)
        {
            var current = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            if (User.IsInRole("Chef_Patissier"))
            {
                return produitVendableService.ValiderDemande(demandeID, current);
            }
            return produitVendableService.AccepterDemandePrets(demandeID, current, listBon);

        }
        public IActionResult getProduitPretAtelier(int? produitId, int pg = 1)
        {
            var current = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
           // var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var query = produitVendableService.getListProduitPretAtelier(current, produitId);
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;
            int recsCount = query.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var model = query.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            if (User.IsInRole("Chef_Patissier"))
            {
               // var produits = produitVendableService.getListProduitPretAtelier(current, produitId);
                return View("~/Views/ProduitVendable/ProduitConsomable/ListePorduitEnStock.cshtml", model);
            }
            return NotFound();
        }
        public IActionResult getProduitMagasin(int pg=1)
        {
            if (User.IsInRole("Chef_Patissier"))
            {
                // var produits = produitVendableService.getListProduitMaisonStock(current, id);
                var current = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                var query = produitVendableService.getListProduitMaisonStock(current, id);
                const int pageSize = 15;
                if (pg < 1)
                    pg = 1;
                int recsCount = query.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var model = query.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                return View("~/Views/ProduitVendable/ListePorduitEnStock.cshtml", model);
            }
            return NotFound();
        }
        [HttpPost]
        public ViewBonSortieModel getQteEnMagasin(int matiereId,int uniteId)
        {
            var current = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var model =  produitVendableService.getQteEnMagasin(current, matiereId, uniteId);
            return model;
        }
        [HttpPost]
        public ViewBonSortieModel getQuantiteEnMagasin(int matiereId)
        {
            var current = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var qte =  produitVendableService.getQuantiteEnMagasin(current, matiereId);
            return qte;
        }
        [HttpPost]
        public ViewBonSortieModel getQuantiteAtelier(int matiereId)
        {
            var qte =  produitVendableService.getQuantiteAtelier(matiereId);
            return qte;
        }
        [HttpPost]
        public ViewBonSortieModel getQuantiteStock(int matiereId)
        {
            var qte =  produitVendableService.getQuantiteStock(matiereId);
            return qte;
        }
        public IActionResult getMatiereStock(int atelierID, int pg=1)
        {
            var current = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;
           
            if (User.IsInRole("Client"))
            {
                var query = produitVendableService.getListMatiereStock(atelierID, id);
                int recsCount = query.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var model = query.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                //var matieres = produitVendableService.getListMatiereStock(atelierID, id);
                return View("~/Views/Clients/ListeMatiereEnStock.cshtml", model);
            }
            if (User.IsInRole("Chef_Patissier"))
            {
                var query = produitVendableService.getListMatiereStock(current, id);
                int recsCount = query.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var model = query.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                return View("~/Views/Clients/ListeMatiereEnStock.cshtml", model);
            }
            return NotFound();
        }
        public IActionResult FicheTechPackage(int? Id)
        {
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            return View("~/Views/ProduitVendable/ProduitPackage/FicheTechniquePackage.cshtml", produitVendableService.getListeFichePackage(aboId, Id));
            
        }
        public IActionResult DetailsFicheTechPackage(int Id)
        {
            return View("~/Views/ProduitVendable/ProduitPackage/DetailsFicheTechniquePackage.cshtml", produitVendableService.getDetailsFichePackage(Id));            
        }
        [HttpPost]
        public List<SousProduitsViewModel> getPackageFormeDetails(int packageId, int formeID)
        {
            var current = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var list = produitVendableService.getPackageFormeDetails(packageId, formeID, current);
            return list;
        }
        [HttpPost]
        public async Task<bool> DemanderComposants(List<SousProduitsViewModel> sousProds, decimal qte, int lieuId)
        {
            foreach(var s in sousProds)
                s.SousProduit_Quantite *= qte;
            var current = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var redirect = await produitVendableService.DemanderComposants(sousProds, current, lieuId, aboId);
            return redirect;
        }
        public async Task<bool> ValiderApprovisionnementPackage(int Approvisionnement_Id)
        {
            int pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            return await produitVendableService.ValiderApprovisionnementPackage(Approvisionnement_Id, pdvId);

        }
        public IActionResult ApprovisionnementPackage()
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["PdV"] = new SelectList(pointVenteService.getListPointVenteActive(Id), "PointVente_Id", "PointVente_Nom");
            ViewData["production"] = new SelectList(produitVendableService.getListAteliers(Id), "Atelier_ID", "Atelier_Nom");
            return View("~/Views/ProduitVendable/ProduitPackage/Approvisionnement.cshtml");
        }
        [HttpPost]
        public SelectList produitPackList(int PointVenteId)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var list = pointVenteService.getListProduitPack(PointVenteId, AboId)
                .Where(s => s.ProduitPackAtelier_Quantite > 0)
                .GroupBy(p => new { key = p.ProduitPackage.ProduitPackage_ID, item = p.ProduitPackage.ProduitPackage_Designation });
            SelectList produitSelect = new SelectList(list, "Key.key", "Key.item");
            return produitSelect;
        }
        [HttpPost]
        public SelectList formeMagasin(int Id)
        {
            //var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            int pdv = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            SelectList formeProduit = new SelectList(produitVendableService.FindFormesProduitPackEnMagasin(Id,pdv), "ProduitPackAtelier_ID", "Forme_Produit.FormeProduit_Libelle");
            return formeProduit;
        }
        [HttpPost]
        public decimal getQtePack(int produitPackAtelierID)
        {
            //var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            //var current = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var qte = produitVendableService.FindformulairePackageMagasin(produitPackAtelierID).ProduitPackAtelier_Quantite;
            return qte;
        }
        [HttpPost]
        public async Task<bool> Approvisionnementpackage(Approvisionnement_ProduitPackageModel approModel)
        {
            //var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            approModel.ApprovisionnementProduitPackage_AbonnementId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            if (User.IsInRole("Chef_Patissier"))
            {
                var userAtelier = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                approModel.ApprovisionnementProduitPackage_AtelierID = userAtelier;
            }
            //approModel.prod = produitVendableService.GetProduitAppros(Id, approModel.Approvisionnement_AtelierID, approModel.Approvisionnement_ProduitId)
            //    .Select(p => p.ProduitAppro_Id)
            //    .FirstOrDefault();
            //approModel.ut = _userManager.GetUserId(HttpContext.User);
            var redirect = await produitVendableService.ApprovisionnementProduitPackage(approModel);
            return redirect;
        }
        public IActionResult ListeApprovisionnementPackage(int? atelierID, int? pdvId, string date)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            if (date == null || date == "null")
                date = "";
            if (User.IsInRole("Chef_Patissier"))
            {
                atelierID = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            }
            if (User.IsInRole("Responsable_Vente"))
            {
                pdvId = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            }
            ViewData["PdV"] = new SelectList(pointVenteService.getListPointVenteActive(Id), "PointVente_Id", "PointVente_Nom");
            ViewData["production"] = new SelectList(produitVendableService.getListAteliers(Id), "Atelier_ID", "Atelier_Nom");
            var model = produitVendableService.getListApprovisionnement(Id, atelierID, pdvId, date);
            return View("~/Views/ProduitVendable/ProduitPackage/ListeApprovisionnementPackage.cshtml",model);
        }
        public IActionResult DeclarationPertePret()
        {
            var abo = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var userLieu = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            ViewData["Unite"] = new SelectList(zoneStockageService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
            var list = produitVendableService.getListProduitPretAtelier(userLieu, null).GroupBy(p => new { key = p.Forme_Produit.FormeProduit_ProduitPretId, item = p.Forme_Produit.Produit_PretConsomer.ProduitPretConsomer_Designation });
            ViewData["produits"] = new SelectList(list, "Key.key", "Key.item");
            return View("~/Views/ProduitVendable/ProduitConsomable/DeclarationPertePret.cshtml");
        }
        [HttpPost]
        public SelectList formePretsMagasin(int Id)
        {
            //var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            int pdv = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            SelectList formeProduit = new SelectList(produitVendableService.findFormesPret_Atelier(pdv,Id), "ProduitPretAtelier_ID", "Forme_Produit.FormeProduit_Libelle");
            return formeProduit;
        }
        [HttpPost]
        public ProduitPret_AtelierModel getQtepret(int produitPretAtelierID)
        {
            var qte = produitVendableService.FindformulairePretMagasin(produitPretAtelierID);
            return qte;
        }
        [HttpPost]
        public async Task<IActionResult> DeclarationPertePret(Perte_PretModel perte_PretModel)
        {
            var abo = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var userLieu = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            perte_PretModel.PertePret_AbonnmentID = abo;
            perte_PretModel.PertePret_AtelierID = userLieu;
            perte_PretModel.PertePret_DateCreation = DateTime.Now;
            var redirect = await produitVendableService.DeclarationPerte(perte_PretModel);
            if (redirect)
            {
                TempData["Creation"] = "OK";
                return Redirect("/ProduitVendable/ListeDeclarationsPret");
            }
            else
            {
                ViewData["Unite"] = new SelectList(zoneStockageService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
                var list = produitVendableService.getListProduitPretAtelier(userLieu, abo).GroupBy(p => new { key = p.Forme_Produit.FormeProduit_ProduitPretId, item = p.Forme_Produit.Produit_PretConsomer.ProduitPretConsomer_Designation });
                ViewData["produits"] = new SelectList(list, "Key.key", "Key.item");
                return View("~/Views/ProduitVendable/ProduitConsomable/DeclarationPertePret.cshtml");
            }
        }
        public IActionResult ListeDeclarationsPret(int? atelierID, string date)
        {
            var abo = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var userLieu = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            if (date == null || date == "null")
                date = "";
            //ViewData["MatiereStockage"] = new SelectList(produitVendableService.getListMatiereStock(userLieu, abo), "MatiereStockAtelier_ID", "Matiere_Premiere.MatierePremiere_Libelle");
            ViewData["production"] = new SelectList(produitVendableService.getListAteliers(abo), "Atelier_ID", "Atelier_Nom");
            IEnumerable<Perte_PretModel> model;
            if (User.IsInRole("Client"))
            {
                model = produitVendableService.getListPertesPrets(abo, atelierID, date);
            }
            else
            {
                model = produitVendableService.getListPertesPrets(abo, userLieu, date);

            }
            return View("~/Views/ProduitVendable/ProduitConsomable/ListeDeclarationsPret.cshtml", model);
        }
        public IActionResult DemanderPret()
        {
            var abo = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["lieuId"] = new SelectList(zoneStockageService.getListLieuStockage(abo, 1), "LieuStockag_Id", "LieuStockag_Nom");
            ViewData["produitconso"] = new SelectList(produitVendableService.getListProduitConsomable(abo, null, null), "ProduitPretConsomer_ID", "ProduitPretConsomer_Designation");
            return View("~/Views/ProduitVendable/ProduitConsomable/DemandesProduits/DemanderPrets.cshtml");
        }
        [HttpPost]
        public async Task<bool> DemanderPret(Demande_PretModel demande_PretModel)
        {
            var abo = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var userLieu = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            demande_PretModel.DemandePret_AbonnementID = abo;
            demande_PretModel.DemandePret_AtelierID = userLieu;
            demande_PretModel.DemandePret_DateCreation = DateTime.Now;
            var redirect = await produitVendableService.DemanderPret(demande_PretModel);
            return redirect;
        }
        [Authorize(Roles = "Client")]
        public IActionResult AjouterProduitBase()
        {
            var aboid = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["ProduitVendable_FormStockageId"] = new SelectList(produitVendableService.getListFormeSotckage(aboid), "FormStockage_Id", "FormStockage_Libelle");
            ViewData["ProduitVendable_UniteMesureId"] = new SelectList(produitVendableService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");
            return View("~/Views/ProduitVendable/ProduitdeBase/Ajouter.cshtml");
        }
        [HttpPost]

        public async Task<IActionResult> AjouterProduitBase([FromForm] ProduitBaseModel produitModel)
        {
            produitModel.ProduitBase_AbonnementID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

            var redirect = await produitVendableService.CreateProduitBase(produitModel);
            if (redirect)
            {
                TempData["Creation"] = "OK";
                return Redirect("/ProduitVendable/ListeProduitBase");
            }
            else
            {
                var aboid = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                ViewData["ProduitVendable_FormStockageId"] = new SelectList(produitVendableService.getListFormeSotckage(aboid), "FormeStockage_Id", "FormeStockage_Libelle");
                ViewData["ProduitVendable_UniteMesureId"] = new SelectList(produitVendableService.getListUniteMesure(), "UniteMesure_Id", "UniteMesure_Libelle");

                return View();
            }
        }
        [Authorize(Roles = "Client")]
        public IActionResult ListeProduitBase(int? formeId, int pg=1)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["ProduitVendable_FormStockageId"] = new SelectList(produitVendableService.getListFormeSotckage(Id), "FormeStockage_Id", "FormeStockage_Libelle");
            var query = produitVendableService.getListProduitBase(Id, formeId);
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;
            int recsCount = query.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var model = query.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View("~/Views/ProduitVendable/ProduitdeBase/ListeDesProduits.cshtml", model);
        }
        [Authorize(Roles = "Client")]

        public IActionResult ConsulterBase(int Id)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            return View("~/Views/ProduitVendable/ProduitdeBase/ConsulterBase.cshtml", produitFicheTechniqueService.findFormulaireFicheBase(Id, AboId).OrderByDescending(p => p.FicheTechniqueProduitBase_InUse));
        }
        [HttpPost]
        public SelectList GetUniteBase(int Id)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            List<Unite_MesureModel> unites = new List<Unite_MesureModel>();
            var unite = produitVendableService.findFormulaireProduitBase(Id, AboId).Unite_Mesure;
            unites.Add(unite);
            var select = new SelectList(unites, "UniteMesure_Id", "UniteMesure_Libelle");
            return select;
            //return produitVendableService.findFormulaireFormeProduit(id);
        }
        [Authorize(Roles = "Chef_Patissier,Client")]

        public IActionResult PlanificationBase()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["PlanificationProduction_ProduitVendableId"] = new SelectList(produitVendableService.getListProduitBase(id,null), "ProduitBase_ID", "ProduitBase_Designation");
            ViewData["ZoneStockage_LieuStockageId"] = new SelectList(zoneStockageService.getListLieuStockage(id, 1), "LieuStockag_Id", "LieuStockag_Nom");
            return View("~/Views/ProduitVendable/ProduitdeBase/PlanificationProduction/Planification.cshtml");
        }



        [HttpPost]
        public async Task<bool> PlanificationBase(PlanificationJourneeBaseModel plan)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            plan.PlanificationJourneeBase_AbonnementID = Id;
            plan.PlanificationJourneeBase_AtelierID = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            plan.PlanificationJourneeBase_UtilisateurId = _userManager.GetUserId(HttpContext.User);
            var result = await produitVendableService.PlanifierBase(plan);
            return result;
        }
        [HttpPost]
        public async Task<int?> PlanificationBaseAuto(int planID)
        {
            var result = await produitVendableService.PlanificationBaseAuto(planID);
            return result;
        }

        public IActionResult ListeDesPlansBase(string etat, int? point, string date, int pg = 1)
        {
            if (date == null || date == "null")
                date = "";
            if (etat == null || etat == "null")
                etat = "";
            const int pageSize = 15;
            if (pg < 1)
                pg = 1;
            if (User.IsInRole("Chef_Patissier"))
            {
                //var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                var plans = produitVendableService.getListPlansBaseAtelier(Id, adresse, etat, point, date);
                int recsCount = plans.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var model = plans.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                //var demandes = produitVendableService.GetDemandes(Id, date, point, adresse);
                //var tuple = new Tuple<IEnumerable<PlanificationJourneeModel>, IEnumerable<DemandeModel>>(plans, demandes);
                ViewData["ZoneStockage_LieuStockageId"] = new SelectList(zoneStockageService.getListLieuStockage(Id, null), "LieuStockag_Id", "LieuStockag_Nom");
                return View("~/Views/ProduitVendable/ProduitdeBase/PlanificationProduction/ListeDesPlans.cshtml", model);
            }
            if (User.IsInRole("Gerant_de_stock"))
            {
                //var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
                var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                ViewData["production"] = new SelectList(produitVendableService.getListAteliers(Id), "Atelier_ID", "Atelier_Nom");
                var plans = produitVendableService.getListPlansBaseAtelier(Id, adresse, etat, point, date);
                int recsCount = plans.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var model = plans.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                return View("~/Views/ProduitVendable/ProduitdeBase/PlanificationProduction/ListeDesPlansGerant.cshtml", model);
            }
            return NotFound();



        }
        public IActionResult DetailsPlanBase(int Id)
        {
            var aboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            return View("~/Views/ProduitVendable/ProduitdeBase/PlanificationProduction/DetailsPlan.cshtml", produitVendableService.getDetailPlanBase(aboId, Id));
        }
        [HttpPost]
        public FicheTehcniqueProduitBaseModel GetFicheTechBase(int Id)
        {
            return produitVendableService.GetFicheTechBase(Id);
        }
        [HttpPost]
        public async Task<MatStockFlagModel> AccepterPlanificationBase(int planificationId, List<BonViewModel> ListBons)
        {
            if (User.IsInRole("Chef_Patissier"))
            {
                return  await this.produitVendableService.ValiderPlanificationBase(planificationId);
            }
            var adresse = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var validePar = _userManager.GetUserAsync(HttpContext.User).Result.Nom_Complet;
            var res = await produitVendableService.AccepterPlanificationBase(planificationId, adresse, ListBons, validePar);
            return res;

        }
        [HttpPost]
        public SelectList produitListeBase(int Id)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var list = produitVendableService.getDetailPlanProduitListBase(AboId, Id);
            SelectList secteurSelect = new SelectList(list, "PlanificationProductionBase_Id", "ProduitBase.ProduitBase_Designation");
            return secteurSelect;
        }
        [HttpPost]
        public decimal QtePrevueBase(int Id, int produitId)
        {
            var AboId = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var qte = produitVendableService.getDetailByPlanBase(AboId, Id, produitId).Select(p => p.PlanificationProductionBase_QuantitePrevue).FirstOrDefault();
            return qte;
        }
        [HttpPost]
        public Task<bool?> ModifierQteProduiteBase(int id, List<PlanificationdeProductionBaseModel> plans)
        {
            return produitVendableService.updateFormulaireQteProduitebase(id, plans);
        }
        public IActionResult getProduitBaseMagasin()
        {
            var current = Convert.ToInt32(HttpContext.Session.GetString("mysession"));
            var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            if (User.IsInRole("Chef_Patissier"))
            {
                var produits = produitVendableService.getListProduitBaseStock(current, id);
                return View("~/Views/ProduitVendable/ProduitdeBase/ListePorduitEnStock.cshtml", produits);
            }
            return NotFound();
        }
        public IActionResult ConsulterFicheBase(int Id)
        {
            var aboID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var produits = produitVendableService.GetFicheTech_ProduitBases(Id, aboID);
            return View("~/Views/FicheTechnique/ConsulterFicheBase.cshtml", produits);
        }
        public IActionResult ConsulterProdBase(int Id)
        {
            var produits = produitVendableService.getProdBasePlan(Id);
            return View("~/Views/ProduitVendable/PlanificationProduction/ConsulterProdBase.cshtml", produits);
        }
        [HttpPost]
        public async Task<bool> AnnulerPlan(int ID)
        {
            var res = await produitVendableService.AnnulerPlanification(ID);
            return res;
        }
        [HttpPost]
        public async Task<bool> RetourStockPlan(int ID)
        {
            var res = await produitVendableService.RetourStockPlan(ID);
            return res;
        }
        [HttpPost]
        public Task<ActionResult> GeneratePDf(int id)
        {
            try
             {
                var aboID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                var ficheBaseListe = new List<FicheTehcniqueProduitBaseModel>();
                var produit = produitVendableService.GetFicheTech(id);
                foreach(var item in produit.FicheTech_ProduitBase)
                {
                    var fichebase = produitFicheTechniqueService.FindFicheTechniqueBaseBYPordBase(item.ProduitBase.ProduitBase_ID, aboID);
                    ficheBaseListe.Add(fichebase);
                }
                var model = new PDF_FicheTechnique()
                {
                    fichetechnique = produit,
                    fichetechniqueBase = ficheBaseListe
                };

                Controller controller = this;
                Task<ActionResult> lFileResult = ConvertHTmlToPdf.ConvertCurrentPageToPdf(controller, model, "PDF", "Fiche_technique_" + produit.Produit_Vendable.ProduitVendable_Designation);
                return lFileResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
   


}