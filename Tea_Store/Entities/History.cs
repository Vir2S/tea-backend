using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tea_Store.Entities
{
    internal class History
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public virtual User? User { get; set; }
        public virtual Order? Order{ get; set; }
    }
}
