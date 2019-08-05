using System.Globalization;

namespace TheSustainables.VendingMachine.Domain
{
    /// <summary>Coin class representing a coin with a fixed value. ie: 5c coin</summary>
    public struct Coin
    {
        public Coin(int value)
        {
            Value = value;
        }
        /// <summary>Gets or sets the value in cents.</summary>
        /// <value>The value in cents.</value>
        public int Value { get; }

        /// <summary>Returns a hash code for this instance, as there's only one coin per value, the hashcode is the actual value.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return Value;
        }

        /// <summary>Converts to string by getting the Euro value and applying currency formatting based on nl-NL culture info.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return (Value / 100d).ToString("C", CultureInfo.GetCultureInfoByIetfLanguageTag("nl-NL")); // The Sustainables maken duurzaamheid makkelijk :)
        }
    }
}
