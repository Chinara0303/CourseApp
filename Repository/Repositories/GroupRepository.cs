using DomainLayer.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System.Text.RegularExpressions;
using Group = DomainLayer.Models.Group;

namespace Repository.Repositories
{
    public class GroupRepository : IRepository<Group>
    {
        private readonly TeacherRepository _repoTeacher;
        public GroupRepository()
        {
            _repoTeacher= new TeacherRepository();
        }
        public void Create(Group entity)
        {
            if (entity is null) throw new ArgumentNullException();
            AppDbContext<Group>.datas.Add(entity);
        }

        public void Delete(Group entity)
        {
            if (entity is null) throw new ArgumentNullException();
            AppDbContext<Group>.datas.Remove(entity);
        }

        public Group Get(Predicate<Group> predicate)
        {
            return AppDbContext<Group>.datas.Find(predicate);
        }

        public List<Group> GetAll(Predicate<Group> predicate = null)
        {
          return predicate == null ? AppDbContext<Group>.datas : AppDbContext<Group>.datas.FindAll(predicate);
        }

        public void Update(Group entity)
        {
           if(entity is null) throw new ArgumentNullException();
            var dbGroup = Get(g => g.Id == entity.Id);
            if (dbGroup == null) throw new ArgumentNullException();

            if(entity.Teacher == null)
            {
                entity.Teacher = dbGroup.Teacher;
            }
            else
            {
                dbGroup.Teacher = entity.Teacher;
            }
            if (String.IsNullOrWhiteSpace(entity.Name))
                entity.Name = dbGroup.Name;
            dbGroup.Name = String.Concat(entity.Name[0].ToString().ToUpper())+ entity.Name.Substring(1).ToLower();
            
            if(entity.Capacity == 0)
                entity.Capacity = dbGroup.Capacity;
            dbGroup.Capacity = entity.Capacity;
        }
    }
}
