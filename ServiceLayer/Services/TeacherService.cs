using DomainLayer.Models;
using Repository.Repositories;
using ServiceLayer.Exceptions;
using ServiceLayer.Helpers.Constants;
using ServiceLayer.Services.Interfaces;

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
            
            Teacher filteredTeacher =  _repo.Get(t => t.Id == id);
           
            if (filteredTeacher is null) throw new InvalidTeacherException(ResponseMessages.NotFound);
            
            _repo.Delete(filteredTeacher);
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
            List<Teacher> teachers = _repo.GetAll(t => t.Name.ToLower().Contains(searchText.ToLower()) || t.Surname.ToLower().Contains(searchText.ToLower()));
          
            if (teachers.Count == 0) throw new InvalidTeacherException(ResponseMessages.NotFound);
          
            return teachers;
        }
        public Teacher Update(int? id, Teacher teacher)
        {
            if (id == null) throw new InvalidTeacherException(ResponseMessages.NotFound);
           
            teacher.Id =(int)id;
           
            _repo.Update(teacher);
            
            return teacher;
        }
    }
}