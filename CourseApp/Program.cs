using CourseApp.Controller;
using ServiceLayer.Helpers.Enums;
using ServiceLayer.Helpers.Extentions;
using Spectre.Console;

TeacherController teacherController = new();
GroupController groupController = new();
AnsiConsole.Write(new FigletText("Hi, P135!").Color(Color.Red3));

while (true)
{
    GetConsole();
    Option: string optionStr = Console.ReadLine();
    int optionNum;
    bool isCorrectOption = int.TryParse(optionStr, out optionNum);
    if (isCorrectOption)
    {
        switch (optionNum)
        {
            case (int)Options.TeacherOptions:
                Console.Clear();
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
                        case (int)Options.BackToTeacherOptions:
                            Console.Clear();
                            break;
                        default:
                            ConsoleColor.Red.WriteConsole("Please, enter correct teacher option");
                            goto TeacherOption;
                    }
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Please, enter correct format option");
                    goto TeacherOption;
                }
                break;
            case (int)Options.GroupOptions:
                Console.Clear();
                GetGroupOption();
               GroupOption: string groupOptionStr = Console.ReadLine();
                int groupOption;
                bool isCorrectGroupOption = int.TryParse(groupOptionStr, out groupOption);
                if (isCorrectGroupOption && groupOption > 0)
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
                        case (int)Options.BackToGroupOptions:
                            Console.Clear();
                            break;
                        default:
                            ConsoleColor.Red.WriteConsole("Please, enter correct group option");
                            goto GroupOption;
                    }
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Please, enter correct format option");
                    goto GroupOption;
                }
                break;
        default:
            ConsoleColor.Red.WriteConsole("Please, enter correct option");
            break;
        }
    }
    //else if(optionNum >= 0 && optionNum < 3)
    //{
    //    ConsoleColor.Red.WriteConsole("Please, enter correct option");
    //    goto Option;
    //}
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
       ("Teacher options: \n1-Create\n2-Delete\n3-Get all\n4-Get by id\n5-Search by name and surname\n6-Update\n7-Back to menu");
}
static void GetGroupOption()
{
    ConsoleColor.DarkCyan.WriteConsole
       ("Group options: \n1-Create\n2-Update\n3-Get by id\n4-Delete" +
       "\n5-Get groups by capacity\n6-Get groups by teacherId " +
       "\n7-Get all groups  by teacherName\n8-Search method for group by name\n9-Get all groups count,\n10-Back to menu"
       );
}