
namespace SchoolEntities.Domain.Interfaces;

public interface IStudent : ICommunityMember
{
    string Grade { get; set; }
    bool isStudent { get; }

}