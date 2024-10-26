using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.CartController
{
    public class CartItemAddViewModel
    {
        [Required]
        public int TeaId { get; set; }
        [Required]
        [Range(1, 100)]
        [DefaultValue(1)]
        public int Quantity { get; set; }
    }
}
