using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class CloturerApiModel
    {
        public string UserId { get; set; }
        public int CaisseId { get; set; }
        public decimal Recette { get; set; }
        public decimal Depenses { get; set; }
        public decimal Allimentation { get; set; }
        public decimal MontantReel { get; set; }
        public string Date { get; set; }
    }
}
