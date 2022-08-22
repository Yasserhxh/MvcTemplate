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
            bonDeCommande.BonDeCommande_Numero = "BC-" + DateTime.UtcNow.Year.ToString() + "/" + DateTime.UtcNow.Month.ToString()+ "-" + count;
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
                  //_db.Entry(achat).State = EntityState.Modified;
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
                        StockAchat_AbonnementID = bonDeLivraison.BonDeLivraison_AbonnementID

                    };
                    await _db.stock_Achats.AddAsync(stockAchat);
                }
               // _db.Entry(articleBC).State = EntityState.Modified;
            }
            if (bc.listeArticles.Count() == i)
            { 
                bc.BonDeCommande_Statut = "Réceptionné";
                //_db.Entry(bc).State = EntityState.Modified;
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
        public IEnumerable<Stock_Achat> GetMatireStockAchat(int aboID, int? matiereID, string lotIntern, string CurrentSort, int? page)
        {
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
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
        public List<ProduitVendable> getAllProds(int startRow, int maxRow)
        {
           // var p = new DynamicParameters();
           // p.Add("@a", 11);
          //  p.Add("@c", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            var query = _db.Database.GetDbConnection().Query<ProduitVendable>("produitCount", new { startRowIndex = startRow, maximumRows = maxRow }, commandType: CommandType.StoredProcedure);
            return query.ToList();
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

        public IEnumerable<Facture> GetFactures(int aboID, int? point, string date)
        {
            var query = _db.factures.Where(p => p.Facture_AbonnementID == aboID);
            if (point != null)
                query = query.Where(p => p.Facture_PointStockID == point);
            if (date != "")
                query = query.Where(p => Convert.ToDateTime(p.Facture_DateFacture).ToString("yyyy-MM-dd") == date);
            return query.Include(p => p.Fournisseur).Include(p => p.bonDeCommande).Include(p => p.Lieu_Stockage).AsEnumerable().OrderByDescending(p => p.Facture_DateSaisie);
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
    }
}
