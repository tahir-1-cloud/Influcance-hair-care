using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.SalesRep.Dtos
{
    public class SaleRepReviewDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerComment { get; set; } = string.Empty;

        public string CustomerImageUrl { get; set; } = string.Empty;
        public DateTime RatingDate { get; set; }
        public double OrderAmount { get; set; }
        public float Rating { get; set; }
        public float OverAllRating { get; set; }
    }
}
