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
                    activeUser = userService.Login(email, password);
                    Helper.Print($"Welcome, {activeUser.Fullname}! ", ConsoleColor.DarkCyan);
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


        if (activeUser.IsAdmin)
        {
        AdminMenu:
            Helper.Print(
                "Admin Menu:\n" +
                "1.Get all Users\n" +
                "2.Block User\n" +
                "3.Unblock User\n" +
                "0.Log out", ConsoleColor.Blue);

            string adminCommand = Console.ReadLine();

            switch (adminCommand)
            {
                case "1":
                    userService.GetAllUsers();
                    goto AdminMenu;
                case "2":
                repeatUserId:
                    Helper.Print("Existing users:", ConsoleColor.Cyan);
                    userService.GetAllUsers();
                    Helper.Print("Please enter the userId you want to block:", ConsoleColor.DarkCyan);
                    string userIdStr = Console.ReadLine();
                    if (!int.TryParse(userIdStr, out int userId))
                    {
                        Helper.Print("Please enter valid user Id!!", ConsoleColor.Red);
                        goto repeatUserId;
                    }
                    try
                    {
                        userService.BlockUser(userId);
                    }
                    catch (NotFoundException ex)
                    {
                        Helper.Print(ex.Message, ConsoleColor.Red);
                        goto repeatUserId;
                    }
                    goto AdminMenu;
                case "3":
                repeatUnblockUserId:
                    Helper.Print("Existing users:", ConsoleColor.Cyan);
                    userService.GetAllUsers();
                    Helper.Print("Please enter the userId you want to unblock:", ConsoleColor.DarkCyan);
                    string unblockUserIdStr = Console.ReadLine();
                    if (!int.TryParse(unblockUserIdStr, out int unblockUserId))
                    {
                        Helper.Print("Please enter valid user ID!!", ConsoleColor.Red);
                        goto repeatUnblockUserId;
                    }
                    try
                    {
                        userService.UnblockUser(unblockUserId);
                    }
                    catch (NotFoundException ex)
                    {
                        Helper.Print(ex.Message, ConsoleColor.Red);
                        goto repeatUnblockUserId;
                    }
                    goto AdminMenu;
                case "0":
                    Helper.Print("Logging out...", ConsoleColor.DarkGreen);
                    activeUser = null;
                    goto Menu;
                default:
                    Helper.Print("Please enter valid command", ConsoleColor.Red);
                    goto AdminMenu;
            }
        }
        else
        {

        UserMenu:
            Helper.Print(
                "1.Create new Category\n" +
                "2.Create new Medicine\n" +
                "3.Remove a medicine\n" +
                "4.List all medicines\n" +
                "5.Update medicine\n" +
                "6.Find medicine by id\n" +
                "7.Find medicine by Name\n" +
                "8.Find medicine by category\n" +
                "9.List all categories\n" +
                "10.Reset Password\n" +
                "0.Log out", ConsoleColor.Cyan);

            string loginUserCommand = Console.ReadLine();


            switch (loginUserCommand)
            {
                case "1":
                    Helper.Print("Please enter category name:", ConsoleColor.DarkCyan);
                    string categoryName = Console.ReadLine();
                    Category category = new(categoryName, activeUser.Id);
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
                repeatCategory:
                    int categoryId = GetValidCategoryID(activeUser.Id);
                    try
                    {
                        Medicine medicine = new(medicineName, price, DateTime.Now, categoryId, activeUser.Id);
                        medicineService.CreateMedicine(medicine);
                    }
                    catch (NotFoundException ex)
                    {
                        Helper.Print(ex.Message, ConsoleColor.Red);
                        goto repeatCategory;
                    }
                    catch (Exception ex)
                    {
                        Helper.Print(ex.Message, ConsoleColor.Red);
                        goto repeatPrice;
                    }
                    goto UserMenu;
                case "3":
                repeatMedID:
                    Helper.Print("Existing medicines:", ConsoleColor.DarkYellow);
                    medicineService.GetAllMedicines(activeUser.Id);
                    Helper.Print("Please enter the medicine ID you want to delete:", ConsoleColor.DarkCyan);
                    string medicineIdStr = Console.ReadLine();
                    if (!int.TryParse(medicineIdStr, out int medicineId))
                    {
                        Helper.Print("Please enter valid medicine ID!!", ConsoleColor.Red);
                        goto repeatMedID;
                    }
                    try
                    {
                        medicineService.RemoveMedicine(medicineId);
                    }
                    catch (NotFoundException ex)
                    {
                        Helper.Print(ex.Message, ConsoleColor.Red);
                        goto repeatMedID;
                    }
                    goto UserMenu;
                case "4":
                    medicineService.GetAllMedicines(activeUser.Id);
                    goto UserMenu;
                case "5":
                repeatNewMedicine:
                    Helper.Print("Existing medicines:", ConsoleColor.DarkYellow);
                    medicineService.GetAllMedicines(activeUser.Id);
                updateId:
                    Helper.Print("Please enter the ID of the medicine you want to update:", ConsoleColor.DarkCyan);
                    string idStr = Console.ReadLine();
                    if (!int.TryParse(idStr, out int id))
                    {
                        Helper.Print("Please enter a valid ID!", ConsoleColor.Red);
                        goto updateId;
                    }
                    Medicine existingMedicine = null;
                    try
                    {
                        existingMedicine = medicineService.GetMedicineById(id, activeUser.Id);
                        if (existingMedicine.UserId != activeUser.Id)
                        {
                            Helper.Print("You can only update your own medicines.", ConsoleColor.Red);
                            goto repeatNewMedicine;
                        }
                    }
                    catch (NotFoundException ex)
                    {
                        Helper.Print(ex.Message, ConsoleColor.Red);
                        goto repeatNewMedicine;
                    }
                    Helper.Print("Please enter the new name for the medicine:", ConsoleColor.DarkCyan);
                    string newName = Console.ReadLine();
                newPrice:
                    Helper.Print("Please enter the new price for the medicine:", ConsoleColor.DarkCyan);
                    string newPriceStr = Console.ReadLine();
                    if (!decimal.TryParse(newPriceStr, out decimal newPrice))
                    {
                        Helper.Print("Please enter a valid price!", ConsoleColor.Red);
                        goto newPrice;
                    }
                    int newCategoryId = GetValidCategoryID(activeUser.Id);
                    try
                    {
                        Medicine newMedicine = new(newName, newPrice, DateTime.Now, newCategoryId, 0);
                        medicineService.UpdateMedicine(id, newMedicine);
                        Helper.Print("Medicine updated successfully!", ConsoleColor.Green);
                    }
                    catch (NotFoundException ex)
                    {
                        Helper.Print(ex.Message, ConsoleColor.Red);
                        goto repeatNewMedicine;
                    }
                    catch (Exception ex)
                    {
                        Helper.Print(ex.Message, ConsoleColor.Red);
                        goto newPrice;
                    }
                    goto UserMenu;
                case "6":
                medId:
                    Helper.Print("Please enter medicine ID:", ConsoleColor.DarkCyan);
                    string medIdStr = Console.ReadLine();
                    if (!int.TryParse(medIdStr, out int medId))
                    {
                        Helper.Print("Please enter a valid ID!", ConsoleColor.Red);
                        goto medId;
                    }
                    try
                    {
                        Medicine med = medicineService.GetMedicineById(medId, activeUser.Id);
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
                case "7":
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
                    int searchCategoryId = GetValidCategoryID(activeUser.Id);
                    try
                    {
                        medicineService.GetMedicineByCategory(searchCategoryId, activeUser.Id);
                    }
                    catch (NotFoundException ex)
                    {
                        Helper.Print(ex.Message, ConsoleColor.Red);
                    }

                    goto UserMenu;
                case "9":
                    categoryService.GetAllCategories(activeUser.Id);
                    goto UserMenu;
                case "10":
                    Helper.Print("Enter new password:", ConsoleColor.DarkCyan);
                    string newPassword = Console.ReadLine();

                    try
                    {
                        userService.ResetPassword(activeUser.Id, newPassword);
                    }
                    catch (NotFoundException ex)
                    {
                        Helper.Print(ex.Message, ConsoleColor.Red);
                    }

                    goto UserMenu;
                case "0":
                    Helper.Print("Logging out...", ConsoleColor.DarkGreen);
                    activeUser = null;
                    goto Menu;
                default:
                    Helper.Print("Please enter valid command", ConsoleColor.Red);
                    goto UserMenu;
            }
        }
    }


    public static int GetValidCategoryID(int userId)
    {
    categoryIdRepeat:
        Helper.Print("Existing categories:", ConsoleColor.Green);
        foreach (var category in DB.Categories)
        {
            if (category.UserId == userId)
            {
                Helper.Print($"ID: {category.Id} - Name: {category.Name}", ConsoleColor.DarkCyan);
            }
            else
            {
                Helper.Print("No categories found for this user.", ConsoleColor.Yellow);
            }
        }
        Helper.Print("Please enter medicine categoryId:", ConsoleColor.DarkCyan);
        string categoryIdStr = Console.ReadLine();
        if (!int.TryParse(categoryIdStr, out int categoryId))
        {
            Helper.Print("Please enter a valid categoryId!", ConsoleColor.Red);
            goto categoryIdRepeat;
        }
        foreach (var category in DB.Categories)
        {
            if (category.Id == categoryId)
            {
                return categoryId;
            }
        }

        Helper.Print("Category ID does not exist. Please enter a valid category ID.", ConsoleColor.Red);
        goto categoryIdRepeat;
    }

}
