using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("FormeDetails")]
    public class FormeDetails
    {
        [Key]
        public int FormeDetails_ID { get; set; }
        [ForeignKey("Forme_Produit")]
        public int FormeDetails_FormeProduitID { get; set; }
        [ForeignKey("Point_Vente")]
        public int FormeDetails_PointVenteID { get; set; }
        //public string FormeDetails_Libelle { get; set; }
        public decimal FormeDetails_Quantite { get; set; }
        [ForeignKey("ProduitPackage")]
        public int FormeDetails_ProduitPackageID { get; set; }
        public DateTime FormeDetails_DateCreation { get; set; }
        public int FormeDetails_AbonnementID { get; set; }
        public ProduitPackage ProduitPackage { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
        public Point_Vente Point_Vente { get; set; }
    }
}
