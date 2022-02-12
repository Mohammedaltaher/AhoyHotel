﻿using Domain.Entities;
using Nest;

namespace Application.Features.LookUp.Commands;
public class DeleteFacilityByIdCommand : MediatR.IRequest<FacilityModel>
{
    public int Id { get; set; }
    public class DeleteFacilityByIdCommandHandler : IRequestHandler<DeleteFacilityByIdCommand, FacilityModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DeleteFacilityByIdCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<FacilityModel> Handle(DeleteFacilityByIdCommand command, CancellationToken cancellationToken)
        {
            var Facility = await _context.Facilities.Where(a => a.Id == command.Id).FirstOrDefaultAsync();
            if (Facility == null)
            {
                return new FacilityModel
                {
                    Data = null,
                    StatusCode = 404,
                    Messege = "No data found"
                };
            };
            Facility.IsDeleted = true;
            await _context.SaveChangesAsync();
            return new FacilityModel
            {
                Data = _mapper.Map<FacilityDto>(Facility),
                StatusCode = 200,
                Messege = "Data has been Deleted"
            };
        }
    }
}
