using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Randewoo.TestTask.BusinessContext.Contracts;
using Randewoo.TestTask.DataContext;
using Randewoo.TestTask.DataContext.Models;

namespace Randewoo.TestTask.DesktopClient.Infrastructure
{
    public class DistributorRepository : IDistributorRepository
    {
        private static DistributorRepository _instance;
        private static TestDbContext _dbContext;

        public static DistributorRepository Instance
        {
            get {
                if ( _instance == null ) {
                    if ( _dbContext == null ) {
                        throw new InvalidOperationException("Context not set.");
                    }

                    _instance = new DistributorRepository();
                }

                return _instance;
            }
        }

        public static void SetTestDbContext( TestDbContext context )
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context), @"Context cannot be null.");
        }

        public static void Crear()
        {
            _instance._distributors = null;
        }


        private Distributor[] _distributors;

        protected DistributorRepository()
        { }

        public IEnumerable< Distributor > Distributors
            => _distributors ??= _dbContext.GetDistributors().ToArray();
    }
}
