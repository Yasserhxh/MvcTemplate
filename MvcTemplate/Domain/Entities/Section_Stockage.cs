using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Section_Stockage")]
    public class Section_Stockage
    {
        [Key]
        public int Section_Id { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Section_Designation { get; set; }
        [ForeignKey("Zone_Stockage")]
        public int Section_ZoneStockageId { get; set; }
        public int Section_IsActive { get; set; }
        public Zone_Stockage Zone_Stockage { get; set; }
    }
}
