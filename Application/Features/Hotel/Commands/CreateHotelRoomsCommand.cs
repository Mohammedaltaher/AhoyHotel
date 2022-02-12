using Domain.Entities;
using FluentValidation;

namespace Application.Features.HotelFeatures.Commands;
public class CreateHotelRoomsCommand : IRequest<BaseModel>
{
    public int RoomNo { get; set; }
    public int NoOfPersons { get; set; }
    public double Price { get; set; }
    public int HotelId { get; set; }

    public class CreateHotelRoomsCommandHandler : IRequestHandler<CreateHotelRoomsCommand, BaseModel>
    {
        private readonly IApplicationDbContext _context;
        public CreateHotelRoomsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<BaseModel> Handle(CreateHotelRoomsCommand command, CancellationToken cancellationToken)
        {
            var room = new Room
            {
                RoomNo = command.RoomNo,
                NoOfPersons = command.NoOfPersons,
                Price = command.Price,
                HotelId = command.HotelId,
            };
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return new BaseModel
            {
                StatusCode = 200,
                Messege = "Data has been added"
            };
        }
    }


}
