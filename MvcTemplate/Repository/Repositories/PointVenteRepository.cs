using Domain.Authentication;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
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
    public class PointVenteRepository : IPointVenteRespository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public PointVenteRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _db = db;
            this.unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<bool?> AjouterFamille(int pointVenteId, List<int> ListFamille)
        {
            var pointVente = _db.point_Ventes.Where(f => f.PointVente_Id == pointVenteId).FirstOrDefault();
            int i = 0;
            foreach (var familleId in ListFamille)
            {
                var link = _db.pointVente_Familles.Where(p => p.FamilleProduit_Id == familleId && p.PointVente_Id == pointVenteId).FirstOrDefault();
                if (link != null)
                    i++;
                else
                {
                    var famille = _db.familleProduits.Where(m => m.FamilleProduit_Id == familleId).FirstOrDefault();
                    var pointVente_Famille = new PointVente_Famille
                    {

                        Famille_Produit = famille,
                        Point_Vente = pointVente,
                        Abonnement_Id = pointVente.PointVente_AbonnementId,
                        IsActive = 1,
                        DateCreation = DateTime.Now,
                    };
                    await _db.pointVente_Familles.AddAsync(pointVente_Famille);
                }               
            }
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return true;
            else
                return null;
        }

        public async Task<bool> AjouterSalle(int id, Salle salle)
        {
            var positionvente = _db.positionVentes
                .Where(p => p.PositionVente_Id == id).FirstOrDefault();
            if (positionvente != null)
            {
                salle.Salle_DateCreation = DateTime.Now;
                positionvente.Salles.Add(salle);
                _db.Entry(positionvente).State = EntityState.Modified;
                await _db.salles.AddAsync(salle);
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> AjouterTable(int id, IEnumerable<Table> table)
        {
            var salle = _db.salles.Where(f => f.Salle_Id == id).Include(f => f.Tables).FirstOrDefault();

            if (salle != null)
            {
               

                foreach (var p in table)
                {
                    await _db.tables.AddAsync(p);
                }
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<int?> CreateAgentServeur(AgentServeur agentserveur)
        {
            agentserveur.Agent_IsActive = 1;
            agentserveur.Agent_DateCreation = DateTime.Now;
            await _db.agents.AddAsync(agentserveur);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return agentserveur.Agent_Id;
            else
                return null;
        }

        public async Task<int?> CreatePointVent(Point_Vente point_Vente)
        {
            point_Vente.PointVente_IsActive = 1;
            point_Vente.PointVente_DateCreation = DateTime.Now;
            point_Vente.PointVente_DateSaisie = point_Vente.PointVente_DateCreation;
            await _db.point_Ventes.AddAsync(point_Vente);
            await unitOfWork.Complete(); 
            var user = _db.Users.Where(u => u.Id == point_Vente.PointVente_UtilisateurId).FirstOrDefault();
            user.PointVente_ID = point_Vente.PointVente_Id;
            _db.Entry(user).State = EntityState.Modified;
            var res = await AjouterUtilisateur(point_Vente.PointVente_Id, point_Vente.PointVente_UtilisateurId);
            if (res == null)
                return null;
            else
            {
                return point_Vente.PointVente_Id;
            }
        }

        public async Task<int?> CreatePositionVente(PositionVente position)
        {
            position.PositionVente_IsActive = 1;
            position.PositionVente_DateCreation = DateTime.Now;
            await _db.positionVentes.AddAsync(position);
            var user = _db.Users.Where(u => u.Id == position.PositionVente_UtilisateurId).FirstOrDefault();
            user.PositionVente_ID = position.PositionVente_Id;
            _db.Entry(user).State = EntityState.Modified;
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return position.PositionVente_Id;
            else
                return null;
        }

        public async Task<bool> deletePointVente(int ID, int code)
        {
            Point_Vente point_Vente = _db.point_Ventes.Where(e => e.PointVente_Id == ID).FirstOrDefault();
            if (point_Vente != null)
            {
                if(code == 0)
                {
                    point_Vente.PointVente_IsActive = 0;
                }
                if(code == 1)
                {
                    point_Vente.PointVente_IsActive = 1;
                }
                _db.Entry(point_Vente).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }  
        public async Task<bool> deleteFamillePdv(int id, int code)
        {
            /*PointVente_Famille famille_pdv = _db.pointVente_Familles.Where(e => e.Id == id).FirstOrDefault();
            if (famille_pdv != null)
            {
                if(code == 0)
                {
                    famille_pdv.IsActive = 0;
                }
                if(code == 1)
                {
                    famille_pdv.IsActive = 1;
                }*/
            var famille_pdv = _db.pointVente_Familles.Where(e => e.Id == id).FirstOrDefault();
            if (famille_pdv != null)
            {
                if (code == 0)
                {
                    famille_pdv.IsActive = 0;
                }
                if (code == 1)
                {
                    famille_pdv.IsActive = 1;
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

        public Point_Vente findFormulairePointVente(int formulairePointId)
        {
            return _db.point_Ventes.Where(e => e.PointVente_Id == formulairePointId)
                .FirstOrDefault();
        }

        public PositionVente findFormulairePositionVente(int formulairePoseId)
        {
            var t = _db.positionVentes.Where(e => e.PositionVente_Id == formulairePoseId).Include(p => p.Salles)
                .FirstOrDefault();
            return t; 
        } 
        public Salle findFormulaireSalle(int formulaireSalleId)
        {
            var t = _db.salles.Where(e => e.Salle_Id == formulaireSalleId)
                .FirstOrDefault();
            return t; 
        }

        public IEnumerable<Point_Vente> getLisPoinVent(int Id, int? type,int? statut)
        {
            IEnumerable<Point_Vente> pdv;
            if(type != null)
            {
                if(statut != null)
                {
                    pdv = _db.point_Ventes
                        .Where(a => a.PointVente_AbonnementId == Id && a.PointVente_IsActive == statut && a.PointVente_TypeService == type)
                        .Include(a => a.Ville);
                    return pdv;
                }
                else 
                {
                    pdv = _db.point_Ventes                                            
                        .Where(a => a.PointVente_AbonnementId == Id && a.PointVente_TypeService == type)
                        .Include(a => a.Ville); 
                    return pdv;
                }
            }
            else
            {
                if (statut != null)
                {
                    pdv = _db.point_Ventes
                        .Where(a => a.PointVente_AbonnementId == Id && a.PointVente_IsActive == statut)
                        .Include(a => a.Ville);
                    return pdv;
                }
                else {
                    pdv = _db.point_Ventes
                     .Where(a => a.PointVente_AbonnementId == Id)
                     .Include(a => a.Ville);
                    return pdv;
                }
            }
            
        }
        public IEnumerable<Point_Vente> getLisPoinVentActive(int Id)
        {
            return _db.point_Ventes
                .Where(a => a.PointVente_IsActive == 1)
                .Where(a => a.PointVente_AbonnementId == Id).Include(p=>p.PositionsVente);
        }

        public IEnumerable<AgentServeur> getListAgents(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FamilleProduit> getListFamilles(int Id)
        {
            return _db.familleProduits.Where(f => f.FamilleProduit_IsActive == 1)
                .Where(f => f.FamilleProduit_AbonnemnetId == Id)
                .AsEnumerable();
        }

        public IEnumerable<ModePaiement> getListModePaiement()
        {
            return _db.modePaiements.AsEnumerable();
        }

        public IEnumerable<PositionVente> getListPositionVente(int Id)
        {
            var pos = _db.positionVentes.Where(p => p.PositionVente_PointVenteId == Id).Where(p => p.PositionVente_IsActive == 1)
                .Include(p => p.Point_Vente).ThenInclude(p => p.Ville)
                .Include(p => p.Mode_Paiement)
                .Include(p=>p.Salles)
                .AsEnumerable();
            return pos;
        }

        public IEnumerable<ProduitAppro> getListProduitVendable(int Id, int AboId)
        {
            var famillesAll = _db.familleProduits.Where(f=>f.FamilleProduit_AbonnemnetId==AboId)
                .Where(f=>f.FamilleProduit_IsActive==1)
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
                .Include(ms=>ms.Famille_Produit)
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
                .Where(ms=>ms.ProduitVendable_IsActive==1)
                .Include(p=>p.Sous_Famille)
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
                .Include(p=>p.Produit_Vendable)
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

        public IEnumerable<Salle> getListSalles(int Id)
        {
            return _db.salles.Where(s => s.Salle_PositionVenteId == Id).Include(s=>s.Position_Vente).ThenInclude(p=>p.Point_Vente)
                .AsEnumerable();
        } 
        public IEnumerable<Atelier> getListAtelier(int Id)
        {
            return _db.ateliers.Where(s => s.Atelier_AbonnementID == Id && s.Atelier_IsActive == 1)
                .AsEnumerable();
        }
        public IEnumerable<Lieu_Stockage> getListPointStock(int Id)
        {
            return _db.lieu_Stockages.Where(s => s.LieuStockag_AbonnementId == Id && s.LieuStockag_IsActive == 1)
                .AsEnumerable();
        }
        public IEnumerable<Table> getListTables(int Id)
        {
            var t =  _db.tables.Where(p => p.Table_SalleId == Id).Include(p=>p.Salle).ThenInclude(p=>p.Position_Vente)
                .AsEnumerable();
            return t;
        }

        public IEnumerable<Ville> getListVilles()
        {
            return _db.villes.AsEnumerable();
        }

        public IEnumerable<PointVente_Stock> getStockPointVente(int Id, int pdv)
        {
            return _db.pointVente_Stocks.Where(p => p.PointVenteStock_AbonnementID == Id).Where(p => p.PointVenteStock_PointVenteID == pdv)
                .Include(p => p.Produit_Vendable).ThenInclude(p => p.Sous_Famille)
                .Include(p=>p.Produit_Vendable).ThenInclude(p=>p.Unite_Mesure)
                .Include(p=>p.Produit_Vendable).ThenInclude(p=>p.formes)
                .Include(p=>p.Produit_Vendable).ThenInclude(p=>p.Taux_TVA)
                .Include(p => p.Point_Vente)
                .Include(p=>p.Forme_Produit).AsEnumerable();
        }
        public IEnumerable<PointVente_Stock> getStockPointVenteByProduit(int Id, int pdv, int produitId)
        {
            return _db.pointVente_Stocks.Where(p => p.PointVenteStock_AbonnementID == Id)
                .Where(p => p.PointVenteStock_PointVenteID == pdv && p.PointVenteStock_ProduitID == produitId)
                .Include(p => p.Produit_Vendable).ThenInclude(p => p.Sous_Famille)
                .Include(p=>p.Produit_Vendable).ThenInclude(p=>p.Unite_Mesure)
                .Include(p=>p.Produit_Vendable).ThenInclude(p=>p.Taux_TVA)
                .Include(p=>p.Produit_Vendable).ThenInclude(p=>p.formes)
                .Include(p => p.Point_Vente)
                .Include(p=>p.Forme_Produit).AsEnumerable();
        }
        public IEnumerable<PointVente_Stock> getStockPointVenteAvecPlan(int Id, int? pdv)
        {
            if (pdv != null)
            {
                return _db.pointVente_Stocks
                   .Where(p => p.PointVenteStock_AbonnementID == Id)
                   .Where(p => p.PointVenteStock_PointVenteID == pdv && p.Produit_Vendable.ProduitVendable_PlanificationFlag != 0)
                   .Include(p => p.Produit_Vendable).ThenInclude(p => p.Sous_Famille)
                   .Include(p => p.Produit_Vendable).ThenInclude(p => p.Unite_Mesure)
                   .Include(p => p.Produit_Vendable).ThenInclude(p => p.formes)
                   .Include(p => p.Point_Vente)
                   .Include(p => p.Forme_Produit).AsEnumerable();
            }
            else
            {
                return _db.pointVente_Stocks
                .Where(p => p.PointVenteStock_AbonnementID == Id)
                .Where(p => p.Produit_Vendable.ProduitVendable_PlanificationFlag != 0)
                .Include(p => p.Produit_Vendable).ThenInclude(p => p.Sous_Famille)
                .Include(p => p.Produit_Vendable).ThenInclude(p => p.Unite_Mesure)
                .Include(p => p.Produit_Vendable).ThenInclude(p => p.formes)
                .Include(p => p.Point_Vente)
                .Include(p => p.Forme_Produit).AsEnumerable();
            }
            
        }
        public IEnumerable<PointVente_Stock> getStockPointVenteAll(int Id)
        {
            return _db.pointVente_Stocks.Where(p => p.PointVenteStock_AbonnementID == Id)
                .Include(p => p.Produit_Vendable).ThenInclude(p => p.Sous_Famille)
                .Include(p=>p.Produit_Vendable).ThenInclude(p=>p.Unite_Mesure)
                .Include(p=>p.Produit_Vendable).ThenInclude(p=>p.formes)
                .Include(p => p.Point_Vente)
                .Include(p=>p.Forme_Produit).AsEnumerable();
        }

        public IEnumerable<ProduitPackage> getStockProduitPackage(int Id, int pdv)
        {
            var f = _db.formeDetails.Where(p => p.FormeDetails_PointVenteID == pdv && p.FormeDetails_AbonnementID == Id)
                .Select(p => p.ProduitPackage).Distinct()
                .Include(p=>p.formes)
                //.Include(p => p.Sous_ProduitPackage).ThenInclude(p => p.Forme_Produit)
                //.Include(p => p.Sous_Famille)
                .AsEnumerable();
            return f;
            /*return _db.produitPackages.Where(p => p.ProduitPackage_AbonnementID == Id)
                //.Where(p => p.ProduitPackage_PointVenteID == pdv)
                .Include(p => p.formes)
                //.Include(p => p.Point_Vente)
                .Include(p=>p.Sous_Famille)
                .Include(p => p.Sous_ProduitPackage).ThenInclude(p => p.Forme_Produit).AsEnumerable();*/
        } 
        public IEnumerable<Forme_Produit> getStockFormePackage(int Id, int pdv)
        {
            var f = _db.formeDetails.Where(p => p.FormeDetails_PointVenteID == pdv && p.FormeDetails_AbonnementID == Id)
                .Select(p=>p.Forme_Produit)
                .AsEnumerable();
            return f;
        }  
        public IEnumerable<ProduitPackage> getStockProduitPackageAll(int Id)
        {
            return _db.produitPackages.Where(p => p.ProduitPackage_AbonnementID == Id)
                .Include(p => p.formes)
                .Include(p=>p.Unite_Mesure)
                .AsEnumerable();
        }

        public async Task<bool> updateFormulairePointVente(int id, Point_Vente newPointVente)
        {
            Point_Vente point_Vente = _db.point_Ventes.Where(e => e.PointVente_Id == id).FirstOrDefault();
            if (point_Vente != null)
            {
                point_Vente.PointVente_Adresse = newPointVente.PointVente_Adresse;
                point_Vente.PointVente_CodePostal = newPointVente.PointVente_CodePostal;
                point_Vente.PointVente_VilleID = newPointVente.PointVente_VilleID;
                point_Vente.PointVente_DateSaisie = newPointVente.PointVente_DateSaisie;
                point_Vente.PointVente_Nom = newPointVente.PointVente_Nom;
                point_Vente.PointVente_TypeService = newPointVente.PointVente_TypeService;
                point_Vente.PointVente_NomResponsable = newPointVente.PointVente_NomResponsable;
                point_Vente.PointVente_Telephone = newPointVente.PointVente_Telephone;
                point_Vente.PointVente_IsActive = 1;
                _db.Entry(point_Vente).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public IEnumerable<PointVente_Famille> getListFammilesPointVente(int pdvId)
        {
            return _db.pointVente_Familles.Where(p => p.PointVente_Id == pdvId)
                .Include(p => p.Point_Vente)
                .Include(p => p.Famille_Produit)
                .AsEnumerable();
        }
        public IEnumerable<PointVente_Stock> getStockPointVenteByForme(int Id, int pdv, int formeId)
        {
            return _db.pointVente_Stocks.Where(p => p.PointVenteStock_AbonnementID == Id)
                .Where(p => p.PointVenteStock_PointVenteID == pdv && p.PointVenteStock_FormeProduitID == formeId).AsEnumerable();
        }
        public IEnumerable<ProduitPackage> getStockProduitPackage(int Id, int pdv, int formeId)
        {
            return _db.produitPackages.Where(p => p.ProduitPackage_AbonnementID == Id)
               //.Where(p => p.ProduitPackage_PointVenteID == pdv)
               .Include(p => p.formes)
               //.Include(p => p.Point_Vente)
               .Include(p => p.Sous_Famille)
               .Include(p => p.Sous_ProduitPackage).ThenInclude(p => p.Forme_Produit).AsEnumerable();
        }
        public async Task<bool?> AjouterUtilisateur(int idPointVente, string responsableId)
        {
            var pointvente = _db.point_Ventes.Where(f => f.PointVente_Id == idPointVente).FirstOrDefault();
            var link = _db.pointVente_Users.Where(p => p.User_Id == responsableId && p.PointVente_Id == idPointVente).FirstOrDefault();
                
            if (link != null)
                   
                return null;
                
            else
            {
                var user = _db.Users.Where(m => m.Id == responsableId).FirstOrDefault();
                var pdvUser = new PointVente_User
                {

                        User = user,
                        Point_Vente = pointvente,
                        Abonnement_ID = pointvente.PointVente_AbonnementId,
                        IsActive = 1,
                        //Date = DateTime.Now,
                    };
                    await _db.pointVente_Users.AddAsync(pdvUser);
                }
            
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return true;
            else
                return null;
        }

        public IEnumerable<PointVente_User> getListPdvUser(string UserId)
        {
            return _db.pointVente_Users.Where(p => p.User_Id == UserId && p.IsActive == 1)
               .Include(p => p.User)
               .Include(p => p.Point_Vente)
               .AsEnumerable();
        }

        public IEnumerable<ProduitPack_Atelier> getListProduitPack(int Id, int AboId)
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
            var ProduitAll = _db.produitPackages.Where(ms => ms.ProduitPackage_AbonnementID == AboId)
                //.Where(ms => ms.isac == 1)
                .Include(p => p.Sous_Famille)
                .AsEnumerable();
            var produitList = new List<ProduitPackage>();
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
            var produtiStock = _db.produitPack_Ateliers.Where(ms => ms.ProduitPackAtelier_AbonnementID == AboId)
                .Include(p => p.ProduitPackage).Include(p=>p.Forme_Produit)
                .AsEnumerable();
            var produitapproList = new List<ProduitPack_Atelier>();
            foreach (var ms in produtiStock)
            {
                foreach (var m in produitList)
                {
                    if (ms.ProduitPackage.ProduitPackage_ID == m.ProduitPackage_ID)
                    {
                        produitapproList.Add(ms);
                    }
                }
            }
            return produitapproList;
        }

        public async Task<int?> CreateEchange(Echange_Produits echange_Produits)
        {
            echange_Produits.EchangeProduits_DateEchange = DateTime.Now.AddHours(1);
            await _db.echange_Produits.AddAsync(echange_Produits);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return echange_Produits.EchangeProduits_ID;
            else
                return null;
        }
        public IEnumerable<Echange_Produits> getListEchanges(int AboID, string statut, string date, int? pdvF, int? pdvRec)
        {
            if (statut != "")
            {
                if (date != "")
                {
                    DateTime newDate = Convert.ToDateTime(date).Date;
                    if (pdvF != null)
                    {
                        if (pdvRec != null)
                        {
                            return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID && Convert.ToDateTime(p.EchangeProduits_DateEchange).ToString("yyyy-MM-dd") == date)
                                .Where(p => p.EchangeProduits_Statut == statut && p.EchangeProduits_PdvFournisseurID == pdvF && p.EchangeProduits_PdvRecepID == pdvRec)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        }
                        else
                        {
                            return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID && Convert.ToDateTime(p.EchangeProduits_DateEchange).ToString("yyyy-MM-dd") == date)
                                .Where(p => p.EchangeProduits_Statut == statut && p.EchangeProduits_PdvFournisseurID == pdvF)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        }
                    }
                    else
                    {
                        if (pdvRec != null)
                        {
                            return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID && p.EchangeProduits_DateEchange.Date == DateTime.Now.Date)
                                .Where(p => p.EchangeProduits_Statut == statut && p.EchangeProduits_PdvRecepID == pdvRec)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        }
                        else
                        {
                            return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID && p.EchangeProduits_DateEchange.Date == DateTime.Now.Date)
                                .Where(p => p.EchangeProduits_Statut == statut)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        }
                    }
                }
                else
                {
                    if (pdvF != null)
                    {
                        if (pdvRec != null)
                        {
                            return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID)
                                .Where(p => p.EchangeProduits_Statut == statut && p.EchangeProduits_PdvFournisseurID == pdvF && p.EchangeProduits_PdvRecepID == pdvRec)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        }
                        else
                        {
                            return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID)
                                .Where(p => p.EchangeProduits_Statut == statut && p.EchangeProduits_PdvFournisseurID == pdvF)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        }
                    }
                    else
                    {
                        if (pdvRec != null)
                        {
                            return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID)
                                .Where(p => p.EchangeProduits_Statut == statut && p.EchangeProduits_PdvRecepID == pdvRec)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        }
                        else
                        {
                            return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID)
                                .Where(p => p.EchangeProduits_Statut == statut)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        }
                    }
                }
            }
            else
            {
                if (date != "")
                {
                    DateTime newDate = Convert.ToDateTime(date);
                    if (pdvF != null)
                    {
                        if (pdvRec != null)
                        {
                            return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID && Convert.ToDateTime(p.EchangeProduits_DateEchange).ToString("yyyy-MM-dd") == date)
                                .Where(p => p.EchangeProduits_PdvFournisseurID == pdvF && p.EchangeProduits_PdvRecepID == pdvRec)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        }
                        else
                        {
                            return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID && Convert.ToDateTime(p.EchangeProduits_DateEchange).ToString("yyyy-MM-dd") == date)
                                .Where(p => p.EchangeProduits_PdvFournisseurID == pdvF)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        }
                    }
                    else
                    {
                        if (pdvRec != null)
                        {
                            return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID && Convert.ToDateTime(p.EchangeProduits_DateEchange).ToString("yyyy-MM-dd") == date)
                                .Where(p => p.EchangeProduits_PdvRecepID == pdvRec)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        }
                        else
                        {
                            return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID && Convert.ToDateTime(p.EchangeProduits_DateEchange).ToString("yyyy-MM-dd") == date)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        }
                    }
                }
                else
                {
                    if (pdvF != null)
                    {
                        if (pdvRec != null)
                        {
                            return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID)
                                .Where(p => p.EchangeProduits_PdvFournisseurID == pdvF && p.EchangeProduits_PdvRecepID == pdvRec)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        }
                        else
                        {
                            return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID)
                                .Where(p => p.EchangeProduits_PdvFournisseurID == pdvF)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        }
                    }
                    else
                    {
                        if (pdvRec != null)
                        {
                            return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID)
                                .Where(p => p.EchangeProduits_PdvRecepID == pdvRec)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        }
                        else
                        {
                            return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID && p.EchangeProduits_DateEchange.ToShortDateString() == DateTime.Now.ToShortDateString())
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        }
                    }
                }
            }


        }
        public IEnumerable<Echange_Produits> getListEchangesSortant(int AboID, string statut, string date, int? pdvF, int? pdvRec)
        {
            if (statut != "")
            {
                if (date != "")
                {
                    string newDate = Convert.ToDateTime(date).ToShortDateString();
                    if (pdvRec != null)
                    {

                        return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID && Convert.ToDateTime(p.EchangeProduits_DateEchange).ToString("yyyy-MM-dd") == date)
                                .Where(p => p.EchangeProduits_Statut == statut && p.EchangeProduits_PdvFournisseurID == pdvF && p.EchangeProduits_PdvRecepID == pdvRec)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                    }
                    else
                    {
                        return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID && Convert.ToDateTime(p.EchangeProduits_DateEchange).ToString("yyyy-MM-dd") == date)
                                .Where(p => p.EchangeProduits_Statut == statut && p.EchangeProduits_PdvFournisseurID == pdvF)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                    }


                }
                else
                {
                    if (pdvRec != null)
                    {
                        return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID && p.EchangeProduits_DateEchange.Date == DateTime.Now.Date)
                                .Where(p => p.EchangeProduits_Statut == statut && p.EchangeProduits_PdvFournisseurID == pdvF && p.EchangeProduits_PdvRecepID == pdvRec)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                    }
                    else
                    {
                        return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID && p.EchangeProduits_DateEchange.Date == DateTime.Now.Date)
                                .Where(p => p.EchangeProduits_Statut == statut && p.EchangeProduits_PdvFournisseurID == pdvF)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                    }
                }
            }
            else
            {
                if (date != "")
                {
                    DateTime newDate = Convert.ToDateTime(date);
                    if (pdvRec != null)
                    {
                        return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID && Convert.ToDateTime(p.EchangeProduits_DateEchange).ToString("yyyy-MM-dd") == date)
                                .Where(p =>  p.EchangeProduits_PdvFournisseurID == pdvF && p.EchangeProduits_PdvRecepID == pdvRec)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                    }
                    else
                    {
                        return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID && Convert.ToDateTime(p.EchangeProduits_DateEchange).ToString("yyyy-MM-dd") == date)
                                .Where(p => p.EchangeProduits_PdvFournisseurID == pdvF)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                    }


                }
                else
                {
                    if (pdvRec != null)
                    {
                        return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID && p.EchangeProduits_DateEchange.Date == DateTime.Now.Date)
                                .Where(p => p.EchangeProduits_PdvFournisseurID == pdvF && p.EchangeProduits_PdvRecepID == pdvRec)
                                .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                    }
                    else
                    {
                        return _db.echange_Produits
                            .Where(p => p.EchangeProduits_AbonnementID == AboID && p.EchangeProduits_DateEchange.Date == DateTime.Now.Date)
                            .Where(p => p.EchangeProduits_PdvFournisseurID == pdvF)
                            .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                    }
                }
            }
            
        }   
        public IEnumerable<Echange_Produits> getListEchangesEntrant(int AboID, string statut, string date, int? pdvF, int? pdvRec)
        {
            if (statut != "")
            {
                if (date != "")
                {
                    DateTime newDate = Convert.ToDateTime(date).Date;
                    if (pdvF != null)
                    {
                        return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID && Convert.ToDateTime(p.EchangeProduits_DateEchange).ToString("yyyy-MM-dd") == date)
                            .Where(p => p.EchangeProduits_Statut == statut && p.EchangeProduits_PdvFournisseurID == pdvF && p.EchangeProduits_PdvRecepID == pdvRec)
                            .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        
                       
                    }
                    else
                    {
                        return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID && Convert.ToDateTime(p.EchangeProduits_DateEchange).ToString("yyyy-MM-dd") == date)
                            .Where(p => p.EchangeProduits_Statut == statut && p.EchangeProduits_PdvRecepID == pdvRec)
                            .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        
                    }
                }
                else
                {
                    if (pdvF != null)
                    {

                            return _db.echange_Produits
                            .Where(p => p.EchangeProduits_AbonnementID == AboID && p.EchangeProduits_DateEchange.Date == DateTime.Now.Date)
                            .Where(p => p.EchangeProduits_Statut == statut && p.EchangeProduits_PdvFournisseurID == pdvF && p.EchangeProduits_PdvRecepID == pdvRec)
                            .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                       
                        
                    }
                    else
                    {
                        return _db.echange_Produits
                            .Where(p => p.EchangeProduits_AbonnementID == AboID && p.EchangeProduits_DateEchange.Date == DateTime.Now.Date)
                            .Where(p => p.EchangeProduits_Statut == statut && p.EchangeProduits_PdvRecepID == pdvRec)
                            .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        
                    }
                }
            }
            else
            {
                if (date != "")
                {
                    DateTime newDate = Convert.ToDateTime(date);
                    if (pdvF != null)
                    {
                        return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID && Convert.ToDateTime(p.EchangeProduits_DateEchange).ToString("yyyy-MM-dd") == date)
                            .Where(p => p.EchangeProduits_PdvFournisseurID == pdvF && p.EchangeProduits_PdvRecepID == pdvRec)
                            .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        
                    }
                    else
                    {

                            return _db.echange_Produits
                                .Where(p => p.EchangeProduits_AbonnementID == AboID && Convert.ToDateTime(p.EchangeProduits_DateEchange).ToString("yyyy-MM-dd") == date)
                            .Where(p =>p.EchangeProduits_PdvRecepID == pdvRec)
                            .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                       
                    }
                }
                else
                {
                    if (pdvF != null)
                    {
                        return _db.echange_Produits
                            .Where(p => p.EchangeProduits_AbonnementID == AboID && p.EchangeProduits_DateEchange.ToShortDateString() == DateTime.Now.ToShortDateString())
                            .Where(p => p.EchangeProduits_PdvFournisseurID == pdvF && p.EchangeProduits_PdvRecepID == pdvRec)
                            .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        
                    }
                    else
                    {
                        return _db.echange_Produits
                            .Where(p => p.EchangeProduits_AbonnementID == AboID && p.EchangeProduits_DateEchange.ToShortDateString() == DateTime.Now.ToShortDateString())
                            .Where(p => p.EchangeProduits_PdvRecepID == pdvRec)
                            .Include(p => p.FournisseurPdv).Include(p => p.ReceptionPdv).Include(p => p.User).AsEnumerable();
                        
                    }
                }
            }

        }
            public IEnumerable<EchangeProduit_Details> getListEchangesDetails(int echangeID)
        {
            var res =  _db.echangeProduit_Details.Where(p => p.EchangeProduitDetails_EchangeID == echangeID)
                .Include(p=>p.Echange_Produits)
                .Include(p=>p.Forme_Produit).ThenInclude(p=>p.Produit_Vendable)
                .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                .Include(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage)
                .Include(p=>p.Unite_Mesure)
                .AsEnumerable();
            return res;
        } 
        public IEnumerable<Point_Vente> getListePdvByCateg(int souscategID, int pdv)
        {
            var categID = _db.sousFamilles.Where(p=>p.SousFamille_ID==souscategID).Include(p=>p.Famille_Produit).FirstOrDefault().Famille_Produit.FamilleProduit_Id;
            return _db.pointVente_Familles.Where(p => p.FamilleProduit_Id == categID && p.PointVente_Id != pdv).Include(p=>p.Point_Vente).Select(p => p.Point_Vente).AsEnumerable();
        }
        public decimal findformulaireStockByFormeID(int Id, int pdv, int formeID)
        {
            return _db.pointVente_Stocks.Where(p => p.PointVenteStock_AbonnementID == Id).Where(p => p.PointVenteStock_PointVenteID == pdv && p.PointVenteStock_FormeProduitID== formeID).FirstOrDefault().PointVenteStock_QuantiteProduit;
        }
        public async Task<IEnumerable<ApiUserModel>> getUsersAsync(int pdvId)
        {
            var Users = new List<ApiUserModel>();
            var positions = _db.positionVentes.Where(p => p.PositionVente_PointVenteId == pdvId).AsEnumerable();
            foreach(var pos in positions)
            {
                var res = _db.Users.Where(p => p.PositionVente_ID == pos.PositionVente_Id).FirstOrDefault();
                var roles = await _userManager.GetRolesAsync(res);
                var user = new ApiUserModel()
                {
                    id = res.Id,
                    userName = res.UserName,
                    passeWord = res.PasswordHash,
                    nom = res.Nom,
                    prenom = res.Prenom,
                    email = res.Email,
                    role = roles[0],
                };
                Users.Add(user);
            }
            return Users;
        }
        public async Task<bool> AccepterLivraisonEchange(int echangeID,string validerPar,int pdvID)
        {
            var echange = _db.echange_Produits.Where(p => p.EchangeProduits_ID == echangeID).FirstOrDefault();
            var details = _db.echangeProduit_Details.Where(p => p.EchangeProduitDetails_EchangeID == echangeID)
                .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_Vendable)
                .Include(p => p.Forme_Produit).ThenInclude(p => p.Produit_PretConsomer)
                .Include(p => p.Forme_Produit).ThenInclude(p => p.ProduitPackage).AsEnumerable();
            echange.EchangeProduits_Statut = "Produits reçus";
            echange.EchangeProduits_PdvFournisseurUserID = validerPar;
            foreach(var item in details)
            {
                if (item.Forme_Produit.Produit_Vendable != null)
                {
                    var formeVendable = _db.pointVente_Stocks.Where(p => p.PointVenteStock_FormeProduitID == item.EchangeProduitDetails_FromeID && p.PointVenteStock_PointVenteID == pdvID).FirstOrDefault();
                    if(formeVendable!=null)
                    {
                        formeVendable.PointVenteStock_QuantiteProduit += item.EchangeProduitDetails_Quantite;
                        _db.Entry(formeVendable).State = EntityState.Modified;
                    }
                    else
                    {
                        var stock = new PointVente_Stock()
                        {
                            PointVenteStock_FormeProduitID = item.EchangeProduitDetails_FromeID,
                            PointVenteStock_ProduitID = item.EchangeProduitDetails_ProduitID,
                            PointVenteStock_QuantiteProduit = item.EchangeProduitDetails_Quantite,
                            PointVenteStock_DateModification = DateTime.Now.AddHours(1),
                            PointVenteStock_AbonnementID = echange.EchangeProduits_AbonnementID,
                            PointVenteStock_PointVenteID = pdvID,
                        };
                        await _db.pointVente_Stocks.AddAsync(stock);
                    }
                }
                if (item.Forme_Produit.ProduitPackage != null)
                {
                    var fomrePack = _db.formeDetails.Where(p => p.FormeDetails_FormeProduitID == item.EchangeProduitDetails_FromeID && p.FormeDetails_PointVenteID == pdvID).FirstOrDefault();
                    if(fomrePack!=null)
                    {
                        fomrePack.FormeDetails_Quantite += item.EchangeProduitDetails_Quantite;
                        _db.Entry(fomrePack).State = EntityState.Modified;

                    }
                    else
                    {
                        var stock = new FormeDetails()
                        {
                            FormeDetails_FormeProduitID = item.EchangeProduitDetails_FromeID,
                            FormeDetails_ProduitPackageID = item.EchangeProduitDetails_ProduitID,
                            FormeDetails_Quantite = item.EchangeProduitDetails_Quantite,
                            FormeDetails_DateCreation = DateTime.Now.AddHours(1),
                            FormeDetails_AbonnementID = echange.EchangeProduits_AbonnementID,
                            FormeDetails_PointVenteID = pdvID,
                        };
                        await _db.formeDetails.AddAsync(stock);
                    }
                }
                if (item.Forme_Produit.Produit_PretConsomer != null)
                {
                    var formePret = _db.pdV_ProduitsPrets.Where(p => p.PdV_ProduitsPret_FormeProduitId == item.EchangeProduitDetails_FromeID && p.PdV_ProduitsPret_PointVenteId == pdvID).FirstOrDefault();
                    if (formePret != null)
                    {
                        formePret.PdV_ProduitsPret_Quantite += item.EchangeProduitDetails_Quantite;
                        _db.Entry(formePret).State = EntityState.Modified;

                    }
                    else
                    {
                        var stock = new PdV_ProduitsPret()
                        {
                            PdV_ProduitsPret_FormeProduitId = item.EchangeProduitDetails_FromeID,
                            PdV_ProduitsPret_ProduitConsomableId = item.EchangeProduitDetails_ProduitID,
                            PdV_ProduitsPret_Quantite = item.EchangeProduitDetails_Quantite,
                            PdV_ProduitsPret_DateModification = DateTime.Now.AddHours(1),
                            PdV_ProduitsPret_AbonnementId = echange.EchangeProduits_AbonnementID,
                            PdV_ProduitsPret_PointVenteId = pdvID,
                        };
                        await _db.pdV_ProduitsPrets.AddAsync(stock);
                    }
                }
            }
            _db.Entry(echange).State = EntityState.Modified;
            await unitOfWork.Complete();
            return true;
        }
        public async Task<bool> AccepterOrdreEchange(int echangeID,string validerPar,int pdvID)
        {
            var echange = _db.echange_Produits.Where(p => p.EchangeProduits_ID == echangeID).Include(p => p.details).FirstOrDefault();
            echange.EchangeProduits_PdvRecepUserID = validerPar;
            echange.EchangeProduits_Statut = "En cours de livraison";
            foreach (var item in echange.details)
            {
                var formeVendable = _db.pointVente_Stocks.Where(p => p.PointVenteStock_FormeProduitID == item.EchangeProduitDetails_FromeID && p.PointVenteStock_PointVenteID == pdvID).FirstOrDefault();
                var formePret = _db.pdV_ProduitsPrets.Where(p => p.PdV_ProduitsPret_FormeProduitId == item.EchangeProduitDetails_FromeID && p.PdV_ProduitsPret_PointVenteId == pdvID).FirstOrDefault();
                var fomrePack = _db.formeDetails.Where(p => p.FormeDetails_FormeProduitID == item.EchangeProduitDetails_FromeID && p.FormeDetails_PointVenteID == pdvID).FirstOrDefault();
                if (formeVendable != null)
                {
                    var qte = formeVendable.PointVenteStock_QuantiteProduit - item.EchangeProduitDetails_Quantite;
                    if (qte < 0)
                        return false;
                    else
                    {
                        formeVendable.PointVenteStock_QuantiteProduit = qte;
                        _db.Entry(formeVendable).State = EntityState.Modified;
                    }
                }
                else if (formePret != null)
                {
                    var qte = formePret.PdV_ProduitsPret_Quantite - item.EchangeProduitDetails_Quantite;
                    if (qte < 0)
                        return false;
                    else
                    {
                        formePret.PdV_ProduitsPret_Quantite = qte;
                        _db.Entry(formePret).State = EntityState.Modified;
                    }
                }
                else
                {
                    var qte = fomrePack.FormeDetails_Quantite - item.EchangeProduitDetails_Quantite;
                    if (qte < 0)
                        return false;
                    else
                    {
                        fomrePack.FormeDetails_Quantite = qte;
                        _db.Entry(fomrePack).State = EntityState.Modified;
                    }
                }

            }
            _db.Entry(echange).State = EntityState.Modified;
            await unitOfWork.Complete();
            return true;
        }
    }
}
