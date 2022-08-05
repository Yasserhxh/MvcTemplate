using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Reception_StockModel
    {
        public int ReceptionStock_ID { get; set; }
        public int ReceptionStock_AtelierID { get; set; }
        public int ReceptionStock_StockID { get; set; }
        public int ReceptionStock_MatiereID { get; set; }
        public int ReceptionStock_UniteID { get; set; }
        public decimal ReceptionStock_Quantite { get; set; }
        public DateTime ReceptionStock_DateReception { get; set; }
        public DateTime ReceptionStock_DateCreation { get; set; }
        public int ReceptionStock_AbonnementID { get; set; }
        public AtelierModel Atelier { get; set; }
        public Lieu_StockageModel Lieu_Stockage { get; set; }
        public MatierePremiereStockageModel MatierePremiere_Stokage { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
    }
}
