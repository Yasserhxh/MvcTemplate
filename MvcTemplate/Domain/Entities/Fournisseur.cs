using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Founisseur")]

    public class Fournisseur
    {
        public Fournisseur()
        {
            Fournisseur_Contact = new Collection<FournisseurContact>();
        }
        [Key]
        public int Founisseur_Id { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Founisseur_RaisonSocial { get; set; }
        [Column(TypeName = "nvarchar(50)")]

        public string Founisseur_ICE { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Founisseur_Adresse { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Founisseur_NomContact { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Founisseur_Telephone { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Founisseur_DateSaisie { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        public string Founisseur_UtilisateurId { get; set; }
        [Column(TypeName = "int")]
        public int Founisseur_AbonnementId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? Founisseur_DateCreation { get; set; }
        [ForeignKey("Ville")]
        public int? Founisseur_VilleID { get; set; }

        public int? Founisseur_IsActive { get; set; }
        public ICollection<FournisseurContact> Fournisseur_Contact { get; set; }
        public ICollection<FournisseurMatiere> MatiereLink { get; set; }
        public Ville Ville { get; set; }



    }





}
