using System.Text;

namespace Tinfinity.Utilities
{
	public class Utilities
	{
		public static string HexToString(string hex)
		{
			int numberChars = hex.Length;
			byte[] bytes = new byte[numberChars / 2];

			for (int i = 0; i < numberChars; i += 2)
			{
				bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
			}

			return Encoding.UTF8.GetString(bytes).Replace("\0", "").Trim();
		}

		public static string bin2hex(string originalString)
		{
			// Convierte la cadena original a bytes
			byte[] bytes = Encoding.UTF8.GetBytes(originalString);

			// Convierte los bytes a una cadena hexadecimal
			string hexString = BitConverter.ToString(bytes).Replace("-", "");

			return hexString;
		}
	}
}
