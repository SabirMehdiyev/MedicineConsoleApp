namespace MedicineConsoleProject.Models;

public class User:BaseEntity
{
    private static int _id = 0;
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }

    public User(string fullName, string email,string password, bool isAdmin)
    {
        Id = ++_id;
        Fullname = fullName;
        Email = email;
        Password = password;
        IsAdmin = isAdmin;
    }
}
