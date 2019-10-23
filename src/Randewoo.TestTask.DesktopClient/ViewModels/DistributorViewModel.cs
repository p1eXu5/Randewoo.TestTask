using System;
using System.Collections.Generic;
using System.Linq;
using Agbm.Wpf.MvvmBaseLibrary;
using Randewoo.TestTask.DataContext.Models;

namespace Randewoo.TestTask.DesktopClient.ViewModels
{
    public class DistributorViewModel : ViewModel
    {
        private readonly Distributor _distributor;

        private PriceViewModel _selectedPriceVm;

        public DistributorViewModel( Distributor distributor )
        {
            _distributor = distributor;
            PriceVmCollection = _distributor.Prices
                                            .Where( p => p.IsActive.HasValue && p.IsActive.Value && p.PricesRecords.Any( pr => pr.Used && !pr.Deleted ) )
                                            .OrderBy( p => p.Name )
                                            .Select( p => new PriceViewModel( p ) ).ToArray();

            SelectedPriceVm = PriceVmCollection.FirstOrDefault();
        }

        public Guid Id => _distributor.Id;

        public string Name => _distributor.Name;

        public ICollection< PriceViewModel > PriceVmCollection { get; set; }

        public PriceViewModel SelectedPriceVm
        {
            get => _selectedPriceVm;
            set {
                if ( !object.ReferenceEquals( value, _selectedPriceVm ) ) {
                    _selectedPriceVm = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
