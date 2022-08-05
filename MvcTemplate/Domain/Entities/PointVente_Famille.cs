using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("PointVente_Famille")]
    public class PointVente_Famille
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Point_Vente")]
        public int PointVente_Id { get; set; }
        [ForeignKey("Famille_Produit")]
        public int FamilleProduit_Id { get; set; }
        [Column(TypeName ="int")]
        public int Abonnement_Id { get; set; }
        [Column(TypeName = "int")]
        public int IsActive { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime DateCreation { get; set; }
        public Point_Vente Point_Vente { get; set; }
        public FamilleProduit Famille_Produit { get; set; }
    }
}
