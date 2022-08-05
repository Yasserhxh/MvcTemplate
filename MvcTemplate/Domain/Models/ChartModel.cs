using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ChartModel
    {
        public int ID { get; set; }
        public string Designation { get; set; }
        public string Quantite { get; set; }
        public Unite_MesureModel Unite { get; set; }
    }
}
