using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Type_Contenu")]
    public class Type_Contenu
    {
        [Key]
        public int TypeContenu_Id { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string TypeContenu_Libelle { get; set; }
    }
}
