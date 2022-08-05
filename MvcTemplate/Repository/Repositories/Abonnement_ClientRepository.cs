using Domain.Authentication;
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
    public class Abonnement_ClientRepository : IAbonnement_ClientRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork unitOfWork;

        public Abonnement_ClientRepository(ApplicationDbContext db, IUnitOfWork unitOfWork)
        {
            _db = db;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool?> AjouterUtilisateur(int idAtelier, string responsableId)
        {
            var atelier = _db.ateliers.Where(f => f.Atelier_ID == idAtelier).FirstOrDefault();
            var link = _db.atelier_Users.Where(p => p.User_Id == responsableId && p.Atelier_Id == idAtelier).FirstOrDefault();

            if (link != null)

                return null;

            else
            {
                var user = _db.Users.Where(m => m.Id == responsableId).FirstOrDefault();
                var atelierUser = new Atelier_User
                {

                    User = user,
                    Atelier = atelier,
                    Abonnement_ID = atelier.Atelier_AbonnementID,
                    IsActive = 1,
                    //Date = DateTime.Now,
                };
                await _db.atelier_Users.AddAsync(atelierUser);
            }

            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return true;
            else
                return null;
        }

        public async Task<int?> CreateAtelier(Atelier atelier)
        {
            atelier.Atelier_IsActive = 1;
            atelier.Atelier_DateCreation = DateTime.Now;
            await _db.ateliers.AddAsync(atelier);
            await unitOfWork.Complete(); 
            var user = _db.Users.Where(u => u.Id == atelier.Atelier_UTILISATEUR).FirstOrDefault();
            user.AtelierID = atelier.Atelier_ID;
            _db.Entry(user).State = EntityState.Modified;
            var res = await AjouterUtilisateur(atelier.Atelier_ID, atelier.Atelier_UTILISATEUR);
            if (res == null)
                return null;
            else
            {
                return atelier.Atelier_ID;
            }

        }

        public async Task<int?> CreateClient(Abonnement_Client abonnement_Client)
        {
            abonnement_Client.Abonnement_IsActive = 1;
            await _db.abonnement_Clients.AddAsync(abonnement_Client);
            var confirm = await unitOfWork.Complete();
            var logo = new LogoUser
            {
                Abonnement_Id = abonnement_Client.Abonnement_Id,
                Logo = abonnement_Client.Abonnement_Logo,
            };
            await _db.logoUser.AddAsync(logo); 
            if (confirm > 0)
                return abonnement_Client.Abonnement_Id;
            else
                return null;
        }

        public async Task<bool> deleteFormulaireAtelier(int ID, int code)
        {
            Atelier atelier = _db.ateliers.Where(e => e.Atelier_ID == ID).FirstOrDefault();
            if (atelier != null)
            {
                if(code == 0)
                {
                    atelier.Atelier_IsActive = 0;
                }
                if(code == 1)
                {
                    atelier.Atelier_IsActive = 1;
                }
                _db.Entry(atelier).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> deleteFormulaireClient(int ID)
        {

            Abonnement_Client client = _db.abonnement_Clients.Where(e => e.Abonnement_Id == ID).FirstOrDefault();
            if (client != null)
            {
                client.Abonnement_IsActive = 0;
                _db.Entry(client).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public Atelier findFormulaireAtelier(int formulaireAtelierId)
        {
            return _db.ateliers.Where(a => a.Atelier_ID == formulaireAtelierId)/*.Where(a => a.Atelier_IsActive == 1)*/.FirstOrDefault();
        }

        public Abonnement_Client findFormulaireClient(int formulaireClientId)
        {
            return _db.abonnement_Clients.Where(a => a.Abonnement_Id == formulaireClientId).Where(a => a.Abonnement_IsActive == 1).FirstOrDefault();
        }

        public IEnumerable<Atelier> getListAtelier(int Id,int? statut)
        {
            IEnumerable<Atelier> ateliers;
            if (statut!=null)
                ateliers = _db.ateliers.Where(a => a.Atelier_AbonnementID == Id && a.Atelier_IsActive==statut).Include(a => a.Ville).AsEnumerable();
            else
                ateliers = _db.ateliers.Where(a => a.Atelier_AbonnementID == Id).Include(a => a.Ville).AsEnumerable();

            return ateliers;
        }

        public IEnumerable<Atelier_User> getListAtelierUser(string UserId)
        {
            return _db.atelier_Users.Where(p => p.User_Id == UserId && p.IsActive == 1)
               .Include(p => p.User)
               .Include(p => p.Atelier)
               .AsEnumerable();
        }

        public IEnumerable<Abonnement_ClientModel> getListClient()
        {
            var clients = (from u in _db.abonnement_Clients
                           where u.Abonnement_IsActive == 1
                           select new Abonnement_ClientModel()
                           {
                               Abonnement_Id =u.Abonnement_Id,
                               Abonnement_NomClient = u.Abonnement_NomClient,
                               Abonnement_ContactEmail = u.Abonnement_ContactEmail,
                               Abonnement_ContactTelephone = u.Abonnement_ContactTelephone,
                               Abonnement_IdentifiantFiscal = u.Abonnement_IdentifiantFiscal,
                               Abonnement_ICE = u.Abonnement_ICE,
                               Abonnement_RegistreCommercial = u.Abonnement_RegistreCommercial,
                               Abonnement_Adresse = u.Abonnement_Adresse,
                               Abonnement_ContactNomPrenom = u.Abonnement_ContactNomPrenom,
                               Abonnement_Telephone = u.Abonnement_Telephone,
                               Abonnement_VilleId = u.Abonnement_VilleId,
                               Abonnement_IsActive = u.Abonnement_IsActive
                           }).ToList();

            return clients;

            //return _db.abonnement_Clients/*.Where(a => a.Abonnement_IsActive == '1')*/.AsEnumerable();//.Include(e => e.VILLE).AsEnumerable();
        }

        public IEnumerable<Lieu_Stockage> getListStocks(int Id)
        {
            return _db.lieu_Stockages.Where(p => p.LieuStockag_AbonnementId == Id && p.LieuStockag_IsActive == 1).AsEnumerable();
        }

        public IEnumerable<Ville> getListVilles()
        {
            return _db.villes.AsEnumerable();
        }

        public async Task<bool> updateFormulaireAtelier(int id, Atelier newAtelier)
        {
            Atelier atelier = _db.ateliers.Where(x => x.Atelier_ID == id).FirstOrDefault();
            //var atelier = (from w in _db.ateliers where w.Atelier_ID == id select new Atelier()).FirstOrDefault();

            if (atelier != null)
            {
                atelier.Atelier_Nom = newAtelier.Atelier_Nom;
                atelier.Atelier_NomResponsable = newAtelier.Atelier_NomResponsable;
                atelier.Atelier_Adresse = newAtelier.Atelier_Adresse;
                atelier.Atelier_CodePostal = newAtelier.Atelier_CodePostal;
                atelier.Atelier_Telephone = newAtelier.Atelier_Telephone;
                atelier.Atelier_VilleID = newAtelier.Atelier_VilleID;
                atelier.Atelier_IsActive = 1;

                _db.Entry(atelier).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> updateFormulaireClient(int id, Abonnement_Client newClient)
        {
            Abonnement_Client client = _db.abonnement_Clients.Where(x => x.Abonnement_Id == id).FirstOrDefault();
            //var client = (from w in _db.abonnement_Clients where w.Abonnement_Id == id select new Abonnement_Client()).FirstOrDefault();

            if (client != null)
            {
                client.Abonnement_ContactNomPrenom = newClient.Abonnement_ContactNomPrenom;
                client.Abonnement_ContactEmail = newClient.Abonnement_ContactEmail;
                client.Abonnement_ContactTelephone = newClient.Abonnement_ContactTelephone;
                client.Abonnement_VilleId = newClient.Abonnement_VilleId;

                client.Abonnement_ICE = newClient.Abonnement_ICE;
                client.Abonnement_RegistreCommercial = newClient.Abonnement_RegistreCommercial;
                client.Abonnement_IdentifiantFiscal = newClient.Abonnement_IdentifiantFiscal;

                client.Abonnement_NomClient = newClient.Abonnement_NomClient;
                client.Abonnement_Telephone = newClient.Abonnement_Telephone;
                client.Abonnement_Adresse = newClient.Abonnement_Adresse;

                client.Abonnement_IsActive = 1;

                _db.Entry(client).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }
        public IEnumerable<FamilleProduit> getListFamilles(int Id)
        {
            return _db.familleProduits.Where(f => f.FamilleProduit_IsActive == 1)
                .Where(f => f.FamilleProduit_AbonnemnetId == Id)
                .AsEnumerable();
        }
        public IEnumerable<PointPorduction_Famille> getListFammilesProduction(int pdvId)
        {
           var prod =  _db.pointPorduction_Familles.Where(p => p.PointProduction_ID == pdvId)
                .Include(p => p.Atelier)
                .Include(p => p.Famille_Produit)
                .AsEnumerable();
            return prod;
        }
        public async Task<bool?> AjouterFamille(int atelierId, List<int> ListFamille)
        {
            var atelier = _db.ateliers.Where(f => f.Atelier_ID == atelierId).FirstOrDefault();
            int i = 0;
            foreach (var familleId in ListFamille)
            {
                var link = _db.pointPorduction_Familles.Where(p => p.FamilleProduit_ID == familleId && p.PointProduction_ID == atelierId).FirstOrDefault();
                if (link != null)
                    i++;
                else
                {
                    var famille = _db.familleProduits.Where(m => m.FamilleProduit_Id == familleId).FirstOrDefault();
                    var pointProd_Famille = new PointPorduction_Famille
                    {

                        Famille_Produit = famille,
                        Atelier = atelier,
                        Abonnement_Id = atelier.Atelier_AbonnementID,
                        Is_Active = 1,
                        DateCreation = DateTime.Now,
                    };
                    await _db.pointPorduction_Familles.AddAsync(pointProd_Famille);
                }
            }
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return true;
            else
                return null;
        }
        public async Task<bool> deleteFamillePdv(int id, int code)
        {
            PointPorduction_Famille famille_pdv = _db.pointPorduction_Familles.Where(e => e.Id == id).FirstOrDefault();
            if (famille_pdv != null)
            {
                if (code == 0)
                {
                    famille_pdv.Is_Active = 0;
                }
                if (code == 1)
                {
                    famille_pdv.Is_Active = 1;
                }
                _db.Entry(famille_pdv).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }
        public IEnumerable<ProduitAppro> getListProduitVendable(int Id, int AboId)
        {
            var famillesAll = _db.familleProduits.Where(f => f.FamilleProduit_AbonnemnetId == AboId)
                .Where(f => f.FamilleProduit_IsActive == 1)
                .Include(fm => fm.PointVente_Link).ToList();
            var familles = new List<FamilleProduit>();
            foreach (var m in famillesAll)
            {
                foreach (var fm in m.PointVente_Link)
                {
                    if (Id == fm.PointVente_Id)
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
            var ProduitAll = _db.produitVendables.Where(ms => ms.ProduitVendable_AbonnementId == AboId)
                .Where(ms => ms.ProduitVendable_IsActive == 1)
                .Include(p => p.Sous_Famille)
                .AsEnumerable();
            var produitList = new List<ProduitVendable>();
            foreach (var ms in ProduitAll)
            {
                foreach (var m in sousfamilleList)
                {
                    if (ms.Sous_Famille.SousFamille_ID == m.SousFamille_ID)
                    {
                        produitList.Add(ms);
                    }
                }

            }
            var ProduitApprovAll = _db.produitAppros.Where(ms => ms.ProduitAppro_AbonnementID == AboId)
                .Include(p => p.Produit_Vendable)
                .AsEnumerable();
            var produitapproList = new List<ProduitAppro>();
            foreach (var ms in ProduitApprovAll)
            {
                foreach (var m in produitList)
                {
                    if (ms.Produit_Vendable.ProduitVendable_Id == m.ProduitVendable_Id)
                    {
                        produitapproList.Add(ms);
                    }
                }

            }

            return produitapproList;
        }

        public async Task<int?> CreateAbonnement(Paiement_Abonnement paiement_Abonnement)
        {
            paiement_Abonnement.PaiementAbonnement_DateSaisie = DateTime.Now;
            await _db.paiement_Abonnements.AddAsync(paiement_Abonnement);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return paiement_Abonnement.PaiementAbonnement_ID;
            else
                return null;
        }

        public IEnumerable<Paiement_Abonnement> getListAbonnement(int? client)
        {
            if (client != null)
                return _db.paiement_Abonnements.Where(p => p.PaiementAbonnement_AbonnementId == client).Include(p => p.Abonnemet_Client).Include(p => p.Point_Vente).AsEnumerable();

            return _db.paiement_Abonnements.Include(p => p.Abonnemet_Client).Include(p => p.Point_Vente).AsEnumerable();
        }
    }
}
