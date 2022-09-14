using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Point_Vente")]
    public class Point_Vente
    {
        public Point_Vente()
        {
            Famille_Link = new Collection<PointVente_Famille>();
            PositionsVente = new Collection<PositionVente>();
        }
        [Key]
        public int PointVente_Id { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string PointVente_Nom { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string PointVente_Adresse { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string PointVente_NomResponsable { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string PointVente_Telephone { get; set; }
        public int PointVente_IsActive { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? PointVente_DateSaisie { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        public string PointVente_UtilisateurId { get; set; }
        [Column(TypeName = "int")]
        public int PointVente_AbonnementId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime PointVente_DateCreation { get; set; }
        [ForeignKey("Ville")]
        public int PointVente_VilleID { get; set; }
        [ForeignKey("Atelier")]
        public int? PointVente_AtelierID { get; set; }
        [ForeignKey("Lieu_Stockage")]
        public int? PointVente_StockID { get; set; }
        [Column(TypeName = "int")]

        public int PointVente_CodePostal { get; set; }
        [Column(TypeName = "int")]
        public int PointVente_TypeService { get; set; }
        public string PointVente_Logo { get; set; }
        public Ville Ville { get; set; }
        public ICollection<PointVente_Famille> Famille_Link { get; set; }
        public ICollection<PositionVente> PositionsVente { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; }
        public Atelier Atelier { get; set; }

    }
}
