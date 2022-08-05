using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PointVenteApi
    {
        public int PointVente_Id { get; set; }
        public string PointVente_Nom { get; set; }
        public List<PositionVenteApi> PositionsVente { get; set; }
    }
}
