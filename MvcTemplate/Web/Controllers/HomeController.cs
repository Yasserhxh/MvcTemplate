using Domain.Authentication;
using Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.IRepositories;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPointVenteService pointVenteService;
        private readonly IVenteProduitsService venteProduitsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthentificationRepository authentificationRepository;
        private readonly IProduitVendableService produitVendableService;
        private readonly IGestionMouvementService gestionMouvementService;
        private readonly IZoneStockageService zoneStockageService;
        private readonly IAbonnement_ClientService abonnement_ClientService ;
        public HomeController(IPointVenteService pointVenteService, UserManager<ApplicationUser> userManager, IAuthentificationRepository authentificationRepository,
            IProduitVendableService produitVendableService, IGestionMouvementService gestionMouvementService, IVenteProduitsService venteProduitsService, IZoneStockageService zoneStockageService, IAbonnement_ClientService abonnement_ClientService)
        {
            this.authentificationRepository = authentificationRepository;
            this.pointVenteService = pointVenteService;
            this.gestionMouvementService = gestionMouvementService;
            this.produitVendableService = produitVendableService;
            this.venteProduitsService = venteProduitsService;
            this.zoneStockageService = zoneStockageService;
            this.abonnement_ClientService = abonnement_ClientService;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            if (User.IsInRole("Administrateur") == true)
                return RedirectToAction("ListeClient", "Clients");
            if (User.IsInRole("Gerant_de_stock") == true)
                return RedirectToAction("ListeZoneStockage", "Stockages");
            if(User.IsInRole("Administrateur") == true)
                return RedirectToAction("Utilisateurs", "Authentification");
            if (User.IsInRole("Chef_Patissier") == true)
                return RedirectToAction("ListeDesPlans", "ProduitVendable");
            if (User.IsInRole("Position_Vente") == true)
            {
                var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
                ViewData["mode"] = new SelectList(pointVenteService.getListModePaiement(), "ModePaiement_Id", "ModePaiement_Libelle");
                var pdv = authentificationRepository.Geturser(_userManager.GetUserId(HttpContext.User)).Position_Vente.Point_Vente.PointVente_Id;
                var stock = pointVenteService.getStockPointVente(id, pdv);
                var pp = pointVenteService.getStockProduitPackage(id, pdv).Where(p=>p.formes.Count()>0);
                var package = pp.Where(p => p.formes.Where(f => f.FormeProduit_PrixVente > 0).Count()>0);
                var tuple = new Tuple<IEnumerable<PointVente_StockModel>, IEnumerable<ProduitPackageModel>>(stock, package);
                var prt = produitVendableService.getListProduitConsomable(id,null,null);
                var pret = prt.Where(p => p.formes.Where(f => f.FormeProduit_PrixVente > 0).Count() > 0);
                var model = new IndecVenteModel
                {
                    tuple = tuple,
                    ProduitPret = pret
                };
                return View(model);
            }
            if (User.IsInRole("Responsable_Vente") == true)
            {
                return RedirectToAction("ListeVentes", "ProduitVendable");
            }

            // var modelView = GetDashboard(1, "27/05/2021");
            return RedirectToAction("Dashboard", "Home"); ;

        }

        private DashboardModel GetDashboard(int? pv, string date)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var listMatieres = new List<MatierePremiereDashboardModel>();
            var matiereCount = gestionMouvementService.getListMatiereStockageAdmin(id)
                .GroupBy(u => u.Matiere_Premiere, (key, items) => new {
                    matiere = key,
                    Quantite = items.Sum(u => u.MatierePremiereStokage_QuantiteActuelle),
                    min = items.Sum(p => p.MatierePremiereStokage_StockMinimum),
                    max = items.Sum(p => p.MatierePremiereStokage_StockMaximum)
                });
            foreach (var matiere in matiereCount)
            {
                var stock = gestionMouvementService.getListMatiereStockageAdmin(id)
                    .Where(p => p.MatierePremiereStokage_MatierePremiereId == matiere.matiere.MatierePremiere_Id).FirstOrDefault();
                var matiereDashboard = new MatierePremiereDashboardModel
                {
                    Id = matiere.matiere.MatierePremiere_Id,
                    Libelle = matiere.matiere.MatierePremiere_Libelle,
                    Stock = matiere.Quantite,
                    Unite = matiere.matiere.Unite_Mesure.UniteMesure_Libelle,
                    Min = matiere.min,
                    Max = matiere.max,
                };
                listMatieres.Add(matiereDashboard);
            }

            if (date == null)
                date = "";
            var aboID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            //var soldec = venteProduitsService.GetSoleCreditBypv(pv, date, aboID);

            //var soldeD = venteProduitsService.GetSoleDebitBypv(pv, date, aboID);

            var recettes = venteProduitsService.getListRecettes(aboID, date, pv);
            var depenses = venteProduitsService.getListDepenses(aboID, date, pv);
            var Ventes = venteProduitsService.getListDetailsVentesApi(pv, date, id, null)
                .GroupBy(u => u.Forme_Produit, (key, items) => new
                {
                    forme = key,
                    Quantite = items.Sum(u => u.VenteDetails_Quantite).ToString()
                });


            var dashboard = new DashboardModel
            {
                MatieresPremieres = listMatieres,
                Recette = recettes.Sum(p => p.MontantTotal),
                Depense = depenses.Sum(p => p.MontantTotal),
                ListRecettes = recettes.ToList(),
                ListDepenses = depenses.ToList(),
                Ventes = recettes.Where(p => p.TypeRecette == "Ventes").Sum(p => p.MontantTotal),
                Commandes = recettes.Where(p => p.TypeRecette == "Commandes").Sum(p => p.MontantTotal),
                Retours = depenses.Where(p => p.TypeDepense == "Retour des produits").Sum(p => p.MontantTotal),
                Retraits = depenses.Where(p => p.TypeDepense == "Retrait de caisse").Sum(p => p.MontantTotal),
            };
            foreach (var v in Ventes)
            {
                var produitDesi = "";
                var produit = v.forme.Produit_Vendable;
                if(produit != null)
                {
                    produitDesi = produit.ProduitVendable_Designation + " - "+v.forme.FormeProduit_Libelle;
                    dashboard.Labels.Add(produitDesi);
                    dashboard.Unites.Add(produit.Unite_Mesure.UniteMesure_Libelle);
                }
                else 
                {
                    var package = v.forme.ProduitPackage;
                    if(package != null)
                    {
                        produitDesi = package.ProduitPackage_Designation;
                        dashboard.Labels.Add(produitDesi);
                       dashboard.Unites.Add(package.Unite_Mesure.UniteMesure_Libelle);
                    }
                    else
                    {
                        var pret = v.forme.Produit_PretConsomer;
                        produitDesi = pret.ProduitPretConsomer_Designation;
                        dashboard.Labels.Add(produitDesi);
                        dashboard.Unites.Add(pret.Unite_Mesure.UniteMesure_Libelle);
                    }
                }
                dashboard.Data.Add(v.Quantite);
            }


            if (pv != null)
            {
                var stok = pointVenteService.getStockPointVente(id, (int)pv);
                var stc = stok.GroupBy(p => new { key = p.PointVenteStock_ProduitID });
                var pp = pointVenteService.getStockProduitPackage(id, (int)pv);
                var pakage = pp.Where(p => p.formes.Where(f => f.FormeProduit_PrixVente > 0).Count() > 0);
                var prets = produitVendableService.getListProduitConsomableEnStock(id, (int)pv);
                var Inventaire = new List<InventaireApiModel>();
                foreach (var s in stc)
                {
                    var formesAll = pointVenteService.getStockPointVenteByProduit(id, (int)pv, s.Key.key);
                    var formes = formesAll.Select(p => p.Forme_Produit);
                    var i = 0;
                    foreach (var f in formes)
                    {
                        var m1 = new InventaireApiModel
                        {
                            Inventaire_FormeLibelle = f.FormeProduit_Libelle,
                            Inventaire_QuantiteEnStock = (formesAll.Count() > 0 ? formesAll.ElementAt(i).PointVenteStock_QuantiteProduit : 0),
                        };
                        Inventaire.Add(m1);
                        i++;
                    }

                }
                foreach (var pack in pakage)
                {

                    var formes = produitVendableService.getListFormeProduitsPackage(id, pack.ProduitPackage_ID);
                    var formeModel = new List<InventaireApiModel>();
                    foreach (var f in formes)
                    {
                        var QteForme = produitVendableService.getFormeDetails(f.FormeProduit_ID,pv);
                        var m1 = new InventaireApiModel
                        {
                            Inventaire_FormeLibelle = f.FormeProduit_Libelle,
                            Inventaire_QuantiteEnStock = (QteForme != null ? QteForme.FormeDetails_Quantite : 0),

                        };
                        Inventaire.Add(m1);
                    }
                }
                foreach (var p in prets)
                {
                    var formes = produitVendableService.getListFormeProduitsPret(id, p.PdV_ProduitsPret_ProduitConsomableId);
                    var formeModel = new List<InventaireApiModel>();
                    foreach (var f in formes)
                    {
                        var QteForme = produitVendableService.getListProduitsStockesPdv(id, f.FormeProduit_ID).FirstOrDefault();
                        var m1 = new InventaireApiModel
                        {
                            Inventaire_FormeLibelle = f.FormeProduit_Libelle,
                            Inventaire_QuantiteEnStock = (QteForme != null ? QteForme.PdV_ProduitsPret_Quantite : 0),
                        };
                        Inventaire.Add(m1);
                    }
                }
                List<decimal> listTaux = new List<decimal>();
                foreach (var v in Inventaire)
                {
                    decimal taux = 0;
                    foreach (var vente in Ventes)
                    {
                        if (vente.forme.FormeProduit_Libelle == v.Inventaire_FormeLibelle)
                        {
                            var qte = (v.Inventaire_QuantiteEnStock + Convert.ToDecimal(vente.Quantite));
                            taux = (Convert.ToDecimal(vente.Quantite) * 100) / (decimal)qte;
                        }
                        else
                            taux = 0;
                        listTaux.Add(taux);
                    }
                }
                if (listTaux.Count > 0)
                    dashboard.TauxDeVente = Convert.ToDecimal(listTaux.Average().ToString("N2"));
                else
                    dashboard.TauxDeVente = 0;
            }
            

            return dashboard;
        }

        public IActionResult Parametrage()
        {
            ViewData["data"] = null;
            var id = HttpContext.User.FindFirst("userId").Value;
            if (User.IsInRole("Responsable_Vente"))
            {
                ViewData["data"] = new SelectList(pointVenteService.getListPdvUser(id), "Point_Vente.PointVente_Id", "Point_Vente.PointVente_Nom");
                return View();
            }
            if (User.IsInRole("Gerant_de_stock"))
            {
                ViewData["data"] = new SelectList(zoneStockageService.getListStockUser(id), "Lieu_Stockage.LieuStockag_Id", "Lieu_Stockage.LieuStockag_Nom");
                return View();
            }
            if (User.IsInRole("Chef_Patissier"))
            {
                ViewData["data"] = new SelectList(abonnement_ClientService.getListAtelierUser(id), "Atelier.Atelier_ID", "Atelier.Atelier_Nom");
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
        }

        public IActionResult Dashboard(int? pv, string date)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

            ViewData["PdV"] = new SelectList(pointVenteService.getListPointVenteActive(id), "PointVente_Id", "PointVente_Nom");
            
            if(date != null)
                date = DateTime.Parse(date).ToShortDateString();
            else
                date = "";

            
            DashboardModel dashboard = GetDashboard(pv, date);
            if(date == null)
            {
                dashboard.Date = DateTime.Now;
            }
            else
            {
                if (date == "")
                    dashboard.Date = DateTime.Now;
                else
                    dashboard.Date = DateTime.Parse(date);
            }

            if (pv != null)
                dashboard.PdvId = pv;

            //Products
            var ProductNames = dashboard.Labels.Take(10).ToList();
            var productUnites = dashboard.Unites.Take(10).ToList();

            for (int i = 0; i < ProductNames.Count; i++)
            {
                ProductNames[i] = "'" + ProductNames[i].Replace("'", " ") + " (" + changeUnite(productUnites[i]) + ")" + "'";
            }
            var products = string.Join(",", ProductNames);
            ViewBag.Productname_List = products;

            // Data
            ViewBag.Productname_List = products;
            var ProductCounts = dashboard.Data.Take(10).ToList();
            for (int i = 0; i < ProductCounts.Count; i++)
            {
                ProductCounts[i] = ProductCounts[i].Replace(",", ".");
            }
            var counts = string.Join(",", ProductCounts);
            ViewBag.MobileCount_List = counts;

            return View(dashboard);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public string Confirmer(int id)
        {
            var aboid = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            var logo = authentificationRepository.getUserLogo(aboid);
            HttpContext.Session.SetString("mysession", id.ToString());
            HttpContext.Session.SetString("mylogo", logo);
            if (User.IsInRole("Responsable_Vente"))
            {
                var point = pointVenteService.findFormulairePointVente(id).PointVente_Nom;
                HttpContext.Session.SetString("point", point);
                return "Responsable_Vente";
            }
            if (User.IsInRole("Gerant_de_stock"))
            {

                var point = zoneStockageService.findFormulaireLieuStockage(id).LieuStockag_Nom;
                HttpContext.Session.SetString("point", point); 
                return "Gerant_de_stock"; 
            }
            if (User.IsInRole("Chef_Patissier"))
            {
                var point = abonnement_ClientService.findFormulaireAtelier(id).Atelier_Nom;
                HttpContext.Session.SetString("point", point);
                return "Chef_Patissier"; 
            }

            return "Client";
        }

        private string changeUnite(string unite)
        {
            if (unite == "Kilogramme(s)")
            {
                return "KG";
            }
            else if (unite == "Gramme(s)")
            {
                return "G";
            }
            else if (unite == "Litre(s)")
            {
                return "L";
            }
            else if (unite == "Unité(s)")
            {
                return "Unité";
            }
            else if (unite == "Millilitre(s)")
            {
                return "ML";
            }
            else
                return "";
        }

    }
}
