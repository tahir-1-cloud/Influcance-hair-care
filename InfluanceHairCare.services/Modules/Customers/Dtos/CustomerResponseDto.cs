using InfluanceHairCare.models;
using InfluanceHairCare.services.Modules.Order.Dtos;
using InfluanceHairCare.services.Modules.SalesRep.Dtos;
using InfluanceHairCare.services.Modules.System.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.Customers.Dtos
{
    public  class CustomerResponseDto : CustomersBaseDto
    {
        public LoginBaseDto Login { get; set; } = null;

        public SaleRepBaseDto? SaleRep { get; set; }

        public List<OrderBaseDto>? CustomerOrder { get; set; }

    }
}
