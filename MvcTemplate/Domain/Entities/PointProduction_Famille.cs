using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Domain.Entities
{
    [Table("PointProduction_Famille")]
    public class PointPorduction_Famille
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Atelier")]
        public int PointProduction_ID { get; set; }
        [ForeignKey("Famille_Produit")]
        public int FamilleProduit_ID { get; set; }
        [Column(TypeName = "int")]
        public int Abonnement_Id { get; set; }
        [Column(TypeName = "int")]
        public int Is_Active { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime DateCreation { get; set; }
        public Atelier Atelier { get; set; }
        public FamilleProduit Famille_Produit { get; set; }
    }
}
