using InfluanceHairCare.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.Customers.Dtos
{
    public class CustomersBaseDto 
    {

        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Salon_Name { get; set; } = string.Empty;
        [Required]
        public string State { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string? Phone { get; set; } = string.Empty;
        public string? CustomerImagePath { get; set; } = string.Empty;
        public string? CustomerImage { get; set; } = string.Empty;
        public float AccountBalance { get; set; } = 0;
        public float CreditLimit { get; set; } = 50;
        public int SaleRepId { get; set; } = 0;
    }
}
