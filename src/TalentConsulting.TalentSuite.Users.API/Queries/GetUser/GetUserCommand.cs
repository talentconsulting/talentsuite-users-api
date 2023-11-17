using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TalentConsulting.TalentSuite.Users.Common;
using TalentConsulting.TalentSuite.Users.Common.Entities;
using TalentConsulting.TalentSuite.Users.Core.Entities;
using TalentConsulting.TalentSuite.Users.Core.Helpers;
using TalentConsulting.TalentSuite.Users.Infrastructure.Persistence.Repository;


namespace TalentConsulting.TalentSuite.Reports.API.Queries.GetReports;


public class GetUserCommand : IRequest<UserDto>
{
    public GetUserCommand(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
}

public class GetUserCommandHandler : IRequestHandler<GetUserCommand, UserDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserCommandHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<UserDto> Handle(GetUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);


        if (entity == null)
        {
            throw new NotFoundException(nameof(UserDto), request.Id.ToString());
        }

        return entity;

    }
}