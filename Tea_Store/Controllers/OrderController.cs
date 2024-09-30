using Microsoft.AspNetCore.Mvc;
using Tea_Store.Data;
using Tea_Store.Models;
using Tea_Store.DTO;
using Microsoft.EntityFrameworkCore;

namespace Tea_Store.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly TeaDBContext _context;

        public OrderController(TeaDBContext context)
        {
            _context = context;
        }

        // Order create
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateDTO orderDto)
        {
            var user = await _context.Users.FindAsync(orderDto.UserId);
            if (user == null)
            {
                return NotFound($"User with ID {orderDto.UserId} not found.");
            }

            var order = new Order
            {
                UserID = orderDto.UserId,
                Date = DateTime.Now,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Status = "Pending"
            };

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();

                    foreach (var teaId in orderDto.TeaIds)
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
        public IActionResult UpdateOrder(int id, OrderUpdateDTO orderDto)
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

        // Order get
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderTeas)
                .ThenInclude(ot => ot.Tea)
                .Where(o => o.Id == id)
                .Select(o => new OrderViewDTO
                {
                    Id = o.Id,
                    Date = o.Date,
                    Status = o.Status,
                    Teas = o.OrderTeas.Select(ot => new TeaDTO
                    {
                        Id = ot.Tea.Id,
                        Title = ot.Tea.Title,
                        Price = ot.Tea.Price
                    }).ToList()
                })
                .FirstOrDefault();

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
    }
}
