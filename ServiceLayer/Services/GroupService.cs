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
        private readonly TeacherRepository _repoTeacher;
        private int _count = 1;
        public GroupService() 
        {
            _repoTeacher = new TeacherRepository();
            _repo = new GroupRepository();
        }

        public Group Create(int? teacherId, Group group)
        {
            group.Id = _count;
            Teacher existTeacher = _repoTeacher.Get(g => g.Id == teacherId);
            group.Teacher = existTeacher;
            if (existTeacher is null) throw new InvalidGroupException(ResponseMessages.NotFound);

            Group existGroup = _repo.Get(g => g.Name.ToLower() == group.Name.ToLower());
            if (existGroup != null) throw new InvalidGroupException(ResponseMessages.ExistMessage);

            _repo.Create(group);
            _count++;
            return group;
        }

        public void Delete(int? id)
        {
            if(id == null) throw new InvalidGroupException(ResponseMessages.NotFound);
            Group dbGroup = _repo.Get(g => g.Id == id);
            if (dbGroup is null) throw new InvalidGroupException(ResponseMessages.NotFound);
            _repo.Delete(dbGroup);
        }

        public List<Group> GetAllByCapacity(int? capacity)
        {
            if (capacity is null) throw new InvalidGroupException(ResponseMessages.NotFound);
            List<Group> dbgroups = _repo.GetAll(g => g.Capacity == capacity);
            if (dbgroups.Count == 0) throw new InvalidGroupException(ResponseMessages.NotFound);
            return dbgroups;
        }

        public List<Group> GetAllByTeacherId(int? teacherId)
        {
            if (teacherId is null) throw new InvalidGroupException(ResponseMessages.NotFound);
            List<Group> dbgroups = _repo.GetAll(g => g.Teacher.Id == teacherId);
            if (dbgroups.Count == 0) throw new InvalidGroupException(ResponseMessages.NotFound);
            return dbgroups;
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
