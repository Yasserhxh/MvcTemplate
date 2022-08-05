using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Authentication
{
    [Table("LogoUser")]
    public class LogoUser
    {
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Logo { get; set; }
        [Column(TypeName = "int")]
        public int Abonnement_Id { get; set; }
    }
}
