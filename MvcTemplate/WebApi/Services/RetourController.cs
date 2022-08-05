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
    public class RetourController : ControllerBase
    {
        private readonly IProduitVendableService produitVendableService;
        private readonly IPointVenteService pointVenteService;
        private readonly IAuthentificationRepository authentificationRepository;
        private readonly IFamilleProduitService familleProduitService;
        private readonly IVenteProduitsService venteProduitsService;

        public RetourController(IProduitVendableService produitVendableService, IPointVenteService pointVenteService, IAuthentificationRepository authentificationRepository, IVenteProduitsService venteProduitsService, IFamilleProduitService familleProduitService)
        {
            this.produitVendableService = produitVendableService;
            this.familleProduitService = familleProduitService;
            this.pointVenteService = pointVenteService;
            this.authentificationRepository = authentificationRepository;
            this.venteProduitsService = venteProduitsService;
        }
        [HttpPost]
        [Route("getProduitVendables")]
        public JsonResult GetProduitVendables([FromBody] FormeCategApi formApiBody)
        {
            var id = authentificationRepository.GetUserAboIdById(formApiBody.userId);
            var pdv = authentificationRepository.Geturser(formApiBody.userId).Position_Vente.Point_Vente.PointVente_Id;
            var stocks = pointVenteService.getStockPointVente(id, pdv).Where(p => p.Produit_Vendable.Sous_Famille.SousFamille_ID == formApiBody.sousCategorieID);
            var stock = stocks.GroupBy(p => new { key = p.PointVenteStock_ProduitID, item = p.Produit_Vendable.ProduitVendable_Designation, photo = p.Produit_Vendable.ProduitVendable_GrandePhoto, unite = p.Produit_Vendable.Unite_Mesure });
            var pp = pointVenteService.getStockProduitPackage(id, pdv).Where(p => p.formes.Count() > 0 && p.ProduitPackage_FamilleID == formApiBody.sousCategorieID);
            var package = pp.Where(p => p.formes.Where(f => f.FormeProduit_PrixVente > 0).Count() > 0);
            var prt = produitVendableService.getListProduitConsomable(id,null,null);
            var pret = prt.Where(p => p.ProduitPretConsomer_FamilleProduit == formApiBody.sousCategorieID && p.formes.Count() > 0);
            var model = new List<VenteApiModel>();
            foreach (var s in stock)
            {
                var v1 = new VenteApiModel
                {
                    ID = s.Key.key,
                    Designation = s.Key.item,
                    Image = s.Key.photo,
                    Type = "vendable",
                    Unite = s.Key.unite
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
                    Type = "package",
                    Unite = pack.Unite_Mesure

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
                    Type = "consomable",
                    Unite = p.Unite_Mesure
                };
                model.Add(v1);
            }
            return new JsonResult(model);
        }


        [HttpPost]
        [Route("getFormes")]
        public JsonResult getFormes([FromBody] FormApiBody formApiBody)
        {
            var AboId = authentificationRepository.GetUserAboIdById(formApiBody.userId);
            var pdv = authentificationRepository.Geturser(formApiBody.userId).Position_Vente.Point_Vente.PointVente_Id;

            if (formApiBody.Type == "vendable")
            {
                var formesAll = pointVenteService.getStockPointVente(AboId, pdv).Where(p => p.PointVenteStock_ProduitID == formApiBody.Id);
                var formes = formesAll.Select(p => p.Forme_Produit);
                var i = 0;
                var model = new List<FormeApiResultModel>();
                foreach (var f in formes)
                {
                    //var prix = produitVendableService.getListPrix(f.FormeProduit_ID);
                    var m1 = new FormeApiResultModel
                    {
                        Forme_ID = f.FormeProduit_ID,
                        Forme_ProduitVendableId = f.FormeProduit_ProduitID,
                        Forme_CoutDeRevient = f.FormeProduit_CoutDeRevient,
                        Forme_Libelle = f.FormeProduit_Libelle,
                        Forme_QteStock = (formesAll.Count() > 0 ? formesAll.ElementAt(i).PointVenteStock_QuantiteProduit : 0),
                        //ListPrix = prix
                        Forme_PrixVente = f.FormeProduit_PrixVente

                    };
                    model.Add(m1);
                    i++;
                }
                return new JsonResult(model);
            }
            else if (formApiBody.Type == "package")
            {
                var formes = produitVendableService.getListFormeProduitsPackage(AboId, formApiBody.Id);
                var model = new List<FormeApiResultModel>();
                foreach (var f in formes)
                {
                    var QteForme = produitVendableService.getFormeDetails(f.FormeProduit_ID, pdv);
                    //var prix = produitVendableService.getListPrix(f.FormeProduit_ID);
                    var m1 = new FormeApiResultModel
                    {
                        Forme_ID = f.FormeProduit_ID,
                        Forme_ProduitPackageId = f.FormeProduit_ProduitPackageId,
                        Forme_CoutDeRevient = f.FormeProduit_CoutDeRevient,
                        Forme_Libelle = f.FormeProduit_Libelle,
                        //ListPrix = prix,
                        Forme_QteStock = (QteForme != null ? QteForme.FormeDetails_Quantite : 0),
                        Forme_PrixVente = f.FormeProduit_PrixVente


                    };
                    model.Add(m1);
                }
                return new JsonResult(model);
            }
            else
            {
                var formes = produitVendableService.getListFormeProduitsPret(AboId, formApiBody.Id);
                var model = new List<FormeApiResultModel>();
                foreach (var f in formes)
                {
                    //var prix = produitVendableService.getListPrix(f.FormeProduit_ID);
                    var QteForme = produitVendableService.getListProduitsStockesPdv(AboId, f.FormeProduit_ID).FirstOrDefault();
                    var m1 = new FormeApiResultModel
                    {
                        Forme_ID = f.FormeProduit_ID,
                        Forme_ProduitPretConsomerId = f.FormeProduit_ProduitPretId,
                        Forme_CoutDeRevient = f.FormeProduit_CoutDeRevient,
                        Forme_Libelle = f.FormeProduit_Libelle,
                        //ListPrix = prix,
                        Forme_QteStock = (QteForme != null ? QteForme.PdV_ProduitsPret_Quantite : 0),
                        Forme_PrixVente = f.FormeProduit_PrixVente

                    };
                    model.Add(m1);
                }
                return new JsonResult(model);
            }
        }

        [HttpGet("getCategorieRetour/{userId}", Name = "GetCategorieRetour")]
        public JsonResult GetCategorieRetour(string userId)
        {
            var Id = authentificationRepository.Geturser(userId).Abonnement_ID;
            return new JsonResult(familleProduitService.getListFamilles((int)Id));
        }

        // POST: api/retour/retour
        [HttpPost]
        [Route("validerRetour")]
        public async Task<JsonResult> ValiderRetour([FromBody] FactureMobileCallModel factureMobileCall)
        {
            var aboId = authentificationRepository.GetUserAboIdById(factureMobileCall.userId);
            var posId = authentificationRepository.Geturser(factureMobileCall.userId).PositionVente_ID;
            //var pdvId = authentificationRepository.Geturser(factureMobileCall.userId).Position_Vente.PositionVente_PointVenteId;
            if (factureMobileCall.isPerte == true)
            {
                List<PerteDetailsModel> details = new List<PerteDetailsModel>();
                foreach (var facture in factureMobileCall.factureMobiles)
                {
                    var coutRevient = produitVendableService.findFormulaireFormeProduit(facture.FormeProduitId).FormeProduit_CoutDeRevient;
                    PerteDetailsModel detail = new PerteDetailsModel()
                    {
                        PerteDetails_CoutDeRevient = coutRevient,
                        PerteDetails_Quantite = facture.Quantite,
                        PerteDetails_UniteVenteID = facture.UniteId,
                        PerteDetails_FormeID = facture.FormeProduitId,
                        PerteDetails_DatePerte = DateTime.Now,
                    };

                    details.Add(detail);
                }
                PerteModel perteModel = new PerteModel()
                {
                    Perte_AbonnementID = aboId,
                    Perte_PositionVenteID = (int)posId,
                    Perte_UtilisateurID = factureMobileCall.userId,
                    Perte_DateCreation = DateTime.Now,
                    Perte_ValeurTotal = details.Sum(p => p.PerteDetails_CoutDeRevient * p.PerteDetails_Quantite),
                    details = details,
                    Perte_DatePerte = DateTime.Now,
                };

                return new JsonResult(await venteProduitsService.CreatePerte(perteModel));
            }
            else
            {
                List<RetourDetailsModel> details = new List<RetourDetailsModel>();
                foreach (var facture in factureMobileCall.factureMobiles)
                {
                    var coutRevient = produitVendableService.findFormulaireFormeProduit(facture.FormeProduitId).FormeProduit_CoutDeRevient;
                    RetourDetailsModel detail = new RetourDetailsModel()
                    {
                        RetourDetails_PrixProduit = coutRevient,
                        RetourDetails_Quantite = facture.Quantite,
                        RetourDetails_UniteVente = facture.UniteId,
                        RetourDetails_FormeID = facture.FormeProduitId,
                        RetourDetails_PrixTotal = facture.Prix,
                        RetourDetails_DateRetour = DateTime.Now,
                    };

                    details.Add(detail);
                }
                RetourProduitsModel retourmodel = new RetourProduitsModel()
                {
                    Retour_AbonnementID = aboId,
                    Retour_PositionVenteID = (int)posId,
                    Retour_UtilisateurID = factureMobileCall.userId,
                    Retour_DateCreation = DateTime.Now,
                    Retour_PrixTotal = details.Sum(p => p.RetourDetails_PrixTotal),
                    retourDetails = details,
                };

                return new JsonResult(await venteProduitsService.RetourProduit(retourmodel));
            }


        }
        [HttpGet("getCommande/{userId}", Name = "GetCommande")]
        public JsonResult GetCommande(string userId)
        {
            var Id = authentificationRepository.Geturser(userId).Position_Vente.PositionVente_PointVenteId;
            return new JsonResult(venteProduitsService.getListCommandesNonPayeeApi((int)Id));
        }
        [HttpPost]
        [Route("commandeFilter")]
        public JsonResult GetCommandeFiltered([FromBody] FilterCommandeApi filterApi)
        {
            var Id = authentificationRepository.Geturser(filterApi.userId).Position_Vente.PositionVente_PointVenteId;
            if(filterApi.date == null)
                filterApi.date = "";
            if (filterApi.nomDemandeur == null)
                filterApi.nomDemandeur = "";

            var commandes = venteProduitsService.getListCommandesFiltered((int)Id, filterApi.date, filterApi.nomDemandeur);
            return new JsonResult(commandes);
        }

        [HttpPost]
        [Route("commande")]
        public async Task<JsonResult> paiementCommande([FromBody] PaiementComandeApi paiementCommande)
        {
            var posId = authentificationRepository.Geturser(paiementCommande.userID).PositionVente_ID;
            var paiementModel = new Commande_PaiementModel
            {
                CommandePaiement_UtilisateurID = paiementCommande.userID,
                CommandePaiement_Montant = (decimal)paiementCommande.montant,
                CommandePaiement_CommandeID = paiementCommande.commandeID,
                CommandePaiement_DatePaiement = DateTime.Now,
                CommandePaiement_PositionVenteID = (int)posId,
                
            };
            var res = await venteProduitsService.PaiementCommande(paiementModel);
            return new JsonResult(res);
        } 
        
        [HttpPost]
        [Route("livrer")]
        public async Task<JsonResult> livrerCommande([FromBody] PaiementComandeApi paiementCommande)
        {
            var res = false;
            if(paiementCommande.flag == true)
            {
                var posId = authentificationRepository.Geturser(paiementCommande.userID).PositionVente_ID;
                var paiementModel = new Commande_PaiementModel
                {
                    CommandePaiement_UtilisateurID = paiementCommande.userID,
                    CommandePaiement_Montant = (decimal)paiementCommande.montant,
                    CommandePaiement_CommandeID = paiementCommande.commandeID,
                    CommandePaiement_DatePaiement = DateTime.Now,
                    CommandePaiement_PositionVenteID = (int)posId,
                    CommandePaiement_ModePaiementID = (int)paiementCommande.modePaiement,
                };
                res = await venteProduitsService.PaiementCommande(paiementModel);
            }
            else
                res = await venteProduitsService.LivrerCommande(paiementCommande.commandeID, paiementCommande.userID);
            return new JsonResult(res);
        }
        [HttpGet("getPaiement/{commandeID}", Name = "getPaiement")]
        public JsonResult getPaiement(int commandeID)
        {
            return new JsonResult(venteProduitsService.getListPaiementCommande(commandeID));
        } 
        [HttpPost]
        [Route("getRetrait")]
        public JsonResult getRetrait([FromBody] FilterCommandeApi filterApi)
        {
            var aboId = authentificationRepository.GetUserAboIdById(filterApi.userId);
            var posVente = authentificationRepository.Geturser(filterApi.userId).Position_Vente;
            var pdvId = posVente.PositionVente_PointVenteId;
            if (String.IsNullOrEmpty(filterApi.date))
                filterApi.date = "";
            var retraits = venteProduitsService.getListRetrait(aboId, pdvId, filterApi.date);
            var soldeD = venteProduitsService.GetSoleDebit(posVente.PositionVente_Id, "", aboId);
            var soldec = venteProduitsService.GetSoleCredit(posVente.PositionVente_Id, "", aboId);
            var alim = venteProduitsService.GetAlimentation(posVente.PositionVente_Id, "", aboId);
            var listRetraitApiModel = new ListeRetraitApiModel()
            {
                RetraitCaisseList = retraits,
                Montant = soldeD + alim - soldec,
                RetraitCaisseType = venteProduitsService.getListRetraitType(),
            };
            return new JsonResult(listRetraitApiModel);
        }
        [HttpPost]
        [Route("Retrait")]
        public async Task<JsonResult> Retrait([FromBody] RetraitCaisseModelApi retraitModel)
        {
            var aboID = authentificationRepository.GetUserAboIdById(retraitModel.userID);
            var posVente = authentificationRepository.Geturser(retraitModel.userID).PositionVente_ID;
            var retrait = new RetraitCaisseModel()
            {
                RetraitCaisse_TypeRetraitID = retraitModel.typeRetrait,
                RetraitCaisse_Montant = retraitModel.montantRetrait,
                RetraitCaisse_Motif = retraitModel.motifRetrait,
                RetraitCaisse_UtilisateurID = retraitModel.userID,
                RetraitCaisse_AbonnementID = aboID,
                RetraitCaisse_PositionVenteID = (int)posVente,
                RetraitCaisse_DateCreation = DateTime.Now,
                RetraitCaisse_FlagCloture = 0,
            };

            var res  = await venteProduitsService.RetraitCaisse(retrait);
            return new JsonResult(res);
        }
        [HttpPost]
        [Route("UpdatePDV")]
        public async Task<JsonResult> UpdatePDV(UpdateInvApi invApi)
        {
            var res = await venteProduitsService.UpdateProduitsPDV(invApi.prodsQte, invApi.pdvID);
            return new JsonResult(res);
        }

    }
}
