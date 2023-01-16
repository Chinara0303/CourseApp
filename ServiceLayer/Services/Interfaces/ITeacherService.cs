using DomainLayer.Models;

namespace ServiceLayer.Services.Interfaces
{
    public interface ITeacherService
    {
        Teacher Create(Teacher teacher);
        void Delete(int? id);   
        Teacher Update(int? id,Teacher teacher);
        Teacher GetById(int? id);
        List<Teacher> GetAll();
        List<Teacher> SearchByNameAndSurname(string searchText);

    }
}
