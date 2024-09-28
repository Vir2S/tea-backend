using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tea_Store.Models
{
    internal class OrderTea
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        public int TeaID { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Tea? Tea { get; set; }
    }
}
