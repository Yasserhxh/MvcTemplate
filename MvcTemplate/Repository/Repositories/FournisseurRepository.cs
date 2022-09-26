using Domain.Entities;
using Domain.Helpers;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.IRepositories;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using Dapper;

namespace Repository.Repositories
{
    public class FournisseurRepository : IFournisseurRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork unitOfWork;

        public FournisseurRepository(ApplicationDbContext db, IUnitOfWork unitOfWork)
        {
            _db = db;
            this.unitOfWork = unitOfWork;


        }

        public async Task<bool?> AjouterMatiere(int idFournisseur, List<int> listMatiere)
        {
            var fournisseur = _db.fournisseurs.Where(f => f.Founisseur_Id == idFournisseur).FirstOrDefault();
            int i = 0;
            foreach (var matiereId in listMatiere)
            {
                var link = _db.fournisseurMatieres.Where(p => p.MatierePremiere_Id == matiereId && p.Founisseur_Id == idFournisseur).FirstOrDefault();
                if (link != null)
                    i++;
                else
                {
                    var matiere = _db.matierePremieres.Where(m => m.MatierePremiere_Id == matiereId).FirstOrDefault();
                    var fournisseurMatiere = new FournisseurMatiere
                    {

                        Matiere_Premiere = matiere,
                        Founisseur = fournisseur,
                        Abonnement_ID = fournisseur.Founisseur_AbonnementId,
                        IsActive = 1,
                        //Date = DateTime.Now,
                    };
                    await _db.fournisseurMatieres.AddAsync(fournisseurMatiere);
                }
            }
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return true;
            else
                return null;
        }

        public async Task<int?> CreateBonDeCommande(BonDeCommande bonDeCommande)
        {
            bonDeCommande.BonDeCommande_DateCreation = DateTime.Now;
            var count = _db.bonDeCommandes.Where(p => p.BonDeCommande_AbonnementID == bonDeCommande.BonDeCommande_AbonnementID && p.BonDeCommande_DateCreation.Year == DateTime.Now.Year).Count() +1;
            bonDeCommande.BonDeCommande_Numero = "BC-" + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString()+ "-" + count;
            await _db.bonDeCommandes.AddAsync(bonDeCommande);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return bonDeCommande.BonDeCommande_ID;
            else
                return null;
        }

        public async Task<List<Article_BL>> CreateBonDeLivraison(BonDeLivraison bonDeLivraison)
        {
            var bc = _db.bonDeCommandes.Where(p => p.BonDeCommande_ID == bonDeLivraison.BonDeLivraison_BCID).FirstOrDefault();
            var i = 0;
            foreach(var item in bonDeLivraison.listeArticles)
            {
                item.ArticleBL_DateReception = bonDeLivraison.BonDeLivraison_DateLivraison;
                int _min = 1000;
                int _max = 9999;
                Random _rdm = new Random();
                item.ArticleBL_LotTemp = DateTime.UtcNow.Year.ToString() + "/" + DateTime.UtcNow.Month.ToString() + DateTime.UtcNow.Day.ToString() + "LOT" + _rdm.Next(_min, _max);
                var articleBC = _db.article_BCs.Where(p => p.ArticleBC_BCID == bonDeLivraison.BonDeLivraison_BCID && p.ArticleBC_MatiereID == item.ArticleBL_MatiereID).FirstOrDefault();
                articleBC.ArticleBC_QteRest -= item.ArticleBL_Quantie;
                if (articleBC.ArticleBC_QteRest == 0)
                    i++;
                var achat = _db.stock_Achats.Where(p => p.StockAchat_MatiereID == item.ArticleBL_MatiereID && p.StockAchat_LotIntern == item.ArticleBL_LotTemp).FirstOrDefault();
                if (achat != null)
                {
                    achat.StockAchat_QuantiteMatiere += item.ArticleBL_Quantie;
                    achat.StockAchat_QuantiteRestante += item.ArticleBL_Quantie;
                  _db.Entry(achat).State = EntityState.Modified;
                }
                else
                {
                    Stock_Achat stockAchat = new Stock_Achat()
                    {
                        StockAchat_MatiereID = item.ArticleBL_MatiereID,
                        StockAchat_QuantiteMatiere = item.ArticleBL_Quantie,
                        StockAchat_QuantiteRestante = item.ArticleBL_Quantie,
                        StockAchat_LotFournisseur = item.ArticleBL_LotFournisseur,
                        StockAchat_LotIntern = item.ArticleBL_LotTemp,
                        StockAchat_AbonnementID = bonDeLivraison.BonDeLivraison_AbonnementID,
                        StockAchat_UniteMesureID = item.ArticleBL_UniteMesureID,
                        StockAchat_DateLimiteConso = item.ArticleBL_DateLimiteConso,
                        StockAchat_Temperature = item.ArticleBL_Teemperature,
                        StockAchat_DateReception = item.ArticleBL_DateReception

                    };
                    await _db.stock_Achats.AddAsync(stockAchat);
                }
                _db.Entry(articleBC).State = EntityState.Modified;
            }
            if (bc.listeArticles.Count() == i)
            { 
                bc.BonDeCommande_Statut = "Réceptionné";
                _db.Entry(bc).State = EntityState.Modified;
            }
            bonDeLivraison.BonDeLivraison_DateSaisie = DateTime.Now;
            bonDeLivraison.BonDeLivraison_StatutID = 1;
            await _db.bonDeLivraisons.AddAsync(bonDeLivraison);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return bonDeLivraison.listeArticles.ToList();
            else
                return null;
        }

