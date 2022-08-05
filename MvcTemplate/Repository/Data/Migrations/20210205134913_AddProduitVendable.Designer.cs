﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository.Data;

namespace Service.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210205134913_AddProduitVendable")]
    partial class AddProduitVendable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Authentication.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Abonnement_ID");

                    b.Property<int?>("Abonnement_ISACTIVE");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("Abonnement_ID");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Domain.Entities.Abonnement_Client", b =>
                {
                    b.Property<int>("Abonnement_ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abonnement_Adresse")
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Abonnement_ContactEmail")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Abonnement_ContactNomPrenom")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Abonnement_ContactTelephone")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Abonnement_ICE")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Abonnement_IdentifiantFiscal")
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Abonnement_IsActive");

                    b.Property<string>("Abonnement_NomClient")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Abonnement_RegistreCommercial")
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Abonnement_Telephone")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Abonnement_VilleId")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 64)))
                        .HasColumnType("nvarchar(4)");

                    b.HasKey("Abonnement_ID");

                    b.ToTable("Abonnement_Client");
                });

            modelBuilder.Entity("Domain.Entities.FamilleProduit", b =>
                {
                    b.Property<int>("FamilleProduit_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FamilleProduit_AbonnemnetId");

                    b.Property<DateTime>("FamilleProduit_DateCreation")
                        .HasColumnType("smalldatetime");

                    b.Property<int>("FamilleProduit_IsActive");

                    b.Property<string>("FamilleProduit_Libelle")
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("FamilleProduit_ParentId")
                        .HasColumnType("int");

                    b.HasKey("FamilleProduit_Id");

                    b.ToTable("Famille_Produit");
                });

            modelBuilder.Entity("Domain.Entities.Forme_Stockage", b =>
                {
                    b.Property<int>("FormStockage_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FormStockage_AbonnementId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FormStockage_DateCreation")
                        .HasColumnType("smalldatetime");

                    b.Property<string>("FormStockage_Libelle")
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("FormStockage_Id");

                    b.ToTable("Forme_Stockage");
                });

            modelBuilder.Entity("Domain.Entities.Fournisseur", b =>
                {
                    b.Property<int>("Founisseur_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Founisseur_AbonnementId")
                        .HasColumnType("int");

                    b.Property<string>("Founisseur_Adresse")
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime?>("Founisseur_DateCreation")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("Founisseur_DateSaisie")
                        .HasColumnType("smalldatetime");

                    b.Property<string>("Founisseur_ICE")
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("Founisseur_IsActive");

                    b.Property<string>("Founisseur_NomContact")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Founisseur_RaisonSocial")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Founisseur_Telephone")
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("Founisseur_UtilisateurId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Founisseur_Id");

                    b.ToTable("Founisseur");
                });

            modelBuilder.Entity("Domain.Entities.Lieu_Stockage", b =>
                {
                    b.Property<int>("LieuStockag_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LieuStockag_AbonnementId")
                        .HasColumnType("int");

                    b.Property<string>("LieuStockag_Adresse")
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("LieuStockag_DateCreation")
                        .HasColumnType("smalldatetime");

                    b.Property<int>("LieuStockag_IsActive");

                    b.Property<string>("LieuStockag_Nom")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("LieuStockag_NomResponsable")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("LieuStockag_Telephone")
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("LieuStockag_Id");

                    b.ToTable("Lieu_Stockage");
                });

            modelBuilder.Entity("Domain.Entities.Paiement_Abonnement", b =>
                {
                    b.Property<int>("PaiementAbonnement_ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PaiementAbonnement_AbonnementId");

                    b.Property<Guid>("PaiementAbonnement_AdminUserId")
                        .HasColumnType("uniqueidentifier(16)");

                    b.Property<DateTime?>("PaiementAbonnement_Date")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("PaiementAbonnement_DateDebutPeriode")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("PaiementAbonnement_DateFinPeriode")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("PaiementAbonnement_DateSaisie")
                        .HasColumnType("smalldatetime");

                    b.Property<decimal>("PaiementAbonnemet_Montant")
                        .HasColumnType("decimal(8,2)");

                    b.HasKey("PaiementAbonnement_ID");

                    b.ToTable("Paiemenet_Abonnement");
                });

            modelBuilder.Entity("Domain.Entities.Point_Vente", b =>
                {
                    b.Property<int>("PointVente_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PointVente_AbonnementId")
                        .HasColumnType("int");

                    b.Property<string>("PointVente_Adresse")
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("PointVente_DateCreation")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("PointVente_DateSaisie")
                        .HasColumnType("smalldatetime");

                    b.Property<int>("PointVente_IsActive");

                    b.Property<string>("PointVente_Nom")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("PointVente_NomResponsable")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("PointVente_Telephone")
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("PointVente_UtilisateurId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PointVente_Id");

                    b.ToTable("Point_Vente");
                });

            modelBuilder.Entity("Domain.Entities.ProduitVendable", b =>
                {
                    b.Property<int>("ProduitVendable_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FORMEFormStockage_Id");

                    b.Property<int?>("FamilleProduit_Id");

                    b.Property<int?>("ProduitVendable_AbonnementId")
                        .HasColumnType("int");

                    b.Property<string>("ProduitVendable_CodeBarre")
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime?>("ProduitVendable_DateCreation")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("ProduitVendable_DelaiConsommation")
                        .HasColumnType("int");

                    b.Property<string>("ProduitVendable_Description")
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ProduitVendable_Designation")
                        .HasColumnType("nvarchar(150)");

                    b.Property<int?>("ProduitVendable_FamilleProduitId");

                    b.Property<int?>("ProduitVendable_FormStockageId");

                    b.Property<string>("ProduitVendable_GrandePhoto")
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("ProduitVendable_IsActive");

                    b.Property<string>("ProduitVendable_PetitePhoto")
                        .HasColumnType("nvarchar(300)");

                    b.Property<decimal?>("ProduitVendable_StockMaximum")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal?>("ProduitVendable_StockMinimum")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int?>("ProduitVendable_UniteMesureId");

                    b.Property<int?>("ProduitVendable_UniteMesureId_FT")
                        .HasColumnType("int");

                    b.Property<int?>("UniteMesure_Id");

                    b.HasKey("ProduitVendable_Id");

                    b.HasIndex("FORMEFormStockage_Id");

                    b.HasIndex("FamilleProduit_Id");

                    b.HasIndex("UniteMesure_Id");

                    b.ToTable("Produit_Vendable");
                });

            modelBuilder.Entity("Domain.Entities.Section_Stockage", b =>
                {
                    b.Property<int>("Section_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Section_Designation")
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("Section_IsActive");

                    b.Property<int>("Section_ZoneStockageId");

                    b.HasKey("Section_Id");

                    b.ToTable("Section_Stockage");
                });

            modelBuilder.Entity("Domain.Entities.Type_Contenu", b =>
                {
                    b.Property<int>("TypeContenu_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TypeContenu_Libelle")
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("TypeContenu_Id");

                    b.ToTable("Type_Contenu");
                });

            modelBuilder.Entity("Domain.Entities.Unite_Mesure", b =>
                {
                    b.Property<int>("UniteMesure_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UniteMesure_Libelle")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UniteMesure_Id");

                    b.ToTable("Unite_Mesure");
                });

            modelBuilder.Entity("Domain.Entities.Zone_Stockage", b =>
                {
                    b.Property<int>("ZoneStockage_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FORMEFormStockage_Id");

                    b.Property<int?>("LieuStockag_Id");

                    b.Property<int?>("TypeContenu_Id");

                    b.Property<int?>("UniteMesure_Id");

                    b.Property<int?>("ZoneStockage_AbonnementId")
                        .HasColumnType("int");

                    b.Property<decimal?>("ZoneStockage_CapaciteStockage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("ZoneStockage_DateCreation")
                        .HasColumnType("smalldatatime");

                    b.Property<int?>("ZoneStockage_FormeStockageId");

                    b.Property<int?>("ZoneStockage_IsActive");

                    b.Property<int?>("ZoneStockage_LieuStockageId");

                    b.Property<int?>("ZoneStockage_TypeContenuId");

                    b.Property<int?>("ZoneStockage_UniteMesureId");

                    b.HasKey("ZoneStockage_Id");

                    b.HasIndex("FORMEFormStockage_Id");

                    b.HasIndex("LieuStockag_Id");

                    b.HasIndex("TypeContenu_Id");

                    b.HasIndex("UniteMesure_Id");

                    b.ToTable("Zone_Stockage");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Domain.Authentication.ApplicationUser", b =>
                {
                    b.HasOne("Domain.Entities.Abonnement_Client", "abonnement_Client")
                        .WithMany()
                        .HasForeignKey("Abonnement_ID");
                });

            modelBuilder.Entity("Domain.Entities.ProduitVendable", b =>
                {
                    b.HasOne("Domain.Entities.Forme_Stockage", "FORME")
                        .WithMany()
                        .HasForeignKey("FORMEFormStockage_Id");

                    b.HasOne("Domain.Entities.FamilleProduit", "FAMILLE")
                        .WithMany()
                        .HasForeignKey("FamilleProduit_Id");

                    b.HasOne("Domain.Entities.Unite_Mesure", "UNITE")
                        .WithMany()
                        .HasForeignKey("UniteMesure_Id");
                });

            modelBuilder.Entity("Domain.Entities.Zone_Stockage", b =>
                {
                    b.HasOne("Domain.Entities.Forme_Stockage", "FORME")
                        .WithMany()
                        .HasForeignKey("FORMEFormStockage_Id");

                    b.HasOne("Domain.Entities.Lieu_Stockage", "LIEU")
                        .WithMany()
                        .HasForeignKey("LieuStockag_Id");

                    b.HasOne("Domain.Entities.Type_Contenu", "TYPE")
                        .WithMany()
                        .HasForeignKey("TypeContenu_Id");

                    b.HasOne("Domain.Entities.Unite_Mesure", "UNITE")
                        .WithMany()
                        .HasForeignKey("UniteMesure_Id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Domain.Authentication.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Domain.Authentication.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Authentication.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Domain.Authentication.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
