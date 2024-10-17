using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.Customers.Dtos
{
    public class CustomerBalanceDto
    {
        public int CustomerId { get; set; }
        public float AccountBalance { get; set; }
        public float CreditLimit { get; set; }
    }
}
