using System;
using System.Linq;
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
                          .AsQueryable().AsNoTracking();
        }

        public static IQueryable< Product > GetProducts( this TestDbContext context, Guid priceId )
        {
            return (from p in context.Prices.Include( p => p.PricesRecords )
                                      .ThenInclude( pr => pr.Links )
                                      .ThenInclude( l => l.Product )
                    where p.Id == priceId
                    from pr in p.PricesRecords 
                    where !pr.Deleted && pr.Used
                    from link in pr.Links
                    select link.Product).Where( p => !p.Deleted ).AsQueryable();
        }
    }
}
