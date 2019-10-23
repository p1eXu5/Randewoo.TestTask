using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Randewoo.TestTask.DataContext.Models;

namespace Randewoo.TestTask.DataContext.Tests.UnitTests
{

    [TestFixture]
    public class TestDbContextExtensionsTests
    {
        private SqliteConnection _connection;
        private DbContextOptions< TestDbContext > _options;

        //[ SetUp ]
        public void OpenConnection()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            _options = new DbContextOptionsBuilder< TestDbContext >().UseSqlite( _connection ).Options;
        }

        //[ TearDown ]
        public void CloseConnection()
        {
            _connection?.Close();
        }

        [Test]
        public void GetProducts_PriceActive_PriceRecordsNotDeletetAndIsUsed_ReturnsAllProduct()
        {
            var priceId = Guid.NewGuid();

            var price1 = new Price {
                Id = priceId,
                IsActive = true,
                Name = "Test Price 1",
                Sheet = "Test Sheet 1",
                Namerange = "Test NameRange 1",
                Pricerange = "",
                PricesRecords = new[] {
                    new PricesRecords {
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
                    new PricesRecords {
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
                    new PricesRecords {
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
                    new PricesRecords {
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

            var price2 = new Price {
                Id = Guid.NewGuid(),
                IsActive = true,
                Name = "Test Price 2",
                Sheet = "Test Sheet 2",
                Namerange = "Test NameRange 2",
                Pricerange = "",
                PricesRecords = new[] {
                    new PricesRecords {
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

            var options = GetOptions( "Test 1" );

            using ( var context = new TestDbContext(options) ) {

                context.Add( price1 );
                context.Add( price2 );
                context.SaveChanges();

                var p = context.Prices.ToArray();
            }

            using ( var context = new TestDbContext(options) ) {

                var products = context.GetProducts( priceId ).ToArray();
                Assert.That( products.Length, Is.EqualTo( 4 ) );
            }
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
