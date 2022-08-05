using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Approvisionnement")]
    public class Approvisionnement
    {
        [Key]
        public int Approvisionnement_Id { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Approvisionnement_Date { get; set; }
        [ForeignKey("Point_Vente")]
        public int Approvisionnement_PointVenteId { get; set; }
        [ForeignKey("Produit_Vendable")]
        public int Approvisionnement_ProduitId { get; set; }
        [ForeignKey("Forme_Produit")]
        public int Approvisionnement_FormeProduitId { get; set; }
        [ForeignKey("ProduitAppro")]
        public int Approvisionnement_ProduitApproID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Approvisionnement_DateSaisie { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? Approvisionnement_DateModification { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        public string Approvisionnement_UtilisateurId { get; set; } 
        [Column(TypeName = "nvarchar(450)")]
        public string Approvisionnement_ValideParId { get; set; }
        [Column(TypeName = "int")]
        public int Approvisionnement_AbonnementId { get; set; }
        [ForeignKey("Atelier")]
        public int Approvisionnement_AtelierID { get; set; }
        [Column(TypeName = "int")]
        public int Approvisionnement_Etat { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Approvisionnement_Quantite { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Approvisionnement_CoutDeRevient { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Approvisionnement_QuantiteRestante { get; set; }

        public ProduitVendable Produit_Vendable { get; set; }
        public Point_Vente Point_Vente { get; set; }
        public ProduitAppro ProduitAppro { get; set; }
        public Atelier Atelier { get; set; }
        public Forme_Produit Forme_Produit { get; set; }

    }
}
