using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Materialverwaltung.Models
{
    public class Material
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [DisplayName("Lagerbestand")]
        public int Stock { get; set; }
        [Range(0, 999999.99), DisplayName("Kaufspreis")]
        public decimal BuyPrice { get; set; }
        [Range(0, 999999.99), DisplayName("Verkaufspreis")]
        public decimal SellPrice { get; set; }

        public virtual Materialgruppe Materialgruppe { get; set; }
    }    
}
