using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics;
using TalentConsulting.TalentSuite.Reports.API.Queries.GetReports;
using TalentConsulting.TalentSuite.Users.API.Commands.CreateUser;
using TalentConsulting.TalentSuite.Users.Common.Entities;

namespace TalentConsulting.TalentSuite.Users.API.Endpoints;

public class MinimalUserEndPoints
{
    public void RegisterMinimalUserEndPoints(WebApplication app)
    {
        app.MapGet("api/users/{id}", async (string id, CancellationToken cancellationToken, ISender _mediator) =>
        {
            try
            {
                GetUserCommand request = new(id);
                var result = await _mediator.Send(request, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }).WithMetadata(new SwaggerOperationAttribute("Users", "Get User") { Tags = new[] { "Users" } });


        app.MapPost("api/users", async ([FromBody] UserDto request, CancellationToken cancellationToken, ISender _mediator, ILogger<MinimalUserEndPoints> logger) =>
        {
            try
            {
                CreateUserCommand command = new(request);
                var result = await _mediator.Send(command, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred creating user (api). {exceptionMessage}", ex.Message);
                Debug.WriteLine(ex.Message);
                throw;
            }
        }).WithMetadata(new SwaggerOperationAttribute("Users", "Create user") { Tags = new[] { "Users" } });

    }
}
