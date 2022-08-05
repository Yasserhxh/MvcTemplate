using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class DemandePret_DetailsModel
    {
        public int DemandePretDetails_ID { get; set; }
        public int DemandePretDetails_DemandeID { get; set; }
        public int DemandePretDetails_ProduitID { get; set; }
        public int DemandePretDetails_FormeID { get; set; }
        public int DemandePretDetails_UniteMesureID { get; set; }
        public decimal DemandePretDetails_QuantiteLivre { get; set; }
        public decimal DemandePretDetails_Quantite { get; set; }
        public Demande_PretModel Demande_Pret { get; set; }
        public ProduitPretConsomerModel Produit_PretConsomer { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
    }
}
