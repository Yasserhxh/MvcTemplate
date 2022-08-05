using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Ville")]
    public class Ville
    {
        [Key]
        public int Ville_ID { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string Ville_Libelle { get; set; }
    }
}
