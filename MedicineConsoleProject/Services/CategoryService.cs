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
}
