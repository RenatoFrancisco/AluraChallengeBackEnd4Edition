namespace AluraChallengeBackEnd.Domain.Tests.Services;

public class IncomeServiceTest
{
    private readonly IIncomeService _incomeService;
    private readonly Mock<IIncomeRepository> _incomeRepository;
    private readonly Mock<INotifier> _notifier;
    private readonly Income _validIncome;

    public IncomeServiceTest()
    {
        _incomeRepository = new Mock<IIncomeRepository>();
        _notifier = new Mock<INotifier>();
        _incomeService = new IncomeService(_incomeRepository.Object, _notifier.Object);
        _incomeRepository.Setup(i => i.UnitOfWork.CommitAsync()).ReturnsAsync(true);
        _validIncome = new Income("Salary", 1300.0m, DateTime.Now);
    }

    [Fact]
    public async void CreateAsync_ValidIncome_MustCreate()   
    {
        // Arrange & Act
        var result = await _incomeService.CreateAsync(_validIncome);

        // Assert
        Assert.True(result);
        _incomeRepository.Verify(i => i.Create(_validIncome), Times.Once);
        _incomeRepository.Verify(i => i.UnitOfWork.CommitAsync(), Times.Once);
        _notifier.Verify(n => n.Handle(It.IsAny<Notification>()), Times.Never);
    }

    [Fact]
    public async void CreateAsync_InvalidIncome_DoesNotCreate()   
    {
        // Arrange & Act
        await Assert.ThrowsAsync<DomainException>(async () => 
            await _incomeService.CreateAsync(new Income(string.Empty, 0m, DateTime.MinValue)));

        // Assert
        _incomeRepository.Verify(i => i.Create(It.IsAny<Income>()), Times.Never);
        _incomeRepository.Verify(i => i.UnitOfWork.CommitAsync(), Times.Never);
        _notifier.Verify(n => n.Handle(It.IsAny<Notification>()), Times.Never);
    }

    [Fact]
    public async void CreateAsync_AlreadyExistsWithSameDescriptionAndMonth_DoesNotCreate()
    {
        // Arrange
        _incomeRepository
            .Setup(i => i.GetByDescriptionAndMonthAsync(_validIncome.Description, _validIncome.DateIncome.Month))
            .ReturnsAsync(new List<Income> { _validIncome });

        // Act
        var result = await _incomeService.CreateAsync(_validIncome);

        // Arrange
        Assert.False(result);
        _incomeRepository.Verify(i => i.Create(_validIncome), Times.Never);
        _incomeRepository.Verify(i => i.UnitOfWork.CommitAsync(), Times.Never);
        _notifier.Verify(n => n.Handle(It.IsAny<Notification>()), Times.Once);
    }

    [Fact]
    public async void EditAsync_ValidIncome_MustEdit()   
    {
        // Arrange
        var validIncome = new Income("Salary", 1300.0m, DateTime.Now);

        // Act
        var result = await _incomeService.EditAsync(validIncome);

        // Assert
        Assert.True(result);
        _incomeRepository.Verify(i => i.Edit(validIncome), Times.Once);
        _incomeRepository.Verify(i => i.UnitOfWork.CommitAsync(), Times.Once);
        _notifier.Verify(n => n.Handle(It.IsAny<Notification>()), Times.Never);
    }

   [Fact]
    public async void EditAsync_InvalidIncome_DoesNotEdit()   
    {
        // Arrange & Act
        await Assert.ThrowsAsync<DomainException>(async () => 
            await _incomeService.EditAsync(new Income(string.Empty, 0m, DateTime.MinValue)));

        // Assert
        _incomeRepository.Verify(i => i.Edit(It.IsAny<Income>()), Times.Never);
        _incomeRepository.Verify(i => i.UnitOfWork.CommitAsync(), Times.Never);
        _notifier.Verify(n => n.Handle(It.IsAny<Notification>()), Times.Never);
    }

    [Fact]
    public async void EditAsync_AlreadyExistsWithSameDescriptionAndMonth_DoesNotEdit()
    {
        // Arrange
        _incomeRepository
            .Setup(i => i.GetByDescriptionAndMonthAsync(_validIncome.Description, _validIncome.DateIncome.Month))
            .ReturnsAsync(new List<Income> { _validIncome });

        // Act
        var result = await _incomeService.EditAsync(_validIncome);

        // Arrange
        Assert.False(result);
        _incomeRepository.Verify(i => i.Edit(_validIncome), Times.Never);
        _incomeRepository.Verify(i => i.UnitOfWork.CommitAsync(), Times.Never);
        _notifier.Verify(n => n.Handle(It.IsAny<Notification>()), Times.Once);
    }
}