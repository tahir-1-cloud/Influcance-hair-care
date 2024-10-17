using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.CustomerFavoriteProducts.Dtos
{
    public class CustomerFavoriteProductBaseDto
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public DateTime DateTime { get; set; }
        public bool Status { get; set; } = false;

    }
}
