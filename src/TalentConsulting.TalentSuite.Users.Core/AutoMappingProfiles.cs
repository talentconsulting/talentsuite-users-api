using AutoMapper;
using TalentConsulting.TalentSuite.Users.Common.Entities;
using TalentConsulting.TalentSuite.Users.Core.Entities;

namespace TalentConsulting.TalentSuite.Users.Core;

public class AutoMappingProfiles : Profile
{
    public AutoMappingProfiles()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<ReportDto, Report>().ReverseMap();
        CreateMap<RiskDto, Risk>().ReverseMap();
    }
}
