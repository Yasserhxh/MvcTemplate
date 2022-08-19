using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.IRepositories;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class GestionMouvementRepository : IGestionMouvementRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork unitOfWork;
        public GestionMouvementRepository(ApplicationDbContext db, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            _db = db;
        }

        public async Task<int?> AlimentationStock(MouvementStock mouvementStock)
        {
            var k = _db.mouvements.Where(m => m.MouvementStock_MatierePremiereStokageId == mouvementStock.MouvementStock_MatierePremiereStokageId).AsEnumerable();
            foreach (var mouvement in k)
            {
                mouvement.MouvementStock_IsActive = 0;
                _db.Entry(mouvement).State = EntityState.Modified;

            }


            mouvementStock.MouvementStock_DateCreation = DateTime.Now;
            mouvementStock.MouvementStock_Date = DateTime.Now;
            mouvementStock.MouvementStock_DateSaisie = DateTime.Now;
            mouvementStock.MouvementStock_MouvementTypeId = 3;
            mouvementStock.MouvementStock_IsActive = 1;
            var stockage = _db.matierePremiereStockages
                .Where(m => m.MatierePremiereStokage_Id == mouvementStock.MouvementStock_MatierePremiereStokageId)
                .Include(x => x.Matiere_Premiere)
                .FirstOrDefault();


            var matiere = _db.matierePremieres.Where(p => p.MatierePremiere_Id == stockage.Matiere_Premiere.MatierePremiere_Id)
                            .Where(p => p.MatierePremiere_IsActive == 1)
                            .FirstOrDefault();


            if ((mouvementStock.MouvementStock_UniteMesureId == 1 && matiere.MatierePremiere_AchatUniteMesureId == 2) || (mouvementStock.MouvementStock_UniteMesureId == 4 && matiere.MatierePremiere_AchatUniteMesureId == 3))
            {
                var qte = mouvementStock.MouvementStock_Quantite /= 1000;
                mouvementStock.MouvementStock_MatiereQuantiteActuelle = stockage.MatierePremiereStokage_QuantiteActuelle + qte;
            }
            else if ((mouvementStock.MouvementStock_UniteMesureId == 2 && matiere.MatierePremiere_AchatUniteMesureId == 1) || (mouvementStock.MouvementStock_UniteMesureId == 3 && matiere.MatierePremiere_AchatUniteMesureId == 4))
            {
                var qte = mouvementStock.MouvementStock_Quantite *= 1000;
                mouvementStock.MouvementStock_MatiereQuantiteActuelle = stockage.MatierePremiereStokage_QuantiteActuelle + qte;

            }
            else
            {
                mouvementStock.MouvementStock_MatiereQuantiteActuelle = stockage.MatierePremiereStokage_QuantiteActuelle + mouvementStock.MouvementStock_Quantite;
            }
            stockage.MatierePremiereStokage_QuantiteActuelle = mouvementStock.MouvementStock_MatiereQuantiteActuelle;
            _db.Entry(matiere).State = EntityState.Modified;
            await _db.mouvements.AddAsync(mouvementStock);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return mouvementStock.MouvementStock_Id;
            else
                return null;
        }

        public async Task<int?> CreateTypeMouvement(MouvementType typeMouvement)
        {
            typeMouvement.MouvementType_DateCreation = DateTime.Now;
            await _db.mouvementTypes.AddAsync(typeMouvement);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return typeMouvement.MouvementType_Id;
            else
                return null;
        }

        public async Task<bool> deleteMouvement(int ID)
        {
            MouvementType mouvement = _db.mouvementTypes.Where(e => e.MouvementType_Id == ID).FirstOrDefault();
            if (mouvement != null)
            {
                _db.Entry(mouvement).State = EntityState.Deleted;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public MouvementType findFormulaireMouvement(int formulaireMouvementId)
        {
            return _db.mouvementTypes.Where(e => e.MouvementType_Id == formulaireMouvementId).FirstOrDefault();
        }

        public IEnumerable<Fournisseur> getListFournisseur(int Id)
        {
            return _db.fournisseurs.Where(a => a.Founisseur_AbonnementId == Id && a.Founisseur_IsActive == 1).AsEnumerable();
        }

        public IEnumerable<MatierePremiere> getListMatierePremiere(int Id)
        {
            return _db.matierePremieres
                .Where(m => m.MatierePremiere_AbonnementId == Id)
                .AsEnumerable();
        }

        public IEnumerable<MatierePremiereStockage> getListMatiereStockage(int Id,int aboId, int adresse)
        {

            var matieresAll = _db.matierePremieres.Include(fm => fm.FournisseurLink).ToList();
            var matieres = new List<MatierePremiere>();
            foreach (var m in matieresAll)
            {
                foreach (var fm in m.FournisseurLink)
                {
                    if (Id==fm.Founisseur_Id)
                    {
                        bool alreadyExists = matieres.Any(x => x.MatierePremiere_Id == m.MatierePremiere_Id);
                        if (!alreadyExists)
                            matieres.Add(m);
                    }
                }
            }
            var matiereStockageAll = _db.matierePremiereStockages.Where(ms => ms.MatierePremiereStokage_AbonnementId == aboId && 
            ms.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == adresse)
                .AsEnumerable();
            var matiereStockageList = new List<MatierePremiereStockage>();
            foreach(var ms in matiereStockageAll)
            {
                foreach(var m in matieres)
                {
                    if (ms.Matiere_Premiere.MatierePremiere_Id==m.MatierePremiere_Id)
                    {
                        matiereStockageList.Add(ms);
                    }
                }
               
            }
            return matiereStockageList;
        }
          
        public IEnumerable<MatierePremiere> getListMatierePremieresBC(int Id,int aboId)
        {

            var matieresAll = _db.matierePremieres.Include(fm => fm.FournisseurLink).ToList();
            var matieres = new List<MatierePremiere>();
            var matsRetturn = new List<MatierePremiere>();
            foreach (var m in matieresAll)
            {
                foreach (var fm in m.FournisseurLink)
                {
                    if (Id==fm.Founisseur_Id)
                    {
                        bool alreadyExists = matieres.Any(x => x.MatierePremiere_Id == m.MatierePremiere_Id);
                        if (!alreadyExists)
                            matieres.Add(m);
                    }
                }
            }
            var matiereAll = _db.matierePremieres.Where(ms => ms.MatierePremiere_AbonnementId == aboId && ms.MatierePremiere_IsActive == 1)
                .AsEnumerable();
            foreach(var ms in matiereAll)
            {
                foreach(var m in matieres)
                {
                    if (ms.MatierePremiere_Id==m.MatierePremiere_Id)
                    {
                        matsRetturn.Add(ms);
                    }
                }
               
            }
            return matsRetturn;
        }

        public IEnumerable<MouvementType> getListMouvement(int Id)
        {
            return _db.mouvementTypes.Where(a => a.MouvementType_AbonnementId == Id).AsEnumerable();
        }

        public async Task<bool> updateFormulaireMouvement(int id, MouvementType newMouvement)
        {
            MouvementType mouvement = _db.mouvementTypes.Where(e => e.MouvementType_Id == id).FirstOrDefault();
            if (mouvement != null)
            {
                mouvement.MouvementType_Libelle = newMouvement.MouvementType_Libelle;

                _db.Entry(mouvement).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<int?> SendStock(MouvementStock mouvementStock, int userlieu)
        {
            var k = _db.mouvements.Where(m => m.MouvementStock_MatierePremiereStokageId == mouvementStock.MouvementStock_MatierePremiereStokageId && m.MouvementStock_IsActive == 1).AsEnumerable();
            foreach (var mouvement in k)
            {
                mouvement.MouvementStock_IsActive = 0;
                _db.Entry(mouvement).State = EntityState.Modified;

            }
            mouvementStock.MouvementStock_MouvementTypeId = 1;
            mouvementStock.MouvementStock_IsActive = 1;
            var stockages = _db.matierePremiereStockages.Where(p=>p.MatierePremiereStokage_IsActive==1)
                .Include(m => m.Section_Stockage).ThenInclude(m => m.Zone_Stockage).ThenInclude(m => m.Lieu_Stockage)
                .Include(x => x.Matiere_Premiere)
                .AsEnumerable();
            var stockage = stockages.Where(p => p.Section_Stockage.Zone_Stockage.Lieu_Stockage.LieuStockag_Id == userlieu && p.MatierePremiereStokage_Id == mouvementStock.MouvementStock_MatierePremiereStokageId).FirstOrDefault();

            var matiere = _db.matierePremieres.Where(p => p.MatierePremiere_Id == stockage.Matiere_Premiere.MatierePremiere_Id)
                            .Where(p => p.MatierePremiere_IsActive == 1)
                            .FirstOrDefault();

            var stocksdesti = stockages.Where(p => p.Section_Stockage.Zone_Stockage.Lieu_Stockage.LieuStockag_Id == mouvementStock.MouvementStock_DestinationStockId && p.MatierePremiereStokage_MatierePremiereId == matiere.MatierePremiere_Id).FirstOrDefault();
            if ((mouvementStock.MouvementStock_UniteMesureId == 1 && matiere.MatierePremiere_AchatUniteMesureId == 2) || (mouvementStock.MouvementStock_UniteMesureId == 4 && matiere.MatierePremiere_AchatUniteMesureId == 3))
            {
                var qte = mouvementStock.MouvementStock_Quantite /= 1000;
                stockage.MatierePremiereStokage_QuantiteActuelle -= qte;
                stocksdesti.MatierePremiereStokage_QuantiteActuelle += qte;
            }
            else if ((mouvementStock.MouvementStock_UniteMesureId == 2 && matiere.MatierePremiere_AchatUniteMesureId == 1) || (mouvementStock.MouvementStock_UniteMesureId == 3 && matiere.MatierePremiere_AchatUniteMesureId == 4))
            {
                var qte = mouvementStock.MouvementStock_Quantite *= 1000;
                stockage.MatierePremiereStokage_QuantiteActuelle -= qte;
                stocksdesti.MatierePremiereStokage_QuantiteActuelle += qte;
            }
            else
            {
                stockage.MatierePremiereStokage_QuantiteActuelle -= mouvementStock.MouvementStock_Quantite;
                stocksdesti.MatierePremiereStokage_QuantiteActuelle += mouvementStock.MouvementStock_Quantite;
            }
            //matiere.MatierePremiere_QuantiteActuelle = mouvementStock.MouvementStock_MatiereQuantiteActuelle;
            _db.Entry(stocksdesti).State = EntityState.Modified;
            _db.Entry(stockage).State = EntityState.Modified;
            
            await _db.mouvements.AddAsync(mouvementStock);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return mouvementStock.MouvementStock_Id;
            else
                return null;
        }
        public IEnumerable<MouvementStock> getlistMouvements(int aboId, int? lieuExpe, int? lieuReception, int? matiere, string date)
        {
            if (date !="")
            {
                if(matiere !=null)
                {
                    if (lieuReception != null)
                    {
                        return _db.mouvements
                            .Where(p => p.MouvementStock_MouvementTypeId == 1 && p.MouvementStock_AbonnementId == aboId && p.MouvementStock_IsActive == 1)
                            .Where(p => p.MatierePremiere_Stokage.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == lieuExpe)
                            .Where(p => p.MouvementStock_DestinationStockId == lieuReception)
                            .Where(p=> Convert.ToDateTime(p.MouvementStock_Date).ToString("yyyy-MM-dd") == date)
                            .Where(p => p.MouvementStock_MatierePremiereStokageId == matiere)
                            .Include(p => p.MatierePremiere_Stokage).ThenInclude(p => p.Matiere_Premiere)
                            .Include(p => p.Unite_Mesure).Include(p=>p.Lieu_Stockage).AsEnumerable();
                    }
                    else
                    {
                        return _db.mouvements
                          .Where(p => p.MouvementStock_MouvementTypeId == 1 && p.MouvementStock_AbonnementId == aboId && p.MouvementStock_IsActive == 1)
                          .Where(p => p.MatierePremiere_Stokage.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == lieuExpe)
                          .Where(p => Convert.ToDateTime(p.MouvementStock_Date).ToString("yyyy-MM-dd") == date)
                          .Where(p => p.MouvementStock_MatierePremiereStokageId == matiere)
                            .Include(p => p.MatierePremiere_Stokage).ThenInclude(p => p.Matiere_Premiere)
                            .Include(p => p.Unite_Mesure).Include(p => p.Lieu_Stockage).AsEnumerable();
                    }
                }
                else
                {
                    if (lieuReception != null)
                    {
                        return _db.mouvements
                            .Where(p => p.MouvementStock_MouvementTypeId == 1 && p.MouvementStock_AbonnementId == aboId && p.MouvementStock_IsActive == 1)
                            .Where(p => p.MatierePremiere_Stokage.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == lieuExpe)
                            .Where(p => p.MouvementStock_DestinationStockId == lieuReception)
                            .Where(p => Convert.ToDateTime(p.MouvementStock_Date).ToString("yyyy-MM-dd") == date)
                            .Include(p => p.MatierePremiere_Stokage).ThenInclude(p => p.Matiere_Premiere)
                            .Include(p => p.Unite_Mesure).Include(p => p.Lieu_Stockage).AsEnumerable();
                    }
                    else
                    {
                        return _db.mouvements
                          .Where(p => p.MouvementStock_MouvementTypeId == 1 && p.MouvementStock_AbonnementId == aboId && p.MouvementStock_IsActive == 1)
                          .Where(p => p.MatierePremiere_Stokage.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == lieuExpe)
                          .Where(p => Convert.ToDateTime(p.MouvementStock_Date).ToString("yyyy-MM-dd") == date)
                            .Include(p => p.MatierePremiere_Stokage).ThenInclude(p => p.Matiere_Premiere)
                            .Include(p => p.Unite_Mesure).Include(p => p.Lieu_Stockage).AsEnumerable();
                    }
                }
            }
            else
            {
                if (matiere != null)
                {
                    if (lieuReception != null)
                    {
                        return _db.mouvements
                            .Where(p => p.MouvementStock_MouvementTypeId == 1 && p.MouvementStock_AbonnementId == aboId && p.MouvementStock_IsActive == 1)
                            .Where(p => p.MatierePremiere_Stokage.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == lieuExpe)
                            .Where(p => p.MouvementStock_DestinationStockId == lieuReception)
                            .Where(p => p.MouvementStock_MatierePremiereStokageId == matiere)
                            .Include(p => p.MatierePremiere_Stokage).ThenInclude(p => p.Matiere_Premiere)
                            .Include(p => p.Unite_Mesure).Include(p => p.Lieu_Stockage).AsEnumerable();
                    }
                    else
                    {
                        return _db.mouvements
                          .Where(p => p.MouvementStock_MouvementTypeId == 1 && p.MouvementStock_AbonnementId == aboId && p.MouvementStock_IsActive == 1)
                          .Where(p => p.MatierePremiere_Stokage.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == lieuExpe)
                          .Where(p => p.MouvementStock_MatierePremiereStokageId == matiere)
                            .Include(p => p.MatierePremiere_Stokage).ThenInclude(p => p.Matiere_Premiere)
                            .Include(p => p.Unite_Mesure).Include(p => p.Lieu_Stockage).AsEnumerable();
                    }
                }
                else
                {
                    if (lieuReception != null)
                    {
                        return _db.mouvements
                            .Where(p => p.MouvementStock_MouvementTypeId == 1 && p.MouvementStock_AbonnementId == aboId && p.MouvementStock_IsActive == 1)
                            .Where(p => p.MatierePremiere_Stokage.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == lieuExpe)
                            .Where(p => p.MouvementStock_DestinationStockId == lieuReception)
                            .Include(p => p.MatierePremiere_Stokage).ThenInclude(p => p.Matiere_Premiere)
                            .Include(p => p.Unite_Mesure).Include(p => p.Lieu_Stockage).AsEnumerable();
                    }
                    else
                    {
                        return _db.mouvements
                          .Where(p => p.MouvementStock_MouvementTypeId == 1 && p.MouvementStock_AbonnementId == aboId && p.MouvementStock_IsActive == 1)
                          .Where(p => p.MatierePremiere_Stokage.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == lieuExpe)
                            .Include(p => p.MatierePremiere_Stokage).ThenInclude(p => p.Matiere_Premiere)
                            .Include(p => p.Unite_Mesure).Include(p => p.Lieu_Stockage).AsEnumerable();
                    }
                }
            }
        }
        public IEnumerable<MatierePremiereStockage> getListMatiereStockageAll(int aboId, int userlieu)
        {
            return _db.matierePremiereStockages.Where(ms => ms.MatierePremiereStokage_AbonnementId == aboId
            && ms.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == userlieu)
                .Include(m => m.Matiere_Premiere)
                .ThenInclude(m => m.Unite_Mesure)
                .AsEnumerable();
        }

        public async Task<int?> ReceptionProduitsPretConso(MouvementProduitsConso mouvementProduitConso)
        {
            var k = _db.mouvementProduits.Where(m => m.MouvementProduitsConso_ProduitConsoId == mouvementProduitConso.MouvementProduitsConso_ProduitConsoId).AsEnumerable();
            foreach (var mouvement in k)
            {
                mouvement.MouvementProduitsConso_IsActive = 0;
                _db.Entry(mouvement).State = EntityState.Modified;

            }


            mouvementProduitConso.MouvementProduitsConso_DateMouvement = DateTime.Now;
            mouvementProduitConso.MouvementProduitsConso_MouvementType = 3;
            mouvementProduitConso.MouvementProduitsConso_IsActive = 1;
            var stockage = _db.produitConsomableStokages
                .Where(m => m.ProduitConsomableStokage_Id == mouvementProduitConso.MouvementProduitsConso_ProduitConsoId)
                .Include(x => x.Produit_PretConsomer)
                .FirstOrDefault();


            var matiere = _db.produitPrets.Where(p => p.ProduitPretConsomer_ID == stockage.Produit_PretConsomer.ProduitPretConsomer_ID)
                            .Where(p => p.ProduitPretConsomer_IsActive == 1)
                            .FirstOrDefault();


            if ((mouvementProduitConso.MouvementProduitsConso_UniteMesureId == 1 && matiere.ProduitPretConsomer_UniteMesureAchatId == 2) || (mouvementProduitConso.MouvementProduitsConso_UniteMesureId == 4 && matiere.ProduitPretConsomer_UniteMesureAchatId == 3))
            {
                var qte = mouvementProduitConso.MouvementProduitsConso_Quantite /= 1000;
                mouvementProduitConso.MouvementProduitsConso_QuantiteActuelle = stockage.ProduitConsomableStokage_QuantiteActuelle + qte;
            }
            else if ((mouvementProduitConso.MouvementProduitsConso_UniteMesureId == 2 && matiere.ProduitPretConsomer_UniteMesureAchatId == 1) || (mouvementProduitConso.MouvementProduitsConso_UniteMesureId == 3 && matiere.ProduitPretConsomer_UniteMesureAchatId == 4))
            {
                var qte = mouvementProduitConso.MouvementProduitsConso_Quantite *= 1000;
                mouvementProduitConso.MouvementProduitsConso_QuantiteActuelle = stockage.ProduitConsomableStokage_QuantiteActuelle + qte;

            }
            else
            {
                mouvementProduitConso.MouvementProduitsConso_QuantiteActuelle = stockage.ProduitConsomableStokage_QuantiteActuelle + mouvementProduitConso.MouvementProduitsConso_Quantite;
            }
            stockage.ProduitConsomableStokage_QuantiteActuelle = mouvementProduitConso.MouvementProduitsConso_QuantiteActuelle;
            _db.Entry(matiere).State = EntityState.Modified;
            await _db.mouvementProduits.AddAsync(mouvementProduitConso);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return mouvementProduitConso.MouvementProduitsConso_Id;
            else
                return null;
        }

        public IEnumerable<ProduitConsomableStokage> getProduitPretConsomers(int Id, int aboId, int atelierId)
        {
            var produitsAll = _db.produitPrets.Include(fm => fm.Fournisseur_Link).ToList();
            var produits = new List<ProduitPretConsomer>();
            foreach (var m in produitsAll)
            {
                foreach (var fm in m.Fournisseur_Link)
                {
                    if (Id == fm.FournisseurProduits_Id)
                    {
                        bool alreadyExists = produits.Any(x => x.ProduitPretConsomer_ID == m.ProduitPretConsomer_ID);
                        if (!alreadyExists)
                            produits.Add(m);
                    }
                }
            }
            var produitStockageAll = _db.produitConsomableStokages.Where(ms => ms.ProduitConsomableStokage_AbonnementId == aboId)
                .Include(ms => ms.Lieu_Stockage).ThenInclude(ms => ms.Ville)
                .Include(ms=>ms.Forme_Produit).ThenInclude(ms=>ms.Produit_PretConsomer)
                .AsEnumerable();
            var produitStockageList = new List<ProduitConsomableStokage>();
            foreach (var ms in produitStockageAll)
            {
                foreach (var m in produits)
                {
                    if (ms.Produit_PretConsomer.ProduitPretConsomer_ID == m.ProduitPretConsomer_ID)
                    {
                        produitStockageList.Add(ms);
                    }
                }

            }
            return produitStockageList.Where(p=>p.ProduitConsomableStokage_LieuStockID ==atelierId && p.ProduitConsomableStokage_IsActive==1 && p.Produit_PretConsomer.ProduitPretConsomer_IsActive==1);
        }
        public IEnumerable<ProduitConsomableStokage> getProduitPretConsomersByPorduit(int Id, int aboId, int atelierUserId, int produitID)
        {
            var produitsAll = _db.produitPrets.Include(fm => fm.Fournisseur_Link).ToList();
            var produits = new List<ProduitPretConsomer>();
            foreach (var m in produitsAll)
            {
                foreach (var fm in m.Fournisseur_Link)
                {
                    if (Id == fm.FournisseurProduits_Id)
                    {
                        bool alreadyExists = produits.Any(x => x.ProduitPretConsomer_ID == m.ProduitPretConsomer_ID);
                        if (!alreadyExists)
                            produits.Add(m);
                    }
                }
            }
            var produitStockageAll = _db.produitConsomableStokages.Where(ms => ms.ProduitConsomableStokage_AbonnementId == aboId)
                .Include(ms => ms.Lieu_Stockage).ThenInclude(ms => ms.Ville)
                .Include(ms=>ms.Forme_Produit).ThenInclude(ms=>ms.Produit_PretConsomer)
                .AsEnumerable();
            var produitStockageList = new List<ProduitConsomableStokage>();
            foreach (var ms in produitStockageAll)
            {
                foreach (var m in produits)
                {
                    if (ms.Produit_PretConsomer.ProduitPretConsomer_ID == m.ProduitPretConsomer_ID)
                    {
                        produitStockageList.Add(ms);
                    }
                }

            }
            var lst = produitStockageList.Where(p=>p.ProduitConsomableStokage_LieuStockID ==atelierUserId && p.ProduitConsomableStokage_IsActive==1 && p.ProduitConsomableStokage_ProduitVendableId==produitID);
            return lst;
        }

        public IEnumerable<FournisseurProduits> getListFournisseurProduits(int Id)
        {
            return _db.fournisseurProduits.Where(f => f.FournisseurProduitConso_AbonnementID == Id && f.FournisseurProduitConso_IsActive == 1).AsEnumerable();
        }
        public IEnumerable<ProduitConsomableStokage> getProduitEnStockByProduit(int produitId,int atelierId)
        {
            return _db.produitConsomableStokages.Where(f => f.ProduitConsomableStokage_ProduitVendableId == produitId && f.ProduitConsomableStokage_LieuStockID == atelierId)
                .Include(p=>p.Forme_Produit)
                .AsEnumerable();
        } 
        public IEnumerable<MouvementProduitsConso> getListMouvementsProduits(int Id, int atelierID,int? produit, string date)
        {
            IEnumerable<MouvementProduitsConso> mouv;
            if (date != "")
            {
                if (produit != null)
                {
                    mouv = _db.mouvementProduits
                        .Where(f => f.MouvementProduitsConso_AbonnementID == Id && f.ProduitConsomable_Stokage.ProduitConsomableStokage_LieuStockID == atelierID && f.MouvementProduitsConso_IsActive == 1)
                        .Where(p=>p.MouvementProduitsConso_ProduitConsoId==produit && Convert.ToDateTime(p.MouvementProduitsConso_DateMouvement).ToString("yyyy-MM-dd")==date)
                        .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p => p.Produit_PretConsomer)
                        .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p => p.Lieu_Stockage)
                        .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p => p.Forme_Produit)
                        .Include(p => p.Unite_Mesure)
                        .AsEnumerable();
                }
                else
                {
                    mouv = _db.mouvementProduits
                        .Where(f => f.MouvementProduitsConso_AbonnementID == Id && f.ProduitConsomable_Stokage.Lieu_Stockage.LieuStockag_Id == atelierID && f.MouvementProduitsConso_IsActive == 1)
                        .Where(p => Convert.ToDateTime(p.MouvementProduitsConso_DateMouvement).ToString("yyyy-MM-dd") == date)
                        .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p => p.Produit_PretConsomer)
                        .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p => p.Lieu_Stockage)
                        .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p => p.Forme_Produit)
                        .Include(p => p.Unite_Mesure)
                        .AsEnumerable();
                }
            }
            else
            {
                if (produit != null)
                {
                    mouv = _db.mouvementProduits
                        .Where(f => f.MouvementProduitsConso_AbonnementID == Id && f.ProduitConsomable_Stokage.Lieu_Stockage.LieuStockag_Id == atelierID && f.MouvementProduitsConso_IsActive == 1)
                        .Where(p => p.MouvementProduitsConso_ProduitConsoId == produit)
                        .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p => p.Produit_PretConsomer)
                        .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p => p.Lieu_Stockage)
                        .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p => p.Forme_Produit)
                        .Include(p => p.Unite_Mesure)
                        .AsEnumerable();
                }
                else
                {
                    mouv = _db.mouvementProduits
                        .Where(f => f.MouvementProduitsConso_AbonnementID == Id && f.ProduitConsomable_Stokage.Lieu_Stockage.LieuStockag_Id == atelierID && f.MouvementProduitsConso_IsActive == 1)
                        .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p => p.Produit_PretConsomer)
                        .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p => p.Lieu_Stockage)
                        .Include(p => p.ProduitConsomable_Stokage).ThenInclude(p => p.Forme_Produit)
                        .Include(p => p.Unite_Mesure)
                        .AsEnumerable();
                }
            }

            return mouv;
        }

        public ProduitConsomableStokage findformulaireProduitStock(int Id, int aboId)
        {
            var prod = _db.produitConsomableStokages.Where(p=>p.ProduitConsomableStokage_AbonnementId== Id && p.ProduitConsomableStokage_Id == aboId).FirstOrDefault();
            return prod;
        }

        public IEnumerable<MatierePremiereStockage> getListMatiereStockageAdmin(int aboId)
        {
            // Random rand = new Random();
            // int toSkip = rand.Next(1, _db.matierePremiereStockages.Count());
            var res =  _db.matierePremiereStockages.Where(ms => ms.MatierePremiereStokage_AbonnementId == aboId)
                .Where(ms => (ms.MatierePremiereStokage_QuantiteActuelle / ms.MatierePremiereStokage_StockMinimum) * 100 < 30)
              .Include(m => m.Matiere_Premiere)
              .ThenInclude(m => m.Unite_Mesure)
              .AsEnumerable();
            return res;
        }

        public async Task<int?> ReceptionStock(Reception_Stock reception)
        { 
            reception.ReceptionStock_DateCreation = DateTime.Now;
            var matStockage = _db.matierePremiereStockages.Where(p => p.MatierePremiereStokage_Id == reception.ReceptionStock_MatiereID).FirstOrDefault();
            var matStock = _db.MatiereStock_Ateliers.Where(p => p.MatiereStockAtelier_MatierePremiereID == matStockage.MatierePremiereStokage_MatierePremiereId)
                .Include(p=>p.Matiere_Premiere).FirstOrDefault();
            if(matStock != null)
            {
                if ((reception.ReceptionStock_UniteID == 1 && matStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 2) || (reception.ReceptionStock_UniteID == 4 && matStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 3))
                {
                    var qte = reception.ReceptionStock_Quantite /= 1000;
                    matStock.MatiereStockAtelier_QauntiteActuelle +=  qte;
                    matStock.MatiereStockAtelier_QuatiteAvecPlanification +=  qte;
                }
                else if ((reception.ReceptionStock_UniteID == 2 && matStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 1) || (reception.ReceptionStock_UniteID == 3 && matStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 4))
                {
                    var qte = reception.ReceptionStock_Quantite *= 1000;
                    matStock.MatiereStockAtelier_QauntiteActuelle += qte;
                    matStock.MatiereStockAtelier_QuatiteAvecPlanification += qte;
                    
                }
                else
                {
                    matStock.MatiereStockAtelier_QauntiteActuelle += reception.ReceptionStock_Quantite;
                    matStock.MatiereStockAtelier_QuatiteAvecPlanification += reception.ReceptionStock_Quantite;
                }
                _db.Entry(matStock).State = EntityState.Modified;

            }
            else
            {
                MatiereStock_Atelier appro = new MatiereStock_Atelier
                {
                    MatiereStockAtelier_AbonnementID = reception.ReceptionStock_AbonnementID,
                    MatiereStockAtelier_AtelierID = reception.ReceptionStock_AtelierID,
                    MatiereStockAtelier_QuatiteAvecPlanification = reception.ReceptionStock_Quantite,
                    MatiereStockAtelier_DateCreation = DateTime.Now,
                    MatiereStockAtelier_QauntiteActuelle = reception.ReceptionStock_Quantite,
                    MatiereStockAtelier_MatierePremiereID = matStockage.MatierePremiereStokage_MatierePremiereId
                };
                await _db.MatiereStock_Ateliers.AddAsync(appro);
            }

            await _db.reception_Stocks.AddAsync(reception);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return reception.ReceptionStock_ID;
            else
                return null;
        }
    }

}
