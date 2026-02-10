

using SchoolEntities.Domain.Interfaces;
using SchoolEntities.Domain.Entities;

namespace SchoolEntities.Visual
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IStudent Ana = new Student("Ana", "Rodriguez", 14, "Los Prados", "8099123456", "3ro secundaria");
            IStudent Carlos = new Exstudent("Carlos", "Martinez", 18, "Ensanche Libertad", "8099988776", "6to secundaria");

            IEmployee Lucia = new Administrative("Lucia", "Santos", 30, "Santo Domingo Este", "8499123456", "Administrativa", 50000);
            ITeacher Luis = new Teacher("Luis", "Fernandez", 35, "Gazcue", "8499876543", "Profesor", 42000, new List<string> { "Fisica", "Quimica" });
            ITeacher Sofia = new Administrator("Sofia", "Lopez", 40, "Ensanche Naco", "8499765432", "Directora y profesora", 70000, new List<string> { "Historia", "Geografia" });
        }
    }
}