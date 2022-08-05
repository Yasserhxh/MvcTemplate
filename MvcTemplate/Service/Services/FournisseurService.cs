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
    public class FournisseurService : IFournisseurService
    {
        private readonly IFournisseurRepository fournisseurRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public FournisseurService(IFournisseurRepository fournisseurRepository, IMapper mapper,
          IUnitOfWork unitOfWork)
        {
            this.fournisseurRepository = fournisseurRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;

        }

        public async Task<bool?> AjouterMatiere(int idFournisseur, List<int> listMatiere)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Creer le fournisseur.
                    var id = await fournisseurRepository.AjouterMatiere(idFournisseur, listMatiere);


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

        public async Task<bool> CreateFournisseur(FournisseurModel fournisseurModel)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    // Creer le fournisseur.
                    Fournisseur fournisseur = mapper.Map<FournisseurModel, Fournisseur>(fournisseurModel);
                    var idFournisseur = await this.fournisseurRepository.CreateFournisseur(fournisseur);


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

        public Task<bool> deleteFournisseur(int ID, int code)
        {
            return this.fournisseurRepository.deleteFournisseur(ID, code);
        }

        public Task<bool> deleteMatieresLink(int ID, int code)
        {
            return this.fournisseurRepository.deleteMatieresLink(ID, code);
        }

        public FournisseurModel findFormulaireFournisseur(int formulaireFourisseurId)
        {
            return mapper.Map<Fournisseur, FournisseurModel>(fournisseurRepository.findFormulaireFournisseur(formulaireFourisseurId));
        }

        public IEnumerable<VilleModel> getListeVille()
        {
            return mapper.Map<IEnumerable<Ville>, IEnumerable<VilleModel>>(fournisseurRepository.getListVilles());
        }

        public IEnumerable<FonctionModel> getListFonction()
        {
            return mapper.Map<IEnumerable<Fonction>, IEnumerable<FonctionModel>>(fournisseurRepository.getListFonction());
        }

        public IEnumerable<FournisseurModel> getListFournisseur(int Id,int? VilleId, int? statut)
        {
            return mapper.Map<IEnumerable<Fournisseur>, IEnumerable<FournisseurModel>>(this.fournisseurRepository.getListFournisseur(Id,VilleId,statut));

        }

        public IEnumerable<MatierePremiereModel> getListMatiere(int Id)
        {
            return mapper.Map<IEnumerable<MatierePremiere>, IEnumerable<MatierePremiereModel>>(fournisseurRepository.getListMatiere(Id));

        }

        public IEnumerable<FournisseurMatiereModel> getListMatiereLink(int fournisseurId)
        {
            return mapper.Map<IEnumerable<FournisseurMatiere>, IEnumerable<FournisseurMatiereModel>>(fournisseurRepository.getListMatiereLink(fournisseurId));
        }

        /*  public IEnumerable<FournisseurModel> getListAllFournisseur()
 {
     return mapper.Map<IEnumerable<Fournisseur>, IEnumerable<FournisseurModel>>(this.fournisseurRepository.getListAllFournisseur());

 }*/

        public Task<bool> updateFormulaireFournisseur(int id, FournisseurModel newFournisseuModel)
        {
            Fournisseur fournisseur = mapper.Map<FournisseurModel, Fournisseur>(newFournisseuModel);
            return this.fournisseurRepository.updateFormulaireFournisseur(id, fournisseur);

        }
    }
}
