using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Domain.DataBaseContext
{
    public interface IDbContext
    {
        string DefaultSchema { get; }
    }
}