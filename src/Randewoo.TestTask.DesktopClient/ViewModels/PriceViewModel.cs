using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agbm.Wpf.MvvmBaseLibrary;
using Randewoo.TestTask.DataContext.Models;

namespace Randewoo.TestTask.DesktopClient.ViewModels
{
    public class PriceViewModel : ViewModel
    {
        private readonly Price _price;

        public PriceViewModel( Price price )
        {
            _price = price;
        }

        public Guid Id => _price.Id;

        public string Name => _price.Name;
    }
}
