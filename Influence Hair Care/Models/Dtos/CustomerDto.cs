namespace Influence_Hair_Care.Models.Dtos
{
    public class CustomerDto
    {
        
        public int Id { get; set; }
 
        public string FirstName { get; set; } = string.Empty;
      
        public string LastName { get; set; } = string.Empty;

        public string Salon_Name { get; set; } = string.Empty;
 
        public string State { get; set; } = string.Empty;
 
        public string City { get; set; } = string.Empty;

 
        public string Address { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public int? SaleRepID { get; set; }



    }
}
