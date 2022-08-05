using Domain.Enums;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IAuthentificationService
    {
        Task<Response> Register(RegisterModel userModel);
        Task<bool> Login(LoginModel loginModel);
        Task<UserModel> LoginMobile(LoginModel loginModel);
        IEnumerable<RoleModel> getListRoles();
        IEnumerable<UserModel> getListUsers();
        IEnumerable<UserModel> getListUsersClient(int ID,string role,int? statut);
        Task<bool> Logout();
        int GetUserAboIdById(string id);
        IEnumerable<Lieu_StockageModel> GetLieu_Stockages(int ID);
        Task<bool> UpdateUserAsync(string id, string oldMdp, RegisterModel userModel);
        Task<bool> UpdateUserAsyncApi(string id, string oldMdp, string newMdp);
        Task<bool> ResetMdpAsync(string id, string mdp);
        Task<bool> ActiverAsync(string id);
        Task<bool> DesactiverAsync(string id);
        UserModel findFormulaireUser(string userId);
        Task<bool> updateFormulaireUser(string userId, UserModel newUser);
    }
}
