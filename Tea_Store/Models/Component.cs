using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tea_Store.Models
{
    public class Component
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<ComponentTea>? ComponentTeas { get; set; }
    }
}
