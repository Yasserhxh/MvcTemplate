using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ApiUserModel
    {
        public string id { get; set; }
        public string userName { get; set; }
        public string passeWord { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public string role { get; set; }
    }
}
