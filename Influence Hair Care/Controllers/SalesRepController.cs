using InfluanceHairCare.models;
using Microsoft.AspNetCore.Hosting;
using Influence_Hair_Care.Models.Data;
using Influence_Hair_Care.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Newtonsoft.Json;
using static Influence_Hair_Care.Models.Data.RequestSender;
using System.Collections.Generic;
using InfluanceHairCare.services.Modules.SalesRep.Dtos;
using Broadcast = InfluanceHairCare.models.Broadcast;
using InfluanceHairCare.services.Modules.Order.Dtos;

namespace Influence_Hair_Care.Controllers
{
    public class SalesRepController : Controller
    {
        private IWebHostEnvironment _env;
        public SalesRepController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        public IActionResult ProductList()
        {
            return View();
        }

        public IActionResult RegisterSaleRep()
        {
            return View();
        }

        public IActionResult SaleRepList()
        {
            return View();
        }

        public IActionResult ProductDetails()
        {
            return View();
        }
        public IActionResult ProductInfo()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllSaleReps()
        {

            try
            {
                SResponse ress = RequestSender.Instance.CallAPI("api", "SaleRep/GetAllSaleReps", "GET");
                if (ress.Status && (ress.Resp != null) && (ress.Resp != ""))
                {
                    ResponseBack<List<SaleRepResponseDto>> response =
                                JsonConvert.DeserializeObject<ResponseBack<List<SaleRepResponseDto>>>(ress.Resp);
                    if (response.Message == "Success")
                    {
                        return View(response.Data);
                    }
                    else
                    {
                        TempData["error"] = "Server is down.";
                    }
                }
                return View();


            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult GetAllSaleRep()
        {

            try
            {
                SResponse ress = RequestSender.Instance.CallAPI("api", "SaleRep/GetAllSaleReps", "GET");
                if (ress.Status && (ress.Resp != null) && (ress.Resp != ""))
                {
                    ResponseBack<List<SaleRepResponseDto>> response =
                                JsonConvert.DeserializeObject<ResponseBack<List<SaleRepResponseDto>>>(ress.Resp);
                    if (response.Message == "Success")
                    {
                        List<SaleRepResponseDto> responseObject = response.Data;
                        return Json(responseObject);
                    }
                    else
                    {
                        TempData["error"] = "Server is down.";
                    }
                }
                return Json("");


            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult ViewAllCustomers()
        {

            try
            {
                SResponse ress = RequestSender.Instance.CallAPI("api", "Customer/AllCustomers", "GET");
                if (ress.Status && (ress.Resp != null) && (ress.Resp != ""))
                {
                    ResponseBack<List<Customer>> response =
                                JsonConvert.DeserializeObject<ResponseBack<List<Customer>>>(ress.Resp);
                    if (response.Message == "Success")
                    {
                        List<Customer> responseObject = response.Data;
                        return View(responseObject);
                    }
                    else
                    {
                        TempData["error"] = "Server is down.";
                    }
                }
                return View();


            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public IActionResult AddCategories()
        {
            return View();
        }

        public IActionResult AddSubCategory()
        {
            return View();
        }

        public IActionResult AddSaleRep()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddSaleRep(SaleRepRegisterDto addsalerep)
        {
            try
            {

                var files = HttpContext.Request.Form.Files;
                foreach (var file in files)
                {
                    if (file.Name != "")
                    {
                        string path = this._env.WebRootPath + "\\salereps\\" + file.FileName;
                        var fileExtension = Path.GetExtension(file.FileName);
                        var fileName = string.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                        using (var fs = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(fs);

                        }

                        addsalerep.SaleRepImagePath = path;
                        addsalerep.SaleRepImage = fileName;
                    }
                }

                var body = JsonConvert.SerializeObject(addsalerep);
                SResponse resp = RequestSender.Instance.CallAPI("api", "SaleRep/RegisterSaleRep", "POST", body);
                if (resp.Status && (resp.Resp != null) && (resp.Resp != ""))
                {
                    TempData["success"] = "SaleRep Added Successfully";
                    return RedirectToAction("AddSaleRep");
                }
                else
                {
                    TempData["error"] = resp.Resp + " Unable To Add.";
                    return RedirectToAction("AddSaleRep");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Unable to Add." + ex.Message;
                return RedirectToAction("AddSalesReps");
            }
        }
     

        public IActionResult Viewproduct()
        {
            return View();
        }

        public IActionResult ViewCustomer()
        {
            return View();
        }

        public IActionResult ProductInformation()
        {
            return View();
        }
        [HttpGet]
        public IActionResult getSaleRep(string status)
        {
            try
            {
                //string status = "Delivered";
                SResponse ress = RequestSender.Instance.CallAPI("api", "SaleRep/SaleRepWithOrderSatus/"+status, "GET");
                if (ress.Status && (ress.Resp != null) && (ress.Resp != ""))
                {
                    ResponseBack<List<SaleRepResponseDto>> response =
                                JsonConvert.DeserializeObject<ResponseBack<List<SaleRepResponseDto>>>(ress.Resp);
                    if (response.Message == "Success")
                    {
                        List<SaleRepResponseDto> responseObject = response.Data;
                        return Json(responseObject);

                    }
                    else
                    {
                        TempData["response"] = "Server is down.";
                    }
                }
                return Json("");


            }
            catch (System.Exception)
            {

                throw;
            }
        }


        [HttpGet]
        public IActionResult ChangeSaleReps()
        {
            try
            {
                SResponse ress = RequestSender.Instance.CallAPI("api", "SaleRep/GetAllSaleReps", "GET");
                if (ress.Status && (ress.Resp != null) && (ress.Resp != ""))
                {
                    ResponseBack<List<SaleRepResponseDto>> response =
                                JsonConvert.DeserializeObject<ResponseBack<List<SaleRepResponseDto>>>(ress.Resp);
                    if (response.Message == "Success")
                    {
                        return View(response.Data);
                    }
                    else
                    {
                        TempData["error"] = "Server is down.";
                    }
                }
                return View();


            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public IActionResult SaleRepDiscount()
        {
            return View();
        }

        public IActionResult ProductDiscount()
        {
            return View();
        }
       

        public IActionResult CurrentOrder()
        {
            try
            {
                SResponse ress = RequestSender.Instance.CallAPI("api", "Order/GetAllPendingOrders", "GET");
                ResponseBack<List<OrderResponseDto>> response =JsonConvert.DeserializeObject<ResponseBack<List<OrderResponseDto>>>(ress.Resp);
                if (ress.Status && (ress.Resp != null) && (ress.Resp != ""))
                {
                    
                    return View(response.Data);
                }
                else
                {
                    TempData["response"] = "No pending order found.";
                    return View("0");
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public IActionResult CurrentOrderBySaleRep(int ID)
        {
            try
            {
                SResponse ress = RequestSender.Instance.CallAPI("api", "Order/GetAllPendingOrders", "GET");
                ResponseBack<List<OrderResponseDto>> response = JsonConvert.DeserializeObject<ResponseBack<List<OrderResponseDto>>>(ress.Resp);
                List<OrderResponseDto> temp = new List<OrderResponseDto>();
                if (response.Data.Count>0)
                {
                    if(ID==0)
                    {
                        return View(response.Data);
                    }
                    else
                    {
                        for (int i = 0; i < response.Data.Count; i++)
                        {
                            if (response.Data[i].SaleRepId == ID)
                            {
                                temp.Add(response.Data[i]);
                            }
                        }
                        return View(temp);
                    }
                   
                }
                else
                {
                    TempData["response"] = "No pending order found.";
                    return View("0");
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public IActionResult CompltedOrder()
        {
            try
            {
                SResponse ress = RequestSender.Instance.CallAPI("api", "Order/GetAllDeliveredOrders", "GET");
                ResponseBack<List<OrderResponseDto>> response = JsonConvert.DeserializeObject<ResponseBack<List<OrderResponseDto>>>(ress.Resp);
                if (ress.Status && (ress.Resp != null) && (ress.Resp != ""))
                {
                    //List<OrderResponseDto> responseObject = response.Data;
                    return View(response.Data);
                }
                else
                {
                    TempData["response"] = "No pending order found.";
                    return View("0");
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }       
        public IActionResult CompleteOrderBySaleRep(int ID)
        {
            try
            {
                SResponse ress = RequestSender.Instance.CallAPI("api", "Order/GetAllDeliveredOrders", "GET");
                ResponseBack<List<OrderResponseDto>> response = JsonConvert.DeserializeObject<ResponseBack<List<OrderResponseDto>>>(ress.Resp);
                List<OrderResponseDto> temp = new List<OrderResponseDto>();
                if (response.Data.Count > 0)
                {
                    if (ID == 0)
                    {
                        return View(response.Data);
                    }
                    else
                    {
                        for (int i = 0; i < response.Data.Count; i++)
                        {
                            if (response.Data[i].SaleRepId == ID)
                            {
                                temp.Add(response.Data[i]);
                            }
                        }
                        return View(temp);
                    }

                }
                else
                {
                    TempData["response"] = "No pending order found.";
                    return View("0");
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public IActionResult BroadcastMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BroadcastMessage(Broadcast broadcast)
        {

            try
            {

                var files = HttpContext.Request.Form.Files;
                foreach (var file in files)
                {
                    if (file.Name != "")
                    {
                        string path = this._env.WebRootPath + "\\Broadcastimages\\" + file.FileName;
                        var fileExtension = Path.GetExtension(file.FileName);
                        var fileName = string.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                        using (var fs = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(fs);

                        }

                        broadcast.ImagePath = path;
                        broadcast.Image = fileName;
                    }
                }

                var body = JsonConvert.SerializeObject(broadcast);
                SResponse resp = RequestSender.Instance.CallAPI("api", "Product/BroadCastMessage", "POST", body);
                if (resp.Status && (resp.Resp != null) && (resp.Resp != ""))
                {
                    TempData["success"] = "BroadcastMessage Added Successfully";
                    return RedirectToAction("BroadCastList");
                }
                else
                {
                    TempData["error"] = resp.Resp + " Unable To Add.";
                    return RedirectToAction("BroadcastMessage");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Unable to Add." + ex.Message;
                return RedirectToAction("BroadcastMessage");
            }
        }

        [HttpGet]
        public IActionResult BroadCastList()
        {
            try
            {
                SResponse ress = RequestSender.Instance.CallAPI("api", "Product/BroadcastGet", "GET");
                if (ress.Status && (ress.Resp != null) && (ress.Resp != ""))
                {
                    ResponseBack<List<Broadcast>> response =
                                JsonConvert.DeserializeObject<ResponseBack<List<Broadcast>>>(ress.Resp);
                    if (response.Message == "Success")
                    {
                        return View(response.Data);
                    }
                    else
                    {
                        TempData["error"] = "Server is down.";
                    }
                }
                return View();


            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public IActionResult Payments()
        {
            return View();
        }
    }
}
