using System.Text;
using System.Linq;

namespace CriadorSkuGtin
{
	public static class EanStatic
	{
		public static int GetCheckSum(this string eanCode)
		{
			int[] digits = eanCode.Select(x => (int)Math.Floor(Char.GetNumericValue(x))).ToArray();
			int sumOfEvens = 0;
			int sumOfOdds = 0;
			for (int i = 0; i < digits.Length; i++)
			{
				if (i % 2 != 0) //even digit
				{
					sumOfEvens += digits[i];
				}
				else //odd digit
				{
					sumOfOdds += digits[i];
				}
			}
			decimal sumOfBoth = (sumOfEvens*3) + sumOfOdds;
			decimal remainder = sumOfBoth % 10;
			if (remainder == 0) return 0;
			else
			{
				return (int)(10 - remainder);
			}
		}

	}
}
