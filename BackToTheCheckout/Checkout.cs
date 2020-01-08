using System.Collections.Generic;
using System.Linq;

namespace BackToTheCheckout
{
    public class Checkout
    {
        private readonly List<string> _basket = new List<string>();
        public decimal Total => SumBasket();

        private decimal SumBasket() { return _basket.Sum(GetPrice); }

        private decimal GetPrice(string sku)
        {
            return sku switch
            {
                "A99" => 0.5m,
                "B15" => 0.3m,
                "C40" => 0.6m,
                _ => 0
            };
        }
        public void Scan(string sku) { _basket.Add(sku); }
    }
}