using AutoMapper;
using System.Globalization;
using Tinfinity.Application.Dtos;
using Tinfinity.Domain.Enums;
using static Tinfinity.Utilities.Utilities;

namespace Tinfinity.Application.Features.User.Queries
{
	public class GetCharacter
	{
		private readonly IMapper _mapper;

		public GetCharacter(IMapper mapper)
		{
			_mapper = mapper;
		}

		public async static Task<CharacterDto> GetCharacterData(string name)
		{
			CharacterDto charInfo = null;

			try
			{
				// Here we put the url were the files are staged
				using (FileStream fileStream = new FileStream(
					MainEnums.CharTadsUrl + $"{name}.TAD", FileMode.Open, FileAccess.Read))

				using (StreamReader streamReader = new StreamReader(fileStream))
				{
					// Read the first line of the document
					string tad = await streamReader.ReadLineAsync();

					if (tad != null)
					{
						string character = ReplaceNulls(tad.Substring(88, 18));
						string god = GetGodName(
							int.Parse(Bin2Hex(tad.Substring(144, 1)), NumberStyles.Integer));
						string charClass = GetClassName(
							HexToInt(Bin2Hex(tad.Substring(116, 1)), 16));

						charInfo = new CharacterDto
						{
							Name = character,
							God = god,
							Class = charClass
						};
					}
				}
			}
			catch (Exception e)
			{
				charInfo = new CharacterDto
				{
					Message = $"No se encontró el personaje: {name}",
					Completed = false
				};
			}

			return charInfo;
		}

		public static string GetGodName(int god)
		{
			string godname;
			switch (god)
			{
				case 1:
					godname = "Brahma";
					break;
				case 2:
					godname = "Vishnu";
					break;
				case 4:
					godname = "Shiva";
					break;
				default:
					godname = "Ninguno";
					break;
			}

			return godname;
		}

		public static string GetClassName(int charClass)
		{
			string classname;
			switch (charClass)
			{
				case 1:
					classname = "Bárbaro";
					break;
				case 2:
					classname = "Valkirya";
					break;
				case 4:
					classname = "Asesino";
					break;
				case 8:
					classname = "Amazona";
					break;
				case 16:
					classname = "Fighter";
					break;
				case 32:
					classname = "Akira";
					break;
				case 64:
					classname = "Mago";
					break;
				case 128:
					classname = "Sorceress";
					break;
				default:
					classname = "GM";
					break;
			}

			return classname;
		}
	}
}
