using DomainLayer.Models;
using ServiceLayer.Exceptions;
using ServiceLayer.Helpers.Constants;
using ServiceLayer.Helpers.Extentions;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;

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

            if (String.IsNullOrWhiteSpace(teacherName))
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.StringMessage + ResponseMessages.EnterAgainMessage);
                goto TeacherName;
            }
            else if (teacherName.CheckSymbol())
            {
                goto TeacherName;
            }
          
            ConsoleColor.Cyan.WriteConsole("Please,enter teacher surname");
            TeacherSurname: string teacherSurname = Console.ReadLine();
          
            if (String.IsNullOrWhiteSpace(teacherSurname))
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.StringMessage + ResponseMessages.EnterAgainMessage);
                goto TeacherSurname;
            }
            else if (teacherSurname.CheckSymbol())
                goto TeacherSurname;
            
            ConsoleColor.Cyan.WriteConsole("Please,enter teacher address");
            TeacherAddress: string teacherAddress = Console.ReadLine();
          
            if (String.IsNullOrWhiteSpace(teacherAddress))
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.StringMessage + ResponseMessages.EnterAgainMessage);
                goto TeacherAddress;
            }
           
            ConsoleColor.Cyan.WriteConsole("Please,enter teacher age");
            TeacherAge: string ageStr = Console.ReadLine();

            int age;
            bool IsCorrectAge = int.TryParse(ageStr, out age);
            if (!IsCorrectAge)
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.FormatMessage + ResponseMessages.EnterAgainMessage);
                goto TeacherAge;
            }
            else if(age < 22)
            {
                ConsoleColor.Red.WriteConsole("Age cannot be less than 22");
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
                    ConsoleColor.Green.WriteConsole($"Teacher added:\nId:{teacher.Id}, Name:{response.Name}, " +
                        $"Surname:{response.Surname}, Age:{response.Age}, Address:{response.Address}");
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

            if (!isCorrectId || id < 0)
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.FormatMessage + ResponseMessages.EnterAgainMessage);
                goto Id;
            }
            else if(id.CheckNumEqualZero())
                goto Id;
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
            try
            {
                var result = _service.GetAll();
                if (result.Count == 0)
                    ConsoleColor.Red.WriteConsole(ResponseMessages.NotFound);

                foreach (var teacher in result)
                {
                    ConsoleColor.Green.WriteConsole($"Id:{teacher.Id}, Name:{teacher.Name}, Surname:{teacher.Surname}, Age:{teacher.Age}, Address:{teacher.Address}");
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
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
                ConsoleColor.Red.WriteConsole(ResponseMessages.FormatMessage + ResponseMessages.EnterAgainMessage);
                goto Id;
            }
            else if(id.CheckNumEqualZero())
                goto Id;
            else
            {
                try
                {
                    Teacher response = _service.GetById(id);
                    ConsoleColor.Green.WriteConsole($"Id:{response.Id}, Name:{response.Name}, Surname:{response.Surname}, Age:{response.Age}, Address:{response.Address}");
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message + ResponseMessages.EnterAgainMessage);
                }
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
                ConsoleColor.Red.WriteConsole(ResponseMessages.FormatMessage + ResponseMessages.EnterAgainMessage);
                goto Id;
            }
            else if(id.CheckNumEqualZero())
            {
                goto Id;
            }
            try
            {
                Teacher dbTeacher = _service.GetById(id);
                if (dbTeacher is null) throw new InvalidTeacherException(ResponseMessages.NotFound);

                ConsoleColor.Cyan.WriteConsole("Please, enter teacher name" + ResponseMessages.EnterAgainMessage);
                TeacherName: string teacherName = Console.ReadLine();
                if (teacherName.Trim().CheckSymbol())
                {
                    goto TeacherName;
                }

                ConsoleColor.Cyan.WriteConsole("Please, enter teacher surname" + ResponseMessages.ForEmptyInputMessage);
                TeacherSurname: string teacherSurname = Console.ReadLine();
                if (teacherSurname.Trim().CheckSymbol())
                {
                    goto TeacherSurname;
                }

                ConsoleColor.Cyan.WriteConsole("Please, enter teacher address" + ResponseMessages.ForEmptyInputMessage);
                string teacherAddress = Console.ReadLine();

                ConsoleColor.Cyan.WriteConsole("Please, enter teacher age" + ResponseMessages.ForEmptyInputMessage);
                Age: string ageStr = Console.ReadLine();
                int age;
                int checkNum = 0;
                if (String.IsNullOrWhiteSpace(ageStr))
                {
                    age = checkNum;
                }
                else
                {
                    bool IsCorrectAge = int.TryParse(ageStr, out age);
                    if (!IsCorrectAge || age < 0)
                    {
                        ConsoleColor.Red.WriteConsole(ResponseMessages.FormatMessage + ResponseMessages.EnterAgainMessage);
                        goto Age;
                    }
                    else if(age.CheckNumEqualZero())
                        goto Age;
                }
                Teacher teacher = new Teacher()
                {
                    Name = teacherName,
                    Surname = teacherSurname,
                    Address = teacherAddress,
                    Age = age
                };
               _service.Update(id, teacher);    
                ConsoleColor.Green.WriteConsole("Successfully updated");
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }
        public void SearchByNameAndSurname()
        {
            ConsoleColor.Cyan.WriteConsole("Please,enter search text...");

        Searchtext: string searchText = Console.ReadLine();

            if (searchText.Trim().CheckSymbol())
            {
                goto Searchtext;
            }
            try
            {
                List<Teacher> result = _service.SearchByNameAndSurname(searchText.Trim());

                if (result.Count == 0)
                    ConsoleColor.Red.WriteConsole(ResponseMessages.NotFound);

                foreach (Teacher teacher in result)
                {
                    ConsoleColor.Green.WriteConsole($"Id:{teacher.Id}, Name:{teacher.Name}, Surname:{teacher.Surname}, Age:{teacher.Age}, Address:{teacher.Address}");
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }
    }
}
