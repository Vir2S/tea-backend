using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tea_Store.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FName { get; set; } = null!;
        public string Sname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public Role Role { get; set; }
        public string Password { get; set; } = null!;
        public int Age { get; set; }
        public byte[] Photo { get; set; } = null!;
        public int? WishListID { get; set; }
        public virtual SiteReview? SiteReview { get; set; }
        public virtual WishList? WishList { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<TeaReview>? TeaReviews { get; set; }
    }
}
