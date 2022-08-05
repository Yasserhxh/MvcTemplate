using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Famille_Produit")]
    public class FamilleProduit
    {
        public FamilleProduit()
        {
            PointVente_Link = new Collection<PointVente_Famille>();
            production_Link = new Collection<PointPorduction_Famille>();
            sousFamille = new Collection<SousFamille>();
        }
        [Key]
        public int FamilleProduit_Id { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string FamilleProduit_Libelle { get; set; }
        [Column(TypeName = "nvarchar(350)")]
        public string FamilleProduit_Image { get; set; }
        [ForeignKey("Abonnemet_Client")]
        public int FamilleProduit_AbonnemnetId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FamilleProduit_DateCreation { get; set; }
        public int FamilleProduit_IsActive { get; set; }
        public ICollection<PointVente_Famille> PointVente_Link { get; set; }
        public ICollection<PointPorduction_Famille> production_Link { get; set; }
        public ICollection<SousFamille> sousFamille { get; set; }
    }
}
