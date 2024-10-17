using InfluanceHairCare.models;
using InfluanceHairCare.services.Modules.Product.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.Order.Dtos
{
    public class OrderProductDto : OrderBaseDto
    {
        public List<OrderProductBaseDto> OrderProducts { get; set; } = new List<OrderProductBaseDto>();
        public int CustomerId { get; set; } 
    }
}
