namespace AluraChallengeBackEnd.Domain.Interfaces;

public interface ISummaryService
{
    Task<SummaryDTO> GetSummaryAsync(int year, int month);
}