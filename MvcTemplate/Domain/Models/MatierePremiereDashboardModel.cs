namespace Domain.Models
{
    public class MatierePremiereDashboardModel
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public decimal Stock { get; set; }
        public string Unite { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
    }
}