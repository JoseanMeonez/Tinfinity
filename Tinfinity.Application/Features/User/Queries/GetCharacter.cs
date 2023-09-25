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
						string god = GodName(
							int.Parse(Bin2Hex(tad.Substring(144, 1)), NumberStyles.HexNumber));
						string tribe = TribeName(
							int.Parse(Bin2Hex(tad.Substring(116, 1)), NumberStyles.HexNumber));
						string charClass = ClassName(
							int.Parse(Bin2Hex(tad.Substring(154, 1)), NumberStyles.Integer));
						int lvl = int.Parse(Bin2Hex(tad.Substring(145, 1)), NumberStyles.HexNumber);

						charInfo = new CharacterDto
						{
							Name = character,
							God = god,
							Tribe = tribe,
							Class = charClass,
							Level = lvl
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

		public static string GodName(int god)
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

		public static string TribeName(int tribe)
		{
			string tribename;
			switch (tribe)
			{
				case 1:
					tribename = "Bárbaro";
					break;
				case 2:
					tribename = "Valkirya";
					break;
				case 4:
					tribename = "Asesino";
					break;
				case 8:
					tribename = "Amazona";
					break;
				case 16:
					tribename = "Fighter";
					break;
				case 32:
					tribename = "Akira";
					break;
				case 64:
					tribename = "Mago";
					break;
				case 128:
					tribename = "Sorceress";
					break;
				default:
					tribename = "GM";
					break;
			}

			return tribename;
		}

		public static string ClassName(int charClass)
		{
			string classname;
			switch (charClass)
			{
				case 0:
					classname = "Satya";
					break;
				case 1:
					classname = "Banar";
					break;
				case 2:
					classname = "Druka";
					break;
				case 3:
					classname = "Karya";
					break;
				case 4:
					classname = "Nakayuda";
					break;
				case 5:
					classname = "Vidya";
					break;
				case 6:
					classname = "Abikara";
					break;
				case 7:
					classname = "Samabat";
					break;
				default:
					classname = "GM";
					break;
			}
			return classname;
		}
	}
}
