using System;
using System.Collections.Generic;

namespace TheSustainables.VendingMachine.Domain
{
    public class Machine
    {
        /// <summary>
        /// <para>
        /// Initializes a new instance of the <see cref="Machine"/> class with the inital data specified in the assignment.
        /// </para>
        ///   <para>Vending machine contains the following products:</para>
        ///   <list type="bullet">
        ///     <item>Tea (1.30 eur), 10 portions</item>
        ///     <item>Espresso (1.80 eur), 20 portions</item>
        ///     <item>Juice  (1.80 eur), 20 portions</item>
        ///     <item>Chicken soup (1.80 eur), 15 portions</item>
        ///   </list>
        ///   <para>At the start the vending machine wallet contains the following coins (for exchange):</para>
        ///   <list type="bullet">
        ///     <item>10 cent, 100 coins</item>
        ///     <item>20 cent, 100 coins</item>
        ///     <item>50 cent, 100 coins</item>
        ///     <item>1 euro, 100 coins<br /></item>
        ///   </list>
        /// </summary>
        
        public Machine()
        {
            //stock
            Stock.AddStock(new Product("Tea", 130), 10);
            Stock.AddStock(new Product("Expresso", 180), 20);
            Stock.AddStock(new Product("Juice", 180), 20);
            Stock.AddStock(new Product("Chicken soup", 180), 15);
            //cashtray
            CashTray.AddCash(new Coin(10), 100);
            CashTray.AddCash(new Coin(20), 100);
            CashTray.AddCash(new Coin(50), 100);
            CashTray.AddCash(new Coin(100), 100);
        }
        public Guid Id { get; set; }
        public IEnumerable<Product> AvailableProducts => Stock.GetAvailableProducts();
        public Stock Stock { get; } = new Stock();
        public CashTray CashTray { get; } = new CashTray();
        public CashTray UserCashTray { get; } = new CashTray();
        public CashTray CombinedCashTray => CashTray.Merge(UserCashTray);
    }
}
