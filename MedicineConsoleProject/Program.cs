using MedicineConsoleProject.ExceptionsFolder;
using MedicineConsoleProject.Models;
using MedicineConsoleProject.Services;
using MedicineConsoleProject.Utilities;
namespace MedicineConsoleProject;

public class Program
{
    static void Main(string[] args)
    {
        User activeUser;
        UserService userService = new();
        MedicineService medicineService = new();
        CategoryService categoryService = new();

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
                Helper.Print("Please enter email:", ConsoleColor.DarkCyan);
                string email = Console.ReadLine();
                Helper.Print("Please enter password:", ConsoleColor.DarkCyan);
                string password = Console.ReadLine();
                try
                {
                    string fullName = "";
                    activeUser = userService.Login(email, password);
                    foreach (var item in DB.Users)
                    {
                        if (email == item.Email)
                        {
                            fullName = item.Fullname;
                        }
                    }
                    Helper.Print($"Welcome, {fullName}! ", ConsoleColor.DarkCyan);

                    break;

                }
                catch (NotFoundException e)
                {
                    Helper.Print(e.Message, ConsoleColor.Red);
                    goto Menu;
                }

            case "3":
                Helper.Print("Bye bye ................", ConsoleColor.Yellow);
                return;
            default:
                Helper.Print("Please enter valid command", ConsoleColor.Red);
                goto Menu;
        }

    UserMenu:
        Helper.Print(
            "1.Create new Category\n" +
            "2.Create new Medicine\n" +
            "3.Remove a medicine\n" +  //getallcategories
            "4.List all medicines\n" +
            "5.Update medicine\n" +
            "6.Find medicine by id\n" +
            "7.Find medicine by Name\n" +
            "8.Find medicine by category\n" +
            "0.Exit", ConsoleColor.Cyan);

        string loginUserCommand = Console.ReadLine();


        switch (loginUserCommand)
        {
            case "1":
                Helper.Print("Please enter category name:", ConsoleColor.DarkCyan);
                string categoryName = Console.ReadLine();
                Category category = new(categoryName);
                categoryService.CreateCategory(category);
                goto UserMenu;
            case "2":
                Helper.Print("Please enter medicine name:", ConsoleColor.DarkCyan);
                string medicineName = Console.ReadLine();
            repeatPrice:
                Helper.Print("Please enter medicine price:", ConsoleColor.DarkCyan);
                string priceStr = Console.ReadLine();
                if (!decimal.TryParse(priceStr, out decimal price))
                {
                    Helper.Print("Please enter valid price!!", ConsoleColor.Red);
                    goto repeatPrice;
                }
                int categoryId = GetValidCategoryID();
                Medicine medicine = new(medicineName, price, DateTime.Now, categoryId, activeUser.Id);
                try
                {
                    medicineService.CreateMedicine(medicine);
                }
                catch (NotFoundException ex)
                {
                    Helper.Print(ex.Message, ConsoleColor.Red);
                }
                goto UserMenu;
            case "3":
                break;
            case "4":
                medicineService.GetAllMedicines(activeUser.Id);
                goto UserMenu;
            case "7":
                Helper.Print("Existing medicines:", ConsoleColor.Cyan);
                foreach (var med in DB.Medicines)
                {
                    Helper.Print($"ID: {med.Id} - Name: {med.Name}", ConsoleColor.DarkCyan);
                }
                Helper.Print("Please enter medicine name:", ConsoleColor.Blue);
                string medName = Console.ReadLine();
                try
                {
                   Medicine med = medicineService.GetMedicineByName(medName, activeUser.Id);
                    string medCategoryName = "";
                    foreach (var cat in DB.Categories)
                    {
                        if (cat.Id == med.CategoryId)
                        {
                            medCategoryName = cat.Name;
                            break;
                        }
                    }
                    Helper.Print($"Medicine:{med.Name} - Price:{med.Price}- Category:{medCategoryName}- CreatedDate/Time:{med.CreatedDate}", ConsoleColor.White);
                }
                catch (NotFoundException ex)
                {
                    Helper.Print(ex.Message, ConsoleColor.Red);
                }
                goto UserMenu;
            case "8":
                int searchCategoryId = GetValidCategoryID();
                try
                {
                    medicineService.GetMedicineByCategory(searchCategoryId, activeUser.Id);
                }
                catch (NotFoundException ex)
                {
                    Helper.Print(ex.Message, ConsoleColor.Red);
                }

                goto UserMenu;
            default:
                break;
        }

    }


    public static int GetValidCategoryID()
    {
    categoryIdRepeat:
        Helper.Print("Existing categories:", ConsoleColor.Green);
        foreach (var category in DB.Categories)
        {
            Helper.Print($"ID: {category.Id} - Name: {category.Name}", ConsoleColor.DarkCyan);
        }
        Helper.Print("Please enter medicine categoryId:", ConsoleColor.DarkCyan);
        string categoryIdStr = Console.ReadLine();
        if (!int.TryParse(categoryIdStr, out int categoryId))
        {
            Helper.Print("Please enter a valid categoryId!", ConsoleColor.Red);
            goto categoryIdRepeat;
        }
        return categoryId;
    }

}
