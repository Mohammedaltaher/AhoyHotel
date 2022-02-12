using Domain.Entities;
using FluentValidation;

namespace Application.Features.LookUp.Commands;
public class CreateFacilityCommand : IRequest<FacilityModel>
{
    public string Name { get; set; }
    public string Icon { get; set; }

    public class CreateFacilityCommandHandler : IRequestHandler<CreateFacilityCommand, FacilityModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateFacilityCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<FacilityModel> Handle(CreateFacilityCommand command, CancellationToken cancellationToken)
        {
            var Facility = _mapper.Map<Facility>(command);
            _context.Facilities.Add(Facility);
            await _context.SaveChangesAsync();
            return new FacilityModel
            {
                Data = _mapper.Map<FacilityDto>(Facility),
                StatusCode = 200,
                Messege = "Data has been added"
            };
        }
    }


}
public class CreateFacilitysCommandValidator : AbstractValidator<CreateFacilityCommand>
{
    public CreateFacilitysCommandValidator()
    {
        //RuleFor(x => x.Name)
        //    .Cascade(CascadeMode.Stop)
        //    .NotEmpty()
        //    .Length(0, 50)
        //    .WithMessage("Name should be not empty. NEVER!")
        //    .Must(IsValidName).WithMessage("Name should be all letters.");
    }
    private bool IsValidName(string name) => name.All(Char.IsLetter);
}
