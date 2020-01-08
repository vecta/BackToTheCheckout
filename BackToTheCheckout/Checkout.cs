using System.Collections.Generic;
using System.Linq;

namespace BackToTheCheckout
{
    public class Checkout
    {
        private readonly List<string> _basket = new List<string>();
        public decimal Total => SumBasket();

        private decimal SumBasket() { return _basket.Sum(GetPrice); }
        private decimal GetPrice(string sku) { return 0.5m; }
        public void Scan(string sku) { _basket.Add(sku); }
    }
}