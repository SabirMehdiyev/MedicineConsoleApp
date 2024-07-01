namespace MedicineConsoleProject.Models;

public class User:BaseEntity
{
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public User(string fullName,string email,string password)
    {
        Fullname = fullName;
        Email = email;
        Password = password;
    }
}
