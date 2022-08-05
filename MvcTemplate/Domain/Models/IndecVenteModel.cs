using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class IndecVenteModel
    {
        public Tuple<IEnumerable<PointVente_StockModel>, IEnumerable<ProduitPackageModel>> tuple { get; set; }
        public IEnumerable<ProduitPretConsomerModel> ProduitPret { get; set; }
    }
}
