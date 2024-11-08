using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.CartController
{
    public class CartItemViewModel
    {
        public int TeaId { get; set; }
        public int Quantity { get; set; }
    }
}
