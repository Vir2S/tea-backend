using ViewModels.OrderController;

namespace ViewModels.CartController
{
    public class CartViewViewModel
    {
        public int UserId { get; set; }
        public List<CartItemAddViewModel> CartItems { get; set; }
    }
}
