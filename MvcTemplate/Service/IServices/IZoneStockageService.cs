using Domain.Entities;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IZoneStockageService
    {
        Task<bool> CreatZoneStockage(Zone_StockageModel zoneModel);
        Task<bool> CreatSectionStockage(Section_StockageModel sectionModel);
        Task<bool> CreatLieuStockage(Lieu_StockageModel lieuModel);
        Task<bool> CreatFormeStockage(Forme_StockageModel formeModel);
        Task<bool?> AjouterUtilisateur(int idStock, string responsableId);
        IEnumerable<Type_ContenuModel> getListTypeContenu();
        IEnumerable<Unite_MesureModel> getListUniteMesure();
        IEnumerable<Unite_CategorieModel> getListUniteCategorie();
        IEnumerable<Lieu_StockageModel> getListLieuStockage(int Id,int? statut);
        IEnumerable<Lieu_StockageModel> getListLieuStockageActive(int Id, int lieuId);
        IEnumerable<Stock_UserModel> getListStockUser(string UserId);
        IEnumerable<Forme_StockageModel> getListFormeSotckage(int Id);
        IEnumerable<Section_StockageModel> getListSectionSotckage(int current, int? zoneID);
        IEnumerable<Zone_StockageModel> getListZoneStockage(int Id);



        Zone_StockageModel findFormulaireZoneStockage(int formulaireZoneId);
        Section_StockageModel findFormulaireSectionStockage(int formulaireSectionId);
        Lieu_StockageModel findFormulaireLieuStockage(int formulaireLieuId);
        Forme_StockageModel findFormulaireFormeStockage(int formulaireformeId);





        Task<bool> updateFormulaireZoneStockage(int id, Zone_StockageModel zone_StockageModel);
        Task<bool> updateFormulaireSectionStockage(int id, Section_StockageModel section_StockageModel);
        Task<bool> updateFormulaireLieuStockage(int id, Lieu_StockageModel lieu_StockageModel);
        Task<bool> updateFormulaireFormeStockage(int id, Forme_StockageModel forme_StockageModel);
        Task<bool> deleteZoneStockage(int ID, int code);
        Task<bool> deleteSectionStockage(int ID, int code);
        Task<bool> deleteLieuStockage(int ID, int code);
        Task<bool> deleteFormeStockage(int ID);


        IEnumerable<Section_StockageModel> getListSection(int zoneId);
        IEnumerable<Zone_StockageModel> getListZones(int lieuId);
        IEnumerable<VilleModel> getlistVille();

    }
}
