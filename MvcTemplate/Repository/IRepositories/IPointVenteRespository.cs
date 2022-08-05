using Domain.Entities;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IPointVenteRespository
    {
        Task<bool?> AjouterUtilisateur(int idPointVente, string responsableId);
        Task<int?> CreatePointVent(Point_Vente point_Vente);
        Task<int?> CreatePositionVente(PositionVente position);
        Task<int?> CreateAgentServeur(AgentServeur agentserveur);
        Task<bool> AjouterSalle(int id, Salle salle);
        Task<bool> AjouterTable(int id, IEnumerable<Table> table);
        IEnumerable<Point_Vente> getLisPoinVent(int Id, int? type,int? statut);
        IEnumerable<PointVente_User> getListPdvUser(string UserId);
        IEnumerable<Point_Vente> getLisPoinVentActive(int Id);
        IEnumerable<PositionVente> getListPositionVente(int Id);
        IEnumerable<Salle> getListSalles(int Id);
        IEnumerable<Table> getListTables(int Id);
        IEnumerable<ModePaiement> getListModePaiement( );
        IEnumerable<AgentServeur> getListAgents(int Id);
        IEnumerable<PointVente_Stock> getStockPointVenteAll(int Id);
        IEnumerable<PointVente_Stock> getStockPointVente(int Id, int pdv);
        IEnumerable<PointVente_Stock> getStockPointVenteByProduit(int Id, int pdv,int produitId);
        IEnumerable<PointVente_Stock> getStockPointVenteAvecPlan(int Id, int? pdv);
        IEnumerable<ProduitPackage> getStockProduitPackage(int Id, int pdv);
        IEnumerable<Forme_Produit> getStockFormePackage(int Id, int pdv);
        IEnumerable<ProduitPackage> getStockProduitPackageAll(int Id);
        Point_Vente findFormulairePointVente(int formulairePointId);
        PositionVente findFormulairePositionVente(int formulairePoseId);
        Salle findFormulaireSalle(int formulaireSalleId);
        Task<bool> updateFormulairePointVente(int id, Point_Vente newPointVente);
        Task<bool> deletePointVente(int ID, int code);
        Task<bool> deleteFamillePdv(int id, int code);
        IEnumerable<Ville> getListVilles();
        IEnumerable<FamilleProduit> getListFamilles(int Id);
        IEnumerable<ProduitAppro> getListProduitVendable(int Id,int AboId);
        Task<bool?> AjouterFamille(int pointVenteId, List<int> ListFamille);
        IEnumerable<PointVente_Famille> getListFammilesPointVente(int pdvId);
        IEnumerable<Atelier> getListAtelier(int Id);
        IEnumerable<Lieu_Stockage> getListPointStock(int Id);
        IEnumerable<ProduitPack_Atelier> getListProduitPack(int Id, int AboId);
        Task<int?> CreateEchange(Echange_Produits echange_Produits);
        IEnumerable<Echange_Produits> getListEchanges(int AboID, string statut, string date, int? pdvF, int? pdvRec);
        IEnumerable<EchangeProduit_Details> getListEchangesDetails(int echangeID);
        IEnumerable<Point_Vente> getListePdvByCateg(int souscategID, int pdv);
        decimal findformulaireStockByFormeID(int Id, int pdv, int formeID);
        Task<IEnumerable<ApiUserModel>> getUsersAsync(int pdvId);
        Task<bool> AccepterOrdreEchange(int echangeID, string validerPar, int pdvID);
        Task<bool> AccepterLivraisonEchange(int echangeID, string validerPar, int pdvID);
        IEnumerable<Echange_Produits> getListEchangesEntrant(int AboID, string statut, string date, int? pdvF, int? pdvRec);
        IEnumerable<Echange_Produits> getListEchangesSortant(int AboID, string statut, string date, int? pdvF, int? pdvRec);
    }
}
