using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tea_Store.Models
{
    internal class WishList
    {
        public int Id { get; set; }
        public int UserID { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<WishListTea>? WishListTeas { get; set; }
    }
}
