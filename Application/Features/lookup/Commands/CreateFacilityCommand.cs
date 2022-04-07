using Application.Dto;
using FluentValidation;

namespace Application.Features.lookup.Commands;
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
            var facility = _mapper.Map<Facility>(command);
            _context.Facilities.Add(facility);
            await _context.SaveChangesAsync();
            return new FacilityModel
            {
                Data = _mapper.Map<FacilityDto>(facility),
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
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name should be not empty!");
    }
}
