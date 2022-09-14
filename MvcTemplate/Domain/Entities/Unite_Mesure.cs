using Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Unite_Mesure")]
    public class Unite_Mesure
    {
        [Key]
        public int UniteMesure_Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string UniteMesure_Libelle { get; set; }
        public ICollection<Unite_MesureMatiere> matiereLink { get; set; }
        [ForeignKey("Unite_Categorie")]
        public int Unite_CategorieID { get; set; }
        public Unite_Categorie Unite_Categorie { get; set; }
    }
}
