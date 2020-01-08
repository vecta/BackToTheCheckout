using System.Collections.Generic;
using System.Linq;

namespace BackToTheCheckout
{
    public class Checkout
    {
        private readonly List<string> _basket = new List<string>();
        public decimal Total => SumBasket();
        public void Scan(string sku) { _basket.Add(sku); }

        private decimal SumBasket()
        {
            var totalA99s = _basket.Count(sku => sku=="A99");
            // ReSharper disable once PossibleLossOfFraction
            var a99Skus = ((totalA99s / 3) * 1.3m) + ((totalA99s % 3) * 0.5m);
            return a99Skus + _basket.Where(s => s!="A99").Sum(GetPrice);
        }


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
    }
}