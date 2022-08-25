using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ReceptionAchatModel
    {
        public int matiereID { get; set; }
        public decimal Quantite { get; set; }
        public int ZoneID { get; set; }
        public int SectionID { get; set; }
        public int TransferID { get; set; }
        public string userID { get; set; }
        public int StockID { get; set; }
        public int MatTransID { get; set; }
    }
}
