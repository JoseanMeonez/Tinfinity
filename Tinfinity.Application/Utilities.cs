using System.Globalization;
using System.Text;
using Tinfinity.Application.Dtos;

namespace Tinfinity.Utilities
{
	public class Utilities
	{
		public static string ReplaceNulls(string str) => str.Replace("\0", "").Trim();
		public static int DecodeOneDigitNumber(string str) => int.Parse(BinToHex(str), NumberStyles.HexNumber);
		public static int DecodeTwoDigitNumber(string str) => HexToInt(BinToHex(str));
		public static int DecodeFourDigitNumber(string str) => HexToGreaterInt(BinToHex(str));
		public static string BinToHex(string originalString)
		{
			// Convert original string to hex byte by byte
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

		public static int HexToGreaterInt(string hexNumber)
		{
			// Reverse the hexnumber given
			string reversedHex = hexNumber.Substring(6, 2) + hexNumber.Substring(4, 2) + hexNumber.Substring(2, 2) + hexNumber.Substring(0, 2);

			// Convert the hex value to int
			int decimalNumber = int.Parse(reversedHex, NumberStyles.HexNumber);

			return decimalNumber;
		}
		public static List<EquipmentDto> GetEquipment(List<string> quantities, List<string> refined, List<string> names)
		{
			var response = new List<EquipmentDto>();
			for (int i = 0; i < 16; i++)
			{
				dynamic itemQuantity = DecodeTwoDigitNumber(quantities[i]);
				int itemRefine = DecodeOneDigitNumber(refined[i]);
				int itemName = DecodeOneDigitNumber(names[i]);

				if (itemQuantity == 0)
					itemQuantity = "(Vacío)";
				else
					itemQuantity = itemQuantity + 4000;

				EquipmentDto equipment = new EquipmentDto
				{
					Quantity = itemQuantity,
					Refined = itemRefine,
					Name = itemName,
				};

				response.Add(equipment);
			}

			return response;
		}
	}
}
