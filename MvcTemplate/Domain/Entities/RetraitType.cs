using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{ 
    [Table("Retrait_Type")]
    public class RetraitType
    {
        [Key]
        public int RetraitType_ID { get; set; }
        [Column(TypeName ="nvarchar(250)")]
        public string RetraitType_Libelle { get; set; }
    }
}
