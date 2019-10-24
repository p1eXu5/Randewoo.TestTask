using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Randewoo.TestTask.DataContext.Models;

namespace Randewoo.TestTask.DataContext.Tests.UnitTests.Factories
{
    public class DbTestCaseFactory
    {
        public static Guid ActivePriceId => Guid.NewGuid();
        public static Product ActiveProduct
        {
            get {
                var id = Guid.NewGuid();
                return new Product { Id = id, Name = id.ToString("B"), Deleted = false };
            }
        }

        public static Product DeletedProduct
        {
            get {
                var id = Guid.NewGuid();
                return new Product { Id = id, Name = id.ToString("B"), Deleted = true };
            }
        }

        public static Link WithActiveProductLink => new Link { Id = Guid.NewGuid(), Product = ActiveProduct };
        public static Link WithDeletedProductLink => new Link { Id = Guid.NewGuid(), Product = DeletedProduct };

        private static int _priceRecordIndex = 1;

        public static PriceRecord UsedNotDeletedPriceRecord
            => new PriceRecord { RecordIndex = _priceRecordIndex++, Used = true, Deleted = false, };

        public static PriceRecord UsedDeletedPriceRecord
            => new PriceRecord { RecordIndex = _priceRecordIndex++, Used = true, Deleted = true, };

        public static PriceRecord NotUsedNotDeletedPriceRecord
            => new PriceRecord { RecordIndex = _priceRecordIndex++, Used = false, Deleted = false, };
        public static PriceRecord NotUsedDeletedPriceRecord
            => new PriceRecord { RecordIndex = _priceRecordIndex++, Used = false, Deleted = true, };

        public static Distributor ActiveDistributor
            => new Distributor { Id = Guid.NewGuid(), Active = true };

        public static Distributor InactiveDistributor
            => new Distributor { Id = Guid.NewGuid(), Active = false };

        public static Price ActivePrice 
            => new Price {
                Id = Guid.NewGuid(),
                IsActive = true,
                Name = "Test Price 1",
                Sheet = "Test Sheet 1",
                Namerange = "Test NameRange 1",
                Pricerange = "" };

        public static Price InactivePrice 
            => new Price {
                Id = Guid.NewGuid(),
                IsActive = false,
                Name = "Test Price 1",
                Sheet = "Test Sheet 1",
                Namerange = "Test NameRange 1",
                Pricerange = "" };

        public static IEnumerable DistributorTestCases()
        {
            yield return new TestCaseData().SetName( "Test #1" );
        }
    }
}
