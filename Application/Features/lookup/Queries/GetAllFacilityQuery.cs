using Application.Dto;
using Application.Dto.Common;

namespace Application.Features.lookup.Queries;
public class GetAllFacilityQuery : Pagination, IRequest<FacilitiesModel>
{

    public class GetAllFacilityQueryHandler : IRequestHandler<GetAllFacilityQuery, FacilitiesModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllFacilityQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<FacilitiesModel> Handle(GetAllFacilityQuery query, CancellationToken cancellationToken)
        {
           
            var facilityList = await _context.Facilities
                    .OrderBy(o => o.Name)
                    .Skip((query.PageNumber - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .ToListAsync(cancellationToken: cancellationToken);
            _mapper.Map<List<FacilityDto>>(facilityList);
            return new FacilitiesModel
            {
                Data = _mapper.Map<List<FacilityDto>>(facilityList),
                StatusCode = 200,
                Messege = "Data found"
            };
        }
  
    }
}
