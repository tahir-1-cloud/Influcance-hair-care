using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.models
{
    public class Broadcast
    {
        [Key]
        public int BroadId { get; set; }

        [MaxLength(30)]
        public string SelectState { get; set; } = string.Empty;

        [MaxLength(30)]
        public string SelectCity { get; set; } = string.Empty;

        [Required(ErrorMessage = "AllSaleRep is Required")]
        public bool AllSaleRep { get; set; }

        [Required(ErrorMessage = "AllCustomers is Required")]

        public bool AllCustomers { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Title is Required")]

        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        [Required(ErrorMessage = "Description is Required")]

        public string Description { get; set; } = string.Empty;

        [MaxLength(500)]
        public string ImagePath { get; set; } = string.Empty;

        [MaxLength(300)]
        public string Image { get; set; } = string.Empty;

        public int SaleRepId { get; set; }

        public int CustomerId { get; set; }


    }
}
