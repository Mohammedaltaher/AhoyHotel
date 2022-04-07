using Application.Dto;
using Application.Dto.Common;
using LinqKit;
using Microsoft.Extensions.Logging;
using Nest;

namespace Application.Features.Hotel.Queries;
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
        private readonly ILogger _logger;
        public SearchHotelsQueryHandler(IApplicationDbContext context, IMapper mapper, ILogger<SearchHotelsQuery> logger, ElasticClient elasticClient = null)
        {
            _context = context;
            _mapper = mapper;
            _elasticClient = elasticClient;
            _logger = logger;
        }
        public async Task<HotelsModel> Handle(SearchHotelsQuery query, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<Domain.Entities.Hotel>();

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
            List<Domain.Entities.Hotel> hotelList = null;
            try
            {
                var searchResponse = await _elasticClient.SearchAsync<Domain.Entities.Hotel>(s => s
                                                .From((query.PageNumber - 1) * query.PageSize).Size(query.PageSize)
                                                .Query(q => q
                                                .Match(m => m
                                                   .Field(f => f.Name).Query(query.Name)
                                                   //.Field(f => f.Email).Query(query.Email)
                                                   //.Field(f => f.PhoneNumber).Query(query.PhoneNumber)
                                                   )), cancellationToken);
                hotelList = searchResponse.Documents.ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

            }

            if (hotelList == null || hotelList.Count == 0)
                hotelList = await _context.Hotels
                    .Include(x => x.Reviews)
                    .Include(x => x.Images)
                    .Include(x => x.HotelFacilities).ThenInclude(y => y.Facility)
                    .Include(x => x.Rooms)
                    .Where(predicate)
                    .OrderBy(o => o.Name)
                    .Skip((query.PageNumber - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .ToListAsync(cancellationToken: cancellationToken);
            _mapper.Map<List<HotelDto>>(hotelList);
            return new HotelsModel
            {
                Data = _mapper.Map<List<HotelDto>>(hotelList),
                StatusCode = 200,
                Message = "Data found"
            };
        }

    }
}
