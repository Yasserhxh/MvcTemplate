using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class FicheTehcniqueProduitBaseModel
    {
        public FicheTehcniqueProduitBaseModel()
        {
            ProduitBase_FicheTechnique = new List<ProduitBaseFicheTechniqueModel>();
        }
        public int FicheTechniqueProduitBase_ID { get; set; }
        public int FicheTechniqueProduitBase_ProduitBaseID { get; set; }
        public string FicheTechniqueProduitBase_Libelle { get; set; }
        public decimal FicheTechniqueProduitBase_CoutDeRevient { get; set; }
        public bool FicheTechniqueProduitBase_InUse { get; set; }
        public int FicheTechniqueProduitBase_AbonnementID { get; set; }
        public int FicheTechniqueProduitBase_IsActive { get; set; }
        public DateTime FicheTechniqueProduitBase_DateCreation { get; set; }
        public int FicheTechniqueProduitBase_UniteMesureID { get; set; }
        public decimal FicheTechniqueProduitBase_QuantiteProd { get; set; }
        public ProduitBaseModel ProduitBase { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public List<ProduitBaseFicheTechniqueModel> ProduitBase_FicheTechnique { get; set; }

    }
}
