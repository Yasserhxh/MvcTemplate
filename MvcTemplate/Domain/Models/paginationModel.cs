using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class paginationModel<T>
    {
     
        public int count { get; set; }
        public List<T> objList { get; set; }

    }
}
