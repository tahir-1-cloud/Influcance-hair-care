using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.Order.Dtos
{
    public class OrderProductBaseDto
    {

        public int ProductId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; } = 0;

        public double TotalCost { get; set; } = 0;

        public float Discount { get; set; } = 0;
        public float TotalPrice { get; set; } = 0;

        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public string ProductImagePath { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;


    }
}
