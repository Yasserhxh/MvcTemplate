using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Forme_Produit")]
    public class Forme_Produit
    {
       
        [Key]
        public int FormeProduit_ID { get; set; }
        [ForeignKey("Produit_Vendable")]
        public int? FormeProduit_ProduitID { get; set; }
        [ForeignKey("ProduitPackage")]
        public int? FormeProduit_ProduitPackageId { get; set; }
        [ForeignKey("Produit_PretConsomer")]
        public int? FormeProduit_ProduitPretId { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string FormeProduit_Libelle { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal FormeProduit_CoutDeRevient { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal FormeProduit_PrixVente { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FormeProduit_DateCreation { get; set; }
        [Column(TypeName = "int")]
        public int FormeProduit_AbonnementID { get; set; }
        public ProduitVendable Produit_Vendable { get; set; }
        public ProduitPretConsomer Produit_PretConsomer { get; set; }
        public ProduitPackage ProduitPackage { get; set; }

    }
}
