using Domain.Entities;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IMatierePremiereService
    {
        Task<bool> CreateMatierePremiere(MatierePremiereModel matiereModel,List<int> ListeUnite, List<int> ListeAllergene);
        Task<bool> CreateMatiereFamille(MatiereFamilleModel matiereFamilleModel);
        Task<bool> CreateMatiereFamilleParent(MatiereFamille_ParentModel matiereFamille_ParentModel);
        Task<bool> CreateAllergene(AllergeneModel allergeneModel);
        Task<bool> StockerMatiere(int Id, MatierePremiereStockageModel matiereStockerModel);
        Task<bool> AjouterUnites(int idMatiere, List<int> listUnite, List<int> ListeAllergene);
        Task<bool> DeclarationPerte(Perte_MatiereModel perte_MatiereModel);
        Task<bool> DeclarationPerteStock(Perte_MatiereStockModel perte_MatiereStockModel);

        IEnumerable<Perte_MatiereModel> getListPertes(int aboId, int? lieuStockID, string date, int? matiereId);
        IEnumerable<Perte_MatiereStockModel> getListPertesStock(int aboId, int? lieuStockID, string date, int? matiereId);
        IEnumerable<MouvementStockModel> getListHistorique(int aboId, int matiereId);
        IEnumerable<AllergeneModel> getListAllergene(int Id);
        IEnumerable<FournisseurModel> getListFournisseur(int Id);
        IEnumerable<Section_StockageModel> getListSectionStockage();
        IEnumerable<MatierePremiereModel> getListMatierePremiere(int Id,int? allergene,int? formeId);
        IEnumerable<Forme_StockageModel> getListFormeStockage(int Id);
        IEnumerable<MatiereFamilleModel> getListMatiereFamille(int Id);
        IEnumerable<MatiereFamille_ParentModel> getListMatiereFamilleParent(int Id);
        IEnumerable<ProduitFicheTechniqueModel> getListFicheTech(int Id);
        IEnumerable<MatierePremiereStockageModel> getListMatiereStocker(int Id, int MatierePremiereStokage_Id);
        IEnumerable<MatierePremiereStockageModel> getListMatiereStockerAll(int Id, int lieuId, int? zone, int? section);
        IEnumerable<Unite_MesureModel> getListUniteMesure(int Id, int aboId);
        IEnumerable<Unite_MesureMatiereModel> getListUniteUtilisation(int matiereId);


        MatierePremiereModel findFormulaireMatiereP(int aboID, int formulaireMatiereId);
        AllergeneModel findFormulaireAllergie(int formulaireAllergieId);

        Task<bool> updateFormulaireMatiere(int id, MatierePremiereModel matiereModel);

        Task<bool> updateFormulaireAllergie(int id, AllergeneModel AllergieModel);

        Task<bool> deleteMatiere(int ID);
        Task<bool> deleteAllergie(int ID);
        Task<bool> deleteMatierePremiereStockes(int ID);
        Task<bool> deleteUniteLink(int ID, int code);
        bool MatiereStocker(int ID);
        decimal CalculerPrix(int FormMatiereId, int FormUnite, decimal FormQuantite);
        string getUniteById(int uniteId);
        Task<bool> CreateApprov(Approvisionnement_MatiereModel approvModel);
        IEnumerable<Approvisionnement_MatiereModel> getListApprov(int aboId, int? stockID, string date, string etat, int? pointPord);
        Task<bool> ValiderApprovisionnement(int ApprovisionnementId, string valideId, int pdvId);
        IEnumerable<Taux_TVAModel> getListCoutTVA();
        List<AllergeneModel> getListAllergeneMatiere(int matPrem, int aboId);

    }
}
