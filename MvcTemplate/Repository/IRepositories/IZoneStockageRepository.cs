using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IZoneStockageRepository
    {
        Task<bool?> AjouterUtilisateur(int idStock, string responsableId);
        Task<int?> CreateZoneStockage(Zone_Stockage ZoneStockage);
        Task<int?> CreateSectionStockage(Section_Stockage SectionStockage);
        Task<int?> CreateLieuStockage(Lieu_Stockage LieuStockage);
        Task<int?> CreateFormeStockage(Forme_Stockage FormeStockage);
        IEnumerable<Unite_Mesure> getListUniteMesure();
        IEnumerable<Forme_Stockage> getListFormeSotckage(int Id);
        IEnumerable<Section_Stockage> getLisTSectionSotckage(int current, int? zoneID);
        IEnumerable<Type_Contenu> getListTypeContenu();
        IEnumerable<Lieu_Stockage> getListLieuStockage(int Id, int?statut);
        IEnumerable<Stock_User> getListLieuUser(string UserId);
        IEnumerable<Lieu_Stockage> getListLieuStockageActive(int Id, int lieuId);
        IEnumerable<Zone_Stockage> getListZoneStockage(int Id);
        Zone_Stockage findFormulaireZoneStockage(int formulaireZoneId);
        Forme_Stockage findFormulaireFormeStockage(int formulaireFormeId);
        Section_Stockage findFormulaireSectionStockage(int formulaireSectionId);
        Lieu_Stockage findFormulaireLieuStockage(int formulaireLieuId);
        Task<bool> updateFormulaireZoneStockage(int id, Zone_Stockage newZone);
        Task<bool> updateFormulaireFormeStockage(int id, Forme_Stockage newForme);
        Task<bool> updateFormulaireSectionStockage(int id, Section_Stockage newSection);
        Task<bool> updateFormulaireLieuStockage(int id, Lieu_Stockage newLieu);
        Task<bool> deleteZoneStockage(int ID, int code);
        Task<bool> deleteFormeStockage(int ID);
        Task<bool> deleteSectionStockage(int ID, int code);
        Task<bool> deleteLieuStockage(int ID, int code);


        IEnumerable<Section_Stockage> getListSection(int zoneId);
        IEnumerable<Ville> getlistVille();
        IEnumerable<Zone_Stockage> getListZones(int lieuId);





    }
}
