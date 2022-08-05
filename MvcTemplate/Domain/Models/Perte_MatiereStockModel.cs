using Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Perte_MatiereStockModel
    {
        public int PerteMatiereStock_ID { get; set; }
        public int PerteMatiere_MatierePremiereStockageID { get; set; }
        public decimal PerteMatiere_Quantite { get; set; }
        public int PerteMatiere_UniteMesureID { get; set; }
        public int PerteMatiere_StockID { get; set; }
        public DateTime PerteMatiere_DateCreation { get; set; }
        public string PerteMatiere_Utilisateur { get; set; }
        public int PerteMatiere_AbonnementID { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public MatierePremiereStockageModel MatierePremiere_Stokage { get; set; }
        public Lieu_StockageModel Lieu_Stockage { get; set; }
        public ApplicationUser User { get; set; }
    }
}
