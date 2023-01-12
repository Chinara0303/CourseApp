using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface ITeacherService
    {
        Teacher Create(Teacher teacher);
        void Delete(int? id);   
        Teacher Update(int id,Teacher teacher);
        Teacher GetById(int? id);
        List<Teacher> GetAll();
        List<Teacher> SearchByNameAndSurname(string name, string surname);

    }
}
