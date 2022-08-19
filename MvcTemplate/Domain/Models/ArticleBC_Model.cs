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
        public decimal ArticleBC_PU { get; set; }
        public decimal ArticleBC_Total { get; set; }
        public decimal ArticleBC_QteRest { get; set; }
        public int ArticleBC_UniteMesure { get; set; }
        public decimal ArticleBC_Quantite { get; set; }
        public int ArticleBC_BCID { get; set; }
        public BonDeCommande_Model bonDeCommande { get; set; }
        public MatierePremiereModel MatierePremiere { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
    }
}
