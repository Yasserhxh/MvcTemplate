using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class MatPremAllergene_Model
    {
        public int MatPrem_AleergID { get; set; }
        public int MatiereID { get; set; }
        public int AllergenID { get; set; }
        public int IsActive { get; set; }
        public int AbonnementID { get; set; }
        public MatierePremiereModel MatierePremiere { get; set; }
        public AllergeneModel Allgerene { get; set; }
    }
}
