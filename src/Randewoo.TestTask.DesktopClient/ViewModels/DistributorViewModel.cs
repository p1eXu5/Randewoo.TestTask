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

        public DistributorViewModel( Distributor distributor )
        {
            _distributor = distributor;
            PriceVmCollection = _distributor.Prices
                                            .Where( p => p.IsActive.HasValue && p.IsActive.Value && p.PricesRecords.Any( pr => pr.Used && !pr.Deleted ) )
                                            .Select( p => new PriceViewModel( p ) ).ToArray();
        }

        public Guid Id => _distributor.Id;

        public string Name => _distributor.Name;

        public ICollection< PriceViewModel > PriceVmCollection { get; set; }
    }
}
