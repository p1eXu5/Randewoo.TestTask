using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agbm.Wpf.MvvmBaseLibrary;
using Randewoo.TestTask.DataContext;

namespace Randewoo.TestTask.DesktopClient.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly TestDbContext _dbContext;
        public MainViewModel( TestDbContext dbContext )
        {
            void LoadDistributors()
            {
                DistributorVmCollection = _dbContext.GetDistributors()
                                                    .Where( d => d.Active && d.Prices.Any( p => p.IsActive.HasValue && p.IsActive.Value ) )
                                                    .Select( d => new DistributorViewModel( d ) )
                                                    .ToArray();
            }


            _dbContext = dbContext;

            LoadDistributors();
        }

        public ICollection< DistributorViewModel > DistributorVmCollection { get; private set; }
    }
}
