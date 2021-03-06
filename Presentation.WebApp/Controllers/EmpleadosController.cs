using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Domain;
using Infrastructure;
using Application;

namespace Presentation.WebApp.Controllers;
public class EmpleadosController : Controller
{
    private readonly EmpleadosDbContext _empleadosDbContext;
    public EmpleadosController(IConfiguration configuration)
    {
        _empleadosDbContext = new EmpleadosDbContext(configuration.GetConnectionString("DefaultConnection"));
    }

    //[Authorize]
    public IActionResult Index()
    {
        var data = _empleadosDbContext.List();
        return View(data);
    }

    //[Authorize]
    public IActionResult Details(Guid id)
    {
        var data = _empleadosDbContext.Details(id);
        return View(data);
    }

    //[Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public IActionResult Create(Empleado data, IFormFile file)
    {
        if (file != null)
        {
            data.Foto = FileConverterService.ConvertToBase64(file.OpenReadStream());
        }
        _empleadosDbContext.Create(data);
        return RedirectToAction("Index");
    }

    //[Authorize(Roles = "Admin")]
    public IActionResult Edit(Guid id)
    {
        var data = _empleadosDbContext.Details(id);
        return View(data);
    }
    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public IActionResult Edit(Empleado data, IFormFile file)
    {
        if (file != null)
        {
            data.Foto = FileConverterService.ConvertToBase64(file.OpenReadStream());
        }
        _empleadosDbContext.Edit(data);
        return RedirectToAction("Index");
    }

    //[Authorize(Roles = "Admin")]
    public IActionResult Delete(Guid id)
    {
        _empleadosDbContext.Delete(id);
        return RedirectToAction("Index");
    }
}