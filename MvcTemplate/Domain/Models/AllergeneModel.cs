namespace Domain.Models
{
    public class AllergeneModel
    {
        public int Allergene_Id { get; set; }
        public string Allergene_Libelle { get; set; }
        public int Allergene_IsActive { get; set; }
        public int Allergene_AbonnementId { get; set; }

    }
}
