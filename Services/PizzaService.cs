using ContosoPizza.Models;
namespace ContosoPizza.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class PizzaService
{
    static List<Pizza> Pizzas { get; }
    static int nextId = 3;

    static PizzaService()
    {
        Pizzas = new List<Pizza>
        {
            new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false, Price = 12.99m },
            new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true, Price = 14.99m }
        };
    }

    // Get all pizzas
    public static List<Pizza> GetAll() => Pizzas;

    // Get a specific pizza by ID
    public static Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

    // Add a new pizza
    public static void Add(Pizza pizza)
    {
        pizza.Id = nextId++;
        Pizzas.Add(pizza);
    }

    // Delete a pizza
    public static void Delete(int id)
    {
        var pizza = Get(id);
        if (pizza is null)
            return;

        Pizzas.Remove(pizza);
    }

    // Update an existing pizza
    public static void Update(Pizza pizza)
    {
        var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
        if (index == -1)
            return;

        Pizzas[index] = pizza;
    }

    // Generate a sales summary report
    public static void GenerateSalesSummaryReport(string outputFilePath)
    {
        // Calculate total sales using the actual prices
        decimal totalSales = Pizzas.Sum(pizza => pizza.Price);

        // Create a StringBuilder to build the report
        var reportBuilder = new StringBuilder();
        reportBuilder.AppendLine("Sales Summary");
        reportBuilder.AppendLine("----------------------------");
        reportBuilder.AppendLine($" Total Sales: {totalSales.ToString("C")}");
        reportBuilder.AppendLine();
        reportBuilder.AppendLine(" Details:");

        // Add details for each pizza
        foreach (var pizza in Pizzas)
        {
            reportBuilder.AppendLine($"  {pizza.Name}: {pizza.Price.ToString("C")}");
        }

        // Write the report to the specified file
        File.WriteAllText(outputFilePath, reportBuilder.ToString());
    }
}