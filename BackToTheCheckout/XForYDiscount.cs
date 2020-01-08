using System.Collections.Generic;
using System.Linq;

namespace BackToTheCheckout
{
    public class XForYDiscount
    {
        private readonly int _totalNeededForDiscount;
        private readonly decimal _discountPrice;

        public XForYDiscount(string sku, int totalNeededForDiscount, decimal discountPrice)
        {
            Sku = sku;
            _totalNeededForDiscount = totalNeededForDiscount;
            _discountPrice = discountPrice;
        }

        public string Sku { get; }

        public decimal Apply(List<string> basket, decimal nonDiscountedPrice)
        {
            var applicableItemCount = basket.Count(sku => sku == Sku);
            // ReSharper disable once PossibleLossOfFraction
            return (applicableItemCount / _totalNeededForDiscount * _discountPrice) +
                   ((applicableItemCount % _totalNeededForDiscount) * nonDiscountedPrice);
        }
    }
}