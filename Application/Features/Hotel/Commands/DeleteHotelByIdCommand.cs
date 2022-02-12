﻿using Nest;

namespace Application.Features.HotelFeatures.Commands;
public class DeleteHotelByIdCommand : MediatR.IRequest<HotelModel>
{
    public int Id { get; set; }
    public class DeleteHotelByIdCommandHandler : IRequestHandler<DeleteHotelByIdCommand, HotelModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ElasticClient _elasticClient;
        public DeleteHotelByIdCommandHandler(IApplicationDbContext context, IMapper mapper, ElasticClient elasticClient = null)
        {
            _context = context;
            _mapper = mapper;
            _elasticClient = elasticClient;
        }
        public async Task<HotelModel> Handle(DeleteHotelByIdCommand command, CancellationToken cancellationToken)
        {
            Hotel hotel = await _context.Hotels.Where(a => a.Id == command.Id).FirstOrDefaultAsync();
            if (hotel == null)
            {
                return new HotelModel
                {
                    Data = null,
                    StatusCode = 404,
                    Messege = "No data found"
                };
            };
            hotel.IsDeleted = true;
            //    _context.Hotels.Update(Hotel);
            await _context.SaveChangesAsync();
            try
            {
                await _elasticClient.DeleteAsync<Hotel>(hotel);
            }
            catch { }

            return new HotelModel
            {
                Data = _mapper.Map<HotelDto>(hotel),
                StatusCode = 200,
                Messege = "Data has been Deleted"
            };
        }
    }
}
