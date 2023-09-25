using System.Text;

namespace Tinfinity.Utilities
{
	public class Utilities
	{
		public static string ReplaceNulls(string str) => str.Replace("\0", "").Trim();
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
