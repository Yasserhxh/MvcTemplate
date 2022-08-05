using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class MatiereStock_AtelierModel
    {
        public int MatiereStockAtelier_ID { get; set; }
        public int MatiereStockAtelier_AtelierID { get; set; }
        public int MatiereStockAtelier_MatierePremiereID { get; set; }
        public decimal MatiereStockAtelier_QauntiteActuelle { get; set; }
        public decimal MatiereStockAtelier_QuatiteAvecPlanification { get; set; }
        public DateTime MatiereStockAtelier_DateCreation { get; set; }
        public int MatiereStockAtelier_AbonnementID { get; set; }
        public AtelierModel Atelier { get; set; }
        public MatierePremiereModel Matiere_Premiere { get; set; }
    }
}
