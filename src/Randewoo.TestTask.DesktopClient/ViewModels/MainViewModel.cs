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
            _dbContext = dbContext;
        }


    }
}
