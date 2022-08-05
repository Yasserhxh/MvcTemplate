using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class FormeDetailsModel
    {
        public int FormeDetails_ID { get; set; }
        public int FormeDetails_FormeProduitID { get; set; }
        public int FormeDetails_PointVenteID { get; set; }
        //public string FormeDetails_Libelle { get; set; }
        public decimal FormeDetails_Quantite { get; set; }
        public int FormeDetails_ProduitPackageID { get; set; }
        public DateTime FormeDetails_DateCreation { get; set; }
        public int FormeDetails_AbonnementID { get; set; }
        public ProduitPackageModel ProduitPackage { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }
        public Point_VenteModel Point_Vente { get; set; }
    }
}
