using BrightHRKata.Exceptions;

namespace BrightHRKata;

public class Checkout : ICheckout
{
    private readonly Dictionary<char, int> _basket = new();
    private Dictionary<string, int> _pricingRules;
    
    
    private readonly IPricingRulesProvider _pricingRulesProvider;
    public Checkout(IPricingRulesProvider pricingRulesProvider)
    {
        _pricingRulesProvider = pricingRulesProvider;
        _pricingRules = GetPricingRules();
    }
    public void Scan(string item)
    {
        foreach (var ch in item)
        {
            Scan(ch);
        }
    }

    private void Scan(Char item)
    {
        if (!_basket.TryAdd(item, 1))
            _basket[item] += 1;
    }

    public int GetTotalPrice()
    {
        var itemString = "";
        foreach (var item in _basket.Keys)
        {
            for (int i = 0; i < _basket[item]; i++)
            {
                itemString += $"{item}";
            }
        }
        return StringToPriceConverter(itemString);
    }

    private int StringToPriceConverter(string itemString)
    {
        var orderedPricingRules = _pricingRules.OrderByDescending(t => t.Key.Length);
        foreach (var rule in orderedPricingRules)
        {
            if (itemString.Contains(rule.Key))
            {
                itemString= itemString.Replace(rule.Key, $"|{rule.Value}|");
            }
        }
        var listOfCosts = itemString.Split('|').ToList();
        listOfCosts = listOfCosts.Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
        var totalPrice = 0;
        foreach (var cost in listOfCosts)
        {
            if (cost.All(char.IsDigit))
            {
                var price = Convert.ToInt32(cost);
                totalPrice += price;
            }
            else
            {
                throw new ItemNotFoundException();
            }
        }

        return totalPrice;
    }

    private Dictionary<string,int> GetPricingRules()
    {
        return _pricingRulesProvider.ProvideLatestRules();
    }
    
}