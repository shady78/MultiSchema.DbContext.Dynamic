using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Interfaces;
using WebApplication3.Models;

namespace WebApplication3.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IGlobalParameterService _parameterService;
    private readonly ApplicationDbContext _context;
    private readonly IServiceProvider _serviceProvider;

    public ProductsController(
        IGlobalParameterService parameterService,
       ApplicationDbContext context,
       IServiceProvider serviceProvider)
    {
        _parameterService = parameterService;
        _context = context;
        _serviceProvider = serviceProvider;
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll([FromQuery] string schema)
    {

        try
        {
            _parameterService.SetSchemaName(schema);

            // إنشاء instance جديد من الـ DbContext في كل طلب
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var products = _context.Products.ToList();

            return Ok(new
            {
                products,
                schema = _parameterService.GetSchemaName()
            });
        }
        catch (Exception ex)
        {

            return Ok($"This Schema Not Found {ex.Message}");
        }
    }
}