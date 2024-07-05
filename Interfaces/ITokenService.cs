using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using onboarding.Models;

namespace onboarding.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}