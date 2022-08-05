using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.IRepositories;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class VenteProduitsRepository : IVenteProduitsRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork unitOfWork;

        public VenteProduitsRepository(ApplicationDbContext db, IUnitOfWork unitOfWork)
        {
            _db = db;
            this.unitOfWork = unitOfWork;
        }
        public async Task<int?> CreatePerte(Perte perte)
        {
            perte.Perte_DateCreation = DateTime.Now;
            await _db.pertes.AddAsync(perte);
            await unitOfWork.Complete();
            var res = await SortieStock_Perte(perte.Perte_ID);
            if (res == null)
            {
                return null;
            }
            else
                return perte.Perte_ID;
           
        }

        public async Task<int?> CreateVente(Vente vente)
        {
            var resv = _db.vente.Where(p => p.Vente_NumeroTicket == vente.Vente_NumeroTicket).FirstOrDefault();
            if (resv != null)
                return resv.Vente_Id;
            else
            {
                vente.Vente_DateCreation = DateTime.Now;
                foreach (var v in vente.Details)
                {

                    v.VenteDetails_AbonnementID = vente.Vente_AbonnementId;
                    v.VenteDetails_DateCreation = DateTime.Now;

                }
                await _db.vente.AddAsync(vente);
                await unitOfWork.Complete();
                var res = await SortieStock_Vente(vente.Vente_Id);
                if (res == null)
                {
                    return null;
                }
                else
                {

                    return vente.Vente_Id;
                }
            }
           
          
        }

        public async Task<int?> SortieStock_Vente(int venteId)
        {
            var vente = _db.vente.Where(p => p.Vente_Id == venteId)
                .Include(p=>p.Point_Vente)
                .Include(p=>p.Details).ThenInclude(p=>p.Forme_Produit).ThenInclude(p=>p.ProduitPackage)
                .Include(p=>p.Details).ThenInclude(p=>p.Forme_Produit).ThenInclude(p=>p.Produit_PretConsomer)
                .Include(p => p.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                .Include(p => p.Details).ThenInclude(p => p.Unite_Mesure).FirstOrDefault();
            foreach(var v in vente.Details)
            {
                if (v.Forme_Produit.Produit_Vendable != null)
                {
                    if (v.Forme_Produit.Produit_Vendable.ProduitVendable_PlanificationFlag == 0)
                    {
                        List<MouvementStock> mouvement = new List<MouvementStock>();
                        var produit = _db.forme_Produits.Where(p => p.FormeProduit_ID == v.VenteDetails_FormeProduitId).Select(p => p.Produit_Vendable).FirstOrDefault();
                        var ficheTech = _db.ficheTechniqueBridges
                            .Where(f => f.FicheTechniqueBridge_ProduitVendableID == produit.ProduitVendable_Id).Where(f=>f.FicheTechniqueBridge_InUse==true)
                            .Include(f => f.Produit_FicheTechnique).Include(f=>f.Fiche_Forme)
                            .FirstOrDefault();
                        var ficheformeQte = ficheTech.Fiche_Forme.AsEnumerable().Where(p => p.FicheForme_FormeProduit == v.VenteDetails_FormeProduitId).FirstOrDefault().FicheForme_Quantite;
                        var pdvstock = _db.pointVente_Stocks.Where(p => p.PointVenteStock_PointVenteID == vente.Vente_PointVenteId)
                            .Where(p => p.PointVenteStock_FormeProduitID == v.VenteDetails_FormeProduitId)
                            .FirstOrDefault();
                        pdvstock.PointVenteStock_QuantiteProduit -= v.VenteDetails_Quantite;
                        _db.Entry(pdvstock).State = EntityState.Modified;
                        foreach (var fich in ficheTech.Produit_FicheTechnique)
                        {
                            var s = _db.matierePremiereStockages
                                .Where(m => m.MatierePremiereStokage_MatierePremiereId == fich.FicheTechnique_MatierePremiereId)
                                .Where(m => m.MatierePremiereStokage_AbonnementId == v.VenteDetails_AbonnementID)
                                //  .Where(m => m.Section_Stockage.Zone_Stockage.Lieu_Stockage.LieuStockag_ParDefaut == 1)
                                .Where(m => m.Section_Stockage.Zone_Stockage.Lieu_Stockage.LieuStockag_Id == vente.Point_Vente.PointVente_StockID)
                                .Include(p => p.Matiere_Premiere)
                                .Include(p => p.Section_Stockage).ThenInclude(p => p.Zone_Stockage).ThenInclude(p => p.Lieu_Stockage)
                                .FirstOrDefault();

                            if (s != null)
                            {
                                var k = _db.mouvements.Where(e => e.MouvementStock_MatierePremiereStokageId == s.MatierePremiereStokage_Id)
                                    .Where(e=>e.MouvementStock_IsActive == 1)
                                    .AsEnumerable();
                                foreach (var mouv in k)
                                {
                                    mouv.MouvementStock_IsActive = 0;
                                    _db.Entry(mouv).State = EntityState.Modified;

                                }
                                if (s.MatierePremiereStokage_QuantiteActuelle > 0)
                                {
                                    /*decimal qte = 0;
                                    if ((fich.FicheTechnique_UniteMesureId == 1 && s.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 2) || (fich.FicheTechnique_UniteMesureId == 4 && s.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 3))
                                    {
                                        qte = (v.VenteDetails_Quantite * fich.FicheTechnique_QuantiteMatiere) / ficheformeQte;
                                        s.MatierePremiereStokage_QuantiteActuelle -= qte / 1000;

                                    }
                                    else if ((fich.FicheTechnique_UniteMesureId == 2 && s.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 1) || (fich.FicheTechnique_UniteMesureId == 3 && s.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 4))
                                    {
                                        qte = (v.VenteDetails_Quantite * fich.FicheTechnique_QuantiteMatiere) / ficheformeQte;
                                        s.MatierePremiereStokage_QuantiteActuelle -=qte * 1000;
                                    }
                                    else
                                    {
                                        qte = (v.VenteDetails_Quantite * fich.FicheTechnique_QuantiteMatiere) / ficheformeQte;
                                        s.MatierePremiereStokage_QuantiteActuelle -= qte;
                                    }*/

                                    decimal qte = 0;
                                    if ((fich.FicheTechnique_UniteMesureId == 1 && s.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 2) || (fich.FicheTechnique_UniteMesureId == 4 && s.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 3))
                                        qte = ((v.VenteDetails_Quantite * fich.FicheTechnique_QuantiteMatiere) / ficheformeQte)/1000;
                                    else if ((fich.FicheTechnique_UniteMesureId == 2 && s.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 1) || (fich.FicheTechnique_UniteMesureId == 3 && s.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 4))
                                        qte = ((v.VenteDetails_Quantite * fich.FicheTechnique_QuantiteMatiere) / ficheformeQte) * 1000;
                                    else
                                        qte = (v.VenteDetails_Quantite * fich.FicheTechnique_QuantiteMatiere) / ficheformeQte;

                                    var qterest = s.MatierePremiereStokage_QuantiteActuelle - qte;
                                    if (qterest < 0)
                                        return null;
                                    else
                                    {
                                        s.MatierePremiereStokage_QuantiteActuelle = qterest;
                                        _db.Entry(s).State = EntityState.Modified;
                                        MouvementStock m = new MouvementStock()
                                        {
                                            MouvementStock_MatierePremiereStokageId = s.MatierePremiereStokage_Id,
                                            MouvementStock_Quantite = qte,
                                            MouvementStock_UniteMesureId = fich.FicheTechnique_UniteMesureId,
                                            MouvementStock_Date = vente.Vente_Date,
                                            MouvementStock_DateSaisie = DateTime.Now,
                                            MouvementStock_DateReception = DateTime.Now,
                                            MouvementStock_MouvementTypeId = 2,
                                            MouvementStock_IsActive = 1,
                                            MouvementStock_AbonnementId = vente.Vente_AbonnementId,
                                            MouvementStock_DateCreation = DateTime.Now,
                                        };
                                        mouvement.Add(m);
                                    }
                                }
                                else
                                    return null;
                            }
                            else
                                return null;
                        }
                        foreach (var m in mouvement)
                        {
                            await _db.mouvements.AddAsync(m);
                        }
                    }
                    else
                    {
                        var pdvstock = _db.pointVente_Stocks
                            .Where(p => p.PointVenteStock_FormeProduitID == v.VenteDetails_FormeProduitId)
                            .Where(p => p.PointVenteStock_PointVenteID == vente.Vente_PointVenteId).FirstOrDefault();
                        pdvstock.PointVenteStock_QuantiteProduit -= v.VenteDetails_Quantite;
                        _db.Entry(pdvstock).State = EntityState.Modified;
                    }
                }
                else if (v.Forme_Produit.ProduitPackage != null)
                {
                    var formeDetails = _db.formeDetails.Where(p => p.FormeDetails_FormeProduitID == v.Forme_Produit.FormeProduit_ID && p.FormeDetails_PointVenteID== vente.Vente_PointVenteId ).FirstOrDefault();
                    formeDetails.FormeDetails_Quantite -= v.VenteDetails_Quantite;
                    _db.Entry(formeDetails).State = EntityState.Modified;

                }
                else if (v.Forme_Produit.Produit_PretConsomer != null)
                {
                    var produitpret = _db.pdV_ProduitsPrets.Where(p => p.PdV_ProduitsPret_FormeProduitId == v.Forme_Produit.FormeProduit_ID && p.PdV_ProduitsPret_PointVenteId==vente.Vente_PointVenteId).FirstOrDefault();
                    produitpret.PdV_ProduitsPret_Quantite -= v.VenteDetails_Quantite;
                    _db.Entry(produitpret).State = EntityState.Modified;
                }
                else
                    return 0;

            }
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return confirm;
            else
                return null;
        }
        public async Task<int?> SortieStock_Commande(int venteId)
        {
            var vente = _db.commandes.Where(p => p.Commande_Id == venteId)
                .Include(p=>p.Position_Vente).ThenInclude(p=>p.Point_Vente)
                .Include(p=>p.details).ThenInclude(p=>p.Forme_Produit).ThenInclude(p=>p.ProduitPackage)
                .Include(p=>p.details).ThenInclude(p=>p.Forme_Produit).ThenInclude(p=>p.Produit_PretConsomer)
                .Include(p => p.details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                .Include(p => p.details).ThenInclude(p => p.Unite_Mesure).FirstOrDefault();
            foreach(var v in vente.details)
            {
                if (v.Forme_Produit.Produit_Vendable != null)
                {
                    if (v.Forme_Produit.Produit_Vendable.ProduitVendable_PlanificationFlag == 0)
                    {
                        List<MouvementStock> mouvement = new List<MouvementStock>();
                        var produit = _db.forme_Produits.Where(p => p.FormeProduit_ID == v.CommandeDetail_FormeProduitId).Select(p => p.Produit_Vendable).FirstOrDefault();
                        var ficheTech = _db.ficheTechniqueBridges
                            .Where(f => f.FicheTechniqueBridge_ProduitVendableID == produit.ProduitVendable_Id).Where(f=>f.FicheTechniqueBridge_InUse==true)
                            .Include(f => f.Produit_FicheTechnique).Include(f=>f.Fiche_Forme)
                            .FirstOrDefault();
                        var ficheformeQte = ficheTech.Fiche_Forme.AsEnumerable().Where(p => p.FicheForme_FormeProduit == v.CommandeDetail_FormeProduitId).FirstOrDefault().FicheForme_Quantite;
                        var pdvstock = _db.pointVente_Stocks.Where(p => p.PointVenteStock_FormeProduitID == v.CommandeDetail_FormeProduitId && p.PointVenteStock_PointVenteID== (int)v.Commande.Position_Vente.PositionVente_PointVenteId).FirstOrDefault();
                        pdvstock.PointVenteStock_QuantiteProduit -= v.CommandeDetail_Quantite;
                        _db.Entry(pdvstock).State = EntityState.Modified;
                        foreach (var fich in ficheTech.Produit_FicheTechnique)
                        {
                            var s = _db.matierePremiereStockages
                                .Where(m => m.MatierePremiereStokage_MatierePremiereId == fich.FicheTechnique_MatierePremiereId)
                                .Where(m => m.MatierePremiereStokage_AbonnementId == v.CommandeDetail_AbonnementId)
                                //  .Where(m => m.Section_Stockage.Zone_Stockage.Lieu_Stockage.LieuStockag_ParDefaut == 1)
                                .Where(m => m.Section_Stockage.Zone_Stockage.Lieu_Stockage.LieuStockag_Id == vente.Position_Vente.Point_Vente.PointVente_StockID)
                                .Include(p => p.Matiere_Premiere)
                                .Include(p => p.Section_Stockage).ThenInclude(p => p.Zone_Stockage).ThenInclude(p => p.Lieu_Stockage)
                                .FirstOrDefault();

                            if (s != null)
                            {
                                var k = _db.mouvements.Where(e => e.MouvementStock_MatierePremiereStokageId == s.MatierePremiereStokage_Id)
                                    .Where(e=>e.MouvementStock_IsActive == 1)
                                    .AsEnumerable();
                                foreach (var mouv in k)
                                {
                                    mouv.MouvementStock_IsActive = 0;
                                    _db.Entry(mouv).State = EntityState.Modified;

                                }
                                if (s.MatierePremiereStokage_QuantiteActuelle > 0)
                                {
                                    decimal qte = 0;
                                    if ((fich.FicheTechnique_UniteMesureId == 1 && s.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 2) || (fich.FicheTechnique_UniteMesureId == 4 && s.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 3))
                                        qte = ((v.CommandeDetail_Quantite * fich.FicheTechnique_QuantiteMatiere) / ficheformeQte)/1000;
                                    else if ((fich.FicheTechnique_UniteMesureId == 2 && s.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 1) || (fich.FicheTechnique_UniteMesureId == 3 && s.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 4))
                                        qte = ((v.CommandeDetail_Quantite * fich.FicheTechnique_QuantiteMatiere) / ficheformeQte) * 1000;
                                    else
                                        qte = (v.CommandeDetail_Quantite * fich.FicheTechnique_QuantiteMatiere) / ficheformeQte;

                                    var qterest = s.MatierePremiereStokage_QuantiteActuelle - qte;
                                    if (qterest < 0)
                                        return null;
                                    else
                                    {
                                        s.MatierePremiereStokage_QuantiteActuelle = qterest;
                                        _db.Entry(s).State = EntityState.Modified;
                                        MouvementStock m = new MouvementStock()
                                        {
                                            MouvementStock_MatierePremiereStokageId = s.MatierePremiereStokage_Id,
                                            MouvementStock_Quantite = qte,
                                            MouvementStock_UniteMesureId = fich.FicheTechnique_UniteMesureId,
                                            MouvementStock_Date = vente.Commande_Date,
                                            MouvementStock_DateSaisie = DateTime.Now,
                                            MouvementStock_DateReception = DateTime.Now,
                                            MouvementStock_MouvementTypeId = 2,
                                            MouvementStock_IsActive = 1,
                                            MouvementStock_AbonnementId = vente.Commande_AbonnementId,
                                            MouvementStock_DateCreation = DateTime.Now,
                                        };
                                        mouvement.Add(m);
                                    }
                                }
                                else
                                    return null;
                            }
                            else
                                return null;
                        }
                        foreach (var m in mouvement)
                        {
                            await _db.mouvements.AddAsync(m);
                        }
                    }
                    else
                    {
                        var pdvstock = _db.pointVente_Stocks
                            .Where(p => p.PointVenteStock_FormeProduitID == v.CommandeDetail_FormeProduitId && p.PointVenteStock_PointVenteID== v.Commande.Commande_PointVenteId)
                            .FirstOrDefault();
                        pdvstock.PointVenteStock_QuantiteProduit -= v.CommandeDetail_Quantite;
                        _db.Entry(pdvstock).State = EntityState.Modified;
                    }
                }
                else if (v.Forme_Produit.ProduitPackage != null)
                {
                    var formeDetails = _db.formeDetails.Where(p => p.FormeDetails_FormeProduitID == v.Forme_Produit.FormeProduit_ID && p.FormeDetails_PointVenteID == v.Commande.Commande_PointVenteId).FirstOrDefault();
                    formeDetails.FormeDetails_Quantite -= v.CommandeDetail_Quantite;
                    _db.Entry(formeDetails).State = EntityState.Modified;

                }
                else if (v.Forme_Produit.Produit_PretConsomer != null)
                {
                    var produitpret = _db.pdV_ProduitsPrets.Where(p => p.PdV_ProduitsPret_FormeProduitId == v.Forme_Produit.FormeProduit_ID && p.PdV_ProduitsPret_PointVenteId== v.Commande.Commande_PointVenteId).FirstOrDefault();
                    produitpret.PdV_ProduitsPret_Quantite -= v.CommandeDetail_Quantite;
                    _db.Entry(produitpret).State = EntityState.Modified;
                }
                else
                    return 0;

            }
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return confirm;
            else
                return null;
        }
        public async Task<int?> SortieStock_Perte(int preteId)
        {
            var perte = _db.pertes.Where(p => p.Perte_ID == preteId)
                .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                .Include(p => p.details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                .Include(p => p.details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                .Include(p => p.details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                .Include(p => p.details).ThenInclude(p => p.Unite_Mesure).FirstOrDefault();
            foreach (var v in perte.details)
            {
                if (v.Forme_Produit.Produit_Vendable != null)
                {
                    if (v.Forme_Produit.Produit_Vendable.ProduitVendable_PlanificationFlag == 0)
                    {
                        List<MouvementStock> mouvement = new List<MouvementStock>();
                        var produit = _db.forme_Produits.Where(p => p.FormeProduit_ID == v.PerteDetails_FormeID).Select(p => p.Produit_Vendable).FirstOrDefault();
                        var ficheTech = _db.ficheTechniqueBridges
                            .Where(f => f.FicheTechniqueBridge_ProduitVendableID == produit.ProduitVendable_Id).Where(f => f.FicheTechniqueBridge_InUse == true)
                            .Include(f => f.Produit_FicheTechnique).Include(f => f.Fiche_Forme)
                            .FirstOrDefault();
                        var ficheformeQte = ficheTech.Fiche_Forme.AsEnumerable().Where(p => p.FicheForme_FormeProduit == v.PerteDetails_FormeID).FirstOrDefault().FicheForme_Quantite;
                        var pdvstock = _db.pointVente_Stocks.Where(p => p.PointVenteStock_FormeProduitID == v.PerteDetails_FormeID && p.PointVenteStock_PointVenteID == v.Perte.Position_Vente.PositionVente_PointVenteId).FirstOrDefault();
                        pdvstock.PointVenteStock_QuantiteProduit -= v.PerteDetails_Quantite;
                        _db.Entry(pdvstock).State = EntityState.Modified;
                        foreach (var fich in ficheTech.Produit_FicheTechnique)
                        {
                            var s = _db.matierePremiereStockages
                                .Where(m => m.MatierePremiereStokage_MatierePremiereId == fich.FicheTechnique_MatierePremiereId)
                                .Where(m => m.MatierePremiereStokage_AbonnementId == v.Perte.Perte_AbonnementID)
                                //  .Where(m => m.Section_Stockage.Zone_Stockage.Lieu_Stockage.LieuStockag_ParDefaut == 1)
                                .Where(m => m.Section_Stockage.Zone_Stockage.Lieu_Stockage.LieuStockag_Id == v.Perte.Position_Vente.PositionVente_PointVenteId)
                                .Include(p => p.Matiere_Premiere)
                                .Include(p => p.Section_Stockage).ThenInclude(p => p.Zone_Stockage).ThenInclude(p => p.Lieu_Stockage)
                                .FirstOrDefault();

                            if (s != null)
                            {
                                var k = _db.mouvements.Where(e => e.MouvementStock_MatierePremiereStokageId == s.MatierePremiereStokage_Id)
                                    .Where(e => e.MouvementStock_IsActive == 1)
                                    .AsEnumerable();
                                foreach (var mouv in k)
                                {
                                    mouv.MouvementStock_IsActive = 0;
                                    _db.Entry(mouv).State = EntityState.Modified;

                                }
                                if (s.MatierePremiereStokage_QuantiteActuelle > 0)
                                {
                                    decimal qte = 0;
                                    if ((fich.FicheTechnique_UniteMesureId == 1 && s.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 2) || (fich.FicheTechnique_UniteMesureId == 4 && s.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 3))
                                        qte = ((v.PerteDetails_Quantite * fich.FicheTechnique_QuantiteMatiere) / ficheformeQte) / 1000;
                                    else if ((fich.FicheTechnique_UniteMesureId == 2 && s.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 1) || (fich.FicheTechnique_UniteMesureId == 3 && s.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 4))
                                        qte = ((v.PerteDetails_Quantite * fich.FicheTechnique_QuantiteMatiere) / ficheformeQte) * 1000;
                                    else
                                        qte = (v.PerteDetails_Quantite * fich.FicheTechnique_QuantiteMatiere) / ficheformeQte;

                                    var qterest = s.MatierePremiereStokage_QuantiteActuelle - qte;
                                    if (qterest < 0)
                                        return null;
                                    else
                                    {
                                        s.MatierePremiereStokage_QuantiteActuelle = qterest;
                                        _db.Entry(s).State = EntityState.Modified;
                                        MouvementStock m = new MouvementStock()
                                        {
                                            MouvementStock_MatierePremiereStokageId = s.MatierePremiereStokage_Id,
                                            MouvementStock_Quantite = qte,
                                            MouvementStock_UniteMesureId = fich.FicheTechnique_UniteMesureId,
                                            MouvementStock_Date = perte.Perte_DatePerte,
                                            MouvementStock_DateSaisie = DateTime.Now,
                                            MouvementStock_DateReception = DateTime.Now,
                                            MouvementStock_MouvementTypeId = 2,
                                            MouvementStock_IsActive = 1,
                                            MouvementStock_AbonnementId = perte.Perte_AbonnementID,
                                            MouvementStock_DateCreation = DateTime.Now,
                                        };
                                        mouvement.Add(m);
                                    }
                                }
                                else
                                    return null;
                            }
                            else
                                return null;
                        }
                        foreach (var m in mouvement)
                        {
                            await _db.mouvements.AddAsync(m);
                        }
                    }
                    else
                    {
                        var pdvstock = _db.pointVente_Stocks
                            .Where(p => p.PointVenteStock_FormeProduitID == v.PerteDetails_FormeID && p.PointVenteStock_PointVenteID == v.Perte.Position_Vente.PositionVente_PointVenteId)
                            .FirstOrDefault();
                        pdvstock.PointVenteStock_QuantiteProduit -= v.PerteDetails_Quantite;
                        _db.Entry(pdvstock).State = EntityState.Modified;
                    }
                }
                else if (v.Forme_Produit.ProduitPackage != null)
                {
                    var formeDetails = _db.formeDetails.Where(p => p.FormeDetails_FormeProduitID == v.Forme_Produit.FormeProduit_ID && p.FormeDetails_PointVenteID == v.Perte.Position_Vente.PositionVente_PointVenteId).FirstOrDefault();
                    formeDetails.FormeDetails_Quantite -= v.PerteDetails_Quantite;
                    _db.Entry(formeDetails).State = EntityState.Modified;

                }
                else if (v.Forme_Produit.Produit_PretConsomer != null)
                {
                    var produitpret = _db.pdV_ProduitsPrets.Where(p => p.PdV_ProduitsPret_FormeProduitId == v.Forme_Produit.FormeProduit_ID && p.PdV_ProduitsPret_PointVenteId == v.Perte.Position_Vente.PositionVente_PointVenteId).FirstOrDefault();
                    produitpret.PdV_ProduitsPret_Quantite -= v.PerteDetails_Quantite;
                    _db.Entry(produitpret).State = EntityState.Modified;
                }
                else
                    return 0;

            }
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return confirm;
            else
                return null;
        }
        public async Task<int?> Retour_Stock(int retourId)
        {
            var retour = _db.retourProduits.Where(p => p.Retour_Id == retourId)
                .Include(p=>p.Position_Vente).ThenInclude(p=>p.Point_Vente)
                .Include(p=>p.retourDetails).ThenInclude(p=>p.Forme_Produit).ThenInclude(p=>p.ProduitPackage)
                .Include(p=>p.retourDetails).ThenInclude(p=>p.Forme_Produit).ThenInclude(p=>p.Produit_PretConsomer)
                .Include(p => p.retourDetails).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                .Include(p => p.retourDetails).ThenInclude(p => p.Unite_Mesure).FirstOrDefault();
            foreach(var v in retour.retourDetails)
            {
                if (v.Forme_Produit.Produit_Vendable != null)
                {
                    if(v.Forme_Produit.Produit_Vendable.ProduitVendable_PlanificationFlag == 1)
                    {
                        var pdvstock = _db.pointVente_Stocks
                            .Where(p => p.PointVenteStock_FormeProduitID == v.RetourDetails_FormeID && p.PointVenteStock_PointVenteID==(int)v.Retour_Produit.Position_Vente.PositionVente_PointVenteId)
                            .FirstOrDefault();
                        pdvstock.PointVenteStock_QuantiteProduit += v.RetourDetails_Quantite;
                        _db.Entry(pdvstock).State = EntityState.Modified;
                    }
                    else
                    {
                        v.RetourDetails_isPerte = 1;
                        _db.Entry(v).State = EntityState.Modified;

                    }
                }
                else if (v.Forme_Produit.ProduitPackage != null)
                {
                    var formeDetails = _db.formeDetails.Where(p => p.FormeDetails_FormeProduitID == v.Forme_Produit.FormeProduit_ID && p.FormeDetails_PointVenteID == v.Retour_Produit.Position_Vente.PositionVente_PointVenteId).FirstOrDefault();
                    formeDetails.FormeDetails_Quantite += v.RetourDetails_Quantite;
                    _db.Entry(formeDetails).State = EntityState.Modified;

                }
                else if (v.Forme_Produit.Produit_PretConsomer != null)
                {
                    var produitpret = _db.pdV_ProduitsPrets.Where(p => p.PdV_ProduitsPret_FormeProduitId == v.Forme_Produit.FormeProduit_ID && p.PdV_ProduitsPret_PointVenteId == v.Retour_Produit.Position_Vente.PositionVente_PointVenteId).FirstOrDefault();
                    produitpret.PdV_ProduitsPret_Quantite += v.RetourDetails_Quantite;
                    _db.Entry(produitpret).State = EntityState.Modified;
                }
                else
                    return 0;

            }
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return confirm;
            else
                return null;
        } 
        public async Task<int?> Retour_Gratuite(int retourId)
        {
            var retour = _db.gratuites.Where(p => p.Gratuite_ID == retourId)
                .Include(p=>p.details).ThenInclude(p=>p.Forme_Produit).ThenInclude(p=>p.ProduitPackage)
                .Include(p=>p.details).ThenInclude(p=>p.Forme_Produit).ThenInclude(p=>p.Produit_PretConsomer)
                .Include(p => p.details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                .Include(p => p.details).ThenInclude(p => p.Unite_Mesure).FirstOrDefault();
            foreach(var v in retour.details)
            {
                if (v.Forme_Produit.Produit_Vendable != null)
                {
                    if(v.Forme_Produit.Produit_Vendable.ProduitVendable_PlanificationFlag == 1)
                    {
                        var pdvstock = _db.pointVente_Stocks
                            .Where(p => p.PointVenteStock_FormeProduitID == v.GratuiteDetails_FormeID && p.PointVenteStock_PointVenteID== (int)v.Gratuite.Position_Vente.PositionVente_PointVenteId)
                            .FirstOrDefault();
                        pdvstock.PointVenteStock_QuantiteProduit -= v.GratuiteDetails_Quantite;
                        _db.Entry(pdvstock).State = EntityState.Modified;
                    }
                    
                }
                else if (v.Forme_Produit.ProduitPackage != null)
                {
                    var formeDetails = _db.formeDetails.Where(p => p.FormeDetails_FormeProduitID == v.Forme_Produit.FormeProduit_ID && p.FormeDetails_PointVenteID == v.Gratuite.Position_Vente.PositionVente_PointVenteId).FirstOrDefault();
                    formeDetails.FormeDetails_Quantite -= v.GratuiteDetails_Quantite;
                    _db.Entry(formeDetails).State = EntityState.Modified;

                }
                else if (v.Forme_Produit.Produit_PretConsomer != null)
                {
                    var produitpret = _db.pdV_ProduitsPrets.Where(p => p.PdV_ProduitsPret_FormeProduitId == v.Forme_Produit.FormeProduit_ID && p.PdV_ProduitsPret_PointVenteId== (int)v.Gratuite.Position_Vente.PositionVente_PointVenteId).FirstOrDefault();
                    produitpret.PdV_ProduitsPret_Quantite -= v.GratuiteDetails_Quantite;
                    _db.Entry(produitpret).State = EntityState.Modified;
                }
                else
                    return 0;

            }
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return confirm;
            else
                return null;
        }

        public Vente findFormulaireVente(int formulaireVenteId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vente> getListVentes(int Id, string date)
        {
            IEnumerable<Vente> ventes;
            if (date != "")
            {
                ventes = _db.vente.Where(v => v.Vente_AbonnementId == Id && v.Vente_Date.ToShortDateString() == date)
                    .Include(v => v.Point_Vente)
                    .Include(v => v.Mode_Paiement)
                    .Include(v => v.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                    .Include(v => v.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                    .Include(v => v.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                    .AsEnumerable();

            }
            else
            {
                ventes = _db.vente.Where(v => v.Vente_AbonnementId == Id && Convert.ToDateTime(v.Vente_Date).ToString("yyyy-MM-dd") == date)
                    .Include(v => v.Point_Vente)
                    .Include(v => v.Mode_Paiement)
                    .Include(v => v.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                    .Include(v => v.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                    .Include(v => v.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                    .AsEnumerable();
            }

            return ventes;
        }
        public IEnumerable<Vente> getListVentesByPdv(int Id, int pdvid, string date)
        {
            IEnumerable<Vente> ventes;
            if (date != "")
            {
                ventes = _db.vente.Where(v => v.Vente_AbonnementId == Id && Convert.ToDateTime(v.Vente_Date).ToString("yyyy-MM-dd") == date && v.Vente_PointVenteId == pdvid).Include(v => v.Point_Vente)
                                .Include(v => v.Mode_Paiement)
                                .Include(v => v.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                                .Include(v => v.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                                .Include(v => v.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                                .AsEnumerable();
             
            }
            else
            {
                ventes = _db.vente.Where(v => v.Vente_AbonnementId == Id && v.Vente_Date.ToShortDateString() == DateTime.Now.ToShortDateString() && v.Vente_PointVenteId == pdvid).Include(v => v.Point_Vente)
                                .Include(v => v.Mode_Paiement)
                                .Include(v => v.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                                .Include(v => v.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                                .Include(v => v.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                                .AsEnumerable();
            }

            return ventes;
        }
        public decimal getListVentesByPdvPrix(int Id, int pdvid)
        {
            var ventes = _db.vente.Where(v => v.Vente_AbonnementId == Id && v.Vente_Date.ToShortDateString() == DateTime.Now.ToShortDateString() && v.Vente_PointVenteId == pdvid).AsEnumerable();
            return ventes.Sum(p => p.Vente_PrixTotalRemise);
        }
        public async Task<int?> RetourProduit(RetourProduits retour)
        {
            retour.Retour_DateCreation = DateTime.Now;
            await _db.retourProduits.AddAsync(retour);
            await unitOfWork.Complete(); 
            var res = await Retour_Stock(retour.Retour_Id);
            if (res == null)
            {
                return null;
            }
            else
                return retour.Retour_Id;
        }

        public IEnumerable<VenteDetails> getListDetailsVentes(int?pv, string date, int aboId, int? VenteId)
        {
            IEnumerable<VenteDetails> ventes = null;
            if ((pv == null && date == ""))
            {
                if (VenteId == null)
                {
                    ventes = _db.venteDetails.Where(p =>p.VenteDetails_AbonnementID == aboId)
                        .Include(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                        .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                        .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                        .Include(p => p.Unite_Mesure).AsEnumerable();
                }
                else
                {                   
                    ventes = _db.venteDetails.Where(p => p.VenteDetails_VentId == VenteId && p.VenteDetails_AbonnementID == aboId)
                        .Include(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                        .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                        .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                        .Include(p => p.Unite_Mesure).AsEnumerable();
                }

            }
            else if ((pv != null && date != ""))
            {
                ventes = _db.venteDetails.Where(p => p.VenteDetails_AbonnementID == aboId)
                    .Include(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                    .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                    .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                    .Include(p => p.Unite_Mesure).AsEnumerable();
            } 
            else if ((pv == null && date != ""))
            {
                ventes = _db.venteDetails.Where(p => p.VenteDetails_AbonnementID == aboId)            
                    .Include(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                    .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                    .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                    .Include(p => p.Unite_Mesure).AsEnumerable();
            }
            else if ((pv != null && date == ""))
            {
                ventes = _db.venteDetails.Where(p => p.VenteDetails_AbonnementID == aboId)            
                    .Include(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                    .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                    .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                    .Include(p => p.Unite_Mesure).AsEnumerable();
            }
        
            return ventes;
        }
        public IEnumerable<VenteDetails> getListDetailsVentesApi(int?pv, string date, int aboId, int? VenteId)
        {
            IEnumerable<VenteDetails> ventes = null;
            if (VenteId == null)
            {
                if (date != "")
                {
                    if (pv != null)
                    {
                        ventes = _db.venteDetails.Where(p => p.VenteDetails_AbonnementID == aboId && p.Vente.Vente_PointVenteId==pv && Convert.ToDateTime(p.VenteDetails_DateCreation).ToString("dd/MM/yyyy") == date)
                            .Include(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)                    
                            .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                            .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                            .Include(p => p.Unite_Mesure).AsEnumerable();
                    }
                    else
                    {
                        ventes = _db.venteDetails.Where(p => p.VenteDetails_AbonnementID == aboId && Convert.ToDateTime(p.VenteDetails_DateCreation).ToString("dd/MM/yyyy") == date)
                            .Include(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                            .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                            .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                            .Include(p => p.Unite_Mesure).AsEnumerable();
                    }
                }
                else
                {
                    if (pv != null)
                    {
                        ventes = _db.venteDetails.Where(p => p.VenteDetails_AbonnementID == aboId && p.Vente.Vente_PointVenteId == pv && p.VenteDetails_DateCreation.Date == DateTime.Now.Date)
                            .Include(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                            .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                            .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                            .Include(p => p.Unite_Mesure).AsEnumerable();
                    }
                    else
                    {
                        ventes = _db.venteDetails.Where(p => p.VenteDetails_AbonnementID == aboId && p.VenteDetails_DateCreation.Date == DateTime.Now.Date)
                            .Include(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                            .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                            .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                            .Include(p => p.Unite_Mesure).AsEnumerable();
                    }
                }
            }
            else
            {
                ventes = _db.venteDetails.Where(p => p.VenteDetails_VentId == VenteId && p.VenteDetails_DateCreation.Date == DateTime.Now.Date).Include(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                            .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                            .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                            .Include(p => p.Unite_Mesure).AsEnumerable(); ;
            }
        
            return ventes;
        }

        public IEnumerable<RetourProduits> getListRetours(int Id, int pdvId,string date)
        {
            /* var query = _db.retourProduits.Where(p => p.Retour_AbonnementID == Id && p.Position_Vente.PositionVente_PointVenteId == pdvId ).Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente);
             if (date != "")
             {
                 DateTime newDate = Convert.ToDateTime(date);
                 query.Where(p => p.Retour_DateCreation.Date == newDate.Date);
             }
             return query.AsEnumerable();*/
            IEnumerable<RetourProduits> retours = null;
            if (date != "")
            {
                retours = _db.retourProduits.Where(p => p.Retour_AbonnementID == Id && Convert.ToDateTime(p.Retour_DateCreation).ToString("yyyy-MM-dd") == date && p.Position_Vente.PositionVente_PointVenteId == pdvId)
                    .Include(p=>p.Position_Vente).ThenInclude(p=>p.Point_Vente)
                    .AsEnumerable();
            }
            else
            {
                retours = _db.retourProduits.Where(p => p.Retour_AbonnementID == Id && p.Position_Vente.PositionVente_PointVenteId == pdvId)
                    .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente).AsEnumerable();

            }
            return retours;
        }

        public IEnumerable<Retour_Details> getListDetails(int Id)
        {
            return _db.retour_Details.Where(p=>p.RetourDetails_RetourID == Id)
                .Include(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                .Include(p => p.Unite_Mesure).AsEnumerable();

        }

        public async Task<int?> AlimentationCaisse(AllimentationCaisse alimentation)
        {
            alimentation.AllimentationCaisse_DateCreation = DateTime.Now;
            await _db.allimentations.AddAsync(alimentation);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return alimentation.AllimentationCaisse_ID;
            else
                return null;
        }

            
        public IEnumerable<AllimentationCaisse> getListAlimentations(int Id, int? pdvId, string date)
        {
            IEnumerable<AllimentationCaisse> alimentations = null;
            if (date != "")
            {
                if (pdvId != null)
                {
                    alimentations = _db.allimentations.Where(p => p.AllimentationCaisse_AbonnementID == Id && p.AllimentationCaisse_PointVenteID ==(int)pdvId)
                                                .Where(p => Convert.ToDateTime(p.AllimentationCaisse_DateCreation).ToString("yyyy-MM-dd") == date)
                                                .Include(p=>p.User).Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente).AsEnumerable();
                }
                else
                {
                    alimentations = _db.allimentations.Where(p => p.AllimentationCaisse_AbonnementID == Id)
                                                .Where(p => Convert.ToDateTime(p.AllimentationCaisse_DateCreation).ToString("yyyy-MM-dd") == date)
                                                .Include(p => p.User).Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente).AsEnumerable();
                }
            }
            else
            {
                if (pdvId != null)
                {
                    alimentations = _db.allimentations.Where(p => p.AllimentationCaisse_AbonnementID == Id && p.AllimentationCaisse_PointVenteID == (int)pdvId)
                                                .Include(p => p.User).Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente).AsEnumerable();
                }
                else
                {
                    alimentations = _db.allimentations.Where(p => p.AllimentationCaisse_AbonnementID == Id)
                                                .Include(p => p.User).Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente).AsEnumerable();
                }
            }
            return alimentations;
        }
        public decimal getListAlimentationsPrix(int Id, int Caisse)
        {
            var alimentations = _db.allimentations.Where(p => p.AllimentationCaisse_AbonnementID == Id && p.AllimentationCaisse_PositionVenteID == Caisse && p.AllimentationCaisse_DateCreation.ToShortDateString() == DateTime.Now.ToShortDateString())
                .AsEnumerable();
            return alimentations.Sum(p => p.AllimentationCaisse_Montant);
        }
        public async Task<int?> RetraitCaisse(RetraitCaisse retraitCaisse)
        {
            retraitCaisse.RetraitCaisse_DateCreation = DateTime.Now;
            await _db.retraitCaisses.AddAsync(retraitCaisse);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return retraitCaisse.RetraitCaisse_ID;
            else
                return null;
        }

        public IEnumerable<RetraitCaisse> getListRetrait(int Id,int? Pdv, string date)
        {
            IEnumerable<RetraitCaisse> clotures;
            if (date != "")
            {
                if (Pdv != null)
                {

                    clotures = _db.retraitCaisses.Where(p => p.RetraitCaisse_AbonnementID == Id)
                        .Where(p => Convert.ToDateTime(p.RetraitCaisse_DateCreation).ToString("yyyy-MM-dd") == date)
                        .Where(p => p.Position_Vente.PositionVente_PointVenteId == Pdv)
                        .Include(p => p.Retrait_Type).Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                        .Include(p => p.User).AsEnumerable();
                }
                else
                {
                    clotures = _db.retraitCaisses.Where(p => p.RetraitCaisse_AbonnementID == Id)
                        .Where(p => Convert.ToDateTime(p.RetraitCaisse_DateCreation).ToString("yyyy-MM-dd") == date)
                        .Include(p => p.Retrait_Type).Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                        .Include(p => p.User).AsEnumerable();
                }
            }
            else
            {
                if (Pdv != null)
                {
                    clotures = _db.retraitCaisses.Where(p => p.RetraitCaisse_AbonnementID == Id)
                        .Where(p => p.Position_Vente.PositionVente_PointVenteId == Pdv && p.RetraitCaisse_DateCreation.Date == DateTime.Now.Date)
                        .Include(p => p.Retrait_Type)
                        .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                        .Include(p => p.User).AsEnumerable();
                }
                else
                {
                    clotures = _db.retraitCaisses.Where(p => p.RetraitCaisse_AbonnementID == Id && p.RetraitCaisse_DateCreation.Date == DateTime.Now.Date)
                        .Include(p=>p.Retrait_Type)
                        .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                        .Include(p => p.User).AsEnumerable();
                }
            }
            return clotures;
        }
        public decimal getListRetraitPrix(int Id,int positionVente)
        {
            var retraits = _db.retraitCaisses.Where(p => p.RetraitCaisse_AbonnementID == Id && p.RetraitCaisse_DateCreation.ToShortDateString() == DateTime.Now.ToShortDateString())
                .Where(p=>p.RetraitCaisse_PositionVenteID == positionVente)
                .AsEnumerable();
            return retraits.Sum(p => p.RetraitCaisse_Montant);
        }

        public IEnumerable<RetraitType> getListRetraitType()
        {
            return _db.retraitTypes.AsEnumerable();
        }
        public IEnumerable<MouvementCaisse> getListMouvementsCaisseParCaisse(int? caisseId)
        {
            var mouvementParDate = _db.mouvementsCaisse.Where(p => p.MouvementsCaisse_PositionVenteID == caisseId && p.MouvementsCaisse_DateMouvement.ToShortDateString() == DateTime.Now.ToShortDateString())
                .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                .Include(p => p.MouvementCaisse_Type)
                .AsEnumerable();
           return  mouvementParDate;
        }

        public IEnumerable<MouvementCaisse> getListMouvementsCaisse(int? pv ,string date,int aboId)
        {
            IEnumerable<MouvementCaisse> mouvement = null;
            if (pv == null && (date != "" ))
            {
                var mouvementParDate = _db.mouvementsCaisse.Where(p => p.MouvementsCaisse_AbonnementID == aboId)
                    .Where(p => p.MouvementsCaisse_DateMouvement.ToString() == date)
                    .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                    .Include(p => p.MouvementCaisse_Type)
                    .AsEnumerable();
                mouvement = mouvementParDate;
            }
            
            else if(pv != null && ( date == ""))
            {
                var m = _db.mouvementsCaisse.Where(p => p.MouvementsCaisse_AbonnementID == aboId)
                    .Where(p => p.MouvementsCaisse_DateMouvement.ToShortDateString() == DateTime.Now.ToShortDateString())
                    .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                    .Include(p => p.MouvementCaisse_Type)
                    .AsEnumerable();
                var mouvementParPv = m.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv);
                mouvement = mouvementParPv;
            }
            else if(pv == null && ( date == ""))
            {
                var mouvementVide = _db.mouvementsCaisse.Where(p => p.MouvementsCaisse_AbonnementID == aboId)
                   .Where(p => p.MouvementsCaisse_DateMouvement.ToShortDateString() == DateTime.Now.ToShortDateString())
                   .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                   .Include(p => p.MouvementCaisse_Type)
                   .AsEnumerable();
                mouvement = mouvementVide;
            }
            else if(pv != null && ( date != ""))
            {
                var dateTime = DateTime.Now.ToShortDateString();
                var m = _db.mouvementsCaisse.Where(p => p.MouvementsCaisse_AbonnementID == aboId)
                    .Where(p => p.MouvementsCaisse_DateMouvement.ToShortDateString() == date)
                    .Include(p => p.Position_Vente).ThenInclude(p=>p.Point_Vente)
                    .Include(p => p.MouvementCaisse_Type)
                    .AsEnumerable();
                var mouvementParPvEstDate = m.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv);
                mouvement = mouvementParPvEstDate;
            }
            return mouvement;

        }

        public async Task<int?> ClotureJournee(ClotureJournee cloture)
        {
            cloture.ClotueJournee_DateCreation = DateTime.Now;
            await _db.cloture.AddAsync(cloture);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return cloture.ClotueJournee_ID;
            else
                return null;
        }
        public async Task<int?> ValiderCloture(List<int>caisses)
        {
            foreach(var i in caisses)
            {
                var cloture = _db.cloture.Where(p => p.ClotueJournee_PositionVenteID == i && p.ClotueJournee_Valide==0).FirstOrDefault();
                cloture.ClotueJournee_Valide = 1;
                _db.Entry(cloture).State = EntityState.Modified;
            }
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return confirm;
            else
                return null;
        }

        public IEnumerable<ClotureJournee> getListCloture(int AboId,int? Pdv,string date)
        {
            IEnumerable<ClotureJournee> clotures;
            if (date != "")
            {
                if (Pdv != null)
                {
                    clotures = _db.cloture.Where(p => p.ClotueJournee_AbonnementID == AboId && p.Position_Vente.PositionVente_PointVenteId == Pdv)
                        .Where(p => Convert.ToDateTime(p.ClotueJournee_DateCreation).ToString("yyyy-MM-dd") == date)
                        .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                        .Include(p=>p.User)
                        .AsEnumerable();
                }
                else
                {
                    clotures = _db.cloture.Where(p => p.ClotueJournee_AbonnementID == AboId && p.Position_Vente.PositionVente_PointVenteId == Pdv)
                        .Where(p => Convert.ToDateTime(p.ClotueJournee_DateCreation).ToString("yyyy-MM-dd") == date)
                        .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                        .Include(p => p.User)
                        .AsEnumerable();
                }
            }
            else
            {
                if (Pdv != null)
                {
                    clotures = _db.cloture.Where(p => p.ClotueJournee_AbonnementID == AboId && p.Position_Vente.PositionVente_PointVenteId == Pdv)
                        .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                        .Include(p => p.User)
                        .AsEnumerable();
                }
                else
                {
                    clotures = _db.cloture.Where(p => p.ClotueJournee_AbonnementID == AboId && p.Position_Vente.PositionVente_PointVenteId == Pdv)
                        .Where(p => p.ClotueJournee_DateCreation.ToShortDateString() == DateTime.Now.ToShortDateString())
                        .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                        .Include(p => p.User)
                        .AsEnumerable();

                }
            }
            return clotures;
            /*return _db.cloture.Where(p => p.ClotueJournee_AbonnementID == AboId)
                .Where(p => p.ClotueJournee_DateCreation.ToShortDateString() == DateTime.Now.ToShortDateString() && p.ClotueJournee_Valide == 0)
                .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                .AsEnumerable();*/
        }

        public IEnumerable<ClotureJournee> getListCloturePerPdv(int AboId,int Pdv,int? caisseId,string date)
        {
            IEnumerable<ClotureJournee> clotures;
            if (date != "")
            {
                if (caisseId != null)
                {
                    clotures = _db.cloture.Where(p => p.ClotueJournee_AbonnementID == AboId && p.Position_Vente.PositionVente_PointVenteId == Pdv)
                        .Where(p => Convert.ToDateTime(p.ClotueJournee_DateCreation).ToString("yyyy-MM-dd") == date && p.ClotueJournee_PositionVenteID==caisseId)
                        .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                        .Include(p=>p.User)
                        .AsEnumerable();
                }
                else
                {
                    clotures = _db.cloture.Where(p => p.ClotueJournee_AbonnementID == AboId && p.Position_Vente.PositionVente_PointVenteId == Pdv)
                        .Where(p => Convert.ToDateTime(p.ClotueJournee_DateCreation).ToString("yyyy-MM-dd") == date)
                        .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente).Include(p => p.User)
                        .AsEnumerable();
                }
            }
            else
            {
                if (caisseId != null)
                {
                    clotures = _db.cloture.Where(p => p.ClotueJournee_AbonnementID == AboId && p.Position_Vente.PositionVente_PointVenteId == Pdv)
                        .Where(p => p.ClotueJournee_PositionVenteID==caisseId)
                        .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                        .Include(p => p.User).AsEnumerable();
                }
                else
                {
                    clotures = _db.cloture.Where(p => p.ClotueJournee_AbonnementID == AboId && p.Position_Vente.PositionVente_PointVenteId == Pdv)
                        .Where(p => p.ClotueJournee_DateCreation.ToShortDateString() == DateTime.Now.ToShortDateString())
                        .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                        .Include(p => p.User).AsEnumerable();

                }
            }
            return clotures;
        }

        public async Task<int?> CreateCommade(Commande commande)
        {
            var resC = _db.commandes.Where(p => p.Commande_NumeroTicket == commande.Commande_NumeroTicket).FirstOrDefault();
            if (resC != null)
                return resC.Commande_Id;
            else
            {
                var paiement = new Commande_Paiement();
                foreach (var c in commande.details)
                    c.CommandeDetail_AbonnementId = commande.Commande_AbonnementId;
                await _db.commandes.AddAsync(commande);
                await unitOfWork.Complete();
                if (commande.Commande_Avance > 0)
                {
                    paiement.CommandePaiement_CommandeID = commande.Commande_Id;
                    paiement.CommandePaiement_DatePaiement = DateTime.Now;
                    paiement.CommandePaiement_Montant = commande.Commande_Avance;
                    paiement.CommandePaiement_PositionVenteID = (int)commande.Commande_CaisseCommandeId;
                    paiement.CommandePaiement_UtilisateurID = commande.Commande_UtilisateurCommandeId;
                    paiement.CommandePaiement_ModePaiementID = 1;
                    await _db.paiementCommandes.AddAsync(paiement);
                }
                var res = await Planification(commande.Commande_Id);
                if (res == null)
                    return null;
                else
                    return res;
            
            }
            
        }

        public IEnumerable<Commande> getListCommandes(int PointVenteId,int? statutLiv,int? statutPaiement,string date)
        {
            var commandes = _db.commandes.Where(p => p.Commande_PointVenteId == PointVenteId)
                            .Where(p => p.Commande_EtatDeLivraison != 2 && p.Commande_EtatDePaiement != 1);
                            //.Where(p => p.Commande_DateLivraisonPrevue.ToShortDateString() == DateTime.Now.ToShortDateString());
            if (date!="")
            {
                if (statutLiv != null)
                {
                    if (statutPaiement != null)
                    {
                        commandes = _db.commandes.Where(p => p.Commande_PointVenteId == PointVenteId)
                            .Where(p => p.Commande_EtatDeLivraison == statutLiv && p.Commande_EtatDePaiement == statutPaiement)
                            .Where(p=> Convert.ToDateTime(p.Commande_DateLivraisonPrevue).ToString("yyyy-MM-dd") == date);
                    }
                    else
                    {
                        commandes = _db.commandes.Where(p => p.Commande_PointVenteId == PointVenteId)
                            .Where(p => p.Commande_EtatDePaiement == statutPaiement)
                            .Where(p => Convert.ToDateTime(p.Commande_DateLivraisonPrevue).ToString("yyyy-MM-dd") == date);
                    }
                }
                else
                {
                    if (statutPaiement != null)
                    {
                        commandes = _db.commandes.Where(p => p.Commande_PointVenteId == PointVenteId)
                            .Where(p => p.Commande_EtatDeLivraison == statutLiv)
                            .Where(p => Convert.ToDateTime(p.Commande_DateLivraisonPrevue).ToString("yyyy-MM-dd") == date);
                    }
                    else
                    {
                        commandes = _db.commandes.Where(p => p.Commande_PointVenteId == PointVenteId)
                            .Where(p => Convert.ToDateTime(p.Commande_DateLivraisonPrevue).ToString("yyyy-MM-dd") == date);
                    }
                }
            }
            else
            {
                if (statutLiv != null)
                {
                    if (statutPaiement != null)
                    {
                        commandes = _db.commandes.Where(p => p.Commande_PointVenteId == PointVenteId)
                            .Where(p => p.Commande_EtatDeLivraison == statutLiv && p.Commande_EtatDePaiement == statutPaiement);
                    }
                    else
                    {
                        commandes = _db.commandes.Where(p => p.Commande_PointVenteId == PointVenteId)
                            .Where(p => p.Commande_EtatDeLivraison == statutLiv);
                    }
                }
                else
                {
                    if (statutPaiement != null)
                    {
                        commandes = _db.commandes.Where(p => p.Commande_PointVenteId == PointVenteId)
                            .Where(p => p.Commande_EtatDePaiement == statutPaiement);
                    }
                    
                }
            }
            return commandes.Include(p => p.Statut_PaiementCommande)
                            .Include(p => p.Statut_Livraison)
                            .Include(p => p.User)
                            .AsEnumerable();
        }
        public IEnumerable<Commande_Paiement> getListPaiementCommande(int commandeID)
        {
            return _db.paiementCommandes.Where(p => p.CommandePaiement_CommandeID == commandeID)
                .AsEnumerable();
        }
        public IEnumerable<Commande> getListCommandesNonPayee(int PointVenteId)
        {
            var commandes= _db.commandes.Where(p => p.Commande_PointVenteId == PointVenteId)
                .Where(p=>p.Commande_EtatDeLivraison != 2 || p.Commande_EtatDePaiement !=1)
               // .Where(p=>p.Commande_DateLivraisonPrevue.ToShortDateString() == DateTime.Now.ToShortDateString())
                .Include(p=>p.Statut_Livraison)
                .Include(p=>p.Statut_PaiementCommande)
                .Include(p=>p.details).ThenInclude(p=>p.Forme_Produit).ThenInclude(p=>p.Produit_Vendable).ThenInclude(p=>p.Taux_TVA)
                .Include(p=>p.details).ThenInclude(p=>p.Forme_Produit).ThenInclude(p=>p.Produit_PretConsomer)
                .Include(p=>p.details).ThenInclude(p=>p.Forme_Produit).ThenInclude(p=>p.ProduitPackage)
                .Include(p=>p.details).ThenInclude(p=>p.Unite_Mesure)
                .Include(p => p.User).AsEnumerable().OrderBy(p=>p.Commande_DateLivraisonPrevue);
            return commandes;
        }
        public IEnumerable<Commande> getListCommandesFiltered(int PointVenteId, string date, string nomComandeur)
        {
            IEnumerable<Commande> commandes = null;
            if( date == "")
            {
                if(nomComandeur == "")
                {
                    commandes = _db.commandes.Where(p => p.Commande_PointVenteId == PointVenteId)               
                        .Where(p => p.Commande_EtatDeLivraison != 2 || p.Commande_EtatDePaiement != 1)
                       // .Where(p=>p.Commande_DateLivraisonPrevue.ToShortDateString() == DateTime.Now.ToShortDateString())
                        .Include(p => p.Statut_Livraison)
                        .Include(p => p.Statut_PaiementCommande)
                        .Include(p => p.details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                        .Include(p => p.details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                        .Include(p => p.details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                        .Include(p => p.details).ThenInclude(p => p.Unite_Mesure)
                        .Include(p=>p.User)
                        .AsEnumerable();

                }
                else
                {
                    commandes = _db.commandes.Where(p => p.Commande_PointVenteId == PointVenteId)
                        .Where(p => p.Commande_EtatDeLivraison != 2 || p.Commande_EtatDePaiement != 1)
                        .Where(p=>p.Commande_NomDemandeurs.Contains(nomComandeur) )
                        .Include(p => p.Statut_Livraison)
                        .Include(p => p.Statut_PaiementCommande)
                        .Include(p => p.details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                        .Include(p => p.details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                        .Include(p => p.details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                        .Include(p => p.details).ThenInclude(p => p.Unite_Mesure)
                        .Include(p => p.User)
                        .AsEnumerable();
                }
               
            }else if( date != "")
            {
                if(nomComandeur != "")
                {
                    commandes = _db.commandes.Where(p => p.Commande_PointVenteId == PointVenteId)
                        .Where(p => p.Commande_EtatDeLivraison != 2 || p.Commande_EtatDePaiement != 1)
                        .Where(p => p.Commande_NomDemandeurs.Contains(nomComandeur))
                        .Where(p => Convert.ToDateTime(p.Commande_DateLivraisonPrevue).ToString("dd/MM/yyyy") == date)
                        .Include(p => p.Statut_Livraison)
                        .Include(p => p.Statut_PaiementCommande)
                        .Include(p => p.details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                        .Include(p => p.details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)                        
                        .Include(p => p.details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                        .Include(p => p.details).ThenInclude(p => p.Unite_Mesure)
                        .Include(p => p.User).AsEnumerable();
                }
                else
                {
                    
                    commandes = _db.commandes.Where(p => p.Commande_PointVenteId == PointVenteId)
                        .Where(p => p.Commande_EtatDeLivraison != 2 || p.Commande_EtatDePaiement != 1)
                        .Where(p => Convert.ToDateTime(p.Commande_DateLivraisonPrevue).ToString("dd/MM/yyyy") == date)
                        .Include(p => p.Statut_Livraison)
                        .Include(p => p.Statut_PaiementCommande)
                        .Include(p => p.details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                        .Include(p => p.details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                        .Include(p => p.details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                        .Include(p => p.details).ThenInclude(p => p.Unite_Mesure)
                        .Include(p => p.User).AsEnumerable();
                }
            }
            return commandes;
        }


        public bool CalculeClotures(int PointVenteId)
        {
            var Pos = _db.positionVentes.Where(p => p.PositionVente_PointVenteId == PointVenteId && p.PositionVente_IsActive==1);
            var numberPos = Pos.Count();
            var numberClotures = _db.cloture.Where(p => p.Position_Vente.PositionVente_PointVenteId == PointVenteId && p.ClotueJournee_DateCreation.ToShortDateString() == DateTime.Now.ToShortDateString() && p.ClotueJournee_Valide==0).Count();
            if (numberPos > numberClotures)
                return false;
            else
                return true;
        }

        public IEnumerable<CommandeDetail> getListDetailsCommande(int commandeId)
        {
            return _db.commandeDetails.Where(p => p.CommandeDetail_CommandeId == commandeId)
                .Include(p => p.Commande)
                .Include(p => p.Unite_Mesure)
                .Include(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)                    
                .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                .AsEnumerable();
        }
        public async Task<int?> Planification(int commandeId)
        {
            int i = 0;
            var commande = _db.commandes.Where(p => p.Commande_Id == commandeId)
                .Include(p=>p.details).ThenInclude(p => p.Forme_Produit).ThenInclude(p=>p.Produit_Vendable)
                .FirstOrDefault();
            var atelierID = _db.point_Ventes.Where(p => p.PointVente_Id == commande.Commande_PointVenteId).FirstOrDefault().PointVente_AtelierID;
            var stockID = _db.ateliers.Where(p => p.Atelier_ID == (int)atelierID).FirstOrDefault().Atelier_StockID;
            var planifPord = new List<PlanificationdeProduction>();
            var BonDetails = new List<BonDetails>();
            foreach (var detail in commande.details)
            {
                var ficheForme = _db.ficheFormes
                    .Where(p => p.FicheForme_FormeProduit == detail.CommandeDetail_FormeProduitId)
                    .Where(p => p.FicheTechnique_Bridge.FicheTechniqueBridge_InUse == true)
                    .Include(p => p.FicheTechnique_Bridge).ThenInclude(p => p.Produit_FicheTechnique).ThenInclude(p => p.Matiere_Premiere)
                    .Include(p => p.FicheTechnique_Bridge).ThenInclude(p => p.Produit_FicheTechnique).ThenInclude(p => p.Unite_Mesure)
                    .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                    .FirstOrDefault();
                if (ficheForme != null && ficheForme.Forme_Produit.Produit_Vendable.ProduitVendable_PlanificationFlag == 1)
                {
                    foreach (var f in ficheForme.FicheTechnique_Bridge.Produit_FicheTechnique)
                    {
                        var nbProd = (detail.CommandeDetail_Quantite / ficheForme.FicheForme_Quantite);
                        decimal nbBase = 0;

                        var res = BonDetails.Where(p => p.BonDeSortie_MatiereId == f.FicheTechnique_MatierePremiereId).FirstOrDefault();
                        if (res == null)
                        {
                            var bon = new BonDetails
                            {
                                BonDeSortie_Quantite = nbProd * f.FicheTechnique_QuantiteMatiere,
                                BonDeSortie_QuantiteDemandee = nbProd * f.FicheTechnique_QuantiteMatiere,
                                BonDeSortie_MatiereId = f.FicheTechnique_MatierePremiereId,
                                BonDeSortie_UniteMesureId = f.FicheTechnique_UniteMesureId,
                            };
                            BonDetails.Add(bon);
                        }
                        else
                        {
                            if ((res.BonDeSortie_UniteMesureId == 1 && f.FicheTechnique_UniteMesureId == 2) || (res.BonDeSortie_UniteMesureId == 4 && f.FicheTechnique_UniteMesureId == 3))
                                nbBase = (nbProd * f.FicheTechnique_QuantiteMatiere) * 1000;
                            else if ((res.BonDeSortie_UniteMesureId == 2 && f.FicheTechnique_UniteMesureId == 1) || (res.BonDeSortie_UniteMesureId == 3 && f.FicheTechnique_UniteMesureId == 4))
                                nbBase = (nbProd * f.FicheTechnique_QuantiteMatiere) / 1000;
                            else
                                nbBase = nbProd * f.FicheTechnique_QuantiteMatiere;
                            res.BonDeSortie_Quantite += nbBase;
                            res.BonDeSortie_QuantiteDemandee += nbBase;
                        }
                      
                    }
                    var plan = new PlanificationdeProduction
                    {
                        PlanificationProduction_FormeProduitId = detail.CommandeDetail_FormeProduitId,
                        PlanificationProduction_DateCreation = DateTime.Now,
                        PlanificationProduction_QuantitePrevue = detail.CommandeDetail_Quantite,
                        PlanificationProduction_CoutRevient = detail.CommandeDetail_CoutdeRevient,
                        PlanificationProduction_Date = DateTime.Now,
                        PlanificationProduction_DateModification = DateTime.Now,
                        PlanificationProduction_ProduitVendableId = (int)detail.Forme_Produit.FormeProduit_ProduitID,
                        PlanificationProduction_AbonnementId = commande.Commande_AbonnementId,
                        PlanificationProduction_QuantiteRestante = detail.CommandeDetail_Quantite,
                    };
                    planifPord.Add(plan);
                }
                else
                {
                    i++;
                }

            }
            if(i == commande.details.Count())
            {
                return commande.Commande_Id;
            }
            else
            {
                var BondeSortie = new BonDeSortie
                {
                    BonDeSortie_AbonnementID = commande.Commande_AbonnementId,
                    BonDeSortie_Libelle = "Bon de commande de"+" " + commande.Commande_Date,
                    BonDeSortie_DateCreation = DateTime.Now,
                    BonDeSortie_DateProduction = commande.Commande_Date,
                    Bon_Details = BonDetails,
                    BonDeSortie_StockID = (int)stockID,
                };
                var planJournee = new PlanificationJournee
                {
                    PlanificationJournee_DateCreation = DateTime.Now,
                    PlanificationJournee_Etat = "En traitement",
                    PlanificationJournee_Date = commande.Commande_DateLivraisonPrevue,
                    PlanificationJournee_AbonnementID = commande.Commande_AbonnementId,
                    PlanificationJournee_CoutDeRevient = planifPord.Sum(p => p.PlanificationProduction_CoutRevient),
                    //Test
                    PlanificationJournee_AtelierID = atelierID,
                    PlanificationJournee_LieuStockageID = stockID,
                    PlanificationJournee_UtilisateurId = commande.Commande_UtilisateurCommandeId,
                    BonDe_Sortie = BondeSortie,
                    Planification_Production = planifPord,
                };
                if (planifPord.Count() > 0 && BonDetails.Count() > 0)
                {
                    await _db.planificationJournees.AddAsync(planJournee);
                    var confirm = await unitOfWork.Complete();
                    if (confirm > 0)
                    {
                        var res = await SetProdsDeBase(planJournee.PlanificationJournee_ID);
                        if (res == true)
                            return planJournee.PlanificationJournee_ID;
                        else
                            return null;
                    }
                    else
                        return null;
                }
                else
                    return 1;
            }
        }
        public async Task<bool> SetProdsDeBase(int planificationId)
        {
            var plan = _db.planificationJournees.Where(p => p.PlanificationJournee_ID == planificationId).Include(p => p.Planification_Production).FirstOrDefault();
            /*var plan = _db.planificationdeProductions
                    .Where(e => e.PlanificationProduction_PlanificationJourneeID == planificationId)
                 //   .Where(e => e.PlanificationProduction_Id == p.PlanificationProduction_Id)
                    .Include(e => e.Planification_Journee).ThenInclude(e => e.Atelier)
                    .AsEnumerable();*/
            var listM = new List<matStockViewModel>();
            foreach (var item in plan.Planification_Production)
            {
                var fichTech = _db.ficheTechniqueBridges.Where(p => p.FicheTechniqueBridge_ProduitVendableID == item.PlanificationProduction_ProduitVendableId).Where(f => f.FicheTechniqueBridge_InUse == true).Include(p => p.FicheTech_ProduitBase).FirstOrDefault();
                var ficheFormQte = _db.ficheFormes.Where(p => p.FicheForme_FicheBridge == fichTech.FicheTechniqueBridge_ID && p.FicheForme_FormeProduit == item.PlanificationProduction_FormeProduitId).FirstOrDefault().FicheForme_Quantite;
                var ficheBase = _db.ficheTech_ProduitBases.Where(p => p.FicheTech_ID == fichTech.FicheTechniqueBridge_ID).Include(p => p.ProduitBase).Include(p => p.Unite_Mesure).AsEnumerable();
                if (ficheBase.Any())
                {
                    foreach (var prod in ficheBase)
                    {
                        var nbProd = item.PlanificationProduction_QuantitePrevue / ficheFormQte;
                        decimal nbBase = 0;

                        var res = listM.Where(p => p.matID == prod.ProduitBase.ProduitBase_ID).FirstOrDefault();
                        if (res != null)
                        {
                            if ((res.uniteStock == "Gramme(s)" && prod.Unite_Mesure.UniteMesure_Libelle == "Kilogramme(s)") || (res.uniteStock == "Millilitre(s)" && prod.Unite_Mesure.UniteMesure_Libelle == "Litre(s)"))
                                nbBase = (nbProd * prod.ProduitQte) * 1000;
                            else if ((res.uniteStock == "Kilogramme(s)" && prod.Unite_Mesure.UniteMesure_Libelle == "Gramme(s)") || (res.uniteStock == "Litre(s)" && prod.Unite_Mesure.UniteMesure_Libelle == "Millilitre(s)"))
                                nbBase = (nbProd * prod.ProduitQte) / 1000;
                            else
                                nbBase = nbProd * prod.ProduitQte;
                            res.qteEnStock += nbBase;
                        }

                        else
                        {
                            var mat = new matStockViewModel()
                            {
                                matiereLibelle = prod.ProduitBase.ProduitBase_Designation,
                                matID = prod.ProduitBase.ProduitBase_ID,
                                qteEnStock = (item.PlanificationProduction_QuantitePrevue * prod.ProduitQte) / ficheFormQte,
                                uniteStock = prod.Unite_Mesure.UniteMesure_Libelle,
                                //   qteLivrer = planificationId,
                            };
                            listM.Add(mat);
                        }

                    }
                }
            }
            if (listM.Any())
            {
                foreach (var m in listM)
                {
                    var table = new Planification_ProdBase()
                    {
                        PlanificationProdBase_PlanificationID = planificationId,
                        PlanificationProdBase_ProdBaseID = m.matID,
                        PlanificationProdBase_Quantite = Convert.ToDecimal(m.qteEnStock.ToString("0.00")),
                        PlanificationProdBase_UniteDesi = m.uniteStock,
                        PlanificationProdBase_ProdBaseDesignation = m.matiereLibelle,
                    };
                    await _db.planification_ProdBases.AddAsync(table);
                }
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                {
                    return true;
                }
                else { return false; }
            }
            return true;
            
        }
        public async Task<int?> SetQteAVP(int planificationId)
        {
            try
            {
                var plan = _db.planificationJournees.Where(s => s.PlanificationJournee_ID == planificationId).Include(p => p.BonDe_Sortie).ThenInclude(p => p.Bon_Details).ThenInclude(p => p.Unite_Mesure).FirstOrDefault();
                if (plan != null)
                {
                    foreach (var d in plan.BonDe_Sortie.Bon_Details)
                    {
                        var matEnStock = _db.MatiereStock_Ateliers.Where(p => p.MatiereStockAtelier_AtelierID == plan.PlanificationJournee_AtelierID && p.MatiereStockAtelier_MatierePremiereID == d.BonDeSortie_MatiereId).Include(p => p.Matiere_Premiere).FirstOrDefault();
                        if (matEnStock != null)
                        {
                            decimal qte = 0;
                            if ((d.BonDeSortie_UniteMesureId == 1 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 2) || (d.BonDeSortie_UniteMesureId == 4 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 3))
                                qte = (decimal)d.BonDeSortie_Quantite / 1000;
                            else if ((d.BonDeSortie_UniteMesureId == 2 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 1) || (d.BonDeSortie_UniteMesureId == 3 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 4))
                                qte = (decimal)d.BonDeSortie_Quantite * 1000;
                            else
                                qte = (decimal)d.BonDeSortie_Quantite;
                            var res = matEnStock.MatiereStockAtelier_QauntiteActuelle - qte;
                            //decimal sub = 0;
                            //   if (res < 0)
                            //        matEnStock.MatiereStockAtelier_QuatiteAvecPlanification += (qte);
                            //  else
                            matEnStock.MatiereStockAtelier_QuatiteAvecPlanification -= qte;

                            _db.Entry(matEnStock).State = EntityState.Modified;

                        }
                        else
                        {
                            var matStock = new MatiereStock_Atelier()
                            {
                                MatiereStockAtelier_AtelierID = (int)plan.PlanificationJournee_AtelierID,
                                MatiereStockAtelier_MatierePremiereID = d.BonDeSortie_MatiereId,
                                MatiereStockAtelier_DateCreation = DateTime.Now,
                                MatiereStockAtelier_AbonnementID = plan.PlanificationJournee_AbonnementID,
                            };
                            var mat = _db.matierePremieres.Where(p => p.MatierePremiere_Id == matStock.MatiereStockAtelier_MatierePremiereID).FirstOrDefault();
                            decimal qte = 0;
                            if ((d.BonDeSortie_UniteMesureId == 1 && mat.MatierePremiere_AchatUniteMesureId == 2) || (d.BonDeSortie_UniteMesureId == 4 && mat.MatierePremiere_AchatUniteMesureId == 3))
                                qte = (decimal)d.BonDeSortie_Quantite / 1000;
                            else if ((d.BonDeSortie_UniteMesureId == 2 && mat.MatierePremiere_AchatUniteMesureId == 1) || (d.BonDeSortie_UniteMesureId == 3 && mat.MatierePremiere_AchatUniteMesureId == 4))
                                qte = (decimal)d.BonDeSortie_Quantite * 1000;
                            else
                                qte = (decimal)d.BonDeSortie_Quantite;

                            matStock.MatiereStockAtelier_QuatiteAvecPlanification -= qte;
                            await _db.MatiereStock_Ateliers.AddAsync(matStock);
                        }
                    }
                }
                else
                {
                    var planBase = _db.planificationJourneeBases.Where(s => s.PlanificationJourneeBase_ID == planificationId).Include(p => p.BonDe_Sortie).ThenInclude(p => p.Bon_Details).ThenInclude(p => p.Unite_Mesure).FirstOrDefault();
                    foreach (var d in planBase.BonDe_Sortie.Bon_Details)
                    {
                        var matEnStock = _db.MatiereStock_Ateliers.Where(p => p.MatiereStockAtelier_AtelierID == planBase.PlanificationJourneeBase_AtelierID && p.MatiereStockAtelier_MatierePremiereID == d.BonDeSortie_MatiereId).Include(p => p.Matiere_Premiere).FirstOrDefault();
                        if (matEnStock != null)
                        {
                            decimal qte = 0;
                            if ((d.BonDeSortie_UniteMesureId == 1 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 2) || (d.BonDeSortie_UniteMesureId == 4 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 3))
                                qte = (decimal)d.BonDeSortie_Quantite / 1000;
                            else if ((d.BonDeSortie_UniteMesureId == 2 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 1) || (d.BonDeSortie_UniteMesureId == 3 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 4))
                                qte = (decimal)d.BonDeSortie_Quantite * 1000;
                            else
                                qte = (decimal)d.BonDeSortie_Quantite;

                            var res = matEnStock.MatiereStockAtelier_QauntiteActuelle - qte;
                            // decimal sub = 0;
                            /*  if (res < 0)
                                  matEnStock.MatiereStockAtelier_QuatiteAvecPlanification += (qte);
                              else*/
                            matEnStock.MatiereStockAtelier_QuatiteAvecPlanification -= (qte);

                            _db.Entry(matEnStock).State = EntityState.Modified;

                        }
                        else
                        {
                            var matStock = new MatiereStock_Atelier()
                            {
                                MatiereStockAtelier_AtelierID = (int)planBase.PlanificationJourneeBase_AtelierID,
                                MatiereStockAtelier_MatierePremiereID = d.BonDeSortie_MatiereId,
                                MatiereStockAtelier_DateCreation = DateTime.Now,
                                MatiereStockAtelier_AbonnementID = planBase.PlanificationJourneeBase_AbonnementID,
                            };
                            var mat = _db.matierePremieres.Where(p => p.MatierePremiere_Id == matStock.MatiereStockAtelier_MatierePremiereID).FirstOrDefault();
                            decimal qte = 0;
                            if ((d.BonDeSortie_UniteMesureId == 1 && mat.MatierePremiere_AchatUniteMesureId == 2) || (d.BonDeSortie_UniteMesureId == 4 && mat.MatierePremiere_AchatUniteMesureId == 3))
                                qte = (decimal)d.BonDeSortie_Quantite / 1000;
                            else if ((d.BonDeSortie_UniteMesureId == 2 && mat.MatierePremiere_AchatUniteMesureId == 1) || (d.BonDeSortie_UniteMesureId == 3 && mat.MatierePremiere_AchatUniteMesureId == 4))
                                qte = (decimal)d.BonDeSortie_Quantite * 1000;
                            else
                                qte = (decimal)d.BonDeSortie_Quantite;

                            matStock.MatiereStockAtelier_QuatiteAvecPlanification -= qte;
                            await _db.MatiereStock_Ateliers.AddAsync(matStock);
                        }
                    }
                }

                var confirm = await unitOfWork.Complete();
                // transaction.Commit();
                return confirm;
            }
            catch
            {
                //transaction.Rollback();
                return null;
            }

        }

        public async Task<int?> PlanificationDeCloture(int aboId,int PointVenteId)
        {
            var clot = _db.cloture.Where(p => p.Position_Vente.PositionVente_PointVenteId == PointVenteId && p.ClotueJournee_Valide ==0).AsEnumerable();
            foreach(var c in clot)
            {
                c.ClotueJournee_Valide = 1;
                _db.Entry(c).State = EntityState.Modified;
            }
            var atelierID = _db.point_Ventes.Where(p => p.PointVente_Id == PointVenteId).FirstOrDefault().PointVente_AtelierID;
            var stockID = _db.ateliers.Where(p => p.Atelier_ID == (int)atelierID).FirstOrDefault().Atelier_StockID;
            var stockpdv = _db.pointVente_Stocks
                .Where(p => p.PointVenteStock_QuantiteProduit < p.Produit_Vendable.ProduitVendable_StockMinimum && p.PointVenteStock_PointVenteID == PointVenteId)
                .Include(p => p.Forme_Produit)
                .Include(p => p.Produit_Vendable)
                .AsEnumerable();
            var planifPord = new List<PlanificationdeProduction>();
            var BonDetails = new List<BonDetails>();
            foreach ( var s in stockpdv)
            {
                if (s.Produit_Vendable.ProduitVendable_PlanificationFlag == 1)
                {
                    var ficheForme = _db.ficheFormes
                    .Where(p => p.FicheForme_FormeProduit == s.PointVenteStock_FormeProduitID)
                    .Where(p => p.FicheTechnique_Bridge.FicheTechniqueBridge_InUse == true)
                    .Include(p => p.FicheTechnique_Bridge).ThenInclude(p => p.Produit_FicheTechnique).ThenInclude(p => p.Matiere_Premiere)
                    .Include(p => p.FicheTechnique_Bridge).ThenInclude(p => p.Produit_FicheTechnique).ThenInclude(p => p.Unite_Mesure)
                    .FirstOrDefault();

                    var stockqte = s.PointVenteStock_QuantiteProduit;
                    var produitMini = (decimal)s.Produit_Vendable.ProduitVendable_StockMinimum;
                    var qteaPlanifier = produitMini - stockqte;
                    foreach (var f in ficheForme.FicheTechnique_Bridge.Produit_FicheTechnique)
                    {
                        var bon = new BonDetails
                        {
                            BonDeSortie_Quantite = (qteaPlanifier / ficheForme.FicheForme_Quantite) * f.FicheTechnique_QuantiteMatiere,
                            BonDeSortie_MatiereId = f.FicheTechnique_MatierePremiereId,
                            BonDeSortie_UniteMesureId = f.FicheTechnique_UniteMesureId,
                        };
                        BonDetails.Add(bon);
                    }
                    var plan = new PlanificationdeProduction
                    {
                        PlanificationProduction_FormeProduitId = s.PointVenteStock_FormeProduitID,
                        PlanificationProduction_DateCreation = DateTime.Now,
                        PlanificationProduction_QuantitePrevue = qteaPlanifier,
                        PlanificationProduction_CoutRevient = s.Forme_Produit.FormeProduit_CoutDeRevient,
                        PlanificationProduction_Date = DateTime.Now,
                        PlanificationProduction_DateModification = DateTime.Now,
                        PlanificationProduction_ProduitVendableId = (int)s.PointVenteStock_ProduitID,
                        PlanificationProduction_AbonnementId = s.PointVenteStock_AbonnementID,
                        PlanificationProduction_QuantiteRestante = qteaPlanifier,
                    };
                    planifPord.Add(plan);
                }
                
            }
            var today = DateTime.Today;
            var BondeSortie = new BonDeSortie
            {
                BonDeSortie_AbonnementID = aboId,
                BonDeSortie_Libelle = "Bon de planification de" + today,
                BonDeSortie_DateCreation = DateTime.Now,
                BonDeSortie_DateProduction = today.AddDays(1),
                Bon_Details = BonDetails,
            };
            var planJournee = new PlanificationJournee
            {
                PlanificationJournee_DateCreation = DateTime.Now,
                PlanificationJournee_Etat = "En traitement",
                PlanificationJournee_Date = today.AddDays(1),
                PlanificationJournee_AbonnementID = aboId,
                PlanificationJournee_CoutDeRevient = planifPord.Sum(p => p.PlanificationProduction_CoutRevient),
                //Test
                PlanificationJournee_AtelierID = atelierID,
                PlanificationJournee_LieuStockageID = stockID,
               // PlanificationJournee_UtilisateurId = commande.Commande_UtilisateurCommandeId,
                BonDe_Sortie = BondeSortie,
                Planification_Production = planifPord,
            };
            if(planifPord.Count() > 0 && BonDetails.Count() > 0)
            {
                await _db.planificationJournees.AddAsync(planJournee);
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return planJournee.PlanificationJournee_ID;
                else
                    return null;
            }else
                return 0;
           
        }
        public async Task<int?> PaiementCommande(Commande_Paiement Paiementcommande)
        {
            await _db.paiementCommandes.AddAsync(Paiementcommande);
            await unitOfWork.Complete();
            var commande = _db.commandes.Where(p => p.Commande_Id == Paiementcommande.CommandePaiement_CommandeID).FirstOrDefault();
            commande.Commande_Avance += Paiementcommande.CommandePaiement_Montant;
            commande.Commande_EtatDeLivraison = 2;
            if (commande.Commande_Avance == commande.Commande_MontantTotal)
                commande.Commande_EtatDePaiement = 1;
            else if (commande.Commande_Avance < commande.Commande_MontantTotal && commande.Commande_Avance > 0)
                commande.Commande_EtatDePaiement = 2;
            _db.Entry(commande).State = EntityState.Modified;
            MouvementCaisse mouvement = new MouvementCaisse
            {
                MouvementsCaisse_TypeID = 1,
                MouvementsCaisse_Montant = Paiementcommande.CommandePaiement_Montant,
                MouvementsCaisse_DateMouvement = Paiementcommande.CommandePaiement_DatePaiement,
                MouvementsCaisse_PositionVenteID = (int)Paiementcommande.CommandePaiement_PositionVenteID,
                MouvementsCaisse_UtilisateurID = Paiementcommande.CommandePaiement_UtilisateurID,
                MouvementsCaisse_AbonnementID = commande.Commande_AbonnementId,

            };
            await _db.mouvementsCaisse.AddAsync(mouvement);
            var res = await unitOfWork.Complete();
            //var res = await SortieStock_Commande(commande.Commande_Id);
            if (res > 0)
                return commande.Commande_Id;
            else
                return null;
        }
        public async Task<int?> LivrerCommande(int commandeId,string userId)
        {
            Commande commande = _db.commandes.Where(p => p.Commande_Id == commandeId).FirstOrDefault();
            if (commande != null)
            { 
                commande.Commande_EtatDeLivraison = 2;
                commande.Commande_UtilisateurLivraisonId = userId;
                _db.Entry(commande).State = EntityState.Modified;
            }
            else
                return null;
            await unitOfWork.Complete();
            var res = await SortieStock_Commande(commande.Commande_Id);
            if (res == null)
                return null;
            else
                return commande.Commande_Id;
        }

        public async Task<int?> CreateGratuite(Gratuite gratuite)
        {
            gratuite.Gratuite_DateCreation = DateTime.Now;
            await _db.gratuites.AddAsync(gratuite);
            await unitOfWork.Complete();
            var res = await Retour_Gratuite(gratuite.Gratuite_ID);
            if (res == null)
            {
                return null;
            }
            else
                return gratuite.Gratuite_ID;
        }

        public IEnumerable<Perte> getListPertes(int PointVenteId, string date)
        {
            var query = _db.pertes.Where(p => p.Position_Vente.PositionVente_PointVenteId == PointVenteId);
            if(date!="")
            {
                DateTime newDate = Convert.ToDateTime(date);
                return _db.pertes.Where(p => p.Position_Vente.PositionVente_PointVenteId == PointVenteId).Where(p => p.Perte_DateCreation.Date == newDate.Date).Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente).AsEnumerable();
            }
            return query.Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente).AsEnumerable();
        }
        public IEnumerable<Statut_Livraison> getListStatutLivraison()
        {
            return _db.Statut_livraisons.AsEnumerable();
        }
        public IEnumerable<Statut_PaiementCommande> getListStatutPaiement()
        {
            return _db.statut_Paiements.AsEnumerable();
        }


        public IEnumerable<PerteDetails> getListDetailsPertes(int perteId)
        {
            return _db.perteDetails.Where(p => p.PerteDetails_PerteID == perteId)
                .Include(p => p.Unite_Mesure)
                .Include(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                .AsEnumerable();
        }

        public IEnumerable<Gratuite> getListGratuite(int PointVenteId, string date)
        {
            /*var query = _db.gratuites.Where(p => p.Position_Vente.PositionVente_PointVenteId == PointVenteId);
            if (date == "")
            {
                DateTime newDate = Convert.ToDateTime(date);
                query.Where(p => p.Gratuite_DateCreation.Date == newDate.Date);
            }
            return query.Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                         .AsEnumerable();*/
            IEnumerable<Gratuite> grats = null;
            if (date != "")
            {
                grats = _db.gratuites.Where(p => Convert.ToDateTime(p.Gratuite_DateCreation).ToString("yyyy-MM-dd") == date && p.Position_Vente.PositionVente_PointVenteId == PointVenteId)
                    .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente).AsEnumerable();
            }
            else
            {
                grats = _db.gratuites.Where(p => p.Position_Vente.PositionVente_PointVenteId == PointVenteId)
                    .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente).AsEnumerable();

            }
            return grats;

        }

        public IEnumerable<GratuiteDetails> getListDetailsGratuite(int gratuiteId)
        {
            return _db.gratuiteDetails.Where(p => p.GratuiteDetails_GratuiteID == gratuiteId)
                .Include(p => p.Unite_Mesure)
                .Include(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                .AsEnumerable();
        }

        public string GetCommandeUserName(string userId)
        {
           return _db.Users.Where(p => p.Id == userId).FirstOrDefault().Nom_Complet;
        } 
        public decimal GetSoleDebit(int  caisseId, string date, int aboId)
        {
            decimal vente = 0;
            decimal commandes = 0;
            if (date != "")
            {
                var ventesDetail = _db.venteDetails.Where(p => p.Vente.Vente_PositionVenteId == caisseId && Convert.ToDateTime(p.VenteDetails_DateCreation).ToString("dd/MM/yyyy") == date  && p.VenteDetails_AbonnementID == aboId && p.VenteDetails_FlagCloture==0)
                    .Include(p=>p.Vente).AsEnumerable();
                foreach(var v in ventesDetail)
                {
                    var t = v.Vente.Vente_TauxDeRemise;
                    v.VenteDetails_Prix -= (t * v.VenteDetails_Prix) / 100;
                }
                vente = Convert.ToDecimal(ventesDetail.Sum(p => p.VenteDetails_Prix).ToString("G29"));
                commandes = _db.paiementCommandes.Where(p => p.CommandePaiement_PositionVenteID == caisseId && Convert.ToDateTime(p.CommandePaiement_DatePaiement).ToString("dd/MM/yyyy") == date && p.Commande.Commande_AbonnementId == aboId && p.CommandePaiement_FlagCloture == 0).AsEnumerable().Sum(p => p.CommandePaiement_Montant);
            }
            else
            {
                var ventesDetail = _db.venteDetails.Where(p => p.Vente.Vente_PositionVenteId == caisseId && p.VenteDetails_AbonnementID == aboId && p.VenteDetails_FlagCloture == 0)
                    .Include(p => p.Vente).AsEnumerable();
                foreach (var v in ventesDetail)
                {
                    var t = v.Vente.Vente_TauxDeRemise;
                    v.VenteDetails_Prix -= (t * v.VenteDetails_Prix) / 100;
                }
                
                vente = Convert.ToDecimal(ventesDetail.Sum(p => p.VenteDetails_Prix).ToString("G29"));
                commandes = _db.paiementCommandes.Where(p => p.CommandePaiement_PositionVenteID == caisseId && p.Commande.Commande_AbonnementId == aboId && p.CommandePaiement_FlagCloture == 0).AsEnumerable().Sum(p => p.CommandePaiement_Montant);
            }
             
            return (vente + commandes);
        }
        public decimal GetAlimentation(int  caisseId, string date, int aboId)
        {
            decimal alim = 0;
            if (date != "")
            {
                alim = _db.allimentations.Where(p => p.AllimentationCaisse_PositionVenteID == caisseId && Convert.ToDateTime(p.AllimentationCaisse_DateCreation).ToString("dd/MM/yyyy") == date && p.AllimentationCaisse_AbonnementID == aboId && p.AllimentationCaisse_FlagCloture==0).AsEnumerable().Sum(p => p.AllimentationCaisse_Montant);
            }
            else
            {
                alim = _db.allimentations.Where(p => p.AllimentationCaisse_PositionVenteID == caisseId && p.AllimentationCaisse_AbonnementID == aboId && p.AllimentationCaisse_FlagCloture==0).AsEnumerable().Sum(p => p.AllimentationCaisse_Montant);
            }
             
            return (alim);
        }
        public decimal GetSoleCredit(int caisseId, string date,int aboId)
        {
            decimal retourProduit = 0;
            decimal retrait = 0;
            if (date != "")
            {
                retourProduit = _db.retourProduits.Where(p => p.Retour_PositionVenteID == caisseId && Convert.ToDateTime(p.Retour_DateCreation).ToString("dd/MM/yyyy") == date && p.Retour_AbonnementID == aboId && p.Retour_FlagCloture==0)
                   .AsEnumerable().Sum(p => p.Retour_PrixTotal);
                retrait = _db.retraitCaisses.Where(p => p.RetraitCaisse_PositionVenteID == caisseId && Convert.ToDateTime(p.RetraitCaisse_DateCreation).ToString("dd/MM/yyyy") == date && p.RetraitCaisse_AbonnementID == aboId && p.RetraitCaisse_FlagCloture==0).AsEnumerable().Sum(p => p.RetraitCaisse_Montant);
            }
            else
            {
                retourProduit = _db.retourProduits.Where(p => p.Retour_PositionVenteID == caisseId && p.Retour_AbonnementID == aboId && p.Retour_FlagCloture == 0)
                   .AsEnumerable().Sum(p => p.Retour_PrixTotal);
                retrait = _db.retraitCaisses.Where(p => p.RetraitCaisse_PositionVenteID == caisseId && p.RetraitCaisse_AbonnementID == aboId && p.RetraitCaisse_FlagCloture==0).AsEnumerable().Sum(p => p.RetraitCaisse_Montant);

            }
            return (retourProduit + retrait);
        }
        public decimal GetSoleDebitBypv(int? pv, string date, int aboId)
        {
            decimal vente = 0;
            decimal commandes = 0;
            decimal alim = 0;
            if (date != "")
            {
                if (pv != null)
                {
                    vente = _db.vente.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv && Convert.ToDateTime(p.Vente_Date).ToString("dd/MM/yyyy") == date && p.Vente_AbonnementId == aboId).AsEnumerable().Sum(p => p.Vente_PrixTotalRemise);
                    commandes = _db.paiementCommandes.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv && Convert.ToDateTime(p.CommandePaiement_DatePaiement).ToString("dd/MM/yyyy") == date && p.Commande.Commande_AbonnementId == aboId).AsEnumerable().Sum(p => p.CommandePaiement_Montant);
                    alim = _db.allimentations.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv && Convert.ToDateTime(p.AllimentationCaisse_DateCreation).ToString("dd/MM/yyyy") == date && p.AllimentationCaisse_AbonnementID == aboId).AsEnumerable().Sum(p => p.AllimentationCaisse_Montant);
                }
                else
                {
                    vente = _db.vente.Where(p => p.Vente_Date.ToShortDateString() == date && p.Vente_AbonnementId == aboId).AsEnumerable().Sum(p => p.Vente_PrixTotalRemise);
                    commandes = _db.paiementCommandes.Where(p => p.CommandePaiement_DatePaiement.ToShortDateString() == date && p.Commande.Commande_AbonnementId == aboId).AsEnumerable().Sum(p => p.CommandePaiement_Montant);
                    alim = _db.allimentations.Where(p => p.AllimentationCaisse_DateCreation.ToShortDateString() == date && p.AllimentationCaisse_AbonnementID == aboId).AsEnumerable().Sum(p => p.AllimentationCaisse_Montant);
                }
                
            }
            else
            {
                if (pv != null)
                {
                    vente = _db.vente.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv  && p.Vente_AbonnementId == aboId).AsEnumerable().Sum(p => p.Vente_PrixTotalRemise);
                    commandes = _db.paiementCommandes.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv && p.Commande.Commande_AbonnementId == aboId).AsEnumerable().Sum(p => p.CommandePaiement_Montant);
                    alim = _db.allimentations.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv && p.AllimentationCaisse_AbonnementID == aboId).AsEnumerable().Sum(p => p.AllimentationCaisse_Montant);
                }
                else
                {
                    vente = _db.vente.Where(p => p.Vente_AbonnementId == aboId).AsEnumerable().Sum(p => p.Vente_PrixTotalRemise);
                    commandes = _db.paiementCommandes.Where(p => p.Commande.Commande_AbonnementId == aboId).AsEnumerable().Sum(p => p.CommandePaiement_Montant);
                    alim = _db.allimentations.Where(p => p.AllimentationCaisse_AbonnementID == aboId).AsEnumerable().Sum(p => p.AllimentationCaisse_Montant);
                }
            }
             
            return (vente + commandes + alim);
        }
        public decimal GetSoleDebitBypvApi(int? pv, string date, int aboId)
        {
            decimal vente = 0;
            decimal commandes = 0;
            decimal alim = 0;
            if (date != "")
            {
                if (pv != null)
                {
                    vente = _db.vente.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv && Convert.ToDateTime(p.Vente_Date).ToString("dd/MM/yyyy") == date && p.Vente_AbonnementId == aboId).AsEnumerable().Sum(p => p.Vente_PrixTotalRemise);
                    commandes = _db.paiementCommandes.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv && Convert.ToDateTime(p.CommandePaiement_DatePaiement).ToString("dd/MM/yyyy") == date && p.Commande.Commande_AbonnementId == aboId).AsEnumerable().Sum(p => p.CommandePaiement_Montant);
                   //alim = _db.allimentations.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv && Convert.ToDateTime(p.AllimentationCaisse_DateCreation).ToString("dd/MM/yyyy") == date && p.AllimentationCaisse_AbonnementID == aboId).AsEnumerable().Sum(p => p.AllimentationCaisse_Montant);
                }
                else
                {
                    vente = _db.vente.Where(p => Convert.ToDateTime(p.Vente_Date).ToString("dd/MM/yyyy") == date && p.Vente_AbonnementId == aboId).AsEnumerable().Sum(p => p.Vente_PrixTotalRemise);
                    commandes = _db.paiementCommandes.Where(p => Convert.ToDateTime(p.CommandePaiement_DatePaiement).ToString("dd/MM/yyyy") == date && p.Commande.Commande_AbonnementId == aboId).AsEnumerable().Sum(p => p.CommandePaiement_Montant);
                   // alim = _db.allimentations.Where(p => Convert.ToDateTime(p.AllimentationCaisse_DateCreation).ToString("dd/MM/yyyy") == date && p.AllimentationCaisse_AbonnementID == aboId).AsEnumerable().Sum(p => p.AllimentationCaisse_Montant);
                }
                
            }
            else
            {
                if (pv != null)
                {
                    vente = _db.vente.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv  && p.Vente_AbonnementId == aboId).AsEnumerable().Sum(p => p.Vente_PrixTotalRemise);
                    commandes = _db.paiementCommandes.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv && p.Commande.Commande_AbonnementId == aboId).AsEnumerable().Sum(p => p.CommandePaiement_Montant);
                   // alim = _db.allimentations.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv && p.AllimentationCaisse_AbonnementID == aboId).AsEnumerable().Sum(p => p.AllimentationCaisse_Montant);
                }
                else
                {
                    vente = _db.vente.Where(p => p.Vente_AbonnementId == aboId).AsEnumerable().Sum(p => p.Vente_PrixTotalRemise);
                    commandes = _db.paiementCommandes.Where(p => p.Commande.Commande_AbonnementId == aboId).AsEnumerable().Sum(p => p.CommandePaiement_Montant);
                   // alim = _db.allimentations.Where(p => p.AllimentationCaisse_AbonnementID == aboId).AsEnumerable().Sum(p => p.AllimentationCaisse_Montant);
                }
            }
             
            return (vente + commandes + alim);
        }
        public decimal GetAlimentationBypv(int? pv, string date, int aboId)
        {
            decimal alim = 0;
            if (date != "")
            {
                if (pv != null)
                {
                    alim = _db.allimentations.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv && Convert.ToDateTime(p.AllimentationCaisse_DateCreation).ToString("dd/MM/yyyy") == date && p.AllimentationCaisse_AbonnementID == aboId).AsEnumerable().Sum(p => p.AllimentationCaisse_Montant);
                }
                else
                {
                    alim = _db.allimentations.Where(p => Convert.ToDateTime(p.AllimentationCaisse_DateCreation).ToString("dd/MM/yyyy") == date && p.AllimentationCaisse_AbonnementID == aboId).AsEnumerable().Sum(p => p.AllimentationCaisse_Montant);
                }
                
            }
            else
            {
                if (pv != null)
                {
                    alim = _db.allimentations.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv && p.AllimentationCaisse_AbonnementID == aboId).AsEnumerable().Sum(p => p.AllimentationCaisse_Montant);
                }
                else
                {
                    alim = _db.allimentations.Where(p => p.AllimentationCaisse_AbonnementID == aboId).AsEnumerable().Sum(p => p.AllimentationCaisse_Montant);
                }
            }
             
            return ( alim);
        }

        public decimal GetSoleCreditBypv(int? pv, string date,int aboId)
        {
            decimal retourProduit = 0;
            decimal retrait = 0;
            if (date != "")
            {
                if(pv!=null)
                {
                    retourProduit = _db.retourProduits.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv && p.Retour_DateCreation.ToShortDateString() == date && p.Retour_AbonnementID == aboId)
                       .AsEnumerable().Sum(p => p.Retour_PrixTotal);
                    retrait = _db.retraitCaisses.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv && p.RetraitCaisse_DateCreation.ToShortDateString() == date && p.RetraitCaisse_AbonnementID == aboId).AsEnumerable().Sum(p => p.RetraitCaisse_Montant);
                }
                else
                {
                    retourProduit = _db.retourProduits.Where(p => Convert.ToDateTime(p.Retour_DateCreation).ToString("dd/MM/yyyy") == date && p.Retour_AbonnementID == aboId)
                       .AsEnumerable().Sum(p => p.Retour_PrixTotal);
                    retrait = _db.retraitCaisses.Where(p => Convert.ToDateTime(p.RetraitCaisse_DateCreation).ToString("dd/MM/yyyy") == date && p.RetraitCaisse_AbonnementID == aboId).AsEnumerable().Sum(p => p.RetraitCaisse_Montant);
                }
            }
            else
            {
                if (pv != null)
                {
                    retourProduit = _db.retourProduits.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv && p.Retour_AbonnementID == aboId)
                       .AsEnumerable().Sum(p => p.Retour_PrixTotal);
                    retrait = _db.retraitCaisses.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv && p.RetraitCaisse_AbonnementID == aboId).AsEnumerable().Sum(p => p.RetraitCaisse_Montant);
                }
                else
                {
                    retourProduit = _db.retourProduits.Where(p => p.Retour_AbonnementID == aboId)
                       .AsEnumerable().Sum(p => p.Retour_PrixTotal);
                    retrait = _db.retraitCaisses.Where(p => p.RetraitCaisse_AbonnementID == aboId).AsEnumerable().Sum(p => p.RetraitCaisse_Montant);
                }

            }
            return (retourProduit + retrait);
        }public decimal GetSoleCreditBypvApi(int? pv, string date,int aboId)
        {
            decimal retourProduit = 0;
            decimal retrait = 0;
            if (date != "")
            {
                if(pv!=null)
                {
                    retourProduit = _db.retourProduits.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv && Convert.ToDateTime(p.Retour_DateCreation).ToString("dd/MM/yyyy") == date && p.Retour_AbonnementID == aboId)
                       .AsEnumerable().Sum(p => p.Retour_PrixTotal);
                    retrait = _db.retraitCaisses.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv && Convert.ToDateTime(p.RetraitCaisse_DateCreation).ToString("dd/MM/yyyy") == date && p.RetraitCaisse_AbonnementID == aboId).AsEnumerable().Sum(p => p.RetraitCaisse_Montant);
                }
                else
                {
                    retourProduit = _db.retourProduits.Where(p => Convert.ToDateTime(p.Retour_DateCreation).ToString("dd/MM/yyyy") == date && p.Retour_AbonnementID == aboId)
                       .AsEnumerable().Sum(p => p.Retour_PrixTotal);
                    retrait = _db.retraitCaisses.Where(p => Convert.ToDateTime(p.RetraitCaisse_DateCreation).ToString("dd/MM/yyyy") == date && p.RetraitCaisse_AbonnementID == aboId).AsEnumerable().Sum(p => p.RetraitCaisse_Montant);
                }
            }
            else
            {
                if (pv != null)
                {
                    retourProduit = _db.retourProduits.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv && p.Retour_AbonnementID == aboId)
                       .AsEnumerable().Sum(p => p.Retour_PrixTotal);
                    retrait = _db.retraitCaisses.Where(p => p.Position_Vente.PositionVente_PointVenteId == pv && p.RetraitCaisse_AbonnementID == aboId).AsEnumerable().Sum(p => p.RetraitCaisse_Montant);
                }
                else
                {
                    retourProduit = _db.retourProduits.Where(p => p.Retour_AbonnementID == aboId)
                       .AsEnumerable().Sum(p => p.Retour_PrixTotal);
                    retrait = _db.retraitCaisses.Where(p => p.RetraitCaisse_AbonnementID == aboId).AsEnumerable().Sum(p => p.RetraitCaisse_Montant);
                }

            }
            return (retourProduit + retrait);
        }

        public IEnumerable<ClotureJournee> getListClotureFiltered(int AboId,string date, int? pv)
        {
            IEnumerable<ClotureJournee> Clotures = null;
            if (date != "")
            {
                if (pv != null)
                {
                    Clotures = _db.cloture.Where(p => p.ClotueJournee_AbonnementID == AboId && p.Position_Vente.PositionVente_PointVenteId == pv)
                        .Where(p => Convert.ToDateTime(p.ClotueJournee_DateCreation).ToString("dd/MM/yyyy") == date)
                        .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                        .Include(p => p.User).AsEnumerable();
                }
                else
                {
                    Clotures = _db.cloture.Where(p => p.ClotueJournee_AbonnementID == AboId)
                        .Where(p => Convert.ToDateTime(p.ClotueJournee_DateCreation).ToString("dd/MM/yyyy") == date )
                        .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                        .Include(p => p.User).AsEnumerable();
                }
            }
            else
            {
                if (pv != null)
                {
                    Clotures = _db.cloture.Where(p => p.ClotueJournee_AbonnementID == AboId && p.Position_Vente.PositionVente_PointVenteId == pv)
                        .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                        .Include(p => p.User).AsEnumerable();
                }
                else
                {
                    Clotures = _db.cloture.Where(p => p.ClotueJournee_AbonnementID == AboId )
                        .Include(p => p.Position_Vente).ThenInclude(p => p.Point_Vente)
                        .Include(p => p.User).AsEnumerable();
                }

            }
            return Clotures;
        }
        public IEnumerable<Commande_Paiement> getListPaiementCommandeByPdv(int? pdvId, int aboId, string date)
        {
            IEnumerable<Commande_Paiement> paiement = null;
            
            if (date != "")
            {
                if (pdvId != null)
                {
                    paiement = _db.paiementCommandes.Where(p => p.Position_Vente.PositionVente_PointVenteId == pdvId && Convert.ToDateTime(p.CommandePaiement_DatePaiement).ToString("dd/MM/yyyy") == date && p.Position_Vente.PositionVente_AbonnementId==aboId)
                        .Include(p => p.Commande)
                        .Include(p => p.Position_Vente)
                        .AsEnumerable();
                }
                else
                {
                    paiement = _db.paiementCommandes.Where(p => Convert.ToDateTime(p.CommandePaiement_DatePaiement).ToString("dd/MM/yyyy") == date && p.Position_Vente.PositionVente_AbonnementId == aboId)
                           .Include(p => p.Commande)
                           .Include(p => p.Position_Vente)
                           .AsEnumerable();
                }
            }
            else
            {
                if (pdvId != null)
                {
                    paiement = _db.paiementCommandes.Where(p => p.Position_Vente.PositionVente_PointVenteId == pdvId && p.CommandePaiement_DatePaiement.Date == DateTime.Now.Date)
                         .Include(p => p.Commande)
                         .Include(p => p.Position_Vente)
                         .AsEnumerable();

                }
                else
                {
                    paiement = _db.paiementCommandes.Where(p =>p.Position_Vente.PositionVente_AbonnementId == aboId && p.CommandePaiement_DatePaiement.Date == DateTime.Now.Date)
                        .Include(p => p.Commande)
                        .Include(p => p.Position_Vente)
                        .AsEnumerable();
                }

            }
            return paiement;
        }
        public IEnumerable<Vente> GetListVente(int? pdvId, int aboId, string date)
        {
            IEnumerable<Vente> venteDetails = null;
            if (date != "")
            {
                if (pdvId != null)
                {
                    venteDetails = _db.vente.Where(p => p.Vente_AbonnementId == aboId && Convert.ToDateTime(p.Vente_Date).ToString("dd/MM/yyyy") == date && p.Vente_PointVenteId == pdvId)
                        .Include(p => p.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                        .Include(p => p.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                        .Include(p => p.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                          .Include(p => p.Details).ThenInclude(p => p.Unite_Mesure).AsEnumerable();
                }
                else
                {
                    venteDetails = _db.vente.Where(p => p.Vente_AbonnementId == aboId && Convert.ToDateTime(p.Vente_Date).ToString("dd/MM/yyyy") == date)
                          .Include(p => p.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                          .Include(p => p.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                          .Include(p => p.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                          .Include(p => p.Details).ThenInclude(p => p.Unite_Mesure).AsEnumerable();
                }
            }
            else
            {
                if (pdvId != null)
                {
                    venteDetails = _db.vente.Where(p => p.Vente_AbonnementId == aboId && p.Vente_PointVenteId == pdvId && p.Vente_Date.Date == DateTime.Now.Date)
                         .Include(p=>p.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                         .Include(p => p.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                         .Include(p => p.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                          .Include(p => p.Details).ThenInclude(p => p.Unite_Mesure).AsEnumerable();

                }
                else
                {
                    venteDetails = _db.vente.Where(p => p.Vente_AbonnementId == aboId && p.Vente_Date.Date == DateTime.Now.Date)
                        .Include(p => p.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                        .Include(p => p.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                        .Include(p => p.Details).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                          .Include(p => p.Details).ThenInclude(p => p.Unite_Mesure).AsEnumerable();
                }

            }
            return venteDetails;
        }        
        public IEnumerable<AllimentationCaisse> GetListAlimentation(int? pdvId, int aboId, string date)
        {
            IEnumerable<AllimentationCaisse> alim = null;
            if (date != "")
            {
                if (pdvId != null)
                {
                    alim = _db.allimentations.Where(p => p.AllimentationCaisse_AbonnementID == aboId && Convert.ToDateTime(p.AllimentationCaisse_DateCreation).ToString("dd/MM/yyyy") == date && p.AllimentationCaisse_PointVenteID == pdvId)
                          .AsEnumerable();
                }
                else
                {
                    alim = _db.allimentations.Where(p => p.AllimentationCaisse_AbonnementID == aboId && Convert.ToDateTime(p.AllimentationCaisse_DateCreation).ToString("dd/MM/yyyy") == date)
                           .AsEnumerable();
                }
            }
            else
            {
                if (pdvId != null)
                {
                    alim = _db.allimentations.Where(p => p.AllimentationCaisse_AbonnementID == aboId && p.AllimentationCaisse_PointVenteID == pdvId && p.AllimentationCaisse_DateCreation.Date == DateTime.Now.Date)
                          .AsEnumerable();

                }
                else
                {
                    alim = _db.allimentations.Where(p => p.AllimentationCaisse_AbonnementID == aboId && p.AllimentationCaisse_DateCreation.Date == DateTime.Now.Date)
                          .AsEnumerable();
                }

            }
            return alim;
        }
        public IEnumerable<RetourProduits> GetListRetours(int? pdvId, int aboId, string date)
        {
            IEnumerable<RetourProduits> retours = null;
            if (date != "")
            {
                if (pdvId != null)
                {
                    retours = _db.retourProduits.Where(p => p.Retour_AbonnementID == aboId && Convert.ToDateTime(p.Retour_DateCreation).ToString("dd/MM/yyyy") == date && p.Position_Vente.PositionVente_PointVenteId == pdvId)
                          .AsEnumerable();
                }
                else
                {
                    retours = _db.retourProduits.Where(p => p.Retour_AbonnementID == aboId && Convert.ToDateTime(p.Retour_DateCreation).ToString("dd/MM/yyyy") == date)
                          .AsEnumerable();
                }
            }
            else
            {
                if (pdvId != null)
                {
                    retours = _db.retourProduits.Where(p => p.Retour_AbonnementID == aboId && p.Position_Vente.PositionVente_PointVenteId == pdvId && p.Retour_DateCreation.Date == DateTime.Now.Date)
                           .AsEnumerable();

                }
                else
                {
                    retours = _db.retourProduits.Where(p => p.Retour_AbonnementID == aboId && p.Retour_DateCreation.Date == DateTime.Now.Date)
                           .AsEnumerable();
                }

            }
            return retours;
        }
        public IEnumerable<RetraitCaisse> GetListRetrait(int? pdvId, int aboId, string date)
        {
            IEnumerable<RetraitCaisse> retraits = null;
            if (date != "")
            {
                if (pdvId != null)
                {
                    retraits = _db.retraitCaisses.Where(p => p.RetraitCaisse_AbonnementID == aboId && Convert.ToDateTime(p.RetraitCaisse_DateCreation).ToString("dd/MM/yyyy") == date && p.Position_Vente.PositionVente_PointVenteId == pdvId)
                          .Include(p=>p.User).AsEnumerable();
                }
                else
                {
                    retraits = _db.retraitCaisses.Where(p => p.RetraitCaisse_AbonnementID == aboId && Convert.ToDateTime(p.RetraitCaisse_DateCreation).ToString("dd/MM/yyyy") == date)
                          .Include(p => p.User).AsEnumerable();
                }
            }
            else
            {
                if (pdvId != null)
                {
                    retraits = _db.retraitCaisses.Where(p => p.RetraitCaisse_AbonnementID == aboId && p.Position_Vente.PositionVente_PointVenteId == pdvId && p.RetraitCaisse_DateCreation.Date == DateTime.Now.Date)
                          .Include(p => p.User).AsEnumerable();

                }
                else
                {
                    retraits = _db.retraitCaisses.Where(p => p.RetraitCaisse_AbonnementID == aboId && p.RetraitCaisse_DateCreation.Date == DateTime.Now.Date)
                          .Include(p => p.User).AsEnumerable();
                }

            }
            return retraits;
        }

        public async Task<int?> UpdateVente(int caisseId, string date)
        {
            if (date != "")
            {
                var ventes = _db.venteDetails.Where(p => p.Vente.Vente_PositionVenteId == caisseId && Convert.ToDateTime(p.VenteDetails_DateCreation).ToString("dd/MM/yyyy") == date && p.VenteDetails_FlagCloture == 0)
                  .ToList();
                foreach (var v in ventes)
                    v.VenteDetails_FlagCloture = 1;

               var paiement =  _db.paiementCommandes.Where(p => p.CommandePaiement_PositionVenteID == caisseId && Convert.ToDateTime(p.CommandePaiement_DatePaiement).ToString("dd/MM/yyyy") == date && p.CommandePaiement_FlagCloture == 0)
                    .ToList();
                foreach (var v in paiement)
                    v.CommandePaiement_FlagCloture = 1;

                var retrait = _db.retraitCaisses.Where(p => p.RetraitCaisse_PositionVenteID == caisseId && Convert.ToDateTime(p.RetraitCaisse_DateCreation).ToString("dd/MM/yyyy") == date && p.RetraitCaisse_FlagCloture == 0)
                    .ToList();
                foreach (var v in retrait)
                    v.RetraitCaisse_FlagCloture = 1;

               var alli= _db.allimentations.Where(p => p.AllimentationCaisse_PositionVenteID == caisseId && Convert.ToDateTime(p.AllimentationCaisse_DateCreation).ToString("dd/MM/yyyy") == date && p.AllimentationCaisse_FlagCloture == 0)
                    .ToList();
                foreach (var v in alli)
                    v.AllimentationCaisse_FlagCloture = 1;
                var retour = _db.retourProduits.Where(p => p.Retour_PositionVenteID == caisseId && Convert.ToDateTime(p.Retour_DateCreation).ToString("dd/MM/yyyy") == date && p.Retour_FlagCloture == 0)
                    .ToList();
                foreach (var v in retour)
                    v.Retour_FlagCloture = 1;
            }
            else
            {
                var ventes = _db.venteDetails.Where(p => p.Vente.Vente_PositionVenteId == caisseId && p.VenteDetails_FlagCloture == 0)
                    .ToList();
                foreach (var v in ventes)
                    v.VenteDetails_FlagCloture = 1;

                var commande = _db.paiementCommandes.Where(p => p.CommandePaiement_PositionVenteID == caisseId && p.CommandePaiement_FlagCloture == 0)
                    .ToList();
                foreach (var v in commande)
                    v.CommandePaiement_FlagCloture = 1;
                var retrait = _db.retraitCaisses.Where(p => p.RetraitCaisse_PositionVenteID == caisseId && p.RetraitCaisse_FlagCloture == 0)
                   .ToList();
                foreach (var v in retrait)
                    v.RetraitCaisse_FlagCloture = 1;
                var allim = _db.allimentations.Where(p => p.AllimentationCaisse_PositionVenteID == caisseId && p.AllimentationCaisse_FlagCloture == 0)
                    .ToList();
                foreach (var v in allim)
                    v.AllimentationCaisse_FlagCloture=1;
                var retour = _db.retourProduits.Where(p => p.Retour_PositionVenteID == caisseId && p.Retour_FlagCloture == 0)
                    .ToList();
                foreach (var v in retour)
                    v.Retour_FlagCloture = 1;
            }
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return confirm;
            else
                return null;

        }

        public Vente findFormulaireVenteByticket(string ticketID)
        {
            return _db.vente.Where(p => p.Vente_NumeroTicket == ticketID)
                .Include(p=>p.Mode_Paiement)
                .Include(p=>p.Tva)
                .Include(p=>p.Details).FirstOrDefault();
        }
       public Commande findFormulaireCommandeByticket(string ticketID)
        {
            return _db.commandes.Where(p => p.Commande_NumeroTicket == ticketID)
                .Include(p=>p.User)
                .Include(p=>p.Statut_PaiementCommande)
                .Include(p=>p.Position_Vente)
                .Include(p=>p.Statut_Livraison)
                .Include(p => p.Tva)
                .Include(p=>p.details).FirstOrDefault();
        }
        public async Task<bool?> UpdateProduitsPDV(List<ProdsQteApi> prodsList, int pdvID)
        {
            foreach(var item in prodsList)
            {
                var PdvStock = _db.pointVente_Stocks.Where(p => p.PointVenteStock_FormeProduitID == item.formeID && p.PointVenteStock_PointVenteID == pdvID).FirstOrDefault();
                if (PdvStock == null)
                {
                    var pdvPret = _db.pdV_ProduitsPrets.Where(p=>p.PdV_ProduitsPret_PointVenteId == pdvID && p.PdV_ProduitsPret_FormeProduitId == item.formeID).FirstOrDefault();
                    if (pdvPret != null)
                    {
                        pdvPret.PdV_ProduitsPret_Quantite += item.quantite;
                        _db.Entry(pdvPret).State = EntityState.Modified;
                    }
                    else
                    {
                        var pdvPackage = _db.formeDetails.Where(p => p.FormeDetails_PointVenteID == pdvID && p.FormeDetails_FormeProduitID == item.formeID).FirstOrDefault();
                        pdvPackage.FormeDetails_Quantite += item.quantite;
                        _db.Entry(pdvPackage).State = EntityState.Modified;
                    }
                }
                else
                {
                    PdvStock.PointVenteStock_QuantiteProduit += item.quantite;
                    _db.Entry(PdvStock).State = EntityState.Modified;
                }
                
            }
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return true;
            else
                return false;
        }
    }    
}
