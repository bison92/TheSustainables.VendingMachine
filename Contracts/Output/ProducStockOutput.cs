using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheSustainables.VendingMachine.Host.Contracts.Output
{
    public class ProductsStockOutput
    {
        public ICollection<ProductOutput> Products { get; } = new List<ProductOutput>();
    }
}
