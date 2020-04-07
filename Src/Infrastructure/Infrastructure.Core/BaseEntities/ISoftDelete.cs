using System;

namespace Infrastructure.Core.BaseEntities
{
    public interface ISoftDelete
    {
        bool Deleted { get; set; }
        DateTime DeletedAt { get; set; }
        string DeletedBy { get; set; }
    }
}
