using MedicineConsoleProject.ExceptionsFolder;
using MedicineConsoleProject.Models;
using MedicineConsoleProject.Utilities;

namespace MedicineConsoleProject.Services;

public class UserService
{
    public User Login(string email, string password)
    {
        foreach (var user in DB.Users)
        {
            if (user.Email == email)
            {
                if (user.Password == password)
                {
                    Helper.Print("Successful login", ConsoleColor.Green);
                    return user;
                }
            }
        }
        throw new NotFoundException("Invalid email or password");

    }
    public void AddUser(User user)
    {
        Array.Resize(ref DB.Users, DB.Users.Length + 1);
        DB.Users[DB.Users.Length - 1] = user;
    }

    public void RegisterUser()
    {
    repeatFullName:
        Helper.Print("Please enter fullName", ConsoleColor.DarkCyan);
        string fullName = Console.ReadLine().Trim();
        if (fullName.Length == 0)
        {
            Helper.Print("Fullname can't be empty!", ConsoleColor.Red);
            goto repeatFullName;
        }
    repeatEmail:
        Helper.Print("Please enter email", ConsoleColor.DarkCyan);
        string email = Console.ReadLine();
        if (!email.Contains("@"))
        {
            Helper.Print("Invalid email. Please try again.", ConsoleColor.Red);
            goto repeatEmail;
        }
        foreach (var user in DB.Users)
        {
            if (email.Trim() == user.Email)
            {
                Helper.Print("This email used by another user. Please enter a different email.", ConsoleColor.Red);
                goto repeatEmail;
            }
        }
    repeatPassword:
        Helper.Print("Please enter password:", ConsoleColor.DarkCyan);
        string password = Console.ReadLine();

        if (IsValidPassword(password))
        {
            User user = new User(fullName, email, password);
            AddUser(user);
            Helper.Print("User registered successfully", ConsoleColor.Green);
        }
        else
        {
            Helper.Print("Password must be at least 8 characters long, containing upper, lower case letters and digits.", ConsoleColor.Red);
            goto repeatPassword;
        }
    }
    static bool IsValidPassword(string password)
    {
        bool hasUpper = false, hasLower = false, hasDigit = false;

        if (password.Length >= 8)
        {
            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpper = true;
                if (char.IsLower(c)) hasLower = true;
                if (char.IsDigit(c)) hasDigit = true;

                if (hasUpper && hasLower && hasDigit) return true;
            }
        }
        return false;
    }

}
