using Domain.Authentication;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IAuthentificationRepository
    {
        Task<Response> Register(RegisterModel userModel);
        Task<Response> RegisterEntity(RegisterModel userModel);
        Task<ApplicationUser> Login(LoginModel loginModel);
        Task<bool> CreateRole(string role);
        IEnumerable<IdentityRole> getListRoles();
        IEnumerable<UserModel> getListUsers();
        IEnumerable<UserModel> getListUsersClient(int ID,string role,int?statut);
        Task<bool> Logout();
        Task<ApplicationUser> GetUserByName(string name);
        ApplicationUser GetUserById(string id);
        int GetUserAboIdById(string id);
        IEnumerable<Lieu_Stockage> GetLieu_Stockages(int ID);
        Task<string> getRoleUserById(string id);
        IEnumerable<UserModel> getLisGerants(int ID);
        ApplicationUser Geturser(string id);
        Task<bool> UpdateUserAsync(string id, string oldMdp, RegisterModel userModel);
        Task<bool> UpdateUserAsyncApi(string id, string oldMdp, string newMdp );
        Task<bool> ActiverAsync(string id);
        Task<bool> DesactiverAsync(string id);
        Task<bool> ResetMdpAsync(string id, string mdp);
        string getUserLogo(int aboId);
        UserModel findFormulaireUser(string userId);
        Task<bool> updateFormulaireUser(string userId, UserModel newUser);
    }
}
