using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tea_Store.Models
{
    internal class Tea
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Price { get; set; } = null!;
        public string Properties { get; set; } = null!;
        public byte[] Photo { get; set; } = null!;
        public float Weight { get; set; }
        public string Type { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool AgeLimits { get; set; }
        public float Rating { get; set; }

        public virtual ICollection<OrderTea>? OrderTeas { get; set; }
        public virtual ICollection<TeaReview>? TeaReviews { get; set; }
        public virtual ICollection<ComponentTea>? ComponentTeas { get; set; }
        public virtual ICollection<WishListTea>? WishListTeas { get; set; }
    }
}
