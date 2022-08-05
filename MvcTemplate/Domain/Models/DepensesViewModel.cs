using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class DepensesViewModel
    {
        public string TypeDepense { get; set; }
        public decimal MontantTotal { get; set; }
        public DateTime DateAction { get; set; }
    }
}
