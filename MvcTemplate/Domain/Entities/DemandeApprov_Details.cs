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
        public int DemandeApprovDetails_ProduitID { get; set; }
        public string DemandeApprovDetails_Type { get; set; }
        public string DemandeApprovDetails_ProduitName { get; set; }
        [ForeignKey("Sous_Famille")]
        public int DemandeApprovDetails_Categorie { get; set; }
        [ForeignKey("DemandeApprov")]
        public int DemandeApprovDetails_DemandeID { get; set; }    
        public int DemandeApprovDetails_Quantite { get; set; }    
        public int DemandeApprovDetails_Etat { get; set; }   
        public Point_Vente Point_Vente { get; set; }    
        public DemandeApprov DemandeApprov { get; set; }
        public SousFamille Sous_Famille { get; set; }
    }

}
