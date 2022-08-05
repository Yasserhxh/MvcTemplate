using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class ProduitPretConsomerModel
    {
        public ProduitPretConsomerModel()
        {
            formes = new List<Forme_ProduitModel>();
            Fournisseur_Link = new List<Fournisseur_ProduitConsoModel>();
        }
        public int ProduitPretConsomer_ID { get; set; }
        public string ProduitPretConsomer_Designation { get; set; }
        public string ProduitPretConsomer_Description { get; set; }
        public int ProduitPretConsomer_FormeStockageId { get; set; }
        public int ProduitPretConsomer_FamilleProduit { get; set; }
        public int? ProduitPretConsomer_UniteMesureAchatId { get; set; }
        public decimal ProduitPretConsomer_StockMinimun { get; set; }
        public decimal ProduitPretConsomer_StockMaximum { get; set; }
        public int ProduitPretConsomer_DelaiConsomation { get; set; }
        public bool ProduitPretConsomer_EnStock { get; set; }
        public string ProduitPretConsomer_Photo { get; set; }
        public int ProduitPretConsomer_IsActive { get; set; }
        public int ProduitPretConsomer_AbonnementID { get; set; }
        public DateTime ProduitPretConsomer_DateCreation { get; set; }
        public decimal ProduitPretConsomer_PrixMoyenAchat { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public Forme_StockageModel Forme_Stockage { get; set; }
        public List<Forme_ProduitModel> formes { get; set; }
        public List<Fournisseur_ProduitConsoModel> Fournisseur_Link { get; set; }
        public SousFamilleModel Sous_Famille { get; set; }
    }
}
