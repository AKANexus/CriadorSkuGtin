using System.Security.Principal;
using CriadorSkuGtin.Domain;
using CriadorSkuGtin.EntityFramework;
using CriadorSkuGtin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CriadorSkuGtin.EanStatic;

namespace CriadorSkuGtin.Controllers
{
	public class DadosController : Controller
	{
		private readonly DatabaseContext _context;

		public DadosController(DatabaseContext context)
		{
			_context = context;
		}
		[HttpGet]
		public async Task<IActionResult> GetFornecedoresContendo(string? q)
		{
			var a = await _context.Fabricantes!
				.Where(x => x.Descrição.Contains(q??""))
				.Select(x => new {id = x.Abreviatura, text = x.Descrição})
				.ToListAsync();
			var resposta = Json(new {results = a});
			return resposta;
		}

		[HttpGet]
		public async Task<IActionResult> GetGruposContendo(string? q)
		{
			var a = await _context.Grupos!
				.Where(x => x.Descrição.Contains(q??""))
				.Select(x => new {id = x.Abreviatura, text = x.Descrição})
				.ToListAsync();
			var resposta = Json(new {results = a});
			return resposta;
		}

		[HttpPost]
		public async Task<IActionResult> GetGruposPaged([FromBody] TableFilteringModel? filteringModel)
		{
            
			if (filteringModel is null
			    //|| filteringModel.columns is null
			    )
			{
				return BadRequest();
			}

			//if (filteringModel.columns.Any(x => x.filters.Any(y => y.filterTypeEnum is null)))
			//{
			//    return BadRequest();
			//}

			//if (filteringModel.columns.Any(x => x.filter is null))
			//{
			//	return BadRequest();
			//}

			//if (filteringModel.ordering is not null &&  filteringModel.ordering.Any(x => x.directionEnum is null))
			//{
			//	return BadRequest();
			//}

			var x = await _context.Grupos!
				.OrderBy(x=>x.Descrição)
				.Skip((filteringModel.pageNumber - 1) * (int)filteringModel.recordsPerPage)
				.Take(filteringModel.recordsPerPage)
				.ToListAsync();

			return Json(new
			{
				returnedData = x,
				currentPage = filteringModel.pageNumber,
				maxPages = Math.Ceiling(x.Count / (decimal)filteringModel.recordsPerPage),
				filteringModel.recordsPerPage,
				recordsTotal = x
			});
		}

		[HttpPost]
		public async Task<IActionResult> GetFornecedoresPaged([FromBody] TableFilteringModel? filteringModel)
		{
            
			if (filteringModel is null
			    //|| filteringModel.columns is null
			   )
			{
				return BadRequest();
			}

			//if (filteringModel.columns.Any(x => x.filters.Any(y => y.filterTypeEnum is null)))
			//{
			//    return BadRequest();
			//}

			//if (filteringModel.columns.Any(x => x.filter is null))
			//{
			//	return BadRequest();
			//}

			//if (filteringModel.ordering is not null &&  filteringModel.ordering.Any(x => x.directionEnum is null))
			//{
			//	return BadRequest();
			//}

			var x = await _context.Fabricantes!
				.OrderBy(x=>x.Descrição)
				.Skip((filteringModel.pageNumber - 1) * (int)filteringModel.recordsPerPage)
				.Take(filteringModel.recordsPerPage)
				.ToListAsync();

			return Json(new
			{
				returnedData = x,
				currentPage = filteringModel.pageNumber,
				maxPages = Math.Ceiling(x.Count / (decimal)filteringModel.recordsPerPage),
				filteringModel.recordsPerPage,
				recordsTotal = x
			});
		}

		[HttpGet]
		public async Task<string> GetNewGtin()
		{
			var b = await _context.GtinStorage.AsNoTracking().FirstAsync();
			b.Sequential++;
			_context.GtinStorage.Update(b);
			await _context.SaveChangesAsync();
			string gtinNoDigit = $"7898181{b.Sequential:D5}";
			return ($"{gtinNoDigit}{gtinNoDigit.GetCheckSum()}");
		}

		[HttpPost]
		public async Task<IActionResult> PostFabricante([FromBody] Fabricante? fabricante)
		{
			if (fabricante is null)
			{
				return BadRequest();
			}

			fabricante.Abreviatura = fabricante.Abreviatura.PadRight(3, '-');
			var preExistente = await _context.Fabricantes!.AnyAsync(x=>x.Abreviatura == fabricante.Abreviatura);

			if (preExistente)
			{
				return Conflict("Abreviatura já utilizada");
			}
			fabricante.Uuid = Guid.Empty;

			_context.Fabricantes!.Update(fabricante);
			await _context.SaveChangesAsync();
			
			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> PostGrupo([FromBody] Grupo? grupo)
		{
			if (grupo is null)
			{
				return BadRequest();
			}

			grupo.Abreviatura = grupo.Abreviatura.PadRight(3, '-');
			var preExistente = await _context.Fabricantes!.AnyAsync(x=>x.Abreviatura == grupo.Abreviatura);

			if (preExistente)
			{
				return Conflict("Abreviatura já utilizada");
			}
			grupo.Uuid = Guid.Empty;

			_context.Grupos!.Update(grupo);
			await _context.SaveChangesAsync();
			
			return Ok();
		}
		
		[HttpGet]
		public async Task<IActionResult> GetExistingSkuByGtin([FromQuery] string? gtin)
		{
			var data = await _context.Skus!.FirstOrDefaultAsync(x => x.Gtin == gtin);
			if (data is null)
			{
				return NotFound();
			}

			return Json(data);
		}

		[HttpPost]
		public async Task<IActionResult> PostNewSku([FromBody] StoredSku? newSku)
		{
			if (newSku is null)
			{
				return BadRequest();
			}
			newSku.Uuid = Guid.Empty;
			_context.Skus!.Update(newSku);
			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> RemoverFornecedor(string? uuid)
		{
			if (uuid is null) return NoContent();
			var tentativo = await _context.Fabricantes!.FirstOrDefaultAsync(x=>x.Uuid == Guid.Parse(uuid));
			if (tentativo is null) return NoContent();
			_context.Fabricantes!.Remove(tentativo);
			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> RemoverGrupo(string? uuid)
		{
			if (uuid is null) return NoContent();
			var tentativo = await _context.Grupos!.FirstOrDefaultAsync(x=>x.Uuid == Guid.Parse(uuid));
			if (tentativo is null) return NoContent();
			_context.Grupos!.Remove(tentativo);
			await _context.SaveChangesAsync();
			return Ok();
		}

	}
}
