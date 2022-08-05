using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PerteDetailsModel
    {
        public int PerteDetails_ID { get; set; }
        public int PerteDetails_PerteID { get; set; }
        public int PerteDetails_FormeID { get; set; }
        public int PerteDetails_UniteVenteID { get; set; }
        public decimal PerteDetails_Quantite { get; set; }
        public decimal PerteDetails_CoutDeRevient { get; set; }
        public DateTime PerteDetails_DatePerte { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }
        public PerteModel Perte { get; set; }
    }
}
