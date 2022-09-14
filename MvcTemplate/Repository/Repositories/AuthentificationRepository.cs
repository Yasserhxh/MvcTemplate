using Domain.Authentication;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.Data;
using Repository.IRepositories;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class AuthentificationRepository : IAuthentificationRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork unitOfWork;

        public AuthentificationRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateRole(string role)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(role));
            if (result.Succeeded)
                return true;
            else
                return false;
        }

        public IEnumerable<IdentityRole> getListRoles()
        {
            return _roleManager.Roles.AsEnumerable();
        }
        public async Task<ApplicationUser> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            if (user != null)
            {
                var p = _db.paiement_Abonnements.Where(v => v.PaiementAbonnement_AbonnementId == user.Abonnement_ID).LastOrDefault();
                var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, false, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    if (p.PaiementAbonnement_DateFinPeriode == DateTime.Now)
                        user.Abonnement_ISACTIVE = 0;
                    return user;

                }  
                else
                    return null;
            }
            //return user;
            return null;
        }
        public async Task<Response> Register(RegisterModel registerModel)
        {

            var userExists = await _userManager.FindByNameAsync(registerModel.UserName);
            if (userExists != null)
                return new Response { Success = false, Message = "Utilisateur existe déjà!" };
            var logo = _db.logoUser.Where(l => l.Abonnement_Id == registerModel.Abonnement_ID).FirstOrDefault().Logo;

            var user = new ApplicationUser
            {
                Abonnement_ISACTIVE = 1,
                UserName = registerModel.UserName,
                Abonnement_ID = registerModel.Abonnement_ID,
                Email = registerModel.Email,
                Nom = registerModel.Nom,
                PhoneNumber = registerModel.GSM,
                Adresse = registerModel.Adresse,
                Prenom = registerModel.Prenom,
                Nom_Complet = registerModel.Nom_Complet,
                //LieuStockage_ID = registerModel.LieuStockage_ID,
                Logo = logo,
            };
           
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var result = await _userManager.CreateAsync(user, registerModel.Password);
                    if (result.Succeeded)
                    {

                        var resultRole = await _userManager.AddToRoleAsync(user, registerModel.Role);
                        if (!resultRole.Succeeded)
                        {
                            transaction.Rollback();
                            return new Response { Success = false, Message = "Prob before role" }; ;
                        }
                        transaction.Commit();
                        return new Response { Success = true, Message = Messages.ErrorRole };
                    }
                    else
                    {
                        transaction.Rollback();
                        return new Response { Success = false, Message = Messages.Error };
                    }
                }
                catch
                {
                    transaction.Rollback();
                    return new Response { Success = false, Message = Messages.Error };
                }
            }
        }

        public IEnumerable<UserModel> getListUsers()
        {
            var users = (from u in _db.Users
                         join ur in _db.UserRoles on u.Id equals ur.UserId
                         join r in _db.Roles on ur.RoleId equals r.Id
                         //join c in _db.abonnement_Clients on u.Abonnement_ID equals c.Abonnement_ID //into empClient from clt in empClient
                         //from cl in empClient
                         select new UserModel()
                         {
                             UserId = u.Id,
                             UserName = u.UserName,
                             Gsm = u.PhoneNumber,
                             Nom = u.Nom,
                             Prenom = u.Prenom,
                             Email = u.Email,
                             Role = r.Name,
                             Statut = u.Abonnement_ISACTIVE
                             //    Client = (clt == null) ?  ("Nom Client Inconnu") : clt.Abonnement_NomClient
                         }).ToList();
            return users;
        }
        public IEnumerable<UserModel> getListUsersClient(int ID,string role, int? statut)
        {
            List<UserModel> users;
            if(role !="")
            {
                if (statut != null)
                {
                    users = (from u in _db.Users
                             join ur in _db.UserRoles on u.Id equals ur.UserId
                             join r in _db.Roles on ur.RoleId equals r.Id
                             where u.Abonnement_ID == ID
                             where r.Id != 0.ToString()
                             where u.Abonnement_ISACTIVE == statut
                             where r.Name == role
                             select new UserModel()
                             {
                                 UserId = u.Id,
                                 UserName = u.UserName,
                                 Gsm = u.PhoneNumber,
                                 Nom = u.Nom,
                                 Prenom = u.Prenom,
                                 Email = u.Email,
                                 Role = r.Name,
                                 Statut = u.Abonnement_ISACTIVE,
                                 Point_Vente = u.Point_Vente,
                                 Position_Vente = u.Position_Vente,
                                 Atelier = u.Atelier,
                                 Lieu_Stockage = u.Lieu_Stockage
                             }).ToList();
                    foreach (var us in users)
                    {
                        if (us.Point_Vente != null)
                            us.Affectation = us.Point_Vente.PointVente_Nom;
                        if (us.Atelier != null)
                            us.Affectation = us.Atelier.Atelier_Nom;
                        if (us.Lieu_Stockage != null)
                            us.Affectation = us.Lieu_Stockage.LieuStockag_Nom;
                        if (us.Position_Vente != null)
                        {
                            //us.Affectation = us.Position_Vente.PositionVente_Libelle;
                            var pdv = _db.positionVentes.Where(p => p.PositionVente_Id == us.Position_Vente.PositionVente_Id).Include(p => p.Point_Vente).FirstOrDefault().Point_Vente.PointVente_Nom;

                            us.Affectation = pdv + " / " + us.Position_Vente.PositionVente_Libelle;

                        }

                    }
                    return users;
                }
                else
                {
                    users = (from u in _db.Users
                             join ur in _db.UserRoles on u.Id equals ur.UserId
                             join r in _db.Roles on ur.RoleId equals r.Id
                             where u.Abonnement_ID == ID
                             where r.Id != 0.ToString()
                             where r.Name == role
                             select new UserModel()
                             {
                                 UserId = u.Id,
                                 UserName = u.UserName,
                                 Gsm = u.PhoneNumber,
                                 Nom = u.Nom,
                                 Prenom = u.Prenom,
                                 Email = u.Email,
                                 Role = r.Name,
                                 Statut = u.Abonnement_ISACTIVE,
                                 Point_Vente = u.Point_Vente,
                                 Position_Vente = u.Position_Vente,
                                 Atelier = u.Atelier,
                                 Lieu_Stockage = u.Lieu_Stockage
                             }).ToList();
                    foreach (var us in users)
                    {
                        if (us.Point_Vente != null)
                            us.Affectation = us.Point_Vente.PointVente_Nom;
                        if (us.Atelier != null)
                            us.Affectation = us.Atelier.Atelier_Nom;
                        if (us.Lieu_Stockage != null)
                            us.Affectation = us.Lieu_Stockage.LieuStockag_Nom;
                        if (us.Position_Vente != null)
                        {
                            //us.Affectation = us.Position_Vente.PositionVente_Libelle;
                            var pdv = _db.positionVentes.Where(p => p.PositionVente_Id == us.Position_Vente.PositionVente_Id).Include(p => p.Point_Vente).FirstOrDefault().Point_Vente.PointVente_Nom;

                            us.Affectation = pdv + " / " + us.Position_Vente.PositionVente_Libelle;

                        }
                    }
                    return users;
                }

                
            }
            else {
                if(statut != null)
                {
                    users = (from u in _db.Users
                             join ur in _db.UserRoles on u.Id equals ur.UserId
                             join r in _db.Roles on ur.RoleId equals r.Id
                             where u.Abonnement_ID == ID
                             where r.Id != 0.ToString()
                             where u.Abonnement_ISACTIVE == statut
                             select new UserModel()
                             {
                                 UserId = u.Id,
                                 UserName = u.UserName,
                                 Gsm = u.PhoneNumber,
                                 Nom = u.Nom,
                                 Prenom = u.Prenom,
                                 Email = u.Email,
                                 Role = r.Name,
                                 Statut = u.Abonnement_ISACTIVE,
                                 Point_Vente = u.Point_Vente,
                                 Position_Vente = u.Position_Vente,
                                 Atelier = u.Atelier,
                                 Lieu_Stockage = u.Lieu_Stockage

                             }).ToList();
                    foreach (var us in users)
                    {
                        if (us.Point_Vente != null)
                            us.Affectation = us.Point_Vente.PointVente_Nom;
                        if (us.Atelier != null)
                            us.Affectation = us.Atelier.Atelier_Nom;
                        if (us.Lieu_Stockage != null)
                            us.Affectation = us.Lieu_Stockage.LieuStockag_Nom;
                        if (us.Position_Vente != null)
                        {
                            //us.Affectation = us.Position_Vente.PositionVente_Libelle;
                            var pdv = _db.positionVentes.Where(p => p.PositionVente_Id == us.Position_Vente.PositionVente_Id).Include(p => p.Point_Vente).FirstOrDefault().Point_Vente.PointVente_Nom;

                            us.Affectation = pdv + " / " + us.Position_Vente.PositionVente_Libelle;

                        }
                    }
                    return users;
                }
                else
                {
                    users = (from u in _db.Users
                             join ur in _db.UserRoles on u.Id equals ur.UserId
                             join r in _db.Roles on ur.RoleId equals r.Id
                             where u.Abonnement_ID == ID
                             where r.Id != 0.ToString()
                             select new UserModel()
                             {
                                 UserId = u.Id,
                                 UserName = u.UserName,
                                 Gsm = u.PhoneNumber,
                                 Nom = u.Nom,
                                 Prenom = u.Prenom,
                                 Email = u.Email,
                                 Role = r.Name,
                                 Statut = u.Abonnement_ISACTIVE,
                                 Point_Vente = u.Point_Vente,
                                 Position_Vente = u.Position_Vente,
                                 Atelier = u.Atelier,
                                 Lieu_Stockage = u.Lieu_Stockage

                             }).ToList();
                    foreach (var us in users)
                    {
                        if (us.Point_Vente != null)
                            us.Affectation = us.Point_Vente.PointVente_Nom;
                        if (us.Atelier != null)
                            us.Affectation = us.Atelier.Atelier_Nom;
                        if (us.Lieu_Stockage != null)
                            us.Affectation = us.Lieu_Stockage.LieuStockag_Nom;
                        if (us.Position_Vente != null)
                        {
                            //us.Affectation = us.Position_Vente.PositionVente_Libelle;
                            var pdv = _db.positionVentes.Where(p => p.PositionVente_Id == us.Position_Vente.PositionVente_Id).Include(p => p.Point_Vente).FirstOrDefault().Point_Vente.PointVente_Nom;
                            us.Affectation = pdv + " / " + us.Position_Vente.PositionVente_Libelle;

                        }
                    }
                    return users;
                }
            }
        }  
        
        public IEnumerable<UserModel> getLisGerants(int ID)
        {
            var users = (from u in _db.Users
                         join ur in _db.UserRoles on u.Id equals ur.UserId
                         join r in _db.Roles on ur.RoleId equals r.Id
                         where u.Abonnement_ID == ID
                         where u.LieuStockage_ID == null
                         select new UserModel()
                         {
                             UserId = u.Id,
                             UserName = u.UserName,
                             Gsm = u.PhoneNumber,
                             Nom = u.Nom,
                             Prenom = u.Prenom,
                             Email = u.Email,
                             Role = r.Name,
                         }).ToList();
            return users;
        }
        public async Task<bool> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<Response> RegisterEntity(RegisterModel registerModel)
        {
            var userExists = await _userManager.FindByNameAsync(registerModel.UserName);
            if (userExists != null)
                return new Response { Success = false, Message = "Utilisateur existe déjà!" };
            //var logoD = _db.logoUser.Where(p => p.Abonnement_Id == registerModel.Abonnement_ID).FirstOrDefault();
            //var logo = _db.logoUser.Where(l => l.Abonnement_Id == registerModel.Abonnement_ID).FirstOrDefault().Logo;

            var user = new ApplicationUser
            {
                UserName = registerModel.UserName,
                Email = registerModel.Email,
                Abonnement_ID = registerModel.Abonnement_ID,
                Logo = registerModel.Logo,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (result.Succeeded)
                {
                    var resultRole = await _userManager.AddToRoleAsync(user, registerModel.Role);
                    if (!resultRole.Succeeded)
                    {
                        return new Response { Success = false, Message = "" }; ;
                    }
                    return new Response { Success = true, Message = Messages.ErrorRole };
                }
                else
                {
                    return new Response { Success = false, Message = Messages.Error };
                }
            }
            catch (Exception)
            {
                return new Response { Success = false, Message = Messages.Error };
            }
        }
        public async Task<ApplicationUser> GetUserByName(string name)
        {
            return await _userManager.FindByNameAsync(name);
        }

        public ApplicationUser GetUserById(string id)
        {
            return _userManager.Users.FirstOrDefault(u => u.Id == id);
        }
        public ApplicationUser Geturser(string id)
        {
            return _userManager.Users
                .Include(u => u.Lieu_Stockage)
                .Include(u => u.Atelier)
                .Include(u => u.Point_Vente)
                .Include(u=>u.Position_Vente).ThenInclude(p=>p.Point_Vente)
                .FirstOrDefault(u => u.Id == id);
        }
        public int GetUserAboIdById(string id)
        {

            string usersAboId = (from u in _db.Users
                                 where u.Id == id
                                 select u.Abonnement_ID).FirstOrDefault().ToString();
            if (usersAboId == "" || usersAboId == null)
            {
                return 0;
            }

            int aboid = Convert.ToInt32(usersAboId);
            return aboid;


        }

        public IEnumerable<Lieu_Stockage> GetLieu_Stockages(int ID)
        {
            return _db.lieu_Stockages
                .Where(l => l.LieuStockag_AbonnementId == ID)
                .Where(l => l.LieuStockag_UTILISATEUR == null)
                .AsEnumerable();
        }

        public async Task<string> getRoleUserById(string id)
        {           
                var user = GetUserById(id);
                var roles = await _userManager.GetRolesAsync(user);
                return roles[0];            
        }
        public async Task<bool> UpdateUserAsync(string id, string oldMdp, RegisterModel userModel)
        {
            try
            {
                var user = _db.Users.Where(u => u.Id == id).FirstOrDefault();

                var mdpHash = user.PasswordHash;

                var passresult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, oldMdp);

                if (user == null || passresult == PasswordVerificationResult.Failed)
                    return false;

                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userModel.Password);

                _db.Entry(user).State = EntityState.Modified;

                await unitOfWork.Complete();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        } 
        public async Task<bool> UpdateUserAsyncApi(string id, string oldMdp, string newMdp)
        {
            try
            {
                var user = _db.Users.Where(u => u.Id == id).FirstOrDefault();

                var mdpHash = user.PasswordHash;

                var passresult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, oldMdp);

                if (user == null || passresult == PasswordVerificationResult.Failed)
                    return false;

                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, newMdp);

                _db.Entry(user).State = EntityState.Modified;

                await unitOfWork.Complete();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ResetMdpAsync(string id, string mdp)
        {
            try
            {
                var user = GetUserById(id);

                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, mdp);

                _db.Entry(user).State = EntityState.Modified;

                await unitOfWork.Complete();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ActiverAsync(string id)
        {
            try
            {
                var user = GetUserById(id);

                if (user == null)
                    return false;

                user.Abonnement_ISACTIVE = 1;
                _db.Entry(user).State = EntityState.Modified;

                await unitOfWork.Complete();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DesactiverAsync(string id)
        {
            try
            {
                var user = GetUserById(id);

                if (user == null)
                    return false;

                user.Abonnement_ISACTIVE = 0;
                _db.Entry(user).State = EntityState.Modified;

                await unitOfWork.Complete();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string getUserLogo(int aboId)
        {
            return _db.logoUser.Where(l => l.Abonnement_Id == aboId).FirstOrDefault().Logo;
        }
        public UserModel findFormulaireUser(string userId)
        {
            var user =  _db.Users
                .Where(l => l.Id == userId)
                .Include(p=>p.Point_Vente)
                .Include(p=>p.Position_Vente)
                .Include(p=>p.Atelier)
                .Include(p=>p.Lieu_Stockage)
                .FirstOrDefault();
            var usModel = new UserModel()
            {
                UserId = user.Id,
                //UserName = user.UserName,
                Gsm = user.PhoneNumber,
                Nom = user.Nom,
                Prenom = user.Prenom,
                Email = user.Email,
                Point_Vente = user.Point_Vente,
                Position_Vente = user.Position_Vente,
                Atelier = user.Atelier,
                Lieu_Stockage = user.Lieu_Stockage
            };
            return usModel;
        }
        public async Task<bool> updateFormulaireUser(string userId, UserModel newUser)
        {
            ApplicationUser user = _db.Users.Where(x => x.Id == userId).FirstOrDefault();

            if (user != null)
            {
                user.Nom = newUser.Nom;
                user.Prenom = newUser.Prenom;
                user.Nom_Complet = newUser.Nom +" "+ newUser.Prenom;
                user.Email = newUser.Email;
                user.PhoneNumber = newUser.Gsm;
                //user.Abonnement_ISACTIVE = 1;

                _db.Entry(user).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        
    }
}
