using FluentValidation.Results;

namespace Services.DTO
{
    public class CommonResponse
    {
        public bool Succeeded { get; set; }
        public ValidationResult Validation { get; set; }
    }
}
