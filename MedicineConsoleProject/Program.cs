using MedicineConsoleProject.ExceptionsFolder;
using MedicineConsoleProject.Models;
using MedicineConsoleProject.Services;
using MedicineConsoleProject.Utilities;
namespace MedicineConsoleProject;

public class Program
{
    static void Main(string[] args)
    {
        UserService userService = new();
        MedicineService medicineService = new();

        //list all categoriee
        Helper.Print("Welcome!", ConsoleColor.DarkCyan);
    Menu:
        Helper.Print
            (
            "\n" +
            "Please select command: \n" +
            "1-User Registration \n" +
            "2-User Login \n" +
            "3-Exit", ConsoleColor.Cyan);


        string userCommand = Console.ReadLine();

        switch (userCommand)
        {
            case "1":
                userService.RegisterUser();
                goto Menu;
            case "2":
            repeatPassword:
                Helper.Print("Please enter email!", ConsoleColor.DarkCyan);
                string email = Console.ReadLine();
                Helper.Print("Please enter password:", ConsoleColor.DarkCyan);
                string password = Console.ReadLine();
                try
                {
                    string fullName = "";
                    userService.Login(email, password);
                    foreach (var item in DB.Users)
                    {
                        if (email == item.Email)
                        {
                            fullName = item.Fullname;
                        }
                    }
                    Helper.Print($"Welcome! {fullName} ", ConsoleColor.DarkCyan);

                    break;

                }
                catch (NotFoundException e)
                {
                    Helper.Print(e.Message, ConsoleColor.DarkCyan);
                    goto repeatPassword;
                }
                catch (Exception)
                {
                    throw;
                }
            case "3":
                Helper.Print("Bye bye ................", ConsoleColor.Yellow);
                return;
            default:
                Helper.Print("Please enter valid command", ConsoleColor.Red);
                goto Menu;
        }
        string loginUserCommand = Console.ReadLine();

        //Helper.Print("1.Create new Category \n" +
        //    "2.Create new Medicine \n " +
        //    "3.");


    }

}
