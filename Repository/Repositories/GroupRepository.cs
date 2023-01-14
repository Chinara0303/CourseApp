using DomainLayer.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class GroupRepository : IRepository<Group>
    {
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
        }
    }
}
