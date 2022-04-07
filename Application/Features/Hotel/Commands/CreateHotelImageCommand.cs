using Application.Dto.Common;
using FluentValidation;

namespace Application.Features.Hotel.Commands;
public class CreateHotelImageCommand : IRequest<BaseModel>
{
    public string Url { get; set; }
    public int HotelId { get; set; }

    public class CreateHotelImageCommandHandler : IRequestHandler<CreateHotelImageCommand, BaseModel>
    {
        private readonly IApplicationDbContext _context;
        public CreateHotelImageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<BaseModel> Handle(CreateHotelImageCommand command, CancellationToken cancellationToken)
        {
            HotelImage image = new()
            {
                Url = command.Url,
                HotelId = command.HotelId,
            };
            _context.HotelImages.Add(image);
            await _context.SaveChangesAsync();
            return new BaseModel
            {
                StatusCode = 200,
                Message = "Data has been added"
            };
        }
    }
    public class CreateHotelImageCommandValidator : AbstractValidator<CreateHotelImageCommand>
    {
        public CreateHotelImageCommandValidator()
        {
            RuleFor(x => x.Url)
                .NotEmpty()
                .WithMessage("Url should be not empty!");
        }
    }


}

