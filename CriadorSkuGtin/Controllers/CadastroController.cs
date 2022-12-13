using System.Diagnostics;
using CriadorSkuGtin.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CriadorSkuGtin.Controllers;

public class CadastroController : Controller
{
	public IActionResult Index()
	{
		return View();
	}

	public IActionResult Grupos()
	{
		return View();
	}

	public IActionResult Fornecedores()
	{
		return View();
	}


}