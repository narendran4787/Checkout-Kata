namespace BrightHRKata;

public class Checkout : ICheckout
{
    private readonly IPricingRulesProvider _pricingRulesProvider;
    public Checkout(IPricingRulesProvider pricingRulesProvider)
    {
        _pricingRulesProvider = pricingRulesProvider;
    }
    public void Scan(string item)
    {
        throw new NotImplementedException();
    }

    public int GetTotalPrice()
    {
        throw new NotImplementedException();
    }
    
    
}