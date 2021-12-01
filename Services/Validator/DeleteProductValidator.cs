using FluentValidation;
using Services.Command;
using Services.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Validator
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        private readonly IRepository _repository;

        public DeleteProductValidator(IRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Id).NotNull().NotEmpty()
                .MustAsync(IsProductExist)
                .WithMessage("Product not found");
        }

        private async Task<bool> IsProductExist(string id, CancellationToken cancelationToken)
        {
            var product = await _repository.GetProductById(id);
            return product == null ? true : false;
        }
    }
}
