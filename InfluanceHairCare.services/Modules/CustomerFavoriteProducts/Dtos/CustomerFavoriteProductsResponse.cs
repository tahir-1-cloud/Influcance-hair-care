using InfluanceHairCare.services.Modules.Order.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.CustomerFavoriteProducts.Dtos
{
    public class CustomerFavoriteProductsResponse: CustomerFavoriteProductBaseDto
    {
        public List<OrderProductBaseDto> FavoriteProducts { get; set; } = new List<OrderProductBaseDto>();

    }
}
