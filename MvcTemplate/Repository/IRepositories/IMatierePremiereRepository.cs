using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IMatierePremiereRepository
    {
        Task<int?> CreateMatiere(MatierePremiere matierePremiere,List<int> ListeUnite);
        Task<int?> CreateMatiereFamile(MatiereFamille matiereFamille);
        Task<int?> CreateMatiereFamilleParent(MatireFamille_Parent matireFamille_Parent);
        Task<int?> CreateAllergene(Allergene allergene);
        Task<int?> StockerMatiere(int Id, MatierePremiereStockage matierePremiereStockage);
        Task<int?> AjouterUnites(int idMatiere, List<int> listUnite);
        Task<int?> DeclarationPerte(Perte_Matiere perte_Matiere);
        Task<int?> DeclarationPerteStock(Perte_MatiereStock perte_MatiereStock);

        IEnumerable<MouvementStock> getListHistorique(int aboId, int matiereId);
        IEnumerable<Allergene> getListAllergene(int Id);
        IEnumerable<Fournisseur> getListFournisseur(int Id);
        IEnumerable<Section_Stockage> getListSectionStockage();
        IEnumerable<MatierePremiere> getListMatierePremiere(int Id,int? allergene,int? formeId);
        IEnumerable<Forme_Stockage> getListFormeStockage(int Id);
        IEnumerable<MatiereFamille> getListMatiereFamille(int Id);
        IEnumerable<MatireFamille_Parent> getListMatiereFamilleParent(int Id);
        IEnumerable<ProduitFicheTechnique> getListFicheTechnique(int Id);
        IEnumerable<MatierePremiereStockage> getListMatieresStockes(int Id, int MatierePremiereStokage_Id);
        IEnumerable<MatierePremiereStockage> getListMatieresStockesAll(int Id, int lieuId, int? zone, int? section);
        IEnumerable<Unite_Mesure> getListUniteMesure(int Id, int aboId);
        IEnumerable<Unite_MesureMatiere> getListUniteUtilisation(int matiereId);
        IEnumerable<Perte_Matiere> getListPertes(int aboId, int? atelierID, string date, int? matiereId);
        IEnumerable<Perte_MatiereStock> getListPertesStock(int aboId, int? stockID, string date, int? matiereId);

        MatierePremiere findFormulaireMatiereP(int aboID, int formulaireMatierePId);
        Allergene findFormulaireAllergene(int formulaireAllergieId);

        Task<bool> updateFormulaireMatierePremiere(int id, MatierePremiere newMatiere);
        Task<bool> updateFormulaireAllergie(int id, Allergene newAllergie);

        Task<bool> deleteMatiereP(int ID);
        Task<bool> deleteAllergie(int ID);
        Task<bool> deleteMatierePremiereStockes(int ID);
        Task<bool> deleteUniteLink(int ID, int code);
        
        bool MatiereStocker(int id);
        decimal CalculerPrix(int FormMatiereId, int FormUnite, decimal FormQuantite);
        string getUniteById(int uniteId);
        Task<int?> CreateApprov(Approvisionnement_Matiere approv);
        IEnumerable<Approvisionnement_Matiere> getListApprov(int aboId, int? stockID, string date, string etat, int? pointPord);
        Task<bool> ValiderApprovisionnement(int ApprovisionnementId, string valideId, int pdvId);
    }
}
