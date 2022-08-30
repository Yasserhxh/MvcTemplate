using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Produit_Vendable")]
    public class ProduitVendable
    {
        public ProduitVendable()
        {
            formes = new Collection<Forme_Produit>();
        }
        [Key]
        public int ProduitVendable_Id { get; set; }
        [ForeignKey("Sous_Famille")]
        public int ProduitVendable_FamilleProduitId { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string ProduitVendable_Designation { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string ProduitVendable_TempConservation { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string ProduitVendable_CodeProduit { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string ProduitVendable_Description { get; set; }
        [ForeignKey("Forme_Stockage")]
        public int? ProduitVendable_FormStockageId { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int? ProduitVendable_UniteMesureId { get; set; }
        [ForeignKey("Taux_TVA")]
        [DefaultValue(3)]
        public int? ProduitVendable_TvaId { get; set; }
        [Column(TypeName = "int")]
        public int? ProduitVendable_UniteMesureId_FT { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal? ProduitVendable_StockMaximum { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal? ProduitVendable_StockMinimum { get; set; }
        [Column(TypeName = "int")]
        public int? ProduitVendable_DelaiConsommation { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string ProduitVendable_CodeBarre { get; set; }
        [Column(TypeName = "nvarchar(300)")]
        public string ProduitVendable_GrandePhoto { get; set; }
        [Column(TypeName = "nvarchar(300)")]
        public string ProduitVendable_PetitePhoto { get; set; }
        public int ProduitVendable_IsActive { get; set; }
        [Column(TypeName = "int")]
        public int? ProduitVendable_AbonnementId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? ProduitVendable_DateCreation { get; set; }
        [Column(TypeName = "int")]
        public int ProduitVendable_PlanificationFlag { get; set; }
        [Column(TypeName = "int")]
        public int ProduitVendable_AvecFT { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Produit_CoutDeRevient { get; set; }
        public ICollection<Forme_Produit> formes { get; set; }

        public Unite_Mesure Unite_Mesure { get; set; }
        public SousFamille Sous_Famille { get; set; }
        public Forme_Stockage Forme_Stockage { get; set; }
        public Taux_TVA Taux_TVA { get; set; }


    }
}
