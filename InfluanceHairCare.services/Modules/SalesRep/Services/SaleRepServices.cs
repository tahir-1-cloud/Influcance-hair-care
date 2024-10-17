using InfluanceHairCare.models;
using InfluanceHairCare.models.DataContext;
using InfluanceHairCare.services.Modules.Customers.Dtos;
using InfluanceHairCare.services.Modules.Order.Dtos;
using InfluanceHairCare.services.Modules.SalesRep.Dtos;
using InfluanceHairCare.services.Modules.SalesRep.Interfaces;
using InfluanceHairCare.utilities.Common;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using static InfluanceHairCare.utilities.Enums.ApplicationEnums;
using MimeKit;


namespace InfluanceHairCare.services.Modules.SalesRep.Services
{
    public class SaleRepServices : ISaleRepInterface
    {
        private readonly ApplicationDataContext _db;
        private static Random random = new Random();
        public SaleRepServices(ApplicationDataContext db)
        {
            _db = db;
        }

        public async Task<GenericResponse<List<Customer>>> GetSaleRepCustomers(int id)
        {
            try
            {       
                var users = await _db.Customers.Where(x=>x.SaleRepID==id).OrderByDescending(x=>x.Id).ToListAsync();
                //var users1 = await _db.Customers.
                return new GenericResponse<List<Customer>>()
                { StatusCode = HttpStatusCode.OK, Data = users };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<GenericResponse<List<SaleRepResponseDto>>> AllSaleReps()
        {
            try
            {
                List<SaleRepResponseDto> saleRepResponse = new List<SaleRepResponseDto>();
                saleRepResponse = await (from sale in _db.SaleReps
                                         join lgn in _db.Logins on sale.LoginId equals lgn.LoginId
                                         select new SaleRepResponseDto { Id = sale.Id, FirstName = sale.FirstName, LastName = sale.LastName, Rating = sale.Rating, City = sale.City, State = sale.State, Location = sale.Location, Discount = sale.Discount, Address = sale.Address, Phone = sale.Phone, Status = sale.Status, SaleRepImagePath = sale.SaleRepImagePath, SaleRepImage= sale.SaleRepImage , Email = lgn.Email}).ToListAsync();

                foreach(var user in saleRepResponse)
                {
                    user.OrdersCount = await (from order in _db.Orders
                                   join cust in _db.Customers on order.CustomerId equals cust.Id
                                   where cust.SaleRepID == user.Id && order.Status == "Pending"
                                   select order).CountAsync();
                    user.CustomerCount = await _db.Customers.Where(x => x.SaleRepID == user.Id).CountAsync();

                }
                //var users1 = await _db.Customers.
                return new GenericResponse<List<SaleRepResponseDto>>()
                { StatusCode = HttpStatusCode.OK, Data = saleRepResponse };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<GenericResponse<List<SaleRepBaseDto>>> SaleRepWithOrderSatus(string status)
        {
            List<SaleRepBaseDto> saleRepResponse = new List<SaleRepBaseDto>();

            saleRepResponse = await (from order in _db.Orders
                                     join cus in _db.Customers on order.CustomerId equals cus.Id
                                     join sale in _db.SaleReps on cus.SaleRepID equals sale.Id
                                     join lgn in _db.Logins on sale.LoginId equals lgn.LoginId
                                     where order.Status == status
                                     select new SaleRepBaseDto { Id = sale.Id, FirstName = sale.FirstName + "_" + sale.LastName }).Distinct().ToListAsync();

            
            return new GenericResponse<List<SaleRepBaseDto>>()
            { StatusCode = HttpStatusCode.OK, Data = saleRepResponse };

        }

        public async Task<GenericResponse<List<SaleRepReviewDto>>> GetSaleRepReviews(int saleRepId)
        {
            try
            {
                List<SaleRepReviewDto> saleRepResponse = new List<SaleRepReviewDto>();
                //saleRepResponse = await (from sale in _db.SaleReps
                //                         join cu in _db.Customers on sale.Id equals cu.SaleRepID
                //                         join or in _db.Orders on cu.Id equals or.CustomerId
                //                         join rw in _db.ProductRatings on or.OrderId equals rw.OrderId
                //                         select new SaleRepReviewDto { CustomerName = cu.FirstName +"_"+cu.LastName, Rating = (float)rw.Rating, CustomerComment=rw.Comments, RatingDate = rw.RatingDate, OverAllRating = (float)sale.Rating, CustomerImageUrl = cu.CustomerImagePath, OrderAmount=or.GrandTotal}).ToListAsync();

                saleRepResponse = await (from rw in _db.ProductRatings
                                         join or in _db.Orders on rw.OrderId equals or.OrderId
                                         join cu in _db.Customers on or.CustomerId equals cu.Id
                                         join sale in _db.SaleReps on cu.SaleRepID equals sale.Id
                                         where rw.SaleRepId == saleRepId
                                         orderby rw.RatingId descending
                                         select new SaleRepReviewDto { CustomerName = cu.FirstName + "_" + cu.LastName, Rating = (float)rw.Rating, CustomerComment = rw.Comments, RatingDate = rw.RatingDate, OverAllRating = (float)sale.Rating, CustomerImageUrl = cu.CustomerImagePath, OrderAmount = or.GrandTotal }).ToListAsync();

                //var users1 = await _db.Customers.
                return new GenericResponse<List<SaleRepReviewDto>>()
                { StatusCode = HttpStatusCode.OK, Data = saleRepResponse };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GenericResponse<SaleRepRegisterDto>> RegisterSaleRep(SaleRepRegisterDto saleObject)
        {
            try
            {

                var userExists = await _db.Logins.FirstOrDefaultAsync(x => x.Email == saleObject.Email);
                if (userExists == null)
                {
                    Login lgn = new Login();
                    lgn.Email = saleObject.Email;
                    lgn.Password = saleObject.Password;
                    lgn.RoleId = Convert.ToInt32(RolesEnum.SaleRep);
                    lgn.IsActive = true;

                    await _db.Logins.AddAsync(lgn);
                    await _db.SaveChangesAsync();

                    SaleRep sale = new SaleRep();
                    sale.FirstName = saleObject.FirstName;
                    sale.LastName = saleObject.LastName;
                    sale.Status = saleObject.Status;
                    sale.State = saleObject.State;
                    sale.Discount = saleObject.Discount;
                    sale.Address = saleObject.Address;
                    sale.City = saleObject.City;    
                    sale.Location = saleObject.Location;
                    sale.Rating = saleObject.Rating;
                    sale.Phone = saleObject.Phone;
                    sale.LoginId = lgn.LoginId;

                     await _db.SaleReps.AddAsync(sale);
                     await _db.SaveChangesAsync();

                    return new GenericResponse<SaleRepRegisterDto>()
                    { StatusCode = HttpStatusCode.OK, Message = "SaleRep created successfully!" };
                }
                else
                {
                    return new GenericResponse<SaleRepRegisterDto>()
                    { StatusCode = HttpStatusCode.OK, Message = "Record Already Exists" };
                }

            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<GenericResponse<CustomerRegisterDto>> RegisterCustomerBySaleRep(CustomerRegisterDto custObject)
        {
            try
            {
                string password = RandomString(10);
                var userExists = await _db.Logins.FirstOrDefaultAsync(x => x.Email == custObject.Email);
                if (userExists == null)
                {
                    Login lgn = new Login();
                    lgn.Email = custObject.Email;
                    lgn.Password = password;
                    lgn.RoleId = Convert.ToInt32(RolesEnum.Customer);
                    lgn.IsActive = true;
                    await _db.Logins.AddAsync(lgn);
                    await _db.SaveChangesAsync();

                    Customer cust = new Customer();
                    cust.FirstName = custObject.FirstName;
                    cust.LastName = custObject.LastName;
                    cust.Salon_Name = custObject.Salon_Name;
                    cust.Address = custObject.Address;
                    cust.State = custObject.State;
                    cust.City = custObject.City;
                    cust.Location = custObject.Location;
                    cust.Phone = custObject.Phone;
                    //cust.CreditLimit = custObject.CreditLimit;
                    cust.CreditLimit = 50;
                    cust.AccountBalance = custObject.AccountBalance;
                    cust.CustomerImage = custObject.CustomerImage;
                    cust.CustomerImagePath = custObject.CustomerImagePath;
                    cust.SaleRepID = custObject.SaleRepId;
                    cust.LoginId = lgn.LoginId;
                    await _db.Customers.AddAsync(cust);
                    await _db.SaveChangesAsync();

                    Execute(password,custObject.Email).Wait();

                    return new GenericResponse<CustomerRegisterDto>()
                    { StatusCode = HttpStatusCode.OK, Message = "Customer created successfully!"};
                }
                else
                {
                    return new GenericResponse<CustomerRegisterDto>()
                    { StatusCode = HttpStatusCode.OK, Message = "Record Already Exists"};
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+<,>.";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        static async Task Execute(string password, string email)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("Murtaza.haider@ab-sol.net", "InfluanceHairCare");
            var subject = "Password of influance App";
            var to = new EmailAddress(email, "testing");
            var plainTextContent = "Hi, your influance Account created successfully";
            var htmlContent = "Hi, your influance Account created successfully<br/><strong>Email:</strong>" + email+ "<br/><strong>Password:</strong>"+password;
            //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            //var message = new MimeMessage();
            //message.From.Add(new MailboxAddress("Murtaza", "murtazainfluance@gmail.com"));
            //message.To.Add(new MailboxAddress("tester", "murtazainfluance@gmail.com"));
            ////message.To.Add(MailboxAddress.Parse("Murtaza.haider@ab-sol.net"));
            //message.Subject = "Influance Account Password";
            //message.Body = new TextPart("plain")
            //{
            //    Text = "This is a test email sent using MailKit!"
            //};

            //// Set up the SMTP client
            //using (var client = new SmtpClient())
            //{
            //    client.Connect("smtp.gmail.com", 587, false);
            //    client.Authenticate("murtazainfluance@gmail.com", "audionic#5");

            //    // Send the email
            //    client.Send(message);

            //    client.Disconnect(true);
            //    client.Dispose();
            //}

        }
    }
}
