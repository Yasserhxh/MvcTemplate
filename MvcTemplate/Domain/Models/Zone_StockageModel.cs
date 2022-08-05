using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Zone_StockageModel
    {
        public Zone_StockageModel()
        {
            Section_Stockage = new List<Section_StockageModel>();
        }
        public int ZoneStockage_Id { get; set; }
        public int? ZoneStockage_LieuStockageId { get; set; }
        public int? ZoneStockage_FormeStockageId { get; set; }
        public decimal? ZoneStockage_CapaciteStockage { get; set; }
        public int? ZoneStockage_UniteMesureId { get; set; }
        public int? ZoneStockage_TypeContenuId { get; set; }
        public int ZoneStockage_IsActive { get; set; }
        public int? ZoneStockage_AbonnementId { get; set; }
        public DateTime? ZoneStockage_DateCreation { get; set; }
        public string ZoneStockage_Designation { get; set; }
        public Type_ContenuModel Type_Contenu { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public Lieu_StockageModel Lieu_Stockage { get; set; }
        public Forme_StockageModel Forme_Stockage { get; set; }

        public List<Section_StockageModel> Section_Stockage { get; set; }

        /*   public Unite_MesureModel UNITE { get; set; }
           public Type_ContenuModel TYPE { get; set; }
           public Lieu_StockageModel LIEU { get; set; }
           public Forme_StockageModel FORME { get; set; }*/
    }
}
