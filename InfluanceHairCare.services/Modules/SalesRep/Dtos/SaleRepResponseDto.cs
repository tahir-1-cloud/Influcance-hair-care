using InfluanceHairCare.services.Modules.Customers.Dtos;
using InfluanceHairCare.services.Modules.Order.Dtos;
using InfluanceHairCare.services.Modules.System.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.SalesRep.Dtos
{
    public class SaleRepResponseDto : SaleRepBaseDto
    {
        public List<OrderResponseDto>? OrderResponses { get; set; } 
        public string Email { get; set; } = string.Empty;
        public int? OrdersCount { get; set; }
        public int? CustomerCount { get; set; }

    }
}
