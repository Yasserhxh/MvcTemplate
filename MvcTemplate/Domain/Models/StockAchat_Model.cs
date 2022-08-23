using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class StockAchat_Model
    {
        public StockAchat_Model()
        {
            unite_Utilisation = new List<Unite_MesureModel>();
        }
        public int StockAchat_ID { get; set; }
        public int StockAchat_MatiereID { get; set; }
        public int StockAchat_UniteMesureID { get; set; }
        public string StockAchat_LotIntern { get; set; }
        public string StockAchat_LotFournisseur { get; set; }
        public decimal StockAchat_QuantiteMatiere { get; set; }
        public decimal StockAchat_QuantiteRestante { get; set; }
        public int StockAchat_AbonnementID { get; set; }
        public MatierePremiereModel MatierePremiere { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public List<Unite_MesureModel> unite_Utilisation { get; set; }
    }
}
