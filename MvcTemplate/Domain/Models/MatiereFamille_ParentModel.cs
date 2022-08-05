using System;

namespace Domain.Models
{
    public class MatiereFamille_ParentModel
    {
        public int MatiereFamilleParent_ID { get; set; }
        public string MatiereFamilleParent_Libelle { get; set; }
        public int MatiereFamilleParent_AbonnementID { get; set; }
        public DateTime MatiereFamilleParent_DateCreation { get; set; }
        public int MatiereFamilleParent_IsActive { get; set; }

    }
}
