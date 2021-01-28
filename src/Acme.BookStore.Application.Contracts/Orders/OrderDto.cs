using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Orders
{
    public class OrderDto : AuditedEntityDto<Guid>
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public string BookName { get; set; }
    }
}
