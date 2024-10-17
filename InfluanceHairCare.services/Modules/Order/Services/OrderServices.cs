using InfluanceHairCare.models.DataContext;
using InfluanceHairCare.models;
using InfluanceHairCare.utilities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfluanceHairCare.services.Modules.Customers.Dtos;
using Microsoft.EntityFrameworkCore;
using static InfluanceHairCare.utilities.Enums.ApplicationEnums;
using System.Net;
using InfluanceHairCare.services.Modules.Order.Dtos;
using InfluanceHairCare.services.Modules.Order.Interfaces;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using InfluanceHairCare.services.Modules.SaleRepsOrderPayments.Dtos;

namespace InfluanceHairCare.services.Modules.Order.Services
{
    public class OrderServices : IOrderInterface
    {
        private readonly ApplicationDataContext _db;

        public OrderServices(ApplicationDataContext db)
        {
            _db = db;
        }

        public async Task<GenericResponse<List<OrderResponseDto>>> AllPendingOrders()
        {
            try
            {

                var s = await (from order in _db.Orders
                               join cust in _db.Customers on order.CustomerId equals cust.Id
                               join sale in _db.SaleReps on cust.SaleRepID equals sale.Id
                               where order.Status == "Pending"
                               select new OrderResponseDto { OrderId = order.OrderId, SaleRepId = sale.Id, OrderPendingPayment = order.OrderPendingPayment, OrderPaidAmount = order.OrderPaidAmount, DateTime = order.DateTime, TotalPrice = order.TotalPrice, Discount = order.Discount, GrandTotal = order.GrandTotal, OrderBy = order.OrderBy, Status = order.Status, FirstName = cust.FirstName + "_" + cust.LastName, Address = cust.Address, CustomerImage = cust.CustomerImage, CustomerImagePath = cust.CustomerImagePath, Salon_Name = cust.Salon_Name, Location = cust.Location, SaleRepName = sale.FirstName + "_" + sale.LastName, Rating = sale.Rating }).ToListAsync();
                foreach (var res in s)
                {
                    res.OrderProducts = await (from op in _db.OrderProducts
                                               join p in _db.Products on op.ProductId equals p.ProductId
                                               where op.OrderId == res.OrderId
                                               select new OrderProductBaseDto { ProductId = op.ProductId, Price = op.Price, Discount = op.Discount, TotalCost = op.TotalCost, Quantity = op.Quantity, TotalPrice = op.TotalPrice, ProductName = p.ProductName, ProductDescription = p.Discription, ProductImage = p.ProductImage, ProductImagePath = p.ProductImagePath }).ToListAsync();
                    res.OrderPayment = await _db.OrderPayments.Where(x => x.OrdersId == res.OrderId).Select(x => new OrderPaymentBaseDto
                    {
                        PaymentMethod = x.PaymentMethod,
                        PaymentAmount = x.PaymentAmount
                    }).ToListAsync();

                }
                //var users1 = await _db.Customers.
                return new GenericResponse<List<OrderResponseDto>>()
                { StatusCode = HttpStatusCode.OK, Data = s };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GenericResponse<List<OrderResponseDto>>> AllDeliveredOrders()
        {
            try
            {

                var s = await (from order in _db.Orders
                               join cust in _db.Customers on order.CustomerId equals cust.Id
                               join sale in _db.SaleReps on cust.SaleRepID equals sale.Id
                               where order.Status == "Delivered"
                               select new OrderResponseDto { OrderId = order.OrderId, SaleRepId = sale.Id, OrderPendingPayment = order.OrderPendingPayment, OrderPaidAmount = order.OrderPaidAmount, DateTime = order.DateTime, TotalPrice = order.TotalPrice, Discount = order.Discount, GrandTotal = order.GrandTotal, OrderBy = order.OrderBy, Status = order.Status, FirstName = cust.FirstName + "_" + cust.LastName, Address = cust.Address, CustomerImage = cust.CustomerImage, CustomerImagePath = cust.CustomerImagePath, Salon_Name = cust.Salon_Name, Location = cust.Location, SaleRepName = sale.FirstName + "_" + sale.LastName, Rating = sale.Rating }).ToListAsync();
                foreach (var res in s)
                {
                    res.OrderProducts = await (from op in _db.OrderProducts
                                               join p in _db.Products on op.ProductId equals p.ProductId
                                               where op.OrderId == res.OrderId
                                               select new OrderProductBaseDto { ProductId = op.ProductId, Price = op.Price, Discount = op.Discount, TotalCost = op.TotalCost, Quantity = op.Quantity, TotalPrice = op.TotalPrice, ProductName = p.ProductName, ProductDescription = p.Discription, ProductImage = p.ProductImage, ProductImagePath = p.ProductImagePath }).ToListAsync();
                    res.OrderPayment = await _db.OrderPayments.Where(x => x.OrdersId == res.OrderId).Select(x => new OrderPaymentBaseDto
                    {
                        PaymentMethod = x.PaymentMethod,
                        PaymentAmount = x.PaymentAmount
                    }).ToListAsync();

                }
                //var users1 = await _db.Customers.
                return new GenericResponse<List<OrderResponseDto>>()
                { StatusCode = HttpStatusCode.OK, Data = s };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<GenericResponse<List<OrderResponseDto>>> SaleRepCustomerOrders(int id)
        {
            try
            {

                var s = await (from order in _db.Orders
                               join cust in _db.Customers on order.CustomerId equals cust.Id
                               //where cust.SaleRepID == id && order.Status == "Pending" 
                               where cust.SaleRepID == id
                               //orderby order.Status == "Pending"
                               select new OrderResponseDto { OrderId = order.OrderId, DateTime = order.DateTime, TotalPrice = order.TotalPrice, Discount = order.Discount, GrandTotal = order.GrandTotal , OrderBy = order.OrderBy, OrderPendingPayment = order.OrderPendingPayment, OrderPaidAmount = order.OrderPaidAmount, Status = order.Status, FirstName = cust.FirstName+"_"+cust.LastName, Address=cust.Address, CustomerImage=cust.CustomerImage, CustomerImagePath=cust.CustomerImagePath, Salon_Name=cust.Salon_Name,Location=cust.Location }).OrderByDescending(x => x.Status == "Pending").ThenByDescending(x => x.OrderId).ToListAsync();
                foreach(var res in s)
                {
                    //res.OrderProducts = await _db.OrderProducts.Where(x => x.OrderId == res.OrderId).Select(x => new OrderProductBaseDto
                    //{
                    //    ProductId = x.ProductId,
                    //    Price = x.Price,
                    //    Discount = x.Discount,
                    //    TotalCost = x.TotalCost,
                    //    Quantity = x.Quantity,
                    //    TotalPrice = x.TotalPrice

                    //}).ToListAsync();

                    res.OrderProducts = await (from op in _db.OrderProducts
                                               join p in _db.Products on op.ProductId equals p.ProductId
                                               where op.OrderId == res.OrderId
                                               select new OrderProductBaseDto { ProductId = op.ProductId, Price = op.Price, Discount = op.Discount, TotalCost = op.TotalCost, Quantity = op.Quantity, TotalPrice = op.TotalPrice, ProductName = p.ProductName, ProductDescription = p.Discription, ProductImage = p.ProductImage, ProductImagePath = p.ProductImagePath }).ToListAsync();
                    res.OrderPayment = await _db.OrderPayments.Where(x => x.OrdersId == res.OrderId).Select(x => new OrderPaymentBaseDto
                    {
                        PaymentMethod = x.PaymentMethod,
                        PaymentAmount = x.PaymentAmount
                    }).ToListAsync();

                }
                //var users1 = await _db.Customers.
                return new GenericResponse<List<OrderResponseDto>>()
                { StatusCode = HttpStatusCode.OK, Data = s };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GenericResponse<OrderResponseDto>> OrderProductsDetail(long orderId)
        {
            try
            {
                var res = await (from order in _db.Orders
                               join cust in _db.Customers on order.CustomerId equals cust.Id
                               join sale in _db.SaleReps on cust.SaleRepID equals sale.Id
                               //where cust.SaleRepID == id && order.Status == "Pending" 
                               where order.OrderId == orderId
                               //orderby order.Status == "Pending"
                               select new OrderResponseDto { OrderId = order.OrderId, SaleRepName=sale.FirstName+"_"+sale.LastName,SaleRepId = sale.Id, DateTime = order.DateTime, TotalPrice = order.TotalPrice, Discount = order.Discount, GrandTotal = order.GrandTotal, OrderBy = order.OrderBy, OrderPendingPayment = order.OrderPendingPayment, OrderPaidAmount = order.OrderPaidAmount, Status = order.Status, FirstName = cust.FirstName + "_" + cust.LastName, Address = cust.Address, CustomerImage = cust.CustomerImage, Salon_Name = cust.Salon_Name, Location = cust.Location }).FirstOrDefaultAsync();
                if (res != null)
                {
                    res.OrderProducts = await (from op in _db.OrderProducts
                                               join p in _db.Products on op.ProductId equals p.ProductId
                                               where op.OrderId == res.OrderId
                                               select new OrderProductBaseDto { ProductId = op.ProductId, Price = op.Price, Discount = op.Discount, TotalCost = op.TotalCost, Quantity = op.Quantity, TotalPrice = op.TotalPrice, ProductName = p.ProductName, ProductDescription = p.Discription, ProductImage = p.ProductImage, ProductImagePath = p.ProductImagePath }).ToListAsync();
                    res.OrderPayment = await _db.OrderPayments.Where(x => x.OrdersId == res.OrderId).Select(x => new OrderPaymentBaseDto
                    {
                        PaymentMethod = x.PaymentMethod,
                        PaymentAmount = x.PaymentAmount
                    }).ToListAsync();
                    return new GenericResponse<OrderResponseDto>()
                    { StatusCode = HttpStatusCode.OK, Data = res };
                }
                else
                {
                    return new GenericResponse<OrderResponseDto>()
                    { StatusCode = HttpStatusCode.OK };
                }
                //var users1 = await _db.Customers.
                
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<GenericResponse<List<OrderResponseDto>>> CustomerOrders(int id)
        {
            try
            {

                var s = await (from order in _db.Orders
                               join cust in _db.Customers on order.CustomerId equals cust.Id
                               join sale in _db.SaleReps on cust.SaleRepID equals sale.Id
                               //where cust.SaleRepID == id && order.Status == "Pending" 
                               where order.CustomerId == id
                               //orderby order.OrderId descending
                               select new OrderResponseDto { OrderId = order.OrderId, OrderPendingPayment = order.OrderPendingPayment, OrderPaidAmount=order.OrderPaidAmount, DateTime = order.DateTime, TotalPrice = order.TotalPrice, Discount = order.Discount, GrandTotal = order.GrandTotal, OrderBy = order.OrderBy, Status = order.Status, FirstName = cust.FirstName, LastName = cust.LastName, Address = cust.Address, CustomerImage = cust.CustomerImage, CustomerImagePath = cust.CustomerImagePath, Salon_Name = cust.Salon_Name, Location = cust.Location, SaleRepName = sale.FirstName+"_"+sale.LastName,Rating=sale.Rating }).OrderByDescending(x=>x.Status=="Pending").ThenByDescending(x=>x.OrderId).ToListAsync();
                foreach (var res in s)
                {

                    res.OrderProducts = await (from op in _db.OrderProducts
                                               join p in _db.Products on op.ProductId equals p.ProductId
                                               where op.OrderId == res.OrderId
                                               select new OrderProductBaseDto { ProductId = op.ProductId, Price = op.Price, Discount = op.Discount, TotalCost = op.TotalCost, Quantity = op.Quantity, TotalPrice = op.TotalPrice, ProductName = p.ProductName, ProductDescription = p.Discription, ProductImage = p.ProductImage, ProductImagePath = p.ProductImagePath }).ToListAsync();
                    res.OrderPayment = await _db.OrderPayments.Where(x => x.OrdersId == res.OrderId).Select(x => new OrderPaymentBaseDto
                    {
                        PaymentMethod = x.PaymentMethod,
                        PaymentAmount = x.PaymentAmount
                    }).ToListAsync();

                }
                return new GenericResponse<List<OrderResponseDto>>()
                { StatusCode = HttpStatusCode.OK, Data = s };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GenericResponse<OrderBaseDto>> UpdateOrderStatusDelivered(SaleRepOrderPayments orderPayment)
        {
            try
            {
                
                var order = await _db.Orders.Where(x=>x.OrderId==orderPayment.OrderId).FirstOrDefaultAsync();

                List<SaleRepOrderPayments>? chk = await _db.SaleRepsOrderPayments.Where(x=>x.OrderId == orderPayment.OrderId).ToListAsync();
                if (chk != null)
                {
                    foreach (var p in chk)
                    {
                        if (p.SaleRepOrderTimeAmount)
                        {
                            p.DeliveryDate = orderPayment.DeliveryDate;
                            _db.Entry(chk).State = EntityState.Modified;
                            await _db.SaveChangesAsync();
                        }
                    }
                }
                if (order != null)
                {
                    var cust = await _db.Customers.Where(x => x.Id == order.CustomerId).FirstOrDefaultAsync();
                    if (cust != null)
                    {
                        if (orderPayment.PaidAmount != 0 || (orderPayment.PaidAmount == 0 && order.OrderPendingPayment == 0))
                        {
                            order.OrderPaidAmount = order.OrderPaidAmount + orderPayment.PaidAmount;
                            if (orderPayment.PaidAmount > order.OrderPendingPayment)
                            {
                                float addBlnc = orderPayment.PaidAmount - order.OrderPendingPayment;
                                cust.AccountBalance = cust.AccountBalance + addBlnc;
                            }
                            if (orderPayment.PaidAmount == order.OrderPendingPayment)
                            {

                            }
                            if (orderPayment.PaidAmount < order.OrderPendingPayment)
                            {
                                float addBlnc = orderPayment.PaidAmount - order.OrderPendingPayment;
                                float amount = (cust.AccountBalance + orderPayment.PaidAmount) - order.OrderPendingPayment;
                                amount = amount * -1; 
                                if (amount > cust.CreditLimit)
                                {
                                    return new GenericResponse<OrderBaseDto>()
                                    { StatusCode = HttpStatusCode.PaymentRequired, Message = "Order amount Increase by Credit Limit" };
                                }
                                else
                                {
                                    cust.AccountBalance = cust.AccountBalance + addBlnc;
                                }
                            }
                        }
                        if (orderPayment.PaidAmount == 0 && order.OrderPendingPayment > 0)
                        {
                            if (-1 * (cust.AccountBalance - order.OrderPendingPayment) > cust.CreditLimit)
                            {
                                return new GenericResponse<OrderBaseDto>()
                                { StatusCode = HttpStatusCode.PaymentRequired, Message = "Order amount Increase by Credit Limit" };
                            }
                        }
                        order.Status = "Delivered";
                        _db.Entry(order).State = EntityState.Modified;
                        await _db.SaveChangesAsync();
                        if (cust != null)
                        {
                            _db.Entry(cust).State = EntityState.Modified;
                            await _db.SaveChangesAsync();
                        }
                        
                        if (orderPayment.PaidAmount > 0)
                        {
                            await _db.SaleRepsOrderPayments.AddAsync(orderPayment);
                            await _db.SaveChangesAsync();
                        }
                        return new GenericResponse<OrderBaseDto>()
                        { StatusCode = HttpStatusCode.OK };
                    }
                    return new GenericResponse<OrderBaseDto>()
                    { StatusCode = HttpStatusCode.NotFound, Message = "Customer Not Found" };
                }
                else
                {
                    return new GenericResponse<OrderBaseDto>()
                    { StatusCode = HttpStatusCode.NotFound, Message="Record Not Found" };
                }
                //var users1 = await _db.Customers.

            }
            catch (Exception)
            {
                return new GenericResponse<OrderBaseDto>()
                { StatusCode = HttpStatusCode.NotFound, Message = "Record Not Found" };
            }
        }


        public async Task<GenericResponse<OrderRequestDto>> AddToCart(OrderRequestDto OrdProduct)
        {
            try
            {
                var order = new Orders();

                order.CustomerId = OrdProduct.CustomerId;
                order.DateTime = OrdProduct.DateTime;
                order.Discount = OrdProduct.Discount;
                order.OrderBy = OrdProduct.OrderBy;
                order.OrderPendingPayment = OrdProduct.OrderPendingPayment;
                order.OrderPaidAmount = OrdProduct.OrderPaidAmount;
                order.TotalPrice = OrdProduct.TotalPrice;
                order.GrandTotal = OrdProduct.GrandTotal;
                await _db.Orders.AddAsync(order);
                await _db.SaveChangesAsync();


               
                var orderProduct = OrdProduct.OrderProducts;
                if (orderProduct != null)
                {
                    var odrprod = new OrderProducts();
                    foreach (var prd in orderProduct)
                    {
                        odrprod.OrderId = order.OrderId;
                        odrprod.ProductId = prd.ProductId;
                        odrprod.Price = prd.Price;
                        odrprod.Quantity = prd.Quantity;
                        odrprod.TotalCost = prd.TotalCost;
                        odrprod.Discount = prd.Discount;
                        odrprod.TotalPrice = prd.TotalPrice;
                        await _db.OrderProducts.AddAsync(odrprod);
                        await _db.SaveChangesAsync();
                    }
                }
                var OrderPayment = OrdProduct.OrderPayment;
                if (OrderPayment != null)
                {
                    var pay = new OrderPayment();
                    IList<OrderPayment> py = new List<OrderPayment>();
                    foreach (var Payment in OrderPayment)
                    {

                        pay.PaymentMethod = Payment.PaymentMethod;
                        pay.PaymentAmount = Payment.PaymentAmount;
                        pay.OrdersId = order.OrderId;
                        pay.Id = 0;
                        py.Add(pay);
                        _db.OrderPayments.Add(pay);
                        _db.SaveChanges();

                        if (OrdProduct.OrderBy == 2 && Payment.PaymentMethod == "7")
                        {
                            var cheque = new SaleRepOrderPayments();
                            cheque.PaidAmount = Payment.PaymentAmount;
                            cheque.ChequeNumber = OrdProduct.ChequeNumber;
                            cheque.ChequeExpiryDate = OrdProduct.ChequeExpiryDate;
                            cheque.ChequeFor = OrdProduct.ChequeFor;
                            cheque.SaleRepOrderTimeAmount = true;
                            cheque.OrderId = order.OrderId;
                            cheque.SaleRepId = (int)await _db.Customers.Where(x => x.Id == OrdProduct.CustomerId).Select(a => a.SaleRepID).FirstOrDefaultAsync();
                            await _db.SaleRepsOrderPayments.AddAsync(cheque);
                            await _db.SaveChangesAsync();
                        }
                        if (OrdProduct.OrderBy == 2 && Payment.PaymentMethod == "1")
                        {
                            var cheque = new SaleRepOrderPayments();
                            cheque.PaidAmount = Payment.PaymentAmount;
                            cheque.SaleRepOrderTimeAmount = true;
                            cheque.OrderId = order.OrderId;
                            cheque.SaleRepId = (int)await _db.Customers.Where(x => x.Id == OrdProduct.CustomerId).Select(a => a.SaleRepID).FirstOrDefaultAsync();
                            await _db.SaleRepsOrderPayments.AddAsync(cheque);
                            await _db.SaveChangesAsync();
                        }

                    }

                }
                
                return new GenericResponse<OrderRequestDto>()
                { StatusCode = HttpStatusCode.OK, Message = "Order Placed successfully!" };
            }
            catch (Exception)
            {
                return new GenericResponse<OrderRequestDto>()
                { StatusCode = HttpStatusCode.NotImplemented, Message = "Order Not Placed successfully!"};
            }
        }


    }
}
