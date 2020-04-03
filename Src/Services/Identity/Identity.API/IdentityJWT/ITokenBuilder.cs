using Identity.API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.IdentityJWT
{
   public interface ITokenBuilder
    {
        TokenResponse BuildToken(string username);
    }
}