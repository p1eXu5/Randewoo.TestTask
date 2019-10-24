using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agbm.Wpf.MvvmBaseLibrary;
using Randewoo.TestTask.DataContext.Models;

namespace Randewoo.TestTask.DesktopClient.ViewModels
{
    public class CompositePriceViewModel : ViewModel
    {
        private readonly PriceRecord _priceRecord;

        public CompositePriceViewModel( PriceRecord priceRecord )
        {
            _priceRecord = priceRecord;
        }


        public string Name => _priceRecord.Name;

        public double Price => _priceRecord.Price;

        public string PriceName => _priceRecord.PriceNavigation.Name;

        public string Distributor => _priceRecord.PriceNavigation.Distributor.Name;
    }
}
