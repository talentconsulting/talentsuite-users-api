using AutoMapper;
using MediatR;
using System.Reflection;
using TalentConsulting.TalentSuite.Users.Common.Interfaces;
using TalentConsulting.TalentSuite.Users.Core;
using TalentConsulting.TalentSuite.Users.Infrastructure.Service;

namespace TalentConsulting.TalentSuite.Users.API;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<ICurrentUserService, CurrentUserService>();
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapMethod = (m => false);
            cfg.AddProfile(new AutoMappingProfiles());
        });

        var mapper = config.CreateMapper();

        services.AddSingleton(mapper);

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}
