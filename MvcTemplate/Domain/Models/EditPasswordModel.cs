using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class EditPasswordModel
    {
        public string OldPassword { get; set; }
        public RegisterModel registerModel { get; set; }
    }

    
}
