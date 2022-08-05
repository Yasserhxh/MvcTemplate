using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.IRepositories;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProduitFicheTechniqueService : IProduitFicheTechniqueService
    {
        private readonly IProduitFicheTechniqueRepository produitFicheTechniqueRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAuthentificationRepository authentificationRepository;


        public ProduitFicheTechniqueService(IProduitFicheTechniqueRepository produitFicheTechniqueRepository, IMapper mapper, IUnitOfWork unitOfWork, IAuthentificationRepository authentificationRepository)
        {
            this.produitFicheTechniqueRepository = produitFicheTechniqueRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.authentificationRepository = authentificationRepository;
        }

        public async Task<bool> CreateFiche(FicheTechniqueBridgeModel ficheModel)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    decimal sum = 0;
                    // Créer le transporteur
                    if (ficheModel.Produit_FicheTechnique.Count() > 0)
                    {
                        foreach (ProduitFicheTechniqueModel FicheTechnique in ficheModel.Produit_FicheTechnique)
                        {
                            FicheTechnique.FicheTechnique_DateCreation = DateTime.Now;
                            FicheTechnique.FicheTechnique_IsActive = 1;
                            FicheTechnique.FicheTechnique_AbonnementId = ficheModel.FicheTechniqueBridge_AbonnementID;
                            FicheTechnique.FicheTechnique_FicheTechniqueBridgeID = ficheModel.FicheTechniqueBridge_ID;
                            sum += FicheTechnique.FicheTechnique_Prix;
                        }
                    }
                    
                    ficheModel.FicheTechniqueBridge_CoutDeRevient = sum;
                    foreach (FicheFromeModel Forme in ficheModel.Fiche_Forme)
                    {
                        Forme.FicheForme_DateCreation = DateTime.Now;
                        Forme.FicheForme_IsActive = 1;
                        Forme.FicheForme_CoutDeRevient = ficheModel.FicheTechniqueBridge_CoutDeRevient / Forme.FicheForme_Quantite;
                    }
                    FicheTechniqueBridge fiche = mapper.Map<FicheTechniqueBridgeModel, FicheTechniqueBridge>(ficheModel);
                    var idTransporteur = await this.produitFicheTechniqueRepository.CreateFiche(fiche);
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

        /*   public async Task<bool> CreateFiche(List<ProduitFicheTechniqueModel> fichesModel)
  {
      using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
      {
          try
          {

              List<ProduitFicheTechnique> fiches = mapper.Map<List<ProduitFicheTechniqueModel>, List<ProduitFicheTechnique>>(fichesModel);
              var idFicheTech = await this.produitFicheTechniqueRepository.CreateFiche(fiches);

              transaction.Commit();
              return true;

          }
          catch (Exception ex)
          {
              transaction.Rollback();
              return false;
          }
      }
  }*/

        public Task<bool> deleteFicheTechnique(int ID)
        {
            return this.produitFicheTechniqueRepository.deleteFicheTechnique(ID);
        }

        public IEnumerable<FicheTechniqueBridgeModel> findFormulaireFiche(int formulaireFicheId, int AboId)
        {
            return mapper.Map<IEnumerable<FicheTechniqueBridge>, IEnumerable<FicheTechniqueBridgeModel>>(this.produitFicheTechniqueRepository.findFormulaireFiche(formulaireFicheId, AboId));
        }
        public FicheTechniqueBridgeModel findFormulaireFiche(int formulaireFicheId)
        {
            return mapper.Map<FicheTechniqueBridge, FicheTechniqueBridgeModel>(this.produitFicheTechniqueRepository.findFormulaireFiche(formulaireFicheId));
        }

        public IEnumerable<ProduitFicheTechniqueModel> findFormulaireFicheById(int formulaireFicheId)
        {
            return mapper.Map<IEnumerable<ProduitFicheTechnique>, IEnumerable<ProduitFicheTechniqueModel>>(this.produitFicheTechniqueRepository.findFormulaireFicheById(formulaireFicheId));
        }

        public ProduitFicheTechniqueModel findFormulaireFicheByLibelle(string Libelle)
        {
            return mapper.Map<ProduitFicheTechnique, ProduitFicheTechniqueModel>(this.produitFicheTechniqueRepository.findFormulaireFicheByLibelle(Libelle));
        }

        public IEnumerable<ProduitFicheTechniqueModel> getListFicheTechnique(int Id, int AboId)
        {
            return mapper.Map<IEnumerable<ProduitFicheTechnique>, IEnumerable<ProduitFicheTechniqueModel>>(this.produitFicheTechniqueRepository.getListFicheTechnique(Id, AboId));
        }
        public IEnumerable<FicheTechniqueBridgeModel> getListFicheTechniqueAll(int Id,int? categ,int? SousCateg)
        {
            return mapper.Map<IEnumerable<FicheTechniqueBridge>, IEnumerable<FicheTechniqueBridgeModel>>(produitFicheTechniqueRepository.getListFicheTechniqueAll(Id,categ,SousCateg));
        }

        public IEnumerable<FicheDetailsModel> getListFicheTechniqueByLibelle(string Libelle, int Id)
        {

            var ok = this.produitFicheTechniqueRepository.getListFicheTechniqueByLibelle(Libelle, Id);
            return ok;
        }

        public IEnumerable<MatierePremiereModel> getListMatierePremiere(int Id)
        {
            return mapper.Map<IEnumerable<MatierePremiere>, IEnumerable<MatierePremiereModel>>(this.produitFicheTechniqueRepository.getListMatierePremiere(Id));
        }

        public IEnumerable<ProduitVendableModel> getListProduitVendable(int Id)
        {
            return mapper.Map<IEnumerable<ProduitVendable>, IEnumerable<ProduitVendableModel>>(this.produitFicheTechniqueRepository.getListProduitVendable(Id));
        }

        public IEnumerable<Unite_MesureModel> getListUniteMesure()
        {
            return mapper.Map<IEnumerable<Unite_Mesure>, IEnumerable<Unite_MesureModel>>(this.produitFicheTechniqueRepository.getListUniteMesure());
        }

        public Task<bool> updateFormulaireFicheTechnique(int id, ProduitFicheTechniqueModel newFicheModel)
        {
            ProduitFicheTechnique fiche = mapper.Map<ProduitFicheTechniqueModel, ProduitFicheTechnique>(newFicheModel);
            return this.produitFicheTechniqueRepository.updateFormulaireFicheTechnique(id, fiche);
        }
        public Task<bool> SetInUse(int id)
        {
            return this.produitFicheTechniqueRepository.SetInUse(id);
        }

        public IEnumerable<Forme_ProduitModel> getListFormes(int produitId)
        {
            return mapper.Map<IEnumerable<Forme_Produit>, IEnumerable<Forme_ProduitModel>>(produitFicheTechniqueRepository.getListFormes(produitId));

        }

        public IEnumerable<FicheFromeModel> GetFicheFormes(int formulaireFicheId)
        {
            return mapper.Map<IEnumerable<FicheForme>, IEnumerable<FicheFromeModel>>(produitFicheTechniqueRepository.GetFicheFormes(formulaireFicheId));
        }

        public async Task<bool> CreateFicheBase(FicheTehcniqueProduitBaseModel ficheBaseModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    decimal sum = 0;
                    // Créer le transporteur
                    foreach (ProduitBaseFicheTechniqueModel FicheTechnique in ficheBaseModel.ProduitBase_FicheTechnique)
                    {
                        FicheTechnique.ProduitBaseFicheTechnique_DateCreation = DateTime.Now;
                        //FicheTechnique.isa = 1;
                        FicheTechnique.ProduitBaseFicheTechnique_AbonnementId = ficheBaseModel.FicheTechniqueProduitBase_AbonnementID;
                        FicheTechnique.ProduitBaseFicheTechnique_FicheTehcniqueProduitBaseID = ficheBaseModel.FicheTechniqueProduitBase_ID;
                        sum += FicheTechnique.ProduitBaseFicheTechnique_Prix;
                    }
                    ficheBaseModel.FicheTechniqueProduitBase_CoutDeRevient = sum;

                    FicheTechniqueProduitBase fiche = mapper.Map<FicheTehcniqueProduitBaseModel, FicheTechniqueProduitBase>(ficheBaseModel);
                    var idFiche = await produitFicheTechniqueRepository.CreateFicheBase(fiche);
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

        public IEnumerable<FicheTehcniqueProduitBaseModel> getListFicheTechniqueBaseAll(int Id, int? categ, int? SousCateg)
        {
            return mapper.Map<IEnumerable<FicheTechniqueProduitBase>, IEnumerable<FicheTehcniqueProduitBaseModel>>(produitFicheTechniqueRepository.getListFicheTechniqueBaseAll(Id, categ, SousCateg));
        }

        public IEnumerable<FicheTehcniqueProduitBaseModel> findFormulaireFicheBase(int formulaireFicheBaseId, int aboId)
        {
            return mapper.Map<IEnumerable<FicheTechniqueProduitBase>, IEnumerable<FicheTehcniqueProduitBaseModel>>(produitFicheTechniqueRepository.findFormulaireFicheBase(formulaireFicheBaseId,aboId));
        }

        public IEnumerable<ProduitBaseFicheTechniqueModel> getListFicheTechniqueBase(int Id, int AboId)
        {
            return mapper.Map<IEnumerable<ProduitBaseFicheTechnique>, IEnumerable<ProduitBaseFicheTechniqueModel>>(produitFicheTechniqueRepository.getListFicheTechniqueBase(Id, AboId));

        }

        public Task<bool> SetInUseBase(int id)
        {
            return produitFicheTechniqueRepository.SetInUseBase(id);
        }

        public decimal CalculerPrix(int prodId, int uniteID, decimal qte)
        {
            return produitFicheTechniqueRepository.CalculerPrix(prodId, uniteID, qte);
        }
    }
}
