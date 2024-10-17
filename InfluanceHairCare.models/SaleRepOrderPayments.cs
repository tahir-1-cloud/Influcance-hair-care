using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.models
{
    public class SaleRepOrderPayments
    {
        public int Id { get; set; }
        public float PaidAmount { get; set; } = 0;
        public DateTime DeliveryDate { get; set; }
        public long OrderId { get; set; }      
        public int SaleRepId { get; set; }
        public string ChequeNumber { get; set; } = string.Empty;
        public string ChequeFor { get; set; } = string.Empty;
        public bool SaleRepOrderTimeAmount { get; set; } = false;
        public DateTime ChequeExpiryDate { get; set; } 
    }
}
