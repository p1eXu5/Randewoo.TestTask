using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Randewoo.TestTask.BusinessContext;
using Randewoo.TestTask.BusinessContext.Contracts;
using Randewoo.TestTask.DataContext.Models;
using Randewoo.TestTask.DesktopClient.Infrastructure;

namespace Randewoo.TestTask.DesktopClient.ViewModels
{
    public class ProductViewModel
    {
        private readonly Product _product;
        private readonly Price _parent;
        private readonly List< Price > _minPrice;
        private readonly IPriceCalculatorStrategy _minPriceCalculator;

        public ProductViewModel( Product product, Price parent )
        {
            _product = product;
            _parent = parent;
            _minPrice = new List< Price >();

            _minPriceCalculator = PriceCalculatorStrategyFactory.Instance.GetMinPriceCalculator( _product, parent );
        }

        public Guid Id => _product.Id;

        public string Name => $"{_product.Name}, {_product.ChildName}";

        public double Price => _product.Price;

        public double? MinPrice 
        {
            get {
                if ( !_minPriceCalculator.IsCalculated ) {
                    _minPriceCalculator.Calculate( DistributorRepository.Instance );
                }

                return _minPriceCalculator.MinPrice;
            }
        }

        public double DiffPrice => Price - (MinPrice ?? 0.0);

        public string PriceName
        {
            get {
                if ( !_minPriceCalculator.IsCalculated ) {
                    _minPriceCalculator.Calculate( DistributorRepository.Instance );
                }

                return _minPriceCalculator.MinPriceRecords
                                          .FirstOrDefault()?
                                          .PriceNavigation.Name ?? "";
            }
        }

        public string Distributor 
        {
            get {
                if ( !_minPriceCalculator.IsCalculated ) {
                    _minPriceCalculator.Calculate( DistributorRepository.Instance );
                }

                return _minPriceCalculator.MinPriceRecords
                                          .FirstOrDefault()?
                                          .PriceNavigation.Distributor.Name ?? "";
            }
        }

        public string Notice 
        {
            get {
                if ( !_minPriceCalculator.IsCalculated ) {
                    _minPriceCalculator.Calculate( DistributorRepository.Instance );
                }

                return String.Join( "; ", 
                            _minPriceCalculator.MinPriceRecords.Select( pr => pr.PriceNavigation.Name ) );
            }
        }


        public ICollection< CompositePriceViewModel > CompositePriceVmCollection
        {
            get {
                if ( !_minPriceCalculator.IsCalculated ) {
                    _minPriceCalculator.Calculate( DistributorRepository.Instance );
                }

                var coll = _minPriceCalculator.GetPriceRecords()
                                          .Select( t => new CompositePriceViewModel( t ) )
                                          .ToArray();

                coll[ 0 ].Initiator = true;

                return coll;
            }
        } 



    }
}
