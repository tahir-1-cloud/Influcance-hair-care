using InfluanceHairCare.services.Modules.Customers.Dtos;
using InfluanceHairCare.services.Modules.Customers.Interfaces;
using InfluanceHairCare.services.Modules.SalesRep.Dtos;
using InfluanceHairCare.services.Modules.SalesRep.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InfluanceHairCare.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleRepController : ControllerBase
    {
        private readonly ISaleRepInterface _sale;
        public SaleRepController(ISaleRepInterface sale)
        {
            _sale = sale;
        }
        // GET: api/<SaleRepController>
        [HttpGet("GetAllSaleReps")]
        public async Task<IActionResult> AllSaleReps()
        {
            var data = await _sale.AllSaleReps();
            return Ok(data);
        }

        // GET api/<SaleRepController>/5
        [HttpGet("SaleRepCustomers/{SalerepId}")]
        public async Task<IActionResult> SaleRepCustomers(int SalerepId)
        {
            var data = await _sale.GetSaleRepCustomers(SalerepId);
            return Ok(data);
        }

        [HttpGet("SaleRepWithOrderSatus/{status}")]
        public async Task<IActionResult> SaleRepWithOrderSatus(string status)
        {
            var data = await _sale.SaleRepWithOrderSatus(status);
            return Ok(data);
        }

        [HttpGet("SaleRepOrderAmount/{SalerepId}")]
        public async Task<IActionResult> SaleRepOrderAmount(int SalerepId)
        {
            var data = await _sale.GetSaleRepCustomers(SalerepId);
            return Ok(data);
        }

        [HttpGet("GetSaleRepReviews/{saleRepId}")]
        public async Task<IActionResult> SaleRepReviews(int saleRepId)
        {
            var data = await _sale.GetSaleRepReviews(saleRepId);
            return Ok(data);
        }
        // POST api/<SaleRepController>
        [Route("RegisterSaleRep")]
        [HttpPost]
        public async Task<IActionResult> RegisterSaleRep([FromBody] SaleRepRegisterDto value)
        {
            var data = await _sale.RegisterSaleRep(value);
            return Ok(data);
        }

        [Route("RegisterCustomerBySaleRep")]
        [HttpPost]
        public async Task<IActionResult> ResgisterCustomerBySaleRep([FromBody] CustomerRegisterDto customerDto)
        {
            var data = await _sale.RegisterCustomerBySaleRep(customerDto);
            return Ok(data);
        }

        // PUT api/<SaleRepController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            
        }

        // DELETE api/<SaleRepController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
