using InfluanceHairCare.models;
using InfluanceHairCare.services.Modules.Order.Dtos;
using InfluanceHairCare.services.Modules.Order.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InfluanceHairCare.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderInterface _ord;
        public OrderController(IOrderInterface ord)
        {
            _ord = ord;
        }
        // GET: api/<OrderController>
        [HttpGet("GetAllPendingOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var response = await _ord.AllPendingOrders();

            return Ok(response);
        }

        [HttpGet("GetAllDeliveredOrders")]
        public async Task<IActionResult> GetAllDeliveredOrders()
        {
            var response = await _ord.AllDeliveredOrders();

            return Ok(response);
        }

        // GET api/<OrderController>/5
        [HttpGet("SaleRepCustomerOrders/{SaleRepId}")]
        public async Task<IActionResult> SaleRepCustomerOrders(int SaleRepId)
        {
            var response = await _ord.SaleRepCustomerOrders(SaleRepId);

            return Ok(response);
        }
        [HttpGet("GetCustomerOrders/{CustomerId}")]
        public async Task<IActionResult> CustomerOrders(int CustomerId)
        {
            var response = await _ord.CustomerOrders(CustomerId);

            return Ok(response);
        }

        [HttpGet("GetOrderProductsDetails/{orderId}")]
        public async Task<IActionResult> GetOrderProductDetails(long orderId)
        {
            var response = await _ord.OrderProductsDetail(orderId);

            return Ok(response);
        }

        // POST api/<OrderController>
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody]OrderRequestDto order)
        {
            var response = await _ord.AddToCart(order);

            return Ok(response);
        }

        [HttpPut("UpdateOrderedStatus")]
        public async Task<IActionResult> UpdateOrderStatusDelivered(SaleRepOrderPayments payment)
        {
            var res = await _ord.UpdateOrderStatusDelivered(payment);
            return Ok(res);
        }
        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
