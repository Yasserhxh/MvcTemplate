using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.IRepositories;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services
{
    public class PointVenteService : IPointVenteService
    {
        private readonly IPointVenteRespository pointVenteRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public PointVenteService(IPointVenteRespository pointVenteRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.pointVenteRepository = pointVenteRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool?> AjouterFamille(int pointVenteId, List<int> ListFamille)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Creer le fournisseur.
                    var id = await pointVenteRepository.AjouterFamille(pointVenteId, ListFamille);
                    if (id == null)
                        return null;
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> AjouterSalle(int id, SalleModel salle)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    Salle salle1 = mapper.Map<SalleModel, Salle>(salle);
                    var idPointVente = await pointVenteRepository.AjouterSalle(id,salle1);


                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> AjouterTable(int id, List<TableModel> table)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer le produit
                    var produit = mapper.Map<IEnumerable<TableModel>, IEnumerable<Table>>(table);
                    var idprix = await pointVenteRepository.AjouterTable(id, produit);

                    transaction.Commit();
                    return true;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> CreateAgentServeur(AgentServeurModel agentserveur)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Creer le point de vente.
                    AgentServeur agent = mapper.Map<AgentServeurModel, AgentServeur>(agentserveur);
                    var idPointVente = await pointVenteRepository.CreateAgentServeur(agent);


                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> CreatePointVente(Point_VenteModel point_VenteModel)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    // Creer le point de vente.
                    Point_Vente point_Vente = mapper.Map<Point_VenteModel, Point_Vente>(point_VenteModel);
                    var idPointVente = await this.pointVenteRepository.CreatePointVent(point_Vente);


                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> CreatePositionVente(PositionVenteModel position)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Creer le point de vente.
                    PositionVente pos = mapper.Map<PositionVenteModel, PositionVente>(position);
                    var idPointVente = await pointVenteRepository.CreatePositionVente(pos);


                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public Task<bool> deletePointVente(int ID, int code)
        {
            return this.pointVenteRepository.deletePointVente(ID, code);
        }

        public Point_VenteModel findFormulairePointVente(int formulairePointVenteId)
        {
            return mapper.Map<Point_Vente, Point_VenteModel>(this.pointVenteRepository.findFormulairePointVente(formulairePointVenteId));
        }

        public PositionVenteModel findFormulairePositionVente(int formulairePoseId)
        {
            return mapper.Map<PositionVente, PositionVenteModel>(pointVenteRepository.findFormulairePositionVente(formulairePoseId));
        }

        public IEnumerable<AgentServeurModel> getListAgents(int Id)
        {
            return mapper.Map<IEnumerable<AgentServeur>, IEnumerable<AgentServeurModel>>(pointVenteRepository.getListAgents(Id));
        }

        public IEnumerable<FamilleProduitModel> getListFamilles(int Id)
        {
            return mapper.Map<IEnumerable<FamilleProduit>, IEnumerable<FamilleProduitModel>>(pointVenteRepository.getListFamilles(Id));
        }

        public IEnumerable<ModePaiementModel> getListModePaiement()
        {
            return mapper.Map<IEnumerable<ModePaiement>, IEnumerable<ModePaiementModel>>(pointVenteRepository.getListModePaiement());
        }

        public IEnumerable<Point_VenteModel> getListPointVente(int Id,int? type,int? statut)
        {
            return mapper.Map<IEnumerable<Point_Vente>, IEnumerable<Point_VenteModel>>(pointVenteRepository.getLisPoinVent(Id,type,statut));
        }
        public IEnumerable<Point_VenteModel> getListPointVenteActive(int Id)
        {
            return mapper.Map<IEnumerable<Point_Vente>, IEnumerable<Point_VenteModel>>(pointVenteRepository.getLisPoinVentActive(Id));
        }
        public IEnumerable<PointVenteApi> getListPointVenteActiveApi(int Id)
        {
            var pdv = mapper.Map<IEnumerable<Point_Vente>, IEnumerable<Point_VenteModel>>(pointVenteRepository.getLisPoinVentActive(Id));
            var pdvViewApi = new List<PointVenteApi>();
            foreach(var p in pdv)
            {
                var pdvApi = new PointVenteApi()
                {
                    PointVente_Id = p.PointVente_Id,
                    PointVente_Nom = p.PointVente_Nom,
                    PositionsVente = new List<PositionVenteApi>()
                };
                foreach(var pos in p.PositionsVente)
                {
                    var posApi = new PositionVenteApi()
                    {
                        PositionVente_Id = pos.PositionVente_Id,
                        PositionVente_Libelle = pos.PositionVente_Libelle
                    };
                    pdvApi.PositionsVente.Add(posApi);
                }
                pdvViewApi.Add(pdvApi);
            }
            return pdvViewApi;
        }

        public IEnumerable<PositionVenteModel> getListPositionVente(int Id)
        {
            return mapper.Map<IEnumerable<PositionVente>, IEnumerable<PositionVenteModel>>(pointVenteRepository.getListPositionVente(Id));
        }

        public IEnumerable<ProduitApproModel> getListProduitVendable(int Id, int AboId)
        {
            return mapper.Map<IEnumerable<ProduitAppro>, IEnumerable<ProduitApproModel>>(pointVenteRepository.getListProduitVendable(Id,AboId));
        }

        public IEnumerable<SalleModel> getListSalles(int Id)
        {
            return mapper.Map<IEnumerable<Salle>, IEnumerable<SalleModel>>(pointVenteRepository.getListSalles(Id));
        }

        public IEnumerable<TableModel> getListTables(int Id)
        {
            return mapper.Map<IEnumerable<Table>, IEnumerable<TableModel>>(pointVenteRepository.getListTables(Id));
        }

        public IEnumerable<VilleModel> getListVilles()
        {
            return mapper.Map<IEnumerable<Ville>, IEnumerable<VilleModel>>(pointVenteRepository.getListVilles());
        }

        public IEnumerable<PointVente_StockModel> getStockPointVente(int Id, int pdv)
        {
            return mapper.Map<IEnumerable<PointVente_Stock>, IEnumerable<PointVente_StockModel>>(pointVenteRepository.getStockPointVente(Id, pdv));
        }
        public IEnumerable<PointVente_StockModel> getStockPointVenteByProduit(int Id, int pdv, int produitId)
        {
            return mapper.Map<IEnumerable<PointVente_Stock>, IEnumerable<PointVente_StockModel>>(pointVenteRepository.getStockPointVenteByProduit(Id, pdv, produitId));
        }     
        public IEnumerable<PointVente_StockModel> getStockPointVenteAvecPlan(int Id, int? pdv)
        {
            return mapper.Map<IEnumerable<PointVente_Stock>, IEnumerable<PointVente_StockModel>>(pointVenteRepository.getStockPointVenteAvecPlan(Id, pdv));
        }

        public IEnumerable<PointVente_StockModel> getStockPointVenteAll(int Id)
        {
            return mapper.Map<IEnumerable<PointVente_Stock>, IEnumerable<PointVente_StockModel>>(pointVenteRepository.getStockPointVenteAll(Id));
        }

        public IEnumerable<ProduitPackageModel> getStockProduitPackage(int Id, int pdv)
        {
            return mapper.Map<IEnumerable<ProduitPackage>, IEnumerable<ProduitPackageModel>>(pointVenteRepository.getStockProduitPackage(Id, pdv));
        }
        public IEnumerable<ProduitPackageModel> getStockProduitPackageAll(int Id)
        {
            return mapper.Map<IEnumerable<ProduitPackage>, IEnumerable<ProduitPackageModel>>(pointVenteRepository.getStockProduitPackageAll(Id));
        }

        public Task<bool> updateFormulairePointVente(int id, Point_VenteModel newPointVenteModel)
        {
            Point_Vente point_Vente = mapper.Map<Point_VenteModel, Point_Vente>(newPointVenteModel);
            return this.pointVenteRepository.updateFormulairePointVente(id, point_Vente);
        }

        public IEnumerable<PointVente_FamilleModel> getListFammilesPointVente(int pdvId)
        {
            return mapper.Map<IEnumerable<PointVente_Famille>, IEnumerable<PointVente_FamilleModel>>(pointVenteRepository.getListFammilesPointVente(pdvId));
        }

        public Task<bool> deleteFamillePdv(int id, int code)
        {
            return this.pointVenteRepository.deleteFamillePdv(id, code);
        }

        public IEnumerable<AtelierModel> getListAtelier(int Id)
        {
            return mapper.Map<IEnumerable<Atelier>, IEnumerable<AtelierModel>>(pointVenteRepository.getListAtelier(Id));

        }

        public IEnumerable<Lieu_StockageModel> getListPointStock(int Id)
        {
            return mapper.Map<IEnumerable<Lieu_Stockage>, IEnumerable<Lieu_StockageModel>>(pointVenteRepository.getListPointStock(Id));
        }

        public SalleModel findFormulaireSalle(int formulaireSalleId)
        {
            return mapper.Map<Salle, SalleModel>(pointVenteRepository.findFormulaireSalle(formulaireSalleId));
        }

        public async Task<bool?> AjouterUtilisateur(int idPointVente, string responsableId)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    var id = await pointVenteRepository.AjouterUtilisateur(idPointVente, responsableId);
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public IEnumerable<PointVente_UserModel> getListPdvUser(string UserId)
        {
            return mapper.Map<IEnumerable<PointVente_User>, IEnumerable<PointVente_UserModel>>(pointVenteRepository.getListPdvUser(UserId));

        }

        public IEnumerable<ProduitPack_AtelierModel> getListProduitPack(int Id, int AboId)
        {
            return mapper.Map<IEnumerable<ProduitPack_Atelier>, IEnumerable<ProduitPack_AtelierModel>>(pointVenteRepository.getListProduitPack(Id, AboId));
        }

        public IEnumerable<Forme_ProduitModel> getStockFormePackage(int Id, int pdv)
        {
            return mapper.Map<IEnumerable<Forme_Produit>, IEnumerable<Forme_ProduitModel>>(pointVenteRepository.getStockFormePackage(Id, pdv));
        }

        public async Task<bool> CreateEchange(Echange_ProduitsModel echange_ProduitsModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    Echange_Produits echange_Produits = mapper.Map<Echange_ProduitsModel, Echange_Produits>(echange_ProduitsModel);
                    var id = await pointVenteRepository.CreateEchange(echange_Produits);
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public IEnumerable<Echange_ProduitsModel> getListEchanges(int AboID, string statut, string date, int? pdvF, int? pdvRec)
        {
            var res = pointVenteRepository.getListEchanges(AboID, statut, date, pdvF, pdvRec);
            return mapper.Map<IEnumerable<Echange_Produits>, IEnumerable<Echange_ProduitsModel>>(res);
        }

        public IEnumerable<EchangeProduit_DetailsModel> getListEchangesDetails(int echangeID)
        {
            return mapper.Map<IEnumerable<EchangeProduit_Details>, IEnumerable<EchangeProduit_DetailsModel>>(pointVenteRepository.getListEchangesDetails(echangeID));
        }

        public IEnumerable<Point_VenteModel> getListePdvByCateg(int souscategID, int pdv)
        {
            var res = pointVenteRepository.getListePdvByCateg(souscategID, pdv);
            return mapper.Map<IEnumerable<Point_Vente>, IEnumerable<Point_VenteModel>>(res);
        }

        public decimal findformulaireStockByFormeID(int Id, int pdv, int formeID)
        {
            return this.pointVenteRepository.findformulaireStockByFormeID(Id, pdv, formeID);
        }

        public async Task<IEnumerable<ApiUserModel>> getUsersAsync(int pdvId)
        {
            var users = await this.pointVenteRepository.getUsersAsync(pdvId);
            return users;
        }

        public async Task<bool?> AccepterOrdreEchange(int echangeID, string validerPar, int pdvID)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    var id = await pointVenteRepository.AccepterOrdreEchange(echangeID, validerPar, pdvID);
                    if (id == false)
                        return null;
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public async Task<bool?> AccepterLivraisonEchange(int echangeID, string validerPar, int pdvID)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    var id = await pointVenteRepository.AccepterLivraisonEchange(echangeID, validerPar, pdvID);
                    if (id == false)
                        return null;
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public IEnumerable<Echange_ProduitsModel> getListEchangesEntrant(int AboID, string statut, string date, int? pdvF, int? pdvRec)
        {
            var res = pointVenteRepository.getListEchangesEntrant(AboID, statut, date, pdvF, pdvRec);
            return mapper.Map<IEnumerable<Echange_Produits>, IEnumerable<Echange_ProduitsModel>>(res);
        }

        public IEnumerable<Echange_ProduitsModel> getListEchangesSortant(int AboID, string statut, string date, int? pdvF, int? pdvRec)
        {
            var res = pointVenteRepository.getListEchangesSortant(AboID, statut, date, pdvF, pdvRec);
            return mapper.Map<IEnumerable<Echange_Produits>, IEnumerable<Echange_ProduitsModel>>(res);
        }
    }
}
