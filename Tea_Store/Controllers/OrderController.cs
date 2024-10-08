using Microsoft.AspNetCore.Mvc;
using Tea_Store.Data;
using Tea_Store.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ViewModels.OrderController;

namespace Tea_Store.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly TeaDBContext _context;
        private readonly IMapper _mapper;

        public OrderController(TeaDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Order create
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateViewModel orderViewModel)
        {
            var user = await _context.Users.FindAsync(orderViewModel.UserId);

            if (user == null)
            {
                return NotFound($"User with ID {orderViewModel.UserId} not found.");
            }

            var order = _mapper.Map<Order>(orderViewModel);

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();

                    foreach (var teaId in orderViewModel.TeaIds)
                    {
                        var tea = await _context.Teas.FindAsync(teaId);

                        if (tea == null)
                        {
                            return NotFound($"Tea with ID {teaId} not found.");
                        }

                        var orderTea = new OrderTea
                        {
                            OrderID = order.Id,
                            TeaID = teaId
                        };

                        _context.OrderTeas.Add(orderTea);
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, "Internal server error.");
                }
            }

            return Ok(order);
        }

        // Order status update
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, OrderUpdateViewModel orderDto)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            order.Status = orderDto.Status;
            order.Updated = DateTime.Now;
            _context.SaveChanges();

            return Ok(order);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderTeas)
                .ThenInclude(ot => ot.Tea)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var orderViewDto = _mapper.Map<OrderViewViewModel>(order);

            return Ok(orderViewDto);
        }
    }
}
