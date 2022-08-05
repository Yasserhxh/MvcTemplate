using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Perte_PretModel
    {
        public int PertePret_ID { get; set; }
        public int PertePret_ProduitPretAtelierID { get; set; }
        public decimal PertePret_Quantite { get; set; }
        public int PertePret_UniteMesureID { get; set; }
        public int PertePret_AtelierID { get; set; }
        public int PertePret_AbonnmentID { get; set; }
        public DateTime PertePret_DateCreation { get; set; }
        public AtelierModel Atelier { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public ProduitPret_AtelierModel ProduitPret_Atelier { get; set; }
    }
}
