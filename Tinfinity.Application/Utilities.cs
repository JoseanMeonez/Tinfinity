using System.Globalization;
using System.Text;

namespace Tinfinity.Utilities
{
	public class Utilities
	{
		public static string ReplaceNulls(string str) => str.Replace("\0", "").Trim();
		public static string BinToHex(string originalString)
		{
			// String to bytes
			byte[] bytes = Encoding.UTF8.GetBytes(originalString);

			// Bytes to hex
			string hexString = BitConverter.ToString(bytes).Replace("-", "");

			return hexString;
		}

		public static int HexToInt(string hexNumber)
		{
			// Reverse the hexnumber given
			string reversedHex = hexNumber.Substring(2) + hexNumber.Substring(0, 2);

			// Convert the hex value to int
			int decimalNumber = int.Parse(reversedHex, NumberStyles.HexNumber);

			return decimalNumber;
		}
	}
}
