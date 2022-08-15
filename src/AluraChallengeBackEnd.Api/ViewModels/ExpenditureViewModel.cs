namespace AluraChallengeBackEnd.Api.ViewModels;
public class ExpenditureViewModel
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} caracteres", MinimumLength = 2)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [Range(0, 9999999.99)]
    public decimal Value { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    public DateTime DateExpenditure { get; set; }
}