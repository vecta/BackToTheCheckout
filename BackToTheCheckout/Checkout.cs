using System;
using System.Collections.Generic;
using System.Linq;

namespace BackToTheCheckout
{
    public class Checkout
    {
        private readonly List<(string sku, decimal price)> _skus;
        private readonly List<string> _basket = new List<string>();
        private readonly List<XForYDiscount> _xForYDiscounts;

        public Checkout(List<(string sku, decimal price)> skus, List<XForYDiscount> discounts)
        {
            _skus = skus;
            _xForYDiscounts = discounts;
        }

        public decimal Total => SumBasket();
        public void Scan(string sku) { _basket.Add(sku); }

        private decimal SumBasket()
        {
            var total = _xForYDiscounts.Sum(discount => discount.Apply(_basket, GetPrice(discount.Sku))); 
            var discountedSkus = _xForYDiscounts.Select(discount => discount.Sku);
            return total + _basket.Where(sku => !discountedSkus.Contains(sku)).Sum(GetPrice);
        }

        private decimal GetPrice(string sku)
        {
            return _skus.Single(skuPrice => skuPrice.sku==sku).price;
        }
    }
}