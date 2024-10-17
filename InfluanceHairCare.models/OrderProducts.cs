using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.models
{
    public class OrderProducts
    {

        [Required]
        public long OrderId { get; set; } 

        public Orders Order { get; set; } 

        [Required]
        public int ProductId { get; set; }
        public Products Product { get; set; } 

        public float Price { get; set; }
        public int Quantity { get; set; } = 0;

        public double TotalCost { get; set; } = 0;

        public float Discount { get; set; } = 0;

        public float TotalPrice { get; set; } = 0;

    }
}
