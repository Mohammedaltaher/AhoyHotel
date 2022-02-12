namespace Application.Features.LookUp.Queries;
public class GetFacilityByIdQuery : IRequest<FacilityModel>
{
    public int Id { get; set; }
    public class GetFacilityByIdQueryHandler : IRequestHandler<GetFacilityByIdQuery, FacilityModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetFacilityByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public Task<FacilityModel> Handle(GetFacilityByIdQuery query, CancellationToken cancellationToken)
        {
            var Facility = _context.Facilities.Where(a => a.Id == query.Id).AsNoTracking().FirstOrDefault();
            if (Facility == null)
            {
                return Task.FromResult(new FacilityModel
                {
                    Data = null,
                    StatusCode = 404,
                    Messege = "No data found"
                });
            }
            return Task.FromResult(new FacilityModel
            {
                Data = _mapper.Map<FacilityDto>(Facility),
                StatusCode = 200,
                Messege = "Data found"
            });
        }
    }
}
