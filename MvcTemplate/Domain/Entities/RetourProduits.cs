using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Retour_Produits")]
    public class RetourProduits
    {
        public RetourProduits()
        {
            retourDetails = new Collection<Retour_Details>();
        }
        [Key]
        public int Retour_Id { get; set; }
        [ForeignKey("Position_Vente")]
        public int Retour_PositionVenteID { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal Retour_PrixTotal { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Retour_DateCreation { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Retour_UtilisateurID { get; set; }
        [Column(TypeName = "int")]
        public int Retour_AbonnementID { get; set; }
        [Column(TypeName = "int")]
        public int Retour_FlagCloture { get; set; }
        public PositionVente Position_Vente { get; set; }
        public ICollection<Retour_Details> retourDetails { get; set; }
    }
}
