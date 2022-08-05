using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("FicheTechniqueProduitBase")]
    public class FicheTechniqueProduitBase
    {
        public FicheTechniqueProduitBase()
        {
            ProduitBase_FicheTechnique = new Collection<ProduitBaseFicheTechnique>();
        }
        [Key]
        public int FicheTechniqueProduitBase_ID { get; set; }
        [ForeignKey("ProduitBase")]
        public int FicheTechniqueProduitBase_ProduitBaseID { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int FicheTechniqueProduitBase_UniteMesureID { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string FicheTechniqueProduitBase_Libelle { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal FicheTechniqueProduitBase_CoutDeRevient { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal FicheTechniqueProduitBase_QuantiteProd { get; set; }
        [Column(TypeName = "bit")]
        public bool FicheTechniqueProduitBase_InUse { get; set; }
        [Column(TypeName = "int")]
        public int FicheTechniqueProduitBase_AbonnementID { get; set; }
        [Column(TypeName = "int")]
        public int FicheTechniqueProduitBase_IsActive { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FicheTechniqueProduitBase_DateCreation { get; set; }

        public ProduitBase ProduitBase { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public ICollection<ProduitBaseFicheTechnique> ProduitBase_FicheTechnique { get; set; }
    }
}
