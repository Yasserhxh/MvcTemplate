using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class DemandeApprov_DetailsModel
    {
        public int DemandeApprovDetails_ID { get; set; }
        public int DemandeApprovDetails_PointVenteID { get; set; }
        public int DemandeApprovDetails_ProduitID { get; set; }
        public int DemandeApprovDetails_Categorie { get; set; }
        public string DemandeApprovDetails_ProduitName { get; set; }
        public string DemandeApprovDetails_Type { get; set; }
        public int DemandeApprovDetails_DemandeID { get; set; }
        public int DemandeApprovDetails_Quantite { get; set; }
        public int DemandeApprovDetails_Etat { get; set; }
        public Point_VenteModel Point_Vente { get; set; }
        public SousFamilleModel Sous_Famille { get; set; }
        public DemandeApprov_Model DemandeApprov { get; set; }
    }
}
