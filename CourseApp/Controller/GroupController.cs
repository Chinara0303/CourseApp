using DomainLayer.Models;
using ServiceLayer.Exceptions;
using ServiceLayer.Helpers.Constants;
using ServiceLayer.Helpers.Extentions;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;

namespace CourseApp.Controller
{
    internal class GroupController
    {
        private readonly IGroupService _groupService;
        private readonly ITeacherService _teacherService;
        public GroupController()
        {
            _groupService = new GroupService();
            _teacherService = new TeacherService();
        }

        public void Create()
        {
            ConsoleColor.Cyan.WriteConsole("Please, enter group name");
            GroupName: string groupName = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(groupName))
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.StringMessage + ResponseMessages.EnterAgainMessage);
                goto GroupName;
            }

            ConsoleColor.Cyan.WriteConsole("Please, enter group capacity");
            Capacity: string capacityStr = Console.ReadLine();
            int capacity;
            bool isCorrectCapacity = int.TryParse(capacityStr, out capacity);
            if (!isCorrectCapacity || capacity < 0)
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.FormatMessage + ResponseMessages.EnterAgainMessage);
                goto Capacity;
            }
            else if(capacity > 20)
            {
                ConsoleColor.Red.WriteConsole("Can not be greater than 20" + ResponseMessages.EnterAgainMessage);
                goto Capacity;
            }
            else if(capacity.CheckNumEqualZero())
                goto Capacity;
           
            ConsoleColor.Cyan.WriteConsole("Please, enter teacher id");
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
                if (!isCorrectId || id < 0)
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.FormatMessage + ResponseMessages.EnterAgainMessage);
                    goto Id;
                }
                else if (id.CheckNumEqualZero())
                {
                    goto Id;
                }
            }
            try
            {
                Group newGroup = new Group()
                {
                    Name = String.Concat(groupName[0].ToString().ToUpper(), groupName.Substring(1).ToLower()),
                    Capacity = capacity,
                    CreateDate = DateTime.Now
                };
                _groupService.Create(id, newGroup);
                ConsoleColor.Green.WriteConsole
                (
                    $"Group added:\nId:{newGroup.Id}, Name:{newGroup.Name}, Capacity:{newGroup.Capacity}," +
                    $" Create date:{newGroup.CreateDate.ToString("MM.dd.yyyy")}," +
                    $" Teacher Info:{newGroup.Teacher.Id},{newGroup.Teacher.Name} {newGroup.Teacher.Surname}," +
                    $"{newGroup.Teacher.Age},{newGroup.Teacher.Address}"
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

            if (!isCorrectId || id < 0)
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.FormatMessage + ResponseMessages.EnterAgainMessage);
                goto Id;
            }
            else if (id.CheckNumEqualZero())
            {
                goto Id;
            }
            else
            {
                try
                {
                    _groupService.Delete(id);
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

            if (!isCorrectCapacity || capacity < 0)
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.FormatMessage + ResponseMessages.EnterAgainMessage);
                goto Capacity;
            }
            else if (capacity > 20)
            {
                ConsoleColor.Red.WriteConsole("Can not be greater than 20" + ResponseMessages.EnterAgainMessage);
                goto Capacity;
            }
            else if (capacity.CheckNumEqualZero())
                goto Capacity;
            else
            {
                try
                {
                    List<Group> dbGroups = _groupService.GetAllByCapacity(capacity);
                    foreach (Group group in dbGroups)
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

            if (!isCorrectTeacherId || id < 0)
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.FormatMessage + ResponseMessages.EnterAgainMessage);
                goto Id;
            }
            else if (id.CheckNumEqualZero())
            {
                goto Id;
            }
            else
            {
                try
                {
                    List<Group> dbGroups = _groupService.GetAllByTeacherId(id);

                    foreach (Group group in dbGroups)
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
                    List<Group> dbGroups= _groupService.GetAllByTeacherName(teacherName);
                    foreach (Group group in dbGroups)
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
                    List<Group> dbGroups = _groupService.SearchByName(searchText);
                    foreach (Group group in dbGroups)
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

            if (!isCorrectId || id < 0)
            {
                ConsoleColor.Red.WriteConsole("Please, enter correct format id");
                goto Id;
            }
            else if (id.CheckNumEqualZero())
            {
                goto Id;
            }
            else
            {
                try
                {
                    Group dbGroup = _groupService.GetById(id);
                    ConsoleColor.Green.WriteConsole
                    (
                        $"Id:{dbGroup.Id}, Name:{dbGroup.Name}, Capacity:{dbGroup.Capacity}," +
                        $" Create date:{dbGroup.CreateDate.ToString("MM.dd.yyyy")}," +
                        $" Teacher info: {dbGroup.Teacher.Id},{dbGroup.Teacher.Name} {dbGroup.Teacher.Surname}," +
                        $"{dbGroup.Teacher.Age},{dbGroup.Teacher.Address}"
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
                int number = _groupService.GetCount();
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
            if (!isCorrectId || id < 0)
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.FormatMessage + ResponseMessages.EnterAgainMessage);
                goto Id;
            }
            else if (id.CheckNumEqualZero())
                goto Id;
            try
            {
                Group dbGroup = _groupService.GetById(id);
                if (dbGroup == null) throw new InvalidGroupException(ResponseMessages.NotFound);
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
                return;
            }
           
            ConsoleColor.Cyan.WriteConsole("Please, enter group name" + ResponseMessages.ForEmptyInputMessage);
            string groupName = Console.ReadLine();

            ConsoleColor.Cyan.WriteConsole("Please, enter group capacity" + ResponseMessages.ForEmptyInputMessage);
        Capacity: string capacityStr = Console.ReadLine();
            int capacity;
            int checkCap = 0;

            if (String.IsNullOrWhiteSpace(capacityStr))
                capacity = checkCap;
            else
            {
                bool IsCorrectCapacity = int.TryParse(capacityStr, out capacity);

                if (!IsCorrectCapacity || capacity < 0)
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.FormatMessage);
                    goto Capacity;
                }
                else if (capacity > 20)
                {
                    ConsoleColor.Red.WriteConsole("Can not be greater than 20" + ResponseMessages.EnterAgainMessage);
                    goto Capacity;
                }
            }

            ConsoleColor.Cyan.WriteConsole("Please, enter teacher id" + ResponseMessages.ForEmptyInputMessage);
            TeacherId: string teacherIdStr = Console.ReadLine();
            int teacherId;
            int checkTeacherId = 0;
            if (String.IsNullOrWhiteSpace(teacherIdStr))
                teacherId = checkTeacherId;
            else
            {
                bool isCorrectTeacherId = int.TryParse(teacherIdStr, out teacherId);

                if (!isCorrectTeacherId || teacherId < 0)
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.FormatMessage);
                    goto TeacherId;
                }
                else if (id.CheckNumEqualZero())
                    goto TeacherId;
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
                    _groupService.Update(id, newGroup);
                    ConsoleColor.Green.WriteConsole($"Successfully updated");
                }
                else
                {
                    Teacher existTeacher = _teacherService.GetById(teacherId);
                    if (existTeacher == null) throw new ArgumentNullException();
                    Group newGroup = new Group()
                    {
                        Name = groupName,
                        Capacity = capacity,
                        Teacher = existTeacher
                    };
                    _groupService.Update(id, newGroup);
                    ConsoleColor.Green.WriteConsole($"Successfully updated");
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }
    }
}
