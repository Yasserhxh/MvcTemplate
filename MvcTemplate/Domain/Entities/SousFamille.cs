using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Sous_Famille")]
    public class SousFamille
    {
        [Key]
        public int SousFamille_ID { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string SousFamille_Libelle { get; set; }
        [Column(TypeName = "nvarchar(350)")]
        public string SousFamille_Image { get; set; }
        [ForeignKey("Famille_Produit")]
        public int SousFamille_ParentID { get; set; }
        [Column(TypeName = "int")]
        public int SousFamille_AbonnementID { get; set; }
        public FamilleProduit Famille_Produit { get; set; }
    }
}
