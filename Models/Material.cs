using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Materialverwaltung.Models
{
    public class Material
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255, ErrorMessage = "Max. ist 255 Zeichen.")]
        public string Name { get; set; }
        [DisplayName("Lagerbestand")]
        public int Stock { get; set; }
        [Range(0, 999999.99), DisplayName("Kaufpreis")]
        public decimal BuyPrice { get; set; }
        [Range(0, 999999.99), DisplayName("Verkaufspreis")]
        public decimal SellPrice { get; set; }

        [Display(Name = "Materialgruppe")]
        [Required(ErrorMessage = "Materialgruppe ist erforderlich!")]
        [ForeignKey("MaterialgruppeId")]
        public virtual Materialgruppe Materialgruppe { get; set; }
        public int MaterialgruppeId { get; set; }
    }    
}
