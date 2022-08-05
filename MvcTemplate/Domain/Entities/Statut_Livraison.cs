using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Statut_Livraison")]
    public class Statut_Livraison
    {
        [Key]
        public int StatutCommande_Id { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string StatutCommande_Libelle { get; set; }
        [Column(TypeName = "int")]
        public int StatutCommande_AbonnementId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime StatutCommande_DateCreation { get; set; }
    }

}
