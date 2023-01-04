using System.ComponentModel.DataAnnotations;

namespace Materialverwaltung.Models
{    public class Materialgruppe
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255, ErrorMessage = "Max. ist 255 Zeichen.")]
        public string Name { get; set; }

        public virtual ICollection<Material> Materials { get; set; }
    }
}
