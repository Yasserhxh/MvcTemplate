using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Article_BC")]

    public class Article_BC
    {
        [Key]
        public int ArticleBC_ID { get; set; }
        public string ArticleBC_Designation { get; set; }
        [ForeignKey("MatierePremiere_Stokage")]
        public int ArticleBC_MatiereID { get; set; }
        public decimal ArticleBC_Quantie { get; set; }
        [ForeignKey("Bon_De_Commande")]
        public int ArticleBC_BonCommandeID { get; set; }
        public BonDeCommande bonDeCommande { get; set; }
        public MatierePremiereStockage MatierePremiere_Stokage { get; set; }
    }
}
