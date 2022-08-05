using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Salle")]
    public class Salle
    {
        public Salle()
        {
            Tables = new Collection<Table>();
        }
        [Key]
        public int Salle_Id { get; set; }
        [Column(TypeName ="nvarchar(50)")]
        public string Salle_Libelle { get; set; }
        [ForeignKey("Position_Vente")]
        public int Salle_PositionVenteId { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string Salle_UtilisateurId { get; set; }
        public int Salle_AbonnementId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Salle_DateCreation { get; set; }
        public PositionVente Position_Vente { get; set; }
        public ICollection<Table> Tables { get; set; }
    }
}
