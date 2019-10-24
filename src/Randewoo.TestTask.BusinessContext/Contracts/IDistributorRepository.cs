using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Randewoo.TestTask.DataContext.Models;

namespace Randewoo.TestTask.BusinessContext.Contracts
{
    public interface IDistributorRepository
    {
        IEnumerable< Distributor > Distributors { get; }
    }
}
