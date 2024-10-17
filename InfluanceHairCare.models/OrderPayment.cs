using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.models
{
    public class OrderPayment
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(10)]
        public string PaymentMethod { get; set; } = string.Empty;
        public float PaymentAmount { get; set; } = 0;

        public long OrdersId { get; set; }
        [ForeignKey("OrdersId")]
        public Orders? Orders { get; set; }
    }
}
