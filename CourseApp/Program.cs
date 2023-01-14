
using CourseApp.Controller;
using ServiceLayer.Helpers.Enums;
using ServiceLayer.Helpers.Extentions;

TeacherController teacherController = new();
GroupController groupController = new();
while (true)
{
    GetConsole();
    Option: string optionStr = Console.ReadLine();
    int optionNum;
    bool IsCorrectOption = int.TryParse(optionStr, out optionNum);
    if (IsCorrectOption)
    {
        switch (optionNum)
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
            case (int)Options.CreateGroup:
                groupController.Create();
                break;
            case (int)Options.UpdateGroup:
                //groupController.Delete();
                break;
            case (int)Options.GetGroupById:
                //teacherController.Update();
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
            case (int)Options.GetAllGroupsByCount:
                //groupController.();
                break;
            default:
                ConsoleColor.Red.WriteConsole("Please, enter correct option");
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
    ConsoleColor.Magenta.WriteConsole
        ("Teacher options:\n1-Create\n2-Delete\n3-Get all\n4-Get by id\n5-Search by name and surname\n6-Update");
    ConsoleColor.Magenta.WriteConsole
        ("Group options:\n7-Create\n8-Update\n9-Get by id\n10-Delete" +
        "\n11-Get groups by capacity\n12-Get groups by teacherId " +
        "\n13-Get all groups  by teacherName\n14-Search method for group by name\n15-Get all groups count");
}