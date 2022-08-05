using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class DetailsProductionModel
    {
        public int ProductionDetails_Id { get; set; }
        public int ProductionDetails_ProductionID { get; set; }
        public int ProductionDetails_FormeID { get; set; }
        public decimal ProductionDetails_Quantite { get; set; }
        public decimal ProductionDetails_CoutDeRevient { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }
        public PackageProductionModel Package_Production { get; set; }
    }
}
