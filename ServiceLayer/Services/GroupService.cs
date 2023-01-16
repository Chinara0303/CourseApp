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

            Group existGroup = _repo.Get(g => g.Name.Trim().ToLower() == group.Name.Trim().ToLower());
            if (existGroup != null) throw new InvalidGroupException(ResponseMessages.ExistMessage);
            
            _repo.Create(group);
            _count++;
            return group;
        }

        public void Delete(int? id)
        {
            if(id == null) throw new InvalidGroupException(ResponseMessages.NotFound);
            Group dbGroup = _repo.Get(g => g.Id == id);
            _repo.Delete(dbGroup);
            if (dbGroup is null) throw new InvalidGroupException(ResponseMessages.NotFound);
        }

        public List<Group> GetAllByCapacity(int? capacity)
        {
            if (capacity is null) throw new InvalidGroupException(ResponseMessages.NotFound);
            List<Group> dbGroups = _repo.GetAll(g => g.Capacity == capacity);
            if (dbGroups.Count == 0) throw new InvalidGroupException(ResponseMessages.NotFound);
            return dbGroups;
        }

        public List<Group> GetAllByTeacherId(int? teacherId)
        {
            if (teacherId is null) throw new InvalidGroupException(ResponseMessages.NotFound);
            List<Group> dbGroups = _repo.GetAll(g => g.Teacher.Id == teacherId);
            if (dbGroups.Count == 0) throw new InvalidGroupException(ResponseMessages.NotFound);
            return dbGroups;
        }

        public List<Group> GetAllByTeacherName(string teacherName)
        {
            if (teacherName is null) throw new InvalidGroupException(ResponseMessages.NotFound);
            List<Group> dbGroups = _repo.GetAll(g => g.Teacher.Name.Trim().ToLower() == teacherName.Trim().ToLower());
            if (dbGroups.Count == 0) throw new InvalidGroupException(ResponseMessages.NotFound);
            return dbGroups;
        }

        public Group GetById(int? id)
        {
            if (id == null) throw new InvalidGroupException(ResponseMessages.NotFound);
            Group dbGroup = _repo.Get(g => g.Id == id);
            if (dbGroup is null) throw new InvalidGroupException(ResponseMessages.NotFound);
            return dbGroup;
        }

        public int GetCount()
        {
            int digit = _repo.GetAll().Count;
            if (digit <= 0) throw new InvalidGroupException(ResponseMessages.NotFound);
            return digit;
        }

        public List<Group> SearchByName(string searchText)
        {
            if (String.IsNullOrWhiteSpace(searchText)) throw new InvalidTeacherException(ResponseMessages.StringMessage);
            List<Group> groups = _repo.GetAll(g => g.Name.Trim().ToLower().Contains(searchText.Trim().ToLower()));
            if (groups.Count == 0) throw new InvalidTeacherException(ResponseMessages.NotFound);
            return groups;
        }

        public Group Update(int? id, Group group)
        {
            if (id is null) throw new InvalidGroupException(ResponseMessages.NotFound);
          
            group.Id = (int)id;
            _repo.Update(group);
           
            return group;
        }
    }
}
