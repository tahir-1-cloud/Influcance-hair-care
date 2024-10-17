using Grpc.Core;
using InfluanceHairCare.models;
using InfluanceHairCare.services.Modules.Customers.Dtos;
using Influence_Hair_Care.Models.Data;
using Influence_Hair_Care.Models.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using static Influence_Hair_Care.Models.Data.RequestSender;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Influence_Hair_Care.Controllers
{
    public class CustomerController : Controller
    {
        //private IHostingEnvironment hostingEnvironment;

        private IWebHostEnvironment _env;

        public CustomerController(/*IHostingEnvironment hostingEnvironment, */IWebHostEnvironment env)
        {
            //this.hostingEnvironment = hostingEnvironment;
            _env = env;
        }

   


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UpdateCustomerSaleRep(int cId, int sId)
        {
            try
            {
                SResponse resp = RequestSender.Instance.CallAPI("api", "Customer/UpdateCustomerSaleRep?cId="+cId+"&sId="+sId, "PUT");
                if (resp.Status && (resp.Resp != null) && (resp.Resp != ""))
                {
                    TempData["success"] = "SaleRep Added Successfully";
                    //return RedirectToAction("CustomerApproval");
                    return Json("done");
                }
                else
                {
                    TempData["error"] = resp.Resp + " Unable To Add.";
                    return RedirectToAction("CustomerApproved");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Unable to Add." + ex.Message;
                return RedirectToAction("CustomerApproved");
            }
        }
        public IActionResult CustomerApproval()
        {
            try
            {
                SResponse ress = RequestSender.Instance.CallAPI("api", "Customer/AllCustomersWithoutSaleRep", "GET");
                if (ress.Status && (ress.Resp != null) && (ress.Resp != ""))
                {
                    ResponseBack<List<Customer>> response =
                                JsonConvert.DeserializeObject<ResponseBack<List<Customer>>>(ress.Resp);
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
        public IActionResult CustomerApproved()
        {
            try
            {
                SResponse ress = RequestSender.Instance.CallAPI("api", "Customer/AllCustomersWithoutSaleRep", "GET");
                if (ress.Status && (ress.Resp != null) && (ress.Resp != ""))
                {
                    ResponseBack<List<Customer>> response =
                                JsonConvert.DeserializeObject<ResponseBack<List<Customer>>>(ress.Resp);
                    if (response.Message == "Success")
                    {
                        return Json(response.Data);
                       
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

        public IActionResult CustomerSaleRep()
        {
            try
            {
                SResponse ress = RequestSender.Instance.CallAPI("api", "Customer/AllCustomersWithoutSaleRep", "GET");
                if (ress.Status && (ress.Resp != null) && (ress.Resp != ""))
                {
                    ResponseBack<List<CustomerSaleRepDto>> response =
                                JsonConvert.DeserializeObject<ResponseBack<List<CustomerSaleRepDto>>>(ress.Resp);
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
                //return View();
            }
        }
        public IActionResult Customerdata()
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
                //return View();
            }
        }





    }
}
