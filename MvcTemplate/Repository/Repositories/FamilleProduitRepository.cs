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
    public class FamilleProduitRepository : IFamilleProduitRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork unitOfWork;
        public FamilleProduitRepository(ApplicationDbContext db, IUnitOfWork unitOfWork)
        {
            _db = db;
            this.unitOfWork = unitOfWork;
        }
        public async Task<int?> CreateFamille(FamilleProduit familleProduit)
        {

            familleProduit.FamilleProduit_IsActive = 1;
            familleProduit.FamilleProduit_DateCreation = DateTime.Now;
            await _db.familleProduits.AddAsync(familleProduit);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return familleProduit.FamilleProduit_Id;
            else
                return null;

        }

        public async Task<bool> deleteFormulaireFamille(int ID)
        {
            FamilleProduit famille = _db.familleProduits.Where(e => e.FamilleProduit_IsActive == ID).FirstOrDefault();
            if (famille != null)
            {
                famille.FamilleProduit_IsActive = 0;
                _db.Entry(famille).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public FamilleProduit findFormulaireFamille(int formulaireFamilleId)
        {
            return _db.familleProduits.Where(e => e.FamilleProduit_Id == formulaireFamilleId).FirstOrDefault();
        }

        public IEnumerable<FamilleProduit> getListFamilles(int Id)
        {

            var f = _db.familleProduits.Where(a => a.FamilleProduit_AbonnemnetId == Id).Include(p=>p.sousFamille);
            return f;
        }
        public IEnumerable<FamilleProduit> getListFamillesByPdv(int Id,int pdv)
        {

            var f = _db.pointVente_Familles.Where(a => a.IsActive == 1 && a.Abonnement_Id == Id && a.PointVente_Id == pdv ).Select(p=>p.Famille_Produit).Include(p=>p.sousFamille).AsEnumerable();
            return f;
        }

        public async Task<bool> updateFormulaireFamille(int id, FamilleProduit newFamile)
        {
            FamilleProduit famille = _db.familleProduits.Where(e => e.FamilleProduit_Id == id).FirstOrDefault();
            if (famille != null)
            {
                famille.FamilleProduit_Libelle = newFamile.FamilleProduit_Libelle;
                famille.FamilleProduit_Image = newFamile.FamilleProduit_Image;
                //famille.FamilleProduit_DateCreation = newFamile.FamilleProduit_DateCreation;
                //famille.FamilleProduit_ParentId = newFamile.FamilleProduit_ParentId;
                famille.FamilleProduit_IsActive = 1;
                _db.Entry(famille).State = EntityState.Modified;
                var confirm = await unitOfWork.Complete();
                if (confirm > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }
        public IEnumerable<FamilleProduit> getListAllFamilles()
        {
            return _db.familleProduits.Where(a => a.FamilleProduit_IsActive == 1);
        }

        public async Task<int?> CreatSousFamille(SousFamille sousFamille)
        {
            await _db.sousFamilles.AddAsync(sousFamille);
            var confirm = await unitOfWork.Complete();
            if (confirm > 0)
                return sousFamille.SousFamille_ID;
            else
                return null;
        }

        public IEnumerable<SousFamille> getListSousFamilles(int? Id, int aboID)
        {
            var query = _db.sousFamilles.Where(s => s.SousFamille_AbonnementID == aboID);
            if (Id != null)
                query.Where(s => s.SousFamille_ParentID == Id);
            return _db.sousFamilles.Where(s => s.SousFamille_ParentID == Id).Include(s => s.Famille_Produit).AsEnumerable();
        } 
        
    }
}
