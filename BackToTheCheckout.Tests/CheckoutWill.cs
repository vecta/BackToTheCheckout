using System.Collections.Generic;
using NUnit.Framework;

namespace BackToTheCheckout.Tests
{
    [TestFixture]
    public class CheckoutWill
    {
        private const string DiscountItem1Sku = "A99";
        private const string DiscountItem2Sku = "B15";
        private const string NonDiscountedItemSku = "C40";
        private const decimal DiscountItem1NormalPrice = 0.5m;
        private const decimal DiscountItem2NormalPrice = 0.3m;
        private const decimal NonDiscountItemNormalPrice = 0.6m;
        private const decimal Discount1Price = 1.3m;
        private const decimal Discount2Price = 0.45m;
        private Checkout _checkout;

        [SetUp]
        public void Setup()
        {
            var skus = new List<(string sku, decimal price)>
            {
                (DiscountItem1Sku, DiscountItem1NormalPrice),
                (DiscountItem2Sku, DiscountItem2NormalPrice),
                (NonDiscountedItemSku, NonDiscountItemNormalPrice)
            };
            var discounts =
                new List<XForYDiscount>()
                {
                    new XForYDiscount(DiscountItem1Sku, 3, Discount1Price), 
                    new XForYDiscount(DiscountItem2Sku, 2, Discount2Price)
                };
            _checkout = new Checkout(skus, discounts);
        }

        [Test]
        public void ReturnAZeroTotalWhenNothingIsScanned()
        {
            Assert.That(_checkout.Total, Is.EqualTo(0m));
        }

        [Test]
        public void ReturnItemPriceWhenASingleItemIsScanned()
        {
            _checkout.Scan(DiscountItem1Sku);
            Assert.That(_checkout.Total, Is.EqualTo(DiscountItem1NormalPrice));
        }

        [Test]
        public void ReturnSumOfItemsPriceWhenNoValidDiscountedItemsAreScanned()
        {
            _checkout.Scan(DiscountItem1Sku);
            _checkout.Scan(DiscountItem2Sku);
            _checkout.Scan(NonDiscountedItemSku);
            Assert.That(_checkout.Total,
                Is.EqualTo(DiscountItem1NormalPrice + DiscountItem2NormalPrice + NonDiscountItemNormalPrice));
        }

        [Test]
        public void ReturnDiscountedPriceIfItemsScannedAreInAValidDiscount()
        {
            _checkout.Scan(DiscountItem1Sku);
            _checkout.Scan(DiscountItem1Sku);
            _checkout.Scan(DiscountItem1Sku);
            Assert.That(_checkout.Total, Is.EqualTo(Discount1Price));
        }

        [Test]
        public void ReturnDiscountedPriceIfItemsScannedAreInASecondValidDiscount()
        {
            _checkout.Scan(DiscountItem2Sku);
            _checkout.Scan(DiscountItem2Sku);
            Assert.That(_checkout.Total, Is.EqualTo(Discount2Price));
        }
        [Test]
        public void ReturnBasketTotalIncludingDiscountedAndNonDiscountedItems()
        {
            _checkout.Scan(DiscountItem2Sku);
            _checkout.Scan(NonDiscountedItemSku);
            _checkout.Scan(DiscountItem1Sku);
            _checkout.Scan(DiscountItem2Sku);
            _checkout.Scan(DiscountItem1Sku);
            _checkout.Scan(DiscountItem2Sku);
            _checkout.Scan(DiscountItem1Sku);
            _checkout.Scan(NonDiscountedItemSku);
            _checkout.Scan(DiscountItem1Sku);
            Assert.That(_checkout.Total,
                Is.EqualTo(Discount1Price + DiscountItem1NormalPrice + Discount2Price + DiscountItem2NormalPrice +
                           NonDiscountItemNormalPrice + NonDiscountItemNormalPrice));
        }
    }
}
