using DomainLayer.Models;
using Repository.Repositories;
using ServiceLayer.Exceptions;
using ServiceLayer.Helpers.Constants;
using ServiceLayer.Services.Interfaces;

namespace ServiceLayer.Services
{
    public class GroupService : IGroupService
    {
        private readonly GroupRepository _repo;
        private int _count = 1;
        public GroupService() => _repo = new GroupRepository();
        
        public Group Create(int? teacherId, Group group)
        {
            group.Id = _count;
            var existTeacher = _repo.Get(g => g.Teacher.Id == teacherId);

            //if (existTeacher is null) throw new InvalidGroupException(ResponseMessages.NotFound);

            var existGroup = _repo.Get(g => g.Name == group.Name);
            if (existGroup != null) throw new InvalidGroupException(ResponseMessages.ExistMessage);
            _repo.Create(group);
            _count++;
            return group;
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetAllByCapacity(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetAllByTeacherId(int? teacherId)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetAllByTeacherName(string teacherName)
        {
            throw new NotImplementedException();
        }

        public Group GetById(int? id)
        {
            if (id == null) throw new InvalidTeacherException(ResponseMessages.NotFound);
            Group dbGroup = _repo.Get(g => g.Id == id);
            if (dbGroup is null) throw new InvalidTeacherException(ResponseMessages.NotFound);
            return dbGroup;
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public List<Group> SearchByName(string searchText)
        {
            throw new NotImplementedException();
        }

        public Group Update(int? id, Group group)
        {
            throw new NotImplementedException();
        }
    }
}
