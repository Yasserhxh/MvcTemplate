using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Table")]
    public class Table
    {
        [Key]
        public int Table_Id { get; set; }
        [ForeignKey("Salle")]
        public int Table_SalleId { get; set; }
        [Column(TypeName ="int")]
        public int Table_Numero { get; set; }
        [Column(TypeName = "int")]
        public int Table_IsActive { get; set; }
        [Column(TypeName = "int")]
        public int Table_AbonnementId { get; set; }
        [Column(TypeName = "nvarchar(350)")]
        public string Table_UtilisateurId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Table_DateCreation { get; set; }
        public Salle Salle { get; set; }
        public ICollection<Affectation_Agent_Table> Agents_Link { get; set; }
    }
}
