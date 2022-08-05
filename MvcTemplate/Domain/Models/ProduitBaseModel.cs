using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ProduitBaseModel
    {
        public int ProduitBase_ID { get; set; }
        public string ProduitBase_Designation { get; set; }
        public int ProduitBase_FormeStockageID { get; set; }
        public int ProduitBase_UniteMesureID { get; set; }
        public decimal ProduitBase_CoutDeRevient { get; set; }
        public int ProduitBase_IsActive { get; set; }
        public int ProduitBase_AbonnementID { get; set; }
        public DateTime ProduitBase_DateCreation { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public Forme_StockageModel Forme_Stockage { get; set; }
        public List<UniteMesure_ProdBaseModel> unites_Utilisation { get; set; }

    }
}
