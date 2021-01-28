using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Orders
{
    public class Order : AuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
    }
}
