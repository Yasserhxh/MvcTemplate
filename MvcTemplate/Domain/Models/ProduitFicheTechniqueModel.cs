using System;

namespace Domain.Models
{
    public class ProduitFicheTechniqueModel
    {

        public int FicheTechnique_Id { get; set; }
        public string FicheTechnique_Libelle { get; set; }

        public int FicheTechnique_ProduitVendableId { get; set; }
        public int FicheTechnique_MatierePremiereId { get; set; }
        public int FicheTechnique_UniteMesureId { get; set; }
        public decimal FicheTechnique_QuantiteMatiere { get; set; }
        public decimal FicheTechnique_Prix { get; set; }
        public int FicheTechnique_IsActive { get; set; }
        public int FicheTechnique_AbonnementId { get; set; }
        public DateTime FicheTechnique_DateCreation { get; set; }
        public int FicheTechnique_FicheTechniqueBridgeID { get; set; }

        public Unite_MesureModel Unite_Mesure { get; set; }
        public MatierePremiereModel Matiere_Premiere { get; set; }
        public ProduitVendableModel Produit_Vendable { get; set; }
        public FicheTechniqueBridgeModel FicheTechnique_Bridge { get; set; }

    }
}
