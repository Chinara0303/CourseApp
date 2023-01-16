using DomainLayer.Models;
using ServiceLayer.Exceptions;
using ServiceLayer.Helpers.Constants;
using ServiceLayer.Helpers.Extentions;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;
using System.Text.RegularExpressions;
using Group = DomainLayer.Models.Group;

namespace CourseApp.Controller
{
    internal class GroupController
    {
        private readonly IGroupService _service;
        private readonly ITeacherService _serviceTeacher = new TeacherService();
        public GroupController() => _service = new GroupService();

       

        public void Create()
        {
            ConsoleColor.Cyan.WriteConsole("Please, enter group name");
            GroupName: string groupName = Console.ReadLine();
            try
            {
                if (String.IsNullOrWhiteSpace(groupName))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.StringMessage + ResponseMessages.EnterAgainMessage);
                    goto GroupName;
                }
                else if (groupName.CheckSymbol())
                {
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
            if (!isCorrectCapacity || capacity < 0)
            {
                ConsoleColor.Red.WriteConsole("Please, enter correct format capacity number");
                goto Capacity;
            }
            else if(capacity > 20)
            {
                ConsoleColor.Red.WriteConsole("Can not be greater than 20" + ResponseMessages.EnterAgainMessage);
                goto Capacity;
            }
                ConsoleColor.Cyan.WriteConsole("Please, enter teacher id for group");
            Id: string idStr =  Console.ReadLine();
            int id;
            int checkId = 0;
            if (String.IsNullOrWhiteSpace(idStr))
            {
                id = checkId;
            }
            else
            {
                bool isCorrectId = int.TryParse(idStr, out id);
                if (!isCorrectId && id <= 0)
                {
                    ConsoleColor.Red.WriteConsole("Please, enter correct format id" + ResponseMessages.EnterAgainMessage);
                    goto Id;
                }
            }
            try
            {
                Group group = new Group()
                {
                    Name = groupName,
                    Capacity = capacity,
                    CreateDate = DateTime.Now,
                };
                _service.Create(id, group);
                ConsoleColor.Green.WriteConsole
                (
                    $"Id:{group.Id}, Name:{group.Name}, Capacity:{group.Capacity}," +
                    $" Create date:{group.CreateDate.ToString("MM.dd.yyyy")}," +
                    $" Teacher Info:{group.Teacher.Id},{group.Teacher.Name} {group.Teacher.Surname}," +
                    $"{group.Teacher.Age},{group.Teacher.Address}"
                );
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }
        public void Delete()
        {
            ConsoleColor.Cyan.WriteConsole("Please, enter group id");
        Id: string idStr = Console.ReadLine();
            int id;
            bool isCorrectId = int.TryParse(idStr, out id);

            if (!isCorrectId && id <= 0)
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
        public void GetAllByCapacity()
        {
            ConsoleColor.Cyan.WriteConsole("Please, enter group capacity number");
        Capacity: string capacityStr = Console.ReadLine();
            int capacity;
            bool isCorrectCapacity = int.TryParse(capacityStr, out capacity);

            if (!isCorrectCapacity || capacity <= 0)
            {
                ConsoleColor.Red.WriteConsole("Please, enter correct format id");
                goto Capacity;
            }
            else
            {
                try
                {
                    var groups = _service.GetAllByCapacity(capacity);
                    foreach (Group group in groups)
                    {
                        ConsoleColor.Green.WriteConsole
                        (
                          $"Id:{group.Id}, Name:{group.Name}, Capacity:{group.Capacity}," +
                          $" Create date:{group.CreateDate.ToString("MM.dd.yyyy")}," +
                          $" Teacher info:{group.Teacher.Id},{group.Teacher.Name} {group.Teacher.Surname}," +
                          $"{group.Teacher.Age},{group.Teacher.Address}"
                        );
                    }
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                }
            }
        }
        public void GetAllByTeacherId()
        {
            ConsoleColor.Cyan.WriteConsole("Please, enter group teacher id");
        Id: string teacherIdStr = Console.ReadLine();
            int id;
            bool isCorrectTeacherId = int.TryParse(teacherIdStr, out id);

            if (!isCorrectTeacherId && id <= 0)
            {
                ConsoleColor.Red.WriteConsole("Please, enter correct format id");
                goto Id;
            }
            else
            {
                try
                {
                    var groups = _service.GetAllByTeacherId(id);

                    foreach (Group group in groups)
                    {
                        ConsoleColor.Green.WriteConsole
                        (
                          $"Id:{group.Id}, Name:{group.Name}, Capacity:{group.Capacity}," +
                          $" Create date:{group.CreateDate.ToString("MM.dd.yyyy")}," +
                          $" Teacher info: {group.Teacher.Id},{group.Teacher.Name} {group.Teacher.Surname}," +
                          $"{group.Teacher.Age},{group.Teacher.Address}"
                        );
                    }
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                }
            }
        }
        public void GetAllByTeacherName()
        {
            ConsoleColor.Cyan.WriteConsole("Please, enter group teacher name");
            TeacherName: string teacherName = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(teacherName))
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.StringCharacterMessage + ResponseMessages.EnterAgainMessage);
                goto TeacherName;
            }
            else
            {
                try
                {
                    var groups = _service.GetAllByTeacherName(teacherName);
                    foreach (Group group in groups)
                    {
                        ConsoleColor.Green.WriteConsole
                        (
                          $"Id:{group.Id}, Name:{group.Name}, Capacity:{group.Capacity}," +
                          $" Create date:{group.CreateDate.ToString("MM.dd.yyyy")}," +
                          $" Teacher info:{group.Teacher.Id},{group.Teacher.Name} {group.Teacher.Surname}," +
                          $"{group.Teacher.Age},{group.Teacher.Address}"
                        );
                    }
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                }
            }
        }
        public void SearchByName()
        {
            ConsoleColor.Cyan.WriteConsole("Please, enter group search text");
        SearchText: string searchText = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(searchText))
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.StringCharacterMessage + ResponseMessages.EnterAgainMessage);
                goto SearchText;
            }
            else
            {
                try
                {
                    var groups = _service.SearchByName(searchText);
                    foreach (DomainLayer.Models.Group group in groups)
                    {
                        ConsoleColor.Green.WriteConsole
                        (
                          $"Id:{group.Id}, Name:{group.Name}, Capacity:{group.Capacity}," +
                          $"Create date:{group.CreateDate.ToString("MM.dd.yyyy")}," +
                          $"Teacher info: {group.Teacher.Id},{group.Teacher.Name} {group.Teacher.Surname}," +
                          $"{group.Teacher.Age},{group.Teacher.Address}"
                        );
                    }
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                }
            }
        }
        public void GetById()
        {
            ConsoleColor.Cyan.WriteConsole("Please, enter group id");
        Id: string IdStr = Console.ReadLine();
            int id;
            bool isCorrectId = int.TryParse(IdStr, out id);

            if (!isCorrectId || id <= 0)
            {
                ConsoleColor.Red.WriteConsole("Please, enter correct format id");
                goto Id;
            }
            else
            {
                try
                {
                    var group = _service.GetById(id);
                    ConsoleColor.Green.WriteConsole
                    (
                        $"Id:{group.Id}, Name:{group.Name}, Capacity:{group.Capacity}," +
                        $" Create date:{group.CreateDate.ToString("MM.dd.yyyy")}," +
                        $" Teacher info: {group.Teacher.Id},{group.Teacher.Name} {group.Teacher.Surname}," +
                        $"{group.Teacher.Age},{group.Teacher.Address}"
                   );
                   
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                }
            }
        }
        public void GetCount()
        {
            try
            {
                int number = _service.GetCount();
                ConsoleColor.Green.WriteConsole($"Group Count: {number}");
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
            
        }

        public void Update()
        {
            ConsoleColor.Cyan.WriteConsole("Please,enter group id");
        Id: string idStr = Console.ReadLine();
            int id;
            bool isCorrectId = int.TryParse(idStr, out id);
            try
            {
                Group dbGroup = _service.GetById(id);
                if (dbGroup == null) throw new InvalidGroupException(ResponseMessages.NotFound);
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
                return;
            }
            if (!isCorrectId || id <= 0)
            {
                ConsoleColor.Red.WriteConsole("Please, enter correct format id");
                goto Id;
            }
           
            ConsoleColor.Cyan.WriteConsole("Please, enter group name" + ResponseMessages.EnterAgainMessage);
            string groupName = Console.ReadLine();

            ConsoleColor.Cyan.WriteConsole("Please, enter group capacity");
        Capacity: string capacityStr = Console.ReadLine();
            int capacity;
            int checkCap = 0;
            if (String.IsNullOrWhiteSpace(capacityStr))
            {
                capacity = checkCap;
            }
            else
            {
                bool IsCorrectCapacity = int.TryParse(capacityStr, out capacity);

                if (!IsCorrectCapacity || capacity <= 0)
                {
                    ConsoleColor.Red.WriteConsole("Please, enter correct format capacity");
                    goto Capacity;
                }
            }
            if (capacity > 20)
            {
                ConsoleColor.Red.WriteConsole("Can not be greater than 20" + ResponseMessages.EnterAgainMessage);
                goto Capacity;
            }

            ConsoleColor.Cyan.WriteConsole("Please, enter teacher id / If you don't want to change, enter the previous teacher id ");
        TeacherId: string teacherIdStr = Console.ReadLine();
            int teacherId;
            int checkTeacherId = 0;
            if (String.IsNullOrWhiteSpace(teacherIdStr))
            {
                teacherId = checkTeacherId;
            }
            else
            {
                bool isCorrectTeacherId = int.TryParse(teacherIdStr, out teacherId);

                if (!isCorrectTeacherId || teacherId <= 0)
                {
                    ConsoleColor.Red.WriteConsole("Please, enter correct format teacher id ");
                    goto TeacherId;
                }
            }
            try
            {
                if(teacherId == 0)
                {
                    
                    Group newGroup = new Group()
                    {
                        Name = groupName,
                        Capacity = capacity,
                    };
                    Group group = new();
                    group = _service.Update(id, newGroup);
                    ConsoleColor.Green.WriteConsole($"Successfully updated");
                }
                else
                {
                    Teacher existTeacher = _serviceTeacher.GetById(teacherId);
                    if (existTeacher == null) throw new ArgumentNullException();
                    Group newGroup = new Group()
                    {
                        Name = groupName,
                        Capacity = capacity,
                        Teacher = existTeacher
                    };
                    Group group = new();
                    group = _service.Update(id, newGroup);
                    ConsoleColor.Green.WriteConsole($"Successfully updated {group.Name} {group.Teacher.Id}");
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }
    }
}
