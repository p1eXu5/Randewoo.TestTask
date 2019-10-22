using System;
using System.Collections.Generic;

namespace Randewoo.TestTask.DataContext.Models
{
    public partial class Distributors
    {
        public Distributors()
        {
            Prices = new HashSet<Prices>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public bool? Goinpurchaselist { get; set; }
        public bool? Firstalways { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool? Sendmail { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public byte? Priority { get; set; }
        public int? Dealerorder { get; set; }

        public virtual ICollection<Prices> Prices { get; set; }
    }
}
