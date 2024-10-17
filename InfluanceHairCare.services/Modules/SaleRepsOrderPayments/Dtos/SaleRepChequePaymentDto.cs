using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.SaleRepsOrderPayments.Dtos
{
    public class SaleRepChequePaymentDto : SaleRepsPaymentsBaseDto
    {
        public string ChequeNumber { get; set; } = string.Empty;
        public string ChequeFor { get; set; } = string.Empty;
        public DateTime ChequeExpiryDate { get; set; }

    }
}
