using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tea_Store.Data;
using Tea_Store.Models;
using ViewModels.CartController;

namespace Tea_Store.Services
{
    public interface IShoppingCart
    {
        Task<ShoppingCart> AddItemToCart(int userId, int teaId, CartItemUpdateViewModel addCartItemViewModel);
        Task<ShoppingCart> UpdateCartItem(CartItem oldCartItem, int newQuantity);
        Task<ShoppingCart?> GetShoppingCartByUserId(int userId);
        Task<bool> UserExists(int userId);
        Task<bool> TeaExists(int teaId);
        Task<CartItem> GetCartItem(int userId, int teaId);
        Task DeleteCartItem(int userId, int teaId);
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

        public async Task<ShoppingCart> AddItemToCart(int userId, int teaId, CartItemUpdateViewModel addCartItemViewModel)
        {
            var cart = await GetShoppingCartByUserId(userId);

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
                if (cart.CartItems.Any(ci => ci.TeaId == teaId))
                {
                    var _cartItem = cart.CartItems.First(ci => ci.TeaId == teaId);
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
            cartItem.TeaId = teaId;
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<ShoppingCart> UpdateCartItem(CartItem oldCartItem, int newQuantity)
        {
            oldCartItem.Quantity = newQuantity;
            await _context.SaveChangesAsync();
            return await _context.ShoppingCarts.FirstOrDefaultAsync(c => c.Id == oldCartItem.ShoppingCartId);
        }


        public async Task<ShoppingCart?> GetShoppingCartByUserId(int userId)
        {
            return await _context.ShoppingCarts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
        public async Task<CartItem?> GetCartItem(int userId, int teaId)
        {
            var cart = await GetShoppingCartByUserId(userId);
            if (cart != null)
            {
                return cart.CartItems.FirstOrDefault(ci => ci.TeaId == teaId);
            }
            return null;
        }

        public async Task DeleteCartItem(int userId, int teaId)
        {
            var cartItem = await GetCartItem(userId, teaId);

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExists(int userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId);
        }

        public async Task<bool> TeaExists(int teaId)
        {
            return await _context.Teas.AnyAsync(t => t.Id == teaId);
        }
    }
}
