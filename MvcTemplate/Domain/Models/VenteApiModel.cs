using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class VenteApiModel
    {
        public int ID { get; set; }
        public string Designation { get; set; }
        public string Image { get; set; }
        public decimal tva { get; set; }
        public string Type { get; set; }
        public string filter { get; set; }
        public int planifFlag { get; set; } 
        public int? sousCategId { get;set; }
        public int? uniteId { get;set; }
        public string concat { get; set; }
        public Unite_MesureModel Unite { get; set; }
        public List<FormeApiResultModel> formes { get; set; }
    }
}
