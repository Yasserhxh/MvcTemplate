using Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PointVente_UserModel
    {
        public int Id { get; set; }
        public string User_Id { get; set; }
        public int PointVente_Id { get; set; }
        public int Abonnement_ID { get; set; }
        public int IsActive { get; set; }
        public ApplicationUser User { get; set; }
        public Point_VenteModel Point_Vente { get; set; }
    }
}
