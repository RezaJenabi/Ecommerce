using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Domain.BaseEntities
{
    public interface IEntity
    {
    }

    public abstract class BaseEntity<TKey> : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<long>
    {
    }
}
