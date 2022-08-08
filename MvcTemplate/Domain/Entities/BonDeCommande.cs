using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Bon_De_Commande")]
    public class BonDeCommande
    {
        [Key]
        public int BonDeCommande_ID { get; set; }   
        public int BonDeCommande_Numero { get; set; }
        [ForeignKey("Fournisseur")]
        public int BonDeCommande_FournisseurID { get; set; }   
        public string BonDeCommande_CreePar { get; set; }
        [ForeignKey("Lieu_Stockage")]
        public int BonDeCommande_PointStockID { get; set; }
        public string BonDeCommande_ValidéPar { get; set; }   
        public DateTime BonDeCommande_DateCreation { get; set; }   
        public DateTime BonDeCommande_DateValidation { get; set; }   
        public int BonDeCommande_AbonnementID { get; set; }
        public decimal BonDeCommande_TotalHT { get; set; }   
        public decimal BonDeCommande_TotalTVA { get; set; }   
        public decimal BonDeCommande_TotalTTC { get; set; }   
        public Fournisseur Fournisseur { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; }
    }
}
