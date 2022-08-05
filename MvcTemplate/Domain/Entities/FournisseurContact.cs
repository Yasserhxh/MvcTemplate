using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Fournisseur_Contact")]
    public class FournisseurContact
    {
        [Key]
        public int FournisseurContact_ID { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string FournisseurContact_Nom { get; set; }
        [ForeignKey("Fonction")]
        public int FournisseurContact_FonctionID { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string FournisseurContact_Email { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string FournisseurContact_GSM { get; set; }
        [ForeignKey("Founisseur")]
        public int? FournisseurContact_FournisseurID { get; set; }
        [ForeignKey("Fournisseur_Produits")]
        public int? FournisseurContact_FournisseurProduitID { get; set; }
        public Fournisseur Founisseur { get; set; }
        public FournisseurProduits Fournisseur_Produits { get; set; }
        public Fonction Fonction { get; set; }
    }
}
