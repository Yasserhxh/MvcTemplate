using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class SalleModel
    {
        public SalleModel()
        {
            Tables = new List<TableModel>();
        }
        public int Salle_Id { get; set; }
        public string Salle_Libelle { get; set; }
        public int Salle_PositionVenteId { get; set; }
        public string Salle_UtilisateurId { get; set; }
        public int Salle_AbonnementId { get; set; }
        public DateTime Salle_DateCreation { get; set; }
        public PositionVenteModel Position_Vente { get; set; }
        public List<TableModel> Tables { get; set; }
    }
}
