using InfluanceHairCare.services.Modules.Customers.Dtos;
using InfluanceHairCare.services.Modules.System.Dtos;
using InfluanceHairCare.services.Modules.System.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InfluanceHairCare.api.Controllers
{
    [AllowAnonymous]
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginInterface _login;
       
        public LoginController(ILoginInterface login)
        {
            _login = login;
        }

        [AllowAnonymous]
        [Route("authUser")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto requestDto)
        {
            var response = await _login.AuthenticateUser(requestDto);
            return Ok(response);
        }
        //[AllowAnonymous]
        //[HttpPost]
        //[Route("Register-User")]
        //public async Task<IActionResult> RegisterCustomer(CustomerRegisterDto custObject)
        //{
           
        //}
    }
}
