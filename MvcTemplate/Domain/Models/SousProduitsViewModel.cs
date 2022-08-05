using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class SousProduitsViewModel
    {
        public string SousProduit_ProduitDesi { get; set; }
        public int SousProduit_ProduitID { get; set; }
        public int SousProduit_ProduitPackageID { get; set; }
        public string SousProduit_ProduitType { get; set; }
        public string SousProduit_FormeProduit { get; set; }
        public int SousProduit_FormeProduitID { get; set; }
        public decimal SousProduit_Quantite { get; set; }
        public decimal SousProduit_QuantiteEnStock { get; set; }
        public int SousProduit_UniteMesureId { get; set; }
        public string SousProduit_UniteMesureDesi { get; set; }
    }
}
