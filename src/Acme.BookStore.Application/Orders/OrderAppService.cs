using Acme.BookStore.Books;
using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Orders
{
    [Authorize(BookStorePermissions.Orders.Default)]
    public class OrderAppService :
        CrudAppService<
            Order,
            OrderDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateOrderDto>,
        IOrderAppService
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        public OrderAppService(
            IRepository<Order, Guid> orderRepository,
            IRepository<Book, Guid> bookRepository)
            : base(orderRepository)
        {
            _bookRepository = bookRepository;
            GetPolicyName = BookStorePermissions.Orders.Default;
            GetListPolicyName = BookStorePermissions.Orders.Default;
            CreatePolicyName = BookStorePermissions.Orders.Default;
            UpdatePolicyName = BookStorePermissions.Orders.Default;
            DeletePolicyName = BookStorePermissions.Orders.Default;
        }
        public override async Task<PagedResultDto<OrderDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            await CheckGetPolicyAsync();

            var query = from order in Repository
                        join book in _bookRepository on order.BookId equals book.Id
                        select new { order, book };

            query = query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var queryResult = await AsyncExecuter.ToListAsync(query);

            var orderDtos = queryResult.Select(x =>
            {
                var orderDto = ObjectMapper.Map<Order, OrderDto>(x.order);
                orderDto.BookName = x.book.Name;
                return orderDto;
            }).ToList();

            var totalCount = await Repository.GetCountAsync();
            return new PagedResultDto<OrderDto>(
                totalCount,
                orderDtos
            );
        }
    }
}
