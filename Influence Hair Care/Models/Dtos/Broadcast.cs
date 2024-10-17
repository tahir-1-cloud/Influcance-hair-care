using InfluanceHairCare.models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Influence_Hair_Care.Models.Dtos
{
    public class Broadcast
    {
        [Key]
        public int BroadId { get; set; }

        public string SelectState { get; set; }

        public string SelectCity { get; set; }

        public bool AllSaleRep { get; set; }

        public bool AllCustomers { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string Image { get; set; }

        public int SaleRepId { get; set; }

        public int CustomerId { get; set; }


        [NotMapped]
        public IFormFile? files { get; set; }

        [NotMapped]
        public virtual Customer Customers { get; set; }


        [NotMapped]
        public virtual SaleRep SaleReps { get; set; }
    }

}
