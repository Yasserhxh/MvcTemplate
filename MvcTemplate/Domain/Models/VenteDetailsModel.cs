using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class VenteDetailsModel
    {
        public int VenteDetails_Id { get; set; }
        public int VenteDetails_VentId { get; set; }
        public int VenteDetails_FormeProduitId { get; set; }
        public decimal VenteDetails_Quantite { get; set; }
        public int VenteDetails_UniteId { get; set; }
        public decimal VenteDetails_CoutDeRevient { get; set; }
        public decimal VenteDetails_Prix { get; set; }
        public decimal VenteDetails_Marge { get; set; }
        public DateTime VenteDetails_DateCreation { get; set; }
        public int VenteDetails_AbonnementID { get; set; }
        public int VenteDetails_FlagCloture { get; set; }
        public VenteModel Vente { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
    }
}
