using System;
using System.Collections.Generic;

namespace Randewoo.TestTask.DataContext.Models
{
    public class PricesRecords
    {
        public PricesRecords()
        {
            Links = new HashSet<Link>();
        }

        public int RecordIndex { get; set; }
        public Guid Priceid { get; set; }
        public string Name { get; set; }
        public byte State { get; set; }
        public bool Used { get; set; }
        public double Price { get; set; }
        public bool Deleted { get; set; }
        public string Comment { get; set; }
        public string Sku { get; set; }
        public int Stock { get; set; }
        public int Instock { get; set; }

        public Price PriceNavigation { get; set; }
        public ICollection< Link > Links { get; set; }
    }
}
