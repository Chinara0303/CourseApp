using DomainLayer.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (dbTeacher == null) throw new ArgumentNullException();

            if (String.IsNullOrWhiteSpace(entity.Name))
                    entity.Name = dbTeacher.Name;
            dbTeacher.Name = String.Concat(entity.Name[0].ToString().ToUpper()) + entity.Name.Substring(1).ToLower();

            if (String.IsNullOrWhiteSpace(entity.Surname))
                entity.Surname = dbTeacher.Surname;
            dbTeacher.Surname = String.Concat(entity.Surname[0].ToString().ToUpper()) + entity.Surname.Substring(1).ToLower();

            if (String.IsNullOrWhiteSpace(entity.Address))
                entity.Address = dbTeacher.Address;
            dbTeacher.Address = String.Concat(entity.Address[0].ToString().ToUpper()) + entity.Address.Substring(1).ToLower();
            
            if(entity.Age == 0)
                entity.Age = dbTeacher.Age;
            dbTeacher.Age = entity.Age;

        }
    }
}