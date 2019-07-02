using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamApp.Models;
using ExamApp.Dto;
using AutoMapper;
using ExamApp.Enums;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;

namespace ExamApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersContext _context;
        private readonly IMapper _mapper;

        public OrdersController(OrdersContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<OrdersDto>> GetOrders()
        {
            var orders = await OrdersWithIncludedEntities()
                .Select(o => new { Order = o, BillingAddresses = o.BillingAddresses.First() })
                .Select(oba => SelectOrderDto(oba.Order, oba.BillingAddresses))
                .ToListAsync();

            return new OrdersDto()
            {
                Orders = orders
            };
        }

        // GET: api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdersDto>> GetOrdersByOxId(string id)
        {
            var orders = await OrdersWithIncludedEntities()
                .Where(o => o.OxId.ToString().Contains(id))
                .Select(o => new { Order = o, BillingAddresses = o.BillingAddresses.First() })
                .Select(oba => SelectOrderDto(oba.Order, oba.BillingAddresses))
                .ToListAsync();

            return new OrdersDto()
            {
                Orders = orders
            };
        }

        private IIncludableQueryable<Order, ICollection<BillingAddresse>> OrdersWithIncludedEntities()
        {
            return _context.Orders
                    .Include(o => o.Payments)
                    .Include(o => o.Articles)
                    .Include(o => o.BillingAddresses);
        }

        private OrderDto SelectOrderDto(Order order, BillingAddresse billingAddresse)
        {
            return new OrderDto
            {
                OxId = order.OxId,
                OrderStatus = order.OrderStatus,
                InvoiceNumber = order.InvoiceNumber,
                OrderDatetime = order.OrderDatetime,
                BillingAddress = _mapper.Map<BillingAddresseDto>(billingAddresse),
                Articles = order.Articles.Select(a => _mapper.Map<ArticleDto>(a)).ToList(),
                Payments = order.Payments.Select(p => _mapper.Map<PaymentDto>(p)).ToList(),
            };
        }

        // PUT: api/orders/5/status
        [HttpPut("{id}/status")]
        public async Task<ActionResult> PutOrderStatus(long id, [FromBody]EStatus status)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return NotFound();

            order.OrderStatus = status;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/orders/5/invoicenumber
        [HttpPut("{id}/invoicenumber")]
        public async Task<ActionResult> PutOrderInvoiceNumber(long id, [FromBody]int number)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return NotFound();

            order.InvoiceNumber = number;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/orders
        [HttpPost()]
        public async Task<ActionResult> PostImportXml(OrdersDto ordersDto)
        {
            var orders = ordersDto.Orders.Select(dto =>
            {
                var order = _mapper.Map<Order>(dto);

                order.BillingAddresses.Add(_mapper.Map<BillingAddresse>(dto.BillingAddress));

                foreach (var paymentDto in dto.Payments)
                    order.Payments.Add(_mapper.Map<Payment>(paymentDto));

                foreach (var articleDto in dto.Articles)
                    order.Articles.Add(_mapper.Map<Article>(articleDto));

                return order;
            });

            await _context.Orders.AddRangeAsync(orders);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrders), ordersDto);
        }
    }
}
