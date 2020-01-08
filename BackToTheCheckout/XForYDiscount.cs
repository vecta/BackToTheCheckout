using System.Collections.Generic;
using System.Linq;

namespace BackToTheCheckout
{
    internal class XForYDiscount
    {
        private readonly int _totalNeededForDiscount;
        private readonly decimal _discountPrice;
        private readonly decimal _normalItemPrice;

        public XForYDiscount(string sku, int totalNeededForDiscount, decimal discountPrice, decimal normalItemPrice)
        {
            Sku = sku;
            _totalNeededForDiscount = totalNeededForDiscount;
            _discountPrice = discountPrice;
            _normalItemPrice = normalItemPrice;
        }

        public string Sku { get; }

        public decimal Apply(List<string> basket)
        {
            var applicableItemCount = basket.Count(sku => sku == Sku);
            // ReSharper disable once PossibleLossOfFraction
            return (applicableItemCount / _totalNeededForDiscount * _discountPrice) +
                   ((applicableItemCount % _totalNeededForDiscount) * _normalItemPrice);
        }
    }
}