using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("DemandeApprov_Details")]
    public class DemandeApprov_Details
    {
        [Key]
        public int DemandeApprovDetails_ID { get; set; }
        [ForeignKey("Point_Vente")]
        public int DemandeApprovDetails_PointVenteID { get; set; }
        [ForeignKey("ProduitVendable")]
        public int DemandeApprovDetails_ProduitID { get; set; }
        [ForeignKey("DemandeApprov")]
        public int DemandeApprovDetails_DemandeID { get; set; }    
        public int DemandeApprovDetails_Quantite { get; set; }    
        public int DemandeApprovDetails_Etat { get; set; }   
        public Point_Vente Point_Vente { get; set; }    
        public ProduitVendable ProduitVendable { get; set; }
        public DemandeApprov DemandeApprov { get; set; }
    }
}
