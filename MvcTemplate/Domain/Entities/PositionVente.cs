using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Position_Vente")]
    public class PositionVente
    {
        public PositionVente()
        {
            Salles = new Collection<Salle>();
        }
        [Key]
        public int PositionVente_Id { get; set; }
        [ForeignKey("Point_Vente")]
        public int PositionVente_PointVenteId { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string PositionVente_Libelle { get; set; }
        [Column(TypeName ="int")]
        public int PositionVente_IsActive { get; set; }
        [ForeignKey("Mode_Paiement")]
        public int PositionVente_ModePaiementId { get; set; }
        [Column(TypeName = "int")]
        public int PositionVente_AbonnementId { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string PositionVente_UtilisateurId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime PositionVente_DateCreation { get; set; }
        public Point_Vente Point_Vente { get; set; }
        public ModePaiement Mode_Paiement { get; set; }
        public ICollection<Salle> Salles { get; set; }
    }
}
