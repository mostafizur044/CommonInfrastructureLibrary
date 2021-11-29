using FluentValidation;
using Services.Command;
using Services.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Validator
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IRepository _repository;

        public CreateProductValidator(IRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Id).NotNull().NotEmpty()
                .MustAsync(IsProductExist)
                .WithMessage("Product already exist");
        }

        private async Task<bool> IsProductExist(string id, CancellationToken cancelationToken)
        {
            var product = await _repository.GetProductById(id);
            return product == null ? true : false;
        }
    }
}
