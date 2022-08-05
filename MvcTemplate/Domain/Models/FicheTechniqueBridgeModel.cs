using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class FicheTechniqueBridgeModel
    {
        public FicheTechniqueBridgeModel()
        {
            Produit_FicheTechnique = new List<ProduitFicheTechniqueModel>();
            Fiche_Forme = new List<FicheFromeModel>();
        }
        public int FicheTechniqueBridge_ID { get; set; }
        public int FicheTechniqueBridge_ProduitVendableID { get; set; }
        public string FicheTechniqueBridge_Libelle { get; set; }
        public decimal FicheTechniqueBridge_CoutDeRevient { get; set; }
        public bool FicheTechniqueBridge_InUse { get; set; }
        public int FicheTechniqueBridge_AbonnementID { get; set; }
        public int FicheTechniqueBridge_IsActive { get; set; }
        public DateTime FicheTechniqueBridge_DateCreation { get; set; }
        public ProduitVendableModel Produit_Vendable { get; set; }

        public List<ProduitFicheTechniqueModel> Produit_FicheTechnique { get; set; }
        public List<FicheFromeModel> Fiche_Forme { get; set; }
        public List<FicheTech_ProduitBaseModel> FicheTech_ProduitBase { get; set; }

    }
}
