using DomainLayer.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class TeacherRepository : IRepository<Teacher>
    {
        public void Create(Teacher entity)
        {
            if (entity == null) throw new ArgumentNullException();
            AppDbContext<Teacher>.datas.Add(entity);
        }

        public void Delete(Teacher entity)
        {
            if (entity == null) throw new ArgumentNullException();
            AppDbContext<Teacher>.datas.Remove(entity);
        }

        public Teacher Get(Predicate<Teacher> predicate)
        {
            return AppDbContext<Teacher>.datas.Find(predicate);
        }

        public List<Teacher> GetAll(Predicate<Teacher> predicate = null)
        {
            return predicate == null ? AppDbContext<Teacher>.datas : AppDbContext<Teacher>.datas.FindAll(predicate);
        }

        public void Update(Teacher entity)
        {
            if (entity == null) throw new ArgumentNullException();

            var dbTeacher = Get(t => t.Id == entity.Id);
            if (dbTeacher == null) throw new NullReferenceException();

            if (!String.IsNullOrWhiteSpace(entity.Name))
                 dbTeacher.Name = String.Concat(entity.Name[0].ToString().ToUpper(),entity.Name.Substring(1).ToLower());

            if (!String.IsNullOrWhiteSpace(entity.Surname))
                 dbTeacher.Surname = String.Concat(entity.Surname[0].ToString().ToUpper(),entity.Surname.Substring(1).ToLower());

            if (!String.IsNullOrWhiteSpace(entity.Address))
                 dbTeacher.Address = String.Concat(entity.Address[0].ToString().ToUpper(),entity.Address.Substring(1).ToLower());
            
            if(entity.Age != 0)
                dbTeacher.Age = entity.Age;
        }
    }
}