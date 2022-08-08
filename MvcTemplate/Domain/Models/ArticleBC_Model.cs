using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ArticleBC_Model
    {
        public int ArticleBC_ID { get; set; }
        public string ArticleBC_Designation { get; set; }
        public int ArticleBC_MatiereID { get; set; }
        public decimal ArticleBC_Quantie { get; set; }
        public int ArticleBC_BonCommandeID { get; set; }
        public BonDeCommande_Model bonDeCommande { get; set; }
        public MatierePremiereStockageModel MatierePremiere_Stokage { get; set; }
    }
}
