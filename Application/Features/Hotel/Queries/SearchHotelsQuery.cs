using Domain.Entities;
using LinqKit;
using Nest;
using System.Linq.Expressions;

namespace Application.Features.HotelFeatures.Queries;
public class SearchHotelsQuery : Pagination, MediatR.IRequest<HotelsModel>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public class SearchHotelsQueryHandler : IRequestHandler<SearchHotelsQuery, HotelsModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly ElasticClient _elasticClient;
        private readonly IMapper _mapper;
        public SearchHotelsQueryHandler(IApplicationDbContext context, IMapper mapper, ElasticClient elasticClient = null)
        {
            _context = context;
            _mapper = mapper;
            _elasticClient = elasticClient;
        }
        public async Task<HotelsModel> Handle(SearchHotelsQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<Hotel, bool>> predicate = PredicateBuilder.True<Hotel>();

            if (!string.IsNullOrEmpty(query.Name))
            {
                predicate = predicate.And(i => i.Name.ToLower().Contains(query.Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(query.Email))
            {
                predicate = predicate.And(i => i.Email.Contains(query.Email));
            }
            if (!string.IsNullOrEmpty(query.PhoneNumber))
            {
                predicate = predicate.And(i => i.PhoneNumber.Contains(query.PhoneNumber));
            }
            List<Hotel> HotelList = null;
            try
            {
                var searchResponse = await _elasticClient.SearchAsync<Hotel>(s => s
                                                .From((query.PageNumber - 1) * query.PageSize).Size(query.PageSize)
                                                .Query(q => q
                                                .Match(m => m
                                                   .Field(f => f.Name).Query(query.Name)
                                                   //.Field(f => f.Email).Query(query.Email)
                                                   //.Field(f => f.PhoneNumber).Query(query.PhoneNumber)
                                                   )));
                HotelList = searchResponse.Documents.ToList();

            }
            catch
            { }

            if (HotelList == null || HotelList.Count == 0)
                HotelList = await _context.Hotels
                    .Include(x => x.Reviews)
                    .Include(x => x.Images)
                    .Include(x => x.HotelFacilities).ThenInclude(y => y.Facility)
                    .Include(x => x.Rooms)
                    .Where(predicate)
                    .OrderBy(o => o.Name)
                    .Skip((query.PageNumber - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .ToListAsync();
            if (HotelList == null)
            {
                return new HotelsModel
                {
                    Data = null,
                    StatusCode = 404,
                    Messege = "No data found"
                };
            }
            _mapper.Map<List<HotelDto>>(HotelList);
            return new HotelsModel
            {
                Data = _mapper.Map<List<HotelDto>>(HotelList),
                StatusCode = 200,
                Messege = "Data found"
            };
        }

    }
}
