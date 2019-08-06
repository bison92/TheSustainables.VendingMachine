using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Serilog;
using TheSustainables.VendingMachine.Domain;
using TheSustainables.VendingMachine.Domain.Exceptions;
using TheSustainables.VendingMachine.Host.Contracts.Input;
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

        [HttpPost("products/purchase")]
        public ActionResult<PurchaseOutput> PurchaseProduct([FromBody]PurchaseProductModel model)
        {
            var result = new PurchaseOutput();
            try
            {
                var change = VendingMachine.Sell(model.ProductId);
                result.Change = change.Select(c => new CoinOutput(c)).ToList();
                Log.Information($"Sold product [{model.ProductId}]");
                return result;
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case NotEnoughCreditException exception:
                        result.Succeed = false;
                        result.Error = "Insufficient amount";
                        break;
                    case NotEnoughStockException exception:
                        result.Succeed = false;
                        result.Error = "Insufficient stock";
                        break;
                    case UnacceptableReturnAmountException exception:
                        result.Succeed = false;
                        result.Error = "No change. Please try with exact amount.";
                        break;
                    case UnknownProductIdException exception:
                        result.Succeed = false;
                        result.Error = "Unknown product";
                        break;
                    default:
                        result.Succeed = false;
                        result.Error = "Unknown error";
                        break;
                }
                Log.Warning(ex, "Unable to sell product.");
                result.Change.Clear();
                return result;
            }
        }

        [HttpGet("userCashTray")]
        public ActionResult<int> GetInsertedCoins()
        {
            return VendingMachine.UserCashTray.GetTotalCashInTray();
        }

        [HttpPut("userCashTray")]
        public ActionResult<int> InsertCoin([FromBody]InsertCoinModel model)
        {
            if (model.Value > 0)
            {
                VendingMachine.UserCashTray.AddCash(new Coin(model.Value), 1);
                return VendingMachine.UserCashTray.GetTotalCashInTray();
            }
            else
            {
                return new BadRequestResult();
            }
        }

        [HttpDelete("userCashTray")]
        public ActionResult<List<CoinOutput>> ReturnCoins()
        {
            var result = VendingMachine.UserCashTray.Empty();
            var response = result.Select(c => new CoinOutput(c));
            return response.ToList();
        }
    }
}
