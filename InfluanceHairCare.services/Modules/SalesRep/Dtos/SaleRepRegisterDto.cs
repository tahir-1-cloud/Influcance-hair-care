using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.SalesRep.Dtos
{
    public class SaleRepRegisterDto : SaleRepBaseDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
