
using SchoolEntities.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEntities.Domain.Entities;

public class Administrator : ITeacher
{
    public string Name { get; set; }
    public string Lastname { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Rol { get; set; }
    public int Amount { get; set; }
    public List<string> Subjects { get; set; }
    public bool IsAdministrator { get; } = true;

    public Administrator(string name, string lastname, int age, string address, string phone, string rol, int amount, List<string> subjects)
    {
        Name = name;
        Lastname = lastname;
        Age = age;
        Address = address;
        Phone = phone;
        Rol = rol;
        Amount = amount;
        Subjects = subjects;
    }
}
