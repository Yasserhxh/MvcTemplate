using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Facture")]
    public class Facture
    {
        public Facture()
        {
            listeBL = new Collection<BonDeLivraison>();
        }
        [Key]
        public int Facture_ID { get; set; }
        public string Facture_Numero { get; set; }
        [ForeignKey("Fournisseur")]
        public int Facture_FournisseurID { get; set; }
        [ForeignKey("bonDeCommande")]
        public int Facture_BonDeCommandeID { get; set; }
        public int Facture_AbonnementID { get; set; }
        [ForeignKey("Lieu_Stockage")]
        public int Facture_PointStockID { get; set; }
        public decimal Facture_MontantTVA { get; set; }
        public decimal Facture_TotalHT { get; set; }
        public decimal Facture_TotalTTC { get; set; }
        public DateTime Facture_DateFacture { get; set; }
        public DateTime Facture_DateSaisie { get; set; }
        public BonDeCommande bonDeCommande { get; set; }
        public Fournisseur Fournisseur { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; }
        public ICollection<BonDeLivraison> listeBL {get;set;}
    }
}
