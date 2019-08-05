using System.Collections.Generic;
using System.Linq;

namespace TheSustainables.VendingMachine.Domain
{
    public class Stock
    {
        private const int MINIMUM_AVAILABILITY = 1;
        private IDictionary<Product, int> Plates { get; } = new Dictionary<Product, int>();

        public void AddStock(Product product, int quantity)
        {
            if (!Plates.ContainsKey(product))
            {
                Plates.Add(product, 0);
            }
            Plates[product] += quantity;
        }

        internal IEnumerable<Product> GetAvailableProducts()
        {
            return Plates.Where(product => product.Value >= MINIMUM_AVAILABILITY).Select(p => p.Key);
        }
    }
}
