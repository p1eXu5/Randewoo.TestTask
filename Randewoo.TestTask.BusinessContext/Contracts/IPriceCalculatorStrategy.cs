using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Randewoo.TestTask.DataContext.Models;

namespace Randewoo.TestTask.BusinessContext.Contracts
{
    public interface IPriceCalculatorStrategy
    {
        void Calculate( IDistributorRepository distributorRepository );

        IEnumerable< PriceRecord > GetPriceRecords();

        bool IsCalculated { get; }

        double? MinPrice { get; }

        IEnumerable< PriceRecord > MinPriceRecords { get; }
    }
}
