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
    public class GestionMouvementService : IGestionMouvementService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IGestionMouvementRepository gestionMouvementRepository;
        public GestionMouvementService(IMapper mapper, IUnitOfWork unitOfWork, IGestionMouvementRepository gestionMouvementRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.gestionMouvementRepository = gestionMouvementRepository;

        }

        public async Task<bool> AlimentationStock(MouvementStockModel mouvementStock)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    MouvementStock mouvement = mapper.Map<MouvementStockModel, MouvementStock>(mouvementStock);
                    int? idlouvelent = await gestionMouvementRepository.AlimentationStock(mouvement);
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

        public async Task<bool> CreateTypeMouvement(MouvementTypeModel typeMouvement)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    MouvementType mouvement = mapper.Map<MouvementTypeModel, MouvementType>(typeMouvement);
                    var idlouvelent = await this.gestionMouvementRepository.CreateTypeMouvement(mouvement);


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

        public Task<bool> deleteMouvement(int ID)
        {
            return this.gestionMouvementRepository.deleteMouvement(ID);
        }

        public MouvementTypeModel findFormulaireMouvement(int formulaireMouvementId)
        {
            return mapper.Map<MouvementType, MouvementTypeModel>(this.gestionMouvementRepository.findFormulaireMouvement(formulaireMouvementId));

        }

        public IEnumerable<FournisseurModel> getListFournisseur(int Id)
        {
            return mapper.Map<IEnumerable<Fournisseur>, IEnumerable<FournisseurModel>>(source: gestionMouvementRepository.getListFournisseur(Id));
        }

        public IEnumerable<MatierePremiereModel> getListMatierePremiere(int Id)
        {
            return mapper.Map<IEnumerable<MatierePremiere>, IEnumerable<MatierePremiereModel>>(source: gestionMouvementRepository.getListMatierePremiere(Id));
        }

        public IEnumerable<MatierePremiereStockageModel> getListMatiereStockage(int Id, int aboId, int adresse)
        {
            var result = mapper.Map<IEnumerable<MatierePremiereStockage>, IEnumerable<MatierePremiereStockageModel>>(gestionMouvementRepository.getListMatiereStockage(Id,aboId, adresse));
            return result;
        }

        public IEnumerable<MouvementTypeModel> getListMouvement(int Id)
        {
            return mapper.Map<IEnumerable<MouvementType>, IEnumerable<MouvementTypeModel>>(this.gestionMouvementRepository.getListMouvement(Id));
        }

        public Task<bool> updateFormulaireMouvement(int id, MouvementTypeModel newMouvement)
        {
            MouvementType mouvement = mapper.Map<MouvementTypeModel, MouvementType>(newMouvement);
            return this.gestionMouvementRepository.updateFormulaireMouvement(id, mouvement);
        }


        public async Task<bool> SendStock(MouvementStockModel mouvementStock, int userlieu)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    MouvementStock mouvement = mapper.Map<MouvementStockModel, MouvementStock>(mouvementStock);
                    int? idlouvelent = await gestionMouvementRepository.SendStock(mouvement, userlieu);
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

        public IEnumerable<MatierePremiereStockageModel> getListMatiereStockageAll(int aboId ,int userlieu)
        {
            return mapper.Map<IEnumerable<MatierePremiereStockage>, IEnumerable<MatierePremiereStockageModel>>(this.gestionMouvementRepository.getListMatiereStockageAll(aboId, userlieu));
        }

        public async Task<bool> ReceptionProduitsPretConso(MouvementProduitsConsoModel mouvementStock)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    MouvementProduitsConso mouvement = mapper.Map<MouvementProduitsConsoModel, MouvementProduitsConso>(mouvementStock);
                    int? idlouvelent = await gestionMouvementRepository.ReceptionProduitsPretConso(mouvement);
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

        public IEnumerable<ProduitConsomableStokageModel> getProduitPretConsomers(int Id, int aboId, int atelierUserId)
        {
            return mapper.Map<IEnumerable<ProduitConsomableStokage>, IEnumerable<ProduitConsomableStokageModel>>(gestionMouvementRepository.getProduitPretConsomers(Id,aboId,atelierUserId));
        }
        public IEnumerable<ProduitConsomableStokageModel> getProduitPretConsomersByPorduit(int Id, int aboId, int atelierUserId,int produitId)
        {
            return mapper.Map<IEnumerable<ProduitConsomableStokage>, IEnumerable<ProduitConsomableStokageModel>>(gestionMouvementRepository.getProduitPretConsomersByPorduit(Id,aboId,atelierUserId,produitId));
        }

        public IEnumerable<FournisseurProduitsModel> getListFournisseurProduits(int Id)
        {
            return mapper.Map<IEnumerable<FournisseurProduits>, IEnumerable<FournisseurProduitsModel>>(gestionMouvementRepository.getListFournisseurProduits(Id));
        }

        public IEnumerable<MouvementProduitsConsoModel> getListMouvementsProduits(int Id, int atelierID, int? produit, string date)
        {
            return mapper.Map<IEnumerable<MouvementProduitsConso>, IEnumerable<MouvementProduitsConsoModel>>(gestionMouvementRepository.getListMouvementsProduits(Id,atelierID,produit,date));
        }

        public ProduitConsomableStokageModel findformulaireProduitStock(int Id, int aboId)
        {
            return mapper.Map<ProduitConsomableStokage, ProduitConsomableStokageModel>(gestionMouvementRepository.findformulaireProduitStock(Id, aboId));
        }

        public IEnumerable<MatierePremiereStockageModel> getListMatiereStockageAdmin(int aboId)
        {
            return mapper.Map<IEnumerable<MatierePremiereStockage>, IEnumerable<MatierePremiereStockageModel>>(this.gestionMouvementRepository.getListMatiereStockageAdmin(aboId));
        }

        public IEnumerable<ProduitConsomableStokageModel> getProduitEnStockByProduit(int produitId, int atelierId)
        {
            return mapper.Map<IEnumerable<ProduitConsomableStokage>, IEnumerable<ProduitConsomableStokageModel>>(gestionMouvementRepository.getProduitEnStockByProduit(produitId,atelierId));

        }

        public IEnumerable<MouvementStockModel> getlistMouvements(int aboId, int? lieuExpe, int? lieuReception, int? matiere, string date)
        {
            return mapper.Map<IEnumerable<MouvementStock>, IEnumerable<MouvementStockModel>>(gestionMouvementRepository.getlistMouvements(aboId, lieuExpe, lieuReception, matiere, date));
        }

        public async Task<bool> ReceptionStock(Reception_StockModel receptioModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    Reception_Stock reception = mapper.Map<Reception_StockModel, Reception_Stock>(receptioModel);
                    int? idlouvelent = await gestionMouvementRepository.ReceptionStock(reception);
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

        public IEnumerable<MatierePremiereModel> getListMatierePremieresBC(int Id, int aboId)
        {
            var result = mapper.Map<IEnumerable<MatierePremiere>, IEnumerable<MatierePremiereModel>>(gestionMouvementRepository.getListMatierePremieresBC(Id, aboId));
            return result;
        }
    }
}
