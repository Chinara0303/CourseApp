using DomainLayer.Models;

namespace ServiceLayer.Services.Interfaces
{
    public interface IGroupService
    {
        Group Create(int? teaherId,Group group);
        void Delete(int? id);
        Group Update(int? id, Group group);
        Group GetById(int? id);
        List<Group> GetAllByCapacity(int? id);
        int GetCount();
        List<Group> GetAllByTeacherId(int? teacherId);
        List<Group> GetAllByTeacherName(string teacherName);
        List<Group> SearchByName(string searchText);
    }
}
