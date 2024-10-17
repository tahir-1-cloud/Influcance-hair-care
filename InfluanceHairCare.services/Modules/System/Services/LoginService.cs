using InfluanceHairCare.models.DataContext;
using InfluanceHairCare.services.Modules.System.Dtos;
using InfluanceHairCare.services.Modules.System.Interfaces;
using InfluanceHairCare.utilities.Common;
using InfluanceHairCare.utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using static InfluanceHairCare.utilities.Enums.ApplicationEnums;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using InfluanceHairCare.models;

namespace InfluanceHairCare.services.Modules.System.Services
{
    public class LoginService : ILoginInterface
    {
        private readonly ApplicationDataContext _context;
        private const string secretKey = "this_is_my_case_secret-Key-for-token_generation";
        public LoginService(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task<GenericResponse<LoginResponseDto>> AuthenticateUser(LoginRequestDto loginRequest)
        {
            var response = new LoginResponseDto();
            var result = await _context.Logins.Where(x => x.Email.Equals(loginRequest.Email)
                         && x.Password.Equals(loginRequest.Password)).FirstOrDefaultAsync();

            if (!(result is null))
            {
                if (result.RoleId == Convert.ToInt32(RolesEnum.Customer))
                {
                    var customer = await _context.Customers.FirstOrDefaultAsync(x => x.LoginId == result.LoginId);
                    if (!(customer is null))
                    {
                        response.Email = result.Email;
                        response.IsActive = result.IsActive;
                        response.FirstName = customer.FirstName;
                        response.LastName = customer.LastName;
                        response.Id = customer.Id;
                        response.Address = customer.Address;
                        response.City = customer.City;
                        response.Salon_Name = customer.Salon_Name;
                        response.Location = customer.Location;
                        response.Phone = customer.Phone;
                        response.State = customer.State;
                        response.ImageName = customer.CustomerImage;
                        response.ImagePath = customer.CustomerImagePath;
                        response.RoleId = Convert.ToInt32(RolesEnum.Customer);
                        response.Token = GenerateToken(response);
                    }
                }
                if (result.RoleId == Convert.ToInt32(RolesEnum.SaleRep))
                {
                    var saleRep = await _context.SaleReps.FirstOrDefaultAsync(x => x.LoginId == result.LoginId);
                    if (!(saleRep is null))
                    {
                        response.Email = result.Email;
                        response.IsActive = result.IsActive;
                        response.FirstName = saleRep.FirstName;
                        response.LastName = saleRep.LastName;
                        response.Id = saleRep.Id;
                        response.Address = saleRep.Address;
                        response.City = saleRep.City;
                        response.Location = saleRep.Location;
                        response.Phone = saleRep.Phone;
                        response.State = saleRep.State;
                        response.Discount = saleRep.Discount;
                        response.Rating = saleRep.Rating;
                        response.ImageName = saleRep.SaleRepImage;
                        response.ImagePath = saleRep.SaleRepImagePath;
                        response.RoleId = Convert.ToInt32(RolesEnum.SaleRep);
                        response.Token = GenerateToken(response);
                    }
                }
                if (result.RoleId == Convert.ToInt32(RolesEnum.Admin))
                {
                    var users = await _context.Customers.FirstOrDefaultAsync(x => x.LoginId == result.LoginId);
                    if (!(users is null))
                    {
                        response.Email = result.Email;
                        response.IsActive = result.IsActive;
                        response.FirstName = users.FirstName;
                        response.LastName = users.LastName;
                        response.Token = GenerateToken(response);
                    }
                }

                return new GenericResponse<LoginResponseDto>()
                { StatusCode = HttpStatusCode.OK, Data = response };
            }
            else
            {
                return new GenericResponse<LoginResponseDto>()
                { StatusCode = HttpStatusCode.NotFound, Message = "Failed"};
            }
        }
        private string GenerateToken(LoginResponseDto responseDto)
        {
            var claims = new[]
                   {
                       new Claim("Email", responseDto.Email),
                       new Claim("Role", responseDto.RoleId.ToString()),
                       new Claim("UserId", responseDto.FirstName)
                   };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