        public async Task<int?> CreateFacture(Facture facture, List<BonDeLivraison_Model> listeBL)
        {
            facture.Facture_DateSaisie = DateTime.Now;
            facture.Facture_Etat = "Non payée";
            await _db.factures.AddAsync(facture);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return await updateBonLivraison(listeBL, facture.Facture_ID);
            else
                return null;
        }

        public async Task<int?> updateBonLivraison(List<BonDeLivraison_Model> listeBL, int factureID)
        {
            foreach(var item in listeBL)
            {
                var bl = _db.bonDeLivraisons.Where(p => p.BonDeLivraison_ID == item.BonDeLivraison_ID).FirstOrDefault();
                bl.BonDeLivraison_FactureID = factureID;
                bl.BonDeLivraison_StatutID = 2;
                _db.Entry(bl).State = EntityState.Modified;
            }
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return confirm;
            else
                return null;
        }
        public async Task<int?> CreateFournisseur(Fournisseur fournisseur)
        {
            fournisseur.Founisseur_IsActive = 1;
            fournisseur.Founisseur_DateCreation = DateTime.Now;
            await _db.fournisseurs.AddAsync(fournisseur);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return fournisseur.Founisseur_Id;
            else
                return null;
        }

        public async Task<bool> deleteFournisseur(int ID, int code)
        {
            Fournisseur fournisseur = _db.fournisseurs.Where(e => e.Founisseur_Id == ID).FirstOrDefault();
            if (fournisseur != null)
            {
                if(code == 0)
                {
                    fournisseur.Founisseur_IsActive = 0;
                }
                else
                {
                    fournisseur.Founisseur_IsActive = 1;
                }
                _db.Entry(fournisseur).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> deleteMatieresLink(int ID, int code)
        {
            FournisseurMatiere matiereLink = _db.fournisseurMatieres.Where(e => e.Id == ID).FirstOrDefault();
            if (matiereLink != null)
            {
                if (code == 0)
                {
                    matiereLink.IsActive = 0;
                }
                else
                {
                    matiereLink.IsActive = 1;
                }
                _db.Entry(matiereLink).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public Fournisseur findFormulaireFournisseur(int formulaireFourisseurId)
        {
            return _db.fournisseurs.Where(e => e.Founisseur_Id == formulaireFourisseurId).FirstOrDefault();
        }

        public IEnumerable<Article_BC> GetArticlesBC(int bonCommandeID)
        {
            return _db.article_BCs.Where(p => p.ArticleBC_BCID == bonCommandeID).Include(p=>p.bonDeCommande).Include(p=>p.Unite_Mesure).AsEnumerable();
        }  
        public IEnumerable<Stock_Achat> GetMatireStockAchat(int aboID, int? matiereID, string lotIntern)
        {
            
            var query = _db.stock_Achats.Where(p => p.StockAchat_AbonnementID == aboID);
            if (matiereID != null)
                query = query.Where(p => p.StockAchat_MatiereID == matiereID);
            if (!string.IsNullOrEmpty(lotIntern))
                query = query.Where(p => p.StockAchat_LotIntern.Contains(lotIntern));
           /* if (pg < 1)
                pg = 1;
            int recsCount = query.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;*/
            return query.Include(p=>p.MatierePremiere).Include(p=>p.Unite_Mesure).AsEnumerable();
        }
        public paginationModel<ProduitVendable> getAllProds(int? Sous, string nom, int aboid, int pg = 1)
        {
            int pageSize = 15;
            if (pg < 1)
                pg = 1;
            int startRow = (pg -1) * pageSize;
            var query = new List<ProduitVendable>();
            if (String.IsNullOrEmpty(nom))
                nom = "";
            if ( Sous != null)
            {
                query = _db.Database.GetDbConnection().Query<ProduitVendable>("produitCount", new
                {
                    startRowIndex = startRow,
                    maximumRows = pageSize,
                    aboID = aboid,
                    sousCateg = (int)Sous,
                    name = nom
                }, commandType: CommandType.StoredProcedure).ToList();
            }
            else
            {
                query = _db.Database.GetDbConnection().Query<ProduitVendable>("produitCount", new
                {
                    startRowIndex = startRow,
                    maximumRows = pageSize,
                    aboID = aboid,
                    sousCateg = Sous,
                    name = nom
                }, commandType: CommandType.StoredProcedure).ToList();
            }
            var pagination = new paginationModel<ProduitVendable>
            {
                objList = query,
                count = _db.produitVendables.Count()
            };
            return pagination;
        }  
        public IEnumerable<Article_BL> GetArticlesBL(int bondeLivraisonID)
        {
            return _db.article_BLs.Where(p => p.ArticleBL_BonLivraisonID == bondeLivraisonID).Include(p=>p.bonDeLivraison)
                .Include(p=>p.MatierePremiere)
                .Include(p=>p.Unite_Mesure).AsEnumerable();
        } 
        public IEnumerable<Article_BC> GetArticlesBCForBL(int bonCommandeID)
        {
            return _db.article_BCs.Where(p => p.ArticleBC_BCID == bonCommandeID && p.ArticleBC_QteRest > 0).Include(p => p.bonDeCommande).Include(p => p.Unite_Mesure).AsEnumerable();
        }

        public IEnumerable<BonDeCommande> GetBonDeCommandes(int aboID, int? pointStockID, int? fournisseurID, string name, int? date, string statut)
        {
            var query = _db.bonDeCommandes.Where(p => p.BonDeCommande_AbonnementID == aboID);
            if (statut == null && name == null && date == null && fournisseurID == null)
                return null;
            if (fournisseurID != null)
                query = query.Where(p => p.BonDeCommande_FournisseurID == fournisseurID);
            if (date != null)
                query = query.Where(p => p.BonDeCommande_DateCreation.Year == date);
            if (!string.IsNullOrEmpty(statut))
                query = query.Where(p => p.BonDeCommande_Statut == statut);
            if(!string.IsNullOrEmpty(name))
                query = query.Where(p => p.BonDeCommande_Numero.Contains(name, StringComparison.OrdinalIgnoreCase));
            return query.Include(p=>p.Fournisseur).AsEnumerable().OrderByDescending(p => p.BonDeCommande_DateCreation);
        }

        public IEnumerable<BonDeLivraison> GetBonDeLivraisons(int? fournisseurID, int? bonCommandeID, int aboID, string date, int? statut)
        {
            var query = _db.bonDeLivraisons.Where(p => p.BonDeLivraison_AbonnementID == aboID);
            if (bonCommandeID == null && statut == null && fournisseurID == null && date == "")
                return null;
            if (fournisseurID != null)
                query = query.Where(p => p.Bon_De_Commande.BonDeCommande_FournisseurID == fournisseurID);
            if (bonCommandeID != null)
                query = query.Where(p => p.BonDeLivraison_BCID == bonCommandeID);
            if (date != "")
                query = query.Where(p => Convert.ToDateTime(p.BonDeLivraison_DateLivraison).ToString("yyyy-MM-dd") == date);
            if (statut != null)
                query = query.Where(p => p.BonDeLivraison_StatutID == statut);
            return query.Include(p => p.Bon_De_Commande).ThenInclude(p=>p.Fournisseur)
                .Include(p => p.Statut_BL).AsEnumerable().OrderByDescending(p => p.BonDeLivraison_DateLivraison);
        }

        public IEnumerable<Facture> GetFactures(int aboID, int? bondeCommande, string numeroFac, int? date)
        {
            if (bondeCommande == null && date == null && string.IsNullOrEmpty(numeroFac) == true)
                return null;
            var query = _db.factures.Where(p => p.Facture_AbonnementID == aboID);
            if (bondeCommande != null)
               query = query.Where(p => p.Facture_BonDeCommandeID == bondeCommande);
            if (date != null)
                query = query.Where(p => p.Facture_DateFacture.Year == date);
            if (!string.IsNullOrEmpty(numeroFac))
                query = query.Where(p => p.Facture_Numero.Contains(numeroFac, StringComparison.OrdinalIgnoreCase));
            return query.Include(p => p.Fournisseur).Include(p=>p.listePaiement).Include(p => p.bonDeCommande).AsEnumerable().OrderByDescending(p => p.Facture_DateSaisie);
        }

        public IEnumerable<Fonction> getListFonction()
        {
            return _db.fonctions.AsEnumerable();
        }

        public IEnumerable<Fournisseur> getListFournisseur(int Id, int?VilleId, int? statut)
        {
            IEnumerable<Fournisseur> fournisseur;
            if(VilleId != null)
            {
                if(statut != null)
                {
                    fournisseur = _db.fournisseurs.Where(a => a.Founisseur_AbonnementId == Id && a.Founisseur_VilleID == VilleId && a.Founisseur_IsActive==statut).Include(f => f.Ville);
                }
                else
                {
                    fournisseur = _db.fournisseurs.Where(a => a.Founisseur_AbonnementId == Id && a.Founisseur_VilleID == VilleId).Include(f => f.Ville);
                }
            }

            else
            {
                if (statut != null)
                {
                    fournisseur = _db.fournisseurs.Where(a => a.Founisseur_AbonnementId == Id && a.Founisseur_IsActive == statut).Include(f => f.Ville);

                }
                else
                {
                    fournisseur = _db.fournisseurs.Where(a => a.Founisseur_AbonnementId == Id).Include(f => f.Ville);
                }
            }
            return fournisseur;
            }

        public IEnumerable<MatierePremiere> getListMatiere(int Id)
        {
            return _db.matierePremieres.Where(m => m.MatierePremiere_AbonnementId == Id).AsEnumerable();
        }

        public IEnumerable<FournisseurMatiere> getListMatiereLink(int fournisseurId)
        {
            return _db.fournisseurMatieres.Where(p => p.Founisseur_Id == fournisseurId)
                .Include(p=>p.Founisseur)
                .Include(p=>p.Matiere_Premiere)
                .AsEnumerable();
        }

        public IEnumerable<Ville> getListVilles()
        {
            return _db.villes.AsEnumerable();
        }


        /* public IEnumerable<Fournisseur> getListAllFournisseur()
         {
             return _db.fournisseurs.Where(a => a.Founisseur_IsActive == 1);
         }*/


        public async Task<bool> updateFormulaireFournisseur(int id, Fournisseur newFournisseur)
        {
            Fournisseur fournisseur = _db.fournisseurs.Where(e => e.Founisseur_Id == id).FirstOrDefault();
            if (fournisseur != null)
            {
                fournisseur.Founisseur_RaisonSocial = newFournisseur.Founisseur_RaisonSocial;
                fournisseur.Founisseur_ICE = newFournisseur.Founisseur_ICE;
                fournisseur.Founisseur_Adresse = newFournisseur.Founisseur_Adresse;
                fournisseur.Founisseur_NomContact = newFournisseur.Founisseur_NomContact;
                fournisseur.Founisseur_Telephone = newFournisseur.Founisseur_Telephone;
                //fournisseur.Founisseur_DateCreation = newFournisseur.Founisseur_DateCreation;
                fournisseur.Founisseur_IsActive = 1;
                _db.Entry(fournisseur).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public BonDeCommande FindFormulaireBonDeCommande(int aboID, int? bonCommandeID)
        {
            var bC = _db.bonDeCommandes.Where(p => p.BonDeCommande_AbonnementID == aboID && p.BonDeCommande_ID == bonCommandeID)
                .Include(p=>p.Fournisseur)
                .Include(p=>p.Abonnement_Client)
                .Include(p=>p.listeArticles).ThenInclude(p=>p.MatierePremiere)
                .Include(p=>p.listeArticles).ThenInclude(p=>p.Unite_Mesure)
                .FirstOrDefault();
            return bC;
        } 
        public BonDeLivraison FindFormulaireBonDeLivraison(int aboID, int? bondeLivraison)
        {
            var bC = _db.bonDeLivraisons.Where(p => p.BonDeLivraison_AbonnementID == aboID && p.BonDeLivraison_ID == bondeLivraison)
                .Include(p=>p.Bon_De_Commande).ThenInclude(p=>p.Fournisseur)
                .Include(p=>p.Abonnement_Client)
                .Include(p=>p.listeArticles).ThenInclude(p=> p.MatierePremiere)
                .Include(p=>p.listeArticles).ThenInclude(p=>p.Unite_Mesure)
                .FirstOrDefault();
            return bC;
        }

        public async Task<int?> CreateOrdreTransfer(Transfert_Matiere transfert)
        {
            transfert.TransfertMat_DateCreation = DateTime.Now;
            transfert.TransfertMat_Statut = "En attente";
            await _db.transfert_Matieres.AddAsync(transfert);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return await checkeMatieres(transfert);
            else
                return null;
        }
        public async Task<int?> checkeMatieres(Transfert_Matiere transfer)
        {
            foreach(var item in transfer.listeMatiere)
            {
                var matiere = _db.stock_Achats.Where(p => p.StockAchat_MatiereID == item.MatiereTrans_MatiereID && p.StockAchat_LotIntern == item.MatiereTrans_LotNumber && p.StockAchat_AbonnementID == p.StockAchat_AbonnementID).FirstOrDefault();
                matiere.StockAchat_QuantiteMatiere -= item.MatiereTrans_Quantite;
                if (matiere.StockAchat_QuantiteMatiere <= 0)
                    _db.stock_Achats.Remove(matiere);
                else
                    _db.Entry(matiere).State = EntityState.Modified;
            }
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return transfer.TransfertMat_ID;
            else
                return null;
        }
        public IEnumerable<Transfert_Matiere> GetListeOrdreTransfert(int aboId, string statut, int? stockID, string date)
        {
            var query = _db.transfert_Matieres.Where(p => p.TransfertMat_AbonnementID == aboId);
            if ( string.IsNullOrEmpty(statut) == true && string.IsNullOrEmpty(date) == true && stockID == null)
                return null;
            if (stockID!=null)
                query = query.Where(p => p.listeMatiere.Any(x=>x.MatiereTrans_StockID == stockID)==true);
            if (!string.IsNullOrEmpty(statut))
                query = query.Where(p => p.TransfertMat_Statut == statut);
            if (!string.IsNullOrEmpty(date))
                query = query.Where(p => Convert.ToDateTime(p.TransfertMat_DateCreation).ToString("yyyy-MM-dd") == date);
            return query.AsEnumerable().OrderByDescending(p => p.TransfertMat_DateCreation);
        }

        public IEnumerable<Matiere_Transfert> GetListeMatiereParOrdre(int? transferID,int? stockID, string matiereID, string lot, string statut, string date)
        {
            var query = _db.matiere_Transferts.Where(p => p.MatiereTrans_TransferID == transferID);
            if (transferID == null && stockID == null && string.IsNullOrEmpty(lot) == true && string.IsNullOrEmpty(matiereID) == true && string.IsNullOrEmpty(statut) == true && string.IsNullOrEmpty(date) == true)
                return null;

            if (!string.IsNullOrEmpty(matiereID))
                query = query.Where(p => p.MatierePremiere.MatierePremiere_Libelle.Contains(matiereID) == true);

            if (!string.IsNullOrEmpty(lot))
                query = query.Where(p => p.MatiereTrans_LotNumber.Contains(lot) == true);
            if (stockID != null)
                query = query.Where(p => p.MatiereTrans_StockID == stockID);
            return query.Include(p => p.MatierePremiere).Include(p=>p.Lieu_Stockage).Include(p=>p.Unite_Mesure).Include(p=>p.Transfert_Matiere).AsEnumerable();
        }
        public IEnumerable<Unite_Mesure> findFormulaireUnite(int unite)
        {
            return _db.unite_Mesures.Where(p => p.UniteMesure_Id == unite).AsEnumerable();
        }
        public async Task<bool?> ReceptionMatiereAchats(ReceptionAchatModel receptionAchatModel)
        {
            var matTrans = _db.matiere_Transferts.Where(p => p.MatiereTrans_ID == receptionAchatModel.MatTransID).FirstOrDefault();
            matTrans.MatiereTrans_Statut = "Validée";
            matTrans.MatiereTrans_ValidePar = receptionAchatModel.userID;
            matTrans.MatiereTrans_DateValidation = DateTime.Now;
            var ordreTrans = _db.transfert_Matieres.Where(p => p.TransfertMat_ID == receptionAchatModel.TransferID).Include(p => p.listeMatiere).FirstOrDefault();
            if (ordreTrans.listeMatiere.Where(p => p.MatiereTrans_Statut == "Validée").Count() == ordreTrans.listeMatiere.Count())
            {
                ordreTrans.TransfertMat_Statut = "Validé";
                _db.Entry(ordreTrans).State = EntityState.Modified;

            }
            var stockage = _db.matierePremiereStockages.Where(m => m.MatierePremiereStokage_MatierePremiereId == receptionAchatModel.matiereID && m.MatierePremiereStokage_SectionStockageId == receptionAchatModel.SectionID).FirstOrDefault();
            if (stockage != null)
            {
                var k = _db.mouvements.Where(m => m.MouvementStock_MatierePremiereStokageId == stockage.MatierePremiereStokage_SectionStockageId).AsEnumerable();
                foreach (var item in k)
                {
                    item.MouvementStock_IsActive = 0;
                    _db.Entry(item).State = EntityState.Modified;

                }
                stockage.MatierePremiereStokage_QuantiteActuelle += receptionAchatModel.Quantite;

                _db.Entry(matTrans).State = EntityState.Modified;
                _db.Entry(stockage).State = EntityState.Modified;

                var mouvement = new MouvementStock()
                {
                    MouvementStock_MatierePremiereStokageId = stockage.MatierePremiereStokage_Id,
                    MouvementStock_AbonnementId = stockage.MatierePremiereStokage_AbonnementId,
                    MouvementStock_Quantite = receptionAchatModel.Quantite,
                    MouvementStock_IsActive = 1,
                    MouvementStock_MouvementTypeId = 3,
                    MouvementStock_UniteMesureId = matTrans.MatiereTrans_UniteID,
                    MouvementStock_Date = DateTime.Now,
                    MouvementStock_DateCreation = DateTime.Now,
                    MouvementStock_DateReception = DateTime.Now,
                    MouvementStock_DateSaisie = DateTime.Now,
                    MouvementStock_MatiereQuantiteActuelle = stockage.MatierePremiereStokage_QuantiteActuelle
                };
                await _db.mouvements.AddAsync(mouvement);
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            else
                return null;
            
        }

        public async Task<int?> CreatePayementFacture(Payement_Facture payement_Facture)
        {
            var facture = _db.factures.Where(p => p.Facture_ID == payement_Facture.PayementFacture_FactureID).Include(p => p.listePaiement).FirstOrDefault();
            if (facture != null)
            {
                payement_Facture.PayementFacture_DateSaisie = DateTime.Now;
                await _db.payement_Factures.AddAsync(payement_Facture);
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return await UpdateFacture(facture);
                else
                    return null;
            }
            else
                return null;
        }
        public async Task<int?> UpdateFacture(Facture facture)
        {
            var sum = facture.listePaiement.Sum(p => p.PayementFacture_Montant);
            if (sum == facture.Facture_TotalTTC)
                facture.Facture_Etat = "Payée";
            else
                facture.Facture_Etat = "Partiellement payée";
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return facture.Facture_ID;
            else
                return null;
        }
        public async Task<List<Payement_Facture>> getListPayementFacture(int factureID, int aboID)
        {
            var paiements = await _db.payement_Factures.Where(p=>p.PayementFacture_FactureID == factureID && p.PayementFacture_AbonnementID == aboID)
                .OrderByDescending(p=>p.PayementFacture_DatePayement)
                .Include(p=>p.Facture)
                .ToListAsync();
            return paiements;
        }
        public IEnumerable<Section_Stockage> getListSections(int matiereID)
        {
            var matStock =  _db.matierePremiereStockages.Where(p=>p.MatierePremiereStokage_MatierePremiereId == matiereID)
                .Include(p=>p.Section_Stockage).ThenInclude(p=>p.Zone_Stockage)
                .AsEnumerable().Select(p=>p.Section_Stockage);
            return matStock;

        }
        public IEnumerable<BonDeLivraison> getlistFactureDetails(int factureID, int aboID)
        {
            return _db.bonDeLivraisons.Where(p => p.BonDeLivraison_FactureID == factureID && p.BonDeLivraison_AbonnementID == aboID)
                .Include(p=>p.Bon_De_Commande).ThenInclude(p=>p.Fournisseur)
                .Include(p=>p.Statut_BL).AsEnumerable();
        }

        public async Task<int?> CreateDistributeur(Distributeur distributeur)
        {
            distributeur.Distributeur_IsActive = 1;
            distributeur.Distributeur_DateCreation = DateTime.Now;
            distributeur.Distributeur_DateSaisie = DateTime.Now;
            await _db.distributeurs.AddAsync(distributeur);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return distributeur.Distributeur_ID;
            else
                return null;
        }

        public async Task<List<Distributeur>> getListDistributeur(int Id, int? statut, string rc)
        {
            var query = _db.distributeurs.Where(p => p.Distributeur_AbonnementID == Id);
            if (statut == null && string.IsNullOrEmpty(rc) == true)
                return null;
            if (!string.IsNullOrEmpty(rc))
                query = query.Where(p => p.Distributeur_RaisonSocial.Contains(rc) == true);
            if (statut != null)
                query = query.Where(p => p.Distributeur_IsActive == statut);
            return await query.ToListAsync();
        }

        public async Task<bool> deleteDistributeur(int ID, int code)
        {
            Distributeur distributeur = _db.distributeurs.Where(e => e.Distributeur_ID == ID).FirstOrDefault();
            if (distributeur != null)
            {
                if (code == 0)
                {
                    distributeur.Distributeur_IsActive = 0;
                }
                else
                {
                    distributeur.Distributeur_IsActive = 1;
                }
                _db.Entry(distributeur).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<Article_BL> getArticleBL(int articleID, int aboID)
        {
            var res = await _db.article_BLs.Where(p => p.ArticleBL_ID == articleID && p.bonDeLivraison.BonDeLivraison_AbonnementID == aboID)
                .Include(p=>p.bonDeLivraison)
                .FirstOrDefaultAsync();
            return res;
        }
    }
}
