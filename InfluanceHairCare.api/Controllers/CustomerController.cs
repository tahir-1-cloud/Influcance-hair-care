using InfluanceHairCare.models.DataContext;
using InfluanceHairCare.models;
using InfluanceHairCare.services.Modules.Customers.Dtos;
using InfluanceHairCare.services.Modules.Customers.Interfaces;
using InfluanceHairCare.Shared.DataConfig;
using InfluanceHairCare.utilities.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using StatusCodes = InfluanceHairCare.Shared.DataConfig.StatusCodes;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
using InfluanceHairCare.services.Modules.CustomerFavoriteProducts.Dtos;
using static LinqToDB.Common.Configuration;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InfluanceHairCare.api.Controllers
{

    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerInterface _cust;
        private readonly ApplicationDataContext _db;
        private IWebHostEnvironment _env;

        public CustomerController(ICustomerInterface cust, ApplicationDataContext db, IWebHostEnvironment env)
        {
            _cust = cust;
            _db = db;
            _env = env;
        }

        private string ConvertImageByte(byte[] str, string ImgName)
        {
            string hostRootPath = _env.WebRootPath;
            string webRootPath = _env.ContentRootPath;
            string imgPath = string.Empty;

            if (!string.IsNullOrEmpty(webRootPath))
            {
                string path = webRootPath + "\\CustomerImages\\";
                string imageName = ImgName + ".jpg";
                imgPath = Path.Combine(path, imageName);
                byte[] bytes = str;
                System.IO.File.WriteAllBytes(imgPath, bytes);
                imgPath = $"CustomerImages/{imageName}";
            }
            else if (!string.IsNullOrEmpty(hostRootPath))
            {
                string path = hostRootPath + "\\CustomerImages\\";
                string imageName = ImgName + ".jpg";
                imgPath = Path.Combine(path, imageName);
                byte[] bytes = str;
                System.IO.File.WriteAllBytes(imgPath, bytes);
                imgPath = $"CustomerImages/{imageName}"; ;
            }
            return imgPath;
        }
        [HttpPut("CustomerImagetoByte/{id}")]
        public async Task<IActionResult> CustomerImagetoByte(Customer model, int id)
        {
            try
            {
                var Response = ResponseBuilder.BuildWSResponse<Customer>();


                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (id != model.Id)
                {
                    return BadRequest();
                }
                var result = _db.Customers.Where(x => x.Id == id).FirstOrDefault();
                if (result == null)
                {
                    return BadRequest();
                }
                if (model.CustomerImage != null && result.CustomerImage!=null)
                {
                    //Customer obj = new Customer();
                    result.CustomerImage = model.CustomerImage;
                    var bytes = JsonConvert.DeserializeObject<byte[]>(result.CustomerImage);
                    if (bytes != null)
                    {
                        var customerImagepath = ConvertImageByte(bytes, Guid.NewGuid().ToString());
                        var imagebypath = customerImagepath;
                        //  result.CustomerImage = model.CustomerImage;
                        model.CustomerImagePath = imagebypath;
                    }

                }
                _db.Customers.Update(model);
                await _db.SaveChangesAsync();

                //model = result;

                //byte[] byts = { };
                //var result = _db.Customers.Where(x => x.Id == id).Select(x => x.CustomerImage).FirstOrDefault();

                //if (byts != null )
                //{

                //    result = ConvertImageByte(byts, Guid.NewGuid().ToString());

                //}


                return Ok(Response);
            }

            catch (Exception)
            {

                throw;
            }
        }
        // GET: api/<CustomerController>
        [HttpGet("AllCustomers")]
        public async Task<IActionResult> Get()
        {
            try
            {

                var users = await _cust.GetAllCustomers();
                return Ok(users);

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("AllCustomersWithoutSaleRep")]
        public async Task<IActionResult> GetCustomer()
        {
            try
            {

                var users = await _cust.GetCustomersWithOutSaleRep();
                return Ok(users);

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("GetAccountBalance/{CustomerId}")]
        public async Task<IActionResult> GetCustomerAccountBalance(int CustomerId)
        {
            var users = await _cust.GetCustomerAccountBalance(CustomerId);
            return Ok(users);
        }
        [HttpPut("UpdateCustomerBalanceNCreditLimit")]
        public async Task<IActionResult> UpdateCustomerBalanceNCreditLimit([FromBody] CustomerBalanceDto blnc)
        {
            var res = await _cust.UpdateCustomerAccountBalance(blnc);
            return Ok(res);
        }

        [HttpPut("UpdateCustomerSaleRep")]
        //public async Task<IActionResult> UpdateCustomerSaleRep(CustomersBaseDto obj)
        public async Task<IActionResult> UpdateCustomerSaleRep(int cId, int sId, int creditLimit)
        {
           
            //var o = JsonConvert.DeserializeObject(obj);

            var res = await _cust.UpdateCustomerSaleRep(cId, sId, creditLimit);
            return Ok(res);
        }

        [HttpGet("AllCustomerWithSaleRep")]
        public async Task<IActionResult> AllCustomerWithSaleRep()
        {
            var data = await _cust.AllCustomerWithSaleRep();
            return Ok(data);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CustomerController>
        [AllowAnonymous]
        [Route("RegisterCustomer")]
        [HttpPost]
        public async Task<IActionResult> RegisterCustomer([FromBody] CustomerRegisterDto value)
        {
            var data = await _cust.RegisterCustomer(value);
            return Ok(data);
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
        [HttpGet("GetCustomerFavoriteProducts/{CustomerId}")]
        public async Task<IActionResult> GetCustomerFavoriteProducts(int CustomerId)
        {
            var users = await _cust.GetCustomerFavoriteProducts(CustomerId);
            return Ok(users);
        }

        [HttpPost("AddToFavorite")]
        public async Task<IActionResult> AddToFavorite([FromBody] CustomerFavoriteProductBaseDto value)
        {
            var data = await _cust.AddToFavorite(value);
            return Ok(data);
        }
        [HttpGet("Search")]
        public IActionResult Search(string name)
        {
            try
            {
                var Response = ResponseBuilder.BuildWSResponse<List<Customer>>();

                var record = _db.Customers.Where(x => x.FirstName.ToLower().Contains(name.ToLower()) || x.LastName.ToLower().Contains(name.ToLower())).ToList();
                if (!string.IsNullOrEmpty(name) && record != null)
                {

                    ResponseBuilder.SetWSResponse(Response, StatusCodes.SUCCESS_CODE, null, record);

                }
                return Ok(Response);

            }
            catch (Exception ex)
            {
                if (ex.Message == "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.")
                {
                    var Response = ResponseBuilder.BuildWSResponse<Customer>();
                    ResponseBuilder.SetWSResponse(Response, StatusCodes.FIELD_REQUIRED, null, null);
                    return Ok(Response);
                }
                return BadRequest(ex.Message);
            }
        }


    }
}
