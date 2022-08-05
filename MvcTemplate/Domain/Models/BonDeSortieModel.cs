using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class BonDeSortieModel
    {
        public BonDeSortieModel()
        {
            Bon_Details = new List<BonDetailsModel>();
        }
        public int BonDeSortie_ID { get; set; }
        public string BonDeSortie_Libelle { get; set; }
        public DateTime BonDeSortie_DateProduction { get; set; }
        public DateTime BonDeSortie_DateCreation { get; set; }
        public int BonDeSortie_AbonnementID { get; set; }
        public int BonDeSortie_StockID { get; set; }

        public List<BonDetailsModel> Bon_Details { get; set; }
        public Lieu_StockageModel Lieu_Stockage { get; set; }

    }
}
