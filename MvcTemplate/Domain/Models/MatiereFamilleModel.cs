using System;

namespace Domain.Models
{
    public class MatiereFamilleModel
    {
        public int MatiereFamille_ID { get; set; }
        public string MatiereFamille_Libelle { get; set; }
        public int MatiereFamille_ParentID { get; set; }
        public int MatiereFamille_AbonnementID { get; set; }
        public DateTime MatiereFamille_DateCreation { get; set; }
        public int MatiereFamille_IsActive { get; set; }
        public MatiereFamille_ParentModel MatiereFamille_Parent { get; set; }
    }
}
