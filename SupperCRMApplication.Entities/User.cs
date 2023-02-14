﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SupperCRMApplication.Entities.Abstract;

namespace SupperCRMApplication.Entities
{

    [Table("Users")]
    public class User : EntityBase
    {
        [Required, StringLength(60)]
        public string? Name { get; set; } //John Doe or Codeove
        [Required, StringLength(60)]
        public string? Email { get; set; }
        [Required, StringLength(25)]
        public string? Username { get; set; }
        [Required, StringLength(100)]
        public string? Password { get; set; }
        [Required, StringLength(20)]
        public string? Role { get; set; }
        public bool Locked { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Issue Issues { get; set; }
    }
}