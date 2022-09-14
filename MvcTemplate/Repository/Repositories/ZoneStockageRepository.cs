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
    public class ZoneStockageRepository : IZoneStockageRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork unitOfWork;

        public ZoneStockageRepository(ApplicationDbContext db, IUnitOfWork unitOfWork)
        {
            _db = db;
            this.unitOfWork = unitOfWork;
        }

        public async Task<int?> CreateFormeStockage(Forme_Stockage FormeStockage)
        {

            FormeStockage.FormStockage_DateCreation = DateTime.Now;
            await _db.forme_Stockages.AddAsync(FormeStockage);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return FormeStockage.FormStockage_Id;
            else
                return null;
        }

        public async Task<int?> CreateLieuStockage(Lieu_Stockage LieuStockage)
        {

            LieuStockage.LieuStockag_IsActive = 1;
            LieuStockage.LieuStockag_DateCreation = DateTime.Now;
            await _db.lieu_Stockages.AddAsync(LieuStockage);
            await unitOfWork.Complete();
            var user = _db.Users.Where(u => u.Id == LieuStockage.LieuStockag_UTILISATEUR).FirstOrDefault();
            user.LieuStockage_ID = LieuStockage.LieuStockag_Id;
            _db.Entry(user).State = EntityState.Modified;
            var res = await AjouterUtilisateur(LieuStockage.LieuStockag_Id, LieuStockage.LieuStockag_UTILISATEUR);
            if(res==null)
                return null;
            else
            {
                return LieuStockage.LieuStockag_Id;              
            }

        }

        public async Task<int?> CreateSectionStockage(Section_Stockage SectionStockage)
        {
            SectionStockage.Section_IsActive = 1;
            await _db.section_Stockages.AddAsync(SectionStockage);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return SectionStockage.Section_Id;
            else
                return null;
        }

        public async Task<int?> CreateZoneStockage(Zone_Stockage ZoneStockage)
        {
            ZoneStockage.ZoneStockage_IsActive = 1;
            ZoneStockage.ZoneStockage_DateCreation = DateTime.Now;
            await _db.zone_Stockages.AddAsync(ZoneStockage);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return ZoneStockage.ZoneStockage_Id;
            else
                return null;
        }

        public async Task<bool> deleteFormeStockage(int ID)
        {
            Forme_Stockage forme = _db.forme_Stockages.Where(e => e.FormStockage_Id == ID).FirstOrDefault();
            if (forme != null)
            {
                _db.forme_Stockages.Remove(forme);
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> deleteLieuStockage(int ID, int code)
        {
            Lieu_Stockage Lieu = _db.lieu_Stockages.Where(e => e.LieuStockag_Id == ID).FirstOrDefault();
            if (Lieu != null)
            {
                if(code == 0)
                {
                    Lieu.LieuStockag_IsActive = 0;
                }
                if(code == 1)
                {
                    Lieu.LieuStockag_IsActive = 1;
                }
                _db.Entry(Lieu).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> deleteSectionStockage(int ID, int code)
        {
            Section_Stockage Section = _db.section_Stockages.Where(e => e.Section_Id == ID).FirstOrDefault();
            if (Section != null)
            {
                if (code == 0)
                {
                    Section.Section_IsActive = 0;
                }
                if (code == 1)
                {
                    Section.Section_IsActive = 1;
                }

                _db.Entry(Section).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> deleteZoneStockage(int ID, int code)
        {
            var mat = _db.matierePremiereStockages.Where(p => p.Section_Stockage.Section_ZoneStockageId == ID && p.MatierePremiereStokage_IsActive == 1)
                .FirstOrDefault();
            if (mat != null)
                return false;
            Zone_Stockage Zone = _db.zone_Stockages.Where(e => e.ZoneStockage_Id == ID).FirstOrDefault();
            if (Zone != null)
            {
                if (code == 0)
                {
                    Zone.ZoneStockage_IsActive = 0;
                }
                if (code == 1)
                {
                    Zone.ZoneStockage_IsActive = 1;
                }
                
                _db.Entry(Zone).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public Forme_Stockage findFormulaireFormeStockage(int formulaireFormeId)
        {
            return _db.forme_Stockages.Where(e => e.FormStockage_Id == formulaireFormeId).FirstOrDefault();
        }

        public Lieu_Stockage findFormulaireLieuStockage(int formulaireLieuId)
        {
            return _db.lieu_Stockages.Where(e => e.LieuStockag_Id == formulaireLieuId).FirstOrDefault();
        }

        public Section_Stockage findFormulaireSectionStockage(int formulaireSectionId)
        {
            return _db.section_Stockages.Where(e => e.Section_Id == formulaireSectionId).Include(e => e.Zone_Stockage).FirstOrDefault();
        }

        public Zone_Stockage findFormulaireZoneStockage(int formulaireZoneId)
        {
            return _db.zone_Stockages.Where(e => e.ZoneStockage_Id == formulaireZoneId).Include(e => e.Type_Contenu).Include(e => e.Unite_Mesure).Include(e => e.Lieu_Stockage).Include(e => e.Forme_Stockage).FirstOrDefault();
        }

        public IEnumerable<Forme_Stockage> getListFormeSotckage(int Id)
        {
            //return _db.forme_Stockages.Where(a => a.FormStockage_AbonnementId == Id).AsEnumerable();
            return _db.forme_Stockages.AsEnumerable();
        }

        public IEnumerable<Lieu_Stockage> getListLieuStockage(int Id,int? statut)
        {
            IEnumerable<Lieu_Stockage> lieu;
            if (statut != null)
            {
                lieu = _db.lieu_Stockages.Where(a => a.LieuStockag_AbonnementId == Id && a.LieuStockag_IsActive==statut).Include(a => a.Ville).AsEnumerable();
            }
            else
            {
                lieu = _db.lieu_Stockages.Where(a => a.LieuStockag_AbonnementId == Id).Include(a => a.Ville).AsEnumerable();
            }
            return lieu;
        }
        public IEnumerable<Lieu_Stockage> getListLieuStockageActive(int Id, int lieuId)
        {
            return _db.lieu_Stockages.Where(a => a.LieuStockag_AbonnementId == Id && a.LieuStockag_IsActive == 1 && a.LieuStockag_Id != lieuId)
                .Include(a => a.Ville)
                .AsEnumerable();
        }

        public IEnumerable<Section_Stockage> getListSection(int zoneId)
        {
            return _db.section_Stockages.Where(e => e.Section_ZoneStockageId == zoneId).Where(p=>p.Section_IsActive==1).Include(e => e.Zone_Stockage).AsEnumerable();
        }

        public IEnumerable<Zone_Stockage> getListZones(int lieuId)
        {
            return _db.zone_Stockages.Where(e => e.ZoneStockage_LieuStockageId == lieuId/* && e.ZoneStockage_IsActive == 1*/)
                .Include(e => e.Type_Contenu)
                .Include(e => e.Lieu_Stockage)
                .Include(e => e.Forme_Stockage)
                .Include(e => e.Unite_Mesure)
                .AsEnumerable();
        }

        public IEnumerable<Section_Stockage> getLisTSectionSotckage(int current , int? zoneID)
        {
            if (zoneID != null)
                return _db.section_Stockages.Where(p => p.Zone_Stockage.ZoneStockage_LieuStockageId == current)
                    .Where(p => p.Section_ZoneStockageId == zoneID).Include(p => p.Zone_Stockage).ThenInclude(p => p.Lieu_Stockage).AsEnumerable();
            else
            {
                return _db.section_Stockages.Where(p => p.Zone_Stockage.ZoneStockage_LieuStockageId == current)
                  .Include(p => p.Zone_Stockage).ThenInclude(p => p.Lieu_Stockage).AsEnumerable();
            }
        }

        public IEnumerable<Type_Contenu> getListTypeContenu()
        {
            return _db.type_Contenus.AsEnumerable();
        }

        public IEnumerable<Unite_Mesure> getListUniteMesure()
        {
            return _db.unite_Mesures.AsEnumerable();
        } 
        public IEnumerable<Unite_Categorie> getListUniteCategorie()
        {
            return _db.unite_Categories.AsEnumerable();
        }

        public IEnumerable<Zone_Stockage> getListZoneStockage(int Id)
        {

            return _db.zone_Stockages.Where(a =>/* a.ZoneStockage_IsActive == 1 &&*/ a.ZoneStockage_AbonnementId == Id)
                .Include(e => e.Type_Contenu)
                .Include(e => e.Lieu_Stockage)
                .Include(e => e.Forme_Stockage)
                .Include(e => e.Unite_Mesure)
                .AsEnumerable();
        }

        public async Task<bool> updateFormulaireFormeStockage(int id, Forme_Stockage newForme)
        {
            Forme_Stockage forme = _db.forme_Stockages.Where(e => e.FormStockage_Id == id).FirstOrDefault();
            if (forme != null)
            {
                forme.FormStockage_Libelle = newForme.FormStockage_Libelle;
                _db.Entry(forme).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> updateFormulaireLieuStockage(int id, Lieu_Stockage newLieu)
        {
            Lieu_Stockage Lieu = _db.lieu_Stockages.Where(e => e.LieuStockag_Id == id).FirstOrDefault();
            if (Lieu != null)
            {
                Lieu.LieuStockag_Nom = newLieu.LieuStockag_Nom;
                Lieu.LieuStockag_Adresse = newLieu.LieuStockag_Adresse;
                Lieu.LieuStockag_NomResponsable = newLieu.LieuStockag_NomResponsable;
                Lieu.LieuStockag_Telephone = newLieu.LieuStockag_Telephone;
                Lieu.LieuStockag_VilleID = newLieu.LieuStockag_VilleID;
                Lieu.LieuStockag_CodePostal = newLieu.LieuStockag_CodePostal;
                _db.Entry(Lieu).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> updateFormulaireSectionStockage(int id, Section_Stockage newSection)
        {
            Section_Stockage section = _db.section_Stockages.Where(e => e.Section_Id == id).FirstOrDefault();
            if (section != null)
            {
                section.Section_Designation = newSection.Section_Designation;
                section.Section_ZoneStockageId = newSection.Section_ZoneStockageId;
                section.Section_IsActive = 1;
                _db.Entry(section).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> updateFormulaireZoneStockage(int id, Zone_Stockage newZone)
        {
            Zone_Stockage zone = _db.zone_Stockages.Where(e => e.ZoneStockage_Id == id).FirstOrDefault();
            if (zone != null)
            {
                zone.ZoneStockage_LieuStockageId = newZone.ZoneStockage_LieuStockageId;
                zone.ZoneStockage_FormeStockageId = newZone.ZoneStockage_FormeStockageId;
                zone.ZoneStockage_CapaciteStockage = newZone.ZoneStockage_CapaciteStockage;
                zone.ZoneStockage_UniteMesureId = newZone.ZoneStockage_UniteMesureId;
                zone.ZoneStockage_TypeContenuId = newZone.ZoneStockage_TypeContenuId;
                zone.ZoneStockage_Designation = newZone.ZoneStockage_Designation;
                //zone.ZoneStockage_IsActive = 1;
                _db.Entry(zone).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public IEnumerable<Ville> getlistVille()
        {
            return _db.villes.AsEnumerable();
        }

        public async Task<bool?> AjouterUtilisateur(int idStock, string responsableId)
        {
            var stock = _db.lieu_Stockages.Where(f => f.LieuStockag_Id == idStock).FirstOrDefault();
            var link = _db.stock_Users.Where(p => p.User_Id == responsableId && p.Stock_Id == idStock).FirstOrDefault();

            if (link != null)

                return null;

            else
            {
                var user = _db.Users.Where(m => m.Id == responsableId).FirstOrDefault();
                var stockuser = new Stock_User
                {

                    User = user,
                    Lieu_Stockage = stock,
                    Abonnement_ID = stock.LieuStockag_AbonnementId,
                    IsActive = 1,
                    //Date = DateTime.Now,
                };
                await _db.stock_Users.AddAsync(stockuser);
            }

            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return true;
            else
                return null;
        }

        public IEnumerable<Stock_User> getListLieuUser(string UserId)
        {
            return _db.stock_Users.Where(p => p.User_Id == UserId && p.IsActive==1)
               .Include(p => p.User)
               .Include(p => p.Lieu_Stockage)
               .AsEnumerable();
        }
    }
}
