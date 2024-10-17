using InfluanceHairCare.services.Modules.Product.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.Order.Dtos
{
    public class OrderRequestDto : OrderProductDto
    {
        public List<OrderPaymentBaseDto> OrderPayment { get; set; } = new List<OrderPaymentBaseDto>();
        public string ChequeNumber { get; set; } = string.Empty;
        public DateTime ChequeExpiryDate { get; set; }
        public string ChequeFor { get; set; } = string.Empty;
    }
}
