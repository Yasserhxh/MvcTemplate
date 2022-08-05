using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ProduitBaseFicheTechniqueModel
    {
        public int ProduitBaseFicheTechnique_ID { get; set; }
        public int ProduitBaseFicheTechnique_MatierePremiereID { get; set; }
        public int ProduitBaseFicheTechnique_UniteMesureID { get; set; }
        public decimal ProduitBaseFicheTechnique_QuantiteMatiere { get; set; }
        public decimal ProduitBaseFicheTechnique_Prix { get; set; }
        public int ProduitBaseFicheTechnique_AbonnementId { get; set; }
        public DateTime ProduitBaseFicheTechnique_DateCreation { get; set; }
        public int ProduitBaseFicheTechnique_FicheTehcniqueProduitBaseID { get; set; }

        public FicheTehcniqueProduitBaseModel FicheTechniqueProduitBase { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public MatierePremiereModel Matiere_Premiere { get; set; }
    }
}
