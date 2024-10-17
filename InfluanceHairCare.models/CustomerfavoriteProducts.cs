using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.models
{
    public class CustomerfavoriteProducts
    {
        [Required]
        public int CustomerId { get; set; } 

        public Customer Customer { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Products Product { get; set; }

        public DateTime DateTime { get; set; }
    }
}
