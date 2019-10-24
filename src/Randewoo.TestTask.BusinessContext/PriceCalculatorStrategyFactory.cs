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
    public class PriceCalculatorStrategyFactory
    {
        private static PriceCalculatorStrategyFactory _instance;

        public static TestDbContext TestDbContext { get; set; }

        public static PriceCalculatorStrategyFactory Instance
            => _instance ??= new PriceCalculatorStrategyFactory();

        protected PriceCalculatorStrategyFactory()
        { }

        public IPriceCalculatorStrategy GetMinPriceCalculator( Product product, Price parentPrice )
        {
            return new MinPriceCalculatorStrategy( product, parentPrice );
        }
    }
}
