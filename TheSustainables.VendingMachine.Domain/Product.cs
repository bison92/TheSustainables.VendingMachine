using System;

namespace TheSustainables.VendingMachine.Domain
{
    public struct Product
    {
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
        public string Name { get; }
        public int Price { get; }

        public override int GetHashCode()
        {
            return HashCode.Combine<string, int>(Name, Price);
        }
    }
}



