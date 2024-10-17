using InfluanceHairCare.models;
using InfluanceHairCare.services.Modules.Order.Dtos;
using InfluanceHairCare.utilities.Common;
using Influence_Hair_Care.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using static Influence_Hair_Care.Models.Data.RequestSender;

namespace Influence_Hair_Care.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult OrderInvoice(long id)
        {
            try
            {
                SResponse ress = RequestSender.Instance.CallAPI("api", "Order/GetOrderProductsDetails/" + id, "Get");
                if (ress.Status && (ress.Resp != null) && (ress.Resp != ""))
                {
                    var response = JsonConvert.DeserializeObject<GenericResponse<OrderResponseDto>>(ress.Resp);
                    return Json(response.Data);
                }
                return Json(null);
            }
            catch (Exception)
            {
                throw;
            }

          
        }

        public IActionResult GetAllOrders(int id)
        {
            try
            {
                SResponse ress = RequestSender.Instance.CallAPI("api" , "Order/SaleRepCustomerOrders/" + id, "GET");
                if (ress.Status && (ress.Resp != null) && (ress.Resp != ""))
                {
                    ResponseBack<List<OrderResponseDto>> response =
                              JsonConvert.DeserializeObject<ResponseBack<List<OrderResponseDto>>>(ress.Resp);
                    
                    return View(response.Data);
                }
                return Json(null);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
