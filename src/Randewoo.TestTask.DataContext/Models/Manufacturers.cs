using System;
using System.Collections.Generic;

namespace Randewoo.TestTask.DataContext.Models
{
    public partial class Manufacturers
    {
        public Manufacturers()
        {
            Products = new HashSet<Product>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Extraused { get; set; }
        public int Extra { get; set; }
        public string Description { get; set; }
        public bool? Published { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
