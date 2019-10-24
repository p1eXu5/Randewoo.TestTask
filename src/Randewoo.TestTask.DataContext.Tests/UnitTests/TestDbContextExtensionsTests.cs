using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Randewoo.TestTask.DataContext.Models;
using Randewoo.TestTask.DataContext.Tests.UnitTests.Factories;

namespace Randewoo.TestTask.DataContext.Tests.UnitTests
{

    [TestFixture]
    public class TestDbContextExtensionsTests
    {
        #region GetProducts tests

        [Test]
        public void GetProducts_PriceActive_PriceRecordsNotDeletetAndIsUsed_ReturnsAllProduct()
        {
            var priceId = Guid.NewGuid();

            var price1 = new Price
            {
                Id = priceId,
                IsActive = true,
                Name = "Test Price 1",
                Sheet = "Test Sheet 1",
                Namerange = "Test NameRange 1",
                Pricerange = "",
                PriceRecords = new[] {
                    new PriceRecord {
                        RecordIndex = 1,
                        Used = true,
                        Deleted = false,
                        Links = new[] {
                            new Link {
                                Id = Guid.NewGuid(),
                                Product = new Product {
                                    Id = Guid.NewGuid(),
                                    Name = "Product 1"
                                }
                            },
                            new Link {
                                Id = Guid.NewGuid(),
                                Product = new Product {
                                    Id = Guid.NewGuid(),
                                    Name = "Product 2"
                                }
                            },
                        }
                    },
                    new PriceRecord {
                        RecordIndex = 2,
                        Used = true,
                        Deleted = false,
                        Links = new[] {
                            new Link {
                                Id = Guid.NewGuid(),
                                Product = new Product {
                                    Id = Guid.NewGuid(),
                                    Name = "Product 3"
                                }
                            },
                            new Link {
                                Id = Guid.NewGuid(),
                                Product = new Product {
                                    Id = Guid.NewGuid(),
                                    Name = "Product 4"
                                }
                            },
                        }
                    },
                    new PriceRecord {
                        RecordIndex = 3,
                        Used = false,
                        Deleted = false,
                        Links = new[] {
                            new Link {
                                Id = Guid.NewGuid(),
                                Product = new Product {
                                    Id = Guid.NewGuid(),
                                    Name = "Product 5"
                                }
                            },
                            new Link {
                                Id = Guid.NewGuid(),
                                Product = new Product {
                                    Id = Guid.NewGuid(),
                                    Name = "Product 6"
                                }
                            },
                        }
                    },
                    new PriceRecord {
                        RecordIndex = 4,
                        Used = true,
                        Deleted = true,
                        Links = new[] {
                            new Link {
                                Id = Guid.NewGuid(),
                                Product = new Product {
                                    Id = Guid.NewGuid(),
                                    Name = "Product 7"
                                }
                            },
                            new Link {
                                Id = Guid.NewGuid(),
                                Product = new Product {
                                    Id = Guid.NewGuid(),
                                    Name = "Product 8"
                                }
                            },
                        }
                    }
                }
            };

            var price2 = new Price
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                Name = "Test Price 2",
                Sheet = "Test Sheet 2",
                Namerange = "Test NameRange 2",
                Pricerange = "",
                PriceRecords = new[] {
                    new PriceRecord {
                        RecordIndex = 5,
                        Used = true,
                        Deleted = false,
                        Links = new[] {
                            new Link {
                                Id = Guid.NewGuid(),
                                Product = new Product {
                                    Id = Guid.NewGuid(),
                                    Name = "Product 9"
                                }
                            },
                            new Link {
                                Id = Guid.NewGuid(),
                                Product = new Product {
                                    Id = Guid.NewGuid(),
                                    Name = "Product 10"
                                }
                            },
                        }
                    },
                }
            };

            var options = GetOptions();

            using (var context = new TestDbContext(options))
            {

                context.Add(price1);
                context.Add(price2);
                context.SaveChanges();
            }

            using (var context = new TestDbContext(options))
            {

                var products = context.GetProducts(priceId).ToArray();
                Assert.That(products.Length, Is.EqualTo(4));
            }
        }

        #endregion


        [ Test ]
        public void GetDistributors_DistributorHasNoActivePrices_ReturnsNoDistributors()
        {
            var links = new[] {
                DbTestCaseFactory.WithActiveProductLink,
                DbTestCaseFactory.WithActiveProductLink,
                DbTestCaseFactory.WithDeletedProductLink
            };

            var priceRecord = DbTestCaseFactory.UsedNotDeletedPriceRecord;
            priceRecord.Links = links;

            var price = DbTestCaseFactory.InactivePrice;
            price.PriceRecords = new[] {
                priceRecord,
                DbTestCaseFactory.UsedNotDeletedPriceRecord
            };

            var distributor = DbTestCaseFactory.ActiveDistributor;
            distributor.Prices = new[] {
                price,
                DbTestCaseFactory.InactivePrice,
                DbTestCaseFactory.InactivePrice,
                DbTestCaseFactory.InactivePrice,
            };

            var options = GetOptions();

            using ( var context = new TestDbContext(options) ) {

                context.Distributors.Add( distributor );
                context.SaveChanges();
            }

            using ( var context = new TestDbContext(options) ) {

                var actualDistributors = context.GetDistributors().ToArray();
                Assert.That( actualDistributors.Length, Is.EqualTo( 0 ) );
            }
        }

