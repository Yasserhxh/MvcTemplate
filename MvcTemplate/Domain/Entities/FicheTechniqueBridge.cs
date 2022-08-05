using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("FicheTechnique_Bridge")]
    public class FicheTechniqueBridge
    {
        public FicheTechniqueBridge()
        {
            Produit_FicheTechnique = new Collection<ProduitFicheTechnique>();
            Fiche_Forme = new Collection<FicheForme>();
        }
        [Key]
        public int FicheTechniqueBridge_ID { get; set; }
        [ForeignKey("Produit_Vendable")]
        public int FicheTechniqueBridge_ProduitVendableID { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string FicheTechniqueBridge_Libelle { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal FicheTechniqueBridge_CoutDeRevient { get; set; }
        [Column(TypeName = "bit")]
        public bool FicheTechniqueBridge_InUse { get; set; }
        [Column(TypeName = "int")]
        public int FicheTechniqueBridge_AbonnementID { get; set; }


        [Column(TypeName = "int")]
        public int FicheTechniqueBridge_IsActive { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FicheTechniqueBridge_DateCreation { get; set; }

        public ProduitVendable Produit_Vendable { get; set; }
        public ICollection<ProduitFicheTechnique> Produit_FicheTechnique { get; set; }
        public ICollection<FicheForme> Fiche_Forme { get; set; }
        public ICollection<FicheTech_ProduitBase> FicheTech_ProduitBase { get; set; }
    }
}
