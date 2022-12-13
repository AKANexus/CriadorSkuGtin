#pragma warning disable CA1416
using System;
using System.Drawing;
using System.Drawing.Printing;
using Zen.Barcode;

Console.WriteLine("Hello, World!");
int volume = 1;
int maxVolumes = 99;
Font standardFont = new Font("Roboto Mono", 8);
Font boldFont = new Font("Roboto Mono", 8, FontStyle.Bold);

PrintDocument pd = new()
{
	PrintController = new StandardPrintController()
};
pd.PrinterSettings.PrinterName = "ELGIN L42Pro";
pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
pd.Print();

void pd_PrintPage(object sender, PrintPageEventArgs e)
{
	float linesPerPage = 0;
	float yPos = 0;
	float leftMargin = 8;
	float topMargin = 5;
	int count = 0;
	linesPerPage = 3;

	CodeEan13BarcodeDraw bdf = BarcodeDrawFactory.CodeEan13WithChecksum;
	var lineHeight = standardFont.GetHeight(e.Graphics);
	e.Graphics.DrawImage(bdf.Draw("789115007497", 40), 33f, 10f);
	e.Graphics.DrawString("7 891150 07497", standardFont, Brushes.Black, 24, 30f+lineHeight, new());
	e.Graphics.DrawString($"Data: {DateTime.Today:dd/MM/yy}", boldFont, Brushes.Black, 5, 33f+(lineHeight*2));
	e.Graphics.DrawString($"Vol: 001/050", boldFont, Brushes.Black, 5, 33f+(lineHeight*3));
}