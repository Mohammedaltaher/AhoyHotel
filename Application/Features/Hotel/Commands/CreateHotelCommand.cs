using Domain.Entities;
using FluentValidation;
using Nest;

namespace Application.Features.HotelFeatures.Commands;
public class CreateHotelCommand : MediatR.IRequest<HotelModel>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string Location { get; set; }
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, HotelModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ElasticClient _elasticClient;
        public CreateHotelCommandHandler(IApplicationDbContext context, IMapper mapper, ElasticClient elasticClient = null)
        {
            _context = context;
            _mapper = mapper;
            _elasticClient = elasticClient;
        }
        public async Task<HotelModel> Handle(CreateHotelCommand command, CancellationToken cancellationToken)
        {
            var Hotel = _mapper.Map<Hotel>(command);
            _context.Hotels.Add(Hotel);
            await _context.SaveChangesAsync();
            try
            {
              var g =   await _elasticClient.IndexDocumentAsync(Hotel);
            }
            catch { }
            return new HotelModel
            {
                Data = _mapper.Map<HotelDto>(Hotel),
                StatusCode = 200,
                Messege = "Data has been added"
            };
        }
    }


}
public class CreateHotelCommandValidator : AbstractValidator<CreateHotelCommand>
{
    public CreateHotelCommandValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Length(0, 50)
            .WithMessage("Name should be not empty. NEVER!")
            .Must(IsValidName).WithMessage("Name should be all letters.");
    }
    private bool IsValidName(string name) => name.All(Char.IsLetter);
}
//public class MustHaveCreateUserAccountCommandValidator<T, TProperty> : PropertyValidator<T, TProperty>
//{
//    public override string Name => "CreateUserAccountCommand";

//    public override bool IsValid(ValidationContext<T> context, TProperty value)
//    {
//        return value is IList<CreateUserAccountCommand> list && list.Any();
//    }
//}