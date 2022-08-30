using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Produit_Composants")]
    public class Produit_Composants
    {
        [Key]
        public int ProduitComposant_ID { get; set; }
        [ForeignKey("ProduitVendable")]
        public int ProduitComposant_ProduitID { get; set; }
        public string ProduitComposant_ComposantName { get; set; }
        public string ProduitComposant_ComposantNameAR { get; set; }
        public string ProduitComposant_Valeur { get; set; }
        public int ProduitComposant_isActive { get; set; }
        public int ProduitComposant_AbonnementID { get; set; }
        public virtual ProduitVendable ProduitVendable { get; set; }

    }
}
