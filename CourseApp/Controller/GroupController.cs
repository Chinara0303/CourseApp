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
            ConsoleColor.Cyan.WriteConsole("Please, enter teacher id for group");
           Id: string idStr =  Console.ReadLine();
            int id;
            bool isCorrectId = int.TryParse(idStr, out id);
            if (isCorrectId && id > 0)
            {
                try
                {
                    DomainLayer.Models.Group group = new DomainLayer.Models.Group()
                    {
                        Name = groupName,
                        Capacity = capacity,
                        CreateDate = DateTime.Now,
                    };
                    _service.Create(id,group);
                    ConsoleColor.Green.WriteConsole
                    (
                        $"Id:{group.Id}, Name:{group.Name}, Capacity:{group.Capacity}," +
                        $" Create date:{group.CreateDate}," +
                        $" Teacher:{group.Teacher.Id},{group.Teacher.Name},{group.Teacher.Surname}," +
                        $"{group.Teacher.Age},{group.Teacher.Address}"
                    );
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message + msg);
                    goto Id;
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Please, enter correct format id" + msg);
                goto Id;
            }

        }
    }
}
