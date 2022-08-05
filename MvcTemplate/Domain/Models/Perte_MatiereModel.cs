using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Perte_MatiereModel
    {
        public int PerteMatiere_ID { get; set; }
        public int PerteMatiere_MatierePremiereStockageID { get; set; }
        public decimal PerteMatiere_Quantite { get; set; }
        public int PerteMatiere_UniteMesureID { get; set; }
        public int PerteMatiere_AtelierID { get; set; }
        public DateTime PerteMatiere_DateCreation { get; set; }
        public int PerteMatiere_Utilisateur { get; set; }
        public int PerteMatiere_AbonnementID { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public MatiereStock_AtelierModel matiereStock_Atelier { get; set; }
        public AtelierModel Atelier { get; set; }
    }
}
