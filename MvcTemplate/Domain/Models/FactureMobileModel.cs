using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class FactureMobileModel
    {
        public int UniteId { get; set; }
        public decimal Prix { get; set; }
        public decimal Quantite { get; set; }
        public int FormeProduitId { get; set; }
    }

    public class FactureMobileCallModel
    {
        public string userId { get; set; }
        public int? venteModePaimentId { get; set; }
        public string venteCommentaire { get; set; }
        public string Vente_NumeroTicket { get; set; }
        public decimal? tauxRemise { get; set; }
        public decimal? totalRemise { get; set; }
        public decimal? totalSansRemise { get; set; }
        public bool? isCommande { get; set; }
        public bool? isPerte { get; set; }
        public bool? isGratuite { get; set; }
        public CommandeMobileCallModel commande { get; set; }
        public List<FactureMobileModel> factureMobiles { get; set; }
        public List<TvaModel> tvaList { get; set; }
    }
    public class CommandeMobileCallModel
    {
        public string userId { get; set; }
        public string clientNom { get; set; }
        public string clientTelephone { get; set; }
        public DateTime dateLivraison { get; set; }
        public decimal montantAvance { get; set; }
        public string Commande_NumeroTicket { get; set; }
    }
}
