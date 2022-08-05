using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class GratuiteDetailsModel
    {
        public int GratuiteDetails_ID { get; set; }
        public int GratuiteDetails_GratuiteID { get; set; }
        public int GratuiteDetails_FormeID { get; set; }
        public int GratuiteDetails_UniteVenteID { get; set; }
        public decimal GratuiteDetails_Quantite { get; set; }
        public decimal GratuiteDetails_CoutDeRevient { get; set; }
        public DateTime GratuiteDetails_DateCreation { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }
        public GratuiteModel Gratuite { get; set; }
    }
}
