using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class UniteMesure_ProdBaseModel
    {
        public int ID { get; set; }
        public int UniteMesure_ID { get; set; }
        public int ProduitBase_ID { get; set; }
        public int isActive { get; set; }
        public int Abonnement_ID { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public ProduitBaseModel ProduitBase { get; set; }
    }
}
