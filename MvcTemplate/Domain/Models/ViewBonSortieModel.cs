using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ViewBonSortieModel
    {
        public string MatierePremiere_Libelle { get; set; }
        public decimal MatierePremiere_QuantiteEnMagasin { get; set; }
        public decimal MatierePremiere_QuantiteAvecPlanification { get; set; }
        public string MatierePremiere_UniteMesureLibelle { get; set; }
        public string MatierePremiere_UniteMesureMag { get; set; }
    }
}
