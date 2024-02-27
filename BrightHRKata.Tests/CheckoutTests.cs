using BrightHRKata.Exceptions;
using Moq;

namespace BrightHRKata.Tests;

[TestClass]
public class CheckoutTests
{
    [TestMethod]
    public void GivenItemsScannedIteratively_ProducesCorrectResult_WhenNoRepeatedItems()
    {
        var rulesProvider = new Mock<IPricingRulesProvider>();
        rulesProvider.Setup(t => t.ProvideLatestRules()).Returns(new Dictionary<string, int>()
        {
            {"A",150},
            {"B", 170},
            {"C",210},
            {"AAA", 350},
            {"BB", 310}
        });
        var checkout = new Checkout(rulesProvider.Object);
        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("C");

        var totalPriceActual = checkout.GetTotalPrice();
        Assert.AreEqual(530,totalPriceActual);
    }
    
    [TestMethod]
    public void GivenItemsScannedTogetherAsString_ProducesCorrectResult_WhenNoRepeatedItems()
    {
        var rulesProvider = new Mock<IPricingRulesProvider>();
        rulesProvider.Setup(t => t.ProvideLatestRules()).Returns(new Dictionary<string, int>()
        {
            {"A",150},
            {"B", 170},
            {"C",210},
            {"AAA", 350},
            {"BB", 310}
        });
        var checkout = new Checkout(rulesProvider.Object);
        checkout.Scan("ACB");

        var totalPriceActual = checkout.GetTotalPrice();
        Assert.AreEqual(530,totalPriceActual);
    }

    [TestMethod]
    public void GivenItemsScannedIteratively_ProducesCorrectResult_WhenRepeatedItemsAndMultiPriceItem()
    {
        var rulesProvider = new Mock<IPricingRulesProvider>();
        rulesProvider.Setup(t => t.ProvideLatestRules()).Returns(new Dictionary<string, int>()
        {
            {"A",150},
            {"B", 170},
            {"C",210},
            {"AAA", 350},
            {"BB", 310}
        });
        var checkout = new Checkout(rulesProvider.Object);
        
        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("A");
        checkout.Scan("A");
        checkout.Scan("C");
        checkout.Scan("B");
        checkout.Scan("B");

        var totalPriceActual = checkout.GetTotalPrice();
        Assert.AreEqual(1040,totalPriceActual);
    }
    
    [TestMethod]
    public void GivenItemsScannedTogetherAsAString_ProducesCorrectResult_WhenRepeatedItemsAndMultiPriceItem()
    {
        var rulesProvider = new Mock<IPricingRulesProvider>();
        rulesProvider.Setup(t => t.ProvideLatestRules()).Returns(new Dictionary<string, int>()
        {
            {"A",150},
            {"B", 170},
            {"C",210},
            {"AAA", 350},
            {"BB", 310}
        });
        var checkout = new Checkout(rulesProvider.Object);
        checkout.Scan("ABAACBB");

        var totalPriceActual = checkout.GetTotalPrice();
        Assert.AreEqual(1040,totalPriceActual);
    }
    
    [TestMethod]
    [ExpectedException(typeof(ItemNotFoundException))]
    public void GivenItemsScannedTogetherAsAString_ProducesException_WhenScannedItemDoesNotExist()
    {
        var rulesProvider = new Mock<IPricingRulesProvider>();
        rulesProvider.Setup(t => t.ProvideLatestRules()).Returns(new Dictionary<string, int>()
        {
            {"A",150},
            {"B", 170},
            {"C",210},
            {"AAA", 350}
        });
        var checkout = new Checkout(rulesProvider.Object);
        checkout.Scan("ABAACBZ");
    }
}