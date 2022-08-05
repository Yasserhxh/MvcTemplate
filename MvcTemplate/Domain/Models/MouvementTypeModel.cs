using System;

namespace Domain.Models
{
    public class MouvementTypeModel
    {
        public int MouvementType_Id { get; set; }
        public string MouvementType_Libelle { get; set; }
        public int MouvementType_AbonnementId { get; set; }
        public DateTime MouvementType_DateCreation { get; set; }
    }
}
