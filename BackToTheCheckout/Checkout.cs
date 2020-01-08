using System.Collections.Generic;
using System.Linq;

namespace BackToTheCheckout
{
    public class Checkout
    {
        private readonly List<string> _basket = new List<string>();
        private readonly List<XForYDiscount> _xForYDiscounts;

        public Checkout()
        {
            _xForYDiscounts = new List<XForYDiscount>()
                {
                    new XForYDiscount("A99", 3, 1.3m, GetPrice("A99")),
                    new XForYDiscount("B15", 2, 0.45m, GetPrice("B15"))
                };
        }

        public decimal Total => SumBasket();
        public void Scan(string sku) { _basket.Add(sku); }

        private decimal SumBasket()
        {
            var total = _xForYDiscounts.Sum(discount => discount.Apply(_basket));
            var discountedSkus = _xForYDiscounts.Select(discount => discount.Sku);
            return total + _basket.Where(sku => !discountedSkus.Contains(sku)).Sum(GetPrice);
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