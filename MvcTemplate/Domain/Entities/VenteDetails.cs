using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("VenteDetails")]
    public class VenteDetails
    {
        [Key]
        public int VenteDetails_Id { get; set; }
        [ForeignKey("Vente")]
        public int VenteDetails_VentId { get; set; }
        [ForeignKey("Forme_Produit")]
        public int VenteDetails_FormeProduitId { get; set; }
        [Column(TypeName = "decimal(12,8)")]
        public decimal VenteDetails_Quantite { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int VenteDetails_UniteId { get; set; }
        [Column(TypeName ="decimal(12,8)")]
        public decimal VenteDetails_CoutDeRevient { get; set; }
        [Column(TypeName = "decimal(12,8)")]
        public decimal VenteDetails_Prix { get; set; }
        [Column(TypeName = "decimal(12,8)")]
        public decimal VenteDetails_Marge { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime VenteDetails_DateCreation { get; set; }
        [Column(TypeName = "int")]
        public int VenteDetails_AbonnementID { get; set; }
        [Column(TypeName = "int")]
        public int VenteDetails_FlagCloture { get; set; }
        public Vente Vente { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
    }
}
