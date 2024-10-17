using Grpc.Core;
using InfluanceHairCare.models;
using Influence_Hair_Care.Models.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Influence_Hair_Care.Models.Data.RequestSender;

namespace Influence_Hair_Care.Controllers
{
    public class ProductController : Controller
    {
        private IWebHostEnvironment _env;
        public ProductController(IWebHostEnvironment env)
        {
            _env = env;
        }

      
        [HttpGet]
        public IActionResult ProductsList()
        {
            try
            {
                SResponse ress = RequestSender.Instance.CallAPI("api", "Product/ProductGet", "GET");
                if (ress.Status && (ress.Resp != null) && (ress.Resp != ""))
                {
                    ResponseBack<List<Products>> response =
                                JsonConvert.DeserializeObject<ResponseBack<List<Products>>>(ress.Resp);
                    if (response.Message == "Success")
                    {
                        List<Products> responseObject = response.Data;
                        return View(responseObject);
                    }
                    else
                    {
                        TempData["response"] = "Server is down.";
                    }
                }
                return View();


            }
            catch (System.Exception)
            {

                throw;
            }
        }
     

        public IActionResult ProductAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProductAdd(Products data)
        {

            try
            {
                
                var files = HttpContext.Request.Form.Files;
                foreach (var file in files)
                {
                    if (file.Name != "")
                    {
                        string path = this._env.WebRootPath + "\\productImages\\" + file.FileName;
                       // string path = _env.WebRootPath +"\\"+ file.FileName;
                        var fileExtension = Path.GetExtension(file.FileName);
                        var fileName = string.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                        using (var fs = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(fs);

                        }

                        data.ProductImagePath = path;
                        data.ProductImage = fileName;
                    }
                }
                data.Discount = 0;
                var body = JsonConvert.SerializeObject(data);
                SResponse resp = RequestSender.Instance.CallAPI("api","Product/AddProduct", "POST", body);
                if (resp.Status && (resp.Resp != null) && (resp.Resp != ""))
                {
                    TempData["success"] = "Product Addedd Successfully!";
                    return RedirectToAction("ProductsList");
                }
                else
                {
                    TempData["error"] = _env.WebRootPath +" res="+ resp.Resp + " " + "Unable To Add.";
                    return View("ProductAdd"); 
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Unable to Add." + ex.Message;
                return View("ProductAdd");
            }
        }


        [HttpGet]
        public IActionResult productDelete(int? id)
        {

            try
            {
                SResponse ress = RequestSender.Instance.CallAPI("api","Product/DeleteProduct" + "/" + id, "Get");
                if (ress.Status && (ress.Resp != null) && (ress.Resp != ""))
                {
                    var response = JsonConvert.DeserializeObject<ResponseBack<Products>>(ress.Resp);
                    TempData["response"] = "Delete Successfully";
                    return RedirectToAction("ProductsList");
                }
                return Json(null);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IActionResult UpdateProduct()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            try
            {
                SResponse ress = RequestSender.Instance.CallAPI("api",
             "Product/ProductUpdateById" + "/" + id, "GET");
                if (ress.Status && (ress.Resp != null) && (ress.Resp != ""))
                {
                    var response = JsonConvert.DeserializeObject<Products>(ress.Resp);
                    return View(response);
                }
                return Json(null);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult UpdateDataProduct(Products data)
        {
            try
            {
                var body = JsonConvert.SerializeObject(data);
                SResponse resp = RequestSender.Instance.CallAPI("api", "Product/Productdataupdate", "POST", body);
                if (resp.Status && (resp.Resp != null) && (resp.Resp != ""))
                {
                    TempData["success"] = "Product Updated Successfully!";
                    return RedirectToAction("ProductsList");
                }
                else
                {
                    TempData["error"] = resp.Resp + " " + "Unable To Add.";
                    return RedirectToAction("UpdateProduct");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Unable to Add." + ex.Message;
                return RedirectToAction("FaqsGet");
            }
        }
    }
}
