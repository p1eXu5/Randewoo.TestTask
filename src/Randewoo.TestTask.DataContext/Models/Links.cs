using System;
using System.Collections.Generic;

namespace Randewoo.TestTask.DataContext.Models
{
    public partial class Links
    {
        public Guid Id { get; set; }
        public Guid Catalogproductid { get; set; }
        public int Pricerecordindex { get; set; }

        public virtual Products Catalogproduct { get; set; }
        public virtual Pricesrecords PricerecordindexNavigation { get; set; }
    }
}
