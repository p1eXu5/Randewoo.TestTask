using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Agbm.Wpf.MvvmBaseLibrary;
using Microsoft.EntityFrameworkCore;
using Randewoo.TestTask.DataContext;
using Randewoo.TestTask.DataContext.Models;

namespace Randewoo.TestTask.DesktopClient.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly TestDbContext _dbContext;
        private DistributorViewModel _selectedDistributorVm;

        public MainViewModel( TestDbContext dbContext )
        {
            void LoadDistributors()
            {
                DistributorVmCollection = _dbContext.GetDistributors()
                                                    .OrderBy( d => d.Name )
                                                    .Select( d => new DistributorViewModel( d ) )
                                                    .ToArray();
            }


            _dbContext = dbContext;

            LoadDistributors();
            SelectedDistributorVm = DistributorVmCollection.FirstOrDefault();
        }

        public ICollection< DistributorViewModel > DistributorVmCollection { get; private set; }

        public DistributorViewModel SelectedDistributorVm
        {
            get => _selectedDistributorVm;
            set {
                if ( !object.ReferenceEquals( value, _selectedDistributorVm ) ) {
                    _selectedDistributorVm = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
