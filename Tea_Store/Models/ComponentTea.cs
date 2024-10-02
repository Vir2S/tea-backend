using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tea_Store.Models
{
    public class ComponentTea
    {
        public int Id { get; set; }
        public int ComponentID { get; set; }
        public int TeaID { get; set; }
        public virtual Component? Component { get; set; }
        public virtual Tea? Tea { get; set; }
    }
}
