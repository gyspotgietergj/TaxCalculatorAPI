using TaxCalculatorAPI.Models;

namespace TaxCalculator.Common.Interfaces
{
    public interface ITaxCalculatorInterface
    {
        Task<List<PostalTaxTypes>> GetPostalTaxTypes();
        Task<PostalTaxTypes> GetPostalTaxDetail(int Id);
    }
}
