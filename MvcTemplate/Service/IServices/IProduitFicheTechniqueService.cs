using Domain.Entities;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IProduitFicheTechniqueService
    {
        Task<bool> CreateFiche(FicheTechniqueBridgeModel ficheModel);
        IEnumerable<Unite_MesureModel> getListUniteMesure();
        IEnumerable<MatierePremiereModel> getListMatierePremiere(int Id);
        IEnumerable<ProduitVendableModel> getListProduitVendable(int Id);
        IEnumerable<Forme_ProduitModel> getListFormes(int produitId);
        IEnumerable<ProduitFicheTechniqueModel> getListFicheTechnique(int Id, int AboId);
        IEnumerable<FicheTechniqueBridgeModel> getListFicheTechniqueAll(int Id, int? categ,int?SousCateg);
        IEnumerable<FicheDetailsModel> getListFicheTechniqueByLibelle(string Libelle, int Id);
        IEnumerable<FicheTechniqueBridgeModel> findFormulaireFiche(int formulaireFicheId, int AboId);
        IEnumerable<FicheFromeModel> GetFicheFormes(int formulaireFicheId);
        FicheTechniqueBridgeModel findFormulaireFiche(int formulaireFicheId);
        IEnumerable<ProduitBaseFicheTechniqueModel> getListFicheTechniqueBase(int Id, int AboId);
        IEnumerable<ProduitFicheTechniqueModel> findFormulaireFicheById(int formulaireFicheId);
        ProduitFicheTechniqueModel findFormulaireFicheByLibelle(string Libelle);
        Task<bool> updateFormulaireFicheTechnique(int id, ProduitFicheTechniqueModel newFicheModel);
        Task<bool> deleteFicheTechnique(int ID);
        Task<bool> SetInUse(int id);
        Task<bool> CreateFicheBase(FicheTehcniqueProduitBaseModel ficheBaseModel);
        IEnumerable<FicheTehcniqueProduitBaseModel> getListFicheTechniqueBaseAll(int Id, int? categ, int? SousCateg);
        IEnumerable<FicheTehcniqueProduitBaseModel> findFormulaireFicheBase(int formulaireFicheBaseId, int aboId);
        Task<bool> SetInUseBase(int id);
        decimal CalculerPrix(int prodId, int uniteID, decimal qte);
        FicheTehcniqueProduitBaseModel FindFicheTechniqueBaseBYPordBase(int Id, int AboId);
    }
}
