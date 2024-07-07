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
                Helper.Print("Medicine created successfully!", ConsoleColor.Green);
                return;
            }
        }
        throw new NotFoundException("Category tapilmadi");
    }

    public  void GetAllMedicines(int userId)
    {
        bool isMedicinesFound = false;
        foreach (var item in DB.Medicines)
        {
            if (item.UserId == userId) 
            {
                isMedicinesFound = true;
                Category category1 = new("");
                foreach (var category in DB.Categories)
                {
                    if (category.Id == item.CategoryId)
                    {
                        category1 = category;
                        break;
                    }
                }
                Helper.Print("----------------------------------------------------------------------------------------------------------", ConsoleColor.White);
                Helper.Print($"Id:{item.Id} - Medicine:{item.Name} - Price:{item.Price}- Category:{category1.Name}- CreatedDate/Time:{item.CreatedDate}", ConsoleColor.White);
                Helper.Print("----------------------------------------------------------------------------------------------------------", ConsoleColor.White);
            }
        }

        if (!isMedicinesFound)
        {
            Helper.Print("No medicines found for this user.", ConsoleColor.Yellow);
        }
    }
    public Medicine GetMedicineById(int id, int userId)
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
    public Medicine GetMedicineByName(string name, int userId)
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
    public void GetMedicineByCategory(int categoryId, int userId)
    {
        bool isFound = false;

        foreach (var item in DB.Medicines)
        {
            if (item.CategoryId == categoryId && item.UserId == userId)
            {
                Helper.Print($"MedicineId: {item.Id} - Name: {item.Name} - Price: {item.Price}", ConsoleColor.White);
                isFound = true;
            }
        }
        if (!isFound)
        {
            throw new NotFoundException("Bele adli tapilmadi");
        }
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
                Helper.Print("Medicine successfully deleted", ConsoleColor.Green);
                return;
            }
        }

        throw new NotFoundException("Medicine not found");
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
                return;
            }
        }
        throw new NotFoundException($"{id} id-li medicine tapilmadi");
    }
}
