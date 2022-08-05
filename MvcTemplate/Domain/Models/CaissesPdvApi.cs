using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class CaissesPdvApi
    {
        public int Caisse_Id { get; set; }
        public string Caisse_Nom { get; set; }
        public string Caisse_ClotureStatut { get; set; }
        public decimal Caisse_Recette { get; set; }
        public decimal Caisse_Depenses { get; set; }
        public decimal Caisse_Allimentation { get; set; }
        public decimal Caisse_MontatReel { get; set; }
    }
}
