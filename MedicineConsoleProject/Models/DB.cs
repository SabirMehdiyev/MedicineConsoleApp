namespace MedicineConsoleProject.Models;

public static class DB
{
    public static User[] Users;
    public static Category[] Categories;
    public static Medicine[] Medicines;

    static DB()
    {

        Users = new User[0];
        Categories = new Category[0];
    }

}
