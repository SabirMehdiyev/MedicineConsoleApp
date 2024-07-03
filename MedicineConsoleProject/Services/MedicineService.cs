using MedicineConsoleProject.ExceptionsFolder;
using MedicineConsoleProject.Models;
using MedicineConsoleProject.Utilities;


namespace MedicineConsoleProject.Services;

public class MedicineService
{
    public void CreateMedicine(Medicine medicine)
    {
        foreach (var item in DB.Categories)
        {
            if (medicine.CategoryId == item.Id)
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
                Helper.Print($"ID:{item.Id} - Name:{item.Name} - Price:{item.Price}", ConsoleColor.Cyan);
            }
        }

        throw new NotFoundException("Bele adli tapilmadi");
    }

    public void RemoveMedicine(int id)
    {
        for (int i = 0; i < DB.Medicines.Length; i++)
        { 
            var medicine = DB.Medicines[i];

            if (medicine.Id == id)
            {
                for (int j = i; j < DB.Medicines.Length - 1; j++)
                {
                    DB.Medicines[j] = DB.Medicines[j + 1];
                }

                Array.Resize(ref DB.Medicines, DB.Medicines.Length - 1);
                Helper.Print("Product successfully deleted", ConsoleColor.Red);
            }
        }
    }
    public void UpdateMedicine(int id,Medicine medicine)
    {
        foreach(var item in DB.Medicines)
        {
            if (item.Id == id)
            {
                item.Name = medicine.Name;
                item.Price = medicine.Price;
                item.CategoryId = medicine.CategoryId;
            }
        }
        throw new NotFoundException($"{id} id-li medicine tapilmadi");
    }
}
