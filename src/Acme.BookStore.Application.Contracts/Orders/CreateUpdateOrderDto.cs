using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Orders
{
    public class CreateUpdateOrderDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid BookId { get; set; }
    }
}
