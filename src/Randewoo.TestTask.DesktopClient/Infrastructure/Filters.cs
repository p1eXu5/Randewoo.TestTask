using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Randewoo.TestTask.DesktopClient.Infrastructure
{
    public static class Filters
    {
        private static readonly string[] _filters = new[] {
            "Все",
            "Минимальная цена у других",
            "Минимальная цена здесь",
        };

        public static string All => _filters[0];
        public static string MinOthers => _filters[1];
        public static string MinSelf => _filters[2];

        public static IEnumerable< string > FilterItems => _filters;
    }
}
