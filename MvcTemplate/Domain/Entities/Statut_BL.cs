using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Statut_BL")]
    public class Statut_BL
    {
        [Key]
        public int StatutBL_ID { get; set; }
        public string StatutBL_Name { get; set; }
    }
}
