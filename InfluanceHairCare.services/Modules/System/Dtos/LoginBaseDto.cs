using InfluanceHairCare.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.System.Dtos
{
    public class LoginBaseDto
    {
        public long LoginId { get; set; }
        public int RoleId { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

    }
}
