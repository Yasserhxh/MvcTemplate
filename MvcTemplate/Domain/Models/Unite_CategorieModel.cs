using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Unite_CategorieModel
    {
        public int UniteCategorie_Id { get; set; }
        public string UniteCategorie_Libelle { get; set; }
    }
}
