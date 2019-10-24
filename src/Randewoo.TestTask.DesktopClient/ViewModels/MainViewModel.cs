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
using Randewoo.TestTask.DesktopClient.Infrastructure;

namespace Randewoo.TestTask.DesktopClient.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private DistributorViewModel _selectedDistributorVm;

        public MainViewModel()
        {
            void LoadDistributors()
            {
                DistributorVmCollection = DistributorRepository.Instance.Distributors
                                                    .OrderBy( d => d.Name )
                                                    .Select( d => new DistributorViewModel( d ) )
                                                    .ToArray();
            }


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
