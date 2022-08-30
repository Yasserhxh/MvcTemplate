using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class MatierePremiereModel
    {
      
        public int MatierePremiere_Id { get; set; }
        public string MatierePremiere_Libelle { get; set; }
        public string MatierePremiere_CodeArticle { get; set; }
        public string MatierePremiere_Composants { get; set; }
        public int MatierePremiere_AchatUniteMesureId { get; set; }
        public decimal MatierePremiere_Quantite_FT { get; set; }
        public int MatierePremiere_UniteMesureId_FT { get; set; }
        public int? MatierePremiere_CoutTVAID { get; set; }
        public decimal MatierePremiere_PourcentrageTolerancePerte { get; set; }
        public decimal MatierePremiere_PrixMoyenAchat { get; set; }
        public int? MatierePremiere_FormeStockageId { get; set; }
        public int MatierePremiere_EstAllergene { get; set; }
        //public int? MatierePremiere_AllergeneId { get; set; }
        public int MatierePremiere_IsActive { get; set; }
        public int MatierePremiere_AbonnementId { get; set; }
        public DateTime MatierePremiere_DateCreation { get; set; }
        public bool EnStock { get; set; }
        public decimal MatierePremiere_QuantiteActuelle { get; set; }
        public int MatierePremiere_FamilleID { get; set; }

        public Forme_StockageModel Forme_Stockage { get; set; }
        //public AllergeneModel Allergene { get; set; }
        //  public ProduitFicheTechniqueModel ProduitFicheTechnique { get;set; }
        public MatiereFamilleModel Matiere_Famille { get; set; }
        public ICollection<FournisseurMatiereModel> FournisseurLink { get; set; }
        public ICollection<Unite_MesureMatiereModel> unites_Utilisation { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public Taux_TVAModel Taux_TVA { get; set; }
        public ICollection<MatPremAllergene_Model> listAllergene { get; set; }


    }
}
