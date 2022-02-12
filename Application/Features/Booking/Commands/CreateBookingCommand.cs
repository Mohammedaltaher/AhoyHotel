using FluentValidation;

namespace Application.Features.Booking.Commands;
public class CreateBookingCommand : IRequest<BookingModel>
{
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public bool IsConfirmed { get; set; }
    public double ActualPrice { get; set; }
    public double Discount { get; set; }
    public double PaidAmount { get; set; }
    public string UserName { get; set; }
    public int RoomId { get; set; }

    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookingCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BookingModel> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Booking Booking = _mapper.Map<Domain.Entities.Booking>(command);
            _context.Bookings.Add(Booking);
            await _context.SaveChangesAsync();
            return new BookingModel
            {
                Data = _mapper.Map<BookingDto>(Booking),
                StatusCode = 200,
                Messege = "Data has been added"
            };
        }
    }


}
public class CreateBookingsCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingsCommandValidator()
    {
        //RuleFor(x => x.Name)
        //    .Cascade(CascadeMode.Stop)
        //    .NotEmpty()
        //    .Length(0, 50)
        //    .WithMessage("Name should be not empty. NEVER!")
        //    .Must(IsValidName).WithMessage("Name should be all letters.");
    }
    private bool IsValidName(string name) => name.All(Char.IsLetter);
}
