using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class InventaireFilterModel
    {
        public string userId { get; set; }
        public int? pdv { get; set; }
        public string date { get; set; }
    }
}
