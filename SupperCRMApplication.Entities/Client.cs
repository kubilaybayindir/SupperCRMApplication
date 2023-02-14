using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SupperCRMApplication.Entities.Abstract;

namespace SupperCRMApplication.Entities
{

    [Table("Clients")]
    public class Client: EntityBase
    {
        [Required,StringLength(60)]
        public string? Name { get; set; } //John Doe or Codeove
        [Required, StringLength(60)]
        public string? Email { get; set; }
        [StringLength(25)]
        public string? Phone { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        public bool IsCorporate { get; set; }
        public bool Locked { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}