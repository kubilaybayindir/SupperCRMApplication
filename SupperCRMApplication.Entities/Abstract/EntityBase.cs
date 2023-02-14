using System.ComponentModel.DataAnnotations;

namespace SupperCRMApplication.Entities.Abstract
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }

    }
}