using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.CartController
{
    public class CartItemUpdateViewModel
    {
        [Required]
        [Range(1, 100)]
        [DefaultValue(1)]
        public int Quantity { get; set; }
    }
}
