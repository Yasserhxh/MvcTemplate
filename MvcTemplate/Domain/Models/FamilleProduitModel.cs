using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class FamilleProduitModel
    {
        public FamilleProduitModel()
        {
            sousFamille = new List<SousFamilleModel>();
            PointVente_Link = new List<PointVente_FamilleModel>();
        }
        public int FamilleProduit_Id { get; set; }
        public string FamilleProduit_Libelle { get; set; }
        public string FamilleProduit_Image { get; set; }
        public int FamilleProduit_AbonnemnetId { get; set; }
        public DateTime FamilleProduit_DateCreation { get; set; }
        public int FamilleProduit_IsActive { get; set; }
        public List<SousFamilleModel> sousFamille { get; set; }
        public List<PointVente_FamilleModel> PointVente_Link { get; set; }

    }
}
