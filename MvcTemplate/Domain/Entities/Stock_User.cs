using Domain.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Stock_User")]
    public class Stock_User
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public string User_Id { get; set; }
        [ForeignKey("Lieu_Stockage")]
        public int Stock_Id { get; set; }
        public int Abonnement_ID { get; set; }
        public int IsActive { get; set; }
        public ApplicationUser User { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; }
    }
}
