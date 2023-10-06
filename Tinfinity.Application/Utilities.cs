using System.Globalization;
using System.Text;

namespace Tinfinity.Utilities
{
	public class Utilities
	{
		public static string ReplaceNulls(string str) => str.Replace("\0", "").Trim();
		public static string BinToHex(string originalString)
		{
			// Convierte la cadena binaria a una cadena hexadecimal sin omitir ningún byte
			StringBuilder hex = new StringBuilder(originalString.Length * 2);
			foreach (char c in originalString)
			{
				hex.AppendFormat("{0:X2}", (byte)c);
			}
			return hex.ToString();
		}

		public static int HexToInt(string hexNumber)
		{
			// Reverse the hexnumber given
			string reversedHex = hexNumber.Substring(2, 2) + hexNumber.Substring(0, 2);

			// Convert the hex value to int
			int decimalNumber = int.Parse(reversedHex, NumberStyles.HexNumber);

			return decimalNumber;
		}
	}
}
