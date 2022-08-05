using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class FicheTech_ProduitBaseModel
    {
        public int Id { get; set; }
        public int FicheTech_ID { get; set; }
        public int ProduitBase_ID { get; set; }
        public int Abonnement_ID { get; set; }
        public int IsActive { get; set; }
        public decimal ProduitQte { get; set; }
        public int UniteMesure_ID { get; set; }
        public ProduitBaseModel ProduitBase { get; set; }
        public FicheTechniqueBridgeModel FicheTechnique_Bridge { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }

    }
}
