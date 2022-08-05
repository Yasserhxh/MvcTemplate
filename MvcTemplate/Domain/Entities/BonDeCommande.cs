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
        public int BonDeCommande_CreePar { get; set; }   
        public int BonDeCommande_ValidéPar { get; set; }   
        public int BonDeCommande_DateCreation { get; set; }   
        public int BonDeCommande_DateValidation { get; set; }   
        public int BonDeCommande_TotalHT { get; set; }   
        public int BonDeCommande_TotalTVA { get; set; }   
        public int BonDeCommande_TotalTTC { get; set; }   
        public Fournisseur Fournisseur { get; set; }
    }
}
