using InfluanceHairCare.models;
using InfluanceHairCare.services.Modules.Order.Dtos;
using InfluanceHairCare.utilities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.Order.Interfaces
{
    public interface IOrderInterface
    {
        public Task<GenericResponse<List<OrderResponseDto>>> AllPendingOrders();
        public Task<GenericResponse<List<OrderResponseDto>>> AllDeliveredOrders();
        public Task<GenericResponse<List<OrderResponseDto>>> SaleRepCustomerOrders(int id);
        public Task<GenericResponse<List<OrderResponseDto>>> CustomerOrders(int id);
        public Task<GenericResponse<OrderResponseDto>> OrderProductsDetail(long orderId);
        public Task<GenericResponse<OrderRequestDto>> AddToCart(OrderRequestDto OrdProduct);
        public Task<GenericResponse<OrderBaseDto>> UpdateOrderStatusDelivered(SaleRepOrderPayments orderPayment);
    }
}
