using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ProdBase_AtelierModel
    {
        public int ProdBase_Atelier_Id { get; set; }
        public int ProdBase_Atelier_ProduitID { get; set; }
        public decimal ProdBase_Atelier_QuantiteProduite { get; set; }
        public int ProdBase_Atelier_AbonnementID { get; set; }
        public DateTime ProdBase_Atelier_DateCreation { get; set; }
        public DateTime ProdBase_Atelier_DateModification { get; set; }
        public int ProdBase_Atelier_AtelierID { get; set; }
        public AtelierModel Atelier { get; set; }
        public ProduitBaseModel ProduitBase { get; set; }
    }
}
