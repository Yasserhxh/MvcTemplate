using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class DashboardModel
    {
        public DashboardModel()
        {
            Labels = new List<string>();
            Data = new List<string>();
            MatieresPremieres = new List<MatierePremiereDashboardModel>();
            ListRecettes = new List<RecettesViewModel>();
            ListDepenses = new List<DepensesViewModel>();
            Unites = new List<string>();
        }
        public decimal Recette { get; set; }
        public decimal Ventes { get; set; }
        public decimal Commandes { get; set; }
        public decimal Retraits { get; set; }
        public decimal Retours { get; set; }
        public decimal TauxDeVente { get; set; }
        public decimal Depense { get; set; }
        public decimal Alimentation { get; set; }
        public List<string> Labels { get; set; }
        public List<string> Data { get; set; }
        public List<RecettesViewModel> ListRecettes { get; set; }
        public List<DepensesViewModel> ListDepenses { get; set; }
        public List<MatierePremiereDashboardModel> MatieresPremieres { get; set; }
        public DateTime? Date { get; set; }
        public int? PdvId { get; set; }
        public List<string> Unites { get; set; }
    }
}
