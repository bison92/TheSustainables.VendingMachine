using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheSustainables.VendingMachine.Host.Contracts.Output
{
    public class PurchaseOutput
    {
        public List<CoinOutput> Change { get; set; } = new List<CoinOutput>();
        public bool Succeed { get; set; } = true;
        public string Error { get; set; }
    }
}
