using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Domain;
using Infrastructure;

namespace Presentation.WebApp.Controllers;

public class VentasController : Controller
{
    private readonly VentasDbContext _VentasDbContext;
    private readonly ProductosDbContext _productosDbContext;
    private readonly ClientesDbContext _clientesDbContext;
    public VentasController(IConfiguration configuration)
    {
        _VentasDbContext = new VentasDbContext(configuration.GetConnectionString("DefaultConnection"));
        _clientesDbContext = new ClientesDbContext(configuration.GetConnectionString("DefaultConnection"));
        _productosDbContext = new ProductosDbContext(configuration.GetConnectionString("DefaultConnection"));
    }

    //[Authorize]
    public IActionResult Index()
    {
        var data = _VentasDbContext.List();
        return View(data);
    }

    //[Authorize]
    public IActionResult Details(Guid id)
    {
        var data = _VentasDbContext.Details(id);
        return View(data);
    }

    //[Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        ViewBag.Clientes = _clientesDbContext.List();
        ViewBag.Productos = _productosDbContext.List();
        return View();
    }
    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public IActionResult Create(Venta data)
    {
        _VentasDbContext.Create(data);
        return RedirectToAction("Index");
    }

    //[Authorize(Roles = "Admin")]
    public IActionResult Edit(Guid id)
    {
        var data = _VentasDbContext.Details(id);
        return View(data);
    }
    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public IActionResult Edit(Venta data)
    {
        _VentasDbContext.Edit(data);
        return RedirectToAction("Index");
    }

    //[Authorize(Roles = "Admin")]
    public IActionResult Delete(Guid id)
    {
        _VentasDbContext.Delete(id);
        return RedirectToAction("Index");
    }
}