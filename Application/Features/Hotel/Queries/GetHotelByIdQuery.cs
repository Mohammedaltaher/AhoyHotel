using Application.Dto;

namespace Application.Features.Hotel.Queries;
public class GetHotelByIdQuery : IRequest<HotelModel>
{
    public int Id { get; set; }
    public class GetHotelByIdQueryHandler : IRequestHandler<GetHotelByIdQuery, HotelModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetHotelByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public Task<HotelModel> Handle(GetHotelByIdQuery query, CancellationToken cancellationToken)
        {
            var hotel = _context.Hotels
                .Include(x => x.Reviews)
                    .Include(x => x.Images)
                    .Include(x => x.HotelFacilities).ThenInclude(y => y.Facility)
                    .Include(x => x.Rooms)
                    .Where(a => a.Id == query.Id).AsNoTracking().FirstOrDefault();
            if (hotel == null)
            {
                return Task.FromResult(new HotelModel
                {
                    Data = null,
                    StatusCode = 404,
                    Messege = "No data found"
                });
            }
            return Task.FromResult(new HotelModel
            {
                Data = _mapper.Map<HotelDto>(hotel),
                StatusCode = 200,
                Messege = "Data found"
            });
        }
    }
}
