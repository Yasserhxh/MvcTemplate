using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class EchangeProduit_DetailsModel
    {
        public int EchangeProduitDetails_ID { get; set; }
        public int EchangeProduitDetails_EchangeID { get; set; }
        public int EchangeProduitDetails_FromeID { get; set; }
        public int EchangeProduitDetails_ProduitID { get; set; }
        public decimal EchangeProduitDetails_Quantite { get; set; }
        public int EchangeProduitDetails_UniteID { get; set; }
        public Echange_ProduitsModel Echange_Produits { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
    }
}
