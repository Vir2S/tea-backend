using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tea_Store.Services;
using ViewModels.CartController;

namespace Tea_Store.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly IShoppingCart _cartService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CartController(IShoppingCart service, IUserService userService, IMapper mapper)
        {
            _cartService = service;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetShoppingCart()
        {
            var user = await _userService.GetUserIdByToken(User);

            if (user == null)
            {
                return Unauthorized("Invalid JWT token");
            }

            var shoppingCart = await _cartService.GetShoppingCartByUserId(user.Id);

            if (shoppingCart == null)
            {
                return NotFound("Shopping cart is not found");
            }

            var cartResponse = _mapper.Map<CartViewViewModel>(shoppingCart);
            return Ok(cartResponse);
        }

        [HttpPost("{teaId}")]
        [Authorize]

        public async Task<IActionResult> AddItemToCart(int teaId, [FromBody] CartItemUpdateViewModel addCartItemDto)
        {
            var user = await _userService.GetUserIdByToken(User);

            if (user == null)
            {
                return Unauthorized("Invalid JWT token");
            }

            if (!await _cartService.TeaExists(teaId))
            {
                return NotFound("Tea is not found");
            }

            var cart = await _cartService.AddItemToCart(user.Id, teaId, addCartItemDto);
            var cartResponse = _mapper.Map<CartViewViewModel>(cart);
            return Ok(cartResponse);
        }

        [HttpPut("{teaId}")]
        [Authorize]
        public async Task<IActionResult> UpdateOrder(int teaId, [FromBody] CartItemUpdateViewModel updateCartItemDto)
        {
            var user = await _userService.GetUserIdByToken(User);

            if (user == null)
            {
                return Unauthorized("Invalid JWT token");
            }

            var cartItem = await _cartService.GetCartItem(user.Id, teaId);

            if (cartItem == null)
            {
                return NotFound("Cart item is not found");
            }

            var cart = await _cartService.UpdateCartItem(cartItem, updateCartItemDto.Quantity);

            var cartResponse = _mapper.Map<CartViewViewModel>(cart);
            return Ok(cartResponse);
        }

        [HttpDelete("{teaId}")]
        [Authorize]
        public async Task<IActionResult> DeleteCartItem(int teaId)
        {
            var user = await _userService.GetUserIdByToken(User);

            if (user == null)
            {
                return Unauthorized("Invalid JWT token");
            }

            var cartItem = await _cartService.GetCartItem(user.Id, teaId);

            if (cartItem == null)
            {
                return NotFound("Cart item is not found");
            }

            await _cartService.DeleteCartItem(user.Id, teaId);

            return Ok("Deleted succesfully");
        }

    }
}
