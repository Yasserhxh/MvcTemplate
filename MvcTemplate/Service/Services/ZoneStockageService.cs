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
    public class ZoneStockageService : IZoneStockageService
    {
        private readonly IZoneStockageRepository zoneStockageRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAuthentificationRepository authentificationRepository;
        public ZoneStockageService(IZoneStockageRepository zoneStockageRepository, IMapper mapper, IUnitOfWork unitOfWork, IAuthentificationRepository authentificationRepository)
        {
            this.zoneStockageRepository = zoneStockageRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.authentificationRepository = authentificationRepository;
        }

        public async Task<bool> CreatFormeStockage(Forme_StockageModel formeModel)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {

                    Forme_Stockage forme = mapper.Map<Forme_StockageModel, Forme_Stockage>(formeModel);
                    var idZoneStockage = await this.zoneStockageRepository.CreateFormeStockage(forme);

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

        public async Task<bool> CreatLieuStockage(Lieu_StockageModel lieuModel)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {

                    Lieu_Stockage lieu = mapper.Map<Lieu_StockageModel, Lieu_Stockage>(lieuModel);
                    var idZoneStockage = await this.zoneStockageRepository.CreateLieuStockage(lieu);

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

        public async Task<bool> CreatSectionStockage(Section_StockageModel sectionModel)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {

                    Section_Stockage section = mapper.Map<Section_StockageModel, Section_Stockage>(sectionModel);
                    var idZoneStockage = await this.zoneStockageRepository.CreateSectionStockage(section);

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

        public async Task<bool> CreatZoneStockage(Zone_StockageModel zoneModel)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    // Créer la zone
                    Zone_Stockage zone = mapper.Map<Zone_StockageModel, Zone_Stockage>(zoneModel);
                    var idZoneStockage = await this.zoneStockageRepository.CreateZoneStockage(zone);

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

        public Task<bool> deleteFormeStockage(int ID)
        {
            return this.zoneStockageRepository.deleteFormeStockage(ID);
        }

        public Task<bool> deleteLieuStockage(int ID, int code)
        {
            return this.zoneStockageRepository.deleteLieuStockage(ID, code);
        }

        public Task<bool> deleteSectionStockage(int ID, int code)
        {
            return this.zoneStockageRepository.deleteSectionStockage(ID, code);
        }

        public Task<bool> deleteZoneStockage(int ID, int code)
        {
            return this.zoneStockageRepository.deleteZoneStockage(ID, code);
        }

        public Forme_StockageModel findFormulaireFormeStockage(int formulaireformeId)
        {
            return mapper.Map<Forme_Stockage, Forme_StockageModel>(this.zoneStockageRepository.findFormulaireFormeStockage(formulaireformeId));
        }

        public Lieu_StockageModel findFormulaireLieuStockage(int formulaireLieuId)
        {
            return mapper.Map<Lieu_Stockage, Lieu_StockageModel>(this.zoneStockageRepository.findFormulaireLieuStockage(formulaireLieuId));
        }

        public Section_StockageModel findFormulaireSectionStockage(int formulaireSectionId)
        {
            return mapper.Map<Section_Stockage, Section_StockageModel>(this.zoneStockageRepository.findFormulaireSectionStockage(formulaireSectionId));
        }

        public Zone_StockageModel findFormulaireZoneStockage(int formulaireZoneId)
        {
            return mapper.Map<Zone_Stockage, Zone_StockageModel>(this.zoneStockageRepository.findFormulaireZoneStockage(formulaireZoneId));
        }


        public IEnumerable<Forme_StockageModel> getListFormeSotckage(int Id)
        {
            return mapper.Map<IEnumerable<Forme_Stockage>, IEnumerable<Forme_StockageModel>>(zoneStockageRepository.getListFormeSotckage(Id));
        }

        public IEnumerable<Lieu_StockageModel> getListLieuStockage(int Id,int?statut)
        {
            return mapper.Map<IEnumerable<Lieu_Stockage>, IEnumerable<Lieu_StockageModel>>(zoneStockageRepository.getListLieuStockage(Id,statut));
        } 
        public IEnumerable<Lieu_StockageModel> getListLieuStockageActive(int Id, int lieuId)
        {
            return mapper.Map<IEnumerable<Lieu_Stockage>, IEnumerable<Lieu_StockageModel>>(zoneStockageRepository.getListLieuStockageActive(Id, lieuId));
        }

        public IEnumerable<Section_StockageModel> getListSection(int zoneId)
        {
            return mapper.Map<IEnumerable<Section_Stockage>, IEnumerable<Section_StockageModel>>(zoneStockageRepository.getListSection(zoneId));

        }
        public IEnumerable<Zone_StockageModel> getListZones(int lieuId)
        {
            return mapper.Map<IEnumerable<Zone_Stockage>, IEnumerable<Zone_StockageModel>>(this.zoneStockageRepository.getListZones(lieuId));

        }

        public IEnumerable<Section_StockageModel> getListSectionSotckage(int current, int? zoneID)
        {
            return mapper.Map<IEnumerable<Section_Stockage>, IEnumerable<Section_StockageModel>>(this.zoneStockageRepository.getLisTSectionSotckage(current, zoneID));
        }

        public IEnumerable<Type_ContenuModel> getListTypeContenu()
        {
            return mapper.Map<IEnumerable<Type_Contenu>, IEnumerable<Type_ContenuModel>>(this.zoneStockageRepository.getListTypeContenu());
        }

        public IEnumerable<Unite_MesureModel> getListUniteMesure()
        {
            return mapper.Map<IEnumerable<Unite_Mesure>, IEnumerable<Unite_MesureModel>>(this.zoneStockageRepository.getListUniteMesure());
        }
        public IEnumerable<Unite_CategorieModel> getListUniteCategorie()
        {
            return mapper.Map<IEnumerable<Unite_Categorie>, IEnumerable<Unite_CategorieModel>>(this.zoneStockageRepository.getListUniteCategorie());
        }

        public IEnumerable<Zone_StockageModel> getListZoneStockage(int Id)
        {
            return mapper.Map<IEnumerable<Zone_Stockage>, IEnumerable<Zone_StockageModel>>(this.zoneStockageRepository.getListZoneStockage(Id));
        }



        public Task<bool> updateFormulaireFormeStockage(int id, Forme_StockageModel forme_StockageModel)
        {
            Forme_Stockage forme = mapper.Map<Forme_StockageModel, Forme_Stockage>(forme_StockageModel);
            return this.zoneStockageRepository.updateFormulaireFormeStockage(id, forme);
        }

        public Task<bool> updateFormulaireLieuStockage(int id, Lieu_StockageModel lieu_StockageModel)
        {
            Lieu_Stockage lieu = mapper.Map<Lieu_StockageModel, Lieu_Stockage>(lieu_StockageModel);
            return this.zoneStockageRepository.updateFormulaireLieuStockage(id, lieu);
        }

        public Task<bool> updateFormulaireSectionStockage(int id, Section_StockageModel section_StockageModel)
        {
            Section_Stockage section = mapper.Map<Section_StockageModel, Section_Stockage>(section_StockageModel);
            return this.zoneStockageRepository.updateFormulaireSectionStockage(id, section);
        }

        public Task<bool> updateFormulaireZoneStockage(int id, Zone_StockageModel zone_StockageModel)
        {
            Zone_Stockage zone = mapper.Map<Zone_StockageModel, Zone_Stockage>(zone_StockageModel);
            return this.zoneStockageRepository.updateFormulaireZoneStockage(id, zone);
        }

        public IEnumerable<VilleModel> getlistVille()
        {
            return mapper.Map<IEnumerable<Ville>, IEnumerable<VilleModel>>(zoneStockageRepository.getlistVille());
        }

        public async Task<bool?> AjouterUtilisateur(int idStock, string responsableId)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    var id = await zoneStockageRepository.AjouterUtilisateur(idStock, responsableId);
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

        public IEnumerable<Stock_UserModel> getListStockUser(string UserId)
        {
            return mapper.Map<IEnumerable<Stock_User>, IEnumerable<Stock_UserModel>>(zoneStockageRepository.getListLieuUser(UserId));
        }
    }
}
