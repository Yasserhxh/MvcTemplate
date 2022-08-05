using Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Echange_Produits")]
    public class Echange_Produits
    {
        public Echange_Produits()
        {
            details = new Collection<EchangeProduit_Details>();
        }
        [Key]
        public int EchangeProduits_ID { get; set; }
        [ForeignKey("User")]
        public string  EchangeProduits_AdminID { get; set; }
        public int EchangeProduits_PdvFournisseurID { get; set; }
        public int EchangeProduits_PdvRecepID { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string EchangeProduits_Statut { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string EchangeProduits_PdvFournisseurUserID { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string EchangeProduits_PdvRecepUserID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime EchangeProduits_DateEchange { get; set; }
        [Column(TypeName = "int")]
        public int EchangeProduits_AbonnementID { get; set; }
        public ApplicationUser User { get; set; }
        [ForeignKey("EchangeProduits_PdvFournisseurID")]
        public virtual Point_Vente FournisseurPdv { get; set; }
        [ForeignKey("EchangeProduits_PdvRecepID")]
        public virtual Point_Vente ReceptionPdv { get; set; }
        public ICollection<EchangeProduit_Details> details { get; set; }

    }
}
