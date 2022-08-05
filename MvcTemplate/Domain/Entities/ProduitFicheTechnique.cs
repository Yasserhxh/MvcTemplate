using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Produit_FicheTechnique")]
    public class ProduitFicheTechnique
    {
        [Key]
        public int FicheTechnique_Id { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string FicheTechnique_Libelle { get; set; }
        [ForeignKey("Produit_Vendable")]
        public int FicheTechnique_ProduitVendableId { get; set; }
        [ForeignKey("Matiere_Premiere")]
        public int FicheTechnique_MatierePremiereId { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int FicheTechnique_UniteMesureId { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal FicheTechnique_QuantiteMatiere { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal FicheTechnique_Prix { get; set; }
        public int FicheTechnique_IsActive { get; set; }
        [Column(TypeName = "int")]
        public int FicheTechnique_AbonnementId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FicheTechnique_DateCreation { get; set; }
        [ForeignKey("FicheTechnique_Bridge")]
        public int FicheTechnique_FicheTechniqueBridgeID { get; set; }


        public FicheTechniqueBridge FicheTechnique_Bridge { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public MatierePremiere Matiere_Premiere { get; set; }
        public ProduitVendable Produit_Vendable { get; set; }

    }
}
