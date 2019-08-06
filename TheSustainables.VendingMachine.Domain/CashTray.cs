using System;
using System.Collections.Generic;
using System.Linq;
using TheSustainables.VendingMachine.Domain.Exceptions;

namespace TheSustainables.VendingMachine.Domain
{
    public class CashTray
    {
        public CashTray()
        {

        }

        /// <summary>Initializes a new instance of the <see cref="CashTray"/> class. This constructor is for testing only.</summary>
        /// <param name="Slots">The slots.</param>
        protected CashTray(IDictionary<Coin, int> Slots)
        {
            this.Slots = Slots;
        }

        protected IDictionary<Coin, int> Slots { get; } = new Dictionary<Coin, int>();

        private KeyValuePair<Coin, int>[] NonEmptySlotsOrderedDesc => Slots.Where(s => s.Value > 0).OrderByDescending(s => s.Key.Value).ToArray();

        /// <summary>Merges this CashTray with specified other CashTray.</summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public CashTray Merge(CashTray other)
        {
            var result = new CashTray();
            foreach(var slot in this.Slots.Concat(other.Slots))
            {
                result.AddCash(slot.Key, slot.Value);
            }
            return result;
        }

        /// <summary>
        ///   <para>Adds the specified quantity to the provided coin slot. Creates the slot if not already present.
        /// </para>
        /// </summary>
        /// <param name="coin">The coin we're adding.</param>
        /// <param name="quantity">The quantity of coins.</param>
        public void AddCash(Coin coin, int quantity)
        {
            if (!Slots.ContainsKey(coin))
            {
                Slots.Add(coin, 0);
            }
            Slots[coin] += quantity;
        }

        /// <summary>Determines whether this instance [can return specified amount of change].</summary>
        /// <param name="amount">The amount.</param>
        /// <param name="change">The change.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can return change] the specified amount; otherwise, <c>false</c>.</returns>
        public bool CanReturnChange(int amount, out List<Coin> change)
        {
            var result = true;
            change = new List<Coin>();
            int remainder = amount;
            var coinList = Slots.Where(s => s.Value > 0).OrderByDescending(s => s.Key.Value).ToArray();
            while (remainder != 0)
            {
                if (coinList.Length > 0)
                {
                    for (var i = 0; i < coinList.Length; i++)
                    {
                        var coin = coinList[i].Key;
                        var quantity = coinList[i].Value;
                        if (remainder >= coin.Value && quantity > 0)
                        {
                            change.Add(coin);
                            var newEntry = new KeyValuePair<Coin, int>(coin, --quantity);
                            coinList[i] = newEntry;
                            remainder -= coin.Value;
                            break;
                        }
                        if (i == coinList.Length - 1)
                        {
                            result = false;
                            remainder = 0;
                            change.Clear();
                        }
                    }
                }
                else
                {
                    result = false;
                    remainder = 0;
                    change.Clear();
                }
            }
            return result;
        }

        /// <summary>Removes the coins from slots.</summary>
        /// <param name="coins">The coins.</param>
        public void RemoveCoins(List<Coin> coins)
        {
            foreach(Coin coin in coins)
            {
                Slots[coin]--;
            }
        }

        /// <summary>Returns change by using the smallest amount of coins available.</summary>
        /// <param name="amount">The amount to be returned.</param>
        /// <returns> a <see cref="List{T}"/> of <see cref="Coin"/> representing the requested amount</returns>
        /// <exception cref="UnacceptableReturnAmountException"> if unable to return the requested amount with the available coins.
        [Obsolete("This method would leave the cashtray in a wrong state in case of exception")]
        public List<Coin> ReturnChange(int amount)
        {
            var result = new List<Coin>();
            int remainder = amount;
            while (remainder != 0)
            {
                if (NonEmptySlotsOrderedDesc.Length > 0)
                {
                    for (var i = 0; i < NonEmptySlotsOrderedDesc.Length; i++)
                    {
                        var coin = NonEmptySlotsOrderedDesc[i].Key;
                        var quantity = NonEmptySlotsOrderedDesc[i].Value;
                        if (remainder >= coin.Value && quantity > 0)
                        {
                            result.Add(coin);
                            Slots[coin] -= 1;
                            remainder -= coin.Value;
                            break;
                        }
                        if (i == NonEmptySlotsOrderedDesc.Length - 1)
                        {
                            throw new UnacceptableReturnAmountException("Unable to return the requested amout with the available coins.");
                        }
                    }
                }
                else
                {
                    throw new UnacceptableReturnAmountException("Unable to return the requested amout with the available coins.");
                }
            }
            return result;
        }

        /// <summary>Gets the total cash in tray.</summary>
        /// <returns>Total cash in tray expressed in cents.</returns>
        public int GetTotalCashInTray()
        {
            return Slots.Aggregate(0, (a, b) => a += b.Key.Value * b.Value);
        }

        /// <summary>Gets all coins in tray.</summary>
        /// <returns>List of all coins</returns>
        public IEnumerable<Coin> GetAllCoinsInTray()
        {
            return Slots.SelectMany(kvp => Enumerable.Range(1, kvp.Value).Select(i => kvp.Key));
        }

        /// <summary>Empties this instance.</summary>
        /// <returns></returns>
        public IEnumerable<Coin> Empty()
        {
            var result = Slots.SelectMany(kvp => Enumerable.Range(1, kvp.Value).Select(i => kvp.Key)).ToList();
            Slots.Clear();
            return result;
        }
    }
}
