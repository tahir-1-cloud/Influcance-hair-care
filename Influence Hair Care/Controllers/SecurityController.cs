using Influence_Hair_Care.Models;
using Influence_Hair_Care.Models.Data;
using Influence_Hair_Care.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using static Influence_Hair_Care.Models.Data.RequestSender;

namespace Influence_Hair_Care.Controllers
{
    public class SecurityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {

            return View();

        }

        [HttpPost]
        public IActionResult Login(LoginRequestDto data)
        {
            try
            {
                var body = JsonConvert.SerializeObject(data);
                SResponse ress = RequestSender.Instance.CallAPI("api", "login/authUser", "POST", body);
                ResponseBack<LoginDto> response = JsonConvert.DeserializeObject<ResponseBack<LoginDto>>(ress.Resp);
                if (ress.Status && (ress.Resp != null) && (ress.Resp != "") && response.Data != null)
                {
                    TempData["success"] = "Login Successfully!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["error"] = "Username or password is incorrect!";
                    return RedirectToAction("Login", "Security");
                }

                
            }
            catch (System.Exception)
            {

                throw;
                //return View();
            }

        }
    }
}
