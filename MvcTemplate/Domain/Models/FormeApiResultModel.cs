using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class FormeApiResultModel
    {
        public int Forme_ID { get; set; }
        public int? Forme_ProduitVendableId { get; set; }
        public decimal Forme_PrixVente { get; set; }
        public int? Forme_ProduitPackageId { get; set; }
        public int? Forme_ProduitPretConsomerId { get; set; }
        public string Forme_Libelle { get; set; }
        public decimal Forme_CoutDeRevient { get; set; }
        public decimal Forme_QteStock { get; set; }
        public string type { get; set; }
        public string concat { get; set; }
        public IEnumerable<PrixProduitViewModel> ListPrix { get; set; }
        public  ProduitPretConsomerModel prets { get; set; }
        public ProduitPackageModel package { get; set; }
        public ProduitVendableModel maison { get; set; }
    }
}
