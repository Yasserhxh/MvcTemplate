using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class SyncroApiModel
    {
        public SyncroApiModel()
        {
            sousFamille = new List<SousFamilleApi>();
        }
        public int FamilleProduit_Id { get; set; }
        public string FamilleProduit_Libelle { get; set; }
        public string FamilleProduit_Image { get; set; }
        public int FamilleProduit_AbonnemnetId { get; set; }
        public List<SousFamilleApi> sousFamille { get; set; }

    }
}
