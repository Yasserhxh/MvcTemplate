using System;

namespace Domain.Models
{
    public class MatierePremiereStockageModel
    {
        public int MatierePremiereStokage_Id { get; set; }
        public int MatierePremiereStokage_SectionStockageId { get; set; }
        public int MatierePremiereStokage_MatierePremiereId { get; set; }
        public decimal MatierePremiereStokage_StockMinimum { get; set; }
        public decimal MatierePremiereStokage_StockMaximum { get; set; }
        public int MatierePremiereStokage_IsActive { get; set; }
        public decimal MatierePremiereStokage_QuantiteActuelle { get; set; }
        public int MatierePremiereStokage_AbonnementId { get; set; }
        public DateTime MatierePremiereStokage_DateCreation { get; set; }
        public Section_StockageModel Section_Stockage { get; set; }
        public MatierePremiereModel Matiere_Premiere { get; set; }
    }
}
