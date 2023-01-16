using DomainLayer.Models;
using Repository.Data;
using ServiceLayer.Exceptions;
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
        string msgForEmptyInput = " / If you leave it blank, there will be no change";
        string pattern = "^(?!\\s+$)[a-zA-Z'.-]+$";
        string? checkStr = null;
        public void Create()
        {
            ConsoleColor.Cyan.WriteConsole("Please,enter teacher name");
            TeacherName: string teacherName = Console.ReadLine();

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
          
            ConsoleColor.Cyan.WriteConsole("Please,enter teacher surname");
        TeacherSurname: string teacherSurname = Console.ReadLine();
          
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
            
            ConsoleColor.Cyan.WriteConsole("Please,enter teacher address");
        TeacherAddress: string teacherAddress = Console.ReadLine();
          
            if (String.IsNullOrWhiteSpace(teacherAddress))
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.StringMessage + "/ Please enter again");
                goto TeacherAddress;
            }
           
            ConsoleColor.Cyan.WriteConsole("Please,enter teacher age");
        TeacherAge: string ageStr = Console.ReadLine();

         
            int age;
            bool IsCorrectAge = int.TryParse(ageStr, out age);
            if (!IsCorrectAge || age < 0)
            {
                ConsoleColor.Red.WriteConsole("Please, enter correct format age");
                goto TeacherAge;
            }
            else
            {
                try
                {
                    Teacher teacher = new Teacher()
                    {
                        Name = String.Concat(teacherName[0].ToString().ToUpper()) + teacherName.Substring(1).ToLower(),
                        Surname = String.Concat(teacherSurname[0].ToString().ToUpper()) + teacherSurname.Substring(1).ToLower(),
                        Address = String.Concat(teacherAddress[0].ToString().ToUpper()) + teacherAddress.Substring(1).ToLower(),
                        Age = age
                    };
                    Teacher response = _service.Create(teacher);
                    ConsoleColor.Green.WriteConsole($"Id:{teacher.Id}, Name:{response.Name}, Surname:{response.Surname}, Age:{response.Age}, Address:{response.Address}");
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                    goto TeacherAge;
                }
            }

        }
        public void Delete()
        {
            ConsoleColor.Cyan.WriteConsole("Please,enter teacher id");
        Id: string idStr = Console.ReadLine();
            int id;
            bool isCorrectId = int.TryParse(idStr, out id);

            if (!isCorrectId && id<0)
            {
                ConsoleColor.Red.WriteConsole("Please, enter correct format id");
                goto Id;
            }
            else
            {
                try
                {
                    _service.Delete(id);
                    ConsoleColor.Green.WriteConsole("Successfully deleted");
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                }
            }
        }
        public void GetAll()
        {
            var result = _service.GetAll();
            if (result.Count == 0)
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

            if (!isCorrectId || id < 0)
            {
                ConsoleColor.Red.WriteConsole("Please, enter correct format id");
                goto Id;
            }
            else
            {
                try
                {
                    Teacher response = _service.GetById(id);
                    ConsoleColor.Green.WriteConsole($"Id:{response.Id}, Name:{response.Name}, Surname:{response.Surname}, Age:{response.Age}, Address:{response.Address}");
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message + msg);
                    goto Id;
                }
            }
        }
        public void SearchByNameAndSurname()
        {
            ConsoleColor.Cyan.WriteConsole("Please,enter search text...");
        Searchtext: string searchText = Console.ReadLine();
            try
            {
                if (String.IsNullOrWhiteSpace(searchText))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.StringMessage + msg);
                    goto Searchtext;
                }
                //else if (Regex.IsMatch(searchText, pattern))
                //{
                //    ConsoleColor.Red.WriteConsole(ResponseMessages.StringCharacterMessage + msg);
                //    goto Searchtext;
                //}
                List<Teacher> result = _service.SearchByNameAndSurname(searchText);
                if (result.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.NotFound);
                }
                foreach (Teacher teacher in result)
                {
                    ConsoleColor.Green.WriteConsole($"Id:{teacher.Id}, Name:{teacher.Name}, Surname:{teacher.Surname}, Age:{teacher.Age}, Address:{teacher.Address}");
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message + msg);
            }
        }
        public void Update()
        {

            ConsoleColor.Cyan.WriteConsole("Please, enter teacher id");
        Id: string idStr = Console.ReadLine();
            int id;
            bool isCorrectId = int.TryParse(idStr, out id);
           
            if (!isCorrectId || id < 0)
            {
                ConsoleColor.Red.WriteConsole("Please, enter correct format option number");
                goto Id;
            }
            try
            {
                Teacher dbTeacher = _service.GetById(id);
                if (dbTeacher is null) throw new InvalidTeacherException(ResponseMessages.NotFound);

                ConsoleColor.Cyan.WriteConsole("Please, enter teacher name" + msgForEmptyInput);
                string teacherName = Console.ReadLine();

                ConsoleColor.Cyan.WriteConsole("Please, enter teacher surname" + msgForEmptyInput);
                string teacherSurname = Console.ReadLine();

                ConsoleColor.Cyan.WriteConsole("Please, enter teacher address" + msgForEmptyInput);
                string teacherAddress = Console.ReadLine();

                ConsoleColor.Cyan.WriteConsole("Please, enter teacher age" + msgForEmptyInput);
            Age: string ageStr = Console.ReadLine();
                int age;
                if (String.IsNullOrWhiteSpace(ageStr))
                {
                    checkStr = ageStr;
                }
                else
                {
                    ageStr = checkStr;
                    bool IsCorrectAge = int.TryParse(ageStr, out age);
                    if (!IsCorrectAge || age < 0)
                    {
                        ConsoleColor.Red.WriteConsole("Please, enter correct format age");
                        goto Age;
                    }

                    //checkStr = age.ToString();

                    Teacher teacher = new Teacher()
                    {
                        Name = teacherName,
                        Surname = teacherSurname,
                        Address = teacherAddress,
                        Age = age
                    };
                    Teacher teacher1 = new();
                    teacher1 = _service.Update(id, teacher);
                    ConsoleColor.Green.WriteConsole("Successfully updated");
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message + msg);
                goto Id;
            }
        }
    }
}
