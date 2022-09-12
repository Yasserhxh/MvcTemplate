using Domain.Entities;
using Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Helpers
{
    public class QRCodeProduitModel
    {
        public string DESIGNATION { get; set; }
        public string DATE_PROD { get; set; }
        public string DATE_EXP { get; set; }
        public string CONDITIONNEMENT { get; set; }
        public string FORME_STOCKAGE { get; set; }
    }
}
