using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Randewoo.TestTask.DataContext.Models;

namespace Randewoo.TestTask.DesktopClient.ViewModels
{
    public class ProductViewModel
    {
        private readonly Product _product;
        private readonly List< Price > _minPrice;

        public ProductViewModel( Product product )
        {
            _product = product;
            _minPrice = new List< Price >();
        }

        public Guid Id => _product.Id;

        public string Name => $"{_product.Name} {_product.ChildName}";

        public double Price => _product.Price;

        public double? MinPrice => _minPrice.FirstOrDefault()?.PricesRecords.First( pr => pr.Links.Any( l => l.ProductId == Id ) ).Price;

        public double DiffPrice => Price - (MinPrice ?? 0.0);

        public string Prices =>
            _minPrice.Count <= 1
                ? ""
                : String.Join( " ", _minPrice.Select( p => p.Name ) );
    }
}
