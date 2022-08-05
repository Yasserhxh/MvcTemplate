using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Zone_Stockage")]
    public class Zone_Stockage
    {
        public Zone_Stockage()
        {
            Section_Stockage = new Collection<Section_Stockage>();
        }
        [Key]
        public int ZoneStockage_Id { get; set; }
        [ForeignKey("Lieu_Stockage")]
        public int? ZoneStockage_LieuStockageId { get; set; }
        [ForeignKey("Forme_Stockage")]
        public int? ZoneStockage_FormeStockageId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ZoneStockage_CapaciteStockage { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int? ZoneStockage_UniteMesureId { get; set; }
        [ForeignKey("Type_Contenu")]
        public int? ZoneStockage_TypeContenuId { get; set; }
        public int? ZoneStockage_IsActive { get; set; }
        [Column(TypeName = "int")]
        public int? ZoneStockage_AbonnementId { get; set; }
        [Column(TypeName = "smalldatatime")]
        public DateTime? ZoneStockage_DateCreation { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string ZoneStockage_Designation { get; set; }

        public Type_Contenu Type_Contenu { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; }
        public Forme_Stockage Forme_Stockage { get; set; }
        public ICollection<Section_Stockage> Section_Stockage { get; set; }

    }
}
