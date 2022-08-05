using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Unite_MesureMatiereModel
    {
        public int Id { get; set; }
        public int Unite_Id { get; set; }
        public int MatierePremiere_Id { get; set; }
        public int Abonnement_ID { get; set; }
        public int IsActive { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public MatierePremiereModel Matiere_Premiere { get; set; }
    }
}
