using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TheSustainables.VendingMachine.Domain;
using TheSustainables.VendingMachine.Host.Contracts.Output;

namespace TheSustainables.VendingMachine.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MachineController
    {
        public Machine VendingMachine { get; }

        public MachineController(Machine vendingMachine)
        {
            VendingMachine = vendingMachine;
        }

        [HttpGet("products")]
        public ActionResult<ProductsStockOutput> GetProducts()
        {
            var result = new ProductsStockOutput();
            foreach (var product in VendingMachine.AvailableProducts)
            {
                result.Products.Add(new ProductOutput(product));
            }
            return result;
        }

    }
}
