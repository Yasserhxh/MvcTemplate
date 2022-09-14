using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Unite_MesureModel
    {
        public int UniteMesure_Id { get; set; }
        public string UniteMesure_Libelle { get; set; }
        public int Unite_CategorieID { get; set; }
        public ICollection<Unite_MesureMatiereModel> matiereLink { get; set; }
        public Unite_CategorieModel Unite_Categorie { get; set; }

    }
}
