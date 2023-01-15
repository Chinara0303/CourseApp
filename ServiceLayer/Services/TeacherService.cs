using DomainLayer.Models;
using Repository.Data;
using Repository.Repositories;
using ServiceLayer.Exceptions;
using ServiceLayer.Helpers.Constants;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly TeacherRepository _repo;
        private int _count = 1;
        public TeacherService() => _repo = new TeacherRepository();
       
        public Teacher Create(Teacher teacher)
        {
            teacher.Id = _count;
            _repo.Create(teacher);
            _count++;
            return teacher;
        }

        public void Delete(int? id)
        {
            if (id == null) throw new InvalidTeacherException(ResponseMessages.NotFound);
            Teacher filteredteacher =  _repo.Get(t => t.Id == id);
            if (filteredteacher is null) throw new InvalidTeacherException(ResponseMessages.NotFound);
            _repo.Delete(filteredteacher);
        }

        public List<Teacher> GetAll()
        {
           return _repo.GetAll();
        }

        public Teacher GetById(int? id)
        {
            if (id == null) throw new InvalidTeacherException(ResponseMessages.NotFound);
            Teacher dbTeacher = _repo.Get(t => t.Id == id);
            if (dbTeacher is null) throw new InvalidTeacherException(ResponseMessages.NotFound);
            return dbTeacher;
        }
        public List<Teacher> SearchByNameAndSurname(string searchText)
        {
            if (String.IsNullOrWhiteSpace(searchText)) throw new InvalidTeacherException(ResponseMessages.StringMessage);
            List<Teacher> teachers = _repo.GetAll(t => t.Name.Trim().ToLower().Contains(searchText.Trim().ToLower()) || t.Surname.ToLower().Contains(searchText.ToLower()));
            if (teachers.Count == 0) throw new InvalidTeacherException(ResponseMessages.NotFound);
            return teachers;
        }
        public Teacher Update(int? id, Teacher teacher)
        {
            if (id == null) throw new InvalidTeacherException(ResponseMessages.NotFound);
            var res = GetById(id);
            if (res != null) 
            {
                teacher.Id = (int)id;
                if (String.IsNullOrWhiteSpace(teacher.Name))
                    teacher.Name = res.Name;
                res.Name = String.Concat(teacher.Name[0].ToString().ToUpper()) + teacher.Name.Substring(1).ToLower();
              
                if (String.IsNullOrWhiteSpace(teacher.Surname))
                    teacher.Surname = res.Surname;
                res.Surname = String.Concat(teacher.Surname[0].ToString().ToUpper()) + teacher.Surname.Substring(1).ToLower();
               
                if (String.IsNullOrWhiteSpace(teacher.Address))
                    teacher.Address = res.Address;
                res.Address = String.Concat(teacher.Address[0].ToString().ToUpper()) + teacher.Address.Substring(1).ToLower();
                
                if (teacher.Age == null)
                    teacher.Age = res.Age;
                res.Age = teacher.Age;
                _repo.Update(teacher);
            } 
            else
            {
               throw new InvalidTeacherException(ResponseMessages.NotFound);
            }
            return teacher;
        }
    }
}
