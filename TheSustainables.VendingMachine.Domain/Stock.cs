using System;
using System.Collections.Generic;
using System.Linq;
using TheSustainables.VendingMachine.Domain.Exceptions;

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

        public IEnumerable<Product> SubstractStock(Guid ProductId, int quantity)
        {
            var plate = Plates.First(p => p.Key.Id == ProductId);
            if(plate.Value > 0)
            {
                Plates[plate.Key] -= quantity;
                var result = Enumerable.Range(1, quantity).Select(i => plate.Key);
                return result;
            }
            else
            {
                throw new NotEnoughStockException($"There's no enought stock of Product: [{plate.Key.Name}] with Id [{plate.Key.Id}]");
            }
        }

        internal IEnumerable<Product> GetAvailableProducts()
        {
            return Plates.Where(product => product.Value >= MINIMUM_AVAILABILITY).Select(p => p.Key);
        }
    }
}
