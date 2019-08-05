using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using TheSustainables.VendingMachine.Domain.Exceptions;

namespace TheSustainables.VendingMachine.Domain.Tests
{
    internal class CashTrayForTesting : CashTray
    {
        public CashTrayForTesting(IDictionary<Coin, int> Slots) : base(Slots)
        {
        }

        public new IDictionary<Coin, int> Slots => base.Slots;
    }

    public class CashTrayData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                100,
                new List<Coin> { new Coin(100) },
                new CashTrayForTesting(new Dictionary<Coin, int>() {
                    { new Coin(10), 100 },
                    { new Coin(20), 100 },
                    { new Coin(50), 100 },
                    {new Coin(100), 100 }
                }),
                new CashTrayForTesting(new Dictionary<Coin, int>() {
                    { new Coin(10), 100 },
                    { new Coin(20), 100 },
                    { new Coin(50), 100 },
                    {new Coin(100), 99 }
                })
            },

            new object[] {
                140,
                new List<Coin> { new Coin(100), new Coin(20), new Coin(20) },
                new CashTrayForTesting(new Dictionary<Coin, int>() {
                    { new Coin(10), 100 },
                    { new Coin(20), 100 },
                    { new Coin(50), 100 },
                    {new Coin(100), 100 }
                }),
                new CashTrayForTesting(new Dictionary<Coin, int>() {
                    { new Coin(10), 100 },
                    { new Coin(20), 98 },
                    { new Coin(50), 100 },
                    {new Coin(100), 99 }
                })
            },

            new object[] {
                140,
                new List<Coin> { new Coin(50), new Coin(50), new Coin(20), new Coin(20) },
                new CashTrayForTesting(new Dictionary<Coin, int>() {
                    { new Coin(10), 100 },
                    { new Coin(20), 100 },
                    { new Coin(50), 100 },
                    {new Coin(100), 0 }
                }),
                new CashTrayForTesting(new Dictionary<Coin, int>() {
                    { new Coin(10), 100 },
                    { new Coin(20), 98 },
                    { new Coin(50), 98 },
                    {new Coin(100), 0 }
                })
            },

            new object[] {
                150,
                new List<Coin> { new Coin(20), new Coin(20), new Coin(20), new Coin(20), new Coin(20), new Coin(20), new Coin(20), new Coin(10) },
                new CashTrayForTesting(new Dictionary<Coin, int>() {
                    { new Coin(10), 100 },
                    { new Coin(20), 100 },
                    { new Coin(50), 0 },
                    {new Coin(100), 0 }
                }),
                new CashTrayForTesting(new Dictionary<Coin, int>() {
                    { new Coin(10), 99 },
                    { new Coin(20), 93 },
                    { new Coin(50), 0 },
                    {new Coin(100), 0 }
                })
            },

            new object[] {
                20,
                new List<Coin> { new Coin(10), new Coin(10) },
                new CashTrayForTesting(new Dictionary<Coin, int>() {
                    { new Coin(10), 2 },
                    { new Coin(20), 0 },
                    { new Coin(50), 0 },
                    {new Coin(100), 0 }
                }),
                new CashTrayForTesting(new Dictionary<Coin, int>() {
                    { new Coin(10), 0 },
                    { new Coin(20), 0 },
                    { new Coin(50), 0 },
                    {new Coin(100), 0 }
                })
            },

             new object[] {
                20,
                new List<Coin> { new Coin(20) },
                new CashTrayForTesting(new Dictionary<Coin, int>() {
                    { new Coin(10), 0 },
                    { new Coin(20), 1 },
                    { new Coin(50), 0 },
                    {new Coin(100), 0 }
                }),
                new CashTrayForTesting(new Dictionary<Coin, int>() {
                    { new Coin(10), 0 },
                    { new Coin(20), 0 },
                    { new Coin(50), 0 },
                    {new Coin(100), 0 }
                })
            },
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class CashTrayTests
    {
        [Theory]
        [ClassData(typeof(CashTrayData))]
        internal void CashTray_ReturnChange_ReturnsCorrectAmmountWhenPossible(int ammoutToBeReturned, List<Coin> expectedCoins, CashTrayForTesting beforeCashTray, CashTrayForTesting afterCashTray)
        {
            var result = beforeCashTray.ReturnChange(ammoutToBeReturned);
            Assert.True(AreListsEqual(result, expectedCoins));
            Assert.True(beforeCashTray.Slots[new Coin(100)] == afterCashTray.Slots[new Coin(100)]);
            Assert.True(beforeCashTray.Slots[new Coin(50)] == afterCashTray.Slots[new Coin(50)]);
            Assert.True(beforeCashTray.Slots[new Coin(20)] == afterCashTray.Slots[new Coin(20)]);
            Assert.True(beforeCashTray.Slots[new Coin(10)] == afterCashTray.Slots[new Coin(10)]);
        }

        [Fact]
        internal void CashTray_ReturnChange_ThrowsExceptionWhenNotEnoughCashLeft()
        {
            var CashTray = new CashTrayForTesting(new Dictionary<Coin, int>() {
                    { new Coin(10), 1 },
                    { new Coin(20), 0 },
                    { new Coin(50), 0 },
                    { new Coin(100), 1 }
                });

            Assert.Throws<UnacceptableReturnAmmountException>(() =>
            {
                var result = CashTray.ReturnChange(120);
            });
        }

        [Fact]
        internal void CashTray_ReturnChange_ThrowsExceptionWhenNotReturnableAmmount()
        {
            var CashTray = new CashTrayForTesting(new Dictionary<Coin, int>() {
                    { new Coin(10), 1 },
                    { new Coin(20), 0 },
                    { new Coin(50), 0 },
                    { new Coin(100), 1 }
                });

            Assert.Throws<UnacceptableReturnAmmountException>(() =>
            {
                var result = CashTray.ReturnChange(101);
            });
        }
        private bool AreListsEqual<T>(List<T> a, List<T> b)
        {
            var setA = new HashSet<string>(a.Select(x => x.ToString()));
            var setB = new HashSet<string>(b.Select(x => x.ToString()));
            setA.ExceptWith(setB);
            return !setA.Any() && a.Count == b.Count;
        }

    }


}
