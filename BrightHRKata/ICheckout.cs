namespace BrightHRKata;

public interface ICheckout
{
    void Scan(string item);
    int GetTotalPrice();
    void UpdatePriceChanges();
}