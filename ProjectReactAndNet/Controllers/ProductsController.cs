using Microsoft.AspNetCore.Mvc;
using MyApp.Domain.Entities;
using ProjectReactAndNet.Application.Services;

namespace MyApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController(ProductService service, ILogger<ProductsController> logger) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<Product>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var products = await service.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<Product>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await service.GetByIdAsync(id);
        return product is null ? NotFound(new { message = $"Produto #{id} não encontrado" }) : Ok(product);
    }

    [HttpPost]
    [ProducesResponseType<Product>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await service.CreateAsync(product);
            logger.LogInformation("Produto criado: {Name}", product.Name);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao criar produto");
            return StatusCode(500, new { message = "Erro interno ao criar produto" });
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] Product product)
    {
        if (id != product.Id)
            return BadRequest(new { message = "ID da rota não confere com o corpo da requisição" });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existing = await service.GetByIdAsync(id);
        if (existing is null)
            return NotFound(new { message = $"Produto #{id} não encontrado" });

        try
        {
            await service.UpdateAsync(product);
            logger.LogInformation("Produto atualizado: #{Id}", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao atualizar produto #{Id}", id);
            return StatusCode(500, new { message = "Erro interno ao atualizar produto" });
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await service.GetByIdAsync(id);
        if (existing is null)
            return NotFound(new { message = $"Produto #{id} não encontrado" });

        try
        {
            await service.DeleteAsync(id);
            logger.LogInformation("Produto excluído: #{Id}", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao excluir produto #{Id}", id);
            return StatusCode(500, new { message = "Erro interno ao excluir produto" });
        }
    }
}
