using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Article_BL")]
    public class Article_BL
    {
        [Key]
        public int Acrticle_BL { get; set; }
        public string ArticleBL_Designation { get; set; }
        public int ArticleBL_MatiereID { get; set; }
        public decimal ArticleBL_Quantie { get; set; }
        public decimal ArticleBL_PU { get; set; }
        public decimal ArticleBL_PrixTotal { get; set; }
        public int ArticleBL_BonLivraisonID { get; set; }
        public BonDeLivraison bonDeLivraison { get; set; }
        public MatierePremiereStockage MatierePremiereStockage { get; set;}
    }
}
