using Application.Dto;
using Microsoft.Extensions.Logging;
using Nest;

namespace Application.Features.Hotel.Commands;
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
        private readonly ILogger _logger;
        public UpdateHotelCommandHandler(IApplicationDbContext context, IMapper mapper,ILogger<UpdateHotelCommand> logger, ElasticClient elasticClient = null)
        {
            _context = context;
            _mapper = mapper;
            _elasticClient = elasticClient;
            _logger = logger;
        }
        public async Task<HotelModel> Handle(UpdateHotelCommand command, CancellationToken cancellationToken)
        {
            var hotel = _context.Hotels.FirstOrDefault(a => a.Id == command.Id);

            if (hotel == null)
            {
                return new HotelModel
                {
                    Data = null,
                    StatusCode = 404,
                    Message = "no data found"
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
                    await _elasticClient.UpdateAsync(new DocumentPath<Domain.Entities.Hotel>(hotel), u => u.Doc(hotel), cancellationToken);
                }
                catch(Exception ex) { _logger.LogError(ex.ToString()); }
                return new HotelModel
                {
                    Data = _mapper.Map<HotelDto>(hotel),
                    StatusCode = 200,
                    Message = "Data has been updated"
                };
            }
        }
    }
}
