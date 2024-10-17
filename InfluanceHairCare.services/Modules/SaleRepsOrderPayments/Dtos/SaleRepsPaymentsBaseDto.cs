using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.SaleRepsOrderPayments.Dtos
{
    public class SaleRepsPaymentsBaseDto
    {
        public int Id { get; set; }
        public float PaidAmount { get; set; }
        public DateTime DeliveryDate { get; set; }
        public long OrderId { get; set; }       
        public int SaleRepId { get; set; }
    }
}
