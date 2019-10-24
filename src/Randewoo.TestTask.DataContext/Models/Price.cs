using System;
using System.Collections.Generic;

namespace Randewoo.TestTask.DataContext.Models
{
    public class Price
    {
        public Price()
        {
            PriceRecords = new HashSet<PriceRecord>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Discount { get; set; }
        public byte DefaultCurrency { get; set; }
        public double Turndollarsrate { get; set; }
        public string Sheet { get; set; }
        public string Namerange { get; set; }
        public string Pricerange { get; set; }
        public int Firstrow { get; set; }
        public float Maxpricechange { get; set; }
        public string Stopwords { get; set; }
        public string Filename { get; set; }
        public DateTime? Filedate { get; set; }
        public string Filesheets { get; set; }
        public string Comment { get; set; }
        public string Skurange { get; set; }
        public string Stockrange { get; set; }
        public string Instockrange { get; set; }
        public bool? IsActive { get; set; }

        public Guid? DistributorId { get; set; }
        public Distributor Distributor { get; set; }

        public ICollection<PriceRecord> PriceRecords { get; set; }
    }
}
