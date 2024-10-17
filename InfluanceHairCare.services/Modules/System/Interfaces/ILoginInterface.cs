using InfluanceHairCare.services.Modules.System.Dtos;
using InfluanceHairCare.utilities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluanceHairCare.services.Modules.System.Interfaces
{
    public interface ILoginInterface
    {
        public Task<GenericResponse<LoginResponseDto>> AuthenticateUser(LoginRequestDto loginRequest);
    }
}
