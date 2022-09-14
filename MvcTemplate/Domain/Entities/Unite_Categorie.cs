using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Unite_Categorie")]
    public class Unite_Categorie
    {
        [Key]
        public int UniteCategorie_Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string UniteCategorie_Libelle { get; set; }

    }
}
