using System.Text.Json.Serialization;

namespace CriadorSkuGtin.Domain
{
    public class Fabricante : EntityBase
    {
	    public Fabricante(string descrição, string abreviatura)
	    {
		    Descrição = descrição;
		    Abreviatura = abreviatura;
	    }
		[JsonPropertyName("descricao")]
	    public string Descrição { get; set; }
	    [JsonPropertyName("abreviatura")]
	    public string Abreviatura { get; set; }

    }
}