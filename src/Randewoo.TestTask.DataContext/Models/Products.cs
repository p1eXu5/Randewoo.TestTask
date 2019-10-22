using System;
using System.Collections.Generic;

namespace Randewoo.TestTask.DataContext.Models
{
    public partial class Products
    {
        public Products()
        {
            Links = new HashSet<Links>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid Manid { get; set; }
        public double Price { get; set; }
        public double? Baseprice { get; set; }
        public string Label { get; set; }
        public string Parentlabel { get; set; }
        public byte State { get; set; }
        public bool Userchanged { get; set; }
        public bool Extraused { get; set; }
        public int Extra { get; set; }
        public bool Autoupdate { get; set; }
        public bool? Autoupdatetests { get; set; }
        public bool? Productexists { get; set; }
        public bool Photoexists { get; set; }
        public int Viewstyleid { get; set; }
        public string Childname { get; set; }
        public bool Tester { get; set; }
        public bool? Published { get; set; }
        public DateTime Createdate { get; set; }
        public DateTime Changedate { get; set; }
        public byte Correctedstatus { get; set; }
        public int Instock { get; set; }
        public bool Isprobirka { get; set; }
        public string Comment { get; set; }
        public bool Deleted { get; set; }
        public bool IsPromo { get; set; }
        public int Templateid { get; set; }
        public int Sku { get; set; }
        public bool Isnew { get; set; }
        public DateTime? Isnewstartdate { get; set; }
        public double Weight { get; set; }
        public byte Defaultcurrency { get; set; }

        public virtual Manufacturers Man { get; set; }
        public virtual ICollection<Links> Links { get; set; }
    }
}
