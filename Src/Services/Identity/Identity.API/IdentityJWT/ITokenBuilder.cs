﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.IdentityJWT
{
   public interface ITokenBuilder
    {
        string BuildToken(string username);
    }
}