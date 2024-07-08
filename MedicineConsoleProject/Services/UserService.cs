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
            if (user.IsBlocked)
            {
                throw new NotFoundException("This account is blocked.(Bextinizi bir daha sinayin :))");
            }
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
    public void ResetPassword(int userId, string newPassword)
    {
        foreach (var user in DB.Users)
        {
            if (user.Id == userId)
            {
                if (IsValidPassword(newPassword))
                {
                    user.Password = newPassword;
                    Helper.Print($"Password reset successfully for: {user.Email}", ConsoleColor.Green);
                }
                else
                {
                    Helper.Print("Password must be at least 8 characters, containing upper, lower case letters and digits.", ConsoleColor.Red);
                    return;
                }
            }
        }
    }


    public void AddUser(User user)
    {
        Array.Resize(ref DB.Users, DB.Users.Length + 1);
        DB.Users[DB.Users.Length - 1] = user;
    }
    public void BlockUser(int id)
    {
        foreach (var user in DB.Users)
        {
            if (user.Id == id)
            {
                if (user.IsAdmin)
                {
                    Helper.Print("Admin cannot be blocked.", ConsoleColor.Red);
                    return;
                }

                user.IsBlocked = true;
                Helper.Print("User successfully blocked", ConsoleColor.Green);
                return;
            }
        }
        throw new NotFoundException("User not exists, please enter existing userId!");
    }
    public void UnblockUser(int id)
    {
        foreach (var user in DB.Users)
        {
            if (user.Id == id)
            {
                user.IsBlocked = false;
                Helper.Print("User successfully unblocked", ConsoleColor.Green);
                return;
            }
        }

        throw new NotFoundException("User not exists, please enter existing userId!");
    }

    public void RegisterUser()
    {
        bool isAdmin = false;
    repeatAdmin:
        Helper.Print("Are you registering as admin? (Y/N)", ConsoleColor.DarkCyan);
        string isAdminSelect = Console.ReadLine().Trim().ToUpper();
        if (isAdminSelect.Length == 0)
        {
            Helper.Print("This input can't be empty.Please answer yes or no!", ConsoleColor.Red);
            goto repeatAdmin;
        }
        if (isAdminSelect == "Y" || isAdminSelect == "YES")
        {
            isAdmin = true;
        }
        else if (isAdminSelect == "N" || isAdminSelect == "NO")
        {
            isAdmin = false;
        }
        else
        {
            Helper.Print("Please enter valid command!", ConsoleColor.Red);
            goto repeatAdmin;
        }

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
            User user = new User(fullName, email, password, isAdmin);
            AddUser(user);
            Helper.Print("User registered successfully", ConsoleColor.Green);
        }
        else
        {
            Helper.Print("Password must be at least 8 characters long, containing upper, lower case letters and digits.", ConsoleColor.Red);
            goto repeatPassword;
        }
    }

    public void GetAllUsers()
    {
        foreach (var user in DB.Users)
        {
            string blockedMessage;
            if (user.IsBlocked) 
            {
                blockedMessage = "[BLOCKED]";
            }
            else
            {
                blockedMessage = "";
            }
            Helper.Print("-------------------------------------------------------------------------------------------------------------------", ConsoleColor.White);
            Helper.Print($"UserId:{user.Id} - Email:{user.Email} - Fullname:{user.Fullname} - Password:{user.Password} - Admin:{user.IsAdmin}  {blockedMessage}", ConsoleColor.White);
            Helper.Print("-------------------------------------------------------------------------------------------------------------------", ConsoleColor.White);
        }
    }

    public void RemoveUser(int id)
    {
        for (int i = 0; i < DB.Users.Length; i++)
        {
            var user = DB.Users[i];

            if (user.Id == id)
            {
                if (user.IsAdmin)
                {
                    Helper.Print("Admin cannot be removed.", ConsoleColor.Red);
                    return;
                }
                for (int j = i; j < DB.Users.Length - 1; j++)
                {
                    DB.Users[j] = DB.Users[j + 1];
                }

                Array.Resize(ref DB.Users, DB.Users.Length - 1);
                Helper.Print("User successfully deleted", ConsoleColor.Green);
                return;
            }
        }

        throw new NotFoundException("User not exists, please enter existing userId!");
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
