using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tea_Store.Data;
using Tea_Store.Models;
using Tea_Store.Services;
using ViewModels.CartController;
using ViewModels.OrderController;

namespace Tea_Store.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly IShoppingCart _service;
        private readonly IMapper _mapper;

        public CartController(IShoppingCart service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("{userId}")]

        public async Task<IActionResult> AddItemToCart(int userId, [FromBody] CartItemAddViewModel addCartItemDto)
        {   
            if (!await _service.UserExists(userId))
            {
                return NotFound("User is not found");
            }
            var cart = await _service.AddItemToCart(userId, addCartItemDto);
            var cartResponse = _mapper.Map<CartViewViewModel>(cart);
            return Ok(cartResponse);
        }

        //// Order status update
        //[HttpPut("{id}")]
        //public IActionResult UpdateOrder(int id, OrderUpdateViewModel orderDto)
        //{
        //    var order = _context.Orders.Find(id);

        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    order.Status = orderDto.Status;
        //    order.Updated = DateTime.Now;
        //    _context.SaveChanges();

        //    return Ok(order);
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetOrder(int id)
        //{
        //    var order = _context.Orders
        //        .Include(o => o.OrderTeas)
        //        .ThenInclude(ot => ot.Tea)
        //        .FirstOrDefault(o => o.Id == id);

        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    var orderViewDto = _mapper.Map<OrderViewViewModel>(order);

        //    return Ok(orderViewDto);
        //}
    }
}
