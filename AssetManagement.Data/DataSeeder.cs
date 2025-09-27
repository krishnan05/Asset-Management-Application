using System;
using System.Linq;
using AssetManagement.Shared.Models;
using Microsoft.EntityFrameworkCore; // Required if you call context.SaveChanges() inside a migration seeder

namespace AssetManagement.Data
{
    public static class DataSeeder
    {
        public static void Seed(AssetManagementDbContext context)
        {
            if (!context.Employees.Any())
            {
                context.Employees.AddRange(
                    // FIX 1: Map properties to Name, Email, Department (from your model)
                    // (Assuming you did NOT add FullName, PhoneNumber, Designation, or Status to Employee)
                    new Employee 
                    { 
                        Name = "Alice Johnson", 
                        Department = "IT", 
                        Email = "alice.johnson@example.com", 
                        // If you DO need PhoneNumber, Designation, and Status, you MUST add them to AssetManagement.Shared/Models/Employee.cs first.
                    },
                    new Employee 
                    { 
                        Name = "Bob Smith", 
                        Department = "Finance", 
                        Email = "bob.smith@example.com", 
                    }
                );
            }

            if (!context.Assets.Any())
            {
                context.Assets.AddRange(
                    // FIX 2: Map properties to Name, SerialNumber, and Status (from your model)
                    // FIX 3: Assign the Status using the enum (AssetStatus.Available, NOT a string)
                    new Asset 
                    { 
                        Name = "Dell Laptop",         // Changed AssetName to Name
                        // AssetType, MakeModel, PurchaseDate, WarrantyExpiryDate, Condition, IsSpare, Specifications are removed as they are missing in your shared model.
                        SerialNumber = "SN12345", 
                        Status = AssetStatus.Assigned // Changed string "Available" to the enum value. Using Assigned here to match a common scenario.
                    },
                    new Asset 
                    { 
                        Name = "Office Chair",      // Changed AssetName to Name
                        SerialNumber = "SN67890", 
                        Status = AssetStatus.Available // Changed string "Available" to the enum value.
                    }
                );
            }

            context.SaveChanges();
        }
    }
}