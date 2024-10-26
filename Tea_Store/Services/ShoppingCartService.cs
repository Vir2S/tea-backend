using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tea_Store.Data;
using Tea_Store.Models;
using ViewModels.CartController;

namespace Tea_Store.Services
{
    public interface IShoppingCart
    {
        Task<ShoppingCart> AddItemToCart(int userId, CartItemAddViewModel addCartItemViewModel);
        Task<bool> UserExists(int userId);
    }
    public class ShoppingCartService : IShoppingCart
    {
        private readonly TeaDBContext _context;
        private readonly IMapper _mapper;

        public ShoppingCartService(TeaDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShoppingCart> AddItemToCart(int userId, CartItemAddViewModel addCartItemViewModel)
        {
            var cart = await _context.ShoppingCarts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            var cartItem = _mapper.Map<CartItem>(addCartItemViewModel);

            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId, CartItems = new List<CartItem>() };
                _context.ShoppingCarts.Add(cart);

                cartItem.ShoppingCartId = cart.Id;
                cart.CartItems.Add(cartItem);
            }

            else
            {
                if (cart.CartItems.Any(ci => ci.TeaId == cartItem.TeaId))
                {
                    var _cartItem = cart.CartItems.First(ci => ci.TeaId == cartItem.TeaId);
                    _cartItem.Quantity += cartItem.Quantity;
                    if (_cartItem.Quantity > 100)
                    {
                        _cartItem.Quantity = 100;
                    }
                }
                else
                {
                    cartItem.ShoppingCartId = cart.Id;
                    cart.CartItems.Add(cartItem);
                }
                
            }

            await _context.SaveChangesAsync();
            return cart;
        }

        public Task<bool> UserExists(int userId)
        {
            return _context.Users.AnyAsync(u => u.Id == userId);
        }

        //public async Task<ShoppingCart?> GetShoppingCartByUserId(int userId)
        //{
        //    return await _context.ShoppingCarts
        //        .Include(c => c.CartItems)
        //        .FirstOrDefaultAsync(c => c.UserId == userId);
        //}
    }
}
