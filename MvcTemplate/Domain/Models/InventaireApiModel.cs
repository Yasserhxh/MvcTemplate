using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class InventaireApiModel
    {
        public string Inventaire_FormeLibelle { get; set; }
        public int Inventaire_FormeID { get; set; }
        public string Inventaire_ProduitLibelle { get; set; }
        public decimal? Inventaire_QuantiteEnStock { get; set; }
        public decimal? Inventaire_QuantiteMinimale { get; set; }
        public string Inventaire_Unite { get; set; }

    }
}
