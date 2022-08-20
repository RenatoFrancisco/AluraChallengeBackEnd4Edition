namespace AluraChallengeBackEnd.Domain.Services
{
    public class SummaryService : ServiceBase, ISummaryService
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenditureRepository _expenditureRepository;

        public SummaryService(IIncomeRepository incomeRepository,
                              IExpenditureRepository expenditureRepository,
                              INotifier notifier) : base(notifier)
        {
            _incomeRepository = incomeRepository;
            _expenditureRepository = expenditureRepository;
        }

        public async Task<SummaryDTO> GetSummaryAsync(int year, int month)
        {
            var incomes = await _incomeRepository.GetByYearAndMonthAsync(year, month);
            var expenditures = await _expenditureRepository.GetByYearAndMonthAsync(year, month);

            var totalIncomes = incomes.Sum(i => i.Value);
            var totalExpenditures = expenditures.Sum(e => e.Value);
            var balance = totalIncomes - totalExpenditures;

            var groupBy = expenditures
                .GroupBy(e => e.CategoryExpenditure.Description)
                .Select(gp => new CategoryExpendituresDTO
                {
                    Category = gp.Key,
                    Total = gp.Sum(e => e.Value)
                });


            var summary = new SummaryDTO
            {
                TotalIncomes = totalIncomes,
                TotalExpenditures = totalExpenditures,
                Balance = balance,
                GroupBy = groupBy
            };

            return summary;
        }
    }
}