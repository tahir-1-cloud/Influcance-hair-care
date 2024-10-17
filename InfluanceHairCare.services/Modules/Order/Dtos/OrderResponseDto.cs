using InfluanceHairCare.models;
using InfluanceHairCare.services.Modules.SalesRep.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.Order.Dtos
{
    public class OrderResponseDto : OrderBaseDto
    {
        //public Customer Customer { get; set; } = new Customer();

        public string FirstName { get; set; } = string.Empty;
        [MaxLength(20)]
        public string LastName { get; set; } = string.Empty;
        [MaxLength(30)]
        public string Salon_Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [MaxLength(300)]
        public string Location { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? CustomerImagePath { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? CustomerImage { get; set; } = string.Empty;

        public string SaleRepName { get; set; } = string.Empty;
        public int SaleRepId { get; set; }
        public float Rating { get; set; }
        public List<OrderProductBaseDto> OrderProducts { get; set; } = new List<OrderProductBaseDto>();
        public List<OrderPaymentBaseDto> OrderPayment { get; set; } = new List<OrderPaymentBaseDto>();

    }
}
