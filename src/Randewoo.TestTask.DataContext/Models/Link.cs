using System;
using System.Collections.Generic;

namespace Randewoo.TestTask.DataContext.Models
{
    public partial class Link
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Pricerecordindex { get; set; }

        public virtual Product Product { get; set; }
        public virtual PricesRecords PriceRecord { get; set; }
    }
}
