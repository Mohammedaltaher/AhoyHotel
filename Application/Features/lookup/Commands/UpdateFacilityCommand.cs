using Application.Interfaces;
using Application.Model;
using Application.Model;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Linq;
namespace Application.Features.LookUp.Commands;
public class UpdateFacilityCommand : IRequest<FacilityModel>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public class UpdateFacilityCommandHandler : IRequestHandler<UpdateFacilityCommand, FacilityModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateFacilityCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<FacilityModel> Handle(UpdateFacilityCommand command, CancellationToken cancellationToken)
        {
            Facility Facility = _context.Facilities.Where(a => a.Id == command.Id).FirstOrDefault();

            if (Facility == null)
            {
                return new FacilityModel
                {
                    Data = null,
                    StatusCode = 404,
                    Messege = "no data found"
                };
            }
            else
            {
                Facility.Name = command.Name;
                Facility.Icon = command.Icon; 

                await _context.SaveChangesAsync();
                return new FacilityModel
                {
                    Data = _mapper.Map<FacilityDto>(Facility),
                    StatusCode = 200,
                    Messege = "Data has been updated"
                };
            }
        }
    }
}
