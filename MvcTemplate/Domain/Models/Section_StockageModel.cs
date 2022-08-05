namespace Domain.Models
{
    public class Section_StockageModel
    {
        public int Section_Id { get; set; }
        public string Section_Designation { get; set; }
        public int Section_ZoneStockageId { get; set; }
        public int Section_IsActive { get; set; }
        public Zone_StockageModel Zone_Stockage { get; set; }


    }
}
