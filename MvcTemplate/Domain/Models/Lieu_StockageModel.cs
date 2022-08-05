using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Lieu_StockageModel
    {
        public Lieu_StockageModel()
        {
            Zone_Stockage = new List<Zone_StockageModel>();
        }
        public int LieuStockag_Id { get; set; }
        public string LieuStockag_Nom { get; set; }
        public string LieuStockag_Adresse { get; set; }
        public string LieuStockag_NomResponsable { get; set; }
        public string LieuStockag_Telephone { get; set; }
        public int LieuStockag_IsActive { get; set; }
        public int LieuStockag_AbonnementId { get; set; }
        public DateTime LieuStockag_DateCreation { get; set; }
        public string LieuStockag_UTILISATEUR { get; set; }
        public int LieuStockag_VilleID { get; set; }
        public int LieuStockag_CodePostal { get; set; }
        public int LieuStockag_ParDefaut { get; set; }
        public List<Zone_StockageModel> Zone_Stockage { get; set; }
        public VilleModel Ville { get; set; }

    }
}
