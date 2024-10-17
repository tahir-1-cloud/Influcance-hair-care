using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "ProductName is Required")]
        public string? ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "price is Required")]

        public float? price { get; set; } = 0;

        [Required(ErrorMessage = "Quantity is Required")]

        public int? Quantity { get; set; } = 0;


        public int? LowStockthreshold { get; set; } = 0;

        [Required(ErrorMessage = "RegularPrice is Required")]

        public float? RegularPrice { get; set; } = 0;

        [Required(ErrorMessage = "ProductWaight is Required")]

        public int? ProductWaight { get; set; } = 0;

        [Required(ErrorMessage = "ProductCode is Required")]

        public string? ProductCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "ProductTrackingNo is Required")]

        public string? ProductTrackingNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "StockStatus is Required")]

        public string? StockStatus { get; set; } = string.Empty;

        public string? Visibility { get; set; } = string.Empty;

  

        public string? ProductImagePath { get; set; } = string.Empty;

        [Required(ErrorMessage = "ProductTags is Required")]

        public string? ProductTags { get; set; } = string.Empty;

        public string? ProductImage { get; set; } = string.Empty;

        public int? Discount { get; set; } = 0;

        public string? Discription { get; set; } = string.Empty;

        public virtual ICollection<OrderProducts>? Orders { get; set; }
        public virtual ICollection<CustomerfavoriteProducts>? CustomerFavorites { get; set; }
    }
}
