using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tea_Store.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Status { get; set; } = null!;
        public int UserID { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<OrderTea>? OrderTeas { get; set; }
    }
}
