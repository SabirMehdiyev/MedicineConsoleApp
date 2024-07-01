namespace MedicineConsoleProject.Models;

public class Medicine:BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }

    public Medicine(string name,decimal price,DateTime createdDate,int categoryId, int userId)
    {
        Name = name;
        Price = price;
        CategoryId = categoryId;
        UserId = userId;
        CreatedDate = createdDate;
    }

}
