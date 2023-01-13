using DomainLayer.Models;
using Repository.Data;
using ServiceLayer.Helpers.Constants;
using ServiceLayer.Helpers.Extentions;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CourseApp.Controller
{
    internal class TeacherController
    {
        private readonly ITeacherService _service;
        public TeacherController() => _service = new TeacherService();
        string msg = " / Please enter again";
        string pattern = @"^(?!\\s+$)[a-zA-Z,'. -]+$";
        public void Create()
        {
            ConsoleColor.Cyan.WriteConsole("Please,enter teacher name");
            TeacherName: string teacherName = Console.ReadLine();
           
            try
            {
                if (String.IsNullOrWhiteSpace(teacherName))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.StringMessage + msg);
                    goto TeacherName;
                }
                else if (!Regex.IsMatch(teacherName, pattern))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.StringCharacterMessage + msg);
                    goto TeacherName;
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message); 
                goto TeacherName;
            }
            ConsoleColor.Cyan.WriteConsole("Please,enter teacher surname");
            TeacherSurname: string teacherSurname = Console.ReadLine();
            try
            {
                if (String.IsNullOrWhiteSpace(teacherSurname))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.StringMessage + msg);
                    goto TeacherSurname;
                }
                else if (!Regex.IsMatch(teacherSurname, pattern))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.StringCharacterMessage + msg);
                    goto TeacherSurname;
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
                goto TeacherSurname;
            }
            ConsoleColor.Cyan.WriteConsole("Please,enter teacher address");
            TeacherAddress: string teacherAddress = Console.ReadLine();
            try
            {
                if (String.IsNullOrWhiteSpace(teacherAddress))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.StringMessage + "/ Please enter again");
                    goto TeacherAddress;
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
                goto TeacherAddress;
            }
            ConsoleColor.Cyan.WriteConsole("Please,enter teacher age");
            TeacherAge: string ageStr = Console.ReadLine();
            int age;
            bool IsCorrectAge = int.TryParse(ageStr, out age);
            if (IsCorrectAge)
            {
                try
                {
                    Teacher teacher = new Teacher()
                    {
                        Name = teacherName,
                        Surname = teacherSurname,
                        Address = teacherAddress,
                        Age = age
                    };
                   Teacher response =  _service.Create(teacher);
                   ConsoleColor.Green.WriteConsole($"Id:{teacher.Id}, Name:{response.Name}, Surname:{response.Surname}, Age:{response.Age}, Address:{response.Address}");
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                    goto TeacherAge;
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Please, enter correct format age");
                goto TeacherAge;
            }

        }

        public void Delete()
        {
            ConsoleColor.Cyan.WriteConsole("Please,enter teacher id");
            Id: string idStr = Console.ReadLine();
            int id;
            bool isCorrectId = int.TryParse(idStr, out id);
            if (isCorrectId)
            {
                try
                {
                   _service.Delete(id);
                   ConsoleColor.Green.WriteConsole("Successfully deleted");

                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                    goto Id;
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Please, enter correct format id");
                goto Id;
            }
        }

        public void GetAll()
        {
            var result = _service.GetAll();
            if (result.Count==0)
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.NotFound);
            }
            foreach (var teacher in result)
            {
                ConsoleColor.Green.WriteConsole($"Id:{teacher.Id}, Name:{teacher.Name}, Surname:{teacher.Surname}, Age:{teacher.Age}, Address:{teacher.Address}");

            }
        }

        public void GetById()
        {
            ConsoleColor.Cyan.WriteConsole("Please,enter teacher id");
            Id: string idStr = Console.ReadLine();
            int id;
            bool isCorrectId = int.TryParse(idStr, out id);
            if (isCorrectId)
            {
                try
                {
                    Teacher response = _service.GetById(id);
                    ConsoleColor.Green.WriteConsole($"Id:{response.Id}, Name:{response.Name}, Surname:{response.Surname}, Age:{response.Age}, Address:{response.Address}");
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                    goto Id;
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Please, enter correct format id");
                goto Id;
            }
        }
        public void SearchByNameAndSurname()
        {
            ConsoleColor.Cyan.WriteConsole("Please,enter teacher name");
            TeacherName: string teacherName = Console.ReadLine();
            try
            {
                if (String.IsNullOrWhiteSpace(teacherName))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.StringMessage + msg);
                    goto TeacherName;
                }
                else if (!Regex.IsMatch(teacherName, pattern))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.StringCharacterMessage + msg);
                    goto TeacherName;
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
                goto TeacherName;
            }

            ConsoleColor.Cyan.WriteConsole("Please,enter teacher surname");
            TeacherSurname: string teacherSurname = Console.ReadLine();
            try
            {
                if (String.IsNullOrWhiteSpace(teacherSurname))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.StringMessage + msg);
                    goto TeacherSurname;
                }
                else if (!Regex.IsMatch(teacherSurname, pattern))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.StringCharacterMessage + msg);
                    goto TeacherSurname;
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
                goto TeacherSurname;
            }

           List<Teacher> result = _service.SearchByNameAndSurname(teacherName, teacherSurname);
            if (result.Count == 0)
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.NotFound);
            }
            foreach (var teacher in result)
            {
                ConsoleColor.Green.WriteConsole($"Id:{teacher.Id}, Name:{teacher.Name}, Surname:{teacher.Surname}, Age:{teacher.Age}, Address:{teacher.Address}");

            }

        }

        public void Update()
        {
            ConsoleColor.Cyan.WriteConsole("Please,enter teacher id");
            Id: string idStr = Console.ReadLine();
            int id;
            
            
            
            
            bool isCorrectId = int.TryParse(idStr, out id);

            ConsoleColor.Cyan.WriteConsole("Please,enter teacher name");
           TeacherName: string teacherName = Console.ReadLine();
            try
            {
                //if (String.IsNullOrWhiteSpace(teacherName))
                //{
                //   teacherName = 
                //}
                 if (!Regex.IsMatch(teacherName, pattern))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.StringCharacterMessage + msg);
                    goto TeacherName;
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
                goto TeacherName;
            }
            ConsoleColor.Cyan.WriteConsole("Please,enter teacher surname");
        TeacherSurname: string teacherSurname = Console.ReadLine();
            try
            {
                if (String.IsNullOrWhiteSpace(teacherSurname))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.StringMessage + msg);
                    goto TeacherSurname;
                }
                else if (!Regex.IsMatch(teacherSurname, pattern))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.StringCharacterMessage + msg);
                    goto TeacherSurname;
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
                goto TeacherSurname;
            }
        //    ConsoleColor.Cyan.WriteConsole("Please,enter teacher address");
        //TeacherAddress: string teacherAddress = Console.ReadLine();
        //    try
        //    {
        //        if (String.IsNullOrWhiteSpace(teacherAddress))
        //        {
        //            ConsoleColor.Red.WriteConsole(ResponseMessages.StringMessage + msg);
        //            goto TeacherAddress;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ConsoleColor.Red.WriteConsole(ex.Message);
        //        goto TeacherAddress;
        //    }
            if (isCorrectId)
            {
                try
                {
                    Teacher teacher = new Teacher()
                    {
                        Name = teacherName,
                        Surname = teacherSurname,
                        //Address = teacherAddress,
                    };
                    Teacher teacher1 = new();
                    teacher1 = _service.Update(id, teacher);
                    ConsoleColor.Green.WriteConsole("Successfully updated");
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                    goto Id;
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Please, enter correct format id");
                goto Id;
            }
        }

    }
}
