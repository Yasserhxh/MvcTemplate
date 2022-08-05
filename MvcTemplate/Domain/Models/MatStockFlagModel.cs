using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class MatStockFlagModel
    {
        public MatStockFlagModel()
        {
            Matieres = new List<matStockViewModel>();
        }
        public bool flag { get; set; }
        public List<matStockViewModel> Matieres { get; set; }
    }
}
