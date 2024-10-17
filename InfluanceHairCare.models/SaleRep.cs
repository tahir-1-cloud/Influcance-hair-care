using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.models
{
    public class SaleRep
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string State { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string? SaleRepImagePath { get; set; } = string.Empty;
        public string? SaleRepImage { get; set; } = string.Empty;
        public float Rating { get; set; } = 0;

        public float? Discount { get; set; }
        public bool? Status { get; set; }

        [Required]
        public long LoginId { get; set; }

        [ForeignKey("LoginId")]
        public virtual Login Login { get; set; }

        public virtual ICollection<Customer>? SaleRepCustomer { get; set; }
    }
}
