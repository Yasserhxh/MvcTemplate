using System;
using System.Text;

namespace Domain.Models
{
    public class PackageFormeDetails_MatiereModel
    {
        public int PackageFormeDetailsMatiere_ID { get; set; }
        public int PackageFormeDetailsMatiere_PackageFormeID { get; set; }
        public int PackageFormeDetailsMatiere_SousMatiereID { get; set; }
        public int PackageFormeDetailsMatiere_UniteMesureID { get; set; }
        public decimal PackageFormeDetailsMatiere_Quantite { get; set; }
        public decimal PackageFormeDetailsMatiere_CoutDeRevient { get; set; }
        public Package_FormeModel Package_Forme { get; set; }
        public SousMatierePackageModel SousMatierePackage { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
    }
}
