using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.Customers.Dtos
{
    public class CustomerSaleRepDto:CustomersBaseDto
    {
        public int SaleRepId { get; set; }
        public string SaleRepName { get; set; } = string.Empty;

       // public int CustomerOrderCount { get; set; } = 0;
    }
}
