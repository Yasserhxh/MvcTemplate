using System;

namespace Domain.Models
{
    public class Forme_StockageModel
    {
        public int FormStockage_Id { get; set; }
        public string FormStockage_Libelle { get; set; }
        public DateTime FormStockage_DateCreation { get; set; }
        public int FormStockage_AbonnementId { get; set; }

    }
}
