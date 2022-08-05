using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class AgentServeurModel
    {
        public int Agent_Id { get; set; }
        public string Agent_NomPrenom { get; set; }
        public int Agent_PointVenteId { get; set; }
        public int Agent_IsActive { get; set; }
        public int Agent_AbonnementId { get; set; }
        public string Agent_UtilisateurId { get; set; }
        public DateTime Agent_DateCreation { get; set; }
        public ICollection<Affectation_Agent_ServeurModel> Tables_Link { get; set; }
    }
}
