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
            var priceRecords = context.Prices.Include( p => p.PricesRecords )
                                      .ThenInclude( pr => pr.Links )
                                      .ThenInclude( l => l.Product )
                                      .First( p => p.Id == priceId )
                                      .PricesRecords;

            return (from pr in priceRecords
                    where !pr.Deleted && pr.Used
                    from link in pr.Links
                    select link.Product).AsQueryable();
        }
    }
}
