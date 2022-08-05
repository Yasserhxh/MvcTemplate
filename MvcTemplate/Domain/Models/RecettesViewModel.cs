using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class RecettesViewModel
    {
        public string TypeRecette { get; set; }
        public decimal MontantTotal { get; set; }
        public DateTime DateAction { get; set; }
    }
}
