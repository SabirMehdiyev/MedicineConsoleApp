using MedicineConsoleProject.ExceptionsFolder;
using MedicineConsoleProject.Models;
using System.Xml;

namespace MedicineConsoleProject.Services;

public class MedicineService
{
    public void CreateMedicine(Medicine medicine)
    {
        foreach (var item in DB.Categories)
        {
            if (medicine.Id == item.Id)
            {
                Array.Resize(ref DB.Medicines, DB.Medicines.Length + 1);
                DB.Medicines[DB.Medicines.Length - 1] = medicine;
            }
        }
        throw new NotFoundException("Category tapilmadi");
    }

    public Medicine[] GetAllMedicines()
    {
        return DB.Medicines;
    }
    public Medicine GetMedicineById(int id)
    {
        foreach (var item in DB.Medicines)
        {
            if (item.Id == id)
            {
                return item;
            }
        }

        throw new NotFoundException("Bele id-li tapilmadi");
    }
    public Medicine GetMedicineByName(string name)
    {
        foreach (var item in DB.Medicines)
        {
            if (item.Name == name)
            {
                return item;
            }
        }

        throw new NotFoundException("Bele adli tapilmadi");
    }
    public void GetMedicineByCategory(int categoryId)
    {
        foreach (var item in DB.Medicines)
        {
            if (item.CategoryId == categoryId)
            {
                Console.WriteLine($"ID:{item.Id} - Name:{item.Name} - Price:{item.Price}");
            }
        }

        throw new NotFoundException("Bele adli tapilmadi");
    }

    public void UpdateMedicine(int id,Medicine medicine)
    {
        foreach(var item in DB.Medicines)
        {
            if (item.Id == id)
            {
                item.Name = medicine.Name;
            }
        }
    }
}
