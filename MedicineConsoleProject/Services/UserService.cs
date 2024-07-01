using MedicineConsoleProject.ExceptionsFolder;
using MedicineConsoleProject.Models;

namespace MedicineConsoleProject.Services;

public class UserService
{
    public User Login(string email , string password)
    {
        foreach (var user in DB.Users)
        {
            if (user.Email == email)
            {
                if (user.Password == password) 
                {
                    return user;
                }
            }
        }
        throw new NotFoundException("User tapilmadi");

    }
    public void AddUser(User user)
    {
        Array.Resize(ref DB.Users, DB.Users.Length+1);
        DB.Users[DB.Users.Length-1] = user;
    }
    public void CreateCategory(Category category)
    {
        Array.Resize(ref DB.Categories, DB.Categories.Length + 1);
        DB.Categories[DB.Categories.Length - 1] = category;
    }

}
