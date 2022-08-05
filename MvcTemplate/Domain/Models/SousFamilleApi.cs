using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class SousFamilleApi
    {
        public SousFamilleApi()
        {
            produits = new List<VenteApiModel>();
        }
        public int SousFamille_ID { get; set; }
        public string SousFamille_Libelle { get; set; }
        public int SousFamille_ParentID { get; set; }
        public string SousFamille_Image { get; set; }
        public int SousFamille_AbonnementID { get; set; }
        public List<VenteApiModel> produits { get; set; }

    }
}
