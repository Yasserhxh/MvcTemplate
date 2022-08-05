using System;

namespace Domain.Models
{
    public class FicheFromeModel
    {
        public int FicheForme_ID { get; set; }
        public int FicheForme_FormeProduit { get; set; }
        public int FicheForme_uniteMesure { get; set; }
        public decimal FicheForme_Quantite { get; set; }
        public decimal FicheForme_CoutDeRevient { get; set; }
        public int FicheForme_FicheBridge { get; set; }
        public int FicheForme_IsActive { get; set; }
        public DateTime FicheForme_DateCreation { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public FicheTechniqueBridgeModel FicheTechnique_Bridge { get; set; }
    }
}
