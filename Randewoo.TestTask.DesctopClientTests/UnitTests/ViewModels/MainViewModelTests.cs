using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Randewoo.TestTask.DataContext;
using Randewoo.TestTask.DataContext.Models;
using Randewoo.TestTask.DesktopClient.ViewModels;

namespace Randewoo.TestTask.DesctopClientTests.UnitTests.ViewModels
{

    [TestFixture]
    public class MainViewModelTests
    {
        [Test]
        public void ctor_DbContainsActiveDistributors_LoadActiveDistributors()
        {
            var options = GetOptions( "TestInMemory 1" );

            var inDbDistributors = new[] {
                new Distributor {
                    Id = Guid.NewGuid(), Name = "Distributor 1", Active = true,
                    Prices = new[] { new Price { Id = Guid.NewGuid(), Name = "Test Price", IsActive = true }, }
                },
                new Distributor { Id = Guid.NewGuid(), Name = "Distributor 2", Active = true },
                new Distributor {
                    Id = Guid.NewGuid(), Name = "Distributor 3",
                    Prices = new[] { new Price { Id = Guid.NewGuid(), Name = "Test Price", IsActive = true }, }
                }
            };

            using ( var dbContext = new TestDbContext( options ) ) {
                dbContext.Distributors.AddRange( inDbDistributors );
                dbContext.SaveChanges();

            }

            using ( var dbContext = new TestDbContext( options ) ) {

                var mvm = new MainViewModel( dbContext );
                
                Assert.That( mvm.DistributorVmCollection.Count, Is.EqualTo( 1 ) );

                Assert.That( 
                    mvm.DistributorVmCollection.First().Id, 
                    Is.EqualTo( inDbDistributors[0].Id ) );

                Assert.That( 
                    mvm.DistributorVmCollection.First().Name, 
                    Is.EqualTo( inDbDistributors[0].Name ) );
            }
        }

        [Test]
        public void ctor__DbContainsActiveDistributors_WithActiveNotDeletedAndUsingPrices__LoadDistributorsWithPrices()
        {
            var options = GetOptions( "TestInMemory 2" );

            var distributor = new Distributor {
                Id = Guid.NewGuid(),
                Name = "Test Distributor",
                Active = true,
            };

            var prices = new[] {
                new Price { 
                    Id = Guid.NewGuid(),
                    Name = "Test Loaded Price", 
                    IsActive = true,
                    PricesRecords = new[] {
                        new PricesRecords { Used = true, Deleted = false }, 
                        new PricesRecords { Used = false, Deleted = false }, 
                        new PricesRecords { Used = true, Deleted = true }, 
                        new PricesRecords { Used = false, Deleted = true }, 
                    },
                    DistributorId = distributor.Id
                },
                new Price { 
                    Id = Guid.NewGuid(), 
                    Name = "Test Not Loaded Price 1", 
                    IsActive = false,
                    PricesRecords = new[] {
                        new PricesRecords { Used = true, Deleted = false }, 
                        new PricesRecords { Used = false, Deleted = false }, 
                        new PricesRecords { Used = true, Deleted = true }, 
                        new PricesRecords { Used = false, Deleted = true }, 
                    },
                    DistributorId = distributor.Id
                },
                new Price { 
                    Id = Guid.NewGuid(), 
                    Name = "Test Not Loaded Price 2", 
                    IsActive = true,
                    PricesRecords = new[] {
                        new PricesRecords { Used = false, Deleted = false }, 
                        new PricesRecords { Used = true, Deleted = true }, 
                        new PricesRecords { Used = false, Deleted = true }, 
                    },
                    DistributorId = distributor.Id
                }
            };


            using ( var dbContext = new TestDbContext( options ) ) {
                dbContext.Distributors.Add( distributor );
                dbContext.Prices.AddRange( prices );
                dbContext.SaveChanges();

            }

            using ( var dbContext = new TestDbContext( options ) ) {

                var mvm = new MainViewModel( dbContext );
                
                Assert.That( mvm.DistributorVmCollection.Count, Is.EqualTo( 1 ) );

                var distributorVm = mvm.DistributorVmCollection.First();
                Assert.That( distributorVm.PriceVmCollection.Count(), Is.EqualTo( 1 ) );

                var priceVm = distributorVm.PriceVmCollection.First();

                Assert.That( priceVm.Id, Is.EqualTo( prices[0].Id ) );
            }
        }

        [ Test ]
        public void ctor_DbDoesNotContainsRecords_CreatesEmptyDistributorCollection()
        {
            var options = GetOptions( "TestInMemory 3" );

            using var dbContext = new TestDbContext( options );
            var mvm = new MainViewModel( dbContext );

            Assert.NotNull( mvm.DistributorVmCollection );
        }


        #region factory
        // Insert factory methods here:
        private DbContextOptions< TestDbContext > GetOptions( string dbName )
        {
            return new DbContextOptionsBuilder< TestDbContext >().UseInMemoryDatabase( dbName ).Options;
        }
        #endregion
    }

}
