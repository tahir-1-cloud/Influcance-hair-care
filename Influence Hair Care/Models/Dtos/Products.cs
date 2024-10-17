using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Influence_Hair_Care.Models.Dtos
{
    public class Products
    {
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public float price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int LowStockthreshold { get; set; }

        public float RegularPrice { get; set; }

        public int ProductWaight { get; set; }

        [Required]
        public string ProductCode { get; set; }

        public string ProductTrackingNo { get; set; }

        public bool StockStatus { get; set; }

        public bool Visibility { get; set; }

        //public byte[] ProductImageByte { get; set; }

        public string ProductImagePath { get; set; }

        public string ProductTags { get; set; }

        public string ProductImage { get; set; }

        [NotMapped]
        public IFormFile? files { get; set; }
        //public int ProductId { get; set; }
        //[Required]
        //public string ProductName { get; set; }
        //[Required]

        //public float price { get; set; }

        //[Required]

        //public int Quantity { get; set; }

        //[Required]

        //public int LowStockthreshold { get; set; }

        //[Required]

        //public float RegularPrice { get; set; }

        //[Required]

        //public int ProductWaight { get; set; }

        //public string ProductCode { get; set; }

        //[Required]

        //public string ProductTrackingNo { get; set; }


        //public bool StockStatus { get; set; }


        //public bool Visibility { get; set; }


        //public string ProductImage { get; set; }

        //[Required]

        //public string ProductTags { get; set; }
    }
}
