using Domain.Authentication;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.IRepositories;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Web.Helpers;
namespace Web.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IProduitVendableService produitVendableService;
        private readonly IPointVenteService pointVenteService;
        private readonly IZoneStockageService zoneStockageService;
        private readonly IGestionMouvementService gestionMouvementService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthentificationRepository authentificationRepository;
        private readonly IProduitFicheTechniqueService produitFicheTechniqueService;
        private readonly IMatierePremiereService matierePremiereService;
        private readonly IHostingEnvironment _environment;
        private readonly IFamilleProduitService familleProduitService;
        private readonly IVenteProduitsService venteProduitsService;
        private readonly IAbonnement_ClientRepository abonnement_ClientRepository;
        public TicketsController(IProduitVendableService produitVendableService, IAuthentificationRepository authentificationRepository, UserManager<ApplicationUser> userManager,
          IZoneStockageService zoneStockageService, IProduitFicheTechniqueService produitFicheTechniqueService, IMatierePremiereService matierePremiereService,
          IGestionMouvementService gestionMouvementService, IHostingEnvironment IHostingEnvironment, IPointVenteService pointVenteService, IFamilleProduitService familleProduitService,
          IVenteProduitsService venteProduitsService, IAbonnement_ClientRepository abonnement_ClientRepository)
        {
            this.gestionMouvementService = gestionMouvementService;
            this.authentificationRepository = authentificationRepository;
            this.produitVendableService = produitVendableService;
            this.zoneStockageService = zoneStockageService;
            this.produitFicheTechniqueService = produitFicheTechniqueService;
            this.matierePremiereService = matierePremiereService;
            this.pointVenteService = pointVenteService;
            _userManager = userManager;
            _environment = IHostingEnvironment;
            this.familleProduitService = familleProduitService;
            this.venteProduitsService = venteProduitsService;
            this.abonnement_ClientRepository = abonnement_ClientRepository;

        }
        public IActionResult getTicket()
        {
            var url = HttpContext.Request.Path.ToString();
            var table = url.Split("/");
            var numticket = table.LastOrDefault();
            var ticket = venteProduitsService.GetTicketDetails(numticket);
            var user = abonnement_ClientRepository.findFormulaireClient(ticket.aboID);
            ticket.AgentName = authentificationRepository.GetUserById(ticket.AgentID).Nom_Complet;
            ticket.LogoImg = authentificationRepository.getUserLogo(ticket.aboID);
            ticket.Tel = user.Abonnement_ContactTelephone;
            ticket.Adresse = user.Abonnement_Adresse;
            ticket.ICE = user.Abonnement_ICE;
            ticket.IsCommande = 0;
            return View(ticket);
        }
        public IActionResult getTicketCommande()
        {
            var url = HttpContext.Request.Path.ToString();
            var table = url.Split("/");
            var numticket = table.LastOrDefault();
            var ticket = venteProduitsService.GetTicketDetailsCommande(numticket);
            var user = abonnement_ClientRepository.findFormulaireClient(ticket.aboID);
            ticket.AgentName = authentificationRepository.GetUserById(ticket.AgentID).Nom_Complet;
            ticket.LogoImg = authentificationRepository.getUserLogo(ticket.aboID);
            ticket.Tel = user.Abonnement_ContactTelephone;
            ticket.Adresse = user.Abonnement_Adresse;
            ticket.ICE = user.Abonnement_ICE;
            ticket.IsCommande = 1;

            return View("~/Views/Tickets/getTicket.cshtml", ticket);
        }
    }
}
