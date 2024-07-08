namespace MedicineConsoleProject.Models;

public class Medicine:BaseEntity
{
    private decimal price;
    private static int _id = 0;
    public string Name { get; set; }
    public decimal Price 
    {
        get => price;
        set
        {
            if (value<0)
            {
                throw new Exception("Price can't be negative");
            }
            price = value;
        }
    }
    public int CategoryId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }

    public Medicine(string name,decimal price,DateTime createdDate,int categoryId, int userId)
    {
        Id = ++_id;
        Name = name;
        Price = price;
        CategoryId = categoryId;
        UserId = userId;
        CreatedDate = createdDate;
    }

}
