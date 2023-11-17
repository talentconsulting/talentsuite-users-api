using System.ComponentModel.DataAnnotations.Schema;
using TalentConsulting.TalentSuite.Users.Common;
using TalentConsulting.TalentSuite.Users.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Users.Core.Entities;

[Table("users")]
public class User : EntityBase<string>, IAggregateRoot
{
    private User() { }

    public User(string id, string firstname, string lastname, string email, string usergroupid, ICollection<Report> reports)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        UserGroupId = usergroupid;
        Reports = reports;
    }

    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string UserGroupId { get; set; } = default!;
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
#if ADD_ENTITY_NAV
    public virtual UserGroup Usergroup { get; set; } = null!;
#endif

}