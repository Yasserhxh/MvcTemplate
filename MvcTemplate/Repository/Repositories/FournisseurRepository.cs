using Domain.Entities;
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
            bonDeCommande.BonDeCommande_DateCreation = DateTime.UtcNow;
          
            await _db.bonDeCommandes.AddAsync(bonDeCommande);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return bonDeCommande.BonDeCommande_ID;
            else
                return null;
        }

        public async Task<int?> CreateBonDeLivraison(BonDeLivraison bonDeLivraison)
        {
            bonDeLivraison.BonDeLivraison_DateSaisie = DateTime.UtcNow;

            await _db.bonDeLivraisons.AddAsync(bonDeLivraison);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return bonDeLivraison.BonDeLivraison_ID;
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
            return _db.article_BCs.Where(p => p.ArticleBC_BCID == bonCommandeID).AsEnumerable();
        }

        public IEnumerable<BonDeCommande> GetBonDeCommandes(int aboID, int? pointStockID, int? fournisseurID, string date)
        {
            var query = _db.bonDeCommandes.Where(p => p.BonDeCommande_AbonnementID == aboID);
            if (pointStockID != null)
                query = query.Where(p => p.BonDeCommande_PointStockID == pointStockID);
            if (fournisseurID != null)
                query = query.Where(p => p.BonDeCommande_FournisseurID == fournisseurID);
            if (date != "")
                query = query.Where(p => Convert.ToDateTime(p.BonDeCommande_DateCreation).ToString("yyyy-MM-dd") == date);
            return query.Include(p=>p.Fournisseur).Include(p=>p.Lieu_Stockage).AsEnumerable();
        }

        public IEnumerable<BonDeLivraison> GetBonDeLivraisons(int? bonCommandeID, int aboID, string date)
        {
            var query = _db.bonDeLivraisons.Where(p => p.BonDeLivraison_AbonnementID == aboID);
            if (bonCommandeID != null)
                query = query.Where(p => p.BonDeLivraison_BCID == bonCommandeID);
            if (date != "")
                query = query.Where(p => Convert.ToDateTime(p.BonDeLivraison_DateLivraison).ToString("yyyy-MM-dd") == date);
            return query.Include(p => p.Bon_De_Commande).AsEnumerable();
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
    }
}
