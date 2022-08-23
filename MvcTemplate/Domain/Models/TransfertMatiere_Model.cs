using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class TransfertMatiere_Model
    {
        public TransfertMatiere_Model()
        {
            listeMatiere = new List<MatiereTransfert_Model>();
        }
        public int TransfertMat_ID { get; set; }
        public string TransfertMat_CreePar { get; set; }
        public string TransfertMat_Statut { get; set; }
        public string TransfertMat_ValidePar { get; set; }
        public string TransfertMat_Numero { get; set; }
        public DateTime TransfertMat_DateCreation { get; set; }
        public DateTime? TransfertMat_DateValidation { get; set; }
        public int TransfertMat_AbonnementID { get; set; }
        public List<MatiereTransfert_Model> listeMatiere { get; set; }
    }
}
