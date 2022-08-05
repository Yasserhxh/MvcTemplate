using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Agent_Serveur")]
    public class AgentServeur
    {
        [Key]
        public int Agent_Id { get; set; }
        [Column(TypeName ="nvarchar(100)")]
        public string Agent_NomPrenom { get; set; }
        [Column(TypeName = "int")]
        public int Agent_PointVenteId { get; set; }
        [Column(TypeName = "int")]
        public int Agent_IsActive { get; set; }
        [Column(TypeName = "int")]
        public int Agent_AbonnementId { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string Agent_UtilisateurId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Agent_DateCreation { get; set; }
        public ICollection<Affectation_Agent_Table> Tables_Link { get; set; }
    }
}
