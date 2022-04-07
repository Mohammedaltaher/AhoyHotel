using Application.Dto;

namespace Application.Features.lookup.Commands;
public class DeleteFacilityByIdCommand : IRequest<FacilityModel>
{
    public int Id { get; init; }
    public class DeleteFacilityByIdCommandHandler : IRequestHandler<DeleteFacilityByIdCommand, FacilityModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DeleteFacilityByIdCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<FacilityModel> Handle(DeleteFacilityByIdCommand command, CancellationToken cancellationToken)
        {
            var facility = await _context.Facilities.Where(a => a.Id == command.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            if (facility == null)
            {
                return new FacilityModel
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "No data found"
                };
            }
            facility.IsDeleted = true;
            await _context.SaveChangesAsync();
            return new FacilityModel
            {
                Data = _mapper.Map<FacilityDto>(facility),
                StatusCode = 200,
                Message = "Data has been Deleted"
            };
        }
    }
}
