using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Model
{
    public class Shop
    {

        public Guid Id { get; set; }

        public string? Name { get; set; }
        public List<InventoryItem> Items { get; set; }
        public Illustration? Illustration { get; set; }

        public Shop()
        {
            Items = new List<InventoryItem>();
        }

    }
}
