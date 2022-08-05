using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.IRepositories;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ProduitFicheTechniqueRepository : IProduitFicheTechniqueRepository
    {
        private readonly IProduitVendableRepository produitVendableRepository;
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork unitOfWork;

        public ProduitFicheTechniqueRepository(ApplicationDbContext db, IUnitOfWork unitOfWork, IProduitVendableRepository produitVendableRepository)
        {
            this.produitVendableRepository = produitVendableRepository;
            this.unitOfWork = unitOfWork;
            _db = db;
        }

        public async Task<int?> CreateFiche(FicheTechniqueBridge fiche)
        {
            fiche.FicheTechniqueBridge_IsActive = 1;
            fiche.FicheTechniqueBridge_DateCreation = DateTime.Now;
            await _db.ficheTechniqueBridges.AddAsync(fiche);
            await unitOfWork.Complete();
            var result = await SetInUse(fiche.FicheTechniqueBridge_ID);
            if (result == true)
            {
                return fiche.FicheTechniqueBridge_ID;
            }
            else
                return null;
        }


        public async Task<bool> deleteFicheTechnique(int ID)
        {
            var fiche = _db.ficheTechniqueBridges.Where(e => e.FicheTechniqueBridge_ID == ID).FirstOrDefault();
            if (fiche != null)
            {
                fiche.FicheTechniqueBridge_InUse = false;
                fiche.FicheTechniqueBridge_IsActive = 0;
                _db.Entry(fiche).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public IEnumerable<FicheTechniqueBridge> findFormulaireFiche(int formulaireFicheId, int AboId)
        {
            var res = _db.ficheTechniqueBridges
                .Where(e => e.FicheTechniqueBridge_ProduitVendableID == formulaireFicheId)
                .Where(e => e.FicheTechniqueBridge_AbonnementID == AboId)
                .Include(u => u.Produit_Vendable).AsEnumerable();
            return res;

        }
        public FicheTechniqueBridge findFormulaireFiche(int formulaireFicheId)
        {
            return _db.ficheTechniqueBridges.Where(e => e.FicheTechniqueBridge_ID == formulaireFicheId).FirstOrDefault();

        }
        public IEnumerable<ProduitFicheTechnique> findFormulaireFicheById(int formulaireFicheId)
        {
            var Fiche = (from u in _db.produitFicheTechniques
                         where u.FicheTechnique_ProduitVendableId == formulaireFicheId
                         select new ProduitFicheTechnique()
                         {
                             FicheTechnique_Libelle = u.FicheTechnique_Libelle,
                             FicheTechnique_DateCreation = u.FicheTechnique_DateCreation,
                             FicheTechnique_Prix = u.FicheTechnique_Prix
                         }
                          ).ToList();
            var result = Fiche.GroupBy(x => x.FicheTechnique_Libelle).Select(y => y.First());

            return result;
        }


        public ProduitFicheTechnique findFormulaireFicheByLibelle(string Libelle)
        {
            return _db.produitFicheTechniques.Where(e => e.FicheTechnique_Libelle == Libelle).FirstOrDefault();
        }

        public IEnumerable<ProduitFicheTechnique> getListFicheTechnique(int Id, int AboId)
        {
            var res =  _db.produitFicheTechniques/*.Where(a => a.FicheTechnique_IsActive == 1)*/.Where(a => a.FicheTechnique_AbonnementId == AboId).Where(a => a.FicheTechnique_FicheTechniqueBridgeID == Id)
                .Include(e => e.Unite_Mesure)
                .Include(e => e.Matiere_Premiere)
                .Include(e => e.Produit_Vendable)
                .Include(e => e.FicheTechnique_Bridge)
                .AsEnumerable();
            return res;
        }
        public IEnumerable<FicheTechniqueBridge> getListFicheTechniqueAll(int Id,int? categ,int? SousCateg)
        {
            IEnumerable<FicheTechniqueBridge> produits;
            if (categ != null)
            {
                if (SousCateg != null)
                {
                    produits = _db.ficheTechniqueBridges.Where(a => a.FicheTechniqueBridge_IsActive == 1 && a.FicheTechniqueBridge_InUse == true)
                        .Where(a => a.FicheTechniqueBridge_AbonnementID == Id && a.Produit_Vendable.ProduitVendable_FamilleProduitId==SousCateg)                
                        .Include(e => e.Produit_Vendable).AsEnumerable();
                }
                else
                {
                    produits = _db.ficheTechniqueBridges.Where(a => a.FicheTechniqueBridge_IsActive == 1 && a.FicheTechniqueBridge_InUse == true)
                         .Where(a => a.FicheTechniqueBridge_AbonnementID == Id && a.Produit_Vendable.Sous_Famille.SousFamille_ParentID == categ)
                         .Include(e => e.Produit_Vendable).AsEnumerable();
                }
            }
            else
            {
                if (SousCateg != null)
                {
                    produits = _db.ficheTechniqueBridges.Where(a => a.FicheTechniqueBridge_IsActive == 1 && a.FicheTechniqueBridge_InUse == true)
                         .Where(a => a.FicheTechniqueBridge_AbonnementID == Id && a.Produit_Vendable.ProduitVendable_FamilleProduitId == SousCateg)
                         .Include(e => e.Produit_Vendable).AsEnumerable();
                }
                else
                {
                    produits = _db.ficheTechniqueBridges.Where(a => a.FicheTechniqueBridge_IsActive == 1 && a.FicheTechniqueBridge_InUse == true)
                        .Where(a => a.FicheTechniqueBridge_AbonnementID == Id)
                        .Include(e => e.Produit_Vendable).AsEnumerable();
                }
            }
            return produits;
        }

        public IEnumerable<FicheDetailsModel> getListFicheTechniqueByLibelle(string Libelle, int Id)
        {
            var Fiche = (from u in _db.produitFicheTechniques
                         where u.FicheTechnique_Libelle == Libelle
                         where u.FicheTechnique_AbonnementId == Id
                         select new FicheDetailsModel()
                         {
                             FicheTechnique_MatierePremiere = u.Matiere_Premiere.MatierePremiere_Libelle,
                             FicheTechnique_QuantiteMatiere = u.FicheTechnique_QuantiteMatiere,
                             FicheTechnique_UniteMesure = u.Unite_Mesure.UniteMesure_Libelle,
                             FicheTechnique_Prix = u.FicheTechnique_Prix


                         }
                          ).ToList();
            return Fiche;
        }

        public IEnumerable<MatierePremiere> getListMatierePremiere(int Id)
        {
            return _db.matierePremieres.Where(a => a.MatierePremiere_IsActive == 1).Where(a => a.MatierePremiere_AbonnementId == Id).AsEnumerable();
        }

        public IEnumerable<ProduitVendable> getListProduitVendable(int Id)
        {
            return _db.produitVendables.Where(a => a.ProduitVendable_IsActive == 1).Where(a => a.ProduitVendable_AbonnementId == Id).AsEnumerable();
        }

        public IEnumerable<Unite_Mesure> getListUniteMesure()
        {
            return _db.unite_Mesures.AsEnumerable();
        }



        public async Task<bool> updateFormulaireFicheTechnique(int id, ProduitFicheTechnique newFiche)
        {
            ProduitFicheTechnique fiche = _db.produitFicheTechniques.Where(e => e.FicheTechnique_Id == id).FirstOrDefault();
            if (fiche != null)
            {
                fiche.FicheTechnique_ProduitVendableId = newFiche.FicheTechnique_ProduitVendableId;
                fiche.FicheTechnique_MatierePremiereId = newFiche.FicheTechnique_MatierePremiereId;
                fiche.FicheTechnique_QuantiteMatiere = newFiche.FicheTechnique_QuantiteMatiere;
                fiche.FicheTechnique_UniteMesureId = newFiche.FicheTechnique_UniteMesureId;
                fiche.FicheTechnique_Prix = newFiche.FicheTechnique_Prix;
                fiche.FicheTechnique_IsActive = 1;
                _db.Entry(fiche).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }
        public async Task<bool> SetInUse(int id)
        {

            FicheTechniqueBridge fiche = _db.ficheTechniqueBridges.Where(e => e.FicheTechniqueBridge_ID == id)
                .Include(p=>p.Produit_FicheTechnique).ThenInclude(p=>p.Matiere_Premiere)
                .Include(p => p.Fiche_Forme).Include(p=>p.FicheTech_ProduitBase).ThenInclude(p=>p.ProduitBase).FirstOrDefault();
            foreach(var item in fiche.Produit_FicheTechnique)
            {
                if(item.Matiere_Premiere.MatierePremiere_IsActive == 0)
                {
                    return false;
                }
            }
            IEnumerable<FicheTechniqueBridge> bridges = _db.ficheTechniqueBridges.Where(e => e.FicheTechniqueBridge_ProduitVendableID == fiche.FicheTechniqueBridge_ProduitVendableID).AsEnumerable();
            foreach (var bridge in bridges)
            {
                bridge.FicheTechniqueBridge_InUse = false;
                _db.Entry(bridge).State = EntityState.Modified;

            }
            if (fiche != null)
            {
                if (fiche.FicheTech_ProduitBase.Count() > 0)
                {
                    decimal prix = 0;
                    foreach (var fb in fiche.FicheTech_ProduitBase)
                    {
                        fb.IsActive = 1;
                        fb.Abonnement_ID = fiche.FicheTechniqueBridge_AbonnementID;
                    
                        if (fb.UniteMesure_ID == 1 && fb.ProduitBase.ProduitBase_UniteMesureID == 2 || fb.UniteMesure_ID == 4 && fb.ProduitBase.ProduitBase_UniteMesureID == 3)
                            prix += fb.ProduitBase.ProduitBase_CoutDeRevient * fb.ProduitQte / 1000;
                        else if (fb.UniteMesure_ID == 2 && fb.ProduitBase.ProduitBase_UniteMesureID == 1 || fb.UniteMesure_ID == 3 && fb.ProduitBase.ProduitBase_UniteMesureID == 4)
                            prix += fb.ProduitBase.ProduitBase_CoutDeRevient * fb.ProduitQte * 1000;
                        else
                            prix += fb.ProduitBase.ProduitBase_CoutDeRevient * fb.ProduitQte;
                    }

                    fiche.FicheTechniqueBridge_CoutDeRevient += prix;
                    foreach (var ff in fiche.Fiche_Forme)
                    {
                        ff.FicheForme_CoutDeRevient = fiche.FicheTechniqueBridge_CoutDeRevient / ff.FicheForme_Quantite;
                    }
                }
                
                fiche.FicheTechniqueBridge_InUse = true;
                var produit = _db.produitVendables
                .Where(p => p.ProduitVendable_Id == fiche.FicheTechniqueBridge_ProduitVendableID)
                .Include(p => p.formes)
                .FirstOrDefault();
                produit.ProduitVendable_AvecFT = 1;
                _db.Entry(produit).State = EntityState.Modified;
                if (produit.ProduitVendable_PlanificationFlag == 0)
                {
                    var pointvente = _db.point_Ventes
                        .Where(p => p.PointVente_AbonnementId == fiche.FicheTechniqueBridge_AbonnementID)
                        .AsEnumerable();
                    foreach (var p in pointvente)
                    {
                        foreach (var forme in produit.formes)
                        {
                            var pv = _db.pointVente_Stocks.Where(x => x.PointVenteStock_FormeProduitID == forme.FormeProduit_ID)
                                 .FirstOrDefault();
                            if (pv == null)
                            {
                                PointVente_Stock pv_stock = new PointVente_Stock
                                {
                                    PointVenteStock_AbonnementID = fiche.FicheTechniqueBridge_AbonnementID,
                                    PointVenteStock_FormeProduitID = forme.FormeProduit_ID,
                                    PointVenteStock_ProduitID = fiche.FicheTechniqueBridge_ProduitVendableID,
                                    PointVenteStock_PointVenteID = p.PointVente_Id,
                                    PointVenteStock_QuantiteProduit = 0,
                                    PointVenteStock_DateModification = DateTime.Now,
                                };
                                await _db.pointVente_Stocks.AddAsync(pv_stock);
                            }
                          
                        }
                    }
                }
                await produitVendableRepository.updateCoutDeRevient(fiche.FicheTechniqueBridge_ProduitVendableID, fiche.FicheTechniqueBridge_CoutDeRevient, fiche.FicheTechniqueBridge_ID);

                _db.Entry(fiche).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public IEnumerable<Forme_Produit> getListFormes(int produitId)
        {
            return _db.forme_Produits.Where(f => f.FormeProduit_ProduitID == produitId).AsEnumerable();
        }

        public IEnumerable<FicheForme> GetFicheFormes(int formulaireFicheId)
        {
            return _db.ficheFormes.Where(p => p.FicheForme_FicheBridge == formulaireFicheId)
                .Where(p => p.FicheForme_IsActive == 1)
                .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                .Include(p=>p.Unite_Mesure)
                .Include(p => p.FicheTechnique_Bridge).AsEnumerable();

        }

        public async Task<int?> CreateFicheBase(FicheTechniqueProduitBase ficheBase)
        {
            ficheBase.FicheTechniqueProduitBase_IsActive = 1;
            ficheBase.FicheTechniqueProduitBase_DateCreation = DateTime.Now;
            //ficheBase.FicheTechniqueProduitBase_CoutDeRevient *= ficheBase.FicheTechniqueProduitBase_QuantiteProd;  
            await _db.ficheTechniqueProduitBases.AddAsync(ficheBase);
            await unitOfWork.Complete();
            var result = await SetInUseBase(ficheBase.FicheTechniqueProduitBase_ID);
            if (result == true) 
            {
                return ficheBase.FicheTechniqueProduitBase_ID;
            }
            else
                return null;
        }

        public IEnumerable<FicheTechniqueProduitBase> getListFicheTechniqueBaseAll(int Id, int? categ, int? SousCateg)
        {
            return _db.ficheTechniqueProduitBases.Where(p => p.FicheTechniqueProduitBase_AbonnementID == Id)
                .Where(p => p.FicheTechniqueProduitBase_IsActive == 1)
                .Include(p=>p.Unite_Mesure)
                .Include(p => p.ProduitBase).AsEnumerable();
        }

        public IEnumerable<FicheTechniqueProduitBase> findFormulaireFicheBase(int formulaireFicheBaseId, int aboId)
        {

            return _db.ficheTechniqueProduitBases.Where(p => p.FicheTechniqueProduitBase_ProduitBaseID == formulaireFicheBaseId)                               
                .Include(p => p.Unite_Mesure)
                .Where(p => p.FicheTechniqueProduitBase_AbonnementID == aboId).Include(p => p.ProduitBase).AsEnumerable();
        }

        public IEnumerable<ProduitBaseFicheTechnique> getListFicheTechniqueBase(int Id, int AboId)
        {
            return _db.produitBaseFicheTechniques.Where(p => p.ProduitBaseFicheTechnique_FicheTehcniqueProduitBaseID == Id && p.ProduitBaseFicheTechnique_AbonnementId == AboId)
                .Include(p=>p.FicheTechniqueProduitBase)
                .Include(p=>p.Matiere_Premiere).Include(p=>p.Unite_Mesure)
                .AsEnumerable();
        }
        public async Task<bool> SetInUseBase(int id)
        {

            FicheTechniqueProduitBase fiche = _db.ficheTechniqueProduitBases.Where(e => e.FicheTechniqueProduitBase_ID == id).FirstOrDefault();
            if (fiche != null)
            {
                IEnumerable<FicheTechniqueProduitBase> bridges = _db.ficheTechniqueProduitBases.Where(e => e.FicheTechniqueProduitBase_ProduitBaseID == fiche.FicheTechniqueProduitBase_ProduitBaseID).AsEnumerable();
                foreach (var bridge in bridges)
                {
                    bridge.FicheTechniqueProduitBase_InUse = false;
                    _db.Entry(bridge).State = EntityState.Modified;

                }
                fiche.FicheTechniqueProduitBase_InUse = true;
                _db.Entry(fiche).State = EntityState.Modified;
                await unitOfWork.Complete();
                var confirm = await produitVendableRepository.updateCoutDeRevientBase(fiche.FicheTechniqueProduitBase_ProduitBaseID, fiche.FicheTechniqueProduitBase_CoutDeRevient, fiche.FicheTechniqueProduitBase_ID);
                if (confirm == true)
                    return true;
                else
                    return false;
            }
            return false;
        }
        public decimal CalculerPrix(int prodId, int uniteID, decimal qte)
        {
            var prod = _db.produitBases.Where(p => p.ProduitBase_ID == prodId).FirstOrDefault();

            decimal cdr = prod.ProduitBase_CoutDeRevient * qte;
            if (prod.ProduitBase_UniteMesureID == 1 && uniteID == 2 || prod.ProduitBase_UniteMesureID == 4 && uniteID == 3)
                cdr = cdr * 1000;
            else if (prod.ProduitBase_UniteMesureID == 2 && uniteID == 1 || prod.ProduitBase_UniteMesureID == 3 && uniteID == 4)
                cdr = cdr / 1000;
            return cdr;

        }
    }
    
}
