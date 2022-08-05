using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Approvisionnement_MatiereModel
    {
        public int ApprovisionnementMatiere_ID { get; set; }
        public int ApprovisionnementMatiere_MatiereStockID { get; set; }
        public int ApprovisionnementMatiere_AtelierID { get; set; }
        public int ApprovisionnementMatiere_StockID { get; set; }
        public int ApprovisionnementMatiere_UniteID { get; set; }
        public decimal ApprovisionnementMatiere_Quantite { get; set; }
        public DateTime ApprovisionnementMatiere_DateCreation { get; set; }
        public DateTime ApprovisionnementMatiere_DateApprovisionnement { get; set; }
        public int ApprovisionnementMatiere_AbonnementID { get; set; }
        public string ApprovisionnementMatiere_Etat { get; set; }
        public string ApprovisionnementMatiere_Utilisateur { get; set; }
        public string ApprovisionnementMatiere_ValidéPar { get; set; }
        public MatierePremiereStockageModel MatierePremiereStockage { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public AtelierModel Atelier { get; set; }
        public Lieu_StockageModel Lieu_Stockage { get; set; }
    }
}
