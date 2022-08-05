using Domain.Entities;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IProduitFicheTechniqueRepository
    {
        Task<int?> CreateFiche(FicheTechniqueBridge fiche);
        Task<int?> CreateFicheBase(FicheTechniqueProduitBase ficheBase);
        IEnumerable<Unite_Mesure> getListUniteMesure();
        IEnumerable<MatierePremiere> getListMatierePremiere(int Id);
        IEnumerable<ProduitVendable> getListProduitVendable(int Id);
        IEnumerable<Forme_Produit> getListFormes(int produitId);
        IEnumerable<ProduitFicheTechnique> getListFicheTechnique(int Id, int AboId);
        IEnumerable<ProduitBaseFicheTechnique> getListFicheTechniqueBase(int Id, int AboId);
        IEnumerable<FicheTechniqueBridge> getListFicheTechniqueAll(int Id,int? categ,int? SousCateg);
        IEnumerable<FicheTechniqueProduitBase> getListFicheTechniqueBaseAll(int Id,int? categ,int? SousCateg);
        IEnumerable<FicheDetailsModel> getListFicheTechniqueByLibelle(string Libelle, int Id);
        IEnumerable<FicheTechniqueBridge> findFormulaireFiche(int formulaireFicheId, int AboId);
        IEnumerable<FicheForme> GetFicheFormes(int formulaireFicheId);
        FicheTechniqueBridge findFormulaireFiche(int formulaireFicheId);
        IEnumerable<FicheTechniqueProduitBase> findFormulaireFicheBase(int formulaireFicheBaseId, int aboId);
        ProduitFicheTechnique findFormulaireFicheByLibelle(string Libelle);
        IEnumerable<ProduitFicheTechnique> findFormulaireFicheById(int formulaireFicheId);
        Task<bool> updateFormulaireFicheTechnique(int id, ProduitFicheTechnique newFiche);
        Task<bool> deleteFicheTechnique(int ID);
        Task<bool> SetInUse(int id);
        Task<bool> SetInUseBase(int id);
        decimal CalculerPrix(int prodId, int uniteID, decimal qte);
    }
}
