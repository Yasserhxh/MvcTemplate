using Domain.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("PointVente_User")]
    public class PointVente_User
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public string User_Id { get; set; }
        [ForeignKey("Point_Vente")]
        public int PointVente_Id { get; set; }
        public int Abonnement_ID { get; set; }
        public int IsActive { get; set; }
        public ApplicationUser User { get; set; }
        public Point_Vente Point_Vente { get; set; }
    }
}
