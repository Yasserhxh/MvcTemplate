using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Affectation_Agent_Table")]
    public class Affectation_Agent_Table
    {
        [Key]
        public int Affectation_Id { get; set; }
        [ForeignKey("Agent_Serveur")]
        public int Affectation_AgentId { get; set; }
        [ForeignKey("Table")]
        public int Affectation_TableId { get; set; }
        [Column(TypeName ="int")]
        public int Affectation_IsActive { get; set; }
        public Table Table { get; set; }
        public AgentServeur Agent_Serveur { get; set; }

    }
}
