using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.Data;
using Repository.IRepositories;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ProduitVendableRepository : IProduitVendableRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper mapper;

        private readonly IUnitOfWork unitOfWork;
        public ProduitVendableRepository(ApplicationDbContext db, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            _db = db;
        }
        public async Task<int?> CreateProduit(ProduitVendable produitVendable)
        {
            produitVendable.ProduitVendable_IsActive = 1;
            produitVendable.ProduitVendable_DateCreation = DateTime.Now;
            produitVendable.ProduitVendable_TvaId = 3;
            /*            var forme = new Forme_Produit
                        {
                            FormeProduit_Libelle = "Standard",
                            FormeProduit_AbonnementID = (int)produitVendable.ProduitVendable_AbonnementId,
                            FormeProduit_DateCreation = (DateTime)produitVendable.ProduitVendable_DateCreation,
                          //  FormeProduit_ProduitID = produitVendable.ProduitVendable_Id,
                        };
                        produitVendable.formes.Add(forme);*/
            await _db.produitVendables.AddAsync(produitVendable);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return produitVendable.ProduitVendable_Id;
            else
                return null;
        }

        public async Task<bool> deleteProduitVendable(int ID)
        {
            ProduitVendable produit = _db.produitVendables.Where(e => e.ProduitVendable_Id == ID).FirstOrDefault();
            if (produit != null)
            {
                produit.ProduitVendable_IsActive = 0;
                _db.Entry(produit).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> deleteProduitConso(int ID)
        {
            ProduitPretConsomer produit = _db.produitPrets.Where(e => e.ProduitPretConsomer_ID == ID).FirstOrDefault();
            if (produit != null)
            {
                produit.ProduitPretConsomer_IsActive = 0;
                _db.Entry(produit).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> deleteProduitVendablesStockes(int ID)
        {
            ProduitConsomableStokage produitStocke = _db.produitConsomableStokages.Where(e => e.ProduitConsomableStokage_Id == ID).FirstOrDefault();
            if (produitStocke != null)
            {
                produitStocke.ProduitConsomableStokage_IsActive = 0;
                _db.Entry(produitStocke).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public ProduitVendable findFormulaireProduit(int formulaireProduitId)
        {
            return _db.produitVendables.Where(e => e.ProduitVendable_Id == formulaireProduitId)
                .Include(p=>p.Unite_Mesure)
                .FirstOrDefault();
        } 
        public ProduitVendable findFormulaireProduitPDF(int formulaireProduitId)
        {
            return _db.produitVendables.Where(e => e.ProduitVendable_Id == formulaireProduitId)
                .Include(p=>p.Unite_Mesure)
                .FirstOrDefault();
        }

        public IEnumerable<FamilleProduit> getListFamilleProduit(int Id)
        {
            return _db.familleProduits.Where(p=>p.FamilleProduit_AbonnemnetId==Id).AsEnumerable();
        }

        public IEnumerable<Forme_Stockage> getListFormeSotckage(int Id)
        {
           // return _db.forme_Stockages.Where(p=>p.FormStockage_AbonnementId== Id).AsEnumerable();
            return _db.forme_Stockages.AsEnumerable();
        }

        public IEnumerable<ProduitConsomableStokage> getListProduitsStockes(int Id)
        {
            return _db.produitConsomableStokages.Where(a => a.ProduitConsomableStokage_IsActive == 1).Where(a => a.ProduitConsomableStokage_AbonnementId == Id)
                .Include(s => s.Lieu_Stockage).ThenInclude(s => s.Ville)
                .Include(p => p.Produit_PretConsomer)
                .Include(p => p.Forme_Produit).AsEnumerable();
        }
        public ProduitConsomableStokage finformulaireProduitConsomableStockage(int produitConsoStockageId)
        {
            var prod =  _db.produitConsomableStokages.Where(a => a.ProduitConsomableStokage_ProduitVendableId == produitConsoStockageId)
                .Include(p => p.Produit_PretConsomer).FirstOrDefault();
            return prod;
        } 
        public IEnumerable<ProduitConsomableStokage> getListProduitsStockesByAtelier(int? Id)
        {
            return _db.produitConsomableStokages.Where(a => a.ProduitConsomableStokage_IsActive == 1 && a.ProduitConsomableStokage_LieuStockID == Id)
                .Where(a =>a.Produit_PretConsomer.ProduitPretConsomer_IsActive == 1 )
                .Include(s => s.Lieu_Stockage).ThenInclude(s => s.Ville)
                .Include(p => p.Produit_PretConsomer).ThenInclude(p=>p.Unite_Mesure)
                .Include(p => p.Forme_Produit).AsEnumerable();
        }   
        public IEnumerable<ProduitConsomableStokage> getListProduitsStockesByProduit(int? Id,int produitId)
        {
            return _db.produitConsomableStokages.Where(a => a.ProduitConsomableStokage_IsActive == 1)
                .Where(a => a.ProduitConsomableStokage_LieuStockID == Id && a.ProduitConsomableStokage_ProduitVendableId == produitId)
                .Include(p => p.Forme_Produit).AsEnumerable();
        } 
        public IEnumerable<PdV_ProduitsPret> getListProduitsStockesPdv(int Id, int formeId)
        {
            return _db.pdV_ProduitsPrets.Where(a => a.PdV_ProduitsPret_AbonnementId == Id && a.PdV_ProduitsPret_FormeProduitId==formeId && a.Produit_PretConsomer.ProduitPretConsomer_IsActive==1)
                .Include(s => s.Point_Vente).ThenInclude(s => s.Ville)
                .Include(p => p.Produit_PretConsomer).ThenInclude(p=>p.Unite_Mesure)
                .Include(p => p.Forme_Produit).AsEnumerable();
        }
        public IEnumerable<ProduitVendable> getListProduitVendable(int Id, int? categ, int? SousCateg, string name)
        {
            IEnumerable<ProduitVendable> produits;            
            if (categ != null)
            {
                if (SousCateg != null)
                {
                    produits = _db.produitVendables
                        .Where(a => a.ProduitVendable_IsActive == 1)
                        .Where(a => a.ProduitVendable_AbonnementId == Id && a.ProduitVendable_FamilleProduitId == SousCateg)
                        .Include(e => e.Unite_Mesure)
                        .Include(e => e.Sous_Famille)
                        .ThenInclude(s => s.Famille_Produit)
                        .Include(e => e.Forme_Stockage)
                        .AsEnumerable();
                }
                else
                {
                    produits = _db.produitVendables
                        .Where(a => a.ProduitVendable_IsActive == 1)
                        .Where(a => a.ProduitVendable_AbonnementId == Id && a.Sous_Famille.SousFamille_ParentID == categ)
                        .Include(e => e.Unite_Mesure)
                        .Include(e => e.Sous_Famille)
                        .ThenInclude(s => s.Famille_Produit)
                        .Include(e => e.Forme_Stockage)
                        .AsEnumerable();
                }
            }
            else
            {
                if (SousCateg != null)
                {
                    produits = _db.produitVendables
                        .Where(a => a.ProduitVendable_IsActive == 1)
                        .Where(a => a.ProduitVendable_AbonnementId == Id && a.ProduitVendable_FamilleProduitId == SousCateg)
                        .Include(e => e.Unite_Mesure)
                        .Include(e => e.Sous_Famille)
                        .ThenInclude(s => s.Famille_Produit)
                        .Include(e => e.Forme_Stockage)
                        .AsEnumerable();
                }
                else
                {
                    produits = _db.produitVendables
                        .Where(a => a.ProduitVendable_IsActive == 1)
                        .Where(a => a.ProduitVendable_AbonnementId == Id)
                        .Include(e => e.Unite_Mesure)
                        .Include(e => e.Sous_Famille)
                        .ThenInclude(s => s.Famille_Produit)
                        .Include(e => e.Forme_Stockage)
                        .AsEnumerable();
                }
            }
            
            return produits;

        }
        public IEnumerable<ProduitVendable> getListProduitVendableAvecPlanif(int Id,int AboId)
        {
            var famillesAll = _db.familleProduits.Where(f => f.FamilleProduit_AbonnemnetId == AboId).Where(f => f.FamilleProduit_IsActive == 1).Include(fm => fm.production_Link).ToList();
            var familles = new List<FamilleProduit>();
            foreach (var m in famillesAll)
            {
                foreach (var fm in m.production_Link)
                {
                    if (Id == fm.PointProduction_ID)
                    {
                        bool alreadyExists = familles.Any(x => x.FamilleProduit_Id == m.FamilleProduit_Id);
                        if (!alreadyExists)
                            familles.Add(m);
                    }
                }
            }
            var SousFamillesAll = _db.sousFamilles
                .Where(ms => ms.SousFamille_AbonnementID == AboId)
                .Include(ms => ms.Famille_Produit)
                .AsEnumerable();
            var sousfamilleList = new List<SousFamille>();
            foreach (var ms in SousFamillesAll)
            {
                foreach (var m in familles)
                {
                    if (ms.Famille_Produit.FamilleProduit_Id == m.FamilleProduit_Id)
                    {
                        sousfamilleList.Add(ms);
                    }
                }

            }
            var produitsAll = _db.produitVendables
                .Where(a => a.ProduitVendable_IsActive == 1 && a.ProduitVendable_AbonnementId == AboId)
                .Where(a=>a.ProduitVendable_AvecFT == 1 && a.ProduitVendable_PlanificationFlag == 1)
                .AsEnumerable();
            var produitList = new List<ProduitVendable>();
            foreach (var ms in produitsAll)
            {
                foreach (var m in sousfamilleList)
                {
                    if (ms.Sous_Famille.SousFamille_ID == m.SousFamille_ID)
                    {
                        produitList.Add(ms);
                    }
                }

            }
            return produitList;
        }

        public IEnumerable<Section_Stockage> getListSectionStockage()
        {
            return _db.section_Stockages.AsEnumerable();
        }

        public IEnumerable<Unite_Mesure> getListUniteMesure()
        {
            return _db.unite_Mesures.AsEnumerable();
        }


        public async Task<int?> StockerProduit(int Id, ProduitConsomableStokage produitVendableStockage)
        {
            if (produitVendableStockage.ProduitConsomableStokage_StockMinimum > produitVendableStockage.ProduitConsomableStokage_StockMaximum)
                return null;

            var produitTest = _db.produitConsomableStokages.Where(m => m.ProduitConsomableStokage_ProduitVendableId == Id).Where(m => m.ProduitConsomableStokage_LieuStockID == produitVendableStockage.ProduitConsomableStokage_LieuStockID)
               .FirstOrDefault();
            if (produitTest == null)
            {
                produitVendableStockage.ProduitConsomableStokage_ProduitVendableId = Id;
                produitVendableStockage.ProduitConsomableStokage_IsActive = 1;
                produitVendableStockage.ProduitConsomableStokage_DateCreation = DateTime.Now;
                ProduitPretConsomer produit = _db.produitPrets.Where(e => e.ProduitPretConsomer_ID == Id).FirstOrDefault();
                if (produit != null)
                {
                    produit.ProduitPretConsomer_EnStock = true;
                    _db.Entry(produit).State = EntityState.Modified;

                }
                await _db.produitConsomableStokages.AddAsync(produitVendableStockage);
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return produitVendableStockage.ProduitConsomableStokage_Id;
                else
                    return null;
            }
            else
                return null;
        }

        public async Task<bool> updateFormulaireProduitVendable(int id, ProduitVendable newProduit)
        {
            ProduitVendable produit = _db.produitVendables.Where(e => e.ProduitVendable_Id == id).FirstOrDefault();
            if (produit != null)
            {
                produit.ProduitVendable_FamilleProduitId = newProduit.ProduitVendable_FamilleProduitId;
                produit.ProduitVendable_Designation = newProduit.ProduitVendable_Designation;
                produit.ProduitVendable_Description = newProduit.ProduitVendable_Description;
                produit.ProduitVendable_FamilleProduitId = newProduit.ProduitVendable_FamilleProduitId;
                produit.ProduitVendable_FormStockageId = newProduit.ProduitVendable_FormStockageId;
                produit.ProduitVendable_UniteMesureId = newProduit.ProduitVendable_UniteMesureId;
                produit.ProduitVendable_StockMaximum = newProduit.ProduitVendable_StockMaximum;
                produit.ProduitVendable_StockMinimum = newProduit.ProduitVendable_StockMinimum;
                produit.ProduitVendable_DelaiConsommation = newProduit.ProduitVendable_DelaiConsommation;
                produit.ProduitVendable_CodeBarre = newProduit.ProduitVendable_CodeBarre;
                produit.ProduitVendable_GrandePhoto = newProduit.ProduitVendable_GrandePhoto;
                produit.ProduitVendable_CodeProduit = newProduit.ProduitVendable_CodeProduit;
                produit.ProduitVendable_TempConservation = newProduit.ProduitVendable_TempConservation;
                produit.ProduitVendable_Conditionnement = newProduit.ProduitVendable_Conditionnement;
                produit.ProduitVendable_Specification = newProduit.ProduitVendable_Specification;
                //produit.ProduitVendable_PetitePhoto = newProduit.ProduitVendable_PetitePhoto;
                produit.ProduitVendable_IsActive = 1;
                //produit.Produit_CoutDeRevient = 0;
                _db.Entry(produit).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public bool ProduitStocker(int id)
        {
            var res = _db.produitConsomableStokages.Where(p => p.ProduitConsomableStokage_ProduitVendableId == id).AsEnumerable();
            if (res != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> updateCoutDeRevient(int ProduitId, decimal coutDeRevient, int fichId)
        {
            var formesProduit = _db.forme_Produits.Where(e => e.FormeProduit_ProduitID == ProduitId).AsEnumerable();
            if (formesProduit != null)
            {
                foreach (var fp in formesProduit)
                {
                    var ficheForme = _db.ficheFormes.Where(e => e.FicheForme_FormeProduit == fp.FormeProduit_ID).Where(e => e.FicheForme_FicheBridge == fichId).FirstOrDefault();
                    if (ficheForme != null)
                    {
                        fp.FormeProduit_CoutDeRevient = ficheForme.FicheForme_CoutDeRevient;
                        _db.Entry(fp).State = EntityState.Modified;
                    }
                }

                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }
        public async Task<bool> updateCoutDeRevientBase(int ProduitId, decimal coutDeRevient, int fichId)
        {
            var produits = _db.produitBases.Where(e => e.ProduitBase_ID == ProduitId).FirstOrDefault();
            if (produits != null)
            {
                var fiche = _db.ficheTechniqueProduitBases.Where(e => e.FicheTechniqueProduitBase_ID == fichId).FirstOrDefault();
                produits.ProduitBase_CoutDeRevient = fiche.FicheTechniqueProduitBase_CoutDeRevient / fiche.FicheTechniqueProduitBase_QuantiteProd;
                _db.Entry(produits).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public IEnumerable<PlanificationJournee> getListPansDemandes(int Id, int adresse)
        {
            var plans = _db.planificationJournees
                .Where(a => a.PlanificationJournee_AbonnementID == Id && a.PlanificationJournee_Etat != "Annulée" )
                .Where(p=>p.PlanificationJournee_AtelierID == adresse)
                .Include(p=>p.Planification_Production)
                .Include(e => e.BonDe_Sortie)
                .Include(p => p.Atelier)
                .Include(p => p.Lieu_Stockage).OrderByDescending(p => p.PlanificationJournee_Date)
                .AsEnumerable();
            List<PlanificationJournee> pp = new List<PlanificationJournee>();
            foreach (var item in plans)
            {
                var i = 0;
                foreach(var prod in item.Planification_Production)
                {
                    if(prod.PlanificationProduction_QuantiteProduite > 0)
                    {
                        i++;
                    }
                }
                if(item.Planification_Production.Count() > i)
                {
                    pp.Add(item);
                }
            }
            
            return pp.AsEnumerable();
        } 

        public async Task<MatStockFlagModel> CloturerDemande(int demandeID)
        {
            var demande = _db.demandes.Where(p => p.Demande_ID == demandeID)
                .Include(p=>p.BonDe_Sortie).ThenInclude(p=>p.Bon_Details).ThenInclude(p=>p.Matiere_Premiere)
                .FirstOrDefault();
            decimal test = -0.01M;
            var matStock = new MatStockFlagModel();
            foreach (var item in demande.BonDe_Sortie.Bon_Details)
            {
                var matstock = _db.MatiereStock_Ateliers.Where(p => p.MatiereStockAtelier_MatierePremiereID == item.BonDeSortie_MatiereId).FirstOrDefault();
                if (matstock != null)
                {
                    demande.Demande_Etat = "Cloturée";
                    _db.Entry(demande).State = EntityState.Modified;
                    decimal qte = 0;
                    if ((item.BonDeSortie_UniteMesureId == 1 && matstock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 2) || (item.BonDeSortie_UniteMesureId == 4 && matstock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 3))
                        qte = item.BonDeSortie_QuantiteDemandee / 1000;
                    else if ((item.BonDeSortie_UniteMesureId == 2 && matstock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 1) || (item.BonDeSortie_UniteMesureId == 3 && matstock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 4))
                        qte = item.BonDeSortie_QuantiteDemandee * 1000;
                    else
                        qte = item.BonDeSortie_QuantiteDemandee;

                    var qterest = Decimal.Subtract(Convert.ToDecimal(matstock.MatiereStockAtelier_QauntiteActuelle.ToString("0.00")), qte);
                    if (Convert.ToDecimal(qterest.ToString("0.00")) < test)
                    {
                        matStock.flag = true;
                        matstock.MatiereStockAtelier_QauntiteActuelle = 0;
                    }
                    else if (Convert.ToDecimal(qterest.ToString("0.00")) >= test && Convert.ToDecimal(qterest.ToString("0.00")) <= 0)
                    {
                        matStock.flag = true;
                        matstock.MatiereStockAtelier_QauntiteActuelle = 0;
                        _db.Entry(matstock).State = EntityState.Modified;
                    }
                    else
                    {
                        matStock.flag = true;
                        matstock.MatiereStockAtelier_QauntiteActuelle = qterest;
                        _db.Entry(matstock).State = EntityState.Modified;
                    }
                }
                else
                {
                    var res = matStock.Matieres.Where(x => x.matID == item.Matiere_Premiere.MatierePremiere_Id).FirstOrDefault();
                    if (res == null)
                    {
                        var matieres = new matStockViewModel()
                        {
                            matID = item.Matiere_Premiere.MatierePremiere_Id,
                            matiereLibelle = item.Matiere_Premiere.MatierePremiere_Libelle,
                            qteLivrer = item.BonDeSortie_QuantiteLivree,
                            qteEnStock = 0,
                            uniteStock = item.Unite_Mesure.UniteMesure_Libelle,
                            uniteLivrer = item.Unite_Mesure.UniteMesure_Libelle,
                        };
                        matStock.Matieres.Add(matieres);
                    }
                    else
                    {
                        res.qteLivrer += item.BonDeSortie_QuantiteLivree;
                    }

                    matStock.flag = false;
                }
            }
            if (matStock.Matieres.Count() > 0 || matStock.flag == false)
            {
                matStock.flag = false;
                return matStock;

            }
            else
            {
                matStock.flag = true;
                var confirm = await unitOfWork.Complete();
                return matStock;

            }
        }
           
        

        public IEnumerable<MatierePremiere> fillMatiereDemande(int planJourneeID)
        {
            var plans = _db.planificationJournees
                .Where(a => a.PlanificationJournee_ID == planJourneeID)
                .Include(e => e.BonDe_Sortie).ThenInclude(p=>p.Bon_Details).ThenInclude(p=>p.Matiere_Premiere)
                .FirstOrDefault();
            if(plans != null)
                return plans.BonDe_Sortie.Bon_Details.Select(p => p.Matiere_Premiere);
            else
                return null;
        } 
        public IEnumerable<PlanificationJournee> getListPansRetours(int Id)
        {
            var plans = _db.planificationJournees
                .Where(a => a.PlanificationJournee_AbonnementID == Id && a.PlanificationJournee_Etat == "Recue")
                .Include(e => e.BonDe_Sortie)
                .Include(p => p.Atelier)
                .Include(p => p.Lieu_Stockage)
                .AsEnumerable();
            return plans;
        } 
        public IEnumerable<PlanificationJournee> getListPansAtelier(int Id,int? atelierId,string etat,int? lieu,string date)
        {
            IEnumerable<PlanificationJournee> plans;
            if(atelierId != null)
            {
                if (etat != "")
                {
                    if (lieu != null)
                    {
                        if (date != "")
                        {
                            plans = _db.planificationJournees
                                .Where(a => a.PlanificationJournee_AbonnementID == Id && a.PlanificationJournee_AtelierID == atelierId)
                                .Where(p => p.PlanificationJournee_Etat == etat && p.PlanificationJournee_LieuStockageID == lieu && Convert.ToDateTime(p.PlanificationJournee_Date).ToString("yyyy-MM-dd") == date)
                                .Include(e => e.BonDe_Sortie)
                                .Include(p => p.Atelier)
                                .Include(p => p.Lieu_Stockage)
                                .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                        }
                        else
                        {
                            plans = _db.planificationJournees
                                .Where(a => a.PlanificationJournee_AbonnementID == Id && a.PlanificationJournee_AtelierID == atelierId)
                                .Where(p => p.PlanificationJournee_Etat == etat && p.PlanificationJournee_LieuStockageID == lieu)
                                .Include(e => e.BonDe_Sortie)
                                .Include(p => p.Atelier)
                                .Include(p => p.Lieu_Stockage)
                                .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                        }
                    }
                    else
                    {
                        if (date != "")
                        {
                            plans = _db.planificationJournees
                                .Where(a => a.PlanificationJournee_AbonnementID == Id && a.PlanificationJournee_AtelierID == atelierId)
                                .Where(p => p.PlanificationJournee_Etat == etat && Convert.ToDateTime(p.PlanificationJournee_Date).ToString("yyyy-MM-dd") == date)
                                .Include(e => e.BonDe_Sortie)
                                .Include(p => p.Atelier)
                                .Include(p => p.Lieu_Stockage)
                                .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                        }
                        else
                        {
                            plans = _db.planificationJournees
                                .Where(a => a.PlanificationJournee_AbonnementID == Id && a.PlanificationJournee_AtelierID == atelierId)
                                .Where(p => p.PlanificationJournee_Etat == etat)
                                .Include(e => e.BonDe_Sortie)
                                .Include(p => p.Atelier)
                                .Include(p => p.Lieu_Stockage)
                                .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                        }
                    }
                }
                else
                {
                    if (lieu != null)
                    {
                        if (date != "")
                        {
                            plans = _db.planificationJournees
                                .Where(a => a.PlanificationJournee_AbonnementID == Id && a.PlanificationJournee_AtelierID == atelierId)
                                .Where(p => p.PlanificationJournee_LieuStockageID == lieu && Convert.ToDateTime(p.PlanificationJournee_Date).ToString("yyyy-MM-dd") == date)
                                .Include(e => e.BonDe_Sortie)
                                .Include(p => p.Atelier)
                                .Include(p => p.Lieu_Stockage)
                                .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                        }
                        else
                        {
                            plans = _db.planificationJournees
                                .Where(a => a.PlanificationJournee_AbonnementID == Id && a.PlanificationJournee_AtelierID == atelierId)
                                .Where(p => p.PlanificationJournee_LieuStockageID == lieu)
                                .Include(e => e.BonDe_Sortie)
                                .Include(p => p.Atelier)
                                .Include(p => p.Lieu_Stockage)
                                .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                        }
                    }
                    else
                    {
                        if (date != "")
                        {
                            plans = _db.planificationJournees
                                .Where(a => a.PlanificationJournee_AbonnementID == Id && a.PlanificationJournee_AtelierID == atelierId)
                                .Where(p => Convert.ToDateTime(p.PlanificationJournee_Date).ToString("yyyy-MM-dd") == date)
                                .Include(e => e.BonDe_Sortie)
                                .Include(p => p.Atelier)
                                .Include(p => p.Lieu_Stockage)
                                .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                        }
                        else
                        {
                            plans = _db.planificationJournees
                                .Where(a => a.PlanificationJournee_AbonnementID == Id && a.PlanificationJournee_AtelierID == atelierId)
                                .Include(e => e.BonDe_Sortie)
                                .Include(p => p.Atelier)
                                .Include(p => p.Lieu_Stockage)
                                .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                        }
                    }
                }
            }
            else
            {
                if (etat != "")
                {
                    if (lieu != null)
                    {
                        if (date != "")
                        {
                            plans = _db.planificationJournees
                                .Where(a => a.PlanificationJournee_AbonnementID == Id)
                                .Where(p => p.PlanificationJournee_Etat == etat && p.PlanificationJournee_LieuStockageID == lieu && Convert.ToDateTime(p.PlanificationJournee_Date).ToString("yyyy-MM-dd") == date)
                                .Include(e => e.BonDe_Sortie)
                                .Include(p => p.Atelier)
                                .Include(p => p.Lieu_Stockage)
                                .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                        }
                        else
                        {
                            plans = _db.planificationJournees
                                .Where(a => a.PlanificationJournee_AbonnementID == Id)
                                .Where(p => p.PlanificationJournee_Etat == etat && p.PlanificationJournee_LieuStockageID == lieu)
                                .Include(e => e.BonDe_Sortie)
                                .Include(p => p.Atelier)
                                .Include(p => p.Lieu_Stockage)
                                .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                        }
                    }
                    else
                    {
                        if (date != "")
                        {
                            plans = _db.planificationJournees
                                .Where(a => a.PlanificationJournee_AbonnementID == Id)
                                .Where(p => p.PlanificationJournee_Etat == etat && Convert.ToDateTime(p.PlanificationJournee_Date).ToString("yyyy-MM-dd") == date)
                                .Include(e => e.BonDe_Sortie)
                                .Include(p => p.Atelier)
                                .Include(p => p.Lieu_Stockage)
                                .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                        }
                        else
                        {
                            plans = _db.planificationJournees
                                .Where(a => a.PlanificationJournee_AbonnementID == Id)
                                .Where(p => p.PlanificationJournee_Etat == etat)
                                .Include(e => e.BonDe_Sortie)
                                .Include(p => p.Atelier)
                                .Include(p => p.Lieu_Stockage)
                                .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                        }
                    }
                }
                else
                {
                    if (lieu != null)
                    {
                        if (date != "")
                        {
                            plans = _db.planificationJournees
                                .Where(a => a.PlanificationJournee_AbonnementID == Id)
                                .Where(p => p.PlanificationJournee_LieuStockageID == lieu && Convert.ToDateTime(p.PlanificationJournee_Date).ToString("yyyy-MM-dd") == date)
                                .Include(e => e.BonDe_Sortie)
                                .Include(p => p.Atelier)
                                .Include(p => p.Lieu_Stockage)
                                .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                        }
                        else
                        {
                            plans = _db.planificationJournees
                                .Where(a => a.PlanificationJournee_AbonnementID == Id)
                                .Where(p => p.PlanificationJournee_LieuStockageID == lieu)
                                .Include(e => e.BonDe_Sortie)
                                .Include(p => p.Atelier)
                                .Include(p => p.Lieu_Stockage)
                                .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                        }
                    }
                    else
                    {
                        if (date != "")
                        {
                            plans = _db.planificationJournees
                                .Where(a => a.PlanificationJournee_AbonnementID == Id)
                                .Where(p => Convert.ToDateTime(p.PlanificationJournee_Date).ToString("yyyy-MM-dd") == date)
                                .Include(e => e.BonDe_Sortie)
                                .Include(p => p.Atelier)
                                .Include(p => p.Lieu_Stockage)
                                .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                        }
                        else
                        {
                            /*plans = _db.planificationJournees
                                .Where(a => a.PlanificationJournee_AbonnementID == Id)
                                .Include(e => e.BonDe_Sortie)
                                .Include(p => p.Atelier)
                                .Include(p => p.Lieu_Stockage)
                                .AsEnumerable();*/
                            plans = null;
                        }
                    }
                }
            }
                 
            return plans;
        } 
        public async Task PlansSeenByStock(int lieuId)
        {
            var plans = _db.planificationJournees.Where(p => p.PlanificationJournee_LieuStockageID == lieuId && p.PlanificationJournee_SeenByStock == false).AsEnumerable();
            foreach(var p in plans)
            {
                p.PlanificationJournee_SeenByStock = true;
                _db.Entry(p).State = EntityState.Modified;
            }
            await unitOfWork.Complete();

        }
        public IEnumerable<PlanificationJournee> getListPansStock(int Id, int lieuId,string etat,int? atelier,string date)
        {
            IEnumerable<PlanificationJournee> plans;
            if (etat != "")
            {
                if (atelier != null)
                {
                    if (date != "")
                    {
                        plans = _db.planificationJournees
                            .Where(a => a.PlanificationJournee_AbonnementID == Id && a.PlanificationJournee_LieuStockageID == lieuId)
                            .Where(p => p.PlanificationJournee_Etat == etat && p.PlanificationJournee_AtelierID == atelier && Convert.ToDateTime(p.PlanificationJournee_Date).ToString("yyyy-MM-dd") == date)
                            .Include(e => e.BonDe_Sortie)
                            .Include(p => p.Atelier)
                            .Include(p => p.Lieu_Stockage)
                            .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                    }
                    else
                    {
                        plans = _db.planificationJournees
                            .Where(a => a.PlanificationJournee_AbonnementID == Id && a.PlanificationJournee_LieuStockageID == lieuId)
                            .Where(p => p.PlanificationJournee_Etat == etat && p.PlanificationJournee_AtelierID == atelier)
                            .Include(e => e.BonDe_Sortie)
                            .Include(p => p.Atelier)
                            .Include(p => p.Lieu_Stockage)
                            .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                    }
                }
                else
                {
                    if (date != "")
                    {
                        plans = _db.planificationJournees
                            .Where(a => a.PlanificationJournee_AbonnementID == Id && a.PlanificationJournee_LieuStockageID == lieuId)
                            .Where(p => p.PlanificationJournee_Etat == etat && Convert.ToDateTime(p.PlanificationJournee_Date).ToString("yyyy-MM-dd") == date)
                            .Include(e => e.BonDe_Sortie)
                            .Include(p => p.Atelier)
                            .Include(p => p.Lieu_Stockage)
                            .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                    }
                    else
                    {
                        plans = _db.planificationJournees
                            .Where(a => a.PlanificationJournee_AbonnementID == Id && a.PlanificationJournee_LieuStockageID == lieuId)
                            .Where(p => p.PlanificationJournee_Etat == etat)
                            .Include(e => e.BonDe_Sortie)
                            .Include(p => p.Atelier)
                            .Include(p => p.Lieu_Stockage)
                            .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                    }
                }
            }
            else
            {
                if (atelier != null)
                {
                    if (date != "")
                    {
                        plans = _db.planificationJournees
                            .Where(a => a.PlanificationJournee_AbonnementID == Id && a.PlanificationJournee_LieuStockageID == lieuId)
                            .Where(p => p.PlanificationJournee_AtelierID == atelier && p.PlanificationJournee_Etat != "Recue" && p.PlanificationJournee_Etat != "Matière première disponible" && Convert.ToDateTime(p.PlanificationJournee_Date).ToString("yyyy-MM-dd") == date)
                            .Include(e => e.BonDe_Sortie)
                            .Include(p => p.Atelier)
                            .Include(p => p.Lieu_Stockage)
                            .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                    }
                    else
                    {
                        plans = _db.planificationJournees
                            .Where(a => a.PlanificationJournee_AbonnementID == Id && a.PlanificationJournee_Etat != "Recue" && a.PlanificationJournee_Etat != "Matière première disponible" && a.PlanificationJournee_LieuStockageID == lieuId)
                            .Where(p => p.PlanificationJournee_AtelierID == atelier)
                            .Include(e => e.BonDe_Sortie)
                            .Include(p => p.Atelier)
                            .Include(p => p.Lieu_Stockage)
                            .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                    }
                }
                else
                {
                    if (date != "")
                    {
                        plans = _db.planificationJournees
                            .Where(a => a.PlanificationJournee_AbonnementID == Id && a.PlanificationJournee_Etat != "Recue" && a.PlanificationJournee_Etat != "Matière première disponible" && a.PlanificationJournee_LieuStockageID == lieuId)
                            .Where(p => Convert.ToDateTime(p.PlanificationJournee_Date).ToString("yyyy-MM-dd") == date)
                            .Include(e => e.BonDe_Sortie)
                            .Include(p => p.Atelier)
                            .Include(p => p.Lieu_Stockage)
                            .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                    }
                    else
                    {
                        plans = _db.planificationJournees
                            .Where(a => a.PlanificationJournee_AbonnementID == Id && a.PlanificationJournee_Etat != "Recue" && a.PlanificationJournee_Etat != "Matière première disponible" && a.PlanificationJournee_LieuStockageID == lieuId)
                            .Include(e => e.BonDe_Sortie)
                            .Include(p => p.Atelier)
                            .Include(p => p.Lieu_Stockage)
                            .AsEnumerable().OrderByDescending(p => p.PlanificationJournee_Date);
                    }
                }
            }

            return plans;
        }

        public async Task<MatStockFlagModel> updateFormulaireQteProduite(int id, List<PlanificationdeProductionModel> plans)
        {
            var planJ = _db.planificationJournees.Where(e => e.PlanificationJournee_ID == id).FirstOrDefault();
            decimal test = -0.01M; 
            var matStock = new MatStockFlagModel();
            var demande = _db.demandes.Where(e => e.Demande_PlanificationID == id)
                .Include(e=>e.BonDe_Sortie).ThenInclude(e=>e.Bon_Details).ThenInclude(e=>e.Matiere_Premiere).FirstOrDefault();
            if (demande != null)
            {
                foreach(var item in demande.BonDe_Sortie.Bon_Details)
                {
                    var matstock = _db.MatiereStock_Ateliers.Where(x => x.MatiereStockAtelier_MatierePremiereID == item.BonDeSortie_MatiereId && x.MatiereStockAtelier_AtelierID == planJ.PlanificationJournee_AtelierID).Include(x => x.Matiere_Premiere).FirstOrDefault();
                    if (matstock != null)
                    {
                        decimal qte = 0;
                        if ((item.BonDeSortie_UniteMesureId == 1 && matstock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 2) || (item.BonDeSortie_UniteMesureId == 4 && matstock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 3))
                            qte = item.BonDeSortie_QuantiteDemandee / 1000;
                        else if ((item.BonDeSortie_UniteMesureId == 2 && matstock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 1) || (item.BonDeSortie_UniteMesureId == 3 && matstock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 4))
                            qte = item.BonDeSortie_QuantiteDemandee * 1000;
                        else
                            qte = item.BonDeSortie_QuantiteDemandee;

                        var qterest = Decimal.Subtract(Convert.ToDecimal(matstock.MatiereStockAtelier_QauntiteActuelle.ToString("0.00")), qte);
                        if (Convert.ToDecimal(qterest.ToString("0.00")) < test)
                        {
                            matstock.MatiereStockAtelier_QauntiteActuelle = 0;
                        }
                        else if (Convert.ToDecimal(qterest.ToString("0.00")) >= test && Convert.ToDecimal(qterest.ToString("0.00")) <= 0)
                        {
                            matstock.MatiereStockAtelier_QauntiteActuelle = 0;
                            _db.Entry(matstock).State = EntityState.Modified;
                        }
                        else
                        {
                            matStock.flag = true;
                            matstock.MatiereStockAtelier_QauntiteActuelle = qterest;
                            _db.Entry(matstock).State = EntityState.Modified;
                        }


                    }
                    else
                    {
                        var res = matStock.Matieres.Where(x => x.matID == item.Matiere_Premiere.MatierePremiere_Id).FirstOrDefault();
                        if (res == null)
                        {
                            var matieres = new matStockViewModel()
                            {
                                matID = item.Matiere_Premiere.MatierePremiere_Id,
                                matiereLibelle = item.Matiere_Premiere.MatierePremiere_Libelle,
                                qteLivrer = item.BonDeSortie_QuantiteLivree,
                                qteEnStock = 0,
                                uniteStock = item.Unite_Mesure.UniteMesure_Libelle,
                                uniteLivrer = item.Unite_Mesure.UniteMesure_Libelle,
                            };
                            matStock.Matieres.Add(matieres);
                        }
                        else
                        {
                            res.qteLivrer += item.BonDeSortie_QuantiteLivree;
                        }

                        matStock.flag = false;

                    }
                }
            }
            foreach (var p in plans)
            {
                var plan = _db.planificationdeProductions
                    .Where(e => e.PlanificationProduction_PlanificationJourneeID == id)
                    .Where(e => e.PlanificationProduction_Id == p.PlanificationProduction_Id)
                    .Include(e=>e.Planification_Journee).ThenInclude(e=>e.Atelier)
                    .FirstOrDefault();
                plan.PlanificationProduction_QuantiteProduite = p.PlanificationProduction_QuantiteProduite;
                plan.PlanificationProduction_QuantiteRestante = plan.PlanificationProduction_QuantiteProduite;
                plan.PlanificationProduction_DateModification = p.PlanificationProduction_DateModification;
                var ficheTech = _db.ficheTechniqueBridges
                            .Where(f => f.FicheTechniqueBridge_ProduitVendableID == plan.PlanificationProduction_ProduitVendableId).Where(f => f.FicheTechniqueBridge_InUse == true)
                            .Include(f => f.Produit_FicheTechnique).ThenInclude(f=>f.Unite_Mesure)
                            .Include(f => f.Produit_FicheTechnique).ThenInclude(f=>f.Matiere_Premiere)
                            .Include(f => f.Fiche_Forme)
                            .Include(f=>f.FicheTech_ProduitBase).ThenInclude(f=>f.ProduitBase)
                            .Include(f=>f.FicheTech_ProduitBase).ThenInclude(f=>f.Unite_Mesure)
                            .FirstOrDefault();
                var ficheformeQte = ficheTech.Fiche_Forme.AsEnumerable().Where(x => x.FicheForme_FormeProduit == plan.PlanificationProduction_FormeProduitId).FirstOrDefault().FicheForme_Quantite;

                foreach (var fich in ficheTech.Produit_FicheTechnique)
                {
                    var stock = _db.MatiereStock_Ateliers.Where(x => x.MatiereStockAtelier_MatierePremiereID == fich.FicheTechnique_MatierePremiereId && x.MatiereStockAtelier_AtelierID == planJ.PlanificationJournee_AtelierID).Include(x=>x.Matiere_Premiere).FirstOrDefault();
                    if (stock != null)
                    {
                        decimal qte = 0;
                        if ((fich.FicheTechnique_UniteMesureId == 1 && stock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 2) || (fich.FicheTechnique_UniteMesureId == 4 && stock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 3))
                                qte = ((plan.PlanificationProduction_QuantitePrevue * fich.FicheTechnique_QuantiteMatiere) / ficheformeQte) / 1000;
                            else if ((fich.FicheTechnique_UniteMesureId == 2 && stock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 1) || (fich.FicheTechnique_UniteMesureId == 3 && stock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 4))
                                qte = ((plan.PlanificationProduction_QuantitePrevue * fich.FicheTechnique_QuantiteMatiere) / ficheformeQte) * 1000;
                            else
                                qte = (plan.PlanificationProduction_QuantitePrevue * fich.FicheTechnique_QuantiteMatiere) / ficheformeQte;

                            var qterest = Decimal.Subtract(Convert.ToDecimal(stock.MatiereStockAtelier_QauntiteActuelle.ToString("0.00")), qte);

                            if (Convert.ToDecimal(qterest.ToString("0.00")) < test)
                            {
                                var res = matStock.Matieres.Where(x => x.matID == stock.Matiere_Premiere.MatierePremiere_Id).FirstOrDefault();
                                if (res == null)
                                {
                                    var matieres = new matStockViewModel()
                                    {
                                        matID = stock.Matiere_Premiere.MatierePremiere_Id,
                                        matiereLibelle = stock.Matiere_Premiere.MatierePremiere_Libelle,
                                        qteLivrer = (plan.PlanificationProduction_QuantitePrevue * fich.FicheTechnique_QuantiteMatiere) / ficheformeQte,
                                        qteEnStock = stock.MatiereStockAtelier_QauntiteActuelle,
                                        uniteStock = stock.Matiere_Premiere.Unite_Mesure.UniteMesure_Libelle,
                                        uniteLivrer = fich.Unite_Mesure.UniteMesure_Libelle,
                                    };
                                    matStock.Matieres.Add(matieres);
                                }
                                else
                                {
                                    res.qteLivrer += (plan.PlanificationProduction_QuantitePrevue * fich.FicheTechnique_QuantiteMatiere) / ficheformeQte;
                                }

                            }
                            else if (Convert.ToDecimal(qterest.ToString("0.00")) >= test && Convert.ToDecimal(qterest.ToString("0.00")) <= 0)
                            {
                                stock.MatiereStockAtelier_QauntiteActuelle = 0;

                                _db.Entry(stock).State = EntityState.Modified;
                            }
                            else
                            {
                                matStock.flag = true;
                                stock.MatiereStockAtelier_QauntiteActuelle = qterest;
                                _db.Entry(stock).State = EntityState.Modified;
                            }
                        
                           
                    }
                    else
                    {
                        var res = matStock.Matieres.Where(x => x.matID == fich.Matiere_Premiere.MatierePremiere_Id).FirstOrDefault();
                        if (res == null)
                        {
                            var matieres = new matStockViewModel()
                            {
                                matID = fich.Matiere_Premiere.MatierePremiere_Id,
                                matiereLibelle = fich.Matiere_Premiere.MatierePremiere_Libelle,
                                qteLivrer = (plan.PlanificationProduction_QuantitePrevue * fich.FicheTechnique_QuantiteMatiere) / ficheformeQte,
                                qteEnStock = 0,
                                uniteStock = fich.Unite_Mesure.UniteMesure_Libelle,
                                uniteLivrer = fich.Unite_Mesure.UniteMesure_Libelle,
                            };
                            matStock.Matieres.Add(matieres);
                        }
                        else
                        {
                            res.qteLivrer += (plan.PlanificationProduction_QuantitePrevue * fich.FicheTechnique_QuantiteMatiere) / ficheformeQte;
                        }
                         
                        matStock.flag = false;

                    }
                }
                foreach (var fichbase in ficheTech.FicheTech_ProduitBase)
                {
                    var stockbase = _db.prodBase_Ateliers.Where(x => x.ProdBase_Atelier_ProduitID == fichbase.ProduitBase_ID && x.ProdBase_Atelier_AtelierID == plan.Planification_Journee.PlanificationJournee_AtelierID)
                        .Include(x => x.ProduitBase).ThenInclude(x => x.Unite_Mesure)
                        .FirstOrDefault();
                    if (stockbase != null)
                    {
                        if (stockbase.ProdBase_Atelier_QuantiteProduite > 0)
                        {
                            decimal qte = 0;
                            if ((fichbase.UniteMesure_ID == 1 && stockbase.ProduitBase.ProduitBase_UniteMesureID == 2) || (fichbase.UniteMesure_ID == 4 && stockbase.ProduitBase.ProduitBase_UniteMesureID == 3))
                                qte = ((plan.PlanificationProduction_QuantitePrevue * fichbase.ProduitQte) / ficheformeQte) / 1000;
                            else if ((fichbase.UniteMesure_ID == 2 && stockbase.ProduitBase.ProduitBase_UniteMesureID == 1) || (fichbase.UniteMesure_ID == 3 && stockbase.ProduitBase.ProduitBase_UniteMesureID == 4))
                                qte = ((plan.PlanificationProduction_QuantitePrevue * fichbase.ProduitQte) / ficheformeQte) * 1000;
                            else
                                qte = (plan.PlanificationProduction_QuantitePrevue * fichbase.ProduitQte) / ficheformeQte;

                            // var qterest = stockbase.ProdBase_Atelier_QuantiteProduite - Convert.ToDecimal(qte.ToString("0.00"));
                            var qterest = Decimal.Subtract(Convert.ToDecimal(stockbase.ProdBase_Atelier_QuantiteProduite.ToString("0.00")), qte);
                            
                            // if (Convert.ToDecimal(qterest.ToString("0.00")) < Convert.ToDecimal("0.01"))

                            if (Convert.ToDecimal(qterest.ToString("0.00")) < test)
                            //if (qterest < 0)
                            {
                                var res = matStock.Matieres.Where(x => x.matID == stockbase.ProduitBase.ProduitBase_ID).FirstOrDefault();
                                if (res == null)
                                {
                                    var matieres = new matStockViewModel()
                                    {
                                        matID = stockbase.ProduitBase.ProduitBase_ID,
                                        matiereLibelle = stockbase.ProduitBase.ProduitBase_Designation,
                                        qteLivrer = (plan.PlanificationProduction_QuantitePrevue * fichbase.ProduitQte) / ficheformeQte,
                                        qteEnStock = stockbase.ProdBase_Atelier_QuantiteProduite,
                                        uniteStock = stockbase.ProduitBase.Unite_Mesure.UniteMesure_Libelle,
                                        uniteLivrer = fichbase.Unite_Mesure.UniteMesure_Libelle,
                                    };
                                    matStock.Matieres.Add(matieres);
                                }
                                else
                                {
                                    res.qteLivrer += (plan.PlanificationProduction_QuantitePrevue * fichbase.ProduitQte) / ficheformeQte;
                                }
                                
                            }
                            else if (Convert.ToDecimal(qterest.ToString("0.00")) >= test && Convert.ToDecimal(qterest.ToString("0.00")) <= 0)
                            {
                                stockbase.ProdBase_Atelier_QuantiteProduite = 0;
                                _db.Entry(stockbase).State = EntityState.Modified;
                            }
                            else
                            { 
                                matStock.flag = true;
                                stockbase.ProdBase_Atelier_QuantiteProduite = qterest;
                                _db.Entry(stockbase).State = EntityState.Modified;
                            }
                        }
                        else
                        {
                            var res = matStock.Matieres.Where(x => x.matID == stockbase.ProduitBase.ProduitBase_ID).FirstOrDefault();
                            if (res == null)
                            {
                                var matieres = new matStockViewModel()
                                {
                                    matID = stockbase.ProduitBase.ProduitBase_ID,
                                    matiereLibelle = stockbase.ProduitBase.ProduitBase_Designation,
                                    qteLivrer = (plan.PlanificationProduction_QuantitePrevue * fichbase.ProduitQte) / ficheformeQte,
                                    qteEnStock = stockbase.ProdBase_Atelier_QuantiteProduite,
                                    uniteStock = stockbase.ProduitBase.Unite_Mesure.UniteMesure_Libelle,
                                    uniteLivrer = fichbase.Unite_Mesure.UniteMesure_Libelle,
                                };
                                matStock.Matieres.Add(matieres);
                            }
                            else
                            {
                                res.qteLivrer += (plan.PlanificationProduction_QuantitePrevue * fichbase.ProduitQte) / ficheformeQte;
                            }

                            matStock.flag = false;
                        }
                    }
                    else
                    {
                        var res = matStock.Matieres.Where(x => x.matID == fichbase.ProduitBase.ProduitBase_ID).FirstOrDefault();
                        if (res == null)
                        {
                            var matieres = new matStockViewModel()
                            {
                                matID = fichbase.ProduitBase.ProduitBase_ID,
                                matiereLibelle = fichbase.ProduitBase.ProduitBase_Designation,
                                qteLivrer = (plan.PlanificationProduction_QuantitePrevue * fichbase.ProduitQte) / ficheformeQte,
                                qteEnStock = 0,
                                uniteStock = fichbase.Unite_Mesure.UniteMesure_Libelle,
                                uniteLivrer = fichbase.Unite_Mesure.UniteMesure_Libelle,
                            };
                            matStock.flag = false;

                            matStock.Matieres.Add(matieres);
                        }
                        else
                        {
                            res.qteLivrer += (plan.PlanificationProduction_QuantitePrevue * fichbase.ProduitQte) / ficheformeQte;
                        }

                    }
                }
                if (matStock.Matieres.Count() > 0 || matStock.flag == false)
                {
                    matStock.flag = false;
                }
                else
                {
                    matStock.flag = true;
                    var produitAppro = _db.produitAppros.Where(e => e.ProduitAppro_FormeProduitID == plan.PlanificationProduction_FormeProduitId && e.ProduitAppro_AtelierID == plan.Planification_Journee.PlanificationJournee_AtelierID).FirstOrDefault();
                    if (produitAppro != null)
                    {
                        produitAppro.ProduitAppro_QuantiteProduite += p.PlanificationProduction_QuantiteProduite;
                        produitAppro.ProduitAppro_DateModification = DateTime.Now;
                        _db.Entry(produitAppro).State = EntityState.Modified;
                    }
                    else
                    {
                        ProduitAppro appro = new ProduitAppro
                        {
                        ProduitAppro_ProduitID = plan.PlanificationProduction_ProduitVendableId,
                        ProduitAppro_FormeProduitID = plan.PlanificationProduction_FormeProduitId,
                        ProduitAppro_QuantiteProduite = p.PlanificationProduction_QuantiteProduite,
                        ProduitAppro_DateCreation = DateTime.Now,
                        ProduitAppro_DateModification = DateTime.Now,
                        ProduitAppro_AtelierID = plan.Planification_Journee.Atelier.Atelier_ID,
                        ProduitAppro_AbonnementID = plan.PlanificationProduction_AbonnementId
                    };
                    await _db.AddAsync(appro);
                    }
                    _db.Entry(plan).State = EntityState.Modified;
                
                }

            }
            if (matStock.Matieres.Count() > 0 || matStock.flag == false)
            {
                return matStock;

            }
            else
            {
                var confirm = await unitOfWork.Complete();
                return matStock;
            }

        }

        public async Task<bool> deletePlan(int ID)
        {
            PlanificationdeProduction produit = _db.planificationdeProductions.Where(e => e.PlanificationProduction_Id == ID).FirstOrDefault();
            if (produit != null)
            {
                _db.Entry(produit).State = EntityState.Deleted;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public PlanificationdeProduction findFormulairePlans(int formulaireProduitId)
        {
            return _db.planificationdeProductions.Where(e => e.PlanificationProduction_Id == formulaireProduitId).Include(e => e.Produit_Vendable).FirstOrDefault();
        }

        public async Task<int?> Planifier(PlanificationJournee plan)
        {
            plan.PlanificationJournee_DateCreation = DateTime.Now;
            plan.Planification_GeneratedID = "Planification de:" + " " + DateTime.Now.ToString();
            plan.PlanificationJournee_SeenByStock = false;
            await _db.planificationJournees.AddAsync(plan);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
            {
                var res =  await SetQteAVP(plan.PlanificationJournee_ID);
                if (res > 0)
                    return plan.PlanificationJournee_ID;
                else
                    return null;
            }
            else { return null; }
        }
        public async Task<int?> SetQteAVP(int planificationId)
        {
            try
            {
                var plan = _db.planificationJournees.Where(s => s.PlanificationJournee_ID == planificationId).Include(p => p.BonDe_Sortie).ThenInclude(p => p.Bon_Details).ThenInclude(p=>p.Unite_Mesure).FirstOrDefault();
                var demande = _db.demandes.Where(s => s.Demande_ID == planificationId).Include(p => p.BonDe_Sortie).ThenInclude(p => p.Bon_Details).ThenInclude(p=>p.Unite_Mesure).FirstOrDefault();
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
                else if (demande != null) 
                {
                    foreach (var d in demande.BonDe_Sortie.Bon_Details)
                    {
                        var matEnStock = _db.MatiereStock_Ateliers.Where(p => p.MatiereStockAtelier_AtelierID == demande.Demande_AtelierID && p.MatiereStockAtelier_MatierePremiereID == d.BonDeSortie_MatiereId).Include(p => p.Matiere_Premiere).FirstOrDefault();
                    //    var a = 0;
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
                    var planBase = _db.planificationJourneeBases.Where(s => s.PlanificationJourneeBase_ID == planificationId).Include(p => p.BonDe_Sortie).ThenInclude(p => p.Bon_Details).ThenInclude(p=>p.Unite_Mesure).FirstOrDefault();
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
        public async Task<List<matStockViewModel>> SetProdsDeBase(int planificationId)
        {
            var plan = _db.planificationJournees.Where(p => p.PlanificationJournee_ID == planificationId).Include(p => p.Planification_Production).FirstOrDefault();
            /*var plan = _db.planificationdeProductions
                    .Where(e => e.PlanificationProduction_PlanificationJourneeID == planificationId)
                 //   .Where(e => e.PlanificationProduction_Id == p.PlanificationProduction_Id)
                    .Include(e => e.Planification_Journee).ThenInclude(e => e.Atelier)
                    .AsEnumerable();*/
            var listM = new List<matStockViewModel>();
            foreach(var item in plan.Planification_Production)
            {
                var fichTech = _db.ficheTechniqueBridges.Where(p => p.FicheTechniqueBridge_ProduitVendableID == item.PlanificationProduction_ProduitVendableId).Where(f => f.FicheTechniqueBridge_InUse == true).Include(p=>p.FicheTech_ProduitBase).FirstOrDefault();
                var ficheFormQte = _db.ficheFormes.Where(p => p.FicheForme_FicheBridge == fichTech.FicheTechniqueBridge_ID && p.FicheForme_FormeProduit == item.PlanificationProduction_FormeProduitId).FirstOrDefault().FicheForme_Quantite;
                var ficheBase = _db.ficheTech_ProduitBases.Where(p => p.FicheTech_ID == fichTech.FicheTechniqueBridge_ID).Include(p => p.ProduitBase).Include(p => p.Unite_Mesure).AsEnumerable();
                foreach (var prod in ficheBase)
                {
                    var nbProd = item.PlanificationProduction_QuantitePrevue / ficheFormQte;
                    decimal nbBase = 0;
                    
                    var res = listM.Where(p => p.matID == prod.ProduitBase.ProduitBase_ID).FirstOrDefault();
                    if (res != null)
                    {
                        if ((res.uniteStock == "Gramme(s)" && prod.Unite_Mesure.UniteMesure_Libelle == "Kilogramme(s)") || (res.uniteStock == "Millilitre(s)" && prod.Unite_Mesure.UniteMesure_Libelle == "Litre(s)"))
                            nbBase = (nbProd * prod.ProduitQte) * 1000;
                        else if ((res.uniteStock == "Kilogramme(s)" && prod.Unite_Mesure.UniteMesure_Libelle == "Gramme(s)") || ( res.uniteStock == "Litre(s)" && prod.Unite_Mesure.UniteMesure_Libelle == "Millilitre(s)"))
                            nbBase = (nbProd * prod.ProduitQte) /  1000;
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
            foreach(var m in listM)
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
            return listM;
        }
        public List<matStockViewModel> getProdBasePlan(int planificationId)
        {
            var list = _db.planification_ProdBases.Where(p => p.PlanificationProdBase_PlanificationID == planificationId).Include(p=>p.Planification_Journee).AsEnumerable();
            var res = new List<matStockViewModel>();
            foreach(var item in list)
            {
                var mat = new matStockViewModel()
                {
                    matID = item.PlanificationProdBase_PlanificationID,
                    matiereLibelle = item.PlanificationProdBase_ProdBaseDesignation,
                    uniteStock = item.PlanificationProdBase_UniteDesi,
                    qteEnStock = item.PlanificationProdBase_Quantite,
                    uniteLivrer = (item.Planification_Journee.PlanificationJournee_DateCreation).ToString()
                    
                };
                res.Add(mat);
            }
            return res;
        }
        public decimal getProduitCoutDeRevient(int Id)
        {
            decimal? var = _db.produitVendables.Where(p => p.ProduitVendable_Id == Id).Select(p => p.Produit_CoutDeRevient).FirstOrDefault();
            return (decimal)var;
        }

        public FicheTechniqueBridge GetFicheTech(int Id)
        {
            var fiche = _db.ficheTechniqueBridges
               // .Where(f => f.FicheTechniqueBridge_AbonnementID == Id)
                .Where(f => f.FicheTechniqueBridge_ProduitVendableID == Id)
                .Where(f => f.FicheTechniqueBridge_InUse == true)
                .Include(p=>p.Produit_Vendable)
                .Include(p => p.Produit_FicheTechnique).ThenInclude(p => p.Matiere_Premiere).ThenInclude(p=>p.listAllergene).ThenInclude(p=>p.Allgerene)
                .Include(p => p.Produit_FicheTechnique).ThenInclude(u => u.Unite_Mesure)
                .Include(p=>p.FicheTech_ProduitBase).ThenInclude(p=>p.ProduitBase)
                .Include(p => p.Fiche_Forme).ThenInclude(p=>p.Forme_Produit)
                .FirstOrDefault();
            return fiche;
        }

        public IEnumerable<PlanificationdeProduction> getDetailPlan(int aboId, int Id)
        {
            var plan = _db.planificationdeProductions
                .Where(p => p.PlanificationProduction_PlanificationJourneeID == Id)
                .Where(p => p.PlanificationProduction_AbonnementId == aboId)
                .Include(p => p.Forme_Produit)
                .Include(p => p.Produit_Vendable).ThenInclude(p => p.Unite_Mesure)
                .AsEnumerable();
            return plan;
        }
        public IEnumerable<PlanificationdeProduction> getDetailPlanByPlan(int aboId, int Id, int planId)
        {
            var plan = _db.planificationdeProductions
                .Where(p => p.PlanificationProduction_PlanificationJourneeID == Id)
                .Where(p => p.PlanificationProduction_AbonnementId == aboId && p.PlanificationProduction_Id == planId)
                .Include(p => p.Forme_Produit)
                .Include(p => p.Produit_Vendable).ThenInclude(p => p.Unite_Mesure)
                .AsEnumerable();
            return plan;
        }
        public IEnumerable<PlanificationdeProduction> getDetailPlanProduitList(int aboId, int Id)
        {
            var plan = _db.planificationdeProductions
                .Where(p => p.PlanificationProduction_PlanificationJourneeID == Id)
                .Where(p => p.PlanificationProduction_AbonnementId == aboId && p.PlanificationProduction_QuantiteProduite == 0)
                .Include(p => p.Forme_Produit)
                .Include(p => p.Produit_Vendable).ThenInclude(p => p.Unite_Mesure)
                .AsEnumerable();
            return plan;
        }
        public IEnumerable<PlanificationdeProduction> getDetailPlanFormeList(int Id, int produitId)
        {
            var plan = _db.planificationdeProductions
                .Where(p => p.PlanificationProduction_PlanificationJourneeID == Id)
                .Where(p => p.PlanificationProduction_ProduitVendableId == produitId && p.PlanificationProduction_QuantiteProduite == 0)
                .Include(p => p.Forme_Produit)
                .AsEnumerable();
            return plan;
        }

        public IEnumerable<BonDetails> getBonDetails(int Id)
        {
            var bon = _db.bonDetails.Where(p => p.BonDeSortie_BonDeSortieID == Id)
                .Include(p=>p.BonDe_Sortie).ThenInclude(p=>p.Lieu_Stockage)
                .Include(p => p.Matiere_Premiere).Include(p => p.Unite_Mesure).AsEnumerable();
            return bon;
        }

        public async Task<int?> CreerDemande(Demande demande)
        {
            demande.Demande_DateCreation = DateTime.Now;
            demande.BonDe_Sortie.BonDeSortie_DateProduction = DateTime.Now;
            demande.BonDe_Sortie.BonDeSortie_StockID = demande.Demande_LieuStockageID;
            await _db.demandes.AddAsync(demande);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
            {
                if (demande.Demande_Type == "Complementaire")
                {
                    var res = await SetQteAVP(demande.Demande_ID);
                    if (res > 0)
                        return demande.Demande_ID;

                    else
                        return null;
                }
                else
                {
                    return demande.Demande_ID;
                }
               
            }
            else { return null; }
        }

        public IEnumerable<Demande> GetDemandes(int Id, string date, int? lieuID, int? atelierID)
        {
            var query = _db.demandes;
            if (date != "")
            {
                DateTime newdate = Convert.ToDateTime(date);
                query.Where(d => d.Demande_AbonnementID == Id).Where(p => p.Demande_DateCreation.Date == newdate.Date);
            }
            //if (lieuID != null)
            //query.Where(p => p.Planification_Journee.PlanificationJournee_LieuStockageID == lieuID);
            //if (atelierID != null)
            //query.Where(p => p.Planification_Journee.PlanificationJournee_AtelierID == atelierID);
            return query.Include(d => d.Lieu_Stockage)
                .Include(p => p.Atelier)
                .Include(d => d.BonDe_Sortie).AsEnumerable();

        }
        public IEnumerable<Demande> GetDemandesAtelier(int Id , int atelierId)
        {
            return _db.demandes.Where(d => d.Demande_AbonnementID == Id && d.Demande_AtelierID == atelierId)
                .Include(d => d.Lieu_Stockage).Include(p => p.Atelier)
                .Include(d => d.BonDe_Sortie).AsEnumerable();
        }
        public IEnumerable<Demande> GetDemandesStock(int Id, int lieuId)
        {
            return _db.demandes.Where(d => d.Demande_AbonnementID == Id && d.Demande_LieuStockageID == lieuId)
                .Include(p => p.Atelier)
                .Include(p=>p.Lieu_Stockage)
                .Include(d => d.BonDe_Sortie).AsEnumerable();
        }

        public async Task<bool> AccepterDemande(int demandeId, int adresse, string validId)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var demande = _db.demandes.Where(s => s.Demande_ID == demandeId).FirstOrDefault();
                    demande.Demande_Etat = "Livré";
                    demande.Demande_ValideParID = validId;
                    List<MouvementStock> mouvement = new List<MouvementStock>();
                    var bonDetails = _db.bonDetails
                        .Where(b => b.BonDeSortie_BonDeSortieID == demande.Demande_BonDeSortieID).AsEnumerable();
                    foreach (BonDetails details in bonDetails)
                    {

                        var matiere = _db.matierePremieres.Where(p => p.MatierePremiere_Id == details.BonDeSortie_MatiereId)
                            .Where(p => p.MatierePremiere_IsActive == 1)
                            .FirstOrDefault();

                        var mpStockage = _db.matierePremiereStockages
                            .Where(s => s.MatierePremiereStokage_MatierePremiereId == details.BonDeSortie_MatiereId && s.MatierePremiereStokage_IsActive == 1)
                            .Where(p => p.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == adresse)
                            .Include(s => s.Section_Stockage).ThenInclude(s => s.Zone_Stockage).ThenInclude(s => s.Lieu_Stockage)
                            .FirstOrDefault();
                        MouvementStock m = new MouvementStock()
                        {
                            MouvementStock_MatierePremiereStokageId = mpStockage.MatierePremiereStokage_Id,
                            MouvementStock_Quantite = details.BonDeSortie_Quantite,
                            MouvementStock_UniteMesureId = details.BonDeSortie_UniteMesureId,
                            MouvementStock_Date = demande.Demande_DateCreation,
                            MouvementStock_DateReception = DateTime.Now,
                            MouvementStock_DateSaisie = DateTime.Now,
                            MouvementStock_MouvementTypeId = 4,
                            MouvementStock_AbonnementId = demande.Demande_AbonnementID,
                            MouvementStock_DateCreation = DateTime.Now,
                            MouvementStock_MatiereQuantiteActuelle = 0,
                            MouvementStock_IsActive = 1,
                            MouvementStock_DestinationStockId = null,
                        };
                        decimal qte = 0;
                        if ((details.BonDeSortie_UniteMesureId == 1 && matiere.MatierePremiere_AchatUniteMesureId == 2) || (details.BonDeSortie_UniteMesureId == 4 && matiere.MatierePremiere_AchatUniteMesureId == 3))
                            qte = details.BonDeSortie_Quantite / 1000;
                        else if ((details.BonDeSortie_UniteMesureId == 2 && matiere.MatierePremiere_AchatUniteMesureId == 1) || (details.BonDeSortie_UniteMesureId == 3 && matiere.MatierePremiere_AchatUniteMesureId == 4))
                            qte = details.BonDeSortie_Quantite * 1000;
                        else
                            qte = details.BonDeSortie_Quantite;
                        details.BonDeSortie_QuantiteLivree = details.BonDeSortie_Quantite;
                      //  details.BonDeSorite_UniteStock = details.uni;

                        var qterest = mpStockage.MatierePremiereStokage_QuantiteActuelle - qte;
                        if (qterest < 0)
                            return false;
                        else
                            m.MouvementStock_MatiereQuantiteActuelle = qterest;

                        mpStockage.MatierePremiereStokage_QuantiteActuelle = m.MouvementStock_MatiereQuantiteActuelle;
                        _db.Entry(matiere).State = EntityState.Modified;
                        mouvement.Add(m);                            
                    }
                    foreach (var m in mouvement)
                    {
                        await _db.mouvements.AddAsync(m);
                        var i = _db.mouvements.Where(p => p.MouvementStock_MatierePremiereStokageId == m.MouvementStock_MatierePremiereStokageId).AsEnumerable();
                        foreach (var move in i)
                        {
                            move.MouvementStock_IsActive = 0;
                            _db.Entry(move).State = EntityState.Modified;
                        }
                    }
                    _db.Entry(demande).State = EntityState.Modified;
                    await unitOfWork.Complete();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> ValiderLivraison(int demandeId)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var demande = _db.demandes.Where(s => s.Demande_ID == demandeId)
                        .Include(p=>p.Lieu_Stockage)
                        .Include(p=>p.Atelier)
                        .Include(p=>p.BonDe_Sortie).ThenInclude(p=>p.Bon_Details).FirstOrDefault();
                    demande.Demande_Etat = "Recue";

                    foreach (var d in demande.BonDe_Sortie.Bon_Details)
                    {
                        var matEnStock = _db.MatiereStock_Ateliers.Where(p => p.MatiereStockAtelier_AtelierID == demande.Demande_AtelierID && p.MatiereStockAtelier_MatierePremiereID == d.BonDeSortie_MatiereId).Include(p => p.Matiere_Premiere).FirstOrDefault();
                        if (matEnStock != null)
                        {
                            decimal qte = 0;
                            if ((d.BonDeSortie_UniteMesureId == 1 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 2) || (d.BonDeSortie_UniteMesureId == 4 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 3))
                                qte = d.BonDeSortie_Quantite / 1000;
                            else if ((d.BonDeSortie_UniteMesureId == 2 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 1) || (d.BonDeSortie_UniteMesureId == 3 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 4))
                                qte = d.BonDeSortie_Quantite * 1000;
                            else
                                qte = d.BonDeSortie_Quantite;

                            matEnStock.MatiereStockAtelier_QauntiteActuelle += qte;
                            matEnStock.MatiereStockAtelier_QuatiteAvecPlanification += qte;
                            _db.Entry(matEnStock).State = EntityState.Modified;

                        }
                        else
                        {
                            var matStock = new MatiereStock_Atelier()
                            {
                                MatiereStockAtelier_AtelierID = (int)demande.Demande_AtelierID,
                                MatiereStockAtelier_MatierePremiereID = d.BonDeSortie_MatiereId,
                                MatiereStockAtelier_DateCreation = DateTime.Now,
                                MatiereStockAtelier_AbonnementID = demande.Demande_AbonnementID,
                            };
                            var mat = _db.matierePremieres.Where(p => p.MatierePremiere_Id == matStock.MatiereStockAtelier_MatierePremiereID).FirstOrDefault();
                            decimal qte = 0;
                            if ((d.BonDeSortie_UniteMesureId == 1 && mat.MatierePremiere_AchatUniteMesureId == 2) || (d.BonDeSortie_UniteMesureId == 4 && mat.MatierePremiere_AchatUniteMesureId == 3))
                                qte = d.BonDeSortie_Quantite / 1000;
                            else if ((d.BonDeSortie_UniteMesureId == 2 && mat.MatierePremiere_AchatUniteMesureId == 1) || (d.BonDeSortie_UniteMesureId == 3 && mat.MatierePremiere_AchatUniteMesureId == 4))
                                qte = d.BonDeSortie_Quantite * 1000;
                            else
                                qte = d.BonDeSortie_Quantite;

                            matStock.MatiereStockAtelier_QauntiteActuelle = qte;
                            matStock.MatiereStockAtelier_QuatiteAvecPlanification += 0;
                            await _db.MatiereStock_Ateliers.AddAsync(matStock);
                        }                   
                    }
                    _db.Entry(demande).State = EntityState.Modified;
                    await unitOfWork.Complete();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public IEnumerable<MouvementStock> GetMouvementStocks(int Id)
        {
            return _db.mouvements.Where(p => p.MouvementStock_AbonnementId == Id)
                .Include(p => p.MatierePremiere_Stokage)
                .ThenInclude(x => x.Matiere_Premiere)
                .Include(p => p.Unite_Mesure)
                .Include(m => m.MatierePremiere_Stokage)
                .ThenInclude(m => m.Section_Stockage).ThenInclude(m => m.Zone_Stockage).ThenInclude(m => m.Lieu_Stockage)
                .AsEnumerable().OrderByDescending(p => p.MouvementStock_DateSaisie);
        }
        public IEnumerable<MouvementStock> GetMouvementStocksActiveOnly(int Id, int? matiere, string date)
        {
            IEnumerable<MouvementStock> mouvements;
            if (date != "")
            {
                if (matiere != null)
                {
                    mouvements = _db.mouvements.Where(p => p.MouvementStock_AbonnementId == Id)
                        .Where(p => p.MatierePremiere_Stokage.MatierePremiereStokage_MatierePremiereId == matiere && Convert.ToDateTime(p.MouvementStock_Date).ToString("yyyy-MM-dd") == date)
                        .Include(p => p.MatierePremiere_Stokage)
                        .ThenInclude(x => x.Matiere_Premiere)
                        .AsEnumerable().OrderByDescending(p => p.MouvementStock_DateSaisie);
                }
                else
                {
                    mouvements = _db.mouvements.Where(p => p.MouvementStock_AbonnementId == Id  )
                        .Where(p => Convert.ToDateTime(p.MouvementStock_Date).ToString("yyyy-MM-dd") == date)
                        .Include(p => p.MatierePremiere_Stokage)
                        .ThenInclude(x => x.Matiere_Premiere)
                        .AsEnumerable().OrderByDescending(p => p.MouvementStock_DateSaisie);
                }
            }
            else
            {
                if (matiere != null)
                {
                    mouvements = _db.mouvements.Where(p => p.MouvementStock_AbonnementId == Id)
                        .Where(p => p.MatierePremiere_Stokage.MatierePremiereStokage_MatierePremiereId == matiere)
                        .Include(p => p.MatierePremiere_Stokage)
                        .ThenInclude(x => x.Matiere_Premiere)
                        .AsEnumerable().OrderByDescending(p => p.MouvementStock_DateSaisie);
                }
                else
                {
                    mouvements = _db.mouvements.Where(p => p.MouvementStock_AbonnementId == Id && p.MouvementStock_IsActive == 1)
                        .Include(p => p.MatierePremiere_Stokage)
                        .ThenInclude(x => x.Matiere_Premiere)
                        .Include(p => p.Unite_Mesure)
                        .AsEnumerable().OrderByDescending(p => p.MouvementStock_DateSaisie);
                }
            }
            return mouvements;
        }  
        public IEnumerable<MouvementStock> GetMouvementStocksBystock(int Id, int lieuId)
        {
            return _db.mouvements.Where(p => p.MouvementStock_AbonnementId == Id &&
            p.MatierePremiere_Stokage.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == lieuId)
                .Include(p => p.MatierePremiere_Stokage)
                .ThenInclude(x => x.Matiere_Premiere)
                .Include(p => p.Unite_Mesure)
                .AsEnumerable().OrderByDescending(p => p.MouvementStock_DateSaisie);
        }
        public IEnumerable<MouvementStock> GetMouvementStocksBystockActiveOnly(int Id, int lieuId,int? matiere,string date)
        {
            IEnumerable<MouvementStock> mouvements;
            if (date != "") 
            {
                if (matiere != null)
                {
                    mouvements = _db.mouvements.Where(p => p.MouvementStock_AbonnementId == Id && p.MatierePremiere_Stokage.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == lieuId)
                        .Where(p=>p.MouvementStock_MatierePremiereStokageId==matiere && Convert.ToDateTime(p.MouvementStock_Date).ToString("yyyy-MM-dd") == date)
                        .Include(p => p.MatierePremiere_Stokage)
                        .ThenInclude(x => x.Matiere_Premiere)
                        .AsEnumerable().OrderByDescending(p => p.MouvementStock_DateSaisie);
                }
                else
                {
                    mouvements = _db.mouvements.Where(p => p.MouvementStock_AbonnementId == Id && p.MatierePremiere_Stokage.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == lieuId)
                        .Where(p => Convert.ToDateTime(p.MouvementStock_Date).ToString("yyyy-MM-dd") == date)
                        .Include(p => p.MatierePremiere_Stokage)
                        .ThenInclude(x => x.Matiere_Premiere)
                        .AsEnumerable().OrderByDescending(p => p.MouvementStock_DateSaisie);
                }
            }
            else
            {
                if (matiere != null)
                {
                    mouvements = _db.mouvements.Where(p => p.MouvementStock_AbonnementId == Id && p.MatierePremiere_Stokage.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == lieuId)
                        .Where(p => p.MouvementStock_MatierePremiereStokageId == matiere)
                        .Include(p => p.MatierePremiere_Stokage)
                        .ThenInclude(x => x.Matiere_Premiere)
                        .AsEnumerable().OrderByDescending(p => p.MouvementStock_DateSaisie);
                }
                else
                {
                    mouvements= _db.mouvements.Where(p => p.MouvementStock_AbonnementId == Id &&p.MatierePremiere_Stokage.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == lieuId && p.MouvementStock_IsActive == 1)
                        .Include(p => p.MatierePremiere_Stokage)
                        .ThenInclude(x => x.Matiere_Premiere)
                        .AsEnumerable().OrderByDescending(p => p.MouvementStock_DateSaisie);
                }
            }
            return mouvements;
        }

        public async Task<MatStockFlagModel> AccepterPlanification(int planificationId, int adresse, List<BonViewModel> ListBons, string validePar)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                var matStock = new MatStockFlagModel();
                try
                {
                    var plan = _db.planificationJournees.Where(s => s.PlanificationJournee_ID == planificationId).FirstOrDefault();
                    plan.PlanificationJournee_Etat = "Livré";
                    plan.PlanificationJournee_ValidePar = validePar;
                    List<MouvementStock> mouvement = new List<MouvementStock>();
                    double test = 0.0001;
                    var bonDetails = _db.bonDetails
                        .Where(b => b.BonDeSortie_BonDeSortieID == plan.PlanificationJournee_BonDeSortieID &&  Convert.ToDouble(b.BonDeSortie_QuantiteDemandee.ToString("0.0000")) > test).Include(p => p.Unite_Mesure).AsEnumerable();
                    foreach (BonDetails details in bonDetails)
                    {
                        details.BonDeSortie_QuantiteLivree = ListBons.Where(p => p.ID == details.BonDetails_ID).FirstOrDefault().value;
                        _db.Entry(details).State = EntityState.Modified;
                        var matiere = _db.matierePremieres.Where(p => p.MatierePremiere_Id == details.BonDeSortie_MatiereId)
                            .Include(p=>p.Unite_Mesure)
                           .Where(p => p.MatierePremiere_IsActive == 1)
                           .FirstOrDefault();
                        if (matiere != null)
                        {
                            if (details.BonDeSortie_QuantiteLivree != 0)
                            {
                                var matierestock = _db.matierePremiereStockages
                                .Where(s => s.MatierePremiereStokage_MatierePremiereId == details.BonDeSortie_MatiereId)
                                .Where(p => p.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == adresse)
                                .Include(s => s.Matiere_Premiere)
                                .Include(s => s.Section_Stockage).ThenInclude(s => s.Zone_Stockage).ThenInclude(s => s.Lieu_Stockage)
                                .FirstOrDefault();
                                if (matierestock != null) 
                                { 
                                    var k = _db.mouvements.Where(e => e.MouvementStock_MatierePremiereStokageId == matierestock.MatierePremiereStokage_Id).AsEnumerable();
                                    foreach (var mouv in k)
                                    {
                                        mouv.MouvementStock_IsActive = 0;
                                        _db.Entry(mouv).State = EntityState.Modified;
                                    }
                                MouvementStock m = new MouvementStock()
                                {
                                    MouvementStock_MatierePremiereStokageId = matierestock.MatierePremiereStokage_Id,
                                    MouvementStock_Quantite = (decimal)details.BonDeSortie_QuantiteLivree,
                                    MouvementStock_UniteMesureId = details.BonDeSortie_UniteMesureId,
                                    MouvementStock_Date = plan.PlanificationJournee_DateCreation,
                                    MouvementStock_DateReception = DateTime.Now,
                                    MouvementStock_DateSaisie = DateTime.Now,
                                    MouvementStock_MouvementTypeId = 2,
                                    MouvementStock_IsActive = 1,
                                    MouvementStock_AbonnementId = plan.PlanificationJournee_AbonnementID,
                                    MouvementStock_DateCreation = DateTime.Now,
                                    MouvementStock_DestinationStockId = null,
                                };
                                    decimal qte = 0;
                                    if ((details.BonDeSortie_UniteMesureId == 1 && matiere.MatierePremiere_AchatUniteMesureId == 2) || (details.BonDeSortie_UniteMesureId == 4 && matiere.MatierePremiere_AchatUniteMesureId == 3))
                                        qte = (decimal)details.BonDeSortie_QuantiteLivree / 1000;
                                    else if ((details.BonDeSortie_UniteMesureId == 2 && matiere.MatierePremiere_AchatUniteMesureId == 1) || (details.BonDeSortie_UniteMesureId == 3 && matiere.MatierePremiere_AchatUniteMesureId == 4))
                                        qte = (decimal)details.BonDeSortie_QuantiteLivree * 1000;
                                    else
                                        qte = (decimal)details.BonDeSortie_QuantiteLivree;
                                    var qterest = matierestock.MatierePremiereStokage_QuantiteActuelle - qte;
                                    if (qterest < 0)
                                    {
                                        var matieres = new matStockViewModel()
                                        {
                                            matiereLibelle = matierestock.Matiere_Premiere.MatierePremiere_Libelle,
                                            qteLivrer = details.BonDeSortie_QuantiteLivree,
                                            qteEnStock = matiere.MatierePremiere_QuantiteActuelle,
                                            uniteStock = matiere.Unite_Mesure.UniteMesure_Libelle,
                                            uniteLivrer = details.Unite_Mesure.UniteMesure_Libelle,
                                        };
                                        matStock.Matieres.Add(matieres);
                                    }
                                    else
                                    {
                                    m.MouvementStock_MatiereQuantiteActuelle = qterest;
                                    matierestock.MatierePremiereStokage_QuantiteActuelle = m.MouvementStock_MatiereQuantiteActuelle;
                                    matStock.flag = true;
                                    _db.Entry(matierestock).State = EntityState.Modified;
                                    //mouvement.Add(m);
                                    if (m.MouvementStock_Quantite > 0)
                                        await _db.mouvements.AddAsync(m);
                                    }
                                }
                                else
                                {
                                    var matieres = new matStockViewModel()
                                    {
                                        matiereLibelle = matiere.MatierePremiere_Libelle,
                                        qteLivrer = details.BonDeSortie_QuantiteLivree,
                                        qteEnStock =0,
                                        uniteStock = matiere.Unite_Mesure.UniteMesure_Libelle,
                                        uniteLivrer = details.Unite_Mesure.UniteMesure_Libelle,
                                    };
                                    matStock.Matieres.Add(matieres);
                                    matStock.flag = false;

                                }
                            }
                        }
                        else
                            matStock.flag = false;
                    }
                    if (matStock.Matieres.Count() > 0 || matStock.flag==false)
                    {

                        matStock.flag = false; 
                        return matStock;

                    }
                    else
                    {
                        matStock.flag = true;
                        _db.Entry(plan).State = EntityState.Modified;
                        await unitOfWork.Complete();
                        transaction.Commit(); 
                        return matStock;

                    }


                }
                catch
                {
                    transaction.Rollback();
                    matStock.flag = false; 
                    return matStock;

                }
            }
        }
        public async Task<MatStockFlagModel> ValiderPlanification(int planificationId)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                var matStockFlag = new MatStockFlagModel();
                try
                {
                    var plan = _db.planificationJournees.Where(s => s.PlanificationJournee_ID == planificationId).Include(p=>p.BonDe_Sortie).ThenInclude(p=>p.Bon_Details).FirstOrDefault();
                    plan.PlanificationJournee_Etat = "Recue";
                    foreach(var d in plan.BonDe_Sortie.Bon_Details)
                    {
                        var matEnStock = _db.MatiereStock_Ateliers.Where(p => p.MatiereStockAtelier_AtelierID == plan.PlanificationJournee_AtelierID && p.MatiereStockAtelier_MatierePremiereID == d.BonDeSortie_MatiereId).Include(p=>p.Matiere_Premiere).FirstOrDefault();
                        if (matEnStock != null)
                        {
                            decimal qte = 0;
                            if ((d.BonDeSortie_UniteMesureId == 1 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 2) || (d.BonDeSortie_UniteMesureId == 4 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 3))
                                qte = (decimal)d.BonDeSortie_QuantiteLivree / 1000;
                            else if ((d.BonDeSortie_UniteMesureId == 2 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 1) || (d.BonDeSortie_UniteMesureId == 3 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 4))
                                qte = (decimal)d.BonDeSortie_QuantiteLivree * 1000;
                            else
                                qte = (decimal)d.BonDeSortie_QuantiteLivree;

                            matEnStock.MatiereStockAtelier_QauntiteActuelle += qte; 
                            matEnStock.MatiereStockAtelier_QuatiteAvecPlanification += qte; 
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
                                qte = (decimal)d.BonDeSortie_QuantiteLivree / 1000;
                            else if ((d.BonDeSortie_UniteMesureId == 2 && mat.MatierePremiere_AchatUniteMesureId == 1) || (d.BonDeSortie_UniteMesureId == 3 && mat.MatierePremiere_AchatUniteMesureId == 4))
                                qte = (decimal)d.BonDeSortie_QuantiteLivree * 1000;
                            else
                                qte = (decimal)d.BonDeSortie_QuantiteLivree;

                            matStock.MatiereStockAtelier_QauntiteActuelle = qte;
                            matStock.MatiereStockAtelier_QuatiteAvecPlanification += 0;
                            await _db.MatiereStock_Ateliers.AddAsync(matStock);
                        }
                    }
                    _db.Entry(plan).State = EntityState.Modified;
                    await unitOfWork.Complete();
                    transaction.Commit();
                    matStockFlag.flag = true;
                    return matStockFlag;
                }
                catch
                {
                    transaction.Rollback(); 
                    matStockFlag.flag = false;
                    return matStockFlag;
                }
            }
        }

        public IEnumerable<PlanificationdeProduction> getlPlansWithQte(int aboId)
        {
            var plans = _db.planificationdeProductions
                .Where(e => e.PlanificationProduction_AbonnementId == aboId)
                .Where(e => e.PlanificationProduction_QuantiteProduite > 0)
                .Include(e => e.Produit_Vendable)
                .AsEnumerable();
            return plans;
        }

        public IEnumerable<ProduitAppro> GetProduitAppros(int aboId,int atelierId, int produitId)
        {
            var list = _db.produitAppros.Where(p => p.ProduitAppro_AbonnementID == aboId)
                .Where(p => p.ProduitAppro_AtelierID == atelierId && p.ProduitAppro_ProduitID == produitId)
                .Include(p => p.Forme_Produit)
                .Include(p => p.Produit_Vendable)
                .AsEnumerable();
            return list;
        }

        public decimal FindFormulaireProduitAppro(int aboId,int current, int produitApproId)
        {
            var qte = _db.produitAppros.Where(p => p.ProduitAppro_AbonnementID == aboId && p.ProduitAppro_AtelierID == current)
                 .Where(p => p.ProduitAppro_FormeProduitID == produitApproId)
                 .FirstOrDefault();
            if (qte != null)
                return qte.ProduitAppro_QuantiteProduite;
            return 0;
        }

        public async Task<int?> Approvisionnement(Approvisionnement approvisionnement)
        {
            var produitapp = _db.produitAppros.Where(p => p.ProduitAppro_FormeProduitID == approvisionnement.Approvisionnement_FormeProduitId && p.ProduitAppro_AtelierID==approvisionnement.Approvisionnement_AtelierID).FirstOrDefault();
            var plan = _db.planificationdeProductions
                .Where(p => p.PlanificationProduction_FormeProduitId == approvisionnement.Approvisionnement_FormeProduitId)
               // .Where(p => p.PlanificationProduction_QuantiteRestante >= approvisionnement.Approvisionnement_Quantite)
                .OrderBy(p => p.PlanificationProduction_DateModification).AsEnumerable();
            produitapp.ProduitAppro_QuantiteProduite -= approvisionnement.Approvisionnement_Quantite;
            foreach (var item in plan)
            {
                var qte = item.PlanificationProduction_QuantiteRestante - approvisionnement.Approvisionnement_Quantite;
                if (qte < 0) 
                {
                    item.PlanificationProduction_QuantiteRestante = 0;
                    approvisionnement.Approvisionnement_Quantite = -qte;
                }
                else
                {
                    item.PlanificationProduction_QuantiteRestante = qte;
                    break;
                }
                _db.Entry(item).State = EntityState.Modified;

            }
            //decimal qte = approvisionnement.Approvisionnement_Quantite;

                approvisionnement.Approvisionnement_DateModification = DateTime.Now;
                approvisionnement.Approvisionnement_DateSaisie = DateTime.Now;
                approvisionnement.Approvisionnement_Date = Convert.ToDateTime(approvisionnement.Approvisionnement_Date);
                approvisionnement.Approvisionnement_CoutDeRevient = plan.ElementAt(0).PlanificationProduction_CoutRevient;
                _db.Entry(produitapp).State = EntityState.Modified;
                await _db.approvisionnements.AddAsync(approvisionnement);
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                {
                    return approvisionnement.Approvisionnement_Id;
                }
                else { return null; }
        }

        public IEnumerable<Approvisionnement> ListeApprovisionnement(int Id, int? ateId ,int?  point, string date, int? etat)
        {
            var query = _db.approvisionnements
                .Where(a => a.Approvisionnement_AbonnementId == Id);

            if (ateId != null)
                query = query.Where(p => p.Approvisionnement_AtelierID == ateId);
            if (point != null)
                query = query.Where(p => p.Approvisionnement_PointVenteId == point);
            if (etat != null)
                query = query.Where(p => p.Approvisionnement_Etat == etat);
            if (date != "")
            {
                DateTime newDate = Convert.ToDateTime(date);
                query = query.Where(p => p.Approvisionnement_Date.Date == newDate.Date);
            }

            return query
                .Include(a => a.Point_Vente)
                .Include(a => a.Produit_Vendable).ThenInclude(p => p.Unite_Mesure)
                .Include(a => a.Forme_Produit)
                .Include(a => a.Atelier)
                .AsEnumerable();
        }
        public IEnumerable<Approvisionnement> ListeApprovisionnementAtelier(int Id, int atelierId,int? pdv, string date, int? etat)
        {
            IEnumerable<Approvisionnement> appro;
            if (date != "")
            {
                if (pdv != null)
                {
                    appro = _db.approvisionnements.Where(a => a.Approvisionnement_AbonnementId == Id && a.Approvisionnement_AtelierID == atelierId && a.Approvisionnement_Etat == etat)
                        .Where(p=>p.Approvisionnement_PointVenteId==pdv && Convert.ToDateTime(p.Approvisionnement_Date).ToString("yyyy-MM-dd") == date)
                        .Include(a => a.Point_Vente)
                        .Include(a => a.Produit_Vendable).ThenInclude(p => p.Unite_Mesure)
                        .Include(a => a.Forme_Produit)
                        .Include(a => a.Atelier)
                        ;
                }
                else
                {
                    appro = _db.approvisionnements.Where(a => a.Approvisionnement_AbonnementId == Id && a.Approvisionnement_AtelierID == atelierId && a.Approvisionnement_Etat == etat)
                        .Where(p => Convert.ToDateTime(p.Approvisionnement_Date).ToString("yyyy-MM-dd") == date)
                        .Include(a => a.Point_Vente)
                        .Include(a => a.Produit_Vendable).ThenInclude(p => p.Unite_Mesure)
                        .Include(a => a.Forme_Produit)
                        .Include(a => a.Atelier)
                        ;
                }
            }
            else
            {
                if (pdv != null)
                {
                    appro = _db.approvisionnements.Where(a => a.Approvisionnement_AbonnementId == Id && a.Approvisionnement_AtelierID == atelierId && a.Approvisionnement_Etat == etat)
                        .Where(p => p.Approvisionnement_PointVenteId == pdv)
                        .Include(a => a.Point_Vente)
                        .Include(a => a.Produit_Vendable).ThenInclude(p => p.Unite_Mesure)
                        .Include(a => a.Forme_Produit)
                        .Include(a => a.Atelier)
                        ;
                }
                else
                {
                    appro = _db.approvisionnements.Where(a => a.Approvisionnement_AbonnementId == Id && a.Approvisionnement_AtelierID == atelierId && a.Approvisionnement_Etat == etat)
                        .Include(a => a.Point_Vente)
                        .Include(a => a.Produit_Vendable).ThenInclude(p => p.Unite_Mesure)
                        .Include(a => a.Forme_Produit)
                        .Include(a => a.Atelier)
                        ;
                }
            }
            return appro.AsEnumerable();
        }
        public IEnumerable<Approvisionnement> ListeApprovisionnementPointVente(int Id, int pdvId,int? atelier,string date)
        {
            IEnumerable<Approvisionnement> appro;
            if (date != "")
            {
                if (atelier != null)
                {
                    appro = _db.approvisionnements.Where(a => a.Approvisionnement_AbonnementId == Id && a.Approvisionnement_PointVenteId == pdvId)
                        .Where(p => p.Approvisionnement_AtelierID == atelier && Convert.ToDateTime(p.Approvisionnement_Date).ToString("yyyy-MM-dd") == date)
                        .Include(a => a.Point_Vente)
                        .Include(a => a.Produit_Vendable).ThenInclude(p => p.Unite_Mesure)
                        .Include(a => a.Forme_Produit)
                        .Include(a => a.Atelier)
                        .AsEnumerable();
                }
                else
                {
                    appro = _db.approvisionnements.Where(a => a.Approvisionnement_AbonnementId == Id && a.Approvisionnement_PointVenteId == pdvId)
                        .Where(p => Convert.ToDateTime(p.Approvisionnement_Date).ToString("yyyy-MM-dd") == date)
                        .Include(a => a.Point_Vente)
                        .Include(a => a.Produit_Vendable).ThenInclude(p => p.Unite_Mesure)
                        .Include(a => a.Forme_Produit)
                        .Include(a => a.Atelier)
                        .AsEnumerable();
                }
            }
            else
            {
                if (atelier != null)
                {
                    appro = _db.approvisionnements
                        .Where(a => a.Approvisionnement_AbonnementId == Id && a.Approvisionnement_PointVenteId == pdvId)
                        .Where(p=>p.Approvisionnement_AtelierID == atelier)
                        .Include(a => a.Point_Vente)
                        .Include(a => a.Produit_Vendable).ThenInclude(p => p.Unite_Mesure)
                        .Include(a => a.Forme_Produit)
                        .Include(a => a.Atelier)
                        .AsEnumerable();
                }
                else
                {
                    appro = _db.approvisionnements
                        .Where(a => a.Approvisionnement_AbonnementId == Id && a.Approvisionnement_PointVenteId == pdvId)
                        .Include(a => a.Point_Vente)
                        .Include(a => a.Produit_Vendable).ThenInclude(p => p.Unite_Mesure)
                        .Include(a => a.Forme_Produit)
                        .Include(a => a.Atelier)
                        .AsEnumerable();
                }
            }
            return appro;
        }

        public async Task<bool> ValiderApprovisionnement(int ApprovisionnementId,string valideId,int pdvId)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var appro = _db.approvisionnements.Where(a => a.Approvisionnement_Id == ApprovisionnementId).FirstOrDefault();
                    appro.Approvisionnement_Etat = 1;
                    appro.Approvisionnement_ValideParId = valideId;
                    var pdvS = _db.pointVente_Stocks.Where(p => p.PointVenteStock_ProduitID == appro.Approvisionnement_ProduitId)
                        .Where(p => p.PointVenteStock_FormeProduitID == appro.Approvisionnement_FormeProduitId && p.PointVenteStock_PointVenteID==pdvId)
                        .FirstOrDefault();
                    if (pdvS == null)
                    {
                        PointVente_Stock pointVente = new PointVente_Stock
                        {
                            PointVenteStock_FormeProduitID = appro.Approvisionnement_FormeProduitId,
                            PointVenteStock_ProduitID = appro.Approvisionnement_ProduitId,
                            PointVenteStock_PointVenteID = appro.Approvisionnement_PointVenteId,
                            PointVenteStock_QuantiteProduit = appro.Approvisionnement_Quantite,
                            PointVenteStock_DateModification = DateTime.Now,
                            PointVenteStock_AbonnementID = appro.Approvisionnement_AbonnementId,

                        };
                        await _db.pointVente_Stocks.AddAsync(pointVente);

                    }
                    else if (pdvS != null)
                    {
                        pdvS.PointVenteStock_QuantiteProduit += appro.Approvisionnement_Quantite;
                        _db.Entry(pdvS).State = EntityState.Modified;

                    }
                    _db.Entry(appro).State = EntityState.Modified;
                    await unitOfWork.Complete();

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<int?> PackageProduit(ProduitPackage produitPackage)
        {
            foreach (var p in produitPackage.Sous_ProduitPackage)
            {

                p.SousProduitPackage_AbonnementID = produitPackage.ProduitPackage_AbonnementID;
                p.SousProduitPackage_DateCreation = DateTime.Now;
               
            }
            foreach (var p in produitPackage.SousMatierePackages)
            {

                p.SousMatierePackage_AbonnementID = produitPackage.ProduitPackage_AbonnementID;
                p.SousMatierePackage_DateCreation = DateTime.Now;
               
            }
            await _db.produitPackages.AddAsync(produitPackage);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return produitPackage.ProduitPackage_ID;
            else
                return null;
        }

/*        public async Task<bool> AjouterPrixProduit(int id, IEnumerable<PrixProduit> prix)
        {
            var forme = _db.forme_Produits.Where(f => f.FormeProduit_ID == id).FirstOrDefault();

            if (forme != null)
            {
                forme.prixProduits = (ICollection<PrixProduit>)prix;
                _db.Entry(forme).State = EntityState.Modified;

                foreach (var p in prix)
                {
                    await _db.prixProduits.AddAsync(p);

                }
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;

        }*/

        public decimal GetPrix(int Id, decimal qte)
        {
            var prixlist = _db.prixProduits
                .Where(p => p.PrixProduit_FormeProduitId == Id)
                .AsEnumerable();
            decimal prix = 0;
            foreach (var p in prixlist)
            {
                if (qte >= p.PrixProduit_QuantiteMinimale)
                {
                    prix = p.PrixProduit_Prix;
                }
            }
            return prix;
        }

        public IEnumerable<Forme_Produit> getListFormeProduits(int AboId, int produitId)
        {
            var f = _db.forme_Produits.Where(p => p.FormeProduit_AbonnementID == AboId)
                .Where(p => p.FormeProduit_ProduitID == produitId)
                .Include(p => p.Produit_Vendable).ThenInclude(p=>p.Unite_Mesure)
                .AsEnumerable();
            return f;
        }
        public IEnumerable<Forme_Produit> getListFormes(int AboId, int produitId)
        {
            var f = _db.forme_Produits.Where(p => p.FormeProduit_AbonnementID == AboId)
                .Where(p => p.FormeProduit_ProduitID == produitId)
                .Include(p => p.Produit_Vendable).ThenInclude(p => p.Unite_Mesure)
                .AsEnumerable();
            if (f.Count() == 0 )
            {
              
               var k = _db.forme_Produits.Where(p => p.FormeProduit_AbonnementID == AboId)              
                    .Where(p => p.FormeProduit_ProduitPretId == produitId)
                    .Include(p => p.Produit_PretConsomer).ThenInclude(p => p.Unite_Mesure)
                    .AsEnumerable();
                if(k.Count() != 0)
                {
                    return k;
                }
                else
                {
                    var m = _db.forme_Produits.Where(p => p.FormeProduit_AbonnementID == AboId)
                         .Where(p => p.FormeProduit_ProduitPackageId == produitId)
                         .Include(p => p.ProduitPackage).ThenInclude(p => p.Unite_Mesure)
                         .AsEnumerable();
                    return m;
                }
            }                
            return f;         
        }

        public async Task<bool> AjouterFormeProduit(int id, Forme_Produit forme)
        {

            var produit = _db.produitVendables
                .Where(p => p.ProduitVendable_Id == id).FirstOrDefault();
            if (produit != null)
            {
                forme.FormeProduit_DateCreation = DateTime.Now;
                produit.formes.Add(forme);
                _db.Entry(produit).State = EntityState.Modified;
                await _db.forme_Produits.AddAsync(forme);
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<int?> RetourStock(RetourStock retour)
        {
            retour.RetourStok_DateCreation = DateTime.Now;
            retour.BonDe_Sortie.BonDeSortie_DateProduction = _db.planificationJournees
                .Where(p => p.PlanificationJournee_ID == retour.RetourStok_PlanificationID)
                .Select(p => p.PlanificationJournee_Date)
                .FirstOrDefault();
            await _db.retours.AddAsync(retour);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
            {
                return retour.RetourStok_ID;
            }
            else { return null; }
        }

        public async Task<bool> AccepterRetour(int Id, string adresse, string valideId)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var retour = _db.retours.Where(s => s.RetourStok_ID == Id).FirstOrDefault();
                    retour.RetourStok_Etat = 1;
                    retour.RetourStok_ValideParID = valideId;
                    List<MouvementStock> mouvement = new List<MouvementStock>();
                    var bonDetails = _db.bonDetails
                        .Where(b => b.BonDeSortie_BonDeSortieID == retour.RetourStok_BonDeSortieID).AsEnumerable();
                    foreach (BonDetails details in bonDetails)
                    {
                        var matiere = _db.matierePremieres.Where(p => p.MatierePremiere_Id == details.BonDeSortie_MatiereId)
                            .Where(p => p.MatierePremiere_IsActive == 1)
                            .FirstOrDefault();

                        var matierestock = _db.matierePremiereStockages
                            .Where(s => s.MatierePremiereStokage_MatierePremiereId == details.BonDeSortie_MatiereId)
                            .Where(p => p.Section_Stockage.Zone_Stockage.Lieu_Stockage.LieuStockag_Nom == adresse)
                            .Include(s => s.Section_Stockage).ThenInclude(s => s.Zone_Stockage).ThenInclude(s => s.Lieu_Stockage)
                            .FirstOrDefault();
                        
                        var k = _db.mouvements.Where(e => e.MouvementStock_MatierePremiereStokageId == matierestock.MatierePremiereStokage_Id).AsEnumerable();
                        foreach (var mouv in k)
                        {
                            mouv.MouvementStock_IsActive = 0;
                            _db.Entry(mouv).State = EntityState.Modified;
                        }                                
                        MouvementStock m = new MouvementStock()
                        {                        
                            MouvementStock_MatierePremiereStokageId = matierestock.MatierePremiereStokage_Id,
                            MouvementStock_Quantite = details.BonDeSortie_Quantite,
                            MouvementStock_UniteMesureId = details.BonDeSortie_UniteMesureId,
                            MouvementStock_Date = retour.RetourStok_DateCreation,
                            MouvementStock_DateReception = DateTime.Now,
                            MouvementStock_DateSaisie = DateTime.Now,                            
                            MouvementStock_MouvementTypeId = 5,
                            MouvementStock_AbonnementId = retour.RetourStok_AbonnementID,                            
                            MouvementStock_DateCreation = DateTime.Now,
                            MouvementStock_MatiereQuantiteActuelle = 0,
                            MouvementStock_IsActive = 1,
                            MouvementStock_DestinationStockId = null,
                        };
                        decimal qte = 0;
                        if ((details.BonDeSortie_UniteMesureId == 1 && matiere.MatierePremiere_AchatUniteMesureId == 2) || (details.BonDeSortie_UniteMesureId == 4 && matiere.MatierePremiere_AchatUniteMesureId == 3))
                            qte = details.BonDeSortie_Quantite / 1000;                            
                        else if ((details.BonDeSortie_UniteMesureId == 2 && matiere.MatierePremiere_AchatUniteMesureId == 1) || (details.BonDeSortie_UniteMesureId == 3 && matiere.MatierePremiere_AchatUniteMesureId == 4))
                            qte = details.BonDeSortie_Quantite * 1000;                            
                        else
                            qte = details.BonDeSortie_Quantite;
                        
                        m.MouvementStock_MatiereQuantiteActuelle = matierestock.MatierePremiereStokage_QuantiteActuelle + qte;
                        
                        matierestock.MatierePremiereStokage_QuantiteActuelle = m.MouvementStock_MatiereQuantiteActuelle;
                        
                        _db.Entry(matiere).State = EntityState.Modified;
                        mouvement.Add(m);
                    }
                    foreach (var m in mouvement)
                    {
                        await _db.mouvements.AddAsync(m);
                        var i = _db.mouvements.Where(p => p.MouvementStock_MatierePremiereStokageId == m.MouvementStock_MatierePremiereStokageId).AsEnumerable();
                        foreach (var move in i)
                        {
                            move.MouvementStock_IsActive = 0;
                            _db.Entry(move).State = EntityState.Modified;
                        }
                    }
                    _db.Entry(retour).State = EntityState.Modified;
                    await unitOfWork.Complete();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public IEnumerable<RetourStock> GetRetourStocks(int Id)
        {
            return _db.retours.Where(d => d.RetourStok_AbonnementID == Id)
              .Include(d => d.Planification_Journee)
              .ThenInclude(p => p.Atelier)
              .Include(d => d.BonDe_Sortie).AsEnumerable();
        } 
        public IEnumerable<RetourStock> GetRetourStocksByAtelier(int Id, int atelierId)
        {
            return _db.retours.Where(d => d.RetourStok_AbonnementID == Id && d.Planification_Journee.PlanificationJournee_AtelierID == atelierId)
              .Include(d => d.Planification_Journee)
              .ThenInclude(p => p.Atelier)
              .Include(d => d.BonDe_Sortie).AsEnumerable();
        }  
        public IEnumerable<RetourStock> GetRetourStocksByStock(int Id, int stockId)
        {
            return _db.retours.Where(d => d.RetourStok_AbonnementID == Id && d.Planification_Journee.PlanificationJournee_LieuStockageID == stockId)
              .Include(d => d.Planification_Journee)
              .ThenInclude(p => p.Atelier)
              .Include(d => d.BonDe_Sortie).AsEnumerable();
        }

        public async Task<int?> ProduitConsomable(ProduitPretConsomer produitPret)
        {
            if (produitPret.ProduitPretConsomer_StockMinimun > produitPret.ProduitPretConsomer_StockMaximum)
            {
                return null;
            }
            produitPret.ProduitPretConsomer_IsActive = 1;
            produitPret.
                ProduitPretConsomer_DateCreation= DateTime.Now;
            /*            var forme = new Forme_Produit
                        {
                            FormeProduit_Libelle = "Standard",
                            FormeProduit_AbonnementID = (int)produitVendable.ProduitVendable_AbonnementId,
                            FormeProduit_DateCreation = (DateTime)produitVendable.ProduitVendable_DateCreation,
                          //  FormeProduit_ProduitID = produitVendable.ProduitVendable_Id,
                        };
                        produitVendable.formes.Add(forme);*/
            await _db.produitPrets.AddAsync(produitPret);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return produitPret.ProduitPretConsomer_ID;
            else
                return null;
        }

        public IEnumerable<ProduitPretConsomer> getListProduitConsomable(int Id,int? categ,int? SousCateg)
        {
            IEnumerable<ProduitPretConsomer> produits;
            if (categ != null)
            {
                if (SousCateg!=null)
                {
                    produits = _db.produitPrets.Where(p => p.ProduitPretConsomer_AbonnementID == Id).Where(x => x.ProduitPretConsomer_IsActive == 1 && x.ProduitPretConsomer_FamilleProduit==SousCateg)
                        .Include(p => p.Unite_Mesure)
                        .Include(p => p.Forme_Stockage)
                        .Include(p => p.formes)
                        .AsEnumerable();
                }
                else
                {
                    produits = _db.produitPrets.Where(p => p.ProduitPretConsomer_AbonnementID == Id).Where(x => x.ProduitPretConsomer_IsActive == 1 && x.Sous_Famille.SousFamille_ParentID==categ)
                        .Include(p => p.Unite_Mesure)
                        .Include(p => p.Forme_Stockage)
                        .Include(p => p.formes)
                        .AsEnumerable();
                }
            }
            else
            {
                if (SousCateg != null)
                {
                    produits = _db.produitPrets.Where(p => p.ProduitPretConsomer_AbonnementID == Id).Where(x => x.ProduitPretConsomer_IsActive == 1 && x.ProduitPretConsomer_FamilleProduit==SousCateg)
                        .Include(p => p.Unite_Mesure)
                        .Include(p => p.Forme_Stockage)
                        .Include(p => p.formes)
                        .AsEnumerable();
                }
                else
                {
                    produits = _db.produitPrets.Where(p => p.ProduitPretConsomer_AbonnementID == Id).Where(x => x.ProduitPretConsomer_IsActive == 1)
                        .Include(p => p.Unite_Mesure)
                        .Include(p => p.Forme_Stockage)
                        .Include(p => p.formes)
                        .AsEnumerable();
                }
            }
            return produits;
        }
        public IEnumerable<PdV_ProduitsPret> getListProduitConsomableEnStock(int Id,int pdv)
        {
            return _db.pdV_ProduitsPrets.Where(p => p.PdV_ProduitsPret_AbonnementId == Id && p.PdV_ProduitsPret_PointVenteId == pdv)
                .Include(p => p.Produit_PretConsomer)
                .AsEnumerable();
        }
        public IEnumerable<ProduitPretConsomer> getListProduitConsomableEnMagasin(int Id,int pdv, int? familleID)
        {
            if (familleID != null)
            {
                return _db.pdV_ProduitsPrets.Where(p => p.PdV_ProduitsPret_AbonnementId == Id && p.PdV_ProduitsPret_PointVenteId == pdv && p.Produit_PretConsomer.ProduitPretConsomer_FamilleProduit == familleID)
                   .Include(p => p.Produit_PretConsomer).Select(p => p.Produit_PretConsomer).Include(p => p.Unite_Mesure).Distinct().AsEnumerable();
            }
            else {
                return _db.pdV_ProduitsPrets.Where(p => p.PdV_ProduitsPret_AbonnementId == Id && p.PdV_ProduitsPret_PointVenteId == pdv)
                 .Include(p => p.Produit_PretConsomer).Select(p => p.Produit_PretConsomer).Include(p => p.Unite_Mesure).Distinct().AsEnumerable();
            }
        }
        public IEnumerable<Forme_Produit> getFormesConsomableEnMagasin(int Id,int pdv)
        {
            var query = _db.pdV_ProduitsPrets.Where(p => p.PdV_ProduitsPret_AbonnementId == Id && p.PdV_ProduitsPret_PointVenteId == pdv)
                   .Select(p => p.Forme_Produit).AsEnumerable();
            /*if (familleID != null)
                query.Where(p => p.Produit_PretConsomer.ProduitPretConsomer_FamilleProduit == familleID);*/
            //var q = query.GroupBy(p => new { key = p.PdV_ProduitsPret_ProduitConsomableId, item = p.Produit_PretConsomer });
           // var q = query.Select(p=>p.Produit_PretConsomer).Include(p=>p.Unite_Mesure).Distinct().AsEnumerable();//q.Select(p=>p.Key.item);
            return query;
        }

        public async Task<int?> FournisseurProduits(FournisseurProduits fournisseurP)
        {

            fournisseurP.FournisseurProduitConso_IsActive = 1;
            fournisseurP.FournisseurProduitConso_DateCreation = DateTime.Now;
            await _db.fournisseurProduits.AddAsync(fournisseurP);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return fournisseurP.FournisseurProduitConso_Id;
            else
                return null;
        }

        public IEnumerable<Fonction> getListFonction()
        {
            return _db.fonctions.AsEnumerable();
        }

        public IEnumerable<Ville> getListVilles()
        {
            return _db.villes.AsEnumerable();
        }

        public IEnumerable<MatierePremiere> getListMatiere(int Id)
        {
            return _db.matierePremieres.Where(m => m.MatierePremiere_AbonnementId == Id).AsEnumerable();
        }

        public async Task<bool?> AjouterMatiere(int idFournisseur, List<int> listMatiere)
        {
            var fournisseur = _db.fournisseurProduits.Where(f => f.FournisseurProduitConso_Id == idFournisseur).FirstOrDefault();
            var i = 0;
            foreach (var produitId in listMatiere)
            {
                var link = _db.FournisserusProduits_Link.Where(p => p.FournisseurProduits_Id == idFournisseur && p.ProduitConsomable_Id == produitId).FirstOrDefault();
                if (link != null)
                    i++;
                else
                {
                    var produitsPret = _db.produitPrets.Where(m => m.ProduitPretConsomer_ID == produitId).FirstOrDefault();
                    var FournisserusProduits_Link = new Fournisseur_ProduitConso
                    {
                        Produit_PretConsomer = produitsPret,
                        Fournisseur_Produits = fournisseur,
                        Abonnement_Id = fournisseur.FournisseurProduitConso_AbonnementID,
                        IsActive = 1,
                    };
                    await _db.FournisserusProduits_Link.AddAsync(FournisserusProduits_Link);
                }
            }
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return true;
            else
                return null;
        }

        public FournisseurProduits findFormulaireFournisseur(int formulaireFourisseurId)
        {
            return _db.fournisseurProduits.Where(f => f.FournisseurProduitConso_Id == formulaireFourisseurId).FirstOrDefault();
        }

        public bool ProduitConsoStocker(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int?> ApprovisionnementProduitConso(Approvisionnement_ProduitConso approvisionnement)
        {
            approvisionnement.ApprovisionnementProduit_DateCreation = DateTime.Now;
            approvisionnement.ApprovisionnementProduit_Etat = "En traitement";
            await _db.approvisionnement_ProduitConsos.AddAsync(approvisionnement);
            var stockage = _db.produitConsomableStokages.Where(p => p.ProduitConsomableStokage_Id == approvisionnement.ApprovisionnementProduit_ProduitStockageId).FirstOrDefault();
            stockage.ProduitConsomableStokage_QuantiteActuelle -= approvisionnement.ApprovisionnementProduit_Quantite;
            _db.Entry(stockage).State = EntityState.Modified;
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return approvisionnement.ApprovisionnementProduit_Id;
            else
                return null;
        }

        public IEnumerable<Approvisionnement_ProduitConso> ListeApprovisionnementProduit(int Id)
        {
            return _db.approvisionnement_ProduitConsos.Where(p => p.ApprovisionnementProduit_AbonnementId == Id && p.ApprovisionnementProduit_DateCreation.ToShortDateString() == DateTime.Now.ToShortDateString())
                .Include(p => p.Point_Vente)
                .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p=>p.Produit_PretConsomer)
                .Include(p => p.Unite_Mesure)
                .Include(p => p.Lieu_Stockage).ThenInclude(p=>p.Ville)
                .AsEnumerable();
        }
        public IEnumerable<Approvisionnement_ProduitConso> ListeApprovisionnementProduitAtelier(int Id, int atelierId, int? pdv, string date)
        {
            IEnumerable<Approvisionnement_ProduitConso>appro;
            if (date != "")
            {
                if (pdv != null)
                {
                    appro = _db.approvisionnement_ProduitConsos.Where(p => p.ApprovisionnementProduit_AbonnementId == Id && p.ApprovisionnementProduit_StockID == atelierId)
                        .Where(p=>p.ApprovisionnementProduit_PointVenteID==pdv && Convert.ToDateTime(p.ApprovisionnementProduit_Date).ToString("yyyy-MM-dd") == date)
                        .Include(p => p.Point_Vente)
                        .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p => p.Produit_PretConsomer)
                        .Include(p => p.Unite_Mesure)
                        .Include(p => p.Lieu_Stockage).ThenInclude(p => p.Ville)
                        .AsEnumerable();
                }
                else
                {
                    appro = _db.approvisionnement_ProduitConsos.Where(p => p.ApprovisionnementProduit_AbonnementId == Id && p.ApprovisionnementProduit_StockID == atelierId)
                        .Where(p => Convert.ToDateTime(p.ApprovisionnementProduit_Date).ToString("yyyy-MM-dd") == date)
                        .Include(p => p.Point_Vente)
                        .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p => p.Produit_PretConsomer)
                        .Include(p => p.Unite_Mesure)
                        .Include(p => p.Lieu_Stockage).ThenInclude(p => p.Ville)
                        .AsEnumerable();
                }
            }
            else
            {
                if (pdv != null)
                {
                    appro = _db.approvisionnement_ProduitConsos.Where(p => p.ApprovisionnementProduit_AbonnementId == Id && p.ApprovisionnementProduit_StockID == atelierId)
                        .Where(p => p.ApprovisionnementProduit_PointVenteID == pdv)
                        .Include(p => p.Point_Vente)
                        .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p => p.Produit_PretConsomer)
                        .Include(p => p.Unite_Mesure)
                        .Include(p => p.Lieu_Stockage).ThenInclude(p => p.Ville)
                        .AsEnumerable();
                }
                else
                {
                    appro = _db.approvisionnement_ProduitConsos.Where(p => p.ApprovisionnementProduit_AbonnementId == Id && p.ApprovisionnementProduit_StockID == atelierId && p.ApprovisionnementProduit_Date.ToShortDateString() == DateTime.Now.ToShortDateString())
                        .Include(p => p.Point_Vente)
                        .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p => p.Produit_PretConsomer)
                        .Include(p => p.Unite_Mesure)
                        .Include(p => p.Lieu_Stockage).ThenInclude(p => p.Ville)
                        .AsEnumerable();
                }
            }
            return appro;
        } 
        public IEnumerable<Approvisionnement_ProduitConso> ListeApprovisionnementProduitPdv(int Id, int pdvId, int? atelier, string date)
        {
            IEnumerable<Approvisionnement_ProduitConso> appro;
            if (date != "")
            {
                if (atelier != null)
                {
                    appro = _db.approvisionnement_ProduitConsos.Where(p => p.ApprovisionnementProduit_AbonnementId == Id && p.ApprovisionnementProduit_PointVenteID == pdvId)
                        .Where(p=>p.ApprovisionnementProduit_StockID == atelier && Convert.ToDateTime(p.ApprovisionnementProduit_Date).ToString("yyyy-MM-dd") == date)
                        .Include(p => p.Point_Vente)
                        .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p => p.Produit_PretConsomer)
                        .Include(p => p.Unite_Mesure)
                        .Include(p => p.Lieu_Stockage).ThenInclude(p => p.Ville)
                        .AsEnumerable();
                }
                else
                {
                    appro = _db.approvisionnement_ProduitConsos.Where(p => p.ApprovisionnementProduit_AbonnementId == Id && p.ApprovisionnementProduit_PointVenteID == pdvId)
                        .Where(p => Convert.ToDateTime(p.ApprovisionnementProduit_Date).ToString("yyyy-MM-dd") == date)
                        .Include(p => p.Point_Vente)
                        .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p => p.Produit_PretConsomer)
                        .Include(p => p.Unite_Mesure)
                        .Include(p => p.Lieu_Stockage).ThenInclude(p => p.Ville)
                        .AsEnumerable();
                }
            }
            else
            {
                if (atelier != null)
                {
                    appro = _db.approvisionnement_ProduitConsos.Where(p => p.ApprovisionnementProduit_AbonnementId == Id && p.ApprovisionnementProduit_PointVenteID == pdvId)
                        .Where(p => p.ApprovisionnementProduit_StockID == atelier)
                        .Include(p => p.Point_Vente)
                        .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p => p.Produit_PretConsomer)
                        .Include(p => p.Unite_Mesure)
                        .Include(p => p.Lieu_Stockage).ThenInclude(p => p.Ville)
                        .AsEnumerable();
                }
                else
                {
                    appro = _db.approvisionnement_ProduitConsos.Where(p => p.ApprovisionnementProduit_AbonnementId == Id && p.ApprovisionnementProduit_PointVenteID == pdvId && p.ApprovisionnementProduit_Date.ToShortDateString() == DateTime.Now.ToShortDateString())
                        .Include(p => p.Point_Vente)
                        .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p => p.Produit_PretConsomer)
                        .Include(p => p.Unite_Mesure)
                        .Include(p => p.Lieu_Stockage).ThenInclude(p => p.Ville)
                        .AsEnumerable();
                }
            }
            return appro;
        }

        public async Task<bool> ValiderApprovisionnementProduit(int ApprovisionnementId,string userid,int pdvId)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var appro = _db.approvisionnement_ProduitConsos.Where(a => a.ApprovisionnementProduit_Id == ApprovisionnementId).Include(p=>p.ProduitConsomable_Stokage).ThenInclude(p=>p.Produit_PretConsomer).FirstOrDefault();
                    appro.ApprovisionnementProduit_Etat = "Livré";
                    appro.ApprovisionnementProduit_PointVenteUserId = userid;
                    var pdvS = _db.pdV_ProduitsPrets.Where(p => p.PdV_ProduitsPret_FormeProduitId == appro.ProduitConsomable_Stokage.ProduitConsomableStokage_FormeProduitId && p.PdV_ProduitsPret_PointVenteId==appro.ApprovisionnementProduit_PointVenteID)
                        .Where(p=>p.PdV_ProduitsPret_PointVenteId==pdvId)
                        .FirstOrDefault();
                    if (pdvS == null)
                    {
                        PdV_ProduitsPret pointVente = new PdV_ProduitsPret
                        {
                            PdV_ProduitsPret_ProduitConsomableId = appro.ProduitConsomable_Stokage.Produit_PretConsomer.ProduitPretConsomer_ID,
                            PdV_ProduitsPret_PointVenteId = appro.ApprovisionnementProduit_PointVenteID,
                            PdV_ProduitsPret_FormeProduitId = appro.ProduitConsomable_Stokage.ProduitConsomableStokage_FormeProduitId,
                            PdV_ProduitsPret_Quantite = appro.ApprovisionnementProduit_Quantite,
                            PdV_ProduitsPret_DateModification = DateTime.Now,
                            PdV_ProduitsPret_AbonnementId = appro.ApprovisionnementProduit_AbonnementId,

                        };
                        await _db.pdV_ProduitsPrets.AddAsync(pointVente);

                    }
                    else if (pdvS != null)
                    {
                        pdvS.PdV_ProduitsPret_Quantite += appro.ApprovisionnementProduit_Quantite;
                        _db.Entry(pdvS).State = EntityState.Modified;

                    }
                    //var stockage = _db.produitConsomableStokages.Where(p => p.ProduitConsomableStokage_Id == appro.ApprovisionnementProduit_ProduitStockageId).FirstOrDefault();
                    //stockage.ProduitConsomableStokage_QuantiteActuelle -= appro.ApprovisionnementProduit_Quantite;
                    //_db.Entry(stockage).State = EntityState.Modified;
                    _db.Entry(appro).State = EntityState.Modified;
                    await unitOfWork.Complete();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public Forme_Produit findFormulaireFormeProduit(int formulaireProduitId)
        {
            return _db.forme_Produits.Where(p => p.FormeProduit_ID == formulaireProduitId).Include(p=>p.Produit_PretConsomer).FirstOrDefault();
        }

        public IEnumerable<ProduitPackage> getListProduitpackage(int aboId)
        {
            var produit =  _db.produitPackages.Where(p => p.ProduitPackage_AbonnementID == aboId)
                  .Include(p => p.Sous_ProduitPackage)
                  .Include(p => p.Sous_Famille)
                  .Include(p => p.Unite_Mesure)
                  .Include(p => p.formes)
                  .AsEnumerable();
            return produit;
        }
        public IEnumerable<ProduitPackage> getListProduitpackagePointVente(int aboId, int pdvid)
        {
            var produit =  _db.produitPackages.Where(p => p.ProduitPackage_AbonnementID == aboId/* && p.ProduitPackage_PointVenteID == pdvid*/)
                .Include(p => p.Sous_ProduitPackage)
                .Include(p=>p.Sous_Famille)
                .Include(p=>p.Unite_Mesure)
                .Include(p => p.formes)
                .AsEnumerable();
            return produit;
        }
               
        public IEnumerable<ProduitPackage> getProduitpackagePointVente(int formulaireProduitId)
        {
            var produit = _db.produitPackages.Where(p => p.ProduitPackage_ID == formulaireProduitId)
                .Include(p => p.formes)
                .Include(p => p.Sous_ProduitPackage).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage).ThenInclude(p => p.Unite_Mesure)
                .Include(p => p.Sous_ProduitPackage).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable).ThenInclude(p => p.Unite_Mesure)
                .Include(p => p.Sous_ProduitPackage).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer).ThenInclude(p => p.Unite_Mesure)
                .AsEnumerable();
            return produit;
        }

        public ProduitPackage findFormulaireProduitPackage(int formulaireProduitId)
        {
            var produit = _db.produitPackages.Where(p => p.ProduitPackage_ID == formulaireProduitId)
                .Include(p => p.formes)
                .Include(p => p.Sous_ProduitPackage).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage).ThenInclude(p => p.Unite_Mesure)
                .Include(p => p.Sous_ProduitPackage).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable).ThenInclude(p => p.Unite_Mesure)
                .Include(p => p.Sous_ProduitPackage).ThenInclude(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer).ThenInclude(p => p.Unite_Mesure)
                .Include(p => p.SousMatierePackages).ThenInclude(p => p.Matiere_Premiere)
                .FirstOrDefault();
            return produit;
        }
        public async Task<bool> AjouterFormeProduitPackage(int id, Forme_Produit forme)
        {
            var produit = _db.produitPackages
                      .Where(p => p.ProduitPackage_ID == id).FirstOrDefault();
            if (produit != null)
            {
                forme.FormeProduit_DateCreation = DateTime.Now;
                forme.FormeProduit_ProduitID = null;
                forme.FormeProduit_ProduitPretId = null;
                forme.FormeProduit_ProduitPackageId = id;
                //produit.formes.Add(forme);
                await _db.forme_Produits.AddAsync(forme);
                var fromeDetails = new FormeDetails
                {
                    FormeDetails_ProduitPackageID = produit.ProduitPackage_ID,
                    //FormeDetails_Libelle = forme.FormeProduit_Libelle,
                    FormeDetails_FormeProduitID = forme.FormeProduit_ID,
                    FormeDetails_AbonnementID = forme.FormeProduit_AbonnementID,
                    FormeDetails_DateCreation = DateTime.Now,
                    FormeDetails_Quantite = 0,
                };
                _db.Entry(produit).State = EntityState.Modified;
                //await _db.formeDetails.AddAsync(fromeDetails);
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> AjouterFormeProduitPretConsomer(int id, Forme_Produit forme)
        {
            var produit = _db.produitPrets
                      .Where(p => p.ProduitPretConsomer_ID == id).FirstOrDefault();
            if (produit != null)
            {
                forme.FormeProduit_DateCreation = DateTime.Now;
                forme.FormeProduit_ProduitID = null;
                produit.formes.Add(forme);
                _db.Entry(produit).State = EntityState.Modified;
                await _db.forme_Produits.AddAsync(forme);
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public IEnumerable<Forme_Produit> getListFormeProduitsPackage(int AboId, int produitId)
        {
            return _db.forme_Produits.Where(p => p.FormeProduit_AbonnementID == AboId)
                .Where(p => p.FormeProduit_ProduitPackageId == produitId)
                .Include(p => p.ProduitPackage).ThenInclude(p=>p.Unite_Mesure)
                .AsEnumerable();
        }

        public IEnumerable<Forme_Produit> getListFormeProduitsPret(int AboId, int produitId)
        {
            return _db.forme_Produits.Where(p => p.FormeProduit_AbonnementID == AboId)
                          .Where(p => p.FormeProduit_ProduitPretId == produitId)
                          .Include(p => p.Produit_PretConsomer).ThenInclude(p=>p.Unite_Mesure)
                          .AsEnumerable();
        }

        public async Task<int?> ProductionPackage(PackageProduction productionpackage,int pdvId)
        {
            var demandePretDetails = new List<DemandePret_Details>();
            productionpackage.PackageProduction_DateCreation = DateTime.Now;
            var produitpackageAtelier = _db.produitPack_Ateliers
                .Where(p => p.ProduitPackAtelier_FormeID == productionpackage.PackageProduction_ProduitPackageID)
                .Where(p => p.ProduitPackAtelier_AbonnementID == productionpackage.PackageProduction_AbonnementID && p.ProduitPackAtelier_AtelierID==pdvId)
                .FirstOrDefault();
            if (produitpackageAtelier != null)
            { 
                produitpackageAtelier.ProduitPackAtelier_Quantite += productionpackage.PackageProduction_Quantite; 
                _db.Entry(produitpackageAtelier).State = EntityState.Modified;

            }
            else
            {
                var prodPackAtelier = new ProduitPack_Atelier()
                {
                    ProduitPackAtelier_FormeID = productionpackage.PackageProduction_ProduitPackageID,
                    ProduitPackAtelier_ProduitPackID = productionpackage.PackageProduction_ProduitID,
                    ProduitPackAtelier_AbonnementID = productionpackage.PackageProduction_AbonnementID,
                    ProduitPackAtelier_Quantite = productionpackage.PackageProduction_Quantite,
                    ProduitPackAtelier_AtelierID = pdvId,
                    ProduitPackAtelier_DateCreation = DateTime.Now,

                };
                await _db.produitPack_Ateliers.AddAsync(prodPackAtelier);

            }
            var fiche = _db.package_Formes.Where(p => p.PackageForme_FormeProduitID == productionpackage.PackageProduction_ProduitPackageID).Include(p=>p.details).FirstOrDefault();
            foreach (var sp in fiche.details)
            {
                var sousProd = _db.sous_ProduitPackages.Where(p => p.SousProduitPackage_ID == sp.PackageFormeDetails_SousProduitID).FirstOrDefault();
                var produitstock = _db.produitAppros.Where(p => p.ProduitAppro_FormeProduitID == sousProd.SousProduitPackage_FormeProduittID && p.ProduitAppro_AtelierID == pdvId)
                    .FirstOrDefault();
                if (produitstock != null)
                {
                    var qte = produitstock.ProduitAppro_QuantiteProduite - (sp.PackageFormeDetails_Quantite * productionpackage.PackageProduction_Quantite);
                    if (qte < 0)
                        return null;
                    else
                    {
                        produitstock.ProduitAppro_QuantiteProduite = qte;
                        _db.Entry(produitstock).State = EntityState.Modified;
                    }
                }
                else
                {
                    var pretStock = _db.produitPret_Ateliers.Where(p => p.ProduitPretAtelier_FormeID == sousProd.SousProduitPackage_FormeProduittID && p.ProduitPretAtelier_AtelierID == pdvId)
                        .FirstOrDefault();
                    if (pretStock != null)
                    {
                        var qte = pretStock.ProduitPretAtelier_Quantite - (sp.PackageFormeDetails_Quantite * productionpackage.PackageProduction_Quantite);
                        if (qte < 0)
                            return null;
                        else
                        {
                            pretStock.ProduitPretAtelier_Quantite = qte;
                            _db.Entry(pretStock).State = EntityState.Modified;
                        }
                    }
                    else
                        return null;

                }


            }
            await _db.packageProductions.AddAsync(productionpackage);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return confirm;
            else
                return null;
        }

        public FormeDetails getFormeDetails(int formulaireProduitId, int? pdvId)
        {
            var query = _db.formeDetails.Where(p => p.FormeDetails_FormeProduitID == formulaireProduitId);                
            if (pdvId != null)
            {
                query.Where(p => p.FormeDetails_PointVenteID == pdvId);
            }
            return query.Include(p => p.Forme_Produit)
                .Include(p => p.ProduitPackage)
                .FirstOrDefault();
        }
        public IEnumerable<FormeDetails> getFormeDetailsAll(int AboId)
        {
            return _db.formeDetails.Where(p => p.FormeDetails_AbonnementID == AboId)
                .AsEnumerable();
        }

        public ProduitPretConsomer findFormulaireProduitPret(int formulaireProduitId)
        {
            return _db.produitPrets.Where(p => p.ProduitPretConsomer_ID == formulaireProduitId)
                           .Include(p => p.formes)
                           .Include(p => p.Fournisseur_Link)
                           .Include(p=>p.Unite_Mesure)
                           .FirstOrDefault();
        }

        public IEnumerable<FournisseurProduits> ListeFournisseurProduits(int Id,int? VilleId,int? statut)
        {
            IEnumerable<FournisseurProduits> fournisseur;
            if (VilleId != null)
            {
                if (statut != null)
                {
                    fournisseur = _db.fournisseurProduits.Where(p => p.FournisseurProduitConso_AbonnementID == Id && p.FournisseurProduitConso_VilleID == VilleId && p.FournisseurProduitConso_IsActive == statut)
                        .Include(p => p.Fournisseur_Contact).Include(x => x.Ville)
                        .AsEnumerable();
                }
                else
                {
                    fournisseur = _db.fournisseurProduits.Where(p => p.FournisseurProduitConso_AbonnementID == Id && p.FournisseurProduitConso_VilleID==VilleId).Include(p => p.Fournisseur_Contact).Include(x => x.Ville)
                        .AsEnumerable();
                }
            }
            else
            {
                if (statut != null)
                {
                    fournisseur = _db.fournisseurProduits.Where(p => p.FournisseurProduitConso_AbonnementID == Id && p.FournisseurProduitConso_IsActive==statut).Include(p => p.Fournisseur_Contact).Include(x => x.Ville)
                        .AsEnumerable();
                }
                else
                {
                    fournisseur = _db.fournisseurProduits.Where(p => p.FournisseurProduitConso_AbonnementID == Id).Include(p => p.Fournisseur_Contact).Include(x => x.Ville)
                        .AsEnumerable();
                }
            }
            return fournisseur;
        }

        public IEnumerable<Atelier> getListAteliers(int aboId)
        {
            return _db.ateliers.Where(p => p.Atelier_AbonnementID == aboId).Where(p => p.Atelier_IsActive == 1).AsEnumerable();
        }

        public IEnumerable<PrixProduit> getListPrix(int Id)
        {
            var prix = _db.prixProduits.Where(p => p.PrixProduit_FormeProduitId == Id).Where(p => p.PrixProduit_IsActive == 1).AsEnumerable();
            return prix;
        }public IEnumerable<PrixProduit> getListPrixFormes(int Id)
        {
            var prix = _db.prixProduits.Where(p => p.PrixProduit_FormeProduitId == Id).Where(p => p.PrixProduit_IsActive == 1)
                .Include(p=>p.Forme_Produit).ThenInclude(p=>p.ProduitPackage).ThenInclude(p=>p.Unite_Mesure)
                .Include(p=>p.Forme_Produit).ThenInclude(p=>p.Produit_Vendable).ThenInclude(p => p.Unite_Mesure)
                .Include(p=>p.Forme_Produit).ThenInclude(p=>p.Produit_PretConsomer).ThenInclude(p => p.Unite_Mesure)
                .AsEnumerable();
            return prix;
        }

        public IEnumerable<Sous_ProduitPackage> getListSousProduitpackage(int produitId)
        {
            var produit = _db.sous_ProduitPackages.Where(p => p.SousProduitPackage_ProduitPackageID == produitId)
                .Include(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage).ThenInclude(p => p.Unite_Mesure)
                .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable).ThenInclude(p => p.Unite_Mesure)
                .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer).ThenInclude(p => p.Unite_Mesure)
                .AsEnumerable();
            return produit;
        }

        public async Task<bool> deleteFournisseurPrdConso(int ID, int code)
        {
            FournisseurProduits fournisseur = _db.fournisseurProduits.Where(e => e.FournisseurProduitConso_Id == ID).FirstOrDefault();
            if (fournisseur != null)
            {
                if (code == 0)
                    fournisseur.FournisseurProduitConso_IsActive = 0;
                else
                    fournisseur.FournisseurProduitConso_IsActive = 1;

                _db.Entry(fournisseur).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public IEnumerable<Unite_Mesure> findFormulaireUnite(int unite)
        {
            return _db.unite_Mesures.Where(p => p.UniteMesure_Id == unite).AsEnumerable();
        }

        public async Task<bool> updateFormulaireFournisseur(int id, FournisseurProduits newFournisseur)
        {
            FournisseurProduits fournisseur = _db.fournisseurProduits.Where(e => e.FournisseurProduitConso_Id == id).FirstOrDefault();
            if (fournisseur != null)
            {
                fournisseur.FournisseurProduitConso_RaisonSocial = newFournisseur.FournisseurProduitConso_RaisonSocial;
                fournisseur.FournisseurProduitConso_ICE = newFournisseur.FournisseurProduitConso_ICE;
                fournisseur.FournisseurProduitConso_Adresse = newFournisseur.FournisseurProduitConso_Adresse;
                fournisseur.FournisseurProduitConso_NomContact = newFournisseur.FournisseurProduitConso_NomContact;
                fournisseur.FournisseurProduitConso_VilleID = newFournisseur.FournisseurProduitConso_VilleID;
                fournisseur.FournisseurProduitConso_TelephoneContact = newFournisseur.FournisseurProduitConso_TelephoneContact;
                _db.Entry(fournisseur).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public IEnumerable<Fournisseur_ProduitConso> ListeProduitFournisseur(int fournisseurId)
        {
            return _db.FournisserusProduits_Link.Where(p => p.FournisseurProduits_Id == fournisseurId)
                .Include(p => p.Fournisseur_Produits)
                .Include(p => p.Produit_PretConsomer)
                .AsEnumerable();
        }

        public async Task<bool> deleteProduitsLink(int ID, int code)
        {
            Fournisseur_ProduitConso fournisseur = _db.FournisserusProduits_Link.Where(e => e.Id == ID).FirstOrDefault();
            if (fournisseur != null)
            {
                if (code == 0)
                    fournisseur.IsActive = 0;
                else
                    fournisseur.IsActive = 1;

                _db.Entry(fournisseur).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public string GetUnitebyFrome(int formeProduitId)
        {
            var uniteLibelle = "";
            var forme = _db.forme_Produits.Where(p => p.FormeProduit_ID == formeProduitId)
                .Include(p => p.Produit_PretConsomer).ThenInclude(p=>p.Unite_Mesure)
                .Include(p=>p.ProduitPackage).ThenInclude(p => p.Unite_Mesure)
                .Include(p=>p.Produit_Vendable).ThenInclude(p => p.Unite_Mesure)
                .FirstOrDefault();
            if (forme.Produit_PretConsomer != null)
                uniteLibelle = forme.Produit_PretConsomer.Unite_Mesure.UniteMesure_Libelle;
            if (forme.ProduitPackage != null)
                uniteLibelle = forme.ProduitPackage.Unite_Mesure.UniteMesure_Libelle;
            if (forme.Produit_Vendable != null)
                uniteLibelle = forme.Produit_Vendable.Unite_Mesure.UniteMesure_Libelle;
            return uniteLibelle;
        }

        public async Task<bool> deletePositionVente(int ID, int code)
        {
            var venteDetail = _db.venteDetails.Where(p => p.Vente.Vente_PositionVenteId == ID && p.VenteDetails_FlagCloture == 0)
                .FirstOrDefault();           
            var commande = _db.paiementCommandes.Where(p => p.CommandePaiement_PositionVenteID == ID && p.CommandePaiement_FlagCloture == 0)
                .FirstOrDefault();            
            var alim = _db.allimentations.Where(p => p.AllimentationCaisse_PositionVenteID == ID && p.AllimentationCaisse_FlagCloture == 0)
                .FirstOrDefault();           
            var retrait = _db.retraitCaisses.Where(p => p.RetraitCaisse_PositionVenteID == ID && p.RetraitCaisse_FlagCloture == 0)
                .FirstOrDefault();            
            var retour = _db.retourProduits.Where(p => p.Retour_PositionVenteID == ID && p.Retour_FlagCloture == 0)
                .FirstOrDefault();
            if (venteDetail != null || commande != null || alim != null || retour != null || retrait != null)
                return false;
            PositionVente position = _db.positionVentes.Where(e => e.PositionVente_Id == ID).FirstOrDefault();
            if (position != null)
            {
                if (code == 0)
                {
                    position.PositionVente_IsActive = 0;
                }
                else
                {
                    position.PositionVente_IsActive = 1;
                }
                _db.Entry(position).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public Task<bool> deleteSalle(int ID, int code)
        {
            throw new NotImplementedException();
        }
        /*public async Task<bool> deleteSalle(int ID, int code)
{
   Salle salle = _db.salles.Where(e => e.Salle_Id == ID).FirstOrDefault();
   if (salle != null)
   {
       if (code == 0)
       {
           salle. = 0;
       }
       else
       {
           salle.PositionVente_IsActive = 1;
       }
       _db.Entry(position).State = EntityState.Modified;
       var confirm = await unitOfWork.Complete();
       if (confirm > 0)
           return true;
       else
           return false;
   }
   return false;
}*/
        public async Task<bool> updateFormePrix(int formeId, decimal prix)
        {
            Forme_Produit forme = _db.forme_Produits.Where(e => e.FormeProduit_ID == formeId).FirstOrDefault();
            if (forme != null)
            {
                forme.FormeProduit_PrixVente = prix;
                _db.Entry(forme).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<int> GetPlanificationsForStock(int stockId)
        {
            int r  = await _db.planificationJournees.CountAsync(p => p.PlanificationJournee_LieuStockageID == stockId && p.PlanificationJournee_SeenByStock == false);
            return r;
        }

        public async Task<int> GetPlanificationsForAtelier(int atelierId)
        {
            int r = await _db.planificationJournees.CountAsync(p => p.PlanificationJournee_AtelierID == atelierId && p.PlanificationJournee_SeenByAtelier == false && p.PlanificationJournee_Etat == "Livré");
            return r;
        }

        public async Task PlansSeenByAtelier(int planId)
        {
            var plans = _db.planificationJournees.Where(p => p.PlanificationJournee_ID == planId).AsEnumerable();
            foreach (var p in plans)
            {
                p.PlanificationJournee_SeenByAtelier = true;
                _db.Entry(p).State = EntityState.Modified;
            }
            await unitOfWork.Complete();
        }
        public async Task<int?> DemanderPret(Demande_Pret demande_Pret)
        {
            demande_Pret.DemandePret_Etat = "En traitement";
            await _db.demande_Prets.AddAsync(demande_Pret);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return demande_Pret.DemandePret_ID;
            else
                return null;

        }
        public IEnumerable<Demande_Pret> getListDemandesPretStock(int stockID, int aboID, string date,int? atelierID,string etat)
        {
            IEnumerable<Demande_Pret> query = _db.demande_Prets.Where(p => p.DemandePret_StockID == stockID && p.DemandePret_AbonnementID == aboID).Include(p=>p.Atelier).AsEnumerable();
            if (date != "")
            {
                DateTime? newdate = Convert.ToDateTime(date);
                query = query.Where(s => s.DemandePret_DateCreation == newdate);
            }
            if (atelierID != null)
                query = query.Where(s => s.DemandePret_AtelierID == atelierID);
            if (etat != "")
                query = query.Where(s => s.DemandePret_Etat == etat);

            return query;
        }
        public IEnumerable<Demande_Pret> getListDemandesPretAtelier(int atelierID, int aboID, string date, int? stockID, string etat)
        {            
            var query = _db.demande_Prets.Where(p => p.DemandePret_AtelierID == atelierID && p.DemandePret_AbonnementID == aboID);
            if (date != "")
            {
                DateTime? newdate = Convert.ToDateTime(date);
                query = query.Where(s => s.DemandePret_DateCreation == newdate);
            }
            if (stockID != null)
                query = query.Where(s => s.DemandePret_StockID == stockID);
            if (etat!="")
                query = query.Where(s => s.DemandePret_Etat == etat);

            return query.Include(p => p.Lieu_Stockage).AsEnumerable();
        }
        public IEnumerable<DemandePret_Details> getListDetailsDemandesPretStock(int demandePretID, int aboID, int stockID)
        {
            var query = _db.demandePret_Details.Where(p => p.DemandePretDetails_DemandeID == demandePretID && p.Demande_Pret.DemandePret_AbonnementID == aboID && p.Demande_Pret.DemandePret_StockID==stockID);
            return query.Include(p=>p.Demande_Pret).ThenInclude(p=>p.Atelier)
                .Include(p => p.Produit_PretConsomer).Include(p => p.Unite_Mesure).Include(p => p.Forme_Produit).AsEnumerable();
        }
        public IEnumerable<DemandePret_Details> getListDetailsDemandesPretAtelier(int demandePretID, int aboID, int atelierID)
        {
            var query = _db.demandePret_Details.Where(p => p.DemandePretDetails_DemandeID == demandePretID && p.Demande_Pret.DemandePret_AbonnementID == aboID && p.Demande_Pret.DemandePret_AtelierID== atelierID);
            return query.Include(p => p.Demande_Pret).ThenInclude(p => p.Lieu_Stockage)
                .Include(p => p.Produit_PretConsomer).Include(p => p.Unite_Mesure).Include(p => p.Forme_Produit).AsEnumerable();
        }
        public async Task<bool> AccepterDemandePrets(int demandeID, int adresse, List<BonViewModel> listBon)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var demande = _db.demande_Prets.Where(s => s.DemandePret_ID == demandeID).FirstOrDefault();

                    demande.DemandePret_Etat = "Livré";

                    List<MouvementProduitsConso> mouvement = new List<MouvementProduitsConso>();
                    var demandeDetails = _db.demandePret_Details
                        .Where(b => b.DemandePretDetails_DemandeID == demandeID).AsEnumerable();
                    foreach (DemandePret_Details details in demandeDetails)
                    {
                        details.DemandePretDetails_QuantiteLivre = listBon.Where(p => p.ID == details.DemandePretDetails_ID).FirstOrDefault().value;
                        _db.Entry(details).State = EntityState.Modified;
                        var forme = _db.forme_Produits.Where(p => p.FormeProduit_ID == details.DemandePretDetails_FormeID)
                           .FirstOrDefault();
                        if (forme != null)
                        {
                            var produitEnstock = _db.produitConsomableStokages
                                .Where(s => s.ProduitConsomableStokage_FormeProduitId == details.DemandePretDetails_FormeID)
                                .Where(p => p.Lieu_Stockage.LieuStockag_Id == adresse)
                                .Include(s => s.Produit_PretConsomer)
                                .FirstOrDefault();

                            var k = _db.mouvementProduits.Where(e => e.MouvementProduitsConso_ProduitConsoId == produitEnstock.ProduitConsomableStokage_Id).AsEnumerable();
                            foreach (var mouv in k)
                            {
                                mouv.MouvementProduitsConso_IsActive = 0;
                                _db.Entry(mouv).State = EntityState.Modified;
                            }
                            MouvementProduitsConso m = new MouvementProduitsConso()
                            {
                                MouvementProduitsConso_ProduitConsoId = produitEnstock.ProduitConsomableStokage_Id,
                                MouvementProduitsConso_Quantite = details.DemandePretDetails_QuantiteLivre,
                                MouvementProduitsConso_UniteMesureId = details.DemandePretDetails_UniteMesureID,
                                MouvementProduitsConso_DateMouvement = demande.DemandePret_DateCreation,
                                MouvementProduitsConso_MouvementType = 2,
                                MouvementProduitsConso_IsActive = 1,
                                MouvementProduitsConso_AbonnementID = demande.DemandePret_AbonnementID,
                            };
                            decimal qte = 0;
                            if ((details.DemandePretDetails_UniteMesureID == 1 && forme.Produit_PretConsomer.ProduitPretConsomer_UniteMesureAchatId == 2) || (details.DemandePretDetails_UniteMesureID == 4 && forme.Produit_PretConsomer.ProduitPretConsomer_UniteMesureAchatId == 3))
                                qte = details.DemandePretDetails_QuantiteLivre / 1000;
                            else if ((details.DemandePretDetails_UniteMesureID == 2 && forme.Produit_PretConsomer.ProduitPretConsomer_UniteMesureAchatId == 1) || (details.DemandePretDetails_UniteMesureID == 3 && forme.Produit_PretConsomer.ProduitPretConsomer_UniteMesureAchatId == 4))
                                qte = details.DemandePretDetails_QuantiteLivre * 1000;
                            else
                                qte = details.DemandePretDetails_QuantiteLivre;

                            var qterest = produitEnstock.ProduitConsomableStokage_QuantiteActuelle - qte;
                            if (qterest < 0)
                                return false;
                            else
                                m.MouvementProduitsConso_QuantiteActuelle = qterest;

                            produitEnstock.ProduitConsomableStokage_QuantiteActuelle = m.MouvementProduitsConso_QuantiteActuelle;
                            _db.Entry(produitEnstock).State = EntityState.Modified;
                            mouvement.Add(m);
                            foreach (var mm in mouvement)
                                await _db.mouvementProduits.AddAsync(mm);
                        }
                        else
                            return false;
                    }
                    _db.Entry(demande).State = EntityState.Modified;
                    await unitOfWork.Complete();
                    transaction.Commit();
                    return true;

                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public async Task<bool> ValiderDemande(int demandeID, int atelier)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var demande = _db.demande_Prets.Where(s => s.DemandePret_ID == demandeID).FirstOrDefault();
                    demande.DemandePret_Etat = "Recue";
                    var details = _db.demandePret_Details.Where(p => p.DemandePretDetails_DemandeID == demandeID).AsEnumerable();
                    foreach(var detail in details)
                    {
                        var atelierStock = _db.produitPret_Ateliers.Where(p => p.ProduitPretAtelier_FormeID == detail.DemandePretDetails_FormeID && p.ProduitPretAtelier_AtelierID == atelier).FirstOrDefault();
                        if (atelierStock != null)
                        { 
                            atelierStock.ProduitPretAtelier_Quantite += detail.DemandePretDetails_QuantiteLivre; 
                            _db.Entry(atelierStock).State = EntityState.Modified;
                        }
                        else
                        {
                            var atelierPret = new ProduitPret_Atelier()
                            {
                                ProduitPretAtelier_AtelierID = atelier,
                                ProduitPretAtelier_FormeID = detail.DemandePretDetails_FormeID,
                                ProduitPretAtelier_Quantite = detail.DemandePretDetails_QuantiteLivre,
                                ProduitPretAtelier__ProduitID = detail.DemandePretDetails_ProduitID,
                                ProduitPretAtelier_DateCreation = DateTime.Now,
                            };
                            await _db.produitPret_Ateliers.AddAsync(atelierPret);
                        }
                    }
                    _db.Entry(demande).State = EntityState.Modified;
                    await unitOfWork.Complete();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public IEnumerable<ProduitPret_Atelier> getListProduitPretAtelier(int atelierID, int? produitID)
        {
            var query = _db.produitPret_Ateliers.Where(p => p.ProduitPretAtelier_AtelierID == atelierID);
            if (produitID != null)
                query = query.Where(s => s.ProduitPretAtelier__ProduitID == produitID);

            return query.Include(p => p.Forme_Produit).Include(p=>p.Produit_PretConsomer).Include(p=>p.Atelier).AsEnumerable();
        }
        public decimal getQteProduitPretAtelier(int atelierID, int formeID)
        {
            var query = _db.produitPret_Ateliers.Where(p => p.ProduitPretAtelier_AtelierID == atelierID && p.ProduitPretAtelier_FormeID == formeID).FirstOrDefault();
            if(query!=null)
                return query.ProduitPretAtelier_Quantite;
            return 0;

        }
        public IEnumerable<ProduitAppro> getListProduitMaisonStock(int atelierID, int aboID)
        {
            var query = _db.produitAppros.Where(p => p.ProduitAppro_AtelierID == atelierID && p.ProduitAppro_AbonnementID == aboID);
            return query.Include(p => p.Atelier)
                .Include(p=>p.Forme_Produit)
                .Include(p=>p.Produit_Vendable).ThenInclude(p=>p.Unite_Mesure)
                .AsEnumerable();
        } 
        public IEnumerable<MatiereStock_Atelier> getListMatiereStock(int atelierId,int aboId)
        {
            var query = _db.MatiereStock_Ateliers.Where(p => p.MatiereStockAtelier_AtelierID == atelierId && p.MatiereStockAtelier_AbonnementID==aboId && p.MatiereStockAtelier_QauntiteActuelle > Convert.ToDecimal((0.001).ToString()));
            return query.Include(p => p.Matiere_Premiere).ThenInclude(p=>p.Unite_Mesure)
                .AsEnumerable();
        }
        public ViewBonSortieModel getQteEnMagasin(int atelierId, int matiereId, int uniteId)
        {
            var matiereQte = _db.MatiereStock_Ateliers.Where(p => p.MatiereStockAtelier_AtelierID == atelierId && p.MatiereStockAtelier_MatierePremiereID== matiereId).FirstOrDefault();
            decimal qteAvecPlan = 0;
            decimal qteActu = 0;
            if (matiereQte != null)
            {
                qteAvecPlan = matiereQte.MatiereStockAtelier_QuatiteAvecPlanification;
                qteActu = matiereQte.MatiereStockAtelier_QauntiteActuelle ;

            }
            var matiere = _db.matierePremieres.Where(p => p.MatierePremiere_Id == matiereId).Include(p=>p.Unite_Mesure).FirstOrDefault();
            var matiereUnite = _db.unite_Mesures.Where(p => p.UniteMesure_Id == uniteId).FirstOrDefault().UniteMesure_Libelle;
            var ViewModel = new ViewBonSortieModel()
            {
                MatierePremiere_Libelle = matiere.MatierePremiere_Libelle,
                MatierePremiere_QuantiteEnMagasin = qteActu,
                MatierePremiere_QuantiteAvecPlanification = qteAvecPlan,
                MatierePremiere_UniteMesureLibelle = matiereUnite,
                MatierePremiere_UniteMesureMag = matiere.Unite_Mesure.UniteMesure_Libelle,
            };
            return ViewModel;
        }
        public ViewBonSortieModel getQuantiteEnMagasin(int atelierId, int matiereId)
        {
            var matiereQte = _db.MatiereStock_Ateliers.Where(p => p.MatiereStockAtelier_AtelierID == atelierId && p.MatiereStockAtelier_MatierePremiereID== matiereId)
                .Include(p=>p.Matiere_Premiere).ThenInclude(p=>p.Unite_Mesure).FirstOrDefault();
            var ViewModel = new ViewBonSortieModel();
            if (matiereQte != null)
            {
                ViewModel.MatierePremiere_QuantiteEnMagasin = matiereQte.MatiereStockAtelier_QauntiteActuelle;
                ViewModel.MatierePremiere_UniteMesureLibelle = matiereQte.Matiere_Premiere.Unite_Mesure.UniteMesure_Libelle;
            }
            else
            {
                var matiereUnite = _db.matierePremieres.Where(p => p.MatierePremiere_Id == matiereId).Include(p => p.Unite_Mesure).FirstOrDefault();
                ViewModel.MatierePremiere_QuantiteEnMagasin = 0;
                ViewModel.MatierePremiere_UniteMesureLibelle = matiereUnite.Unite_Mesure.UniteMesure_Libelle;
            }
            return ViewModel;
        }
        public ViewBonSortieModel getQuantiteAtelier(int matiereStockID)
        {
            var matiereQte = _db.MatiereStock_Ateliers.Where(p => p.MatiereStockAtelier_ID == matiereStockID)
                .Include(p=>p.Matiere_Premiere).ThenInclude(p=>p.Unite_Mesure).FirstOrDefault();
            var ViewModel = new ViewBonSortieModel();
            if (matiereQte != null)
            {
                ViewModel.MatierePremiere_QuantiteEnMagasin = matiereQte.MatiereStockAtelier_QauntiteActuelle;
                ViewModel.MatierePremiere_UniteMesureLibelle = matiereQte.Matiere_Premiere.Unite_Mesure.UniteMesure_Libelle;
            }
            else
            {
                ViewModel.MatierePremiere_QuantiteEnMagasin = 0;
                ViewModel.MatierePremiere_UniteMesureLibelle = "";
            }
            return ViewModel;
        } 
        public ViewBonSortieModel getQuantiteStock(int matiereStockID)
        {
            var matiereQte = _db.matierePremiereStockages.Where(p => p.MatierePremiereStokage_Id == matiereStockID)
                .Include(p=>p.Matiere_Premiere).ThenInclude(p=>p.Unite_Mesure).FirstOrDefault();
            var ViewModel = new ViewBonSortieModel();
            if (matiereQte != null)
            {
                ViewModel.MatierePremiere_QuantiteEnMagasin = matiereQte.MatierePremiereStokage_QuantiteActuelle;
                ViewModel.MatierePremiere_UniteMesureLibelle = matiereQte.Matiere_Premiere.Unite_Mesure.UniteMesure_Libelle;
            }
            else
            {
                ViewModel.MatierePremiere_QuantiteEnMagasin = 0;
                ViewModel.MatierePremiere_UniteMesureLibelle = "";
            }
            return ViewModel;
        }
        public async Task<int?> PackageForme(Package_Forme packForme)
        {
            packForme.PackageForme_DateCreation = DateTime.Now;
            var res = _db.package_Formes.Where(p => p.PackageForme_FormeProduitID == packForme.PackageForme_FormeProduitID).AsEnumerable();
            foreach(var item in res)
            {
                item.PackageForme_IsInUse = false;
                _db.Entry(item).State = EntityState.Modified;
            }
            packForme.PackageForme_IsInUse = true;
            var forme = _db.forme_Produits.Where(p => p.FormeProduit_ID == packForme.PackageForme_FormeProduitID).FirstOrDefault();
            forme.FormeProduit_CoutDeRevient = packForme.details.Sum(p => p.PackageFormeDetails_CoutdeRevient);
            var formedetail = new FormeDetails()
            {
                FormeDetails_FormeProduitID = forme.FormeProduit_ID,
                FormeDetails_ProduitPackageID = packForme.PackageForme_ProduitPackageID,
                FormeDetails_Quantite = 0,
                FormeDetails_DateCreation = DateTime.Now,
                //FormeDetails_Libelle = forme.FormeProduit_Libelle,
                FormeDetails_AbonnementID = forme.FormeProduit_AbonnementID
            };
            _db.Entry(forme).State = EntityState.Modified;
            //await _db.formeDetails.AddAsync(formedetail);
            await _db.package_Formes.AddAsync(packForme);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
            {
                return packForme.PackageForme_ID;
            }
            else { return null; }
        }
        public IEnumerable<Package_Forme> getListeFichePackage(int? aboId, int? produitID)
        {
            var query = _db.package_Formes.Where(p => p.PackageForme_AbonnementID == (int)aboId);
            if (produitID != null)
            {
                var query2 = query.Where(p => p.PackageForme_ProduitPackageID == produitID);
                return query2.Include(p => p.Forme_Produit).Include(p => p.ProduitPackage).AsEnumerable();
            }
            else
                return query.Include(p => p.Forme_Produit).Include(p => p.ProduitPackage).AsEnumerable();
        } 
        public Package_Forme getPackageFormeDetails(int packageId, int formeId)
        {
            var query = _db.package_Formes.Where(p => p.PackageForme_ProduitPackageID == packageId && p.PackageForme_FormeProduitID == formeId && p.PackageForme_IsInUse==true);
            return query.Include(p => p.details).ThenInclude(p=>p.Sous_ProduitPackage).ThenInclude(p=>p.Forme_Produit).FirstOrDefault();
        }
        public IEnumerable<PackageForme_Details> getDetailsFichePackage(int produitID)
        {
            var query = _db.packageForme_Details.Where(p => p.PackageFormeDetails_PackageFormeID == produitID);
            return query.Include(p => p.Sous_ProduitPackage).ThenInclude(p=>p.Forme_Produit).ThenInclude(p=>p.Produit_Vendable).ThenInclude(p=>p.Unite_Mesure)
                .Include(p => p.Sous_ProduitPackage).ThenInclude(p=>p.Forme_Produit).ThenInclude(p=>p.Produit_PretConsomer).ThenInclude(p => p.Unite_Mesure)
                .Include(p => p.Package_Forme).ThenInclude(p=>p.ProduitPackage).AsEnumerable();
        }
        public Sous_ProduitPackage findFormulaireSousProduitPackage(int sousProdId)
        {
            var query = _db.sous_ProduitPackages.Where(p => p.SousProduitPackage_ID == sousProdId);
            return query.Include(p=>p.Forme_Produit).ThenInclude(p=>p.Produit_Vendable).ThenInclude(p=>p.Unite_Mesure)
                .Include(p=>p.Forme_Produit).ThenInclude(p=>p.Produit_PretConsomer).ThenInclude(p => p.Unite_Mesure).FirstOrDefault();
        }
        public async Task<int?> DemanderComposants(List<SousProduitsViewModel> sousProds, int atelierID, int lieuId,int aboId)
        {
            var details = new List<DemandePret_Details>();
            foreach(var s in sousProds)
            {
                if (s.SousProduit_ProduitType == "consommable")
                {
                    var detail = new DemandePret_Details(){
                        DemandePretDetails_FormeID = s.SousProduit_FormeProduitID,
                        DemandePretDetails_ProduitID = s.SousProduit_ProduitID,
                        DemandePretDetails_Quantite = s.SousProduit_Quantite,
                        DemandePretDetails_UniteMesureID = s.SousProduit_UniteMesureId,
                        
                    };
                    details.Add(detail);
                }
            }
            var demande = new Demande_Pret()
            {
                DemandePret_AtelierID = atelierID,
                DemandePret_Etat = "En traitement",
                DemandePret_DateCreation = DateTime.Now,
                DemandePret_StockID = lieuId,
                DemandePret_AbonnementID = aboId,
                details = details,
            };
            await _db.demande_Prets.AddAsync(demande);
            var confirm = await unitOfWork.Complete();

            if (confirm > 0)
            return demande.DemandePret_ID;

            else
                return null;
        }
        public async Task<int?> ApprovisionnementProduitPackage(Approvisionnement_ProduitPackage approvisionnement)
        {
            approvisionnement.ApprovisionnementProduitPackage__DateCreation = DateTime.Now;
            approvisionnement.ApprovisionnementProduitPackage__Etat = "En traitement";
            var produitsPackage = _db.produitPack_Ateliers.Where(p => p.ProduitPackAtelier_ID == approvisionnement.ApprovisionnementProduitPackage_ProduitpackAtelierId).FirstOrDefault();
            produitsPackage.ProduitPackAtelier_Quantite -= approvisionnement.ApprovisionnementProduitPackage__Quantite;
            await _db.approvisionnement_ProduitPackages.AddAsync(approvisionnement);
            _db.Entry(produitsPackage).State = EntityState.Modified;
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return approvisionnement.ApprovisionnementProduitPackage_Id;
            else
                return null;
        }
        public async Task<bool> ValiderApprovisionnementPackage(int ApprovisionnementId, int pdvId)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var appro = _db.approvisionnement_ProduitPackages.Where(a => a.ApprovisionnementProduitPackage_Id == ApprovisionnementId).Include(p=>p.ProduitPack_Atelier).FirstOrDefault();
                    appro.ApprovisionnementProduitPackage__Etat = "Recue";
                    appro.ApprovisionnementProduitPackage__DateReception = DateTime.Now;
                    var pdvS = _db.formeDetails.Where(p => p.FormeDetails_FormeProduitID == appro.ProduitPack_Atelier.ProduitPackAtelier_FormeID)
                        .Where(p => p.FormeDetails_PointVenteID == pdvId)
                        .FirstOrDefault();
                    if (pdvS == null)
                    {
                        FormeDetails formeDetail = new FormeDetails
                        {
                            FormeDetails_FormeProduitID = appro.ProduitPack_Atelier.ProduitPackAtelier_FormeID,
                            FormeDetails_ProduitPackageID = appro.ProduitPack_Atelier.ProduitPackAtelier_ProduitPackID,
                            FormeDetails_PointVenteID = appro.ApprovisionnementProduitPackage_PointVenteID,
                            FormeDetails_Quantite = appro.ApprovisionnementProduitPackage__Quantite,
                            FormeDetails_DateCreation = DateTime.Now,
                            FormeDetails_AbonnementID = appro.ApprovisionnementProduitPackage_AbonnementId,

                        };
                        await _db.formeDetails.AddAsync(formeDetail);

                    }
                    else if (pdvS != null)
                    {
                        pdvS.FormeDetails_Quantite += appro.ApprovisionnementProduitPackage__Quantite;
                        _db.Entry(pdvS).State = EntityState.Modified;

                    }
                    _db.Entry(appro).State = EntityState.Modified;
                    await unitOfWork.Complete();

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public IEnumerable<ProduitPack_Atelier> getProduitPackEnMagasin(int atelierId)
        {
            var query = _db.produitPack_Ateliers.Where(p => p.ProduitPackAtelier_AtelierID == atelierId);
            return query.Include(p => p.Forme_Produit)
                .Include(p => p.ProduitPackage)
                .Include(p => p.Atelier).AsEnumerable();
        }
        public IEnumerable<ProduitPack_Atelier> FindFormesProduitPackEnMagasin(int produitID,int atelierID)
        {
            var query = _db.produitPack_Ateliers.Where(p => p.ProduitPackAtelier_ProduitPackID == produitID && p.ProduitPackAtelier_AtelierID==atelierID);
            return query.Include(p => p.Forme_Produit)
                .Include(p => p.ProduitPackage)
                .Include(p => p.Atelier).AsEnumerable();
        }
        public ProduitPack_Atelier FindformulairePackageMagasin(int ID)
        {
            var query = _db.produitPack_Ateliers.Where(p => p.ProduitPackAtelier_ID == ID);
            return query.Include(p => p.Forme_Produit)
                .Include(p => p.ProduitPackage)
                .Include(p => p.Atelier).FirstOrDefault();
        }
        public IEnumerable<Approvisionnement_ProduitPackage> getListApprovisionnement(int aboId, int? atelierID, int? pdvId, string date)
        {
            var query = _db.approvisionnement_ProduitPackages.Where(p => p.ApprovisionnementProduitPackage_AbonnementId == aboId);
            if (atelierID != null)
                query = query.Where(s => s.ApprovisionnementProduitPackage_AtelierID == atelierID); 
            if (pdvId != null)
                query = query.Where(s => s.ApprovisionnementProduitPackage_PointVenteID == pdvId);
            if (date != "")
            {
                DateTime? newdate = Convert.ToDateTime(date);
                query = query.Where(s => s.ApprovisionnementProduitPackage_Date.Date == newdate);
            }
            return query.Include(p => p.Atelier)
                .Include(p => p.ProduitPack_Atelier).ThenInclude(p=>p.ProduitPackage).ThenInclude(p=>p.Unite_Mesure)
                .Include(p => p.ProduitPack_Atelier).ThenInclude(p=>p.Forme_Produit)
                .Include(p => p.Point_Vente).AsEnumerable();
        }

        public async Task<int?> DeclarationPerte(Perte_Pret perte_Pret)
        {
            perte_Pret.PertePret_DateCreation = DateTime.Now;
            var matEnStock = _db.produitPret_Ateliers.Where(p => p.ProduitPretAtelier_ID == perte_Pret.PertePret_ProduitPretAtelierID).Include(p => p.Forme_Produit).ThenInclude(p=>p.Produit_PretConsomer).FirstOrDefault();
            if (matEnStock != null)
            {
                decimal qte = 0;
                if ((perte_Pret.PertePret_UniteMesureID == 1 && matEnStock.Forme_Produit.Produit_PretConsomer.ProduitPretConsomer_UniteMesureAchatId == 2) || (perte_Pret.PertePret_UniteMesureID == 4 && matEnStock.Forme_Produit.Produit_PretConsomer.ProduitPretConsomer_UniteMesureAchatId == 3))
                    qte = perte_Pret.PertePret_Quantite / 1000;
                else if ((perte_Pret.PertePret_UniteMesureID == 2 && matEnStock.Forme_Produit.Produit_PretConsomer.ProduitPretConsomer_UniteMesureAchatId == 1) || (perte_Pret.PertePret_UniteMesureID == 3 && matEnStock.Forme_Produit.Produit_PretConsomer.ProduitPretConsomer_UniteMesureAchatId == 4))
                    qte = perte_Pret.PertePret_Quantite * 1000;
                else
                    qte = perte_Pret.PertePret_Quantite;

                matEnStock.ProduitPretAtelier_Quantite -= qte;
                _db.Entry(matEnStock).State = EntityState.Modified;
            }
            await _db.perte_Prets.AddAsync(perte_Pret);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return perte_Pret.PertePret_ID;
            else
                return null;
        }
        public IEnumerable<Perte_Pret> getListPertesPrets(int aboId, int? atelierID, string date)
        {
            var query = _db.perte_Prets.Where(p => p.PertePret_AbonnmentID == aboId);
            if (atelierID != null)
                query = query.Where(s => s.PertePret_AtelierID == atelierID);
            if (date != "")
            {
                DateTime newdate = Convert.ToDateTime(date);
                query = query.Where(s => s.PertePret_DateCreation.Date == newdate.Date);
            }
            
            return query.Include(p => p.Unite_Mesure).Include(p => p.Atelier).Include(p => p.ProduitPret_Atelier).ThenInclude(p => p.Forme_Produit).ThenInclude(p=>p.Produit_PretConsomer).AsEnumerable();
        }
        public IEnumerable<ProduitPret_Atelier> findFormesPret_Atelier( int atelierID, int prduitpretID)
        {
            var query = _db.produitPret_Ateliers.Where(p => p.ProduitPretAtelier_AtelierID == atelierID && p.Forme_Produit.FormeProduit_ProduitPretId == prduitpretID);
            return query.Include(p => p.Forme_Produit).Include(p => p.Atelier).AsEnumerable();
        }
        public ProduitPret_Atelier FindformulairePretMagasin(int ID)
        {
            var query = _db.produitPret_Ateliers.Where(p => p.ProduitPretAtelier_ID == ID);
            return query.Include(p => p.Forme_Produit).ThenInclude(p=>p.Produit_PretConsomer).ThenInclude(p=>p.Unite_Mesure)
                .Include(p => p.Atelier).FirstOrDefault();
        }

        public async Task<int?> CreateProduitBase(ProduitBase produitBase)
        {
            produitBase.ProduitBase_IsActive = 1;
            produitBase.ProduitBase_DateCreation = DateTime.Now;
            await _db.produitBases.AddAsync(produitBase);
            var confirm = await unitOfWork.Complete();
             //await AjouterUnites(produitBase.ProduitBase_ID, ListeUnite); 
            if (confirm > 0)
                return produitBase.ProduitBase_ID;
            else
                return null;
        }

        public IEnumerable<ProduitBase> getListProduitBase(int Id, int? formeID)
        {
            var query = _db.produitBases.Where(p => p.ProduitBase_AbonnementID == Id);
            if (formeID != null)
                query.Where(p => p.ProduitBase_FormeStockageID == formeID);
            return query.Include(p => p.Forme_Stockage).Include(p => p.Unite_Mesure).AsEnumerable();
        }

        public async Task<bool> updateFormulaireProduitBase(int id, ProduitBase newProduit)
        {
            var produit = _db.produitBases.Where(p => p.ProduitBase_ID == id).FirstOrDefault();
            if (produit != null)
            {
                produit.ProduitBase_Designation = newProduit.ProduitBase_Designation;
                produit.ProduitBase_FormeStockageID = newProduit.ProduitBase_FormeStockageID;
                produit.ProduitBase_UniteMesureID = newProduit.ProduitBase_UniteMesureID;
                produit.ProduitBase_IsActive = 1;
                _db.Entry(produit).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;

        }

        public async Task<bool> deleteProduitBase(int ID)
        {
            ProduitBase produit = _db.produitBases.Where(e => e.ProduitBase_ID == ID).FirstOrDefault();
            if (produit != null)
            {
                produit.ProduitBase_IsActive = 0;
                _db.Entry(produit).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }
        public async Task<int?> AjouterUnites(int idProdBase, List<int> listUnite)
        {
            try
            {
                ProduitBase produit = _db.produitBases.Where(m => m.ProduitBase_ID == idProdBase)
               .Include(m => m.unites_Utilisation)
               .FirstOrDefault();
                foreach (var id in listUnite)
                {
                    Unite_Mesure unite = _db.unite_Mesures.Where(u => u.UniteMesure_Id == id).FirstOrDefault();
                    var uniteMatiere = new UniteMesure_ProdBase
                    {
                        ProduitBase = produit,
                        Unite_Mesure = unite,
                        Abonnement_ID = produit.ProduitBase_AbonnementID,
                        isActive = 1,
                    };
                    await _db.uniteMesure_ProdBases.AddAsync(uniteMatiere);
                }
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return produit.ProduitBase_ID;
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public ProduitBase findFormulaireProduitBase(int produitId, int aboId)
        {
            return _db.produitBases.Where(p => p.ProduitBase_ID == produitId && p.ProduitBase_AbonnementID == aboId)
                .Include(p=>p.Unite_Mesure)
                .FirstOrDefault();
        }

        public IEnumerable<PlanificationJourneeBase> getListPlansBaseAtelier(int Id, int? atelierId, string etat, int? lieu, string date)
        {
            var query = _db.planificationJourneeBases.Where(p => p.PlanificationJourneeBase_AbonnementID == Id);
            if (atelierId != null)
                query = query.Where(p => p.PlanificationJourneeBase_AtelierID == atelierId);
            if (lieu != null)
                query = query.Where(p => p.PlanificationJourneeBase_LieuStockageID == lieu);
            if (etat != "")
                query = query.Where(p => p.PlanificationJourneeBase_Etat == etat);
            if (date != "")
            {
                DateTime newDate = Convert.ToDateTime(date);
                query = query.Where(p => p.PlanificationJourneeBase_DateCreation.Date == newDate.Date);
            }
            return query.Include(p => p.Lieu_Stockage)
                .Include(p=>p.BonDe_Sortie).ThenInclude(p=>p.Bon_Details)
                .Include(p => p.Atelier).OrderByDescending(p => p.PlanificationJourneeBase_ID)
                .AsEnumerable();
        }

        public async Task<int?> PlanifierBase(PlanificationJourneeBase plan)
        {
            plan.PlanificationJourneeBase_DateCreation = DateTime.Now;
            plan.PlanificationJourneeBase_SeenByStock = false;
            await _db.planificationJourneeBases.AddAsync(plan);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
            {
                return await SetQteAVP(plan.PlanificationJourneeBase_ID);
               // return plan.PlanificationJourneeBase_ID;
            }
            else { return null; }
        }
        public async Task<int?> PlanificationBaseAuto(int planID)
        {
            var planifProduction = _db.planificationJournees.Where(p => p.PlanificationJournee_ID == planID).FirstOrDefault();
            var listeProds = _db.planification_ProdBases.Where(p => p.PlanificationProdBase_PlanificationID == planID).Include(p=>p.ProduitBase).AsEnumerable();
            var planifBase = new PlanificationJourneeBase()
            {
                PlanificationJourneeBase_AtelierID = planifProduction.PlanificationJournee_AtelierID,
                PlanificationJourneeBase_AbonnementID = planifProduction.PlanificationJournee_AbonnementID,
                PlanificationJourneeBase_UtilisateurId = planifProduction.PlanificationJournee_UtilisateurId,
                PlanificationJourneeBase_Date = planifProduction.PlanificationJournee_Date,
                PlanificationJourneeBase_DateCreation = planifProduction.PlanificationJournee_DateCreation,
                PlanificationJourneeBase_LieuStockageID = planifProduction.PlanificationJournee_LieuStockageID,
                PlanificationJourneeBase_Etat = "En traitement",
            };
            var details = new List<PlanificationdeProductionBase>();
            var detailsBon = new List<BonDetails>();
            var BonSortie = new BonDeSortie()
            {
                BonDeSortie_AbonnementID = planifBase.PlanificationJourneeBase_AbonnementID,
                BonDeSortie_DateCreation = planifBase.PlanificationJourneeBase_DateCreation,
                BonDeSortie_DateProduction = planifBase.PlanificationJourneeBase_Date,
                BonDeSortie_Libelle = "Bon de sortie de : " + planifBase.PlanificationJourneeBase_DateCreation.ToString(),
                BonDeSortie_StockID =(int) planifBase.PlanificationJourneeBase_LieuStockageID,
            };
            
            var unite = _db.unite_Mesures.AsEnumerable();
            foreach (var item in listeProds)
            {
                var items = new PlanificationdeProductionBase()
                {
                    PlanificationProductionBase_AbonnementId = planifProduction.PlanificationJournee_AbonnementID,
                    PlanificationProductionBase_Date = planifProduction.PlanificationJournee_Date,
                    PlanificationProductionBase_DateCreation = planifProduction.PlanificationJournee_DateCreation,
                    PlanificationProductionBase_QuantitePrevue = item.PlanificationProdBase_Quantite,
                    PlanificationProductionBase_ProduitBase = item.PlanificationProdBase_ProdBaseID,
                    PlanificationProductionBase_QuantiteProduite = 0,
                    PlanificationProductionBase_QuantiteRestante = 0,
                    PlanificationProductionBase_DateModification = DateTime.Now,
                };
                details.Add(items);
               var fiche = _db.ficheTechniqueProduitBases.Where(f => f.FicheTechniqueProduitBase_ProduitBaseID == item.ProduitBase.ProduitBase_ID)
                    .Where(f => f.FicheTechniqueProduitBase_InUse == true)
                    .Include(p => p.ProduitBase_FicheTechnique).ThenInclude(p => p.Matiere_Premiere)
                    .Include(p => p.ProduitBase_FicheTechnique).ThenInclude(u => u.Unite_Mesure)
                    .FirstOrDefault();
                foreach(var f in fiche.ProduitBase_FicheTechnique)
                {
                    var qteMag = getQteEnMagasin((int)planifProduction.PlanificationJournee_AtelierID, f.ProduitBaseFicheTechnique_MatierePremiereID, f.ProduitBaseFicheTechnique_UniteMesureID);
                    var quantite = Convert.ToDecimal(((item.PlanificationProdBase_Quantite * f.ProduitBaseFicheTechnique_QuantiteMatiere) / fiche.FicheTechniqueProduitBase_QuantiteProd).ToString("0.00"));
                    var check = detailsBon.Where(p => p.BonDeSortie_MatiereId == f.ProduitBaseFicheTechnique_MatierePremiereID).FirstOrDefault();
                    if (check != null) 
                    {
                        if (qteMag.MatierePremiere_UniteMesureMag == "Kilogramme(s)" && qteMag.MatierePremiere_UniteMesureLibelle == "Gramme(s)")
                        {
                            check.BonDeSortie_Quantite += (quantite) / 1000;
                            check.BonDeSortie_UniteLibelle = "Kilogramme(s)";
                            check.BonDeSortie_UniteMesureId = 2;

                        }
                        else if (qteMag.MatierePremiere_UniteMesureMag == "Gramme(s)" && qteMag.MatierePremiere_UniteMesureLibelle == "Kilogramme(s)")
                        {
                            check.BonDeSortie_Quantite += (quantite) * 1000;
                            check.BonDeSortie_UniteLibelle = "Gramme(s)";
                            check.BonDeSortie_UniteMesureId = 2;
                        }
                        else
                        {
                            check.BonDeSortie_Quantite += quantite;
                            check.BonDeSortie_UniteLibelle = qteMag.MatierePremiere_UniteMesureLibelle;
                            check.BonDeSortie_UniteMesureId = f.ProduitBaseFicheTechnique_UniteMesureID;
                        }
                        var qte = Convert.ToDecimal((check.BonDeSortie_QuantiteEnStockAvecPlan - check.BonDeSortie_Quantite).ToString("0.00"));
                        if (qte < 0)
                        {
                            check.BonDeSortie_QuantiteDemandee += check.BonDeSortie_Quantite;
                            check.BonDeSortie_QuantiteEnStockAvecPlan += qte;

                        }
                        else
                        {
                            check.BonDeSortie_QuantiteDemandee += 0;
                            check.BonDeSortie_QuantiteEnStockAvecPlan += qte;

                        }
                    }
                    else
                    {
                        var bonsModel = new BonDetailsModel()
                        {
                            // BonDeSortie_Quantite = (item.PlanificationProdBase_Quantite * f.ProduitBaseFicheTechnique_QuantiteMatiere) / fiche.FicheTechniqueProduitBase_QuantiteProd,
                            // BonDeSortie_UniteMesureId = f.ProduitBaseFicheTechnique_UniteMesureID,
                            BonDeSortie_MatiereId = f.ProduitBaseFicheTechnique_MatierePremiereID,
                            BonDeSortie_QuantiteEnStock = qteMag.MatierePremiere_QuantiteEnMagasin,
                            BonDeSortie_QuantiteEnStockAvecPlan = qteMag.MatierePremiere_QuantiteAvecPlanification,
                            BonDeSortie_QuantiteDemandee = 0,
                            BonDeSortie_MatiereLibelle = qteMag.MatierePremiere_Libelle,
                            // BonDeSortie_UniteLibelle = qteMag.MatierePremiere_UniteMesureLibelle ,
                            BonDeSorite_UniteStock = qteMag.MatierePremiere_UniteMesureMag,
                        };
                        if (qteMag.MatierePremiere_UniteMesureMag == "Kilogramme(s)" && qteMag.MatierePremiere_UniteMesureLibelle == "Gramme(s)")
                        {
                            bonsModel.BonDeSortie_Quantite = (quantite) / 1000;
                            bonsModel.BonDeSortie_UniteLibelle = "Kilogramme(s)";
                            bonsModel.BonDeSortie_UniteMesureId = 2;

                        }
                        else if (qteMag.MatierePremiere_UniteMesureMag == "Gramme(s)" && qteMag.MatierePremiere_UniteMesureLibelle == "Kilogramme(s)")
                        {
                            bonsModel.BonDeSortie_Quantite = (quantite) * 1000;
                            bonsModel.BonDeSortie_UniteLibelle = "Gramme(s)";
                            bonsModel.BonDeSortie_UniteMesureId = 2;
                        }
                        else
                        {
                            bonsModel.BonDeSortie_Quantite = quantite;
                            bonsModel.BonDeSortie_UniteLibelle = qteMag.MatierePremiere_UniteMesureLibelle;
                            bonsModel.BonDeSortie_UniteMesureId = f.ProduitBaseFicheTechnique_UniteMesureID;
                        }
                        var qte = Convert.ToDecimal((bonsModel.BonDeSortie_QuantiteEnStockAvecPlan - bonsModel.BonDeSortie_Quantite).ToString("0.00"));
                        if (qte < 0)
                        {
                            bonsModel.BonDeSortie_QuantiteDemandee = bonsModel.BonDeSortie_Quantite;
                            bonsModel.BonDeSortie_QuantiteEnStockAvecPlan = qte;

                        }
                        else
                        {
                            bonsModel.BonDeSortie_QuantiteDemandee = 0;
                            bonsModel.BonDeSortie_QuantiteEnStockAvecPlan = qte;

                        }
                        var bon  = mapper.Map<BonDetailsModel,BonDetails>(bonsModel);

                        detailsBon.Add(bon);
                    }
                   
                }
                
                   
             }

          //  var bon = (ICollection<BonDetails>)(detailsBon);

            BonSortie.Bon_Details = detailsBon;
            planifBase.Planification_ProductionBase = (ICollection<PlanificationdeProductionBase>)details.AsEnumerable();
            planifBase.BonDe_Sortie =  BonSortie;
            var confirm = await PlanifierBase(planifBase);
            if (confirm > 0)
                return confirm;
            else
                return null;
            
        }

        public async Task<MatStockFlagModel> AccepterPlanificationBase(int planificationBaseId, int adresse, List<BonViewModel> ListBons, string validePar)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                var matStock = new MatStockFlagModel();

                try
                {
                    var plan = _db.planificationJourneeBases.Where(s => s.PlanificationJourneeBase_ID == planificationBaseId).FirstOrDefault();

                    plan.PlanificationJourneeBase_Etat = "Livré";
                    plan.PlanificationJourneeBase_ValidePar = validePar;
                    List<MouvementStock> mouvement = new List<MouvementStock>();
                    var bonDetails = _db.bonDetails
                        .Where(b => b.BonDeSortie_BonDeSortieID == plan.PlanificationJourneeBase_BonDeSortieID)
                        .Include(p=>p.Unite_Mesure).AsEnumerable();
                    foreach (BonDetails details in bonDetails)
                    {
                        details.BonDeSortie_QuantiteLivree = ListBons.Where(p => p.ID == details.BonDetails_ID).FirstOrDefault().value;
                        _db.Entry(details).State = EntityState.Modified;
                        var matiere = _db.matierePremieres.Where(p => p.MatierePremiere_Id == details.BonDeSortie_MatiereId).Include(p => p.Unite_Mesure)
                           .Where(p => p.MatierePremiere_IsActive == 1)
                           .FirstOrDefault();
                        if (matiere != null)
                        {
                            if (details.BonDeSortie_QuantiteLivree != 0)
                            {
                                var matierestock = _db.matierePremiereStockages
                                .Where(s => s.MatierePremiereStokage_MatierePremiereId == details.BonDeSortie_MatiereId)
                                .Where(p => p.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == adresse)
                                .Include(p=>p.Matiere_Premiere)
                                .Include(s => s.Section_Stockage).ThenInclude(s => s.Zone_Stockage).ThenInclude(s => s.Lieu_Stockage)
                                .FirstOrDefault();

                                var k = _db.mouvements.Where(e => e.MouvementStock_MatierePremiereStokageId == matierestock.MatierePremiereStokage_Id).AsEnumerable();
                                foreach (var mouv in k)
                                {
                                    mouv.MouvementStock_IsActive = 0;
                                    _db.Entry(mouv).State = EntityState.Modified;
                                }
                                MouvementStock m = new MouvementStock()
                                {
                                    MouvementStock_MatierePremiereStokageId = matierestock.MatierePremiereStokage_Id,
                                    MouvementStock_Quantite = (decimal)details.BonDeSortie_QuantiteLivree,
                                    MouvementStock_UniteMesureId = details.BonDeSortie_UniteMesureId,
                                    MouvementStock_Date = plan.PlanificationJourneeBase_DateCreation,
                                    MouvementStock_DateReception = DateTime.Now,
                                    MouvementStock_DateSaisie = DateTime.Now,
                                    MouvementStock_MouvementTypeId = 2,
                                    MouvementStock_IsActive = 1,
                                    MouvementStock_AbonnementId = plan.PlanificationJourneeBase_AbonnementID,
                                    MouvementStock_DateCreation = DateTime.Now,
                                    MouvementStock_DestinationStockId = null,
                                };
                                decimal qte = 0;
                                if ((details.BonDeSortie_UniteMesureId == 1 && matiere.MatierePremiere_AchatUniteMesureId == 2) || (details.BonDeSortie_UniteMesureId == 4 && matiere.MatierePremiere_AchatUniteMesureId == 3))
                                    qte = (decimal)details.BonDeSortie_QuantiteLivree / 1000;
                                else if ((details.BonDeSortie_UniteMesureId == 2 && matiere.MatierePremiere_AchatUniteMesureId == 1) || (details.BonDeSortie_UniteMesureId == 3 && matiere.MatierePremiere_AchatUniteMesureId == 4))
                                    qte = (decimal)details.BonDeSortie_QuantiteLivree * 1000;
                                else
                                    qte = (decimal)details.BonDeSortie_QuantiteLivree;

                                var qterest = matierestock.MatierePremiereStokage_QuantiteActuelle - qte;
                                if (qterest < 0)
                                {
                                    var matieres = new matStockViewModel()
                                    {
                                        matiereLibelle = matierestock.Matiere_Premiere.MatierePremiere_Libelle,
                                        qteLivrer = details.BonDeSortie_QuantiteLivree,
                                        qteEnStock = matierestock.MatierePremiereStokage_QuantiteActuelle,
                                        uniteStock = matiere.Unite_Mesure.UniteMesure_Libelle,
                                        uniteLivrer = details.Unite_Mesure.UniteMesure_Libelle,
                                    };
                                    matStock.Matieres.Add(matieres);
                                }
                                else
                                {
                                    m.MouvementStock_MatiereQuantiteActuelle = qterest;
                                    matStock.flag = true;
                                    matierestock.MatierePremiereStokage_QuantiteActuelle = m.MouvementStock_MatiereQuantiteActuelle;
                                    _db.Entry(matierestock).State = EntityState.Modified;
                                    mouvement.Add(m);
                                    foreach (var mm in mouvement)
                                        await _db.mouvements.AddAsync(mm);
                                }
                            }
                        }
                        else
                            matStock.flag = false;
                    }
                    if (matStock.Matieres.Count() > 0 || matStock.flag == false)
                        matStock.flag = false;

                    else
                    {
                        matStock.flag = true;
                        _db.Entry(plan).State = EntityState.Modified;
                        await unitOfWork.Complete();
                        transaction.Commit();
                    }
                    return matStock;


                }
                catch
                {
                    transaction.Rollback();
                    matStock.flag = false;
                    return matStock;

                }
            }
        }

        public async Task<MatStockFlagModel> ValiderPlanificationBase(int planificationbaseId)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                var matStockFlag = new MatStockFlagModel();
                try
                {
                    var plan = _db.planificationJourneeBases.Where(s => s.PlanificationJourneeBase_ID == planificationbaseId).Include(p => p.BonDe_Sortie).ThenInclude(p => p.Bon_Details).FirstOrDefault();
                    plan.PlanificationJourneeBase_Etat = "Recue";
                    foreach (var d in plan.BonDe_Sortie.Bon_Details)
                    {
                        var matEnStock = _db.MatiereStock_Ateliers.Where(p => p.MatiereStockAtelier_AtelierID == plan.PlanificationJourneeBase_AtelierID && p.MatiereStockAtelier_MatierePremiereID == d.BonDeSortie_MatiereId).Include(p => p.Matiere_Premiere).FirstOrDefault();
                        if (matEnStock != null)
                        {
                            decimal qte = 0;
                            if ((d.BonDeSortie_UniteMesureId == 1 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 2) || (d.BonDeSortie_UniteMesureId == 4 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 3))
                                qte = (decimal)d.BonDeSortie_QuantiteLivree / 1000;
                            else if ((d.BonDeSortie_UniteMesureId == 2 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 1) || (d.BonDeSortie_UniteMesureId == 3 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 4))
                                qte = (decimal)d.BonDeSortie_QuantiteLivree * 1000;
                            else
                                qte = (decimal)d.BonDeSortie_QuantiteLivree;

                            matEnStock.MatiereStockAtelier_QauntiteActuelle += qte;
                            matEnStock.MatiereStockAtelier_QuatiteAvecPlanification += qte; 
                            _db.Entry(matEnStock).State = EntityState.Modified;

                        }
                        else
                        {
                            var matStock = new MatiereStock_Atelier()
                            {
                                MatiereStockAtelier_AtelierID = (int)plan.PlanificationJourneeBase_AtelierID,
                                MatiereStockAtelier_MatierePremiereID = d.BonDeSortie_MatiereId,
                                MatiereStockAtelier_DateCreation = DateTime.Now,
                                MatiereStockAtelier_AbonnementID = plan.PlanificationJourneeBase_AbonnementID,
                            };
                            var mat = _db.matierePremieres.Where(p => p.MatierePremiere_Id == matStock.MatiereStockAtelier_MatierePremiereID).FirstOrDefault();
                            decimal qte = 0;
                            if ((d.BonDeSortie_UniteMesureId == 1 && mat.MatierePremiere_AchatUniteMesureId == 2) || (d.BonDeSortie_UniteMesureId == 4 && mat.MatierePremiere_AchatUniteMesureId == 3))
                                qte = (decimal)d.BonDeSortie_QuantiteLivree / 1000;
                            else if ((d.BonDeSortie_UniteMesureId == 2 && mat.MatierePremiere_AchatUniteMesureId == 1) || (d.BonDeSortie_UniteMesureId == 3 && mat.MatierePremiere_AchatUniteMesureId == 4))
                                qte = (decimal)d.BonDeSortie_QuantiteLivree * 1000;
                            else
                                qte = (decimal)d.BonDeSortie_QuantiteLivree;

                            matStock.MatiereStockAtelier_QauntiteActuelle = qte;
                            matStock.MatiereStockAtelier_QuatiteAvecPlanification = 0;
                            await _db.MatiereStock_Ateliers.AddAsync(matStock);
                        }
                    }
                    _db.Entry(plan).State = EntityState.Modified;
                    await unitOfWork.Complete();
                    transaction.Commit();
                    matStockFlag.flag = true;
                    return matStockFlag;
                }
                catch
                {
                    transaction.Rollback();
                    matStockFlag.flag = false;
                    return matStockFlag;
                }
            }
        }
        public IEnumerable<PlanificationdeProductionBase> getDetailPlanBase(int aboId, int Id)
        {
            var plan = _db.planificationdeProductionBases
                .Where(p => p.PlanificationProductionBase_PlanificationJourneeID == Id)
                .Where(p => p.PlanificationProductionBase_AbonnementId == aboId)
                .Include(p => p.ProduitBase).ThenInclude(p => p.Unite_Mesure)
                .AsEnumerable();
            return plan;
        }
        public FicheTechniqueProduitBase GetFicheTechBase(int Id)
        {
            var fiche = _db.ficheTechniqueProduitBases
                .Where(f => f.FicheTechniqueProduitBase_ProduitBaseID == Id)
                .Where(f => f.FicheTechniqueProduitBase_InUse == true)
                .Include(p => p.ProduitBase)
                .Include(p => p.ProduitBase_FicheTechnique).ThenInclude(p => p.Matiere_Premiere)
                .Include(p => p.ProduitBase_FicheTechnique).ThenInclude(u => u.Unite_Mesure)
                .FirstOrDefault();
            return fiche;
        }

        public IEnumerable<PlanificationdeProductionBase> getDetailPlanProduitListBase(int aboId, int Id)
        {
            var plan = _db.planificationdeProductionBases
                .Where(p => p.PlanificationProductionBase_PlanificationJourneeID == Id)
                .Where(p => p.PlanificationProductionBase_AbonnementId == aboId && p.PlanificationProductionBase_QuantiteProduite == 0)
                .Include(p => p.ProduitBase).ThenInclude(p => p.Unite_Mesure)
                .AsEnumerable();
            return plan;
        }
        public IEnumerable<PlanificationdeProductionBase> getDetailByPlanBase(int aboId, int Id, int planId)
        {
            var plan = _db.planificationdeProductionBases
                .Where(p => p.PlanificationProductionBase_PlanificationJourneeID == Id)
                .Where(p => p.PlanificationProductionBase_AbonnementId == aboId && p.PlanificationProductionBase_Id == planId)
                .Include(p => p.ProduitBase).ThenInclude(p => p.Unite_Mesure)
                .AsEnumerable();
            return plan;
        }
        public async Task<bool?> updateFormulaireQteProduitebase(int id, List<PlanificationdeProductionBaseModel> plans)
        {
            foreach (var p in plans)
            {
                var plan = _db.planificationdeProductionBases
                    .Where(e => e.PlanificationProductionBase_PlanificationJourneeID == id)
                    .Where(e => e.PlanificationProductionBase_Id == p.PlanificationProductionBase_Id)
                    .Include(e => e.Planification_JourneeBase).ThenInclude(e => e.Atelier)
                    .FirstOrDefault();
                plan.PlanificationProductionBase_QuantiteProduite = p.PlanificationProductionBase_QuantiteProduite;
                plan.PlanificationProductionBase_QuantiteRestante = plan.PlanificationProductionBase_QuantiteProduite;
                plan.PlanificationProductionBase_DateModification = DateTime.Now;
                var ficheTech = _db.ficheTechniqueProduitBases
                            .Where(f => f.FicheTechniqueProduitBase_ProduitBaseID == plan.PlanificationProductionBase_ProduitBase).Where(f => f.FicheTechniqueProduitBase_InUse == true)
                            .Include(f => f.ProduitBase_FicheTechnique)
                            .FirstOrDefault();
                var ficheformeQte = ficheTech.FicheTechniqueProduitBase_QuantiteProd;

                foreach (var fich in ficheTech.ProduitBase_FicheTechnique)
                {
                    var stock = _db.MatiereStock_Ateliers.Where(x => x.MatiereStockAtelier_MatierePremiereID == fich.ProduitBaseFicheTechnique_MatierePremiereID && x.MatiereStockAtelier_AtelierID == plan.Planification_JourneeBase.PlanificationJourneeBase_AtelierID).Include(x => x.Matiere_Premiere).FirstOrDefault();
                    if (stock != null)
                    {
                        if (Convert.ToDecimal((stock.MatiereStockAtelier_QauntiteActuelle).ToString("0.000")) > 0)
                        {
                            decimal qte = 0;
                            if ((fich.ProduitBaseFicheTechnique_UniteMesureID == 1 && stock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 2) || (fich.ProduitBaseFicheTechnique_UniteMesureID == 4 && stock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 3))
                                qte = ((plan.PlanificationProductionBase_QuantitePrevue * fich.ProduitBaseFicheTechnique_QuantiteMatiere) / ficheformeQte) / 1000;
                            else if ((fich.ProduitBaseFicheTechnique_UniteMesureID == 2 && stock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 1) || (fich.ProduitBaseFicheTechnique_UniteMesureID == 3 && stock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 4))
                                qte = ((plan.PlanificationProductionBase_QuantitePrevue * fich.ProduitBaseFicheTechnique_QuantiteMatiere) / ficheformeQte) * 1000;
                            else
                                qte = (plan.PlanificationProductionBase_QuantitePrevue * fich.ProduitBaseFicheTechnique_QuantiteMatiere) / ficheformeQte;

                            var qterest = Decimal.Subtract(Convert.ToDecimal(stock.MatiereStockAtelier_QauntiteActuelle.ToString("0.00")), qte);
                            decimal test = -0.01M;

                            if (Convert.ToDecimal(qterest.ToString("0.00")) < test)
                                return null;
                            else if(Convert.ToDecimal(qterest.ToString("0.00")) >= test && Convert.ToDecimal(qterest.ToString("0.00")) <= 0)
                            {
                                stock.MatiereStockAtelier_QauntiteActuelle = 0;
                            //    stock.MatiereStockAtelier_QuatiteAvecPlanification += qte;
                                _db.Entry(stock).State = EntityState.Modified;
                            }
                            else
                            {
                                stock.MatiereStockAtelier_QauntiteActuelle = qterest;
                            //    stock.MatiereStockAtelier_QuatiteAvecPlanification += qte;
                                _db.Entry(stock).State = EntityState.Modified;
                            }
                        }
                        else
                            return null;
                    }
                    else
                        return null;
                }
                var prodBase_Atelier = _db.prodBase_Ateliers.Where(e => e.ProdBase_Atelier_ProduitID == plan.PlanificationProductionBase_ProduitBase && e.ProdBase_Atelier_AtelierID == plan.Planification_JourneeBase.PlanificationJourneeBase_AtelierID).FirstOrDefault();
                if (prodBase_Atelier != null)
                {
                    prodBase_Atelier.ProdBase_Atelier_QuantiteProduite += p.PlanificationProductionBase_QuantiteProduite;
                    prodBase_Atelier.ProdBase_Atelier_DateModification = DateTime.Now;
                    _db.Entry(prodBase_Atelier).State = EntityState.Modified;

                }
                else
                {
                    ProdBase_Atelier prodBase = new ProdBase_Atelier
                    {
                        ProdBase_Atelier_ProduitID = plan.PlanificationProductionBase_ProduitBase,
                        ProdBase_Atelier_QuantiteProduite = p.PlanificationProductionBase_QuantiteProduite,
                        ProdBase_Atelier_DateCreation = DateTime.Now,
                        ProdBase_Atelier_DateModification = DateTime.Now,
                        ProdBase_Atelier_AtelierID = (int)plan.Planification_JourneeBase.PlanificationJourneeBase_AtelierID,
                        ProdBase_Atelier_AbonnementID = plan.PlanificationProductionBase_AbonnementId
                    };
                    await _db.AddAsync(prodBase);
                }
                _db.Entry(plan).State = EntityState.Modified;

            }
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return true;
            else
                return false;


        }
        public IEnumerable<ProdBase_Atelier> getListProduitBaseStock(int atelierID, int aboID)
        {
            return _db.prodBase_Ateliers.Where(p => p.ProdBase_Atelier_AtelierID == atelierID && p.ProdBase_Atelier_AbonnementID == aboID).Include(p => p.Atelier)
                .Include(p => p.ProduitBase).ThenInclude(p => p.Unite_Mesure)
                .AsEnumerable();
            
        }
        public IEnumerable<FicheTech_ProduitBase> GetFicheTech_ProduitBases(int Id, int aboID)
        {
            var query = _db.ficheTech_ProduitBases.Where(p => p.FicheTech_ID == Id && p.Abonnement_ID == aboID);
            return query.Include(p => p.FicheTechnique_Bridge)
                .Include(p => p.ProduitBase).Include(p => p.Unite_Mesure)
                .AsEnumerable();
        }
        public async Task<bool> updateFicheForme(int formeId, int produitID, decimal qteForme)
        {
            FicheTechniqueBridge fiche = _db.ficheTechniqueBridges.Where(e => e.FicheTechniqueBridge_ProduitVendableID == produitID && e.FicheTechniqueBridge_InUse == true).FirstOrDefault();
            ProduitVendable produit = _db.produitVendables.Where(p => p.ProduitVendable_Id == produitID).Include(p=>p.Unite_Mesure).Include(p=>p.formes).FirstOrDefault();
            FicheForme ficheForme = new FicheForme()
            {
                FicheForme_FormeProduit = formeId,
                FicheForme_Quantite = qteForme,
                FicheForme_CoutDeRevient = fiche.FicheTechniqueBridge_CoutDeRevient / qteForme,
                FicheForme_FicheBridge = fiche.FicheTechniqueBridge_ID,
                FicheForme_DateCreation = DateTime.Now,
                FicheForme_uniteMesure = (int)produit.ProduitVendable_UniteMesureId,
                FicheForme_IsActive = 1,
                FicheTechnique_Bridge = fiche,
                Unite_Mesure = produit.Unite_Mesure,
                Forme_Produit = produit.formes.Where(p => p.FormeProduit_ID == formeId).FirstOrDefault(),
            };
            if (fiche != null)
            {
                fiche.Fiche_Forme.Add(ficheForme);
                await _db.ficheFormes.AddAsync(ficheForme);
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                {
                    await updateCoutDeRevient(produitID, ficheForme.FicheForme_CoutDeRevient, fiche.FicheTechniqueBridge_ID);
                    return true;
                }
                else
                    return false;
            }
            return false;
        }

        public IEnumerable<Taux_TVA> getListTVA(int aboID)
        {
            return _db.taux_TVAs.Where(p => p.TauxTVA_AbonnementId == aboID);
        }

        public async Task<int?> CreateTVA(Taux_TVA tauxTVA)
        {
            tauxTVA.TauxTVA_DateCreation = DateTime.Now;
            await _db.taux_TVAs.AddAsync(tauxTVA);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return tauxTVA.TauxTVA_Id;
            else
                return null;
        }

        public async Task<bool> AnnulerPlanification(int planID)
        {
            var plan = _db.planificationJournees.Where(p => p.PlanificationJournee_ID == planID).FirstOrDefault();
            if (plan != null)
            {
                if (plan.PlanificationJournee_Etat != "Recue")
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
                            matEnStock.MatiereStockAtelier_QuatiteAvecPlanification += qte;

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
                            matStock.MatiereStockAtelier_QuatiteAvecPlanification += qte;
                            await _db.MatiereStock_Ateliers.AddAsync(matStock);
                        }
                    }
                }
                plan.PlanificationJournee_Etat = "Annulée";
                _db.Entry(plan).State = EntityState.Modified;
            }
            else
            {
                var planBase = _db.planificationJourneeBases.Where(s => s.PlanificationJourneeBase_ID == planID).Include(p => p.BonDe_Sortie).ThenInclude(p => p.Bon_Details).ThenInclude(p => p.Unite_Mesure).FirstOrDefault();
                if (planBase.PlanificationJourneeBase_Etat != "Recue")
                {
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

                        matEnStock.MatiereStockAtelier_QuatiteAvecPlanification += (qte);
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

                        matStock.MatiereStockAtelier_QuatiteAvecPlanification += qte;
                        await _db.MatiereStock_Ateliers.AddAsync(matStock);
                    }
                }
            }
                planBase.PlanificationJourneeBase_Etat = "Annulée";
                _db.Entry(planBase).State = EntityState.Modified;
            }

            
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return true;
            else
                return false;
        } 
        public async Task<bool> RetourStockPlan(int planID)
        {
            var plan = _db.planificationJournees.Where(p => p.PlanificationJournee_ID == planID)
                .Include(p=>p.BonDe_Sortie).ThenInclude(p=>p.Bon_Details)
                .Include(p=>p.Planification_Production).FirstOrDefault();
            if (plan != null)
            {
                foreach (var d in plan.BonDe_Sortie.Bon_Details)
                {
                    var matEnStock = _db.MatiereStock_Ateliers.Where(p => p.MatiereStockAtelier_AtelierID == plan.PlanificationJournee_AtelierID && p.MatiereStockAtelier_MatierePremiereID == d.BonDeSortie_MatiereId).Include(p => p.Matiere_Premiere).FirstOrDefault();
                    if (matEnStock != null)
                    {
                        decimal qte = 0;
                        if ((d.BonDeSortie_UniteMesureId == 1 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 2) || (d.BonDeSortie_UniteMesureId == 4 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 3))
                            qte = (decimal)d.BonDeSortie_QuantiteLivree / 1000;
                        else if ((d.BonDeSortie_UniteMesureId == 2 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 1) || (d.BonDeSortie_UniteMesureId == 3 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 4))
                            qte = (decimal)d.BonDeSortie_QuantiteLivree * 1000;
                        else
                            qte = (decimal)d.BonDeSortie_QuantiteLivree;
                        matEnStock.MatiereStockAtelier_QauntiteActuelle -= qte;
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
                            qte = (decimal)d.BonDeSortie_QuantiteLivree / 1000;
                        else if ((d.BonDeSortie_UniteMesureId == 2 && mat.MatierePremiere_AchatUniteMesureId == 1) || (d.BonDeSortie_UniteMesureId == 3 && mat.MatierePremiere_AchatUniteMesureId == 4))
                            qte = (decimal)d.BonDeSortie_QuantiteLivree * 1000;
                        else
                            qte = (decimal)d.BonDeSortie_QuantiteLivree;

                        matStock.MatiereStockAtelier_QauntiteActuelle -= qte;
                        await _db.MatiereStock_Ateliers.AddAsync(matStock);
                    }
                }

            }
            else
            {
                var planBase = _db.planificationJourneeBases.Where(s => s.PlanificationJourneeBase_ID == planID).Include(p => p.BonDe_Sortie).ThenInclude(p => p.Bon_Details).ThenInclude(p => p.Unite_Mesure).FirstOrDefault();
                foreach (var d in planBase.BonDe_Sortie.Bon_Details)
                {
                    var matEnStock = _db.MatiereStock_Ateliers.Where(p => p.MatiereStockAtelier_AtelierID == planBase.PlanificationJourneeBase_AtelierID && p.MatiereStockAtelier_MatierePremiereID == d.BonDeSortie_MatiereId).Include(p => p.Matiere_Premiere).FirstOrDefault();
                    if (matEnStock != null)
                    {
                        decimal qte = 0;
                        if ((d.BonDeSortie_UniteMesureId == 1 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 2) || (d.BonDeSortie_UniteMesureId == 4 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 3))
                            qte = (decimal)d.BonDeSortie_QuantiteLivree / 1000;
                        else if ((d.BonDeSortie_UniteMesureId == 2 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 1) || (d.BonDeSortie_UniteMesureId == 3 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 4))
                            qte = (decimal)d.BonDeSortie_QuantiteLivree * 1000;
                        else
                            qte = (decimal)d.BonDeSortie_QuantiteLivree;                        
                        matEnStock.MatiereStockAtelier_QauntiteActuelle += (qte);

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
                            qte = (decimal)d.BonDeSortie_QuantiteLivree / 1000;
                        else if ((d.BonDeSortie_UniteMesureId == 2 && mat.MatierePremiere_AchatUniteMesureId == 1) || (d.BonDeSortie_UniteMesureId == 3 && mat.MatierePremiere_AchatUniteMesureId == 4))
                            qte = (decimal)d.BonDeSortie_QuantiteLivree * 1000;
                        else
                            qte = (decimal)d.BonDeSortie_QuantiteLivree;

                        matStock.MatiereStockAtelier_QauntiteActuelle -= qte;
                        await _db.MatiereStock_Ateliers.AddAsync(matStock);
                    }
                }
            }
            
            plan.PlanificationJournee_Etat = "Matières retournées";
            _db.Entry(plan).State = EntityState.Modified;
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return true;
            else
                return false;
        }
    }
}
