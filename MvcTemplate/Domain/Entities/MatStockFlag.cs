using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class MatStockFlag
    {
        public MatStockFlag()
        {
            Matieres = new List<MatStockView>();
        }
        public bool flag { get; set; }
        public List<MatStockView> Matieres { get; set; }
    }
}
