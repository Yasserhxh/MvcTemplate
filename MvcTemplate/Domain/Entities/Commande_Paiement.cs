using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Commande_Paiement")]
    public class Commande_Paiement
    {
        [Key]
        public int CommandePaiement_ID { get; set; }
        [ForeignKey("Commande")]
        public int CommandePaiement_CommandeID { get; set; }
        [ForeignKey("Mode_Paiement")]
        public int CommandePaiement_ModePaiementID { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal CommandePaiement_Montant { get; set; }
        [ForeignKey("Position_Vente")]
        public int CommandePaiement_PositionVenteID { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string CommandePaiement_UtilisateurID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime CommandePaiement_DatePaiement { get; set; }
        [Column(TypeName = "int")]
        public int CommandePaiement_FlagCloture { get; set; }
        public PositionVente Position_Vente { get; set; }
        public Commande Commande { get; set; }
        public ModePaiement Mode_Paiement { get; set; }
    }
}
