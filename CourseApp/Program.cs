
using CourseApp.Controller;
using ServiceLayer.Helpers.Enums;
using ServiceLayer.Helpers.Extentions;

TeacherController teacherController = new();
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
                //teacherController.GetById();
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
    ConsoleColor.Magenta.WriteConsole("Teacher options:\n1-Create\n2-Delete\n3-Get all\n4-Get by id\n5-Search by name and surname\n6-Update");
}