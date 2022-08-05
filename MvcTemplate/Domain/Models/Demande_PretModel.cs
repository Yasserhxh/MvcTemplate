using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Demande_PretModel
    {
        public Demande_PretModel()
        {
            details = new List<DemandePret_DetailsModel>();
        }
        public int DemandePret_ID { get; set; }
        //public int DemandePret_ProductionID { get; set; }
        public int DemandePret_AtelierID { get; set; }
        public int DemandePret_StockID { get; set; }
        public int DemandePret_AbonnementID { get; set; }
        public string DemandePret_Etat { get; set; }
        public DateTime DemandePret_DateCreation { get; set; }
        //public PackageProductionModel Package_Production { get; set; }
        public AtelierModel Atelier { get; set; }
        public Lieu_StockageModel Lieu_Stockage { get; set; }
        public List<DemandePret_DetailsModel> details { get; set; }
    }
}
