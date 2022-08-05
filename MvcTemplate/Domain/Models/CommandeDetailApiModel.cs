using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class CommandeDetailApiModel
    {
        public int CommandeDetail_Id { get; set; }
        public int CommandeDetail_CommandeId { get; set; }
        public string CommandeDetail_FormeProduit { get; set; }
        public string CommandeDetail_NomProduit { get; set; }
        public decimal CommandeDetail_Quantite { get; set; }
        public decimal CommandeDetail_PrixUnitaire { get; set; }
        public string CommandeDetail_UniteMesure { get; set; }
        public decimal CommandeDetail_Prix { get; set; }
        public decimal CommandeDetail_Tva { get; set; }
        public CommandeModel Commande { get; set; }
    }
}
