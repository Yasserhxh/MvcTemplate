using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class FilterCommandeApi
    {
        public string userId { get; set; }
        public string date { get; set; }
        public string nomDemandeur { get; set; }
    }
}
