using AutoMapper;
using System.Globalization;
using System.Text;
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
			CharacterDto? charInfo = null;

			try
			{
				// Here we put the url were the files are staged
				using (FileStream fileStream = new FileStream(MainEnums.CharTadsUrl + $"{name}.TAD", FileMode.Open, FileAccess.Read))
				{
					// Read the first line of the document
					using (BinaryReader reader = new BinaryReader(fileStream))
					{
						// Define the buffer size to read data
						int bufferSize = 7124;
						byte[] buffer = new byte[bufferSize];
						int bytesRead = reader.Read(buffer, 0, bufferSize);

						string tad = Encoding.Latin1.GetString(buffer, 0, bytesRead);

						// Simple values
						string character = ReplaceNulls(tad.Substring(88, 18));
						int lvl = int.Parse(BinToHex(tad.Substring(145, 1)), NumberStyles.HexNumber);
						int zoneId = int.Parse(BinToHex(tad.Substring(150, 1)), NumberStyles.HexNumber);
						int gold = HexToGreaterInt(BinToHex(tad.Substring(132, 4)));
						int godPoints = HexToGreaterInt(BinToHex(tad.Substring(136, 4)));
						int pet = HexToInt(BinToHex(tad.Substring(1136, 2)));
						int petLevel = int.Parse(BinToHex(tad.Substring(1279, 1)), NumberStyles.HexNumber);

						// Equiped Items
						var quantityList = new List<string>();
						var refinedList = new List<string>();
						var namesList = new List<string>();
						for (int i = 0; i < 16; i++)
						{
							quantityList.Add(tad.Substring(1520, 2));
							refinedList.Add(tad.Substring(1528, 1));
							namesList.Add(tad.Substring(1535, 1));
						}
						var equipment = GetEquipment(quantityList, refinedList, namesList);

						// Codified Names
						string god = GodName(
						int.Parse(BinToHex(tad.Substring(144, 1)),
						NumberStyles.HexNumber));
						string tribe = TribeName(
							int.Parse(BinToHex(tad.Substring(116, 1)),
							NumberStyles.HexNumber));
						string charClass = ClassName(
							int.Parse(BinToHex(tad.Substring(155, 1)),
							NumberStyles.HexNumber));
						string regenerationZone = ZoneName(
							int.Parse(BinToHex(tad.Substring(151, 1)),
							NumberStyles.HexNumber));

						var zone = new ZoneDto
						{
							Name = ZoneName(zoneId),
							X = HexToInt(BinToHex(tad.Substring(160, 2))),
							Y = HexToInt(BinToHex(tad.Substring(162, 2))),
						};

						var chakras = new ChakrasDto
						{
							Muscle = HexToInt(BinToHex(tad.Substring(108, 2))),
							Nerve = HexToInt(BinToHex(tad.Substring(110, 2))),
							Heart = HexToInt(BinToHex(tad.Substring(112, 2))),
							Mental = HexToInt(BinToHex(tad.Substring(114, 2))),
						};

						charInfo = new CharacterDto
						{
							Name = character,
							God = god,
							Tribe = tribe,
							Level = lvl,
							Class = charClass,
							Zone = zone,
							RegenerationZone = regenerationZone,
							Chakras = chakras,
							Gold = gold,
							GodPoints = godPoints,
							Pet = pet,
							PetLvl = petLevel,
							Equipment = equipment,
						};
					}
				}
			}
			catch (Exception e)
			{
				charInfo = new CharacterDto
				{
					Message = $"No se encontró el usuario: {name}",
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

		public static string ZoneName(int zone)
		{
			string zoneName;

			switch (zone)
			{
				case 1:
					zoneName = "Aztlalan";
					break;
				case 2:
					zoneName = "Uxmal";
					break;
				case 3:
					zoneName = "Calabozo del jinete - Primera Sala";
					break;
				case 4:
					zoneName = "Calabozo del jinete - Segunda Sala";
					break;
				case 5:
					zoneName = "Calabozo del Uxmal - Primera Sala";
					break;
				case 6:
					zoneName = "Calabozo de Uxmal - Segunda Sala";
					break;
				case 7:
					zoneName = "Jinata";
					break;
				case 8:
					zoneName = "Plandep";
					break;
				case 9:
					zoneName = "Exilio";
					break;
				case 10:
					zoneName = "Zona Dios (Nivel 60~Amara10)";
					break;
				case 11:
					zoneName = "Chaturanga";
					break;
				case 12:
					zoneName = "Entrada a Tumba del Emperador";
					break;
				case 13:
					zoneName = "Karaya Nivel Bajo";
					break;
				case 14:
					zoneName = "Karya Nivel Medio";
					break;
				case 15:
					zoneName = "Karya Nivel Alto";
					break;
				case 16:
					zoneName = "Nar Durga";
					break;
				case 17:
					zoneName = "Viryu";
					break;
				case 18:
					zoneName = "Atlan Bizarra";
					break;
				case 20:
					zoneName = "Zona Dios (Nivel 30~59)";
					break;
				case 21:
					zoneName = "Mudha";
					break;
				case 22:
					zoneName = "Forge";
					break;
				case 24:
					zoneName = "Ruins";
					break;
				case 26:
					zoneName = "Ciudad Barbaro";
					break;
				case 28:
					zoneName = "Calabozo de Ciudad Barbaro";
					break;
				default:
					zoneName = "Bugueado";
					break;
			}

			return zoneName;
		}
	}
}
