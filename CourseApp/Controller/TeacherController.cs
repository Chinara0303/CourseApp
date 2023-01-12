using DomainLayer.Models;
using ServiceLayer.Helpers.Constants;
using ServiceLayer.Helpers.Extentions;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CourseApp.Controller
{
    internal class TeacherController
    {
        private readonly ITeacherService _service;
        public TeacherController() => _service = new TeacherService();

        public void Create()
        {
            ConsoleColor.Cyan.WriteConsole("Please,enter teacher name");
            TeacherName: string teacherName = Console.ReadLine();
            string pattern = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            if (teacherName == string.Empty)
            {
                ConsoleColor.Red.WriteConsole("The name of the teacher cannot be empty / Please enter again");
                goto TeacherName;
            }
            //else if (!Regex.IsMatch(teacherName, pattern))
            //{
            //    ConsoleColor.Red.WriteConsole("Please enter correct again");
            //    goto TeacherName;
            //}

            ConsoleColor.Cyan.WriteConsole("Please,enter teacher surname");
            TeacherSurname: string teacherSurname = Console.ReadLine();
            if (teacherSurname == string.Empty)
            {
                ConsoleColor.Red.WriteConsole("The surname of the teacher cannot be empty / Please enter again");
                goto TeacherSurname;
            }

            ConsoleColor.Cyan.WriteConsole("Please,enter teacher address");
            TeacherAddress: string teacherAddress = Console.ReadLine();
            if (teacherAddress == string.Empty)
            {
                ConsoleColor.Red.WriteConsole("The address of the teacher cannot be empty / Please enter again");
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
                    ConsoleColor.Green.WriteConsole($"Name:{response.Name}, Surname:{response.Surname}, Age:{response.Age}, Address:{response.Address}");
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
                ConsoleColor.Green.WriteConsole($"Name:{teacher.Name}, Surname:{teacher.Surname}, Age:{teacher.Age}, Address:{teacher.Address}");

            }
        }

    }
}
