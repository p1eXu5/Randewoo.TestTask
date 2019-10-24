using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Agbm.Wpf.MvvmBaseLibrary;
using Randewoo.TestTask.DataContext.Models;

namespace Randewoo.TestTask.DesktopClient.ViewModels
{
    public class PriceViewModel : ViewModel
    {
        private readonly Price _price;
        private List< ProductViewModel > _productVmCollection;

        public PriceViewModel( Price price )
        {
            _price = price;
        }

        public Guid Id => _price.Id;

        public string Name => _price.Name;

        public ICollection< ProductViewModel > ProductVmCollection => _productVmCollection;


        public ICommand AnalysisCommand => new MvvmCommand( AnalysisAsync );

        private void AnalysisAsync( object o )
        {
            _productVmCollection = _price.PriceRecords
                                         .Where( pr => pr.Used && !pr.Deleted )
                                         .Select( pr => pr.Links )
                                         .Select( lc => lc.Where( l => !l.Product.Deleted )
                                                          .Select( l => new ProductViewModel( l.Product, _price ) ) )
                                         .Aggregate( new List<ProductViewModel>(), (acc, pvms) => {
                                             acc.AddRange( pvms );
                                             return acc;
                                         } );

            OnPropertyChanged( nameof( ProductVmCollection ) );
        }
    }
}
