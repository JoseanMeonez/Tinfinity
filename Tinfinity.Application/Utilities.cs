using System.Text;

namespace Tinfinity.Utilities
{
	public class Utilities
	{
		public static string ReplaceNulls(string str) => str.Replace("\0", "").Trim();
		public static int HexToInt(string str, int fromBase = 16)
		{
			int len = str.Length;
			int power = 1; // Initialize
						   // power of base
			int num = 0; // Initialize result
			int i;

			// Decimal equivalent is
			// str[len-1]*1 + str[len-2] *
			// base + str[len-3]*(base^2) + ...
			for (i = len - 1; i >= 0; i--)
			{
				// A digit in input number
				// must be less than
				// number's base
				if (val(str[i]) >= fromBase)
				{
					Console.WriteLine("Invalid Number");
					return -1;
				}

				num += val(str[i]) * power;
				power = power * fromBase;
			}

			return num;
		}
		private static int val(char c)
		{
			if (c >= '0' && c <= '9')
				return (int)c - '0';
			else
				return (int)c - 'A' + 10;
		}

		public static string Bin2Hex(string originalString)
		{
			// String to bytes
			byte[] bytes = Encoding.UTF8.GetBytes(originalString);

			// Bytes to hex
			string hexString = BitConverter.ToString(bytes).Replace("-", "");

			return hexString;
		}
	}
}
