using System;
using System.Configuration;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Randewoo.TestTask.BusinessContext;
using Randewoo.TestTask.DataContext;
using Randewoo.TestTask.DesktopClient.Infrastructure;
using Randewoo.TestTask.DesktopClient.ViewModels;

namespace Randewoo.TestTask.DesktopClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup( StartupEventArgs e )
        {
            var connectionString = ConfigurationManager.ConnectionStrings[ "TestDb" ].ConnectionString;

            if ( !String.IsNullOrWhiteSpace( connectionString ) ) 
            {
                var options = new DbContextOptionsBuilder< TestDbContext >( ).UseSqlServer( connectionString ).Options;

                var dbContext = new TestDbContext( options );
                DistributorRepository.SetTestDbContext( dbContext );

                var mvm = new MainViewModel();

                PriceCalculatorStrategyFactory.TestDbContext = dbContext;

                var mainWindow = new MainWindow {
                    DataContext = mvm,
                };

                mainWindow.Show();
            }
        }
    }
}
