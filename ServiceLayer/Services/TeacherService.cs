﻿using DomainLayer.Models;
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
            _repo.Delete(filteredteacher);
        }

        public List<Teacher> GetAll()
        {
           return _repo.GetAll();
        }

        public Teacher GetById(int? id)
        {
            if (id == null) throw new InvalidTeacherException(ResponseMessages.NotFound);
            return _repo.Get(t => t.Id == id);
        }

        public List<Teacher> SearchByNameAndSurname(string name, string surname)
        {
            if (String.IsNullOrWhiteSpace(name) && String.IsNullOrWhiteSpace(surname)) throw new InvalidTeacherException(ResponseMessages.StringMessage);
            List<Teacher> teachers = _repo.GetAll(t=>t.Name.ToLower() == name.ToLower() && t.Surname.ToLower() == surname.ToLower());
            if (teachers.Count == 0) throw new InvalidTeacherException(ResponseMessages.NotFound);
            return teachers;
        }

        public Teacher Update(int id, Teacher teacher)
        {
            throw new NotImplementedException();
        }
    }
}
