using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.models
{
    public class ProductRating
    {
        [Key]
        public int RatingId { get; set; }

        public string Comments { get; set; }=string.Empty;

        public float Rating { get; set; }

        public DateTime RatingDate { get; set; }
        public long OrderId { get; set; }   

        public int CustomerId { get; set; }

        public int SaleRepId { get; set; }

        //public Customer Customer { get; set; }

        //public Orders orders { get; set; }  

    }
}
