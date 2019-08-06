using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSustainables.VendingMachine.Domain;

namespace TheSustainables.VendingMachine.Host.Contracts.Output
{
    public class CoinOutput
    {
        public CoinOutput(Coin coin)
        {
            // in a real world example We could use a library like AutoMapper instead.
            Value = coin.Value;
        }
        public int Value { get; set; }
    }
}
