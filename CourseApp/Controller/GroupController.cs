using DomainLayer.Models;
using ServiceLayer.Helpers.Constants;
using ServiceLayer.Helpers.Extentions;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;
using System.Text.RegularExpressions;

namespace CourseApp.Controller
{
    internal class GroupController
    {
        private readonly IGroupService _service;
        public GroupController() => _service = new GroupService();

        string pattern = "^(?!\\s+$)['.-]+$";
        string msg = " / Please enter again";
        string msgForEmptyInput = " / If you leave it blank, there will be no change";

        public void Create()
        {
            ConsoleColor.Cyan.WriteConsole("Please, enter group name");
            GroupName: string groupName = Console.ReadLine();
            try
            {
                if (String.IsNullOrWhiteSpace(groupName))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.StringMessage + msg);
                    goto GroupName;
                }
                else if (Regex.IsMatch(groupName, pattern))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.StringCharacterMessage + msg);
                    goto GroupName;
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
                goto GroupName;
            }

            ConsoleColor.Cyan.WriteConsole("Please, enter group capacity");
        Capacity: string capacityStr = Console.ReadLine();
            int capacity;
            bool isCorrectCapacity = int.TryParse(capacityStr, out capacity);
            if (!isCorrectCapacity && capacity < 0)
            {
                ConsoleColor.Red.WriteConsole("Please, enter correct format capacity number");
                goto Capacity;
            }
            else if(capacity >= 20)
            {
                ConsoleColor.Red.WriteConsole("Can not be greater than 20" + msg);
                goto Capacity;
            }
                ConsoleColor.Cyan.WriteConsole("Please, enter teacher id for group");
            Id: string idStr =  Console.ReadLine();
            int id;
            bool isCorrectId = int.TryParse(idStr, out id);
            if (!isCorrectId && id < 0)
            {
                ConsoleColor.Red.WriteConsole("Please, enter correct format id" + msg);
                goto Id;
            }
            else
            {
                try
                {
                    DomainLayer.Models.Group group = new DomainLayer.Models.Group()
                    {
                        Name = groupName,
                        Capacity = capacity,
                        CreateDate = DateTime.Now,
                    };
                    _service.Create(id, group);
                    ConsoleColor.Green.WriteConsole
                    (
                        $"Id:{group.Id}, Name:{group.Name}, Capacity:{group.Capacity}," +
                        $" Create date:{group.CreateDate.ToString("yyyy,MM,dd")}," +
                        $" Teacher:{group.Teacher.Id},{group.Teacher.Name} {group.Teacher.Surname}," +
                        $"{group.Teacher.Age},{group.Teacher.Address}"
                    );
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message + msg);
                    goto Id;
                }
            }
        }

        public void Delete()
        {
            ConsoleColor.Cyan.WriteConsole("Please, enter group id");
        Id: string idStr = Console.ReadLine();
            int id;
            bool isCorrectId = int.TryParse(idStr, out id);

            if (!isCorrectId && id < 0)
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
                    goto Id;
                }
            }
        }

        public void GetAllByCapacity()
        {
            ConsoleColor.Cyan.WriteConsole("Please, enter group capacity number");
        Capacity: string capacityStr = Console.ReadLine();
            int capacity;
            bool isCorrectCapacity = int.TryParse(capacityStr, out capacity);

            if (!isCorrectCapacity && capacity < 0)
            {
                ConsoleColor.Red.WriteConsole("Please, enter correct format id");
                goto Capacity;
            }
            else
            {
                try
                {
                    var groups = _service.GetAllByCapacity(capacity);
                    foreach (DomainLayer.Models.Group group in groups)
                    {
                        ConsoleColor.Green.WriteConsole
                        (
                          $"Id:{group.Id}, Name:{group.Name}, Capacity:{group.Capacity}," +
                          $" Create date:{group.CreateDate.ToString("yyyy,MM,dd")}," +
                          $" Teacher:{group.Teacher.Id},{group.Teacher.Name} {group.Teacher.Surname}," +
                          $"{group.Teacher.Age},{group.Teacher.Address}"
                        );
                    }
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                    goto Capacity;
                }
            }
        }

        public void GetAllByTeacherId()
        {
            ConsoleColor.Cyan.WriteConsole("Please, enter group teacher id");
        Id: string teacherIdStr = Console.ReadLine();
            int id;
            bool isCorrectTeacherId = int.TryParse(teacherIdStr, out id);

            if (!isCorrectTeacherId && id < 0)
            {
                ConsoleColor.Red.WriteConsole("Please, enter correct format id");
                goto Id;
            }
            else
            {
                try
                {
                    var groups = _service.GetAllByTeacherId(id);
                    foreach (DomainLayer.Models.Group group in groups)
                    {
                        ConsoleColor.Green.WriteConsole
                        (
                          $"Id:{group.Id}, Name:{group.Name}, Capacity:{group.Capacity}," +
                          $" Create date:{group.CreateDate.ToString("yyyy,MM,dd")}," +
                          $" Teacher:{group.Teacher.Id},{group.Teacher.Name} {group.Teacher.Surname}," +
                          $"{group.Teacher.Age},{group.Teacher.Address}"
                        );
                    }
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                    goto Id;
                }
            }
        }
        public void GetAllByTeacherName()
        {
            ConsoleColor.Cyan.WriteConsole("Please, enter group teacher name");
            TeacherName: string teacherName = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(teacherName))
            {
                ConsoleColor.Red.WriteConsole(msgForEmptyInput + msg);
                goto TeacherName;
            }
            else
            {
                try
                {
                    var groups = _service.GetAllByTeacherName(teacherName);
                    foreach (DomainLayer.Models.Group group in groups)
                    {
                        ConsoleColor.Green.WriteConsole
                        (
                          $"Id:{group.Id}, Name:{group.Name}, Capacity:{group.Capacity}," +
                          $" Create date:{group.CreateDate.ToString("yyyy,MM,dd")}," +
                          $" Teacher:{group.Teacher.Id},{group.Teacher.Name} {group.Teacher.Surname}," +
                          $"{group.Teacher.Age},{group.Teacher.Address}"
                        );
                    }
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                    goto TeacherName;
                }
            }
        }
    }
}
