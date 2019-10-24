using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Randewoo.TestTask.DataContext.Models;

namespace Randewoo.TestTask.DataContext
{
    public static class TestDbContextExtensions
    {
        /// <summary>
        /// Returns active distributors with active prices with used and not deleted price records,
        /// which contains links with not deleted products.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IQueryable< Distributor > GetDistributors( this TestDbContext context )
        {
            return ( from d in context.Distributors
                                      .Include( d => d.Prices )
                                      .ThenInclude( p => p.PricesRecords )
                                      .ThenInclude( pr => pr.Links )
                                      .ThenInclude( l => l.Product )
                     where d.Active && d.Prices.Any( p => p.IsActive.HasValue 
                                                          && p.IsActive.Value
                                                          && p.PricesRecords.Any( pr => pr.Used && !pr.Deleted 
                                                                                                && pr.Links.Any( l => !l.Product.Deleted)) )
                     select d ).AsQueryable().AsNoTracking();
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
