using AutoMapper;
using Domain.Authentication;
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
    public class Abonnement_ClientService : IAbonnement_ClientService
    {
        private readonly IAuthentificationRepository authentificationRepository;
        private readonly IAbonnement_ClientRepository abonnement_ClientRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public Abonnement_ClientService(IAbonnement_ClientRepository abonnement_ClientRepository, IMapper mapper,
            IUnitOfWork unitOfWork, IAuthentificationRepository authentificationRepository)
        {
            this.authentificationRepository = authentificationRepository;
            this.abonnement_ClientRepository = abonnement_ClientRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool?> AjouterUtilisateur(int idAtelier, string responsableId)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    var id = await abonnement_ClientRepository.AjouterUtilisateur(idAtelier, responsableId);
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

        public async Task<bool> CreateAtelier(AtelierModel atelierModel)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    //Créer le Client
                    Atelier atelier = mapper.Map<AtelierModel, Atelier>(atelierModel);
                    var idAtelier = await abonnement_ClientRepository.CreateAtelier(atelier);

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

        public async Task<bool> CreateClient(Abonnement_ClientModel ClientModel)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    //Créer le Client
                    Abonnement_Client client = mapper.Map<Abonnement_ClientModel, Abonnement_Client>(ClientModel);
                    var idClient = await this.abonnement_ClientRepository.CreateClient(client);

                    //Créer le compte du Client
                    RegisterModel registerModel = new RegisterModel
                    {
                        UserName = ClientModel.UserName,
                        Email = ClientModel.Abonnement_ContactEmail,
                        //Telephone = ClientModel.Abonnement_ContactTelephone,
                        // Nom = ClientModel.Abonnement_NomClient,
                        Role = UserRoles.Client,
                        Password = ClientModel.Password,
                        Abonnement_ID = idClient,
                        Logo = ClientModel.Abonnement_Logo,
                    };
                    await this.authentificationRepository.RegisterEntity(registerModel);

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

        public Task<bool> deleteFormulaireAtelier(int ID, int code)
        {
            return abonnement_ClientRepository.deleteFormulaireAtelier(ID, code);
        }

        public Task<bool> deleteFormulaireClient(int ID)
        {
            return this.abonnement_ClientRepository.deleteFormulaireClient(ID);
        }

        public AtelierModel findFormulaireAtelier(int formulaireAtelierId)
        {
            return mapper.Map<Atelier, AtelierModel>(abonnement_ClientRepository.findFormulaireAtelier(formulaireAtelierId));
        }

        public Abonnement_ClientModel findFormulaireClient(int formulaireClientId)
        {
            return mapper.Map<Abonnement_Client, Abonnement_ClientModel>(this.abonnement_ClientRepository.findFormulaireClient(formulaireClientId));
        }

        public IEnumerable<AtelierModel> getListAtelier(int Id,int? statut)
        {
            return mapper.Map<IEnumerable<Atelier>, IEnumerable<AtelierModel>>(abonnement_ClientRepository.getListAtelier(Id, statut));
        }

        public IEnumerable<Atelier_UserModel> getListAtelierUser(string UserId)
        {
            return mapper.Map<IEnumerable<Atelier_User>, IEnumerable<Atelier_UserModel>>(abonnement_ClientRepository.getListAtelierUser(UserId));

        }

        /* public IEnumerable<Abonnement_ClientModel> getListClient()
         {
             return mapper.Map<IEnumerable<Abonnement_Client>, IEnumerable<Abonnement_ClientModel>>(this.abonnement_ClientRepository.getListClient());
         }*/
        public IEnumerable<Abonnement_ClientModel> getListClient()
        {
            return this.abonnement_ClientRepository.getListClient();
        }

        public IEnumerable<Lieu_StockageModel> getListStocks(int Id)
        {
            return mapper.Map<IEnumerable<Lieu_Stockage>, IEnumerable<Lieu_StockageModel>>(abonnement_ClientRepository.getListStocks(Id));
        }

        public IEnumerable<VilleModel> getListVilles()
        {
            return mapper.Map<IEnumerable<Ville>, IEnumerable<VilleModel>>(abonnement_ClientRepository.getListVilles());
        }

        public Task<bool> updateFormulaireAtelier(int id, AtelierModel newAtelier)
        {
            Atelier atelier = mapper.Map<AtelierModel, Atelier>(newAtelier);
            return abonnement_ClientRepository.updateFormulaireAtelier(id, atelier);
        }

        public Task<bool> updateFormulaireClient(int id, Abonnement_ClientModel ClientModel)
        {
            Abonnement_Client client = mapper.Map<Abonnement_ClientModel, Abonnement_Client>(ClientModel);
            return this.abonnement_ClientRepository.updateFormulaireClient(id, client);

        }
        public IEnumerable<FamilleProduitModel> getListFamilles(int Id)
        {
            return mapper.Map<IEnumerable<FamilleProduit>, IEnumerable<FamilleProduitModel>>(abonnement_ClientRepository.getListFamilles(Id));
        }

        public IEnumerable<PointProduction_FamilleModel> getListFammilesProduction(int pdvId)
        {
            return mapper.Map<IEnumerable<PointPorduction_Famille>, IEnumerable<PointProduction_FamilleModel>>(abonnement_ClientRepository.getListFammilesProduction(pdvId));
        }

        public Task<bool?> AjouterFamille(int atelierId, List<int> ListFamille)
        {
            return abonnement_ClientRepository.AjouterFamille(atelierId, ListFamille);
        }

        public Task<bool> deleteFamillePdv(int id, int code)
        {
            return abonnement_ClientRepository.deleteFamillePdv(id, code);
        }

        public async Task<bool> CreateAbonnement(PaiementAbonnementModel paiement_AbonnementModel)
        {
            using (IDbContextTransaction transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    Paiement_Abonnement paiement_Abonnement = mapper.Map<PaiementAbonnementModel, Paiement_Abonnement>(paiement_AbonnementModel);
                    var idabonnement = await abonnement_ClientRepository.CreateAbonnement(paiement_Abonnement);
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

        public IEnumerable<PaiementAbonnementModel> getListAbonnement(int? client)
        {
            return mapper.Map<IEnumerable<Paiement_Abonnement>, IEnumerable<PaiementAbonnementModel>>(abonnement_ClientRepository.getListAbonnement(client));
        }
    }
}
