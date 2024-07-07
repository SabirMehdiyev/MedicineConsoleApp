using MedicineConsoleProject.Models;
using MedicineConsoleProject.Utilities;

namespace MedicineConsoleProject.Services;

public class CategoryService
{
    public void CreateCategory(Category category)
    {
        Array.Resize(ref DB.Categories, DB.Categories.Length + 1);
        DB.Categories[DB.Categories.Length - 1] = category;
        Helper.Print("Category created successfully", ConsoleColor.Green);
    }
    public void GetAllCategories(int userId)
    {
        bool isCategoriesFound = false;
        foreach (var item in DB.Categories)
        {
            if (item.UserId == userId)
            {
                isCategoriesFound = true;
                Helper.Print("-----------------------------", ConsoleColor.White);
                Helper.Print($"{item.Id}-{item.Name}", ConsoleColor.White);
                Helper.Print("-----------------------------", ConsoleColor.White);
            }
        }
        if (!isCategoriesFound)
        {
            Helper.Print("No categories found for this user.", ConsoleColor.Yellow);
        }
    }
}
