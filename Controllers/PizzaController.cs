using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    {
    }

    // Get all pizzas
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() =>
        PizzaService.GetAll();

    // Get a specific pizza by ID
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza == null)
            return NotFound();

        return pizza;
    }

    // Create a new pizza
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
    }

    // Update an existing pizza
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();

        var existingPizza = PizzaService.Get(id);
        if (existingPizza is null)
            return NotFound();

        PizzaService.Update(pizza);
        return NoContent();
    }

    // Delete a pizza
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza is null)
            return NotFound();

        PizzaService.Delete(id);
        return NoContent();
    }

    // Generate a sales summary report
    [HttpGet("GenerateSalesReport")]
    public IActionResult GenerateSalesReport()
    {
        // Define the output file path
        string outputFilePath = Path.Combine(Directory.GetCurrentDirectory(), "SalesSummaryReport.txt");

        // Generate the sales summary report
        PizzaService.GenerateSalesSummaryReport(outputFilePath);

        // Return a message indicating the report was generated
        return Ok($"Sales summary report generated at: {outputFilePath}");
    }
}