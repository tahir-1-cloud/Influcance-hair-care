using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.OrderRating.Dtos
{
    public class OrderRatingBaseDto
    {
        public int RatingId { get; set; }

        public string Comments { get; set; } = string.Empty;

        public float Rating { get; set; }

        public DateTime RatingDate { get; set; }
        public long OrderId { get; set; }

        public int CustomerId { get; set; }

        public int SaleRepId { get; set; }
    }
}
