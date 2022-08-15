using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ArticleBL_Model
    {
        public int ArticleBL_ID { get; set; }
        public string ArticleBL_Designation { get; set; }
        public string ArticleBL_LotFournisseur { get; set; }
        public string ArticleBL_LotTemp { get; set; }
        public int ArticleBL_MatiereID { get; set; }
        public decimal ArticleBL_Quantie { get; set; }
        public decimal ArticleBL_PU { get; set; }
        public decimal ArticleBL_PrixTotal { get; set; }
        public int ArticleBL_UniteMesureID { get; set; }
        public int ArticleBL_BonLivraisonID { get; set; }
        public BonDeLivraison_Model bonDeLivraison { get; set; }
        public MatierePremiereStockageModel MatierePremiere_Stokage { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }

    }
}
