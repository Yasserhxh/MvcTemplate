using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("MouvementCaisse_Type")]
    public class MouvementCaisse_Type
    {
        [Key]
        public int TypeMouvementCaisse_ID { get; set; }
        [Column(TypeName ="nvarchar(150)")]
        public string TypeMouvementCaisse_Libelle { get; set; }
    }
}
