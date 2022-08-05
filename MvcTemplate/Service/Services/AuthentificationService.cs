using AutoMapper;
using Domain.Authentication;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Repository.IRepositories;
using Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly IAuthentificationRepository authentificationRepository;
        private readonly IMapper mapper;


        public AuthentificationService(IAuthentificationRepository authentificationRepository, IMapper mapper)
        {
            this.authentificationRepository = authentificationRepository;
            this.mapper = mapper;
        }

        public async Task<Response> Register(RegisterModel registerModel)
        {
            return await this.authentificationRepository.Register(registerModel);
        }

        public async Task<bool> Login(LoginModel loginModel)
        {
            var user = await authentificationRepository.Login(loginModel);
            if (user == null)
                return false;
            if (user.Abonnement_ISACTIVE == 0)
                return false;

            return true;
        }

        public async Task<UserModel> LoginMobile(LoginModel loginModel)
        {
            ApplicationUser user = await this.authentificationRepository.Login(loginModel);
            if (user != null)
            {
                var roleUser = await this.authentificationRepository.getRoleUserById(user.Id);
                UserModel userModel = mapper.Map<ApplicationUser, UserModel>(user);
                if (roleUser != "Position_Vente" && user.Abonnement_ISACTIVE == 0)
                { 
                    userModel.Message = 0;
                }
                else
                {
                    userModel.Message = 1;
                }
                return userModel;
            }
            else
            {
                return null;
            }
          
        }

        public int GetUserAboIdById(string id)
        {
            return this.authentificationRepository.GetUserAboIdById(id);
        }

        public IEnumerable<RoleModel> getListRoles()
        {
            return mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleModel>>(this.authentificationRepository.getListRoles());
        }
        public IEnumerable<UserModel> getListUsers()
        {
            return this.authentificationRepository.getListUsers();
        }

        public async Task<bool> Logout()
        {
            return await this.authentificationRepository.Logout();
        }

        public IEnumerable<UserModel> getListUsersClient(int ID,string role,int? statut)
        {
            return authentificationRepository.getListUsersClient(ID, role,statut);
        }

        public IEnumerable<Lieu_StockageModel> GetLieu_Stockages(int ID)
        {
            return mapper.Map<IEnumerable<Lieu_Stockage>, IEnumerable<Lieu_StockageModel>>(authentificationRepository.GetLieu_Stockages(ID));
        }
        public async Task<bool> UpdateUserAsync(string id, string oldMdp, RegisterModel userModel)
        {
            return await this.authentificationRepository.UpdateUserAsync(id, oldMdp, userModel);
        }

        public async Task<bool> ResetMdpAsync(string id, string mdp)
        {
            return await this.authentificationRepository.ResetMdpAsync(id, mdp);
        }

        public async Task<bool> ActiverAsync(string id)
        {
            return await this.authentificationRepository.ActiverAsync(id);
        }

        public async Task<bool> DesactiverAsync(string id)
        {
            return await this.authentificationRepository.DesactiverAsync(id);
        }

        public async Task<bool> UpdateUserAsyncApi(string id, string oldMdp, string newMdp)
        {
            return await authentificationRepository.UpdateUserAsyncApi(id, oldMdp, newMdp);

        }

        public UserModel findFormulaireUser(string userId)
        {
            return authentificationRepository.findFormulaireUser(userId);
        }

        public Task<bool> updateFormulaireUser(string userId, UserModel newUser)
        {
            return this.authentificationRepository.updateFormulaireUser(userId, newUser);
        }
    }
}
