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

        protected CashTray(IDictionary<Coin, int> Slots)
        {
            this.Slots = Slots;
        }

        protected IDictionary<Coin, int> Slots { get; } = new Dictionary<Coin, int>();
        private KeyValuePair<Coin, int>[] NonEmptySlotsOrderedDesc => Slots.Where(s => s.Value > 0).OrderByDescending(s => s.Key.Value).ToArray();

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

        public List<Coin> ReturnChange(int ammount)
        {
            var result = new List<Coin>();
            int remainder = ammount;
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
                            throw new UnacceptableReturnAmmountException("Unable to return the requested ammout with the available coins.");
                        }
                    }
                }
                else
                {
                    throw new UnacceptableReturnAmmountException("Unable to return the requested ammout with the available coins.");
                }
            }
            return result;
        }
    }
}
