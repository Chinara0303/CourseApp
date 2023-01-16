using CourseApp.Controller;
using ServiceLayer.Helpers.Enums;
using ServiceLayer.Helpers.Extentions;
using Spectre.Console;

TeacherController teacherController = new();
GroupController groupController = new();
while (true)
{
    AnsiConsole.Write(
    new FigletText("Hi, P135!")
        .Color(Color.Red3));
    GetConsole();
    Option: string optionStr = Console.ReadLine();
    int optionNum;
    bool isCorrectOption = int.TryParse(optionStr, out optionNum);
    if (isCorrectOption)
    {
        switch (optionNum)
        {
            case (int)Options.TeacherOptions:
                GetTeacherOptions();
            TeacherOption: string teacherOptionStr = Console.ReadLine();
                int teacherOption;

                bool isCorrectTeacherOption = int.TryParse(teacherOptionStr, out teacherOption);
                if (isCorrectTeacherOption)
                {
                    switch (teacherOption)
                    {
                        case (int)Options.CreateTeacher:
                            teacherController.Create();
                            break;
                        case (int)Options.Deleteteacher:
                            teacherController.Delete();
                            break;
                        case (int)Options.GetAllTeacher:
                            Console.Clear();
                            Console.WriteLine("All teachers:");
                            teacherController.GetAll();
                            break;
                        case (int)Options.GetTeacherById:
                            teacherController.GetById();
                            break;
                        case (int)Options.SearchNameAndSurnameByTeacher:
                            teacherController.SearchByNameAndSurname();
                            break;
                        case (int)Options.UpdateTeacher:
                            teacherController.Update();
                            break;
                    }
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Please, enter correct format option");
                    goto TeacherOption;
                }
                break;
            case (int)Options.GroupOptions:
                GetGroupOption();
               GroupOption: string groupOptionStr = Console.ReadLine();
                int groupOption;
                bool isCorrectGroupOption = int.TryParse(groupOptionStr, out groupOption);
                if (isCorrectGroupOption)
                {
                    switch (groupOption)
                    {
                        case (int)Options.CreateGroup:
                            groupController.Create();
                            break;
                        case (int)Options.UpdateGroup:
                            groupController.Update();
                            break;
                        case (int)Options.GetGroupById:
                            groupController.GetById();
                            break;
                        case (int)Options.DeleteGroup:
                            groupController.Delete();
                            break;
                        case (int)Options.GetAllGroupsByCapacity:
                            groupController.GetAllByCapacity();
                            break;
                        case (int)Options.GetAllGroupsByTeacherId:
                            groupController.GetAllByTeacherId();
                            break;
                        case (int)Options.GetAllGroupsByTeacherName:
                            groupController.GetAllByTeacherName();
                            break;
                        case (int)Options.SearchForGroupByName:
                            groupController.SearchByName();
                            break;
                        case (int)Options.GetAllGroupsByCount:
                            groupController.GetCount();
                            break;
                        default:
                            ConsoleColor.Red.WriteConsole("Please, enter correct option");
                            break;
                    }
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Please, enter correct format option");
                    goto GroupOption;
                }
                break;
        }
    }
    else
    {
        ConsoleColor.Red.WriteConsole("Please, enter correct format option");
        goto Option;
    }

}

static void GetConsole()
{
    ConsoleColor.White.WriteConsole("Please, choose one option");
    ConsoleColor.DarkCyan.WriteConsole("1-Teacher options");
    ConsoleColor.DarkCyan.WriteConsole("2-Group options");
   
   
}
static void GetTeacherOptions()
{
    ConsoleColor.DarkCyan.WriteConsole
       ("\n1-Create\n2-Delete\n3-Get all\n4-Get by id\n5-Search by name and surname\n6-Update");
}
static void GetGroupOption()
{
    ConsoleColor.DarkCyan.WriteConsole
       ("\n1-Create\n2-Update\n3-Get by id\n4-Delete" +
       "\n5-Get groups by capacity\n6-Get groups by teacherId " +
       "\n7-Get all groups  by teacherName\n8-Search method for group by name\n9-Get all groups count"
       );
}