using InfluanceHairCare.models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.Product.Dto
{
    public class ProductBaseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public float price { get; set; } = 0;

        public int Quantity { get; set; } = 0;

        public int? LowStockthreshold { get; set; } = 0;

        public float RegularPrice { get; set; } = 0;

        public int? ProductWaight { get; set; } = 0;
        public string? ProductCode { get; set; } = string.Empty;

        public string? ProductTrackingNo { get; set; } = string.Empty;


        public string? StockStatus { get; set; } = string.Empty;

        public string? Visibility { get; set; } = string.Empty;


        public string? ProductImagePath { get; set; } = string.Empty;

        public string? ProductTags { get; set; } = string.Empty;

        public string? ProductImage { get; set; } = string.Empty;

        public int Discount { get; set; } = 0;

        public string? Discription { get; set; } = string.Empty;
    }
}
