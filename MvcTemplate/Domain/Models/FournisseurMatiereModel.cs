using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class FournisseurMatiereModel
    {
        public int Id { get; set; }
        public int Founisseur_Id { get; set; }
        public int MatierePremiere_Id { get; set; }
        public int Abonnement_ID { get; set; }
        public int IsActive { get; set; }
        public FournisseurModel Founisseur { get; set; }
        public MatierePremiereModel Matiere_Premiere { get; set; }
    }
}
