using System;

namespace Infrastructure.Domain.BaseEntities
{
    public interface ISoftDelete
    {
        bool Deleted { get; set; }
        DateTime DeletedAt { get; set; }
        string DeletedBy { get; set; }
    }
}
