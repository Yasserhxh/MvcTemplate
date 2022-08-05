using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class RetourDetailsModel
    {
        public int RetourDetails_ID { get; set; }
        public int RetourDetails_RetourID { get; set; }
        public int RetourDetails_FormeID { get; set; }
        public int RetourDetails_UniteVente { get; set; }
        public decimal RetourDetails_PrixProduit { get; set; }
        public decimal RetourDetails_Quantite { get; set; }
        public decimal RetourDetails_PrixTotal { get; set; }
        public int RetourDetails_isPerte { get; set; }
        public DateTime RetourDetails_DateRetour { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public RetourProduitsModel Retour_Produit { get; set; }
    }
}
