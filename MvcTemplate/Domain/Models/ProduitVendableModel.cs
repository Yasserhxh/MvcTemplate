using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class ProduitVendableModel
    {
        public ProduitVendableModel()
        {
            formes = new List<Forme_ProduitModel>();
        }
        public int ProduitVendable_Id { get; set; }
        public int? ProduitVendable_FamilleProduitId { get; set; }
        public string ProduitVendable_Designation { get; set; }
        public string ProduitVendable_Description { get; set; }
        public int? ProduitVendable_FormStockageId { get; set; }
        public int? ProduitVendable_UniteMesureId { get; set; }
        public int? ProduitVendable_TvaId { get; set; }
        public int? ProduitVendable_UniteMesureId_FT { get; set; }
        public decimal? ProduitVendable_StockMaximum { get; set; }
        public decimal? ProduitVendable_StockMinimum { get; set; }
        public int? ProduitVendable_DelaiConsommation { get; set; }
        public string ProduitVendable_CodeBarre { get; set; }
        public string ProduitVendable_GrandePhoto { get; set; }
        public string ProduitVendable_PetitePhoto { get; set; }
        public int ProduitVendable_IsActive { get; set; }
        public int? ProduitVendable_AbonnementId { get; set; }
        public DateTime? ProduitVendable_DateCreation { get; set; }
        public int ProduitVendable_AvecFT { get; set; }
        public int ProduitVendable_PlanificationFlag { get; set; }
        public decimal? Produit_CoutDeRevient { get; set; }
        public List<Forme_ProduitModel> formes { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public SousFamilleModel Sous_Famille { get; set; }
        public Forme_StockageModel Forme_Stockage { get; set; }
        public Taux_TVAModel Taux_TVA { get; set; }

    }
}
