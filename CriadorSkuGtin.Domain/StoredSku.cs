using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CriadorSkuGtin.Domain
{
	public class StoredSku : EntityBase
	{
		public StoredSku(string sku, string gtin)
		{
			Sku = sku;
			Gtin = gtin;
		}

		[JsonPropertyName("sku")]
		public string Sku { get; set; }
		[JsonPropertyName("gtin")]
		public string Gtin { get; set; }
	}
}
