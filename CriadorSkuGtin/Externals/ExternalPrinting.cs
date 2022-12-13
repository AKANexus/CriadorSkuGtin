#pragma warning disable CA1416
using System.Drawing;
using System.Drawing.Printing;
using Zen.Barcode;

namespace CriadorSkuGtin.Externals
{
	public class ExternalPrinting
	{
		private readonly string _gtin;
		private int _volumeAtual;
		private readonly int _maxVolumes;
		private readonly Font _standardFont = new("Roboto Mono", 8);
		private readonly Font _boldFont = new("Roboto Mono", 8, FontStyle.Bold);
		private readonly PrintDocument document;


		public ExternalPrinting(string gtin, int maxVolumes)
		{
			_gtin = gtin;
			_maxVolumes = maxVolumes;
			document = new();
			document.PrinterSettings.PrinterName = "ELGIN L42Pro";
			document.PrintPage += Document_PrintPage;
			}

		public void Print()
		{
			for (int i = 0; i < _maxVolumes; i++)
			{
				_volumeAtual = i + 1;
				document.Print();
			}
		}
		private void Document_PrintPage(object sender, PrintPageEventArgs e)
		{

			CodeEan13BarcodeDraw bdf = BarcodeDrawFactory.CodeEan13WithChecksum;

			var lineHeight = _standardFont.GetHeight(e.Graphics);
			
			e.Graphics.DrawImage(bdf.Draw("789115007497", 40), 33f, 10f);
			e.Graphics.DrawString($"{_gtin.Substring(0,1)} {_gtin.Substring(1, 6)} {_gtin.Substring(7, 5)}", _standardFont, Brushes.Black, 24, 30f + lineHeight, new());
			e.Graphics.DrawString($"Data: {DateTime.Today:dd/MM/yy}", _boldFont, Brushes.Black, 5, 33f + lineHeight * 2);
			e.Graphics.DrawString($"Vol: {_volumeAtual:D3}/{_maxVolumes}", _boldFont, Brushes.Black, 5, 33f + lineHeight * 3);
		}
	}
}
