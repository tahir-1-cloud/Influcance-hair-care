using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.System.Dtos
{
    public class LoginResponseDto : LoginBaseDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Salon_Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string? Phone { get; set; } = string.Empty;

        public string ImagePath = string.Empty;
        public string ImageName = string.Empty;

        public float? Rating { get; set; }

        public float? Discount { get; set; }

        public string Token { get; set; } = string.Empty;
    }
}
