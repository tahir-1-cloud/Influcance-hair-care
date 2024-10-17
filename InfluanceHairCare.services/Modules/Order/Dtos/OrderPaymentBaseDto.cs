using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.Order.Dtos
{
    public class OrderPaymentBaseDto
    {
        public int Id { get; set; }
        [MaxLength(10)]
        public string PaymentMethod { get; set; } = "";
        public float PaymentAmount { get; set; } = 0;
        public long OrderId { get; set; }

    }
}
