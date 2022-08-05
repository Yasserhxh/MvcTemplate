using Domain.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Atelier_User")]
    public class Atelier_User
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public string User_Id { get; set; }
        [ForeignKey("Atelier")]
        public int Atelier_Id { get; set; }
        public int Abonnement_ID { get; set; }
        public int IsActive { get; set; }
        public ApplicationUser User { get; set; }
        public Atelier Atelier { get; set; }
    }
}
