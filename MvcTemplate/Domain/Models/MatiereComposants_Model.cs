using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class MatiereComposants_Model
    {
        public int MatiereComposants_ID { get; set; }
        public int MatiereComposants_MatiereID { get; set; }
        public string MatiereComposants_Name { get; set; }
        public string MatiereComposants_NameAR { get; set; }
        public string MatiereComposants_Valeur { get; set; }
        public int MatiereComposants_IsActive { get; set; }
        public int MatiereComposants_AbonnementID { get; set; }
        public MatierePremiereModel MatierePremiere { get; set; }
    }
}
