using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Models
{
    public class AllergeneModel
    {
        public int Allergene_Id { get; set; }
        public string Allergene_Libelle { get; set; }
        public string Allergene_LibelleAR { get; set; }
        public int Allergene_IsActive { get; set; }
        public int Allergene_AbonnementId { get; set; }
        public ICollection<MatPremAllergene_Model> listMateires { get; set; }


    }
}
