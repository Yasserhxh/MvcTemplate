using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
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
    public class MatierePremiereRepository : IMatierePremiereRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork unitOfWork;

        public MatierePremiereRepository(ApplicationDbContext db, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            _db = db;
        }

        public async Task<int?> CreateMatiere(MatierePremiere matierePremiere, List<int> ListeUnite)
        {
            matierePremiere.MatierePremiere_IsActive = 1;
            matierePremiere.MatierePremiere_DateCreation = DateTime.Now;
            // matierePremiere.MatierePremiere_QuantiteActuelle = matierePremiere.MatierePremiere_Quantite_FT;
            await _db.matierePremieres.AddAsync(matierePremiere);
            await unitOfWork.Complete();
            var confirm = await AjouterUnites(matierePremiere.MatierePremiere_Id, ListeUnite);
            if (confirm > 0)
                return matierePremiere.MatierePremiere_Id;
            else
                return null;
        }

        public async Task<int?> CreateAllergene(Allergene allergene)
        {
            allergene.Allergene_IsActive = 1;
            await _db.allergenes.AddAsync(allergene);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return allergene.Allergene_Id;
            else
                return null;
        }

        public async Task<bool> deleteMatiereP(int ID)
        {
            var ft = _db.produitFicheTechniques.Where(p => p.FicheTechnique_MatierePremiereId == ID && p.FicheTechnique_Bridge.FicheTechniqueBridge_IsActive==1)
                .FirstOrDefault();
            if (ft != null)
                return false;
            MatierePremiere matiere = _db.matierePremieres.Where(e => e.MatierePremiere_Id == ID).FirstOrDefault();
            if (matiere != null)
            {
                matiere.MatierePremiere_IsActive = 0;
                _db.Entry(matiere).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public MatierePremiere findFormulaireMatiereP(int aboID, int formulaireMatierePId)
        {
            return _db.matierePremieres.Where(e => e.MatierePremiere_Id == formulaireMatierePId && e.MatierePremiere_AbonnementId == aboID && e.MatierePremiere_IsActive == 1).FirstOrDefault();
        }

        public IEnumerable<Allergene> getListAllergene(int Id)
        {
            return _db.allergenes.Where(a => a.Allergene_IsActive == 1).Where(a => a.Allergene_AbonnementId == Id).AsEnumerable();
        }

        public IEnumerable<MatierePremiere> getListMatierePremiere(int Id,int? allergene,int?formeId)
        {
            IEnumerable<MatierePremiere> m;
            if (allergene != null)
            {
                if (formeId != null)
                {
                    m = _db.matierePremieres.Where(a => a.MatierePremiere_IsActive == 1 && a.MatierePremiere_AllergeneId==allergene && a.MatierePremiere_FormeStockageId==formeId)
                                    .Where(a => a.MatierePremiere_AbonnementId == Id)
                                    .Include(e => e.Allergene)
                                    .Include(e => e.Forme_Stockage)
                                    .Include(e => e.Unite_Mesure)
                                    .Include(e => e.Matiere_Famille).ThenInclude(e => e.MatiereFamille_Parent)
                                    .AsEnumerable();
                }
                else
                {
                    m = _db.matierePremieres.Where(a => a.MatierePremiere_IsActive == 1 && a.MatierePremiere_AllergeneId==allergene)
                                    .Where(a => a.MatierePremiere_AbonnementId == Id)
                                    .Include(e => e.Allergene)
                                    .Include(e => e.Forme_Stockage)
                                    .Include(e => e.Unite_Mesure)
                                    .Include(e => e.Matiere_Famille).ThenInclude(e => e.MatiereFamille_Parent)
                                    .AsEnumerable();
                }
            }
            else
            {
                if (formeId != null)
                {
                    m = _db.matierePremieres.Where(a => a.MatierePremiere_IsActive == 1 && a.MatierePremiere_FormeStockageId==formeId)
                                    .Where(a => a.MatierePremiere_AbonnementId == Id)
                                    .Include(e => e.Allergene)
                                    .Include(e => e.Forme_Stockage)
                                    .Include(e => e.Unite_Mesure)
                                    .Include(e => e.Matiere_Famille).ThenInclude(e => e.MatiereFamille_Parent)
                                    .AsEnumerable();
                }
                else
                {
                    m = _db.matierePremieres.Where(a => a.MatierePremiere_IsActive == 1)
                                    .Where(a => a.MatierePremiere_AbonnementId == Id)
                                    .Include(e => e.Allergene)
                                    .Include(e => e.Forme_Stockage)
                                    .Include(e => e.Unite_Mesure)
                                    .Include(e => e.Matiere_Famille).ThenInclude(e => e.MatiereFamille_Parent)
                                    .AsEnumerable();
                }
            }
                
            return m;
        }

        public async Task<bool> updateFormulaireMatierePremiere(int id, MatierePremiere newMatiere)
        {
            MatierePremiere matiere = _db.matierePremieres.Where(e => e.MatierePremiere_Id == id).FirstOrDefault();
            if (matiere != null)
            {
                matiere.MatierePremiere_Libelle = newMatiere.MatierePremiere_Libelle;
                matiere.MatierePremiere_AchatUniteMesureId = newMatiere.MatierePremiere_AchatUniteMesureId;
                matiere.MatierePremiere_Quantite_FT = newMatiere.MatierePremiere_Quantite_FT;
                matiere.MatierePremiere_UniteMesureId_FT = newMatiere.MatierePremiere_UniteMesureId_FT;
                matiere.MatierePremiere_PourcentrageTolerancePerte = newMatiere.MatierePremiere_PourcentrageTolerancePerte;
                if (matiere.MatierePremiere_PrixMoyenAchat != newMatiere.MatierePremiere_PrixMoyenAchat)
                {
                    var res = await updatePrixMoyen(id, newMatiere.MatierePremiere_PrixMoyenAchat);
                    if (res == true)
                        matiere.MatierePremiere_PrixMoyenAchat = newMatiere.MatierePremiere_PrixMoyenAchat;
                    else
                        return false;

                }
                matiere.MatierePremiere_FormeStockageId = newMatiere.MatierePremiere_FormeStockageId;
                matiere.MatierePremiere_EstAllergene = newMatiere.MatierePremiere_EstAllergene;
                matiere.MatierePremiere_AllergeneId = newMatiere.MatierePremiere_AllergeneId;
                matiere.MatierePremiere_IsActive = 1;
                _db.Entry(matiere).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public Allergene findFormulaireAllergene(int formulaireAllergieId)
        {
            return _db.allergenes.Where(e => e.Allergene_Id == formulaireAllergieId).FirstOrDefault();
        }

        public async Task<bool> updateFormulaireAllergie(int id, Allergene newAllergie)
        {
            Allergene allergie = _db.allergenes.Where(e => e.Allergene_Id == id).FirstOrDefault();
            if (allergie != null)
            {
                allergie.Allergene_Libelle = newAllergie.Allergene_Libelle;
                allergie.Allergene_IsActive = 1;
                _db.Entry(allergie).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> updatePrixMoyen(int matiereID, decimal newPrixMoyen)
        {
            var matiere = _db.matierePremieres.Where(p => p.MatierePremiere_Id == matiereID).FirstOrDefault();
            var ficheTech = _db.produitFicheTechniques.Where(p => p.FicheTechnique_MatierePremiereId == matiereID)
                .Include(p=>p.FicheTechnique_Bridge).ThenInclude(p=>p.Produit_FicheTechnique)
                .Include(p=>p.FicheTechnique_Bridge).ThenInclude(p=>p.Fiche_Forme).ThenInclude(p=>p.Forme_Produit)
                .AsEnumerable();
            if(ficheTech != null)
            {
                foreach(var item in ficheTech)
                {
                    var prod = _db.produitVendables.Where(p => p.ProduitVendable_Id == item.FicheTechnique_Bridge.FicheTechniqueBridge_ProduitVendableID).FirstOrDefault();
                    item.FicheTechnique_Prix = item.FicheTechnique_QuantiteMatiere * newPrixMoyen;
                    if (item.FicheTechnique_UniteMesureId == 1 && matiere.MatierePremiere_AchatUniteMesureId == 2 || item.FicheTechnique_UniteMesureId == 4 && matiere.MatierePremiere_AchatUniteMesureId == 3)
                        item.FicheTechnique_Prix /=  1000;
                    else if (item.FicheTechnique_UniteMesureId == 2 && matiere.MatierePremiere_AchatUniteMesureId == 1 || item.FicheTechnique_UniteMesureId == 3 && matiere.MatierePremiere_AchatUniteMesureId == 4)
                        item.FicheTechnique_Prix *= 1000;
                    item.FicheTechnique_Bridge.FicheTechniqueBridge_CoutDeRevient = item.FicheTechnique_Bridge.Produit_FicheTechnique.Sum(p => p.FicheTechnique_Prix);// ficheTech.Sum(p => p.FicheTechnique_Prix);
                    foreach(var form in item.FicheTechnique_Bridge.Fiche_Forme)
                    {
                        form.FicheForme_CoutDeRevient = item.FicheTechnique_Bridge.FicheTechniqueBridge_CoutDeRevient / form.FicheForme_Quantite;
                        form.Forme_Produit.FormeProduit_CoutDeRevient = form.FicheForme_CoutDeRevient;
                        _db.Entry(form).State = EntityState.Modified;
                    }
                    prod.Produit_CoutDeRevient = item.FicheTechnique_Bridge.FicheTechniqueBridge_CoutDeRevient;
                    _db.Entry(item).State = EntityState.Modified;
                    _db.Entry(prod).State = EntityState.Modified;

                }
                // _db.Entry(ficheTech).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;

            }
        }
        public async Task<bool> deleteAllergie(int ID)
        {
            Allergene allergie = _db.allergenes.Where(e => e.Allergene_Id == ID).FirstOrDefault();
            if (allergie != null)
            {
                allergie.Allergene_IsActive = 0;
                _db.Entry(allergie).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public IEnumerable<Forme_Stockage> getListFormeStockage(int Id)
        {
            //return _db.forme_Stockages.Where(a => a.FormStockage_AbonnementId == Id).AsEnumerable();
            return _db.forme_Stockages.AsEnumerable();
        }

        public IEnumerable<ProduitFicheTechnique> getListFicheTechnique(int Id)
        {
            return _db.produitFicheTechniques.Where(a => a.FicheTechnique_IsActive == 1).Where(a => a.FicheTechnique_AbonnementId == Id).Include(e => e.Unite_Mesure).AsEnumerable();
        }

        public async Task<int?> StockerMatiere(int Id, MatierePremiereStockage matierePremiereStockage)
        {
            if (matierePremiereStockage.MatierePremiereStokage_StockMinimum > matierePremiereStockage.MatierePremiereStokage_StockMaximum)
            {
                return null;
            }
            var matieretest = _db.matierePremiereStockages.Where(m => m.MatierePremiereStokage_MatierePremiereId == Id).Where(m=>m.MatierePremiereStokage_SectionStockageId==matierePremiereStockage.MatierePremiereStokage_SectionStockageId)
                .Include(m=>m.Section_Stockage).ThenInclude(m=>m.Zone_Stockage).ThenInclude(m=>m.Lieu_Stockage)
                .FirstOrDefault();
            if (matieretest == null)
            {
                matierePremiereStockage.MatierePremiereStokage_MatierePremiereId = Id;
                matierePremiereStockage.MatierePremiereStokage_IsActive = 1;
                matierePremiereStockage.MatierePremiereStokage_DateCreation = DateTime.Now;
                MatierePremiere matiere = _db.matierePremieres.Where(e => e.MatierePremiere_Id == Id).FirstOrDefault();
                if (matiere != null)
                {
                    matiere.EnStock = true;
                    _db.Entry(matiere).State = EntityState.Modified;

                }
                await _db.matierePremiereStockages.AddAsync(matierePremiereStockage);
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return matierePremiereStockage.MatierePremiereStokage_Id;
                else
                    return null;

            }
            else
            {
                return null;
            }

        }

        public IEnumerable<MatierePremiereStockage> getListMatieresStockes(int Id, int MatierePremiereStokage_Id)
        {
            var m = _db.matierePremiereStockages.Where(a => a.MatierePremiereStokage_IsActive == 1)
                .Where(a => a.MatierePremiereStokage_AbonnementId == Id && a.MatierePremiereStokage_Id == MatierePremiereStokage_Id)
                .Include(e => e.Matiere_Premiere).ThenInclude(p=>p.Unite_Mesure)
                .Include(e => e.Section_Stockage)
                .ThenInclude(w => w.Zone_Stockage)
                .ThenInclude(w => w.Lieu_Stockage)
                .AsEnumerable();
            return m;
        }  
        public IEnumerable<MatierePremiereStockage> getListMatieresStockesAll(int Id, int lieuId,int? zone,int? section)
        {
            IEnumerable<MatierePremiereStockage> m;
            if (zone != null)
            {
                if (section != null)
                {
                    m = _db.matierePremiereStockages.Where(a => a.MatierePremiereStokage_IsActive == 1)
                                    .Where(a => a.MatierePremiereStokage_AbonnementId == Id && a.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == lieuId && a.MatierePremiereStokage_SectionStockageId==section)
                                    .Include(e => e.Matiere_Premiere).ThenInclude(p => p.Unite_Mesure)
                                    .Include(e => e.Section_Stockage)
                                    .ThenInclude(w => w.Zone_Stockage)
                                    .ThenInclude(w => w.Lieu_Stockage)
                                    .AsEnumerable();
                }
                else
                {
                    m = _db.matierePremiereStockages.Where(a => a.MatierePremiereStokage_IsActive == 1)
                                    .Where(a => a.MatierePremiereStokage_AbonnementId == Id && a.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == lieuId && a.Section_Stockage.Section_ZoneStockageId == zone)
                                    .Include(e => e.Matiere_Premiere).ThenInclude(p => p.Unite_Mesure)
                                    .Include(e => e.Section_Stockage)
                                    .ThenInclude(w => w.Zone_Stockage)
                                    .ThenInclude(w => w.Lieu_Stockage)
                                    .AsEnumerable();
                }
            }
            else
            {
                if (section != null)
                {
                    m = _db.matierePremiereStockages.Where(a => a.MatierePremiereStokage_IsActive == 1)
                                    .Where(a => a.MatierePremiereStokage_AbonnementId == Id && a.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId == lieuId && a.MatierePremiereStokage_SectionStockageId == section)
                                    .Include(e => e.Matiere_Premiere).ThenInclude(p => p.Unite_Mesure)
                                    .Include(e => e.Section_Stockage)
                                    .ThenInclude(w => w.Zone_Stockage)
                                    .ThenInclude(w => w.Lieu_Stockage)
                                    .AsEnumerable();
                }
                else
                {
                    m = _db.matierePremiereStockages.Where(a => a.MatierePremiereStokage_IsActive == 1)
                                    .Where(a => a.MatierePremiereStokage_AbonnementId == Id && a.Section_Stockage.Zone_Stockage.ZoneStockage_LieuStockageId== lieuId)
                                    .Include(e => e.Matiere_Premiere).ThenInclude(p => p.Unite_Mesure)
                                    .Include(e => e.Section_Stockage)
                                    .ThenInclude(w => w.Zone_Stockage)
                                    .ThenInclude(w => w.Lieu_Stockage)
                                    .AsEnumerable();
                }
            }
            
            return m;
        }

        public async Task<bool> deleteMatierePremiereStockes(int ID)
        {
            MatierePremiereStockage matieretStocke = _db.matierePremiereStockages.Where(e => e.MatierePremiereStokage_Id == ID).FirstOrDefault();
            if (matieretStocke != null)
            {
                matieretStocke.MatierePremiereStokage_IsActive = 0;
                _db.Entry(matieretStocke).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public IEnumerable<Section_Stockage> getListSectionStockage()
        {
            return _db.section_Stockages.Where(a => a.Section_IsActive == 1).AsEnumerable();
        }

        public bool MatiereStocker(int id)
        {
            var res = _db.matierePremiereStockages.Where(p => p.MatierePremiereStokage_MatierePremiereId == id).AsEnumerable();
            if (res != null)
            {
                return true;
            }
            return false;
        }

        public decimal CalculerPrix(int FormMatiereId, int FormUnite, decimal FormQuantite)
        {
            MatierePremiere matiere = _db.matierePremieres.Where(m => m.MatierePremiere_Id == FormMatiereId).FirstOrDefault();
            var prixM = matiere.MatierePremiere_PrixMoyenAchat;
            var uniteM = matiere.MatierePremiere_AchatUniteMesureId;
            var qteM = matiere.MatierePremiere_Quantite_FT;

            if ((uniteM == 1 && FormUnite == 2) || (uniteM == 4 && FormUnite == 3))
            {
                var qteCal = FormQuantite * 1000;
                var prixCal = qteCal * prixM;
                return prixCal / 1;
            }
            else if ((uniteM == 2 && FormUnite == 1) || (uniteM == 3 && FormUnite == 4))
            {
                var qteCal = FormQuantite / 1000;
                var prixCal = qteCal * prixM;
                return prixCal / 1;


            }
            else if (FormUnite == uniteM)
            {
                return (prixM * FormQuantite);

            }

            else { return 0; }

        }

        public string getUniteById(int uniteId)
        {
            Unite_Mesure unite = _db.unite_Mesures.Where(u => u.UniteMesure_Id == Convert.ToInt32(uniteId)).FirstOrDefault();
            return unite.UniteMesure_Libelle;
        }

        public IEnumerable<Fournisseur> getListFournisseur(int Id)
        {
            return _db.fournisseurs.Where(f => f.Founisseur_AbonnementId == Id).Where(f => f.Founisseur_IsActive == 1).AsEnumerable();
        }
        public IEnumerable<Taux_TVA> getListCoutTVA()
        {
            return _db.taux_TVAs.AsEnumerable();
        }

        public async Task<int?> CreateMatiereFamile(MatiereFamille matiereFamille)
        {
            matiereFamille.MatiereFamille_DateCreation = DateTime.Now;
            matiereFamille.MatiereFamille_IsActive = 1;
            await _db.matiereFamilles.AddAsync(matiereFamille);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return matiereFamille.MatiereFamille_ID;
            else
                return null;
        }

        public async Task<int?> CreateMatiereFamilleParent(MatireFamille_Parent matireFamille_Parent)
        {
            matireFamille_Parent.MatiereFamilleParent_IsActive = 1;
            matireFamille_Parent.MatiereFamilleParent_DateCreation = DateTime.Now;
            await _db.matireFamille_Parents.AddAsync(matireFamille_Parent);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return matireFamille_Parent.MatiereFamilleParent_ID;
            else
                return null;
        }

        public IEnumerable<MatiereFamille> getListMatiereFamille(int Id)
        {
            return _db.matiereFamilles.Where(m => m.MatiereFamille_AbonnementID == Id).Include(m => m.MatiereFamille_Parent).AsEnumerable();
        }

        public IEnumerable<MatireFamille_Parent> getListMatiereFamilleParent(int Id)
        {
            return _db.matireFamille_Parents.Where(m => m.MatiereFamilleParent_AbonnementID == Id).AsEnumerable();
        }

        public async Task<int?> AjouterUnites(int idMatiere, List<int> listUnite)
        {
            try
            {
                MatierePremiere matiere = _db.matierePremieres.Where(m => m.MatierePremiere_Id == idMatiere)
               .Include(m => m.unites_Utilisation)
               .FirstOrDefault();
                foreach (var id in listUnite)
                {
                    Unite_Mesure unite = _db.unite_Mesures.Where(u => u.UniteMesure_Id == id).FirstOrDefault();
                    var uniteMatiere = new Unite_MesureMatiere
                    {
                        Matiere_Premiere = matiere,
                        Unite_Mesure = unite,
                        Abonnement_ID = matiere.MatierePremiere_AbonnementId,
                        IsActive = 1,
                    };
                    await _db.unite_MesureMatieres.AddAsync(uniteMatiere);
                }
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return matiere.MatierePremiere_Id;
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
           
        }

        public IEnumerable<Unite_Mesure> getListUniteMesure(int Id, int aboId)
        {

            var uniteAll = _db.unite_Mesures.Include(fm => fm.matiereLink).ToList();
            var unites = new List<Unite_Mesure>();
            foreach (var m in uniteAll)
            {
                foreach (var fm in m.matiereLink)
                {
                    if (Id == fm.MatierePremiere_Id)
                    {
                        bool alreadyExists = unites.Any(x => x.UniteMesure_Id == m.UniteMesure_Id);
                        if (!alreadyExists)
                            unites.Add(m);
                    }
                }
            }

            return unites;
        }

        public IEnumerable<Unite_MesureMatiere> getListUniteUtilisation(int matiereId)
        {
            return _db.unite_MesureMatieres.Where(p => p.MatierePremiere_Id == matiereId)
                .Include(p => p.Matiere_Premiere)
                .Include(p => p.Unite_Mesure)
                .AsEnumerable();
        } 
        public IEnumerable<MouvementStock> getListHistorique(int aboId,int matiereId)
        {
            var hist = _db.mouvements.Where(p => p.MatierePremiere_Stokage.MatierePremiereStokage_MatierePremiereId == matiereId && p.MouvementStock_MouvementTypeId == 3 && p.MouvementStock_AbonnementId == aboId)
                .Include(p => p.MatierePremiere_Stokage).ThenInclude(p=>p.Matiere_Premiere)
                .Include(p=>p.Founisseur)
                .Include(p => p.Unite_Mesure)
                .AsEnumerable().OrderByDescending(p=>p.MouvementStock_DateReception);
            return hist;
        }
        public async Task<bool> deleteUniteLink(int ID, int code)
        {
            Unite_MesureMatiere unite = _db.unite_MesureMatieres.Where(e => e.Id == ID).FirstOrDefault();
            if (unite != null)
            {
                _db.Entry(unite).State = EntityState.Deleted;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<int?> DeclarationPerte(Perte_Matiere perte_Matiere)
        {
            perte_Matiere.PerteMatiere_DateCreation = DateTime.Now;
            // matierePremiere.MatierePremiere_QuantiteActuelle = matierePremiere.MatierePremiere_Quantite_FT;
            var matEnStock = _db.MatiereStock_Ateliers.Where(p => p.MatiereStockAtelier_ID == perte_Matiere.PerteMatiere_MatierePremiereStockageID).Include(p => p.Matiere_Premiere).FirstOrDefault();
            if (matEnStock != null)
            {
                decimal qte = 0;
                if ((perte_Matiere.PerteMatiere_UniteMesureID == 1 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 2) || (perte_Matiere.PerteMatiere_UniteMesureID == 4 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 3))
                    qte = perte_Matiere.PerteMatiere_Quantite / 1000;
                else if ((perte_Matiere.PerteMatiere_UniteMesureID == 2 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 1) || (perte_Matiere.PerteMatiere_UniteMesureID == 3 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 4))
                    qte = perte_Matiere.PerteMatiere_Quantite * 1000;
                else
                    qte =perte_Matiere.PerteMatiere_Quantite;

                matEnStock.MatiereStockAtelier_QauntiteActuelle -= qte;
                _db.Entry(matEnStock).State = EntityState.Modified;
            }
            await _db.perte_Matieres.AddAsync(perte_Matiere);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return perte_Matiere.PerteMatiere_ID;
            else
                return null;
        }

        public IEnumerable<Perte_Matiere> getListPertes(int aboId, int? atelierID, string date, int? matiereId)
        {
            var query = _db.perte_Matieres.Where(p => p.PerteMatiere_AbonnementID == aboId);
            if (atelierID != null)
                query = query.Where(s => s.PerteMatiere_AtelierID == atelierID);
            if (date != "")
            {
                DateTime newdate = Convert.ToDateTime(date);
                query = query.Where(s => s.PerteMatiere_DateCreation.Date == newdate.Date);
            }
            if (matiereId != null)
                query = query.Where(s => s.matiereStock_Atelier.Matiere_Premiere.MatierePremiere_Id == matiereId);
            return query.Include(p=>p.Unite_Mesure).Include(p => p.Atelier).Include(p=>p.matiereStock_Atelier).ThenInclude(p => p.Matiere_Premiere).AsEnumerable();
        }

        public async Task<int?> DeclarationPerteStock(Perte_MatiereStock perte_MatiereStock)
        {
            perte_MatiereStock.PerteMatiere_DateCreation = DateTime.Now;
            var matEnStock = _db.matierePremiereStockages.Where(p => p.MatierePremiereStokage_Id == perte_MatiereStock.PerteMatiere_MatierePremiereStockageID).Include(p => p.Matiere_Premiere).FirstOrDefault();                
            decimal qte = 0;
            if ((perte_MatiereStock.PerteMatiere_UniteMesureID == 1 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 2) || (perte_MatiereStock.PerteMatiere_UniteMesureID == 4 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 3))
                qte = perte_MatiereStock.PerteMatiere_Quantite / 1000;
            else if ((perte_MatiereStock.PerteMatiere_UniteMesureID == 2 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 1) || (perte_MatiereStock.PerteMatiere_UniteMesureID == 3 && matEnStock.Matiere_Premiere.MatierePremiere_AchatUniteMesureId == 4))
                qte = perte_MatiereStock.PerteMatiere_Quantite * 1000;
            else
                qte = perte_MatiereStock.PerteMatiere_Quantite;
            matEnStock.MatierePremiereStokage_QuantiteActuelle -= qte;
            _db.Entry(matEnStock).State = EntityState.Modified;
           // perte_MatiereStock.MatierePremiere_Stokage = null;
            await _db.perte_MatiereStocks.AddAsync(perte_MatiereStock);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return perte_MatiereStock.PerteMatiereStock_ID;
            else
                return null;
        }

        public IEnumerable<Perte_MatiereStock> getListPertesStock(int aboId, int? stockID, string date, int? matiereId)
        {
            var query = _db.perte_MatiereStocks.Where(p => p.PerteMatiere_AbonnementID == aboId);
            if (stockID != null)
                query = query.Where(s => s.PerteMatiere_StockID == stockID);
            if (date != "")
            {
                DateTime newdate = Convert.ToDateTime(date);
                query = query.Where(s => s.PerteMatiere_DateCreation.Date == newdate.Date);
            }
            if (matiereId != null)
                query = query.Where(s => s.MatierePremiere_Stokage.Matiere_Premiere.MatierePremiere_Id == matiereId);
            return query.Include(p => p.Unite_Mesure).Include(p => p.Lieu_Stockage).Include(p=>p.User).Include(p => p.MatierePremiere_Stokage).ThenInclude(p => p.Matiere_Premiere).AsEnumerable();
        }
        public async Task<int?> CreateApprov(Approvisionnement_Matiere approv)
        {
            approv.ApprovisionnementMatiere_DateCreation = DateTime.Now;
            var matStock = _db.matierePremiereStockages.Where(p => p.MatierePremiereStokage_Id == approv.ApprovisionnementMatiere_MatiereStockID).FirstOrDefault();
            matStock.MatierePremiereStokage_QuantiteActuelle -= approv.ApprovisionnementMatiere_Quantite;
            _db.Entry(matStock).State = EntityState.Modified;
            await _db.approvisionnement_Matieres.AddAsync(approv);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return approv.ApprovisionnementMatiere_ID;
            else
                return null;
        }
        public IEnumerable<Approvisionnement_Matiere> getListApprov(int aboId, int? stockID, string date, string etat, int? pointPord)
        {
            var query = _db.approvisionnement_Matieres.Where(p => p.ApprovisionnementMatiere_AbonnementID == aboId);
            if (stockID != null)
                query = query.Where(s => s.ApprovisionnementMatiere_StockID == stockID);
            if (date != "")
            {
                DateTime? newdate = Convert.ToDateTime(date);
                query = query.Where(s =>s.ApprovisionnementMatiere_DateApprovisionnement.Date == newdate);
            }
            if (etat != "")
                query = query.Where(s => s.ApprovisionnementMatiere_Etat == etat);
            if (pointPord != null)
                query = query.Where(s => s.ApprovisionnementMatiere_AtelierID == pointPord);
            return query.Include(p => p.Unite_Mesure).Include(p => p.Lieu_Stockage).Include(p => p.Atelier).Include(p => p.MatierePremiereStockage).ThenInclude(p => p.Matiere_Premiere).AsEnumerable();
        }
        public async Task<bool> ValiderApprovisionnement(int ApprovisionnementId, string valideId, int pdvId)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var appro = _db.approvisionnement_Matieres.Where(a => a.ApprovisionnementMatiere_ID == ApprovisionnementId)
                        .Include(a=>a.MatierePremiereStockage).FirstOrDefault();
                    appro.ApprovisionnementMatiere_Etat = "Reçue";
                    appro.ApprovisionnementMatiere_ValidéPar = valideId;
                    var matStock = _db.MatiereStock_Ateliers.Where(p => p.MatiereStockAtelier_MatierePremiereID == appro.MatierePremiereStockage.MatierePremiereStokage_MatierePremiereId)
                        .Where(p => p.MatiereStockAtelier_AtelierID == pdvId)
                        .FirstOrDefault();
                    if (matStock == null)
                    {
                        MatiereStock_Atelier matierStock = new MatiereStock_Atelier
                        {
                            MatiereStockAtelier_MatierePremiereID = appro.MatierePremiereStockage.MatierePremiereStokage_MatierePremiereId,
                            MatiereStockAtelier_AbonnementID = appro.ApprovisionnementMatiere_AbonnementID,
                            MatiereStockAtelier_AtelierID = appro.ApprovisionnementMatiere_AtelierID,
                            MatiereStockAtelier_QuatiteAvecPlanification = appro.ApprovisionnementMatiere_Quantite,
                            MatiereStockAtelier_QauntiteActuelle = appro.ApprovisionnementMatiere_Quantite,
                            MatiereStockAtelier_DateCreation = DateTime.Now,
                        };
                        await _db.MatiereStock_Ateliers.AddAsync(matierStock);

                    }
                    else
                    {
                        matStock.MatiereStockAtelier_QauntiteActuelle += appro.ApprovisionnementMatiere_Quantite;
                        matStock.MatiereStockAtelier_QuatiteAvecPlanification += appro.ApprovisionnementMatiere_Quantite;
                        _db.Entry(matStock).State = EntityState.Modified;

                    }
                    _db.Entry(appro).State = EntityState.Modified;
                    await unitOfWork.Complete();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
