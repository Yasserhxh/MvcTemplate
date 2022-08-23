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
        public int MatiereTrans_TransferID { get; set; }
        public MatierePremiereModel MatierePremiere { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public TransfertMatiere_Model Transfert_Matiere { get; set; }
    }
}
