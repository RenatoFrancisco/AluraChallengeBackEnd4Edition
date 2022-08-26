namespace AluraChallengeBackEnd.Domain.Tests.Services;

public class ExpenditureServiceTest
{
    private readonly IExpenditureService _expenditureService;
    private readonly Mock<IExpenditureRepository> _expenditureRepository;
    private readonly Mock<INotifier> _notifier;
    private readonly Expenditure _validExpenditure;

    public ExpenditureServiceTest()
    {

        _expenditureRepository = new Mock<IExpenditureRepository>();
        _notifier = new Mock<INotifier>();
        _expenditureService = new ExpenditureService(_expenditureRepository.Object, _notifier.Object);
        _expenditureRepository.Setup(e => e.UnitOfWork.CommitAsync()).ReturnsAsync(true);
        _validExpenditure = new Expenditure("Aluguek", 2500.0m, DateTime.Now, "Moradia");
    }

    [Fact]
    public async void CreateAsync_ValidExpenditure_MustCreate()
    {
        // Arrange
        _expenditureRepository
            .Setup(e => e.GetCategoryByDescriptionAsync(_validExpenditure.CategoryExpenditure.Description))
            .ReturnsAsync(new CategoryExpenditure("Moradia"));

        // Act
        var result = await _expenditureService.CreateAsync(_validExpenditure);

        // Asset
        Assert.True(result);
        _expenditureRepository.Verify(e => e.Create(_validExpenditure), Times.Once);
        _expenditureRepository.Verify(e => e.UnitOfWork.CommitAsync(), Times.Once);
        _notifier.Verify(n => n.Handle(It.IsAny<Notification>()), Times.Never);
    }

    [Fact]
    public async void CreateAsync_AlreadyExistsWithSameDescriptionAndMonth_DoesNotCreate()
    {
        // Arrange
        _expenditureRepository
            .Setup(i => i.GetByDescriptionAndMonthAsync(_validExpenditure.Description, _validExpenditure.DateExpenditure.Month))
            .ReturnsAsync(new List<Expenditure> { _validExpenditure });

        // Act
        var result = await _expenditureService.CreateAsync(_validExpenditure);

        // Arrange
        Assert.False(result);
        _expenditureRepository.Verify(i => i.Create(_validExpenditure), Times.Never);
        _expenditureRepository.Verify(i => i.UnitOfWork.CommitAsync(), Times.Never);
        _notifier.Verify(n => n.Handle(It.IsAny<Notification>()), Times.Once);
    }   

    [Fact]
    public async void CreateAsync_InvalidExpenditure_DoesNotCreate()
    {
        // Arrange & Act
        await Assert.ThrowsAsync<DomainException>(async () =>
            await _expenditureService.CreateAsync(new Expenditure(string.Empty, 0m, DateTime.MinValue, string.Empty)));

        // Asset
        _expenditureRepository.Verify(e => e.Create(_validExpenditure), Times.Never);
        _expenditureRepository.Verify(e => e.UnitOfWork.CommitAsync(), Times.Never);
        _notifier.Verify(n => n.Handle(It.IsAny<Notification>()), Times.Never);
    }

    [Fact]
    public async void CreateAsync_WhenCategoryNotFound_DoesNotCreate()
    {
        // Assert
        _expenditureRepository.Setup(e => e.GetCategoryByDescriptionAsync("CategoryTest"))
            .ReturnsAsync(() => default);

        // Act
        var result = await _expenditureService.CreateAsync(_validExpenditure);

        // Arrange
        Assert.False(result);
        _expenditureRepository.Verify(i => i.Create(_validExpenditure), Times.Never);
        _expenditureRepository.Verify(i => i.UnitOfWork.CommitAsync(), Times.Never);
        _notifier.Verify(n => n.Handle(It.IsAny<Notification>()), Times.Once);
    }

    [Fact]
    public async void EditAsync_ValidExpenditure_MustEdit()
    {
        // Arrange
        _expenditureRepository
            .Setup(e => e.GetCategoryByDescriptionAsync(_validExpenditure.CategoryExpenditure.Description))
            .ReturnsAsync(new CategoryExpenditure("Moradia"));

        // Act
        var result = await _expenditureService.EditAsync(_validExpenditure);

        // Asset
        Assert.True(result);
        _expenditureRepository.Verify(e => e.Edit(_validExpenditure), Times.Once);
        _expenditureRepository.Verify(e => e.UnitOfWork.CommitAsync(), Times.Once);
        _notifier.Verify(n => n.Handle(It.IsAny<Notification>()), Times.Never);
    }   

    [Fact]
    public async void EditAsync_InvalidExpenditure_DoesNotEdit()
    {
        // Arrange & Act
        await Assert.ThrowsAsync<DomainException>(async () =>
            await _expenditureService.EditAsync(new Expenditure(string.Empty, 0m, DateTime.MinValue, string.Empty)));

        // Asset
        _expenditureRepository.Verify(e => e.Edit(_validExpenditure), Times.Never);
        _expenditureRepository.Verify(e => e.UnitOfWork.CommitAsync(), Times.Never);
        _notifier.Verify(n => n.Handle(It.IsAny<Notification>()), Times.Never);
    }

    [Fact]
    public async void EditAsync_AlreadyExistsWithSameDescriptionAndMonth_DoesNotEdit()
    {
        // Arrange
        _expenditureRepository
            .Setup(i => i.GetByDescriptionAndMonthAsync(_validExpenditure.Description, _validExpenditure.DateExpenditure.Month))
            .ReturnsAsync(new List<Expenditure> { _validExpenditure });

        // Act
        var result = await _expenditureService.EditAsync(_validExpenditure);

        // Arrange
        Assert.False(result);
        _expenditureRepository.Verify(i => i.Edit(_validExpenditure), Times.Never);
        _expenditureRepository.Verify(i => i.UnitOfWork.CommitAsync(), Times.Never);
        _notifier.Verify(n => n.Handle(It.IsAny<Notification>()), Times.Once);
    }

    [Fact]
    public async void EditAsync_WhenCategoryNotFound_DoesNotEdit()
    {
        // Assert
        _expenditureRepository.Setup(e => e.GetCategoryByDescriptionAsync("CategoryTest"))
            .ReturnsAsync(() => default);

        // Act
        var result = await _expenditureService.EditAsync(_validExpenditure);

        // Arrange
        Assert.False(result);
        _expenditureRepository.Verify(i => i.Edit(_validExpenditure), Times.Never);
        _expenditureRepository.Verify(i => i.UnitOfWork.CommitAsync(), Times.Never);
        _notifier.Verify(n => n.Handle(It.IsAny<Notification>()), Times.Once);
    }
}