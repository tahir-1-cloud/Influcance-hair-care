using InfluanceHairCare.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.Customers.Dtos
{
    public class CustomerRegisterDto : CustomersBaseDto
    {
        public string Email { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public int SaleRepId { get; set; }

    }
}
