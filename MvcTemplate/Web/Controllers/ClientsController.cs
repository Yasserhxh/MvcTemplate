using Domain.Authentication;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.IRepositories;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Authorize(Roles = "Administrateur,Client")]

    public class ClientsController : Controller
    {
        private readonly IAbonnement_ClientService abonnement_Client_Service;
        private readonly IAuthentificationRepository authentificationRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _environment;
        private readonly IPointVenteService pointVenteService;

        public ClientsController(IAbonnement_ClientService abonnement_Client_Service, IAuthentificationRepository authentificationRepository, UserManager<ApplicationUser> userManager, IPointVenteService pointVenteService, IHostingEnvironment environment)
        {
            this.abonnement_Client_Service = abonnement_Client_Service;
            this.authentificationRepository = authentificationRepository;
            _environment = environment;
            _userManager = userManager;
            this.pointVenteService = pointVenteService;

        }

        public IActionResult Ajouter()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Ajouter([FromForm] Abonnement_ClientModel clientModel)
        {
            var newFileName = string.Empty;
            if (HttpContext.Request.Form.Files != null)
            {
                var fileName = string.Empty;
                string PathDB = string.Empty;

                var files = HttpContext.Request.Form.Files;

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        //Getting FileName
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var FileExtension = Path.GetExtension(fileName);

                        // concating  FileName + FileExtension
                        newFileName = myUniqueFileName + FileExtension;

                        // Combines two strings into a path.
                        fileName = Path.Combine(_environment.WebRootPath, "images") + $@"\logoUser\{newFileName}";

                        // if you want to store path of folder in database
                        PathDB = "images/logoUser/" + newFileName;

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                        clientModel.Abonnement_Logo = PathDB;

                    }
                }


            }
            clientModel.Abonnement_IsActive = 1;
            var redirect = await abonnement_Client_Service.CreateClient(clientModel);
            if (redirect)
            {
                TempData["Creation"] = "OK";
                return Redirect("/Clients/ListeClient");
            }
            else
            {
                return View();
            }

        }

        public IActionResult ListeClient()
        {
            var client = abonnement_Client_Service.getListClient();
            return View(client);
        }

        public IActionResult Modification(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var donnee = abonnement_Client_Service.findFormulaireClient((int)id);
            if (donnee == null)
            {
                return NotFound();
            }
            return View(donnee);
        }
        [HttpPost]

        public async Task<IActionResult> Modification(int id, Abonnement_ClientModel clientModel)
        {
            var redirect = await abonnement_Client_Service.updateFormulaireClient(id, clientModel);

            if (redirect)
            {
                TempData["Modification"] = "OK";
                return Redirect("/Clients/ListeClient");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]

        public async Task<bool> deleteClient(int id)
        {
            var result = await abonnement_Client_Service.deleteFormulaireClient(id);
            return result;
        }
        [HttpPost]

        public async Task<bool> deleteAtelier(int id, int code)
        {
            var result = await abonnement_Client_Service.deleteFormulaireAtelier(id, code);
            return result;
        }


        [HttpPost]
        public string getRespoName(string userId)
        {
            var qte = "";
            if (userId != null)
                qte = authentificationRepository.GetUserById(userId).Nom_Complet;
            return qte;
        }
        [HttpPost]
        public string getRespoPhoneNumber(string userId)
        {
            var qte = authentificationRepository.GetUserById(userId).PhoneNumber;
            return qte;
        }
        [HttpPost]

        public async Task<bool> AjouterAtelier(AtelierModel atelierModel)
        {
            atelierModel.Atelier_AbonnementID = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);

            var redirect = await abonnement_Client_Service.CreateAtelier(atelierModel);
            return redirect;

        }
        public IActionResult ListeAtelier(int? statut)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            ViewData["Ville"] = new SelectList(abonnement_Client_Service.getListVilles(), "Ville_ID", "Ville_Libelle");
            ViewData["Stocks"] = new SelectList(abonnement_Client_Service.getListStocks(Id), "LieuStockag_Id", "LieuStockag_Nom");
            ViewData["User"] = new SelectList(_userManager.GetUsersInRoleAsync("Chef_Patissier").Result.Where(u => u.Abonnement_ID == Id & u.Abonnement_ISACTIVE == 1).AsEnumerable(), "Id", "Nom_Complet");
            return View(abonnement_Client_Service.getListAtelier(Id, statut));
        }
        public IActionResult ModificationAtelier(int? id)
        {
            var Id = Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value);
            if (id == null)
            {
                return NotFound();
            }
            ViewData["Atelier_NomResponsable"] = new SelectList(_userManager.GetUsersInRoleAsync("Chef_Patissier").Result.Where(u => u.Abonnement_ID == Id & u.Abonnement_ISACTIVE==1).Where(u => u.AtelierID == null).AsEnumerable(), "Nom", "Nom");
            ViewData["Atelier_VilleID"] = new SelectList(abonnement_Client_Service.getListVilles(), "Ville_ID", "Ville_Libelle");
            var donnee = abonnement_Client_Service.findFormulaireAtelier((int)id);
            if (donnee == null)
            {
                return NotFound();
            }
            return View(donnee);
        }

        [HttpPost]
        public async Task<IActionResult> ModificationAtelier(int id, AtelierModel atelierModel)
        {
            var redirect = await abonnement_Client_Service.updateFormulaireAtelier(id, atelierModel);

            if (redirect)
            {
                TempData["Modification"] = "OK";
                return Redirect("/Clients/ListeAtelier");
            }
            else
            {
                ViewData["Atelier_NomResponsable"] = new SelectList(_userManager.GetUsersInRoleAsync("Chef_Patissier").Result.Where(u => u.AtelierID == null & u.Abonnement_ISACTIVE == 1).AsEnumerable(), "Nom", "Nom");
                ViewData["Atelier_VilleID"] = new SelectList(abonnement_Client_Service.getListVilles(), "Ville_ID", "Ville_Libelle");
                return View();
            }
        }
        public IActionResult ConsulterFamilles(int Id)
        {
            ViewData["Familles"] = new SelectList(abonnement_Client_Service.getListFamilles(Convert.ToInt32(HttpContext.User.FindFirst("AboId").Value)), "FamilleProduit_Id", "FamilleProduit_Libelle");
            return View("~/Views/Clients/ListeDesFamilles.cshtml", abonnement_Client_Service.getListFammilesProduction(Id));
        }
        [HttpPost]
        public string getPvNom(int Id)
        {
            return abonnement_Client_Service.findFormulaireAtelier(Id).Atelier_Nom;
        }
        [Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<bool> AjouterFamille(int idPointVente, List<int> listFamille)
        {
            try
            {

                var res = await abonnement_Client_Service.AjouterFamille(idPointVente, listFamille);
                if (res == null)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost]
        public async Task<bool> DeleteFamillePdv(int id, int code)
        {
            var result = await abonnement_Client_Service.deleteFamillePdv(id, code);
            return result;
        }
        [Authorize(Roles = "Administrateur")]
        public IActionResult CreerAbonnement()
        {
            ViewData["client"] = new SelectList(abonnement_Client_Service.getListClient(), "Abonnement_Id", "Abonnement_NomClient");
            return View();
        }
        [HttpPost]
        public async Task<bool> CreerAbonnement(PaiementAbonnementModel paiementModel)
        {
            paiementModel.PaiementAbonnement_AdminUserId =_userManager.GetUserId(HttpContext.User);
            paiementModel.PaiementAbonnement_Date = DateTime.Now;
            paiementModel.PaiementAbonnement_DateDebutPeriode = DateTime.Now;
            paiementModel.PaiementAbonnement_DateFinPeriode = DateTime.Today.AddMonths((int)paiementModel.PaiementAbonnement_PeriodeDePaiement);
            var redirect = await abonnement_Client_Service.CreateAbonnement(paiementModel);
            var date = DateTime.UtcNow.AddHours(1);
            return redirect;
        }
        public IActionResult ListePaiement(int? client)
        {
            ViewData["client"] = new SelectList(abonnement_Client_Service.getListClient(), "Abonnement_Id", "Abonnement_NomClient");
            return View(abonnement_Client_Service.getListAbonnement(client));
        }
        [HttpPost]
        public SelectList getListePdv(int clientID)
        {
            SelectList secteurSelect = new SelectList(pointVenteService.getListPointVenteActive(clientID), "PointVente_Id", "PointVente_Nom");
            return secteurSelect;
        }
    }

}
