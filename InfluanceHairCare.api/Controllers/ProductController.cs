using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InfluanceHairCare.services.Modules.System.Dtos;
using InfluanceHairCare.Shared.DataConfig;
using StatusCodes = InfluanceHairCare.Shared.DataConfig.StatusCodes;
using InfluanceHairCare.models.DataContext;
using InfluanceHairCare.models;
using Microsoft.EntityFrameworkCore;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;
using System.IO;
//using LinqToDB;
using InfluanceHairCare.services.Modules.Order.Dtos;
using InfluanceHairCare.utilities.Common;
using System.Net;
using LinqToDB.EntityFrameworkCore;
using EllipticCurve.Utils;
using Abp.Linq.Expressions;
using System.Linq;

namespace InfluanceHairCare.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDataContext db;
        private IWebHostEnvironment env;
        public ProductController(ApplicationDataContext _db, IWebHostEnvironment _env)
        {
            db = _db;
            env = _env;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(Products product)
        {
            try
            {
                var Response = ResponseBuilder.BuildWSResponse<Products>();

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                Products prd = new Products();
                prd.Discount = product.Discount;
                prd.ProductId = product.ProductId;
                prd.ProductName = product.ProductName;
                prd.Discription = product.Discription;
                prd.LowStockthreshold = product.LowStockthreshold;
                prd.ProductImagePath = product.ProductImagePath;
                prd.price = product.price;
                prd.ProductTags = product.ProductTags;
                prd.ProductTrackingNo = product.ProductTrackingNo;
                prd.ProductWaight = product.ProductWaight;
                prd.RegularPrice = product.RegularPrice;
                prd.Visibility = product.Visibility;
                prd.StockStatus = product.StockStatus;
                prd.ProductCode = product.ProductCode;
                prd.Quantity = product.Quantity;

                await db.Products.AddAsync(prd);
                await db.SaveChangesAsync();
                return Ok(Response);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.")
                {
                    var Response = ResponseBuilder.BuildWSResponse<Products>();
                    ResponseBuilder.SetWSResponse(Response, StatusCodes.FIELD_REQUIRED, null, null);
                    return Ok(Response);
                }
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateProduct/{id}")]

        public async Task<IActionResult> UpdateProduct(int id, Products data)
        {
            try
            {
                var Response = ResponseBuilder.BuildWSResponse<Products>();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);

                }

                if (id != data.ProductId)
                {
                    return BadRequest();
                }

                var record = await db.Products.FindAsync(id);
                if (record == null)
                {
                    return BadRequest();
                }
                if (data.ProductName != null && data.ProductName != "undefined")
                {
                    record.ProductName = data.ProductName;
                }
                if (data.ProductCode != null && data.ProductCode != "undefined")
                {
                    record.ProductCode = data.ProductCode;
                }

                data = record;
                db.Entry(data).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok(Response);


            }
            catch (Exception ex)
            {
                if (ex.Message == "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.")
                {
                    var Response = ResponseBuilder.BuildWSResponse<Products>();
                    ResponseBuilder.SetWSResponse(Response, StatusCodes.FIELD_REQUIRED, null, null);
                    return Ok(Response);
                }
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("ProductUpdateById/{id}")]

        public async Task<IActionResult> ProductUpdateById(int id)
        {
            try
            {
                var Response = ResponseBuilder.BuildWSResponse<Products>();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (id == 0)
                {
                    return BadRequest("Id Required");
                }

                var record = await db.Products.FindAsync(id);

                return Ok(record);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.")
                {
                    var Response = ResponseBuilder.BuildWSResponse<Products>();
                    ResponseBuilder.SetWSResponse(Response, StatusCodes.FIELD_REQUIRED, null, null);
                    return Ok(Response);
                }
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Productdataupdate")]
        public async Task<IActionResult> Productdataupdate(Products update)
        {
            try
            {
                var Response = ResponseBuilder.BuildWSResponse<Products>();
                if (ModelState.IsValid)
                {
                    var data = db.Products.Where(x => x.ProductId == update.ProductId).FirstOrDefault();
                    if (data != null)
                    {
                        data.ProductName = update.ProductName;
                        data.price = update.price;
                        data.Quantity = update.Quantity;
                        data.LowStockthreshold = update.LowStockthreshold;
                        data.RegularPrice = update.RegularPrice;
                        data.ProductWaight = update.ProductWaight;
                        data.ProductCode = update.ProductCode;
                        data.ProductTrackingNo = update.ProductTrackingNo;
                        data.StockStatus = update.StockStatus;
                        data.Visibility = update.Visibility;
                        data.ProductImage = update.ProductImage;
                        data.ProductImagePath = update.ProductImagePath;
                        data.ProductTags = update.ProductTags;
                        data.Discount = update.Discount;
                        data.Discription = update.Discription;
                        db.Products.Update(data);
                        await db.SaveChangesAsync();
                        return Ok(Response);
                    }

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.")
                {
                    var Response = ResponseBuilder.BuildWSResponse<Products>();
                    ResponseBuilder.SetWSResponse(Response, StatusCodes.FIELD_REQUIRED, null, null);
                    return Ok(Response);
                }
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ProductGet")]
        public IActionResult ProductGet()
        {
            try
            {
                var Response = ResponseBuilder.BuildWSResponse<List<Products>>();
                var record = db.Products.OrderByDescending(x => x.ProductId).ToList();
                ResponseBuilder.SetWSResponse(Response, StatusCodes.SUCCESS_CODE, null, record);
                return Ok(Response);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.")
                {
                    var Response = ResponseBuilder.BuildWSResponse<Products>();
                    ResponseBuilder.SetWSResponse(Response, StatusCodes.FIELD_REQUIRED, null, null);
                    return Ok(Response);
                }
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var Response = ResponseBuilder.BuildWSResponse<Products>();
                var record = db.Products.Where(x => x.ProductId == id)?.FirstOrDefault();
                if (record != null)
                {
                    db.Products.Remove(record);
                    await db.SaveChangesAsync();
                    ResponseBuilder.SetWSResponse(Response, StatusCodes.SUCCESS_CODE, null, record);

                }
                else
                {
                    ResponseBuilder.SetWSResponse(Response, StatusCodes.RECORD_NOTFOUND, null, null);
                }
                return Ok(Response);

            }
            catch (Exception ex)
            {
                if (ex.Message == "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.")
                {
                    var Response = ResponseBuilder.BuildWSResponse<Products>();
                    ResponseBuilder.SetWSResponse(Response, StatusCodes.FIELD_REQUIRED, null, null);
                    return Ok(Response);
                }
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("ProductSearch")]

        public IActionResult ProductSearch(string? name, string? code)
        {
            try
            {
                var Response = ResponseBuilder.BuildWSResponse<List<Products>>();

                var record = new List<Products>();
               
                if (name != null)
                {
                    record = db.Products.Where(x => x.ProductName.ToLower().Contains(name.ToLower())).ToList();
                }
                if (code != null)
                {

                    record = db.Products.Where(x => x.ProductCode.ToLower().Contains(code.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(name) && record != null)
                {
                    record = record.Where(x => x.ProductName.ToLower().Contains(name.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(code) && record != null)
                {
                    record = record.Where(x => x.ProductCode.ToLower().Contains(code.ToLower())).ToList();
                }
                if (record != null && record.Any())
                {
                    ResponseBuilder.SetWSResponse(Response, StatusCodes.SUCCESS_CODE, null, record);
                }
                return Ok(Response);

            }
            catch (Exception ex)
            {
                if (ex.Message == "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.")
                {
                    var Response = ResponseBuilder.BuildWSResponse<Products>();
                    ResponseBuilder.SetWSResponse(Response, StatusCodes.FIELD_REQUIRED, null, null);
                    return Ok(Response);
                }
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("RandomSearch")]
        public IActionResult RandomSearch(string? search)
        {
            try
            {
                var Response = ResponseBuilder.BuildWSResponse<List<Products>>();

                var record = new List<Products>();
                if (search != null)
                {
                    record = db.Products.Where(x => x.ProductName==search 
                    ||x.ProductCode==search|| x.ProductTags==search|| x.ProductTrackingNo==search||x.Discription==search || x.ProductId.ToString() == search).ToList();
                }
             
                if (!string.IsNullOrEmpty(search) && record != null)
                {
                    record = db.Products.Where(x => x.ProductName == search
                     || x.ProductCode == search ||  x.ProductTags == search || x.ProductTrackingNo == search || x.Discription == search ||x.ProductId.ToString()==search).ToList();
                }
               
                if (record != null && record.Any())
                {
                    ResponseBuilder.SetWSResponse(Response, StatusCodes.SUCCESS_CODE, null, record);
                }
                return Ok(Response);

            }
            catch (Exception ex)
            {
                if (ex.Message == "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.")
                {
                    var Response = ResponseBuilder.BuildWSResponse<Products>();
                    ResponseBuilder.SetWSResponse(Response, StatusCodes.FIELD_REQUIRED, null, null);
                    return Ok(Response);
                }
                return BadRequest(ex.Message);
            }


        }

        //BroadcastMessage

        [HttpGet("TestingSearch")]
        public async Task<IEnumerable<Products>> TestingSearch(string? search)
        {
            try
            {
                var query = db.Products.AsQueryable();

                var result = await query.Select(x => new Products
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    ProductCode = x.ProductCode,
                    ProductTags = x.ProductTags,
                    ProductWaight = x.ProductWaight,
                    ProductTrackingNo = x.ProductTrackingNo,
                    Discription = x.Discription,


                }).ToListAsync();

                return result;

               // return null;


            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("BroadCastMessage")]
        public async Task<IActionResult> BroadCastMessage(Broadcast broadcast)
        {
            try
            {


                var Response = ResponseBuilder.BuildWSResponse<Broadcast>();

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                db.Broadcasts.Add(broadcast);
                await db.SaveChangesAsync();
                return Ok(Response);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.")
                {
                    var Response = ResponseBuilder.BuildWSResponse<Broadcast>();
                    ResponseBuilder.SetWSResponse(Response, StatusCodes.FIELD_REQUIRED, null, null);
                    return Ok(Response);
                }
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("BroadcastGet")]
        public IActionResult BroadcastGet()
        {
            try
            {
                var Response = ResponseBuilder.BuildWSResponse<List<Broadcast>>();
                var record = db.Broadcasts.OrderByDescending(x => x.BroadId).ToList();
                ResponseBuilder.SetWSResponse(Response, StatusCodes.SUCCESS_CODE, null, record);
                return Ok(Response);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.")
                {
                    var Response = ResponseBuilder.BuildWSResponse<Broadcast>();
                    ResponseBuilder.SetWSResponse(Response, StatusCodes.FIELD_REQUIRED, null, null);
                    return Ok(Response);
                }
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("ProductRating")]
        public async Task<IActionResult> ProductRating(ProductRating Rating)
        {
            try
            {
                var Response = ResponseBuilder.BuildWSResponse<ProductRating>();
                var record = db.Customers.FirstOrDefault(x => x.Id == Rating.CustomerId);
                if (record != null && record.SaleRepID != null)
                {
                    ProductRating rate = new ProductRating();
                    rate.Comments = Rating.Comments;
                    rate.Rating = Rating.Rating;
                    rate.RatingDate = Rating.RatingDate;
                    rate.SaleRepId = (int)record.SaleRepID;
                    rate.OrderId = Rating.OrderId;
                    rate.CustomerId = Rating.CustomerId;
                    db.ProductRatings.Add(rate);
                    await db.SaveChangesAsync();
                    var avg = await db.ProductRatings.Where(x => x.SaleRepId == record.SaleRepID).AverageAsync(a => a.Rating);

                    var saleRep = await db.SaleReps.Where(x => x.Id == rate.SaleRepId).FirstOrDefaultAsync();
                    if (saleRep != null)
                    {
                        saleRep.Rating = avg;
                        db.Entry(saleRep).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                    //await db.SaleReps.Where(c => c.Id == rate.SaleRepId).Set(cu => cu.Rating, avg).UpdateAsync();
                }

                return Ok(Response);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.")
                {
                    var Response = ResponseBuilder.BuildWSResponse<ProductRating>();
                    ResponseBuilder.SetWSResponse(Response, StatusCodes.FIELD_REQUIRED, null, null);
                    return Ok(Response);
                }
                return BadRequest(ex.Message);
            }
        }

    }
}
