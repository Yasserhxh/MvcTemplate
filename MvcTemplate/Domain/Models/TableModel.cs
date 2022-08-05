using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class TableModel
    {
        public int Table_Id { get; set; }
        public int Table_SalleId { get; set; }
        public int Table_Numero { get; set; }
        public int Table_IsActive { get; set; }
        public int Table_AbonnementId { get; set; }
        public string Table_UtilisateurId { get; set; }
        public DateTime Table_DateCreation { get; set; }
        public SalleModel Salle { get; set; }
        public ICollection<Affectation_Agent_ServeurModel> Agent_Link { get; set; }
    }
}
