using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(20)]
        [Required]
        public string LastName { get; set; } = string.Empty;
        [MaxLength(30)]
        [Required]
        public string Salon_Name { get; set; } = string.Empty;
        [MaxLength(30)]
        [Required]
        public string State { get; set; } = string.Empty;
        [MaxLength(30)]
        [Required]
        public string City { get; set; } = string.Empty;

        [MaxLength(100)]
        [Required]
        public string Address { get; set; } = string.Empty;

        [MaxLength(300)]
        public string Location { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? CustomerImagePath { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? CustomerImage { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Phone { get; set; } = string.Empty;

        public float AccountBalance { get; set; } = 0;
        public float CreditLimit { get; set; } = 50;

        [AllowNull]
        public int? SaleRepID { get; set; } = null;
        public virtual SaleRep? SaleRep { get; set; } 

        [Required]
        public long LoginId { get; set; }

        [ForeignKey("LoginId")]
        public virtual Login Login { get; set; } 

        public virtual ICollection<Orders>? CustomerOrders { get; set; }
        public virtual ICollection<CustomerfavoriteProducts>? FavoriteProducts { get; set; }

    }
}
