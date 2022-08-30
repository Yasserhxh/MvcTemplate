using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Matiere_Premiere")]
    public class MatierePremiere
    {
     
      
        [Key]
        public int MatierePremiere_Id { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string MatierePremiere_Libelle { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string MatierePremiere_LibelleAR { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string MatierePremiere_LibelleEN { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string MatierePremiere_CodeArticle { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string MatierePremiere_Composants { get; set; }

        [ForeignKey("Unite_Mesure")]
        public int MatierePremiere_AchatUniteMesureId { get; set; }
        [ForeignKey("Taux_TVA")]
        public int? MatierePremiere_CoutTVAID { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal MatierePremiere_Quantite_FT { get; set; }

        [Column(TypeName = "int")]
        public int MatierePremiere_UniteMesureId_FT { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal MatierePremiere_PourcentrageTolerancePerte { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal MatierePremiere_PrixMoyenAchat { get; set; }

        [ForeignKey("Forme_Stockage")]
        public int? MatierePremiere_FormeStockageId { get; set; }

        [Column(TypeName = "int")]
        public int MatierePremiere_EstAllergene { get; set; }

        //[ForeignKey("Allergene")]
        //public int? MatierePremiere_AllergeneId { get; set; }
        public int MatierePremiere_IsActive { get; set; }
        [Column(TypeName = "int")]
        public int MatierePremiere_AbonnementId { get; set; }
        [ForeignKey("Matiere_Famille")]
        public int MatierePremiere_FamilleID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime MatierePremiere_DateCreation { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MatierePremiere_QuantiteActuelle { get; set; }
        public ICollection<Unite_MesureMatiere> unites_Utilisation { get; set; }

        public bool EnStock { get; set; }
        public Forme_Stockage Forme_Stockage { get; set; }
        //public Allergene Allergene { get; set; }
        //public ProduitFicheTechnique ProduitFicheTechnique { get; set; }
        public ICollection<FournisseurMatiere> FournisseurLink { get; set; }
        public MatiereFamille Matiere_Famille { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public Taux_TVA Taux_TVA { get; set; }
        public ICollection<MatPrem_Allergene> listAllergene { get; set; }

    }
}
