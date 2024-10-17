using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.models
{
    public class Login
    {
        [Key]
        public long LoginId { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string? Password { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public virtual Customer? Customer { get; set; }

        public virtual SaleRep? SaleRep { get; set; }
    }
}
