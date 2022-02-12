using Domain.Entities;
using FluentValidation;

namespace Application.Features.HotelFeatures.Commands;
public class CreateHotelReviewCommand : IRequest<BaseModel>
{
    public string RevieweName { get; set; }
    public string ReviewerEmail { get; set; }
    public string Description { get; set; }
    public int Rating { get; set; }
    public int HotelId { get; set; }

    public class CreateHotelReviewCommandHandler : IRequestHandler<CreateHotelReviewCommand, BaseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateHotelReviewCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BaseModel> Handle(CreateHotelReviewCommand command, CancellationToken cancellationToken)
        {
            var Review = new Review
            {
                RevieweName = command.RevieweName,
                ReviewerEmail = command.ReviewerEmail,
                Description = command.Description,
                Rating = command.Rating,
                HotelId = command.HotelId,
            };
            _context.Reviews.Add(Review);
            await _context.SaveChangesAsync();
            return new BaseModel
            {
                StatusCode = 200,
                Messege = "Data has been added"
            };
        }
    }


}
 
