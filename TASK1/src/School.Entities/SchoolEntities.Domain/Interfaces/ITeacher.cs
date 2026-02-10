
namespace SchoolEntities.Domain.Interfaces;

public interface ITeacher : IEmployee
{
    List<string> Subjects { get; set; }
    bool IsAdministrator { get; }
}