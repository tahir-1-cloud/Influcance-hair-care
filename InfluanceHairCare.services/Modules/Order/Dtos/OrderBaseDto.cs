using InfluanceHairCare.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.Order.Dtos
{
    public class OrderBaseDto
    {
        public long OrderId { get; set; }
        public double TotalPrice { get; set; }
        public int Discount { get; set; }
        public double GrandTotal { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public float OrderPendingPayment { get; set; }
        public float OrderPaidAmount { get; set; }
        public int OrderBy { get; set; }

    }
}
