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
    public class FamilleProduitService : IFamilleProduitService
    {
        private readonly IFamilleProduitRepository familleProduitRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public FamilleProduitService(IFamilleProduitRepository familleProduitRepository, IMapper mapper,
          IUnitOfWork unitOfWork)
        {
            this.familleProduitRepository = familleProduitRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateFamille(FamilleProduitModel familleProduitModel)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    // Creer la famille.
                    FamilleProduit familleProduit = mapper.Map<FamilleProduitModel, FamilleProduit>(familleProduitModel);
                    var idEntreprise = await this.familleProduitRepository.CreateFamille(familleProduit);


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

        public Task<bool> deleteFormulaireFamille(int ID)
        {
            return familleProduitRepository.deleteFormulaireFamille(ID);
        }

        public FamilleProduitModel findFormulaireFamille(int formulairefamilleProduitId)
        {
            return mapper.Map<FamilleProduit, FamilleProduitModel>(familleProduitRepository.findFormulaireFamille(formulairefamilleProduitId));

        }

        public IEnumerable<FamilleProduitModel> getListFamilles(int Id)
        {
            return mapper.Map<IEnumerable<FamilleProduit>, IEnumerable<FamilleProduitModel>>(this.familleProduitRepository.getListFamilles(Id));

        }

        public Task<bool> updateFormulaireFamille(int id, FamilleProduitModel newfamilleProduitModel)
        {
            FamilleProduit famille = mapper.Map<FamilleProduitModel, FamilleProduit>(newfamilleProduitModel);
            return familleProduitRepository.updateFormulaireFamille(id, famille);
        }

        public IEnumerable<FamilleProduitModel> getListAllFamilles()
        {

            return mapper.Map<IEnumerable<FamilleProduit>, IEnumerable<FamilleProduitModel>>(familleProduitRepository.getListAllFamilles());

        }

        public async Task<bool> CreatSousFamille(SousFamilleModel sousFamilleModel)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    // Creer la famille.
                    SousFamille sousFamille = mapper.Map<SousFamilleModel, SousFamille>(sousFamilleModel);
                    var idEntreprise = await this.familleProduitRepository.CreatSousFamille(sousFamille);


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

        public IEnumerable<SousFamilleModel> getListSousFamilles(int? Id, int aboID)
        {
            return mapper.Map<IEnumerable<SousFamille>, IEnumerable<SousFamilleModel>>(familleProduitRepository.getListSousFamilles(Id, aboID));
        }

        public IEnumerable<FamilleProduitModel> getListFamillesByPdv(int Id, int pdv)
        {
            return mapper.Map<IEnumerable<FamilleProduit>, IEnumerable<FamilleProduitModel>>(familleProduitRepository.getListFamillesByPdv(Id, pdv));
        }
    }
}
