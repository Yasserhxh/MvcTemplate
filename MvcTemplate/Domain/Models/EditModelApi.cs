using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class EditModelApi
    {
        public string userId { get; set; }
        public string oldMdp { get; set; }
        public string newMdp { get; set; }
    }
}
