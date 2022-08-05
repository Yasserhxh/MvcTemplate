using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Lieu_Stockage")]
    public class Lieu_Stockage
    {
        public Lieu_Stockage()
        {
            Zone_Stockage = new Collection<Zone_Stockage>();
        }
        [Key]
        public int LieuStockag_Id { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string LieuStockag_Nom { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string LieuStockag_Adresse { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string LieuStockag_NomResponsable { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string LieuStockag_Telephone { get; set; }
        public int LieuStockag_IsActive { get; set; }
        [Column(TypeName = "int")]
        public int LieuStockag_AbonnementId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime LieuStockag_DateCreation { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string LieuStockag_UTILISATEUR { get; set; }
        [ForeignKey("Ville")]
        public int LieuStockag_VilleID { get; set; }
        [Column(TypeName = "int")]
        public int LieuStockag_CodePostal { get; set; }
        [Column(TypeName = "int")]
        public int LieuStockag_ParDefaut { get; set; }
        public ICollection<Zone_Stockage> Zone_Stockage { get; set; }
        public Ville Ville { get; set; }

    }
}
