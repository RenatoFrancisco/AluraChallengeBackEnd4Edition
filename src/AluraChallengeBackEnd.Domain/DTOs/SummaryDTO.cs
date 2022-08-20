namespace AluraChallengeBackEnd.Domain.DTOs;

public class SummaryDTO
{
    public decimal TotalIncomes { get; set; }        
    public decimal TotalExpenditures { get; set; }        
    public decimal Balance { get; set; }        
    public IEnumerable<CategoryExpendituresDTO> GroupBy { get; set; }        
}