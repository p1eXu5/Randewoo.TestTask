using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Randewoo.TestTask.BusinessContext.Contracts;
using Randewoo.TestTask.DataContext;
using Randewoo.TestTask.DataContext.Models;

namespace Randewoo.TestTask.BusinessContext
{
    public class MinPriceCalculatorStrategy : IPriceCalculatorStrategy
    {
        private readonly Product _product;
        private HashSet< PriceRecord > _records;
        private readonly PriceRecord _parentRecord;
        private double? _minPrice;

        public MinPriceCalculatorStrategy( Product product, Price parent )
        {
            _product = product ?? throw new ArgumentNullException(nameof(product), @"Product cannot be null.");
            _parentRecord = _product.Links.First( l => l.PriceRecord.PriceNavigation == parent ).PriceRecord;
        }

        public void Calculate( IDistributorRepository distributorRepository )
        {
            _records = (from d in distributorRepository.Distributors
                            from price in d.Prices
                            where price.IsActive.HasValue && price.IsActive.Value
                            from priceRecord in price.PriceRecords
                            where priceRecord.Used && !priceRecord.Deleted
                            from link in priceRecord.Links
                            where link.ProductId == _product.Id
                            select link.PriceRecord).ToHashSet();

            _records.Remove( _parentRecord );
            _minPrice = _records.Count > 0 
                            ? _records.Min( r => r.Price )
                            : (double?)null;
        }

        public bool IsCalculated => _records != null;
        public double? MinPrice => _minPrice;

        public IEnumerable< PriceRecord > MinPriceRecords
            => IsCalculated
                   ? _records.Where( r => r.Price.Equals( MinPrice ) )
                   : new PriceRecord[0];

        public IEnumerable< PriceRecord > GetPriceRecords()
        {
            if ( !IsCalculated ) return new PriceRecord[0];

            var competitors = new List< PriceRecord >( _records.Count + 1 ) { _parentRecord };

            competitors.AddRange( _records.OrderBy( r => r.Price ) );

            return competitors;
        }


    }
}
