using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupperCRMApplication.Entities.Abstract;

namespace SupperCRMApplication.Entities
{
    [Table("Issues")]
    public class Issue : EntityBase
    {
        [Required, StringLength(400)]
        public string? Summary { get; set; }
        public DateTime? DueDate { get; set; }
        public bool Completed { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual User User { get; set; }
    }
}
