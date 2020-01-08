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
            Assert.That(checkout.Total, Is.EqualTo(0));
        }
    }
}
