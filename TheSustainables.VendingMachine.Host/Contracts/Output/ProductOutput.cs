using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TheSustainables.VendingMachine.Domain;

namespace TheSustainables.VendingMachine.Host.Contracts.Output
{
    public class ProductOutput
    {

        public ProductOutput(Product product)
        {
            // in a real world example We could use a library like AutoMapper instead.
            Id = product.Id;
            Name = product.Name;
            Price = (product.Price /100d).ToString("C", CultureInfo.GetCultureInfoByIetfLanguageTag("nl-NL"));
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
    }
}
