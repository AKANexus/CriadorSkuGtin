using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriadorSkuGtin.Domain
{
	public class NextGtinGenerator
	{
		[Key]
		public int Id { get; set; }
		public int Sequential { get; set; } = 1;
	}
}
