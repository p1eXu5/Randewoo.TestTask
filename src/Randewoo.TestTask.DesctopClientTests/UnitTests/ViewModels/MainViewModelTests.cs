using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Randewoo.TestTask.DataContext;
using Randewoo.TestTask.DataContext.Models;
using Randewoo.TestTask.DesktopClient.Infrastructure;
using Randewoo.TestTask.DesktopClient.ViewModels;

namespace Randewoo.TestTask.DesctopClientTests.UnitTests.ViewModels
{

    [TestFixture]
    public class MainViewModelTests
    {
        [ TearDown ]
        public void ClearDistributors()
        {
            DistributorRepository.Crear();
        }

        [Test]
        public void ctor_DbContainsActiveDistributors_LoadActiveDistributors()
        {
            var options = GetOptions();

            var dId = Guid.NewGuid();

            var inDbDistributors = new[] {
                new Distributor {
                    Id = Guid.NewGuid(), 
                    Name = "Distributor 1", Active = true,
                    Prices = new[] {
                        new Price {
                            Id = Guid.NewGuid(), 
                            Name = "Test Price", 
                            IsActive = true,
                            PriceRecords = new[] {
                                new PriceRecord {
                                    RecordIndex = 1,
                                    Used = true,
                                    Deleted = false,
                                    Links = new[] {
                                        new Link {
                                            Id = Guid.NewGuid(),
                                            Product = new Product { Id = Guid.NewGuid() }
                                        }, 
                                    }
                                }, 
                            }
                        },
                    }
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

                DistributorRepository.SetTestDbContext( dbContext );
                var mvm = new MainViewModel();

                var actual = mvm.DistributorVmCollection.ToArray();
                
                Assert.That( actual.Length, Is.EqualTo( 1 ) );

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
            var options = GetOptions();

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
                    PriceRecords = new[] {
                        new PriceRecord {
                            RecordIndex = 10,
                            Used = true,
                            Deleted = false,
                            Links = new[] {
                                new Link {
                                    Id = Guid.NewGuid(),
                                    Product = new Product { Id = Guid.NewGuid() }
                                }, 
                            }
                        },  
                        new PriceRecord { RecordIndex = 11, Used = false, Deleted = false }, 
                        new PriceRecord { RecordIndex = 12, Used = true, Deleted = true }, 
                        new PriceRecord { RecordIndex = 13, Used = false, Deleted = true }, 
                    },
                    DistributorId = distributor.Id
                },
                new Price { 
                    Id = Guid.NewGuid(), 
                    Name = "Test Not Loaded Price 1", 
                    IsActive = false,
                    PriceRecords = new[] {
                        new PriceRecord { RecordIndex = 14, Used = true, Deleted = false }, 
                        new PriceRecord { RecordIndex = 15, Used = false, Deleted = false }, 
                        new PriceRecord { RecordIndex = 16, Used = true, Deleted = true }, 
                        new PriceRecord { RecordIndex = 17, Used = false, Deleted = true }, 
                    },
                    DistributorId = distributor.Id
                },
                new Price { 
                    Id = Guid.NewGuid(), 
                    Name = "Test Not Loaded Price 2", 
                    IsActive = true,
                    PriceRecords = new[] {
                        new PriceRecord { RecordIndex = 26, Used = false, Deleted = false }, 
                        new PriceRecord { RecordIndex = 27, Used = true, Deleted = true }, 
                        new PriceRecord { RecordIndex = 28, Used = false, Deleted = true }, 
                    },
                    DistributorId = distributor.Id
                }
            };

            distributor.Prices = prices;

            using ( var dbContext = new TestDbContext( options ) ) {
                dbContext.Distributors.Add( distributor );
                dbContext.SaveChanges();

            }

            using ( var dbContext = new TestDbContext( options ) ) {

                DistributorRepository.SetTestDbContext( dbContext );
                var mvm = new MainViewModel();
                
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
            var options = GetOptions();

            using var dbContext = new TestDbContext( options );

            DistributorRepository.SetTestDbContext( dbContext );
            var mvm = new MainViewModel();

            Assert.NotNull( mvm.DistributorVmCollection );
        }


        #region factory
        // Insert factory methods here:
        private DbContextOptions< TestDbContext > GetOptions( [CallerMemberName]string dbName = null )
        {
            return new DbContextOptionsBuilder< TestDbContext >().UseInMemoryDatabase( dbName ?? "TestDb" ).Options;
        }
        #endregion
    }

}
