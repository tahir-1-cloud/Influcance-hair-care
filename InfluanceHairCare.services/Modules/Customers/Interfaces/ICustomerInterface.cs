using InfluanceHairCare.models;
using InfluanceHairCare.services.Modules.CustomerFavoriteProducts.Dtos;
using InfluanceHairCare.services.Modules.Customers.Dtos;
using InfluanceHairCare.services.Modules.Order.Dtos;
using InfluanceHairCare.utilities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.Customers.Interfaces
{
    public interface ICustomerInterface
    {
        public Task<GenericResponse<List<Customer>>> GetAllCustomers();

        public Task<GenericResponse<List<CustomerSaleRepDto>>> AllCustomerWithSaleRep();
        public Task<GenericResponse<List<OrderProductBaseDto>>> GetCustomerFavoriteProducts(int CustomerId);
        public Task<GenericResponse<CustomerFavoriteProductBaseDto>> RemoveFromFavorite(CustomerFavoriteProductBaseDto df);
        public Task<GenericResponse<CustomerFavoriteProductBaseDto>> AddToFavorite(CustomerFavoriteProductBaseDto df);
        public Task<GenericResponse<CustomerRegisterDto>> RegisterCustomer(CustomerRegisterDto custObject);
       // public Task<GenericResponse<CustomerSaleRepDto>> UpdateCustomerSaleRep(CustomerSaleRepDto CustObject);

        public Task<GenericResponse<List<Customer>>> GetCustomersWithOutSaleRep();

        public Task<GenericResponse<CustomerBalanceDto>> GetCustomerAccountBalance(int customerId);
        public Task<GenericResponse<CustomerBalanceDto>> UpdateCustomerAccountBalance(CustomerBalanceDto blnc);
        public Task<GenericResponse<CustomersBaseDto>> UpdateCustomerSaleRep(int cId, int sId, int creditLimit);
    }
}
