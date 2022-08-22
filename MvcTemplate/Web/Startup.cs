using AutoMapper;
using Domain.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Data;
using Repository.IRepositories;
using Repository.Repositories;
using Repository.UnitOfWork;
using Service.IServices;
using Service.Services;
using System;
using System.Globalization;
using System.Net;
using Web.Helpers;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, MyUserClaimsPrincipalFactory>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Authentification/Login");
                options.AccessDeniedPath = new PathString("/Authentification/NotAuthorized");
            });
           // services.AddSession(options => { options.IdleTimeout = TimeSpan.MaxValue; });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc().AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //Authentification 
            services.AddScoped<IAuthentificationService, AuthentificationService>();
            services.AddScoped<IAuthentificationRepository, AuthentificationRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Clients
            services.AddScoped<IAbonnement_ClientService, Abonnement_ClientService>();
            services.AddScoped<IAbonnement_ClientRepository, Abonnement_ClientRepository>();

            //Fournisseur
            services.AddScoped<IFournisseurService, FournisseurService>();
            services.AddScoped<IFournisseurRepository, FournisseurRepository>();

            //FamilleProduits
            services.AddScoped<IFamilleProduitService, FamilleProduitService>();
            services.AddScoped<IFamilleProduitRepository, FamilleProduitRepository>();

            //Stockages
            services.AddScoped<IZoneStockageService, ZoneStockageService>();
            services.AddScoped<IZoneStockageRepository, ZoneStockageRepository>();

            //Point de Vente
            services.AddScoped<IPointVenteService, PointVenteService>();
            services.AddScoped<IPointVenteRespository, PointVenteRepository>();

            //Produit Vendable
            services.AddScoped<IProduitVendableService, ProduitVendableService>();
            services.AddScoped<IProduitVendableRepository, ProduitVendableRepository>();

            //Matiere Premiere
            services.AddScoped<IMatierePremiereService, MatierePremiereService>();
            services.AddScoped<IMatierePremiereRepository, MatierePremiereRepository>();

            //Fcihe Technique
            services.AddScoped<IProduitFicheTechniqueRepository, ProduitFicheTechniqueRepository>();
            services.AddScoped<IProduitFicheTechniqueService, ProduitFicheTechniqueService>();
            
            //Ventes
            services.AddScoped<IVenteProduitsRepository, VenteProduitsRepository>();
            services.AddScoped<IVenteProduitsService, VenteProduitsService>();
            
            //Mouvement stocks
            services.AddScoped<IGestionMouvementService, GestionMouvementService>();
            services.AddScoped<IGestionMouvementRepository, GestionMouvementRepository>();

            services.AddAutoMapper();


            // Real Time Notifications
            services.AddSignalRCore();
            services.AddSignalR();
            // End

            //Session configuration 
            services.AddDistributedMemoryCache();
            services.AddSession();
            //End
            //services.AddPaging();
            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            //Caching In-Memory
            services.AddMemoryCache();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var cultureInfo = new CultureInfo("fr-FR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            //Use Session 
            app.UseSession();
            //End

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Authentification}/{action=Login}/{id?}");
                routes.MapRoute(
                    "404-PageNotFound",
                      "{*url}",
                        new { controller = "Authentification", action = "PageNotFound" }
                        );


            });

        }
    }
}
