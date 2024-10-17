using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.models
{
    public class Orders
    {
        [Key]
        public long OrderId { get; set; }
        public double TotalPrice { get; set; }
        public int Discount { get; set; } = 0;
        public double GrandTotal { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; } = "Pending";
        public float OrderPendingPayment { get; set; }
        public float OrderPaidAmount { get; set; } = 0;
        public int OrderBy { get; set; } = 0;
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public virtual ICollection<OrderPayment>? OrderPayments { get; set; }

        public virtual ICollection<OrderProducts> Products { get; set; }


    }
}
