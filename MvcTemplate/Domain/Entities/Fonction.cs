using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Fonction")]
    public class Fonction
    {
        [Key]
        public int Fonction_ID { get; set; }
        public string Fonction_Libelle { get; set; }
    }
}
