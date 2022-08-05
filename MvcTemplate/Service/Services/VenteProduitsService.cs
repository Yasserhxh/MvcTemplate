using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.IRepositories;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class VenteProduitsService : IVenteProduitsService
    {
        private readonly IVenteProduitsRepository venteProduitsRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public VenteProduitsService(IMapper mapper, IUnitOfWork unitOfWork, IVenteProduitsRepository venteProduitsRepository)
        {
            this.venteProduitsRepository = venteProduitsRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;

        }
        public async Task<bool> CreateVente(VenteModel venteModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Creer la vente.
                    Vente vente = mapper.Map<VenteModel, Vente>(venteModel);
                    var idvente = await venteProduitsRepository.CreateVente(vente);
                    if (idvente == null)
                    {
                        return false;
                    }
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

        public Task<bool> CreatePerte()
        {
            throw new NotImplementedException();
        }

        public VenteModel findFormulaireVente(int formulaireVenteId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VenteModel> getListVente(int Id, string date)
        {
            return mapper.Map<IEnumerable<Vente>, IEnumerable<VenteModel>>(venteProduitsRepository.getListVentes(Id,date));
        }
        public IEnumerable<VenteModel> getListVenteByPdv(int Id, int pdvid, string date)
        {
            return mapper.Map<IEnumerable<Vente>, IEnumerable<VenteModel>>(venteProduitsRepository.getListVentesByPdv(Id, pdvid, date));
        }

        public async Task<bool> RetourProduit(RetourProduitsModel retourModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Creer la vente.
                    RetourProduits retour = mapper.Map<RetourProduitsModel, RetourProduits>(retourModel);
                    var idretour = await venteProduitsRepository.RetourProduit(retour);
                    if (idretour == null)
                    {
                        return false;
                    }
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

        public IEnumerable<RetourProduitsModel> getListRetours(int Id,int pdvId,string date)
        {
            return mapper.Map<IEnumerable<RetourProduits>, IEnumerable<RetourProduitsModel>>(venteProduitsRepository.getListRetours(Id, pdvId, date));
        }

        public IEnumerable<VenteDetailsModel> getListDetailsVentes(int? pv, string date, int aboId, int? VenteId)
        {
            return mapper.Map<IEnumerable<VenteDetails>, IEnumerable<VenteDetailsModel>>(venteProduitsRepository.getListDetailsVentes(pv,date,aboId,VenteId));
        }

        public IEnumerable<RetourDetailsModel> getListDetails(int Id)
        {
            return mapper.Map<IEnumerable<Retour_Details>, IEnumerable<RetourDetailsModel>>(venteProduitsRepository.getListDetails(Id));
        }

        public async Task<bool?> AlimentationCaisse(AllimentationCaisseModel alimentationModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Creer la vente.
                    AllimentationCaisse alimentation = mapper.Map<AllimentationCaisseModel, AllimentationCaisse>(alimentationModel);
                    var idretour = await venteProduitsRepository.AlimentationCaisse(alimentation);
                    if (idretour == null)
                    {
                        return false;
                    }
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

        public async Task<bool?> RetraitCaisse(RetraitCaisseModel retraitCaisseModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Creer la vente.
                    RetraitCaisse retraitCaisse = mapper.Map<RetraitCaisseModel, RetraitCaisse>(retraitCaisseModel);
                    var idretour = await venteProduitsRepository.RetraitCaisse(retraitCaisse);
                    if (idretour == null)
                    {
                        return false;
                    }
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

        public IEnumerable<AllimentationCaisseModel> getListAlimentations(int Id,int? pdvId, string date)
        {
            return mapper.Map<IEnumerable<AllimentationCaisse>, IEnumerable<AllimentationCaisseModel>>(venteProduitsRepository.getListAlimentations(Id,pdvId,date));
        }
        public IEnumerable<RetraitCaisseModel> getListRetrait(int Id, int? Pdv, string date)
        {
            return mapper.Map<IEnumerable<RetraitCaisse>, IEnumerable<RetraitCaisseModel>>(venteProduitsRepository.getListRetrait(Id,Pdv,date));
        }

        public IEnumerable<RetraitTypeModel> getListRetraitType()
        {
            return mapper.Map<IEnumerable<RetraitType>, IEnumerable<RetraitTypeModel>>(venteProduitsRepository.getListRetraitType());
        }

        public IEnumerable<MouvementCaisseModel> getListMouvementsCaisse(int? pv, string date, int aboId)
        {
            return mapper.Map<IEnumerable<MouvementCaisse>, IEnumerable<MouvementCaisseModel>>(venteProduitsRepository.getListMouvementsCaisse(pv,date,aboId));
        }

        public async Task<bool> ClotureJournee(ClotureJourneeModel clotureModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    ClotureJournee cloture = mapper.Map<ClotureJourneeModel, ClotureJournee>(clotureModel);
                    var idretour = await venteProduitsRepository.ClotureJournee(cloture);
                    if (idretour == null)
                    {
                        return false;
                    }
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

        public IEnumerable<ClotureJourneeModel> getListCloture(int CaisseId,int? Pdv, string date)
        {
            return mapper.Map<IEnumerable<ClotureJournee>, IEnumerable<ClotureJourneeModel>>(venteProduitsRepository.getListCloture(CaisseId,Pdv,date));
        }

        public IEnumerable<MouvementCaisseModel> getListMouvementsCaisseParCaisse(int? caisseId)
        {
            return mapper.Map<IEnumerable<MouvementCaisse>, IEnumerable<MouvementCaisseModel>>(venteProduitsRepository.getListMouvementsCaisseParCaisse(caisseId));
        }

        public async Task<bool> CreateCommade(CommandeModel commandeModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    // Creer la commande.
                    Commande commande = mapper.Map<CommandeModel, Commande>(commandeModel);
                    var idcommande = await venteProduitsRepository.CreateCommade(commande);
                    if (idcommande == null)
                    {
                        return false;
                    }
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

        public IEnumerable<CommandeModel> getListCommandes(int PointVenteId,int? statutLiv,int? statutPaiement,string date)
        {
            return mapper.Map<IEnumerable<Commande>, IEnumerable<CommandeModel>>(venteProduitsRepository.getListCommandes(PointVenteId,statutLiv,statutPaiement,date));
        }

        public IEnumerable<CommandeDetailModel> getListDetailsCommande(int commandeId)
        {
            return mapper.Map<IEnumerable<CommandeDetail>, IEnumerable<CommandeDetailModel>>(venteProduitsRepository.getListDetailsCommande(commandeId));
        }

        public IEnumerable<ClotureJourneeModel> getListCloturePerPdv(int AboId, int Pdv,int? caisseId,string date)
        {
            return mapper.Map<IEnumerable<ClotureJournee>, IEnumerable<ClotureJourneeModel>>(venteProduitsRepository.getListCloturePerPdv(AboId, Pdv,caisseId,date));
        }

        public bool CalculeClotures(int PointVenteId)
        {
            return venteProduitsRepository.CalculeClotures(PointVenteId);
        }

        public IEnumerable<CommandeApiModel> getListCommandesNonPayeeApi(int PointVenteId)
        {
            var commandes = mapper.Map<IEnumerable<Commande>, IEnumerable<CommandeModel>>(venteProduitsRepository.getListCommandesNonPayee(PointVenteId));
            var commandeApis = new List<CommandeApiModel>();
            foreach (var c in commandes)
            {
                var commandeApi = new CommandeApiModel
                {
                    Commande_Id = c.Commande_Id,
                    Commande_DateLivraisonPrevue = c.Commande_DateLivraisonPrevue,
                    Commande_Avance = c.Commande_Avance,
                    Commande_EtatDePaiement = c.Commande_EtatDePaiement,
                    Commande_MontantTotal = c.Commande_MontantTotal,
                    Commande_NomDemandeurs = c.Commande_NomDemandeurs,
                    Commande_NumeroTel = c.Commande_NumeroTel,
                    Commande_NumeroTicket = c.Commande_NumeroTicket,
                    Commande_TauxRemise = c.Commande_TauxdeRemise,
                    Commande_MontantSansRemise = c.Commande_MontantSansRemise,
                    Commande_NomVendeur = c.User.Nom_Complet,
                    commande_EtatDeLivraison = c.Commande_EtatDeLivraison,
                    statut_Livraison = c.Statut_Livraison,
                    statut_Paiement = c.Statut_PaiementCommande,
                    commande_ResteAPayer = Convert.ToDecimal((c.Commande_MontantTotal - c.Commande_Avance).ToString("N1")),

                }; 
                var details = new List<CommandeDetailApiModel>();
                foreach (var d in c.details)
                {
                    var detail = new CommandeDetailApiModel
                    {
                        CommandeDetail_CommandeId = d.CommandeDetail_CommandeId,
                        CommandeDetail_FormeProduit = d.Forme_Produit.FormeProduit_Libelle,
                        CommandeDetail_UniteMesure = d.Unite_Mesure.UniteMesure_Libelle,
                        CommandeDetail_Id = d.CommandeDetail_Id,
                        CommandeDetail_Prix = d.CommandeDetail_Prix,
                        CommandeDetail_Quantite = d.CommandeDetail_Quantite,
                        CommandeDetail_PrixUnitaire = d.CommandeDetail_Prix / d.CommandeDetail_Quantite,
                    };
                    if (d.Forme_Produit.Produit_Vendable != null)
                    { 
                        detail.CommandeDetail_NomProduit = d.Forme_Produit.Produit_Vendable.ProduitVendable_Designation;
                        detail.CommandeDetail_Tva = d.Forme_Produit.Produit_Vendable.Taux_TVA.TauxTVA_Pourcentage;
                    }
                    else if (d.Forme_Produit.Produit_PretConsomer != null)
                    {
                        detail.CommandeDetail_NomProduit = d.Forme_Produit.Produit_PretConsomer.ProduitPretConsomer_Designation;
                        // detail.CommandeDetail_Tva = d.Forme_Produit.Produit_PretConsomer.Taux_TVA.TauxTVA_Pourcentage;
                    }
                    else if (d.Forme_Produit.ProduitPackage != null)
                    {
                        detail.CommandeDetail_NomProduit = d.Forme_Produit.ProduitPackage.ProduitPackage_Designation;
                        //detail.CommandeDetail_Tva = d.Forme_Produit.ProduitPackage.Taux_TVA.TauxTVA_Pourcentage;

                    }
                    details.Add(detail);
                }
                commandeApi.details = details;
                commandeApis.Add(commandeApi);
            }
            return commandeApis;
        }
        public IEnumerable<CommandeModel> getListCommandesNonPayee(int PointVenteId)
        {
            return mapper.Map<IEnumerable<Commande>, IEnumerable<CommandeModel>>(venteProduitsRepository.getListCommandesNonPayee(PointVenteId));
            
        }

        public async Task<bool> PaiementCommande(Commande_PaiementModel Paiementcommande)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    Commande_Paiement Paiement = mapper.Map<Commande_PaiementModel, Commande_Paiement>(Paiementcommande);
                    var idvente = await venteProduitsRepository.PaiementCommande(Paiement);
                    if (idvente == null)
                    {
                        return false;
                    }
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

        public async Task<bool> LivrerCommande(int commandeId,string userId)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    var idcommande = await venteProduitsRepository.LivrerCommande(commandeId,userId);
                    if (idcommande == null)
                    {
                        return false;
                    }
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

        public IEnumerable<Commande_PaiementModel> getListPaiementCommande(int commandeID)
        {
            return mapper.Map<IEnumerable<Commande_Paiement>, IEnumerable<Commande_PaiementModel>>(venteProduitsRepository.getListPaiementCommande(commandeID));
        }

        public async Task<bool> PlanificationDeCloture(int aboId, int PointVenteId)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    var idvente = await venteProduitsRepository.PlanificationDeCloture(aboId, PointVenteId);
                    if (idvente == null)
                    {
                        return false;
                    }
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

        public async Task<bool> CreatePerte(PerteModel perteModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    Perte perte = mapper.Map<PerteModel, Perte>(perteModel);
                    var idperte = await venteProduitsRepository.CreatePerte(perte);
                    if (idperte == null)
                        return false;
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

        public async Task<bool> CreateGratuite(GratuiteModel gratuiteModel)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    Gratuite gratuite = mapper.Map<GratuiteModel, Gratuite>(gratuiteModel);
                    var idgratuite = await venteProduitsRepository.CreateGratuite(gratuite);
                    if (idgratuite == null)
                        return false;
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

        public IEnumerable<PerteModel> getListPertes(int PointVenteId, string date)
        {
            return mapper.Map<IEnumerable<Perte>, IEnumerable<PerteModel>>(venteProduitsRepository.getListPertes(PointVenteId, date));
        }

        public IEnumerable<PerteDetailsModel> getListDetailsPertes(int perteId)
        {
            return mapper.Map<IEnumerable<PerteDetails>, IEnumerable<PerteDetailsModel>>(venteProduitsRepository.getListDetailsPertes(perteId));
        }

        public IEnumerable<GratuiteModel> getListGratuite(int PointVenteId, string date)
        {
            return mapper.Map<IEnumerable<Gratuite>, IEnumerable<GratuiteModel>>(venteProduitsRepository.getListGratuite(PointVenteId, date));
        }

        public IEnumerable<GratuiteDetailsModel> getListDetailsGratuite(int gratuiteId)
        {
            return mapper.Map<IEnumerable<GratuiteDetails>, IEnumerable<GratuiteDetailsModel>>(venteProduitsRepository.getListDetailsGratuite(gratuiteId));
        }

        public IEnumerable<CommandeApiModel> getListCommandesFiltered(int PointVenteId, string date, string nomComandeur)
        {
            var commandes = mapper.Map<IEnumerable<Commande>, IEnumerable<CommandeModel>>(venteProduitsRepository.getListCommandesFiltered(PointVenteId, date, nomComandeur));
            var commandeApis = new List<CommandeApiModel>();
            foreach (var c in commandes)
            {
                var commandeApi = new CommandeApiModel
                {
                    Commande_Id = c.Commande_Id,
                    Commande_DateLivraisonPrevue = c.Commande_DateLivraisonPrevue,
                    Commande_Avance = c.Commande_Avance,
                    Commande_EtatDePaiement = c.Commande_EtatDePaiement,
                    Commande_MontantTotal = c.Commande_MontantTotal,
                    Commande_NomDemandeurs = c.Commande_NomDemandeurs,
                    Commande_TauxRemise = c.Commande_TauxdeRemise,
                    commande_EtatDeLivraison = c.Commande_EtatDeLivraison,
                    Commande_NomVendeur = c.User.Nom_Complet,
                    statut_Livraison = c.Statut_Livraison,
                    statut_Paiement = c.Statut_PaiementCommande,
                    commande_ResteAPayer = Convert.ToDecimal((c.Commande_MontantTotal - c.Commande_Avance).ToString("N1")),

                };
                var details = new List<CommandeDetailApiModel>();
                foreach (var d in c.details)
                {
                    var detail = new CommandeDetailApiModel
                    {
                        CommandeDetail_CommandeId = d.CommandeDetail_CommandeId,
                        CommandeDetail_FormeProduit = d.Forme_Produit.FormeProduit_Libelle,
                        CommandeDetail_UniteMesure = d.Unite_Mesure.UniteMesure_Libelle,
                        CommandeDetail_Id = d.CommandeDetail_Id,
                        CommandeDetail_Prix = d.CommandeDetail_Prix,
                        CommandeDetail_Quantite = d.CommandeDetail_Quantite,
                    };
                    if (d.Forme_Produit.Produit_Vendable != null)
                        detail.CommandeDetail_NomProduit = d.Forme_Produit.Produit_Vendable.ProduitVendable_Designation;
                    else if (d.Forme_Produit.Produit_PretConsomer != null)
                        detail.CommandeDetail_NomProduit = d.Forme_Produit.Produit_PretConsomer.ProduitPretConsomer_Designation;
                    else if (d.Forme_Produit.ProduitPackage != null)
                        detail.CommandeDetail_NomProduit = d.Forme_Produit.ProduitPackage.ProduitPackage_Designation;
                    details.Add(detail);
                }
                commandeApi.details = details;
                commandeApis.Add(commandeApi);
            }
            return commandeApis;
        }

        public decimal GetSoleDebit(int caisseId, string date, int aboId)
        {
            return venteProduitsRepository.GetSoleDebit(caisseId, date, aboId);
        }

        public decimal GetSoleCredit(int caisseId, string date, int aboId)
        {
            return venteProduitsRepository.GetSoleCredit(caisseId, date, aboId);
        }

        public decimal GetSoleDebitBypv(int? pv, string date, int aboId)
        {
            return venteProduitsRepository.GetSoleDebitBypv(pv, date, aboId);
        }

        public decimal GetSoleCreditBypv(int? pv, string date, int aboId)
        {
            return venteProduitsRepository.GetSoleCreditBypv(pv, date, aboId);
        }

        public decimal GetAlimentationBypv(int? pv, string date, int aboId)
        {
            return venteProduitsRepository.GetAlimentationBypv(pv, date, aboId);
        }

        public decimal GetAlimentation(int caisseId, string date, int aboId)
        {
            return venteProduitsRepository.GetAlimentation(caisseId, date, aboId);
        }

        public IEnumerable<ClotureJourneeModel> getListClotureFiltered(int AboId, string date, int? pv)
        {
            return mapper.Map<IEnumerable<ClotureJournee>, IEnumerable<ClotureJourneeModel>>(venteProduitsRepository.getListClotureFiltered(AboId, date, pv));
        }

        public IEnumerable<VenteDetailsModel> getListDetailsVentesApi(int? pv, string date, int aboId, int? VenteId)
        {
            return mapper.Map<IEnumerable<VenteDetails>, IEnumerable<VenteDetailsModel>>(venteProduitsRepository.getListDetailsVentesApi(pv, date, aboId, VenteId));
        }

        public decimal GetSoleDebitBypvApi(int? pv, string date, int aboId)
        {
            return venteProduitsRepository.GetSoleDebitBypvApi(pv, date, aboId);
        }

        public decimal GetSoleCreditBypvApi(int? pv, string date, int aboId)
        {
            return venteProduitsRepository.GetSoleCreditBypvApi(pv, date, aboId);
        }
        

        public IEnumerable<RecettesViewModel> getListRecettes(int AboId, string date, int? pv)
        {
            var ventes = mapper.Map<IEnumerable<Vente>, IEnumerable<VenteModel>>(venteProduitsRepository.GetListVente(pv, AboId, date));
            var paiementCommandes = mapper.Map<IEnumerable<Commande_Paiement>, IEnumerable<Commande_PaiementModel>>(venteProduitsRepository.getListPaiementCommandeByPdv(pv, AboId, date));
            var alim = mapper.Map<IEnumerable<AllimentationCaisse>, IEnumerable<AllimentationCaisseModel>>(venteProduitsRepository.GetListAlimentation(pv, AboId, date));
            var recette = new List<RecettesViewModel>();

            foreach(var a in alim)
            {
                var r = new RecettesViewModel()
                {
                    TypeRecette = "Alimentations de caisse",
                    DateAction  = a.AllimentationCaisse_DateCreation,
                    MontantTotal = a.AllimentationCaisse_Montant

                };
                recette.Add(r);
            }
            foreach(var v in ventes)
            {
                var r = new RecettesViewModel()
                {
                    TypeRecette = "Ventes",
                    DateAction  = v.Vente_Date,
                    MontantTotal = v.Vente_PrixTotalRemise

                };
                recette.Add(r);
            }
            foreach(var p in paiementCommandes)
            {
                var r = new RecettesViewModel()
                {
                    TypeRecette = "Commandes",
                    DateAction  = p.CommandePaiement_DatePaiement,
                    MontantTotal = p.CommandePaiement_Montant

                };
                recette.Add(r);
            }
            return recette;
        }

        public IEnumerable<DepensesViewModel> getListDepenses(int AboId, string date, int? pv)
        {
            var retrait = mapper.Map<IEnumerable<RetraitCaisse>, IEnumerable<RetraitCaisseModel>>(venteProduitsRepository.GetListRetrait(pv, AboId, date));
            var retours = mapper.Map<IEnumerable<RetourProduits>, IEnumerable<RetourProduitsModel>>(venteProduitsRepository.GetListRetours(pv, AboId, date));
            var depense = new List<DepensesViewModel>();

            foreach (var ret in retrait)
            {
                var d = new DepensesViewModel()
                {
                    TypeDepense = "Retrait de caisse",
                    DateAction = ret.RetraitCaisse_DateCreation,
                    MontantTotal = ret.RetraitCaisse_Montant

                };
                depense.Add(d);
            }
            foreach (var ret in retours)
            {
                var d = new DepensesViewModel()
                {
                    TypeDepense = "Retour des produits",
                    DateAction = ret.Retour_DateCreation,
                    MontantTotal = ret.Retour_PrixTotal

                };
                depense.Add(d);
            }
            return depense;
        }

        public async Task<bool> ValiderCloture(List<int> caisses)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    var idvente = await venteProduitsRepository.ValiderCloture(caisses);
                    if (idvente == null)
                    {
                        return false;
                    }
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

        public async Task<bool> UpdateVente(int caisseId, string date)
        {
            using (IDbContextTransaction transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    var idvente = await venteProduitsRepository.UpdateVente(caisseId, date);
                    if (idvente == null)
                    {
                        return false;
                    }
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

        public IEnumerable<Statut_LivraisonModel> getListStatutLivraison()
        {
            return mapper.Map<IEnumerable<Statut_Livraison>, IEnumerable<Statut_LivraisonModel>>(venteProduitsRepository.getListStatutLivraison());
        }

        public IEnumerable<Statut_PaiementCommandeModel> getListStatutPaiement()
        {
            return mapper.Map<IEnumerable<Statut_PaiementCommande>, IEnumerable<Statut_PaiementCommandeModel>>(venteProduitsRepository.getListStatutPaiement());
        }
        public TicketModel GetTicketDetails(string ticketID)
        {
            var vente = venteProduitsRepository.findFormulaireVenteByticket(ticketID);
            var ventedetails = venteProduitsRepository.getListDetailsVentes(null,"",vente.Vente_AbonnementId, vente.Vente_Id);
            //var dett = mapper.Map <IEnumerable<VenteDetails>, IEnumerable<VenteDetailsModel>>(ventedetails);
            //var group = ventedetails.GroupBy(p => new { key = p., item = p.Produit_Vendable.ProduitVendable_Designation });
            var TicketDetails = new List<TicketDetailsModel>();
            var listformes = new List<ticketFormes>();
            foreach (var det in ventedetails)
            {
                /*var formes = new ticketFormes()
                {
                    formeLibelle = det.Forme_Produit.FormeProduit_Libelle,
                    Quantité = det.VenteDetails_Quantite,
                    prixTotal = det.VenteDetails_Prix,
                    prixUnité = det.VenteDetails_Prix / det.VenteDetails_Quantite
                };
                listformes.Add(formes);
           */
                var ticketDet = new TicketDetailsModel()
                {
                   // ProduitVendable = det.Forme_Produit.Produit_Vendable,
                    formeLibelle = det.Forme_Produit.FormeProduit_Libelle,
                    Quantité = det.VenteDetails_Quantite,
                    prixTotal = det.VenteDetails_Prix,
                    prixUnité = det.VenteDetails_Prix / det.VenteDetails_Quantite,
                    //formes = listformes
                };
               

                if (det.Forme_Produit.Produit_Vendable != null)
                {
                    ticketDet.produitImage = det.Forme_Produit.Produit_Vendable.ProduitVendable_GrandePhoto;
                    ticketDet.produitLibelle = det.Forme_Produit.Produit_Vendable.ProduitVendable_Designation;
                }
                if (det.Forme_Produit.ProduitPackage != null)
                {
                    ticketDet.produitImage = det.Forme_Produit.ProduitPackage.ProduitPackage_Photo;
                    ticketDet.produitLibelle = det.Forme_Produit.ProduitPackage.ProduitPackage_Designation;
                }
                if (det.Forme_Produit.Produit_PretConsomer != null)
                {
                    ticketDet.produitImage = det.Forme_Produit.Produit_PretConsomer.ProduitPretConsomer_Photo;
                    ticketDet.produitLibelle = det.Forme_Produit.Produit_PretConsomer.ProduitPretConsomer_Designation;
                }

                TicketDetails.Add(ticketDet);
            }
            var ticket = new TicketModel()
            {
                NumerTicket = ticketID.Split("-").FirstOrDefault(),
                aboID = vente.Vente_AbonnementId,
                AgentID = vente.Vente_UtilisateurId,
                modePaiement = vente.Mode_Paiement.ModePaiement_Libelle,
                VenteDate = vente.Vente_DateCreation,
                ventePrix = vente.Vente_PrixTotalRemise,
                Details = TicketDetails,
                remise = vente.Vente_TauxDeRemise,
                montantsansremise = vente.Vente_Prix,
                tva = mapper.Map<IEnumerable<Tva>, IEnumerable<TvaModel>>(vente.Tva).ToList(),
            };
            return ticket;
        }
        public TicketModel GetTicketDetailsCommande(string ticketID)
        {
            var commande = venteProduitsRepository.findFormulaireCommandeByticket(ticketID);
            var commandeDetails = venteProduitsRepository.getListDetailsCommande(commande.Commande_Id);
            var detailPaiement = (mapper.Map<IEnumerable<Commande_Paiement>, IEnumerable<Commande_PaiementModel>>(venteProduitsRepository.getListPaiementCommande(commande.Commande_Id))).ToList();
            //var dett = mapper.Map <IEnumerable<VenteDetails>, IEnumerable<VenteDetailsModel>>(ventedetails);
            //var group = ventedetails.GroupBy(p => new { key = p., item = p.Produit_Vendable.ProduitVendable_Designation });
            var TicketDetails = new List<TicketDetailsModel>();
            var listformes = new List<ticketFormes>();
            foreach (var det in commandeDetails)
            {
                var formes = new ticketFormes()
                {
                    formeLibelle = det.Forme_Produit.FormeProduit_Libelle,
                    Quantité = det.CommandeDetail_Quantite,
                    prixTotal = det.CommandeDetail_Prix,
                    prixUnité = det.CommandeDetail_Prix / det.CommandeDetail_Quantite
                };
                listformes.Add(formes);

                var ticketDet = new TicketDetailsModel()
                {
                    // ProduitVendable = det.Forme_Produit.Produit_Vendable,
                    formeLibelle = det.Forme_Produit.FormeProduit_Libelle,
                    Quantité = det.CommandeDetail_Quantite,
                    prixTotal = det.CommandeDetail_Prix,
                    prixUnité = det.CommandeDetail_Prix / det.CommandeDetail_Quantite,
                    //formes = listformes
                };


                if (det.Forme_Produit.Produit_Vendable != null)
                {
                    ticketDet.produitImage = det.Forme_Produit.Produit_Vendable.ProduitVendable_GrandePhoto;
                    ticketDet.produitLibelle = det.Forme_Produit.Produit_Vendable.ProduitVendable_Designation;
                }
                if (det.Forme_Produit.ProduitPackage != null)
                {
                    ticketDet.produitImage = det.Forme_Produit.ProduitPackage.ProduitPackage_Photo;
                    ticketDet.produitLibelle = det.Forme_Produit.ProduitPackage.ProduitPackage_Designation;
                }
                if (det.Forme_Produit.Produit_PretConsomer != null)
                {
                    ticketDet.produitImage = det.Forme_Produit.Produit_PretConsomer.ProduitPretConsomer_Photo;
                    ticketDet.produitLibelle = det.Forme_Produit.Produit_PretConsomer.ProduitPretConsomer_Designation;
                }

                TicketDetails.Add(ticketDet);
            }
            var ticket = new TicketModel()
            {
                NumerTicket = ticketID.Split("-").FirstOrDefault(),
                aboID = commande.Commande_AbonnementId,
                AgentID = commande.User.Id,
                //modePaiement = commande.mo.ModePaiement_Libelle,
                CommandeDateLiv = commande.Commande_DateLivraisonPrevue,
                Avance = detailPaiement.Sum(p => p.CommandePaiement_Montant),
                ventePrix = commande.Commande_MontantTotal,
                Details = TicketDetails,
                PaiementDetail = detailPaiement,
                remise = commande.Commande_TauxdeRemise,
                montantsansremise = commande.Commande_MontantSansRemise,
                tva = mapper.Map<IEnumerable<Tva>, IEnumerable<TvaModel>>(commande.Tva).ToList(),
                commandepaiement = commande.Statut_PaiementCommande.StatutPaiementCommande_Libelle,
                commandeLiv = commande.Statut_Livraison.StatutCommande_Libelle,
        };
            /*if (commande.Commande_EtatDePaiement == 1)
                ticket.commandepaiement = "Oui";
            else if (commande.Commande_EtatDePaiement == 2)
                ticket.commandepaiement = "Non";
            else
                ticket.commandepaiement = "AVANCE";*/
/*
            if (commande.Commande_EtatDeLivraison == 1)
                ticket.commandeLiv = "Non";
            else
                ticket.commandeLiv = "Oui";*/

            ticket.Reste = ticket.ventePrix - ticket.Avance;

            return ticket;
        }

        public Task<bool?> UpdateProduitsPDV(List<ProdsQteApi> prodsList, int pdvID)
        {
            return this.venteProduitsRepository.UpdateProduitsPDV(prodsList, pdvID);
        }
    }
}
