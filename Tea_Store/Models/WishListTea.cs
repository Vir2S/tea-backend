using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tea_Store.Models
{
    public class WishListTea
    {
        public int Id { get; set; }
        public int WishListID { get; set; }
        public int TeaID { get; set; }
        public virtual WishList? WishList { get; set; }
        public virtual Tea? Tea { get; set; }
    }
}
