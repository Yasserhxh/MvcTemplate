using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities
{
    [Table("Approvisionnement_ProduitPackage")]

    public class Approvisionnement_ProduitPackage
    {
        [Key]
        public int ApprovisionnementProduitPackage_Id { get; set; }
        public DateTime ApprovisionnementProduitPackage_Date { get; set; }
        [ForeignKey("Point_Vente")]
        public int ApprovisionnementProduitPackage_PointVenteID { get; set; }
        [ForeignKey("Atelier")]
        public int ApprovisionnementProduitPackage_AtelierID { get; set; }
        [ForeignKey("ProduitPack_Atelier")]
        public int ApprovisionnementProduitPackage_ProduitpackAtelierId { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal ApprovisionnementProduitPackage__Quantite { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int? ApprovisionnementProduitPackage__UniteMesureId { get; set; }
        public DateTime? ApprovisionnementProduitPackage__DateReception { get; set; }
        public DateTime ApprovisionnementProduitPackage__DateCreation { get; set; }
        public string ApprovisionnementProduitPackage__Etat { get; set; }
        public int ApprovisionnementProduitPackage_AbonnementId { get; set; }

        public Point_Vente Point_Vente { get; set; }
        public Atelier Atelier { get; set; }
        public ProduitPack_Atelier ProduitPack_Atelier { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
    }
}
