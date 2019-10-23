using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Randewoo.TestTask.DataContext.Models;

namespace Randewoo.TestTask.DataContext
{
    public static class TestDbContextExtensions
    {
        public static IQueryable< Distributor > GetDistributors( this TestDbContext context )
        {
            return context.Distributors
                          .Include( d => d.Prices )
                          .ThenInclude( p => p.PricesRecords )
                          .AsQueryable();
        }
    }
}
