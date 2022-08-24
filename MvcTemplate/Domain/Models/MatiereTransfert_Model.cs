using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class MatiereTransfert_Model
    {
        public int MatiereTrans_ID { get; set; }
        public int MatiereTrans_MatiereID { get; set; }
        public decimal MatiereTrans_Quantite { get; set; }
        public int MatiereTrans_UniteID { get; set; }
        public string MatiereTrans_LotNumber { get; set; }
        public string MatiereTrans_Statut { get; set; }
        public string MatiereTrans_ValidePar { get; set; }
        public DateTime? MatiereTrans_DateValidation { get; set; }
        public int MatiereTrans_TransferID { get; set; }
        public int MatiereTrans_StockID { get; set; }
        public MatierePremiereModel MatierePremiere { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public TransfertMatiere_Model Transfert_Matiere { get; set; }
        public Lieu_StockageModel Lieu_Stockage { get; set; }
    }
}
