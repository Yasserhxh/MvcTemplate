using Domain.Entities;
using Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Helpers
{
    public class QRCodeModel
    {
        //[Display(Name = "Enter QRCode Text")]
        public string REFERENCE { get; set; }
        public string LOT_INTERN { get; set; }
        public string LOT_FOURNISSEUR { get; set; }
        public string DATE_RECEP { get; set; }
        public string DATE_P { get; set; }
        public string DLC { get; set; }
        //public List<Article_BL> listeArticles { get; set; }

    }
}
