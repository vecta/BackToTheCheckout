using NUnit.Framework;

namespace BackToTheCheckout.Tests
{
    [TestFixture]
    public class CheckoutWill
    {
        [Test]
        public void ReturnAZeroTotalWhenNothingIsScanned()
        {
            var checkout = new Checkout();
            Assert.That(checkout.Total, Is.EqualTo(0m));
        }

        [Test]
        public void ReturnItemPriceWhenASingleItemIsScanned()
        {
            var checkout = new Checkout();
            checkout.Scan("A99");
            Assert.That(checkout.Total, Is.EqualTo(0.5m));
        }

        [Test]
        public void ReturnSumOfItemsPriceWhenNoValidDiscountedItemsAreScanned()
        {
            var checkout = new Checkout();
            checkout.Scan("A99");
            checkout.Scan("B15");
            checkout.Scan("C40");
            Assert.That(checkout.Total, Is.EqualTo(1.4m));
        }

        [Test]
        public void ReturnDiscountedPriceIfItemsScannedAreInAValidDiscount()
        {
            var checkout = new Checkout();
            checkout.Scan("A99");
            checkout.Scan("A99");
            checkout.Scan("A99");
            Assert.That(checkout.Total, Is.EqualTo(1.3m));
        }

        [Test]
        public void ReturnDiscountedPriceIfItemsScannedAreInASecondValidDiscount()
        {
            var checkout = new Checkout();
            checkout.Scan("B15");
            checkout.Scan("B15");
            Assert.That(checkout.Total, Is.EqualTo(0.45m));
        }
        [Test]
        public void ReturnBasketTotalIncludingDiscountedAndNonDiscountedItems()
        {
            var checkout = new Checkout();
            checkout.Scan("B15");
            checkout.Scan("C40");
            checkout.Scan("A99");
            checkout.Scan("B15");
            checkout.Scan("A99");
            checkout.Scan("B15");
            checkout.Scan("A99");
            checkout.Scan("C40");
            checkout.Scan("A99");
            Assert.That(checkout.Total, Is.EqualTo(3.75m));
        }
    }
}
