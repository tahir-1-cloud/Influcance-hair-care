using InfluanceHairCare.models;
using InfluanceHairCare.services.Modules.Customers.Dtos;
using InfluanceHairCare.services.Modules.SalesRep.Dtos;
using InfluanceHairCare.utilities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.SalesRep.Interfaces
{
    public interface ISaleRepInterface
    {

        public Task<GenericResponse<SaleRepRegisterDto>> RegisterSaleRep(SaleRepRegisterDto saleObject);
        public Task<GenericResponse<List<Customer>>> GetSaleRepCustomers(int id);
        public Task<GenericResponse<List<SaleRepBaseDto>>> SaleRepWithOrderSatus(string status);
        public Task<GenericResponse<List<SaleRepResponseDto>>> AllSaleReps();
        public Task<GenericResponse<CustomerRegisterDto>> RegisterCustomerBySaleRep(CustomerRegisterDto custObject);
        public Task<GenericResponse<List<SaleRepReviewDto>>> GetSaleRepReviews(int saleRepId);
    }
}
