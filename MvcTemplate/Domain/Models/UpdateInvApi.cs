using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class UpdateInvApi
    {
        public List<ProdsQteApi> prodsQte { get; set; }
        public int pdvID { get; set; }
    }
}