        [ Test ]
        public void GetDistributors_DistributorHasActivePrices_WithUsedNotDeletedPriceRecords_ReturnsDistributorsWithPriceRecords()
        {
            var links = new[] {
                DbTestCaseFactory.WithActiveProductLink,
                DbTestCaseFactory.WithActiveProductLink,
                DbTestCaseFactory.WithDeletedProductLink
            };

            var priceRecord = DbTestCaseFactory.UsedNotDeletedPriceRecord;
            priceRecord.Links = links;

            var price = DbTestCaseFactory.ActivePrice;
            price.PriceRecords = new[] {
                priceRecord,
                DbTestCaseFactory.UsedNotDeletedPriceRecord
            };

            var distributor = DbTestCaseFactory.ActiveDistributor;
            distributor.Prices = new[] { price };

            var options = GetOptions();

            using ( var context = new TestDbContext(options) ) {

                context.Distributors.Add( distributor );
                context.SaveChanges();
            }

            using ( var context = new TestDbContext(options) ) {

                var actualDistributors = context.GetDistributors().ToArray();
                Assert.That( actualDistributors.Length, Is.EqualTo( 1 ) );

                Assert.That( actualDistributors[0].Prices.First().PriceRecords.Count(), Is.GreaterThan( 0 ) );
            }
        }

        [ Test ]
        public void GetDistributors_DistributorHasActivePrices_WithNotUsedOrDeletedPriceRecords_ReturnsNoDistributors()
        {
            var links = new[] {
                DbTestCaseFactory.WithActiveProductLink,
                DbTestCaseFactory.WithActiveProductLink,
                DbTestCaseFactory.WithDeletedProductLink
            };

            var priceRecord = DbTestCaseFactory.UsedDeletedPriceRecord;
            priceRecord.Links = links;

            var price = DbTestCaseFactory.ActivePrice;
            price.PriceRecords = new[] {
                priceRecord,
                DbTestCaseFactory.NotUsedDeletedPriceRecord,
                DbTestCaseFactory.NotUsedNotDeletedPriceRecord
            };

            var distributor = DbTestCaseFactory.ActiveDistributor;
            distributor.Prices = new[] { price };

            var options = GetOptions();

            using ( var context = new TestDbContext(options) ) {

                context.Distributors.Add( distributor );
                context.SaveChanges();
            }

            using ( var context = new TestDbContext(options) ) {

                var actualDistributors = context.GetDistributors().ToArray();
                Assert.That( actualDistributors.Length, Is.EqualTo( 0 ) );
            }
        }

        [ Test ]
        public void GetDistributors_DistributorHasActivePrices_WithUsedNotDeletedPriceRecords_WithNotDeletedProducts_ReturnsDistributorsWithProducts()
        {
            var links = new[] {
                DbTestCaseFactory.WithActiveProductLink,
                DbTestCaseFactory.WithActiveProductLink,
                DbTestCaseFactory.WithDeletedProductLink
            };

            var priceRecord = DbTestCaseFactory.UsedNotDeletedPriceRecord;
            priceRecord.Links = links;

            var price = DbTestCaseFactory.ActivePrice;
            price.PriceRecords = new[] {
                priceRecord,
                DbTestCaseFactory.UsedDeletedPriceRecord,
                DbTestCaseFactory.NotUsedDeletedPriceRecord,
                DbTestCaseFactory.NotUsedNotDeletedPriceRecord
            };

            var distributor = DbTestCaseFactory.ActiveDistributor;
            distributor.Prices = new[] { price };

            var options = GetOptions();

            using ( var context = new TestDbContext(options) ) {

                context.Distributors.Add( distributor );
                context.SaveChanges();
            }

            using ( var context = new TestDbContext(options) ) {

                var actualDistributors = context.GetDistributors().ToArray();
                Assert.That( actualDistributors.Length, Is.EqualTo( 1 ) );

                Assert.NotNull( actualDistributors[0].Prices.First().PriceRecords.First().Links.First().Product );
            }
        }


        #region factory
        // Insert factory methods here:

        private DbContextOptions< TestDbContext > GetOptions( [CallerMemberName] string dbName = null )
        {
            return new DbContextOptionsBuilder< TestDbContext >().UseInMemoryDatabase( dbName ?? "TestDb" ).Options;
        }

        #endregion
    }

}
