using EllipticCurve.Utils;
using InfluanceHairCare.models;
using InfluanceHairCare.models.DataContext;
using InfluanceHairCare.services.Modules.Customers.Dtos;
using InfluanceHairCare.services.Modules.Customers.Interfaces;
using InfluanceHairCare.services.Modules.Order.Dtos;
using InfluanceHairCare.services.Modules.System.Dtos;
using InfluanceHairCare.Shared.DataConfig;
using InfluanceHairCare.utilities.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static InfluanceHairCare.utilities.Enums.ApplicationEnums;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using InfluanceHairCare.services.Modules.CustomerFavoriteProducts.Dtos;

namespace InfluanceHairCare.services.Modules.Customers.Services
{
    public class CustomerServices : ICustomerInterface
    {
        private readonly ApplicationDataContext _db;
      
        public CustomerServices(ApplicationDataContext db)
        {
            _db = db;
            
        }

        public async Task<GenericResponse<List<CustomerSaleRepDto>>> AllCustomerWithSaleRep()
        {
            try
            {

                List<CustomerSaleRepDto> CustomersaleRepResponse = new List<CustomerSaleRepDto>();
                CustomersaleRepResponse = await (from customer in _db.Customers
                                         join lgn in _db.Logins on customer.LoginId equals lgn.LoginId 
                                         join sale in _db.SaleReps on customer.SaleRepID equals sale.Id
                                         select new CustomerSaleRepDto { Id = customer.Id, FirstName = customer.FirstName}).ToListAsync();

                //foreach (var user in CustomersaleRepResponse)
                //{
                //    user.CustomerOrderCount = await (from order in _db.Orders
                //                              join cust in _db.Customers on order.CustomerId equals cust.Id
                //                              where cust.SaleRepID == user.Id && order.Status == "Pending"
                //                              select order).CountAsync();
                //    user.CustomerOrderCount = await _db.Customers.Where(x => x.SaleRepID == user.Id).CountAsync();

                //}
                //var users1 = await _db.Customers.
                return new GenericResponse<List<CustomerSaleRepDto>>()
                { StatusCode = HttpStatusCode.OK, Data = CustomersaleRepResponse };
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<GenericResponse<List<Customer>>> GetAllCustomers()
        {
            try
            { 
              var users = await _db.Customers.Include(log=>log.Login).OrderByDescending(x=>x.Id).ToListAsync();
                //var users1 = await _db.Customers.
                return new GenericResponse<List<Customer>>()
                { StatusCode = HttpStatusCode.OK, Data = users };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GenericResponse<List<Customer>>> GetCustomersWithOutSaleRep()
        {
            try
            {
                var users = await _db.Customers.Include(log => log.Login).OrderByDescending(x => x.Id).Where(x=>x.SaleRepID == null).ToListAsync();
                //var users1 = await _db.Customers.
                return new GenericResponse<List<Customer>>()
                { StatusCode = HttpStatusCode.OK, Data = users };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<GenericResponse<CustomerFavoriteProductBaseDto>> AddToFavorite(CustomerFavoriteProductBaseDto df)
        {
            try
            {
                var res = _db.CustomerfavoritesProducts.Any(x=>x.CustomerId == df.CustomerId && x.ProductId == df.ProductId);
                if (df.Status == true && res)
                {
                    return new GenericResponse<CustomerFavoriteProductBaseDto>()
                    { StatusCode = HttpStatusCode.Conflict, Message = "Record Already Exist" };
                }
                if (!res)
                {
                    var fvrt = new CustomerfavoriteProducts();
                    fvrt.CustomerId = df.CustomerId;
                    fvrt.ProductId = df.ProductId;
                    fvrt.DateTime = df.DateTime;
                    await _db.CustomerfavoritesProducts.AddAsync(fvrt);
                    await _db.SaveChangesAsync();
                    //var users1 = await _db.Customers.
                    return new GenericResponse<CustomerFavoriteProductBaseDto>()
                    { StatusCode = HttpStatusCode.OK, Message = "Add to favorite Successfully" };
                }
                else
                {
                    _db.Remove(res);
                    await _db.SaveChangesAsync();
                    return new GenericResponse<CustomerFavoriteProductBaseDto>()
                    { StatusCode = HttpStatusCode.OK, Message = "Remove Successfully" };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GenericResponse<CustomerFavoriteProductBaseDto>> RemoveFromFavorite(CustomerFavoriteProductBaseDto df)
        {
            try
            {
                var res = await _db.CustomerfavoritesProducts.Where(x => x.CustomerId == df.CustomerId && x.ProductId == df.ProductId).FirstOrDefaultAsync();
                if (res != null)
                {
                    _db.Remove(res);
                    await _db.SaveChangesAsync();
                    return new GenericResponse<CustomerFavoriteProductBaseDto>()
                    { StatusCode = HttpStatusCode.OK, Message = "Remove Successfully" };
                }
                else
                {
                    return new GenericResponse<CustomerFavoriteProductBaseDto>()
                    { StatusCode = HttpStatusCode.NotFound, Message = "Favorite Product not found" };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GenericResponse<List<OrderProductBaseDto>>> GetCustomerFavoriteProducts(int CustomerId)
        {
            try
            {
                    var FavoriteProducts = await (from op in _db.CustomerfavoritesProducts
                                                   join p in _db.Products on op.ProductId equals p.ProductId
                                                   where op.CustomerId == CustomerId
                                                   orderby op.DateTime descending
                                                   select new OrderProductBaseDto { ProductId = op.ProductId, ProductName = p.ProductName, ProductDescription = p.Discription, ProductImage = p.ProductImage, ProductImagePath = p.ProductImagePath, TotalPrice = (float)p.price,  Discount = (float)p.Discount, Price = (float)p.RegularPrice}).ToListAsync();

                return new GenericResponse<List<OrderProductBaseDto>>()
                { StatusCode = HttpStatusCode.OK, Data = FavoriteProducts };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<GenericResponse<CustomerBalanceDto>> GetCustomerAccountBalance(int customerId)
        {
            try
            {
                var cust = await _db.Customers.Where(x => x.Id == customerId).Select(x => new CustomerBalanceDto
                {
                    AccountBalance = x.AccountBalance,
                    CreditLimit = x.CreditLimit
                }).FirstOrDefaultAsync();

                return new GenericResponse<CustomerBalanceDto>()
                { StatusCode = HttpStatusCode.OK, Data = cust };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GenericResponse<CustomersBaseDto>> UpdateCustomerSaleRep(int cId, int sId, int creditLimit)
        {
            try
            {
                if (cId != 0)
                {
                    var cust = await _db.Customers.FirstOrDefaultAsync(x => x.Id == cId);
                    if (cust != null)
                    {
                        if (sId != 0)
                        {
                            cust.SaleRepID = sId;
                        }
                        if (creditLimit > 0)
                        {
                            cust.CreditLimit = creditLimit;
                        }
                        _db.Entry(cust).State = EntityState.Modified;
                        await _db.SaveChangesAsync();


                        return new GenericResponse<CustomersBaseDto>()
                        { StatusCode = HttpStatusCode.OK, Message = "Added Successfully" };
                    }
                    else
                    {
                        return new GenericResponse<CustomersBaseDto>()
                        { StatusCode = HttpStatusCode.NotFound, Message = "Customer Not Found" };
                    }
                }
                else
                {
                    return new GenericResponse<CustomersBaseDto>()
                    { StatusCode = HttpStatusCode.NotFound, Message = "Error in updation" };
                }


            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<GenericResponse<CustomerBalanceDto>> UpdateCustomerAccountBalance(CustomerBalanceDto blnc)
        {
            try
            {
                if (blnc != null)
                {
                    var cust = await _db.Customers.FirstOrDefaultAsync(x => x.Id == blnc.CustomerId);
                    if (cust != null)
                    {
                        if ((cust.CreditLimit + blnc.AccountBalance) < 0 && blnc.AccountBalance < 0)
                        {
                            return new GenericResponse<CustomerBalanceDto>()
                            { StatusCode = HttpStatusCode.PaymentRequired, Message = "Account Balance cannot be less than credit limit!" };
                        }
                        if (blnc.AccountBalance != 0)
                        {
                            cust.AccountBalance = blnc.AccountBalance;
                        }
                        if (blnc.CreditLimit != 0)
                        {
                            cust.CreditLimit = blnc.CreditLimit;
                        }

                        _db.Entry(cust).State = EntityState.Modified;
                        await _db.SaveChangesAsync();


                        return new GenericResponse<CustomerBalanceDto>()
                        { StatusCode = HttpStatusCode.OK };
                    }
                    else
                    {
                        return new GenericResponse<CustomerBalanceDto>()
                        { StatusCode = HttpStatusCode.NotFound, Message = "Customer Not Found" };
                    }
                }
                else
                {
                    return new GenericResponse<CustomerBalanceDto>()
                    { StatusCode = HttpStatusCode.NotFound, Message="Error in updation" };
                }

                
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<GenericResponse<CustomerRegisterDto>> RegisterCustomer(CustomerRegisterDto custObject)
        {
            try
            {

                var userExists = await _db.Logins.FirstOrDefaultAsync(x => x.Email == custObject.Email);
                if (userExists == null)
                {
                    Login lgn = new Login();
                    lgn.Email = custObject.Email;
                    lgn.Password = custObject.Password;
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
                    cust.LoginId = lgn.LoginId;
                    cust.CustomerImage =custObject.CustomerImage;
                    //cust.CreditLimit = 50;
                    cust.CustomerImagePath = custObject.CustomerImagePath;
                    
                    await _db.Customers.AddAsync(cust);
                    await _db.SaveChangesAsync();



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

    }

}
