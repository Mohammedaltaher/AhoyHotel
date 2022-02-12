using Nest;
namespace Application.Features.HotelFeatures.Commands;
public class UpdateHotelCommand : MediatR.IRequest<HotelModel>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string Location { get; set; }
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, HotelModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ElasticClient _elasticClient;

        public UpdateHotelCommandHandler(IApplicationDbContext context, IMapper mapper, ElasticClient elasticClient = null)
        {
            _context = context;
            _mapper = mapper;
            _elasticClient = elasticClient;
        }
        public async Task<HotelModel> Handle(UpdateHotelCommand command, CancellationToken cancellationToken)
        {
            Hotel hotel = _context.Hotels.Where(a => a.Id == command.Id).FirstOrDefault();

            if (hotel == null)
            {
                return new HotelModel
                {
                    Data = null,
                    StatusCode = 404,
                    Messege = "no data found"
                };
            }
            else
            {
                hotel.Name = command.Name;
                hotel.Description = command.Description;
                hotel.Email = command.Email;
                hotel.PhoneNumber = command.PhoneNumber;
                hotel.Address = command.Address;
                hotel.Location = command.Location;

                await _context.SaveChangesAsync();
                try
                {
                    await _elasticClient.UpdateAsync<Hotel>(new DocumentPath<Hotel>(hotel), u => u.Doc(hotel));
                }
                catch { }
                return new HotelModel
                {
                    Data = _mapper.Map<HotelDto>(hotel),
                    StatusCode = 200,
                    Messege = "Data has been updated"
                };
            }
        }
    }
}
