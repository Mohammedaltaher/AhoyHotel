using Domain.Entities;
using Nest;
using System.Linq.Expressions;

namespace Application.Features.FacilityFeatures.Queries;
public class GetAllFacilityQuery : Pagination, MediatR.IRequest<FacilitiesModel>
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
           
            List<Facility> FacilityList = await _context.Facilities
                    .OrderBy(o => o.Name)
                    .Skip((query.PageNumber - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .ToListAsync();
            if (FacilityList == null)
            {
                return new FacilitiesModel
                {
                    Data = null,
                    StatusCode = 404,
                    Messege = "No data found"
                };
            }
            _mapper.Map<List<FacilityDto>>(FacilityList);
            return new FacilitiesModel
            {
                Data = _mapper.Map<List<FacilityDto>>(FacilityList),
                StatusCode = 200,
                Messege = "Data found"
            };
        }
  
    }
}
