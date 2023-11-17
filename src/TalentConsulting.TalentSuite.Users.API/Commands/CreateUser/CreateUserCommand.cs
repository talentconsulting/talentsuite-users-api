using AutoMapper;
using MediatR;
using TalentConsulting.TalentSuite.Reports.Core.Interfaces.Commands;
using TalentConsulting.TalentSuite.Users.Common.Entities;
using TalentConsulting.TalentSuite.Users.Core.Entities;
using TalentConsulting.TalentSuite.Users.Infrastructure.Persistence.Repository;

namespace TalentConsulting.TalentSuite.Users.API.Commands.CreateUser;

public class CreateUserCommand : IRequest<string>, ICreateUserCommand
{
    public CreateUserCommand(UserDto userDto)
    {
        UserDto = userDto;
    }

    public UserDto UserDto { get; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateUserCommandHandler> _logger;
    public CreateUserCommandHandler(ApplicationDbContext context, IMapper mapper, ILogger<CreateUserCommandHandler> logger)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }
    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var unsavedEntity = _mapper.Map<User>(request.UserDto);
            ArgumentNullException.ThrowIfNull(unsavedEntity);

            var existing = _context.Users.FirstOrDefault(e => unsavedEntity.Id == e.Id);

            if (existing is not null)
                throw new InvalidOperationException($"User with Id: {unsavedEntity.Id} already exists, Please use Update command");

            //unsavedEntity.Risks = AttachExistingRisk(unsavedEntity.Risks);
            //unsavedEntity.RegisterDomainEvent(new ReportCreatedEvent(unsavedEntity));

            _context.Users.Add(unsavedEntity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred creating a user. {exceptionMessage}", ex.Message);
            throw;
        }

        if (request is not null && request.UserDto is not null)
            return request.UserDto.Id;
        else
            return string.Empty;
    }

    //private ICollection<Risk> AttachExistingRisk(ICollection<Risk>? unSavedEntities)
    //{
    //    var returnList = new List<Risk>();

    //    if (unSavedEntities is null || !unSavedEntities.Any())
    //        return returnList;

    //    var existing = _context.Risks.Where(e => unSavedEntities.Select(c => c.Id).Contains(e.Id)).ToList();

    //    for (var i = 0; i < unSavedEntities.Count; i++)
    //    {
    //        var unSavedItem = unSavedEntities.ElementAt(i);
    //        var savedItem = existing.Find(x => x.Id == unSavedItem.Id);
    //        returnList.Add(savedItem ?? unSavedItem);
    //    }

    //    return returnList;
    //}
}

