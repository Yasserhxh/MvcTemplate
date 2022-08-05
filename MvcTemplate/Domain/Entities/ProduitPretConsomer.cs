using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Produit_PretConsomer")]
    public class ProduitPretConsomer
    {
        public ProduitPretConsomer()
        {
            Fournisseur_Link = new Collection<Fournisseur_ProduitConso>();
            formes = new Collection<Forme_Produit>();
        }
        [Key]
        public int ProduitPretConsomer_ID { get; set; }
        public string ProduitPretConsomer_Designation { get; set; }
        public string ProduitPretConsomer_Description { get; set; }
        [ForeignKey("Forme_Stockage")]
        public int ProduitPretConsomer_FormeStockageId { get; set; }
        [ForeignKey("Sous_Famille")]
        public int ProduitPretConsomer_FamilleProduit { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int ProduitPretConsomer_UniteMesureAchatId { get; set; }
        public decimal ProduitPretConsomer_StockMinimun { get; set; }
        public decimal ProduitPretConsomer_StockMaximum { get; set; }
        public int ProduitPretConsomer_DelaiConsomation { get; set; }
        public string ProduitPretConsomer_Photo { get; set; }
        public int ProduitPretConsomer_IsActive { get; set; }
        public int ProduitPretConsomer_AbonnementID { get; set; }
        public bool ProduitPretConsomer_EnStock { get;set; }
        public DateTime ProduitPretConsomer_DateCreation { get; set; }
        public decimal ProduitPretConsomer_PrixMoyenAchat { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public Forme_Stockage Forme_Stockage { get; set; }
        public ICollection<Fournisseur_ProduitConso> Fournisseur_Link { get; set; }
        public ICollection<Forme_Produit> formes { get; set; }
        public SousFamille Sous_Famille { get; set; }
    }
}
