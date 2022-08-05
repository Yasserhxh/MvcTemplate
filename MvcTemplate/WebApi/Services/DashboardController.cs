using Domain.Authentication;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepositories;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly IProduitVendableService produitVendableService;
        private readonly IPointVenteService pointVenteService;
        private readonly IAuthentificationRepository authentificationRepository;
        private readonly IFamilleProduitService familleProduitService;
        private readonly IVenteProduitsService venteProduitsService;
        private readonly IGestionMouvementService gestionMouvementService;
        public DashboardController(IProduitVendableService produitVendableService, IPointVenteService pointVenteService, IGestionMouvementService gestionMouvementService, IAuthentificationRepository authentificationRepository, IVenteProduitsService venteProduitsService, IFamilleProduitService familleProduitService)
        {
            this.produitVendableService = produitVendableService;
            this.familleProduitService = familleProduitService;
            this.pointVenteService = pointVenteService;
            this.gestionMouvementService = gestionMouvementService;
            this.authentificationRepository = authentificationRepository;
            this.venteProduitsService = venteProduitsService;
        }
        // GET: api/Dashboard/getPointvente
        [HttpGet("getPointvente/{userId}", Name = "getPointvente")]
        public JsonResult getPointvente(string userId)
        {
            var aboId = authentificationRepository.GetUserAboIdById(userId);
            return new JsonResult(pointVenteService.getListPointVenteActiveApi(aboId));
        }
        
        
        [HttpPost]
        [Route("getdashboard")]
        public JsonResult getdashboard([FromBody] InventaireFilterModel invModel)
        {
            var id = authentificationRepository.GetUserAboIdById(invModel.userId);
            var listMatieres = new List<MatierePremiereDashboardModel>();
            var matiereCount = gestionMouvementService.getListMatiereStockageAdmin(id)
                .GroupBy(u => u.Matiere_Premiere, (key, items) => new
                {
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
            if (invModel.date == null)
                invModel.date = "";
            var soldec = venteProduitsService.GetSoleCreditBypvApi(invModel.pdv, invModel.date, id);

            var soldeD = venteProduitsService.GetSoleDebitBypvApi(invModel.pdv, invModel.date, id);
            var alim = venteProduitsService.GetAlimentationBypv(invModel.pdv, invModel.date, id);

            var Ventes = venteProduitsService.getListDetailsVentesApi(invModel.pdv, invModel.date, id, null)
                .GroupBy(u => u.Forme_Produit, (key, items) => new
                {
                    forme = key,
                    Quantite = items.Sum(u => u.VenteDetails_Quantite).ToString()
                });
            
            var recettes = venteProduitsService.getListRecettes( id, invModel.date,invModel.pdv);
            var depenses = venteProduitsService.getListDepenses( id, invModel.date,invModel.pdv);
            var dashboard = new DashboardModel
            {
                MatieresPremieres = listMatieres,
                Recette = soldeD ,
                Depense = soldec,
                Alimentation = alim,
                ListRecettes = recettes.ToList(),
                ListDepenses = depenses.ToList()

            };
            dashboard.Ventes = recettes.Where(p => p.TypeRecette == "Ventes").Sum(p => p.MontantTotal);
            dashboard.Commandes = recettes.Where(p => p.TypeRecette == "Commandes").Sum(p => p.MontantTotal);
            dashboard.Retours = depenses.Where(p => p.TypeDepense == "Retour des produits").Sum(p => p.MontantTotal);
            dashboard.Retraits = depenses.Where(p => p.TypeDepense == "Retrait de caisse").Sum(p => p.MontantTotal);

            foreach (var v in Ventes)
            {
                var produitDesi = "";
                var produit = v.forme.Produit_Vendable;
                if (produit != null)
                {
                    produitDesi = produit.ProduitVendable_Designation;
                    dashboard.Labels.Add(produitDesi);
                }
                else
                {
                    var package = v.forme.ProduitPackage;
                    if (package != null)
                    {
                        produitDesi = package.ProduitPackage_Designation;
                        dashboard.Labels.Add(produitDesi);
                    }
                    else
                    {
                        var pret = v.forme.Produit_PretConsomer;
                        produitDesi = pret.ProduitPretConsomer_Designation;
                        dashboard.Labels.Add(produitDesi);
                    }
                }
                dashboard.Data.Add(v.Quantite);
            }

            var stok = pointVenteService.getStockPointVente(id, (int)invModel.pdv);
            var stc = stok.GroupBy(p => new { key = p.PointVenteStock_ProduitID});
            var pp = pointVenteService.getStockProduitPackage(id, (int)invModel.pdv);
            var pakage = pp.Where(p => p.formes.Where(f => f.FormeProduit_PrixVente > 0).Count() > 0);
            var prets = produitVendableService.getListProduitConsomableEnStock(id, (int)invModel.pdv);
            var Inventaire = new List<InventaireApiModel>();
            foreach (var s in stc)
            {
                var formesAll = pointVenteService.getStockPointVenteByProduit(id, (int)invModel.pdv, s.Key.key);
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
                    var QteForme = produitVendableService.getFormeDetails(f.FormeProduit_ID, invModel.pdv);
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
               foreach(var vente in Ventes)
                {
                    if(vente.forme.FormeProduit_Libelle == v.Inventaire_FormeLibelle)
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
                dashboard.TauxDeVente =0;

            return new JsonResult(dashboard);
        }
        [HttpPost]
        [Route("getIventaireBypdv")]
        public JsonResult getIventaireBypdv( [FromBody] InventaireFilterModel invModel)
        {
            var id = authentificationRepository.GetUserAboIdById(invModel.userId);
            var stocks = pointVenteService.getStockPointVente(id, (int)invModel.pdv);
            var stock = stocks.GroupBy(p => new { key = p.PointVenteStock_ProduitID, item = p.Produit_Vendable.ProduitVendable_Designation, photo = p.Produit_Vendable.ProduitVendable_GrandePhoto, unite = p.Produit_Vendable.Unite_Mesure });
            var pp = pointVenteService.getStockProduitPackage(id, (int)invModel.pdv);
            var package = pp.Where(p => p.formes.Where(f => f.FormeProduit_PrixVente > 0).Count() > 0);
            var pret = produitVendableService.getListProduitConsomableEnStock(id,(int)invModel.pdv);
            var Inventaire = new List<InventaireApiModel>();
            foreach (var s in stock)
            {
                var formesAll = pointVenteService.getStockPointVenteByProduit(id, (int)invModel.pdv, s.Key.key);
                var formes = formesAll.Select(p => p.Forme_Produit);
                var i = 0;
               // var formeModel = new List<InventaireApiModel>();
                foreach (var f in formes)
                {
                    var m1 = new InventaireApiModel
                    {
                        Inventaire_FormeLibelle = f.FormeProduit_Libelle,
                        Inventaire_FormeID = f.FormeProduit_ID,
                        Inventaire_QuantiteEnStock = (formesAll.Count() > 0 ? formesAll.ElementAt(i).PointVenteStock_QuantiteProduit : 0),
                        Inventaire_ProduitLibelle = f.Produit_Vendable.ProduitVendable_Designation,
                        Inventaire_Unite = f.Produit_Vendable.Unite_Mesure.UniteMesure_Libelle,
                        Inventaire_QuantiteMinimale = f.Produit_Vendable.ProduitVendable_StockMinimum,
                    };
                    Inventaire.Add(m1);
                    i++;
                }

            }
            foreach (var pack in package)
            {

                var formes = produitVendableService.getListFormeProduitsPackage(id, pack.ProduitPackage_ID);
                var formeModel = new List<InventaireApiModel>();
                foreach (var f in formes)
                {
                    var QteForme = produitVendableService.getFormeDetails(f.FormeProduit_ID, invModel.pdv);
                    var m1 = new InventaireApiModel
                    {
                        Inventaire_FormeID = f.FormeProduit_ID,
                        Inventaire_FormeLibelle = f.FormeProduit_Libelle,
                        Inventaire_QuantiteEnStock = (QteForme != null ? QteForme.FormeDetails_Quantite : 0),
                        Inventaire_ProduitLibelle = f.ProduitPackage.ProduitPackage_Designation,
                        Inventaire_Unite = f.ProduitPackage.Unite_Mesure.UniteMesure_Libelle,
                        Inventaire_QuantiteMinimale = 0

                    };
                    Inventaire.Add(m1);
                }
               
                //Inventaire.Add(inv);

            }
            foreach (var p in pret)
            {
                var formes = produitVendableService.getListFormeProduitsPret(id, p.PdV_ProduitsPret_ProduitConsomableId);
                var formeModel = new List<InventaireApiModel>();
                foreach (var f in formes)
                {
                    var QteForme = produitVendableService.getListProduitsStockesPdv(id, f.FormeProduit_ID).FirstOrDefault();
                    var m1 = new InventaireApiModel
                    {
                        Inventaire_FormeID = f.FormeProduit_ID,
                        Inventaire_FormeLibelle = f.FormeProduit_Libelle,
                        Inventaire_QuantiteEnStock = (QteForme != null ? QteForme.PdV_ProduitsPret_Quantite : 0),
                        Inventaire_ProduitLibelle = f.Produit_PretConsomer.ProduitPretConsomer_Designation,
                        Inventaire_Unite = f.Produit_PretConsomer.Unite_Mesure.UniteMesure_Libelle,
                        Inventaire_QuantiteMinimale = f.Produit_PretConsomer.ProduitPretConsomer_StockMinimun,

                    };
                    Inventaire.Add(m1);
                }
                
               // Inventaire.Add(inv);

            }
            return new JsonResult(Inventaire);
        }
/*        [HttpPost]
        [Route("getCloture")]
        public JsonResult getClotureBypdv( [FromBody] InventaireFilterModel invModel)
        {
            var id = authentificationRepository.GetUserAboIdById(invModel.userId);
            var listClotures = (List<ClotureJourneeModel>)venteProduitsService.getListCloture(id);
            var clotures = listClotures.GroupBy(u => u.Position_Vente.Point_Vente.PointVente_Nom, (key, items) => new { pointvente = key, recette = items.Sum(u => u.ClotueJournee_SoldeDebit), depenses = items.Sum(u => u.ClotueJournee_SoldeCredit), montantreel = items.Sum(u => u.ClotueJournee_Montant), alimentations = items.Sum(u => u.ClotueJournee_Alimentation) });
            return new JsonResult(clotures);
        }*/
        [HttpPost]
        [Route("getClotureFilter")]
        public JsonResult getClotureFilter( [FromBody] InventaireFilterModel invModel)
        {
            var id = authentificationRepository.GetUserAboIdById(invModel.userId);
            if (invModel.date == null)
                invModel.date = "";
            var caisses = pointVenteService.getListPositionVente((int)invModel.pdv);
            //var clotures = venteProduitsService.getListClotureFiltered(id, invModel.date, invModel.pdv);
            var CaissesPdvApi = new List<CaissesPdvApi>();
            foreach(var c in caisses)
            {
                var soldeD = venteProduitsService.GetSoleDebit(c.PositionVente_Id, invModel.date, id);
                var soldec = venteProduitsService.GetSoleCredit(c.PositionVente_Id, invModel.date, id);
                var alimentation = venteProduitsService.GetAlimentation(c.PositionVente_Id, invModel.date, id);
                var CaissePdv = new CaissesPdvApi()
                {
                    Caisse_Id = c.PositionVente_Id,
                    Caisse_Nom = c.PositionVente_Libelle,
                    Caisse_Depenses = soldec,
                    Caisse_Recette = soldeD,
                    Caisse_Allimentation = alimentation
                };
                if (soldeD == 0 && soldec == 0 && alimentation == 0)
                {
                    CaissePdv.Caisse_MontatReel = 0;
                    CaissePdv.Caisse_ClotureStatut = "Clôturée";
                }
                else
                {
                    CaissePdv.Caisse_MontatReel = 0;
                    CaissePdv.Caisse_ClotureStatut = "Non Clôturée";
                }
               
                CaissesPdvApi.Add(CaissePdv);
            }

            return new JsonResult(CaissesPdvApi);
        }
        [HttpPost]
        [Route("cloturerCaisse")]
        public async Task<JsonResult> CloturerCaisseAsync([FromBody] CloturerApiModel model)
        {
            var id = authentificationRepository.GetUserAboIdById(model.UserId);
            if (model.Date == null)
                model.Date = "";
            var flagCheck = await venteProduitsService.UpdateVente(model.CaisseId, model.Date);
            var cloture = new ClotureJourneeModel()
            {
                ClotueJournee_PositionVenteID = model.CaisseId,
                ClotueJournee_AbonnementID = id,
                ClotueJournee_UtilisateurID = model.UserId,
                ClotueJournee_Alimentation = model.Allimentation,
                ClotueJournee_DateCreation = DateTime.Now,
                ClotueJournee_SoldeDebit = model.Recette,
                ClotueJournee_SoldeCredit = model.Depenses,
                ClotueJournee_Montant = model.MontantReel,
                ClotueJournee_Valide = 0,
            };


            return new JsonResult(await venteProduitsService.ClotureJournee(cloture));
        }
        [HttpPost]
        [Route("validerCloture")]
        public async Task<JsonResult> validerCloture([FromBody] List<int> caisses)
        {
            return new JsonResult(await venteProduitsService.ValiderCloture(caisses));
        }
        [HttpPost]
        [Route("listClotures")]
        public JsonResult listClotures([FromBody] InventaireFilterModel invModel)
        {
            var id = authentificationRepository.GetUserAboIdById(invModel.userId);
            if (invModel.date == null)
                invModel.date = "";
            //var caisses = pointVenteService.getListPositionVente((int)invModel.pdv);
            var clotures = venteProduitsService.getListClotureFiltered(id, invModel.date, invModel.pdv);
            var CaissesPdvApi = new List<CaissesPdvApi>();
            foreach(var cl in clotures)
            {
                var caisseApi = new CaissesPdvApi()
                {
                    Caisse_Id = cl.ClotueJournee_PositionVenteID,
                    Caisse_Nom = cl.Position_Vente.PositionVente_Libelle,
                    Caisse_Depenses = cl.ClotueJournee_SoldeCredit,
                    Caisse_Recette = cl.ClotueJournee_SoldeDebit,
                    Caisse_Allimentation = cl.ClotueJournee_Alimentation,
                    Caisse_MontatReel = cl.ClotueJournee_Montant,

                };
                CaissesPdvApi.Add(caisseApi);
            }

            return new JsonResult(CaissesPdvApi);
        }
        [HttpPost]
        [Route("allimentationView")]
        public JsonResult allimentationView([FromBody] InventaireFilterModel invModel)
        {
            var id = authentificationRepository.GetUserAboIdById(invModel.userId);
            if (invModel.date == null)
                invModel.date = "";
            var caisses = pointVenteService.getListPositionVente((int)invModel.pdv);
            //var clotures = venteProduitsService.getListClotureFiltered(id, invModel.date, invModel.pdv);
            var CaissesPdvApi = new List<CaissesPdvApi>();
            foreach (var c in caisses)
            {
                var soldeD = venteProduitsService.GetSoleDebit(c.PositionVente_Id, invModel.date, id);
                var soldec = venteProduitsService.GetSoleCredit(c.PositionVente_Id, invModel.date, id);
                var alimentation = venteProduitsService.GetAlimentation(c.PositionVente_Id, invModel.date, id);
                var CaissePdv = new CaissesPdvApi()
                {
                    Caisse_Id = c.PositionVente_Id,
                    Caisse_Nom = c.PositionVente_Libelle,
                    Caisse_Depenses = soldec,
                    Caisse_Recette = soldeD,
                    Caisse_Allimentation = alimentation
                };
                CaissesPdvApi.Add(CaissePdv);
            }
            return new JsonResult(CaissesPdvApi);
        }
        [HttpPost]
        [Route("allimenterCaisse")]
        public async Task<JsonResult> allimenterCaisseAsync([FromBody] CloturerApiModel model)
        {
            var id = authentificationRepository.GetUserAboIdById(model.UserId);
            if (model.Date == null)
                model.Date = "";
            //var flagCheck = await venteProduitsService.UpdateVente(model.CaisseId, model.Date);
            var alilmentation = new AllimentationCaisseModel()
            {
                AllimentationCaisse_PositionVenteID = model.CaisseId,
                AllimentationCaisse_UtilsateurID = model.UserId,
                AllimentationCaisse_Montant = model.Allimentation,
                AllimentationCaisse_DateCreation = DateTime.Now,
                AllimentationCaisse_AbonnementID = id,
            };
            var pdv = pointVenteService.findFormulairePositionVente(model.CaisseId).PositionVente_PointVenteId;
            alilmentation.AllimentationCaisse_PointVenteID = pdv;
            return new JsonResult(await venteProduitsService.AlimentationCaisse(alilmentation));
        }
    }
}
