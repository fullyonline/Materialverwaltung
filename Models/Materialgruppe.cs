using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Materialverwaltung.Models
{    public class Materialgruppe
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255, ErrorMessage = "Max. ist 255 Zeichen."), DisplayName("Materialgruppe")]
        public string Name { get; set; }
    }
}
