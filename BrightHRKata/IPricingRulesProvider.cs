namespace BrightHRKata;

public interface IPricingRulesProvider
{
    Dictionary<string,int> ProvideLatestRules();
}