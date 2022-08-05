using Domain.Authentication;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.IRepositories;
using Service.IServices;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class AuthentificationController : Controller
    {
        private readonly IAuthentificationService authentificationService;
        private readonly IAuthentificationRepository authentificationRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthentificationController(IAuthentificationService authentificationService, UserManager<ApplicationUser> userManager, IAuthentificationRepository authentificationRepository)
        {
            this.authentificationService = authentificationService;
            this.authentificationRepository = authentificationRepository;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel user)
        {
            var success = await this.authentificationService.Login(user);

            if (!success)
            {
                TempData["LoginError"] = "Login ou mot de passe incorrect";
                return View();
            }
            return RedirectToAction("Parametrage", "Home");
        }

        [Authorize(Roles = "Client,Administrateur")]
        public IActionResult Register()
        {
            ViewData["Roles"] = new SelectList(authentificationService.getListRoles(), "Name", "Name");
            return View();
        }

        public IActionResult Utilisateurs(string role,int? statut)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            if (User.IsInRole("Administrateur"))
            {
                ViewData["Roles"] = new SelectList(authentificationService.getListRoles(), "Name", "Name");

                return View(authentificationService.getListUsers());
            }
            if (User.IsInRole("Client"))
            {
                ViewData["Roles"] = new SelectList(authentificationService.getListRoles().Where(r => r.Name != "Client").Where(r => r.Name != "Administrateur"), "Name", "Name");
                if (role == null || role == "null")
                    role = "";
                return View(authentificationService.getListUsersClient(id,role,statut));
            }
            return RedirectToAction("NotAuthorized", "Authentification");

        }
        [Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<int> RegisterUser(RegisterModel user)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            user.Abonnement_ID = id;
            user.Nom_Complet = user.Nom + " " + user.Prenom;
            var response = await authentificationService.Register(user);

            if (!response.Success)
            {
                TempData["Error"] = response.Message;
                ViewData["Roles"] = new SelectList(authentificationService.getListRoles(), "Name", "Name");
                return 0;
            }

            TempData["Creation"] = "OK";
            return 1;
        }

        [HttpPost]
        public async Task<bool> Logout()
        {
            return await this.authentificationService.Logout();
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }

        public IActionResult PasswordRecovery()
        {
            return View();
        }
        public IActionResult PageNotFound()
        {
            return View();
        }
        public IActionResult EditProfil()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditProfil(EditPasswordModel editPasswordModel)
        {
            var id = _userManager.GetUserId(HttpContext.User);
            var us = authentificationRepository.GetUserById(id);
            var oldMdp = editPasswordModel.OldPassword;
            var result = await this.authentificationService.UpdateUserAsync(id, oldMdp, editPasswordModel.registerModel);
            if (result)
            {
                return Redirect("/Authentification/Login");
            }
            else
            {
                TempData["MdpError"] = "Mot de passe incorrect";
                return View();
            }
        }

        [HttpPost]
        public async Task<bool> Reset(string id)
        {
            string mdp = "123*Op";
            return await authentificationService.ResetMdpAsync(id, mdp);
        }

        [HttpPost]
        public async Task<bool> Activation(string id)
        {
            return await this.authentificationService.ActiverAsync(id);
        }

        [HttpPost]
        public async Task<bool> Desactivation(string id)
        {
            return await this.authentificationService.DesactiverAsync(id);
        }
        public IActionResult Modification(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var donnee = authentificationService.findFormulaireUser(id);
            if (donnee == null)
            {
                return NotFound();
            }
            return View(donnee);
        }
        [HttpPost]

        public async Task<IActionResult> Modification(string id, UserModel newUser)
        {
            var redirect = await authentificationService.updateFormulaireUser(id, newUser);

            if (redirect)
            {
                TempData["Modification"] = "OK";
                return Redirect("/Authentification/Utilisateurs");
            }
            else
                return View();
        }
    }
}