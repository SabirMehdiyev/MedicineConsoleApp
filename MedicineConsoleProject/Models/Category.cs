namespace MedicineConsoleProject.Models;

public class Category:BaseEntity
{
    private static int _id = 0;
    public string Name { get; set; }
    public int UserId { get; set; }

    public Category(string name, int userId)
    {
        Id = ++_id;
        Name = name;       
        UserId = userId;    
    }
}
