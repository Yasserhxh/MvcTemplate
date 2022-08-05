using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class MatStockView
    {
        public string matiereLibelle { get; set; }
        public decimal qteLivrer{ get; set; }
        public string uniteLivrer { get; set; }
        public decimal qteEnStock { get; set; }
        public string uniteStock { get; set; }

    }
}
