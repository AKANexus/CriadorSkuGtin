using System.Data;
using System.Text.Json.Serialization;
using CriadorSkuGtin.Externals;
using Microsoft.AspNetCore.Mvc;
using ZXing;
using ZXing.Rendering;

namespace CriadorSkuGtin.Controllers;

public class PrintApiModel
{
	[JsonPropertyName("gtin")]
	public string? Gtin { get; set; }
	[JsonPropertyName("maxVolumes")]
	public int? MaxVolumes { get; set; }

	}

public class PrintingController : Controller
{


	[HttpPost]
	public IActionResult Print([FromBody]PrintApiModel? printInfo)
	{
		if (printInfo.MaxVolumes <= 0 || string.IsNullOrWhiteSpace(printInfo.Gtin))
		{
			return BadRequest();
		}
		ExternalPrinting ep = new(printInfo.Gtin, (int)printInfo.MaxVolumes!);
		ep.Print();
		return Ok();
	}

	[HttpGet]
	public IActionResult PrintableLabel([FromQuery] PrintApiModel? printInfo)
	{
		if (printInfo is null) return BadRequest();
		var bcWriter = new BarcodeWriterSvg()
		{
			Renderer = new SvgRenderer(),
			Format = BarcodeFormat.EAN_13
		};
		var aaa = bcWriter.Write(printInfo.Gtin);
		ViewData["volumes"] = printInfo.MaxVolumes;
		ViewData["svg"] = aaa.Content;
		return View();
	}
}