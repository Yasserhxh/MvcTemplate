using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Affectation_Agent_ServeurModel
    {
        public int Affectation_Id { get; set; }
        public int Affectation_AgentId { get; set; }
        public int Affectation_TableId { get; set; }
        public int Affectation_IsActive { get; set; }
        public TableModel Table { get; set; }
        public AgentServeurModel Agent_Serveur { get; set; }
    }
}
