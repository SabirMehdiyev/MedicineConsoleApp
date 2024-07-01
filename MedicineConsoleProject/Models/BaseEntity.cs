namespace MedicineConsoleProject.Models;

public abstract class BaseEntity
{
    private static int _id;
    public int Id { get; set; }
    protected BaseEntity()
    {
       Id = ++_id;
    }
}
