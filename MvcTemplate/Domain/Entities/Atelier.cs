using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Atelier")]
    public class Atelier
    {
        [Key]
        public int Atelier_ID { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string Atelier_Nom { get; set; }
        [Column(TypeName = "nvarchar(250)")]

        public string Atelier_Adresse { get; set; }
        [Column(TypeName = "nvarchar(250)")]

        public string Atelier_NomResponsable { get; set; }
        [Column(TypeName = "nvarchar(50)")]

        public string Atelier_Telephone { get; set; }
        [Column(TypeName = "int")]

        public int Atelier_IsActive { get; set; }
        [Column(TypeName = "nvarchar(250)")]

        public string Atelier_UTILISATEUR { get; set; }
        [Column(TypeName = "int")]

        public int Atelier_AbonnementID { get; set; }
        [Column(TypeName = "smalldatetime")]

        public DateTime Atelier_DateCreation { get; set; }
        [ForeignKey("Ville")]
        public int Atelier_VilleID { get; set; }
        [Column(TypeName = "int")]
        public int Atelier_CodePostal { get; set; }
        public Ville Ville { get; set; }
        [ForeignKey("Lieu_Stockage")]
        public int? Atelier_StockID { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; }

    }
}
