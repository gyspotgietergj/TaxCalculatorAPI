using TaxCalculatorAPI.Models;

namespace TaxCalculator.Common.Interfaces
{
    public interface ITaxCalculatorInterface
    {
        Task<List<PostalTaxTypes>> GetPostalTaxTypes();
        Task<PostalTaxTypes> GetPostalTaxDetail(int Id);
        Task<bool> CreatePostalTaxType(PostalTaxTypes postalTaxTypes);
        Task<bool> UpdatePostalTaxType(int id, PostalTaxTypes postalTaxTypes);
        Task<bool> DeletePostalTaxType(int id);
        Task<decimal> CalculateTax(TaxQuery query);
    }
}
