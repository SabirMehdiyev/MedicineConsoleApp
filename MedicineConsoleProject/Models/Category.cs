namespace MedicineConsoleProject.Models;

public class Category:BaseEntity
{
    private static int _id = 0;
    public string Name { get; set; }

    public Category(string name)
    {
        Id = ++_id;
        Name = name;
    }
}
