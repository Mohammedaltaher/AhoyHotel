namespace Application.Features.LookUp.Commands;
public class UpdateFacilityCommand : IRequest<FacilityModel>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public class UpdateFacilityCommandHandler : IRequestHandler<UpdateFacilityCommand, FacilityModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateFacilityCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<FacilityModel> Handle(UpdateFacilityCommand command, CancellationToken cancellationToken)
        {
            var facility = _context.Facilities.Where(a => a.Id == command.Id).FirstOrDefault();

            if (facility == null)
            {
                return new FacilityModel
                {
                    Data = null,
                    StatusCode = 404,
                    Messege = "no data found"
                };
            }
            else
            {
                facility.Name = command.Name;
                facility.Icon = command.Icon; 

                await _context.SaveChangesAsync();
                return new FacilityModel
                {
                    Data = _mapper.Map<FacilityDto>(facility),
                    StatusCode = 200,
                    Messege = "Data has been updated"
                };
            }
        }
    }
}
